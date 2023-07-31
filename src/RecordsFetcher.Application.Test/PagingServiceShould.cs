using AutoFixture;
using FluentAssertions;
using Moq;
using RecordsFetcher.Application.CachingService;
using RecordsFetcher.Application.RemoteDataService;
using RemoteServer.Models;

namespace RecordsFetcher.Application.Test
{
    public class PagingServiceShould
    {
        private readonly Fixture fixture;
        private readonly Mock<IRemoteDataService> remoteDataServiceMock;
        private readonly Mock<ICachingService> cachingServiceMock;
        private readonly PagingService pagingService;

        public PagingServiceShould()
        {
            fixture = new Fixture();
            remoteDataServiceMock = new Mock<IRemoteDataService>();
            cachingServiceMock = new Mock<ICachingService>();
            pagingService = new PagingService(remoteDataServiceMock.Object, cachingServiceMock.Object);
        }

        [Fact]
        public void GetRecords_ReturnsCorrectRecords()
        {
            // Arrange
            var records = CreateSampleWith6DataRecords();
            ConfigureRemoteDataServiceToReturn(records);
            ConfigureCachingServiceToReturn(records);
            int pageNumber = 1;
            int resultsPerPage = 6;

            // Act
            var actualRecords = pagingService.GetRecords(pageNumber, resultsPerPage);

            // Assert
            actualRecords.Should().BeEquivalentTo(records);
            remoteDataServiceMock.Verify(m => m.GetRecords(
                It.IsAny<ServerDateTime>(),
                It.IsAny<ServerDateTime>(),
                It.IsAny<int>()), Times.AtLeastOnce);
        }


        private DataRecord[] CreateSampleWith6DataRecords()
        {
            var sample = new List<DataRecord>();
            var numberOfRecords = 6;
            for (var i = 1; i <= numberOfRecords; i++)
            {
                sample.Add(new DataRecord
                {
                    ID = i, CreationDate = CreateServerDateTime(milliseconds: i),
                    Data = fixture.Create<byte[]>()
                });
            }

            return sample.ToArray();
        }

        private ServerDateTime CreateServerDateTime(int milliseconds)
        {
            return ServerDateTime.addMilliseconds(ServerDateTime.getMinValue(), milliseconds);
        }

        private void ConfigureRemoteDataServiceToReturn(DataRecord[] records)
        {
            remoteDataServiceMock
                .Setup(m => m.GetRecords(It.IsAny<ServerDateTime>(), It.IsAny<ServerDateTime>(), It.IsAny<int>()))
                .Returns<ServerDateTime, ServerDateTime, int>((_, _, _) => records);
        }

        private void ConfigureCachingServiceToReturn(DataRecord[] records)
        {
            cachingServiceMock
                .SetupSequence(m => m.GetRecords(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => null)
                .Returns(() => records);

            cachingServiceMock
                .SetupSequence(m => m.Count())
                .Returns(0)
                .Returns(records.Length);
        }
    }
}
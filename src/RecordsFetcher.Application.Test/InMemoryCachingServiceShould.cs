using FluentAssertions;
using RecordsFetcher.Application.CachingService;
using RemoteServer.Models;

namespace RecordsFetcher.Application.Test;

public class InMemoryCachingServiceShould
{
    private readonly InMemoryCachingService inMemoryCachingService;
    private readonly DataRecord[] dataRecords;

    public InMemoryCachingServiceShould()
    {
        inMemoryCachingService = new InMemoryCachingService();
        dataRecords = new[]
        {
            new DataRecord {CreationDate = CreateServerDateTime(-1)},
            new DataRecord {CreationDate = CreateServerDateTime(-2)},
            new DataRecord {CreationDate = CreateServerDateTime(-3)},
            new DataRecord {CreationDate = CreateServerDateTime(-4)},
            new DataRecord {CreationDate = CreateServerDateTime(-5)}
        };
    }

    [Fact]
    public void StoreRecords_ShouldIncreaseCount()
    {
        inMemoryCachingService.StoreRecords(dataRecords);
        var count = inMemoryCachingService.Count();
        count.Should().Be(dataRecords.Length);
    }

    [Fact]
    public void ReturnCorrectRecordsPerPage()
    {
        inMemoryCachingService.StoreRecords(dataRecords);

        var pageNumber = 2;
        var resultsPerPage = 2;

        var records = inMemoryCachingService.GetRecords(pageNumber, resultsPerPage)!;
        records.Length.Should().Be(resultsPerPage);
        records[0].Should().BeEquivalentTo(dataRecords[(pageNumber - 1) * resultsPerPage]);
    }

    [Fact]
    public void GetRecords_WithOutOfRange_ShouldReturnNull()
    {
        inMemoryCachingService.StoreRecords(dataRecords);

        var pageNumber = 3;
        var resultsPerPage = 2;

        var records = inMemoryCachingService.GetRecords(pageNumber, resultsPerPage);
        records.Should().BeNull();
    }

    [Fact]
    public void StoreRecords_ShouldNotStoreDuplicates()
    {
        var creationDate = ServerDateTime.getCurrentTime();
        var duplicateRecords = new[]
        {
            new DataRecord {CreationDate = ServerDateTime.addMilliseconds(creationDate, 0)},
            new DataRecord {CreationDate = ServerDateTime.addMilliseconds(creationDate, 0)}
        };

        inMemoryCachingService.StoreRecords(duplicateRecords);
        inMemoryCachingService.Count().Should().Be(1);
    }
    
    private static ServerDateTime CreateServerDateTime(int milliseconds)
    {
        return ServerDateTime.addMilliseconds(ServerDateTime.getCurrentTime(), milliseconds);
    }
}
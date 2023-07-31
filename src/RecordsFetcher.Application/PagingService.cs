using RecordsFetcher.Application.CachingService;
using RecordsFetcher.Application.Exceptions;
using RecordsFetcher.Application.RemoteDataService;
using RemoteServer.Models;

namespace RecordsFetcher.Application
{
    public class PagingService
    {
        private readonly IRemoteDataService remoteDataService;
        private readonly ICachingService cachingService;
        
        private ServerDateTime oldestRetrievedRecordDate = ServerDateTime.getCurrentTime();
        private readonly ServerDateTime minServerDateTime = ServerDateTime.getMinValue();
        private readonly int maxValuesToRetrieveFromServer = 50;

        public PagingService(
            IRemoteDataService remoteDataService,
            ICachingService cachingService)
        {
            this.remoteDataService = remoteDataService;
            this.cachingService = cachingService;
        }
        
        public DataRecord[] GetRecords(int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            int endIndex = startIndex + resultsPerPage - 1;
            
            var dataRecords = cachingService.GetRecords(pageNumber, resultsPerPage);
            if (dataRecords != null) return dataRecords;

            PopulateCache(endIndex);

            dataRecords = cachingService.GetRecords(pageNumber, resultsPerPage)!;
            return dataRecords;
        }

        private void PopulateCache(int endIndex)
        {
            while (cachingService.Count() <= endIndex)
            {
                var serverRecords = remoteDataService.GetRecords(
                    minServerDateTime, oldestRetrievedRecordDate, maxValuesToRetrieveFromServer);
                if (serverRecords.Length == 0) 
                {
                    throw new RemoteServerError("No data received from the server.");
                }
                
                cachingService.StoreRecords(serverRecords);

                oldestRetrievedRecordDate = serverRecords.Last().CreationDate;
            }
        }
    }
}
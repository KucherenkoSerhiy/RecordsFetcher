using System;
using RemoteServer.Models;
using RemoteServer.Services;

namespace RecordsFetcher.Application
{
    public class PagingService
    {
        private readonly RemoteRecordRetriever remoteRecordRetriever;

        public PagingService(RemoteRecordRetriever remoteRecordRetriever)
        {
            this.remoteRecordRetriever = remoteRecordRetriever;
        }
        
        public DataRecord[] GetRecords(int pageNumber, int resultsPerPage)
        {
            return Array.Empty<DataRecord>();
        }
    }
}
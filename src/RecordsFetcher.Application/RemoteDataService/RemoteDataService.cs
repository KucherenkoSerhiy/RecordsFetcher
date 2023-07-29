using RemoteServer.Models;
using RemoteServer.Services;

namespace RecordsFetcher.Application.RemoteDataService
{
    public class RemoteDataService : IRemoteDataService
    {
        private readonly RemoteRecordRetriever remoteRecordRetriever;

        public RemoteDataService(RemoteRecordRetriever remoteRecordRetriever)
        {
            this.remoteRecordRetriever = remoteRecordRetriever;
        }

        public DataRecord[] GetRecords(ServerDateTime notBefore, ServerDateTime notAfter, int recordLimit)
        {
            return remoteRecordRetriever.getRemoteRecords(notBefore, notAfter, recordLimit);
        }
    }
}
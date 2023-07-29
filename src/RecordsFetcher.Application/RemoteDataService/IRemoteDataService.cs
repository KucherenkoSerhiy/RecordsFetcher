using RemoteServer.Models;

namespace RecordsFetcher.Application.RemoteDataService
{
    public interface IRemoteDataService
    {
        DataRecord[] GetRecords(ServerDateTime notBefore, ServerDateTime notAfter, int recordLimit);
    }
}
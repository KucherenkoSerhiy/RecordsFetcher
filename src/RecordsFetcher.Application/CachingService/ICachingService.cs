using RemoteServer.Models;

namespace RecordsFetcher.Application.CachingService;

public interface ICachingService
{
    void StoreRecords(DataRecord[] dataRecords);
    DataRecord[]? GetRecords(int pageNumber, int resultsPerPage);
    int Count();
}
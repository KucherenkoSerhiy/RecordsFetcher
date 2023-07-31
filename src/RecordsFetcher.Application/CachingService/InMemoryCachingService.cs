using RemoteServer.Models;

namespace RecordsFetcher.Application.CachingService;

public class InMemoryCachingService : ICachingService
{
    private readonly SortedDictionary<long, DataRecord> memory = new();
    
    public void StoreRecords(DataRecord[] dataRecords)
    {
        foreach (var dataRecord in dataRecords)
        {
            var creationDate = dataRecord.CreationDate.dateTime.Ticks;
            if (!memory.ContainsKey(creationDate))
                memory.Add(creationDate, dataRecord);
        }
    }
    
    public DataRecord[]? GetRecords(int pageNumber, int resultsPerPage)
    {
        int startIndex = (pageNumber - 1) * resultsPerPage;
        int endIndex = startIndex + resultsPerPage;

        if (endIndex >= memory.Count) return null;

        var recordsInRange = memory.Values.Skip(startIndex).Take(resultsPerPage).ToArray();

        return recordsInRange;
    }
    
    public int Count()
    {
        return memory.Count;
    }
}
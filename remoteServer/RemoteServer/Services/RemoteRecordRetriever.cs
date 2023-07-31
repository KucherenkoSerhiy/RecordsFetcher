using System;
using System.Linq;
using RemoteServer.Models;

namespace RemoteServer.Services
{
    public class RemoteRecordRetriever
    {
        private DataRecord[] dataRecords;
        private readonly int maxRecordLimit = 50;
        
        // Perform a remote lookup of data records from the server. 
        // Will return at most recordLimit (or 50 records if recordLimit is not in the range [1,50]) 
        // notBefore and notAfter are inclusive. 
        public DataRecord[] getRemoteRecords(ServerDateTime notBefore, ServerDateTime notAfter, int recordLimit)
        {
            var result = dataRecords.Where(r =>
                    r.CreationDate.DateTime >= notBefore.DateTime && r.CreationDate.DateTime <= notAfter.DateTime)
                .Take(Math.Min(recordLimit, maxRecordLimit))
                .ToArray();
            return result;
        }

        // for demo purposes only
        public void StoreRecords(DataRecord[] records)
        {
            dataRecords = records;
        }
    }
}
using System;
using RemoteServer.Models;

namespace RemoteServer.Services
{
    public class RemoteRecordRetriever
    {
        // Perform a remote lookup of data records from the server. 
        // Will return at most recordLimit (or 50 records if recordLimit is not in the range [1,50]) 
        // notBefore and notAfter are inclusive. 
        public DataRecord[] getRemoteRecords(ServerDateTime notBefore, ServerDateTime notAfter, int recordLimit)
        {
            throw new NotImplementedException();
        }
    }
}
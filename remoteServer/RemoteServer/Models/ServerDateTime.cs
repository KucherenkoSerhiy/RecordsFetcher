using System;

namespace RemoteServer.Models
{
    public class ServerDateTime
    {
        // Returns a ServerDateTime representing the current instant
        public static ServerDateTime getCurrentTime()
        {
            return new ServerDateTime(DateTime.UtcNow);
        }

        // Returns a ServerDateTime representing the earliest supported time
        public static ServerDateTime getMinValue()
        {
            return new ServerDateTime(DateTime.MinValue);
        }

        // Returns a ServerDateTime representing a time milliseconds after source
        public static ServerDateTime addMilliseconds(ServerDateTime source, int millis)
        {
            return new ServerDateTime(source.dateTime.AddMilliseconds(millis));
        }
        
        private readonly DateTime dateTime;

        private ServerDateTime(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }
    }
}
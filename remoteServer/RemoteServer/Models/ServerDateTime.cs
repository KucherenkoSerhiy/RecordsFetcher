using System;

namespace RemoteServer.Models
{
    public class ServerDateTime
    {
        public static ServerDateTime getCurrentTime()
        {
            return new ServerDateTime(DateTime.UtcNow);
        }

        public static ServerDateTime getMinValue()
        {
            return new ServerDateTime(DateTime.MinValue);
        }

        public static ServerDateTime addMilliseconds(ServerDateTime source, int millis)
        {
            return new ServerDateTime(source.DateTime.AddMilliseconds(millis));
        }
        
        public readonly DateTime DateTime;

        private ServerDateTime(DateTime dateTime)
        {
            this.DateTime = dateTime;
        }
    }
}
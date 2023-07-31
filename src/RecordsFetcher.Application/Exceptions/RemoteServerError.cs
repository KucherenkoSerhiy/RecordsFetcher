namespace RecordsFetcher.Application.Exceptions;

public class RemoteServerError : Exception
{
    public RemoteServerError() : base() { }
    public RemoteServerError(string message) : base(message) { }
    public RemoteServerError(string message, Exception innerException) : base(message, innerException) { }
}
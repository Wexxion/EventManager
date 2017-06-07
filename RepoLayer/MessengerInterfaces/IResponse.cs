namespace RepoLayer.MessengerInterfaces
{
    public interface IResponse
    {
        string Text { get; }
        ResponseStatus Status { get; set; }
    }

    public enum ResponseStatus
    {
        Abort,
        Close,
        Expect,
        Exception
    }
}

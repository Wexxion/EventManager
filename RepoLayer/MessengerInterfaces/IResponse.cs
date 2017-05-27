namespace TaskManager.RepoLayer.MessengerInterfaces
{
    public interface IResponse
    {
        string Text { get; }
        ResponseStatus Status { get; }
    }

    public enum ResponseStatus
    {
        Close,
        Expect,
        Exception
    }
}

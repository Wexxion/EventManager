using System.Collections.Generic;

namespace TaskManager.RepoLayer.MessengerInterfaces
{
    public interface IRequest
    {
        Entity Author { get; }
        string Raw { get; }
        List<string> Args { get; }
        string Command { get; }
    }
}

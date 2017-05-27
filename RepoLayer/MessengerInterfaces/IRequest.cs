using System.Collections.Generic;

namespace TaskManager.RepoLayer.MessengerInterfaces
{
    public interface IRequest
    {
        Entity Author { get; }
        string Command { get; }
    }
}

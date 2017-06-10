using System.Collections.Generic;
using RepoLayer.Session;

namespace AppLayer
{
    public interface ICommandLoader
    {
        IEnumerable<BaseBotSession> GetCommands();
    }
}
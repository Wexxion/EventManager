using System.Collections.Generic;
using System.Linq;
using TaskManager.RepoLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace RepoLayer.Session
{
    public abstract class BaseBotSession
    {
        public string Name { get; }
        protected BaseBotSession(string name)
        {
            this.Name = name;
        }
        public abstract IResponse Execute(IRequest message);
    }
}
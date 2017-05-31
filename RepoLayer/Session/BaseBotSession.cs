using System.Collections.Generic;
using System.Linq;
using RepoLayer.MessengerInterfaces;

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
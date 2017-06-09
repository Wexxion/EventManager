using System.Collections.Generic;
using System.Linq;
using RepoLayer.MessengerInterfaces;

namespace RepoLayer.Session
{
    public abstract class BotSession
    {
        public string Name { get; }
        protected BotSession(string name)
        {
            this.Name = name;
        }
        public abstract IResponse Execute(IRequest message);
    }
}
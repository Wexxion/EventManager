using System;
using RepoLayer.MessengerInterfaces;

namespace RepoLayer.Session
{
    public abstract class BaseBotSession
    {
        public string Name { get; }
        protected BaseBotSession(string name)
        {
            Name = name;
        }
        public abstract IResponse Execute(IRequest message);
    }
}
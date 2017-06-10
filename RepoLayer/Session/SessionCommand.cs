using System;
using RepoLayer.MessengerInterfaces;

namespace RepoLayer.Session
{
    public abstract class SessionCommand
    {
        public string Name { get; }
        public IResponse HelpResponse { get; }
        public bool NeedAnswer { get; }
        protected SessionCommand(string name, IResponse helpResponse, bool needAnswer = true)
        {
            Name = name;
            HelpResponse = helpResponse;
            NeedAnswer = needAnswer;
        }
        public abstract IResponse Apply(IRequest message);
    }
}
using TaskManager.RepoLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace RepoLayer.Session
{
    public abstract class SessionCommand
    {
        public string Name { get; }
        public string Help { get; }
        public bool NeedAnswer { get; }
        protected SessionCommand(string name, bool needAnswer = true) 
            : this(name, $"send {name}", needAnswer)
        {
        }
        protected SessionCommand(string name, string help, bool needAnswer = true)
        {
            Name = name;
            Help = help;
            NeedAnswer = needAnswer;
        }
        public abstract IResponse Apply(IRequest message);
    }
}
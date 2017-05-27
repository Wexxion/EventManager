using AddSession;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class ExitCommand : SessionCommand
    {
        private VEvent Event { get; set; }
        public ExitCommand(VEvent @event) : base("Exit", false)
        {
            Event = @event;
        }
        public override IResponse Apply(IRequest message)
        {
            Event = null;
            return new TextResponse("Exit",ResponseStatus.Close);
        }
    }
}
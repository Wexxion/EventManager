using AddSession;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class DescriptionCommand : SessionCommand
    {
        private VEvent Event { get; set; }
        public DescriptionCommand(VEvent @event) : base("Description", "Send event description")
        {
            Event = @event;
        }

        public override IResponse Apply(IRequest message)
        {
            Event.SetDescription(message.Command);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Expect);
        }
    }
}
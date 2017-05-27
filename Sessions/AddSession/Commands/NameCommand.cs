using System;
using AddSession;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    class NameCommand : SessionCommand
    {
        private VEvent Event { get; set; }
        public NameCommand(VEvent @event) : base("Name", "Send event name")
        {
            Event = @event;
        }
        public override IResponse Apply(IRequest message)
        {
            Event.SetName(message.Command);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Expect);
        }
    }
}
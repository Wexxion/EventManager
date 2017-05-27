using System;
using AddSession;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class FinishTimeCommand : SessionCommand
    {
        private VEvent Event { get; set; }
        public FinishTimeCommand(VEvent @event) 
            : base("Finish time", "Send finish time in format: dd.mm.yyyy hh:mm:ss")
        {
            Event = @event;
        }

        public override IResponse Apply(IRequest message)
        {
            DateTime date;
            if(!DateTime.TryParse(message.Command, out date))
                return new TextResponse($"{Emoji.CrossMark} Parse exception", ResponseStatus.Expect);
            Event.SetEndingTime(date);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Expect);
        }
    }
}
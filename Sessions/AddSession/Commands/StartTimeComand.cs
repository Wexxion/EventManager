using System;
using AddSession;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class StartTimeComand : SessionCommand
    {
        private VEvent Event { get; set; }
        public StartTimeComand( VEvent @event) 
            : base("Start time", "Send start time in format: dd.mm.yyyy hh:mm:ss")
        {
            Event = @event;
        }
        public override IResponse Apply( IRequest message)
        {
            DateTime date;
            if (!DateTime.TryParse(message.Command, out date))
                return new TextResponse($"{Emoji.CrossMark} Parse exception", ResponseStatus.Expect);
            Event.SetStaringTime(date);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Expect);
        }
    }
}
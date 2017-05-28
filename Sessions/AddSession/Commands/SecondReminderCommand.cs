using System;
using AddSession;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class SecondReminderCommand : SessionCommand
    {
        private VEvent Event { get; set; }
        public SecondReminderCommand(VEvent @event) 
            : base("Second Reminder", "Send second reminder in format: yyyy.hh:mm:ss")
        {
            Event = @event;
        }

        public override IResponse Apply(IRequest message)
        {
            TimeSpan date;
            if (!TimeSpan.TryParse(message.Command, out date))
                return new TextResponse($"{Emoji.CrossMark} Parse exception", ResponseStatus.Expect);
            Event.SetSecondReminder(date);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Expect);
        }
    }
}
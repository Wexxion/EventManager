using System;
using AddSession;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class FirstReminderCommand : SessionCommand
    {
        private VEvent Event { get; set; }
        public FirstReminderCommand(VEvent @event) 
            : base("First Reminder", "Send first reminder in format: dd.hh:mm:ss")
        {
            Event = @event;
        }

        public override IResponse Apply(IRequest message)
        {
            TimeSpan date;
            if (!TimeSpan.TryParse(message.Command, out date))
                return new TextResponse($"{Emoji.CrossMark} Parse exception", ResponseStatus.Expect);
            Event.SetFirstReminder(date);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Expect);
        }
    }
}
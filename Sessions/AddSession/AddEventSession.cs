using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AddSession.CustomCommands;
using AppLayer;
using DomainLayer;
using RepoLayer;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace AddSession
{
    [Export(typeof(BaseBotSession))]
    public class AddEventSession : BaseBotSession
    {
        private VEvent Event { get; set; }
        private IRepository<VEvent> EventStorage { get; set; }
        private SessionCommand ExpectedCommand { get; set; }
        private Dictionary<string, SessionCommand> Commands { get; set; }

        [ImportingConstructor]
        public AddEventSession([Import("EventStorage")]IRepository<VEvent> eventStorage) : base("Add event")
        {
            EventStorage = eventStorage;
        }

        private void CommandConfiguration()
        {
            Commands = new List<SessionCommand>
            {
                new BaseCommand("Name",new TextResponse($"Set event name{Emoji.Pointer}",ResponseStatus.Expect), r =>
                    {
                        Event.SetName(r.Command);
                        return new TextResponse(Emoji.CheckMark, ResponseStatus.Close);
                    }),
                new BaseCommand("Description", new TextResponse($"Set event description{Emoji.Pointer}",ResponseStatus.Expect), r =>
                {
                    Event.SetDescription(r.Command);
                    return new TextResponse(Emoji.CheckMark, ResponseStatus.Close);
                }),
                new SetDateTimeCommand("Start time",d => Event.SetStaringTime(d)),
                new SetDateTimeCommand("End time",d => Event.SetEndingTime(d)),
                new SetTimeSpanCommand("First Reminder",span => Event.SetFirstReminder(span)),
                new SetTimeSpanCommand("Second Reminder",span => Event.SetSecondReminder(span)),
                new BaseCommand("Exit",null, r =>
                {
                    Event = null;
                    return new TextResponse("Exit",ResponseStatus.Abort);
                },false),
                new BaseCommand("Save",null, r =>
                {
                    if(Event.Name == null || Name.Length == 0)
                        return new TextResponse(Emoji.CrossMark+"Add event name", ResponseStatus.Exception);
                    EventStorage.Add(Event);
                    return new TextResponse(Emoji.CheckMark, ResponseStatus.Abort);
                },false)
            }.ToDictionary(x => x.Name, x => x);
        }

        private string[] GetCommands()
        {
            if(ExpectedCommand != null)
                return Commands.Keys.Select(x => x == ExpectedCommand.Name ? Emoji.Pen + x : x).ToArray();
            return Commands.Keys.ToArray();
        }

        private IResponse OnStarting(IRequest message)
        {
            Event = new VEvent();
            Event.SetCreator((Person)message.Author);
            CommandConfiguration();
            return new ButtonResponse("Choose", GetCommands(), ResponseStatus.Expect);
        }

        private IResponse ResponseAnalizer(IResponse response, SessionCommand command)
        {
            switch (response.Status)
            {
                case ResponseStatus.Exception:
                    return new ButtonResponse(response.Text, GetCommands(), ResponseStatus.Expect);
                case ResponseStatus.Expect:
                    ExpectedCommand = command;
                    if (response is ButtonResponse)
                        return response;
                    return new ButtonResponse(response.Text, GetCommands(), ResponseStatus.Expect);
                case ResponseStatus.Close:
                    ExpectedCommand = null;
                    return new ButtonResponse(response.Text, GetCommands(), ResponseStatus.Expect);
            }
            return response;
        }

        public override IResponse Execute(IRequest message)
        {
            if (Event == null)
                return OnStarting(message);
            var text = message.Command;
            IResponse response;
            if (ExpectedCommand != null)
            {
                response = ExpectedCommand.Apply(message);
                return ResponseAnalizer(response, ExpectedCommand);
            }
            if (!Commands.ContainsKey(text))
                return new ButtonResponse($"Unsupported command", GetCommands(), ResponseStatus.Expect);
            var command = Commands[text];
            if (command.NeedAnswer)
                return ResponseAnalizer(command.HelpResponse, command);
            response = command.Apply(message);
            return ResponseAnalizer(response, command);
        }
    }
}

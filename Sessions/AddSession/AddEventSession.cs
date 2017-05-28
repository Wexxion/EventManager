using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using RepoLayer;
using RepoLayer.Session;
using TaskManager.AppLayer;
using TaskManager.AppLayer.Sessions.AddSession.Commands;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

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
                new NameCommand(Event),
                new DescriptionCommand(Event),
                new StartTimeComand(Event),
                new FinishTimeCommand(Event),
                new FirstReminderCommand(Event),
                new SecondReminderCommand(Event),
                new SaveCommand(Event,EventStorage),
                new ExitCommand(Event)
            }.ToDictionary(x => x.Name, x => x);
        }

        private string[] GetCommands()
        {
            if(ExpectedCommand != null)
                return Commands.Keys.Select(x => x == ExpectedCommand.Name ? Emoji.Pen + x : x).ToArray();
            return Commands.Keys.ToArray();
        }
        public override IResponse Execute(IRequest message)
        {
            if (Event == null)
            {
                Event = new VEvent();
                Event.SetCreator((Person) message.Author);
                CommandConfiguration();
                return new ButtonResponse("Choose", GetCommands(), ResponseStatus.Expect);
            }
            var text = message.Command;
            IResponse response;
            if (ExpectedCommand != null)
            {
                response = ExpectedCommand.Apply(message);
                ExpectedCommand = null;
                return new ButtonResponse(response.Text,GetCommands(),response.Status);
            }
            if (!Commands.ContainsKey(text))
                return new ButtonResponse($"Unsupported command", GetCommands(), ResponseStatus.Expect);
            if (!Commands[text].NeedAnswer)
            {
                response = Commands[text].Apply(message);
                if (response.Status == ResponseStatus.Exception)
                    return new ButtonResponse(response.Text, GetCommands(), ResponseStatus.Expect);
                Event = null;
                return response;
            }
            ExpectedCommand = Commands[text];
            return new ButtonResponse(ExpectedCommand.Help + "\r\n"+Emoji.Pointer, GetCommands(), ResponseStatus.Expect);
        }
    }
}

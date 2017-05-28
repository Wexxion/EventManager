using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;
using RepoLayer.Session;
using TaskManager.AppLayer;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace EventListSession
{
    [Export(typeof(BaseBotSession))]
    public class EventListSession : BaseBotSession
    {
        private IRepository<VEvent> EventStorage { get; set; }

        public EventListSession() : base("My events")
        {
            EventStorage = StorageFactory.GetRepository<VEvent>();
        }

        private string EventToString(VEvent e)
        {
            var builder = new StringBuilder();
            if (e.Name != null)
                builder.Append($"Name = {e.Name}\r\n");
            if (e.Description != null)
                builder.Append($"Description = {e.Description}\r\n");
            if (e.Start != default(DateTime))
                builder.Append($"Start = {e.Start}\r\n");
            if (e.End != default(DateTime))
                builder.Append($"End = {e.End}\r\n");
            if (e.FirstReminder != default(TimeSpan))
                builder.Append($"First Reminder = {e.FirstReminder}\r\n");
            if (e.SecondReminder != default(TimeSpan))
                builder.Append($"Second Reminder = {e.SecondReminder}\r\n");
            return  builder.ToString();
        }
        public override IResponse Execute(IRequest message)
        {
            if(message.Command == "back")
                return new TextResponse("Ok", ResponseStatus.Close);

            var author = (Person)message.Author;
            if (message.Command.StartsWith("/more"))
            {
                var idString =  new string(message.Command.Skip(5).ToArray());
                var id = int.Parse(idString);
                var e = EventStorage
                .GetAll(x => x.Creator.TelegramId == author.TelegramId)
                .FirstOrDefault(x => x.Id == id);
                if (e == null)
                    return new TextResponse($"event with id {id} not found", ResponseStatus.Expect);
                return new TextResponse(EventToString(e),ResponseStatus.Expect);
            }
            var events = EventStorage
                .GetAll(x => x.Creator.TelegramId == author.TelegramId).ToArray();
            if (events.Length == 0)
                return new TextResponse("you haven't events",ResponseStatus.Close);
            var resp = new StringBuilder();
            var i = 1;
            foreach (var @event in events)
            {
                resp.Append($"{i++}) {@event.Name} /more{@event.Id}\r\n");
            }
            //return new TextResponse(resp.ToString(),ResponseStatus.Close);
            return new ButtonResponse(resp.ToString(),new [] {"back"}, ResponseStatus.Expect);
        }
    }
}

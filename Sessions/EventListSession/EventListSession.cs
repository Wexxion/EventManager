using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using AppLayer;
using DomainLayer;
using RepoLayer;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace EventListSession
{
    [Export(typeof(BaseBotSession))]
    public class EventListSession : BaseBotSession
    {
        private IRepository<VEvent> EventStorage { get; set; }
        [ImportingConstructor]
        public EventListSession([Import("EventStorage")]IRepository<VEvent> eventStorage) : base("My events")
        {
            EventStorage = eventStorage;
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
                return e == null ? new TextResponse($"event with id {id} not found", ResponseStatus.Expect) : 
                    new TextResponse(e.ToString(),ResponseStatus.Expect);
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

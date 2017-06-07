using System.ComponentModel.Composition;
using AppLayer;
using DomainLayer;
using RepoLayer;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace DeleteEventsSession
{
    [Export(typeof(BaseBotSession))]
    public class DeleteEventsSession : BaseBotSession
    {
        private IRepository<VEvent> EventStorage { get; }
        [ImportingConstructor]
        public DeleteEventsSession([Import("EventStorage")]IRepository<VEvent> eventStorage) : base("Delete all events")
        {
            EventStorage = eventStorage;
        }

        public override IResponse Execute(IRequest message)
        {
            EventStorage.Delete(x => x.Creator.TelegramId == ((Person)message.Author).TelegramId);
            return new TextResponse("All your events have been deleted",ResponseStatus.Close);
        }
    }
}

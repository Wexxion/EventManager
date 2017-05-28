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

namespace DeleteEventsSession
{
    [Export(typeof(BaseBotSession))]
    public class DeleteEventsSession : BaseBotSession
    {
        private IRepository<VEvent> EventStorage { get; set; }
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

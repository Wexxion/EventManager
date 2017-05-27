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
        public DeleteEventsSession() : base("Delete all events")
        {
        }

        public override IResponse Execute(IRequest message)
        {
            StorageFactory.GetRepository<VEvent>()
                .Delete(x => x.Creator.TelegramId == ((Person)message.Author).TelegramId);
            return new TextResponse("All your events have been deleted",ResponseStatus.Close);
        }
    }
}

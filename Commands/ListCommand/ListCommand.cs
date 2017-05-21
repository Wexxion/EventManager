using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RepoLayer;
using TaskManager.AppLayer;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace ListCommand
{
    [Export(typeof(BaseCommand))]
    public class ListCommand  :  BaseCommand
    {
        public IRepository<VEvent> EventStorage;
        public ListCommand() : base("list")
        {
            EventStorage = StorageFactory.GetRepository<VEvent>();
        }

        [Pattern]
        public IResponse ShowEvents(IRequest msg)
        {
            var user = (Person)msg.Author;
            var events = EventStorage.GetAll(x => x.Creator.TelegramId == user.TelegramId).ToArray();
            var req = new StringBuilder();
            req.Append($"У вас {events.Count()} задач\r\n");
            var index = 1;
            foreach (var @event in events)
            {
                req.Append($"{index++}) event name : {@event.Name} \r\n" +
                           $"description : {@event.Description}\r\n" +
                           $"start : {@event.Start} \r\n" +
                           $"end : {@event.End} \r\n" +
                           $"first remind : {@event.FirstReminder} \r\n" +
                           $"second remind : {@event.SecondReminder} \r\n\r\n");
            }
            return new BaseResponse(req.ToString());
        }
    }
}

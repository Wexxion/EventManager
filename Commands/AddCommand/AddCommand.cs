using System;
using System.ComponentModel.Composition;
using System.Linq;
using Ninject;
using RepoLayer;
using TaskManager.AppLayer;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace AddCommand
{
    [Export(typeof(BaseCommand))]
    public class AddCommand : BaseCommand
    {
        private IRepository<VEvent> EventSorage { get; set; }

        public AddCommand() : base("add")
        {
            this.EventSorage = StorageFactory.GetRepository<VEvent>();
        }

        [Pattern(typeof(string[]))]
        public IResponse GetEventsInfo(IRequest msg)
        {
            var splitString = msg.Args;
            var person = (Person)msg.Author;
            var e = new VEvent(person, splitString[0]);
            e.SetDescription(splitString[1]);
            e.SetStaringTime(new DateTime(2010, 1, 2, 3, 4, 5, 6));
            e.SetEndingTime(new DateTime(2010, 2, 2, 3, 4, 5, 6));
            e.SetFirstReminder(new TimeSpan(0, 30, 0));
            e.SetSecondReminder(new TimeSpan(0, 15, 0));
            EventSorage.Add(e);
            return new BaseResponse("add new event");
        }
    }
}

using AddSession;
using RepoLayer;
using RepoLayer.Session;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class SaveCommand : SessionCommand
    {
        private VEvent Event { get; set; }
        private IRepository<VEvent> EventStorage { get; set; }
        public SaveCommand(VEvent @event, IRepository<VEvent> storage) : base("Save", false)
        {
            Event = @event;
            EventStorage = storage;
        }

        public override IResponse Apply(IRequest message)
        {
            if(Event.Name == null || Name.Length == 0)
                return new TextResponse(Emoji.CrossMark+"Add event name", ResponseStatus.Exception);
            EventStorage.Add(Event);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Close);
        }
    }
}
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

        public SaveCommand(VEvent @event) : base("Save", false)
        {
            Event = @event;
        }

        public override IResponse Apply(IRequest message)
        {
            if(Event.Name == null || Name.Length == 0)
                return new TextResponse(Emoji.CrossMark+"Add event name", ResponseStatus.Exception);
            StorageFactory.GetRepository<VEvent>().Add(Event);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Close);
        }
    }
}
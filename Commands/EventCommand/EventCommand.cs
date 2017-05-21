using System.ComponentModel.Composition;
using System.Linq;
using TaskManager.AppLayer;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace EventCommand
{
    [Export(typeof(BaseCommand))]
    public class EventCommand : BaseCommand
    {
        public EventCommand() : base("event")
        {
        }

        [Pattern(typeof(ListArg), typeof(string[]))]
        public IResponse GetEventsInfo(IRequest msg)
        {
            var message = msg.Args.Skip(1);
            return new BaseResponse("Don't shout at me!" +
                string.Join(" ", message.Select(x => x.ToUpper())));
        }
    }

    enum ListArg
    {
        List,
        Listed,
        SomeArg
    }
}

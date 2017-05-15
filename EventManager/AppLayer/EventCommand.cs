using System.Linq;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.Messages;

namespace TaskManager.AppLayer
{
    class EventCommand: BaseCommand
    {
        public EventCommand() : base("event")
        {
        }

        [Pattern(typeof(ListArg), typeof(string[]))]
        public Response GetEventsInfo(TgMessage msg)
        {
            var message = msg.Args.Skip(1); 
            return new Response("Don't shout at me!"  + 
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

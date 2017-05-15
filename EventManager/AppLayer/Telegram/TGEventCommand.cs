using System.Linq;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessagerInterfaces;

namespace TaskManager.AppLayer.Telegram
{
    class TGEventCommand: BaseCommand
    {
        public TGEventCommand() : base("event")
        {
        }

        [Pattern(typeof(ListArg), typeof(string[]))]
        public Response GetEventsInfo(IncomingMessage msg)
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

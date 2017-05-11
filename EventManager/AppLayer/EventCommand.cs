using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.Messages;

namespace TaskManager.AppLayer
{
    class EventCommand: BaseCommand
    {
        public EventCommand() : base("event")
        {
        }

        [Pattern("[listed: list]")]
        public Response GetEventsInfo(TgMessage msg)
        {
            return new Response("No info, should implement it!");
        }
    }
}

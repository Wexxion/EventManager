using TaskManager.RepoLayer;
using TaskManager.RepoLayer.Command;

namespace TaskManager.AppLayer
{
    class EventCommand: BaseCommand
    {
        public EventCommand() : base("event")
        {
        }

        [Pattern("[listed: list]")]
        public CommandResponse GetEventsInfo(TgMessage msg)
        {
            return new CommandResponse("No info, should implement it!");
        }
    }
}

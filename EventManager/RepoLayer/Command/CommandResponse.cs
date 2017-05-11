using TaskManager.RepoLayer.Messages;


namespace TaskManager.RepoLayer.Command
{
    public class CommandResponse : IResponsable
    {
        public string Text { get; }

        public CommandResponse(string responseText)
        {
            Text = responseText;
        }
    }
}

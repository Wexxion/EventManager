using TaskManager.RepoLayer.MessagerInterfaces;

namespace TaskManager.RepoLayer.Command
{
    public class Response : IResponsable
    {
        public string Text { get; }

        public Response(string responseText)
        {
            Text = responseText;
        }
    }
}

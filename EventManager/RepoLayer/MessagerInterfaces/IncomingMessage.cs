using System.Collections.Generic;
using System.Linq;
using TaskManager.DomainLayer;

namespace TaskManager.RepoLayer.MessagerInterfaces
{
    public class IncomingMessage
    {
        public Person Author { get; }
        public string Text { get; }
        public List<string> Args { get; }
        public string Command { get; }
        public IncomingMessage(string message)
        {
            Text = message;
            var splittedText = message.Split();
            Args = splittedText.Skip(1).ToList();
            Command = splittedText.First().Replace("/", "");
        }

        public IncomingMessage(Person author, string text)
        {
            Author = author;
            var splittedText = text.Split();
            Args = splittedText.Skip(1).ToList();
            Command = splittedText.First().Replace("/", "");
        }
    }
}

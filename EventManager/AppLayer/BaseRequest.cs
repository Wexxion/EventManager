using System.Collections.Generic;
using System.Linq;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer
{
    public class BaseRequest : IRequest
    {
        public Person Author { get; }
        public string Raw { get; }
        public List<string> Args { get; }
        public string Command { get; }
        Entity IRequest.Author => Author;

        public BaseRequest(Person author, string raw)
        {
            Author = author;
            Raw = raw;
            var splittedText = raw.Split();
            Args = splittedText.Skip(1).ToList();
            Command = splittedText.First().Replace("/", "");
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using RepoLayer;
using RepoLayer.MessengerInterfaces;

namespace AppLayer
{
    public class BaseRequest : IRequest
    {
        public Person Author { get; }
        public string Command { get; }
        Entity IRequest.Author => Author;
        public BaseRequest(Person author, string raw)
        {
            Author = author;
            Command = raw;
        }
    }
}

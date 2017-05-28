using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;
using RepoLayer.Session;
using TaskManager.AppLayer;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace ListSession
{
    [Export(typeof(BaseBotSession))]
    public class ListSession : BaseBotSession
    {
        public ListSession() : base("All users")
        {
        }

        public override IResponse Execute(IRequest message)
        {
            var allPersons = new StringBuilder();
            var persons = StorageFactory
                .GetRepository<Person>()
                .GetAll()
                .ToArray();
            if (persons.Length == 0)
                return new TextResponse("no authentificate users",ResponseStatus.Close);
            foreach (var person in persons)
            {
                allPersons.Append(person.TelegramId);
                allPersons.Append("\r\n");
            }
            return new TextResponse(allPersons.ToString(), ResponseStatus.Close);
        }
    }
}

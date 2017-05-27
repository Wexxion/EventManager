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

namespace LoginSession
{
    [Export(typeof(BaseBotSession))]
    public class LoginSession : BaseBotSession
    {
        private IRepository<Person> persons;
        public LoginSession() : base("Login")
        {
            persons = StorageFactory.GetRepository<Person>();
        }

        public override IResponse Execute(IRequest message)
        {
            var author = (Person)message.Author;
            var tAuthor = persons.Get(x => x.TelegramId == author.TelegramId);
            if (tAuthor != null)
                return new TextResponse("I already know you", ResponseStatus.Close);
            persons.Add(author);
            return new TextResponse("I remember you", ResponseStatus.Close);
        }
    }
}

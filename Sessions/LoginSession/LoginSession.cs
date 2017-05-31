using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLayer;
using DomainLayer;
using RepoLayer;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace LoginSession
{
    [Export(typeof(BaseBotSession))]
    public class LoginSession : BaseBotSession
    {
        private IRepository<Person> PersonsStorage { get; set; }
        [ImportingConstructor]
        public LoginSession([Import("PersonStorage")]IRepository<Person> personsStorage) : base("Login")
        {
            PersonsStorage = personsStorage;
        }

        public override IResponse Execute(IRequest message)
        {
            var author = (Person)message.Author;
            var tAuthor = PersonsStorage.Get(x => x.TelegramId == author.TelegramId);
            if (tAuthor != null)
                return new TextResponse("I already know you", ResponseStatus.Close);
            PersonsStorage.Add(author);
            return new TextResponse("I remember you", ResponseStatus.Close);
        }
    }
}

using System.ComponentModel.Composition;
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
                return new TextResponse("You've already logged in", ResponseStatus.Close);
            PersonsStorage.Add(author);
            return new TextResponse("You've successfully logged in", ResponseStatus.Close);
        }
    }
}

using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using AppLayer;
using DomainLayer;
using RepoLayer;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace ListSession
{
    [Export(typeof(BaseBotSession))]
    public class ListSession : BaseBotSession
    {
        private IRepository<Person> PersonsStorage { get; }
        [ImportingConstructor]
        public ListSession([Import("PersonStorage")]IRepository<Person> personStorage) : base("All users")
        {
            PersonsStorage = personStorage;
        }

        public override IResponse Execute(IRequest message)
        {
            var allPersons = new StringBuilder();
            var persons = PersonsStorage
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

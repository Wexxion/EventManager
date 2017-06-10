using System;
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
        private IRepository<Person> PersonsStorage { get; set; }
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
            for (var i = 0; i < persons.Length; i++)
                allPersons.Append($"{i + 1}) {persons[i].UserName}\n\r");
            return new TextResponse(allPersons.ToString(), ResponseStatus.Close);
        }
    }
}

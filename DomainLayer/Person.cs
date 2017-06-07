using RepoLayer;

namespace DomainLayer
{
    public class Person : Entity
    {
        public long TelegramId { get;}
        public string FirstName { get;}
        public string LastName { get;}
        public string UserName { get;}

        public Person() {}

        public Person(long telegramId, string firstName, string lastName, string username)
        {
            TelegramId = telegramId;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
        }
    }
}

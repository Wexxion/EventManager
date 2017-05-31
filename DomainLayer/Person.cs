using RepoLayer;

namespace DomainLayer
{
    public class Person : Entity
    {
        public long TelegramId { get;private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }

        public Person(){ }
        public Person(long telegramId, string firstName, string lastName, string username)
        {
            TelegramId = telegramId;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
        }
    }
}

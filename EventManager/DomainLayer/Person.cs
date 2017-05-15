using TaskManager.RepoLayer;

namespace TaskManager.DomainLayer
{
    public class Person : Entity
    {
        public long Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }

        public Person(long id, string firstName, string lastName, string username)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
        }
    }
}

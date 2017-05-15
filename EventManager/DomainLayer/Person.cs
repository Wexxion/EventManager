namespace TaskManager.DomainLayer
{
    public class Person
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }

        public Person(int id, string firstName, string lastName, string username)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
        }
    }
}

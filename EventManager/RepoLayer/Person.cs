using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.RepoLayer
{
    public class Person
    {
        public long Id { get; }  // это chatID
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public int TelegramId { get; } //хз как получить

        public Person(long id, string firstName, string lastName, string username)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
        }
    }
}

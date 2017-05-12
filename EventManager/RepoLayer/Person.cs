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
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public int TelegramId { get; }

        public Person(int telegramId, string firstName, string lastName, string username)
        {
            TelegramId = telegramId;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
        }
    }
}

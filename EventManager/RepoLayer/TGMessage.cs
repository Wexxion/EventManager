using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.RepoLayer
{
    public class TgMessage
    {
        public Person Author { get; }
        public string Text { get; }
        public List<string> Args => 
            Text.Split(
                new [] {" "}, 
                StringSplitOptions.RemoveEmptyEntries
                ).ToList();
        public TgMessage(string message)
        {
            Text = message;
        }

        public TgMessage(Person author, string text)
        {
            Author = author;
            Text = text;
        }
    }
}

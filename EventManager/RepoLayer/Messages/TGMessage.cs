using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.RepoLayer.Messages
{
    public class TgMessage
    {
        public Person Author { get; }
        public string Text { get; }
        public List<string> Args { get; }
        public string Command { get; }
        public TgMessage(string message)
        {
            Text = message;
            var splittedText = message.Split();
            Args = splittedText.Skip(1).ToList();
            Command = splittedText.First().Replace("/", "");
        }

        public TgMessage(Person author, string text)
        {
            Author = author;
            Text = text;
        }
    }
}

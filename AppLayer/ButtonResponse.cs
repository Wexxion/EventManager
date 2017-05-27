using System;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer
{
    public class ButtonResponse : IResponse
    {
        public ButtonResponse(string text, string[] buttonNames, ResponseStatus status)
        {
            if(buttonNames == null || buttonNames.Length == 0)
                throw new ArgumentException("buttonNames can't be empty");
            if (text.Length == 0)
                throw new ArgumentException("Empty response text");
            Text = text;
            ButtonNames = buttonNames;
            Status = status;
        }
        public string Text { get; }
        public string[] ButtonNames { get; }
        public ResponseStatus Status { get; }
    }
}
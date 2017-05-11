using System;
using TaskManager.AppLayer;
using TaskManager.RepoLayer.Command;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.RepoLayer.Messages
{
    class SimpleMessageHandler : IMessageHandler
    {
        private EventCommand EventCommand { get; }
        public SimpleMessageHandler()
        {
            EventCommand = new EventCommand();
        }
        public void AnalyseMessage(Message message)
        {
            if (message?.Type == MessageType.TextMessage)
                ProccessTextMessage(message);
        }

        public void ProccessTextMessage(Message message)
        {
            var info = message.Chat;
            var person = new Person(info.Id, info.FirstName, info.LastName, info.Username);
            var tgMessage = new TgMessage(person, message.Text);
            var a = EventCommand.Execute(tgMessage);
            Console.WriteLine(a.Text);
        }

        public void SendMessage(IResponsable massageData, IReplyMarkup replyMarkup)
        {
            
        }
    }
}

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
        private EventCommand EventCommand { get; } = new EventCommand();
        public IResponsable ProcessMessage(Message message)
        {
            if (message?.Type == MessageType.TextMessage)
                return ProccessTextMessage(message);
            throw new ArgumentException($"{message?.Type} Is not supported yet :(");
        }

        public IResponsable ProccessTextMessage(Message message)
        {
            var info = message.Chat;
            var person = new Person(info.Id, info.FirstName, info.LastName, info.Username);
            var tgMessage = new TgMessage(person, message.Text);
            var answer = EventCommand.Execute(tgMessage);
            return answer;
        }
    }
}

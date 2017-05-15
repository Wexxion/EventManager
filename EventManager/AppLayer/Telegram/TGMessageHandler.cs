using System;
using System.Collections.Generic;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessagerInterfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TaskManager.AppLayer.Telegram
{
    public class SimpleMessageHandler : IMessageHandler
    {
        private Dictionary<string, BaseCommand> EventCommandDict { get; }
        public IResponsable ProcessMessage(Message message)
        {
            if (message?.Type == MessageType.TextMessage)
                return ProccessTextMessage(message);
            throw new ArgumentException($"{message?.Type} Is not supported yet :(");
        }

        public SimpleMessageHandler()
        {
            EventCommandDict = new Dictionary<string, BaseCommand>
            {
                {"event", new TGEventCommand()}
            };
        }

        public IResponsable ProccessTextMessage(Message message)
        {
            var info = message.From;
            var person = new Person(info.Id, info.FirstName, info.LastName, info.Username);
            var tgMessage = new IncomingMessage(person, message.Text);
            if (!EventCommandDict.ContainsKey(tgMessage.Command))
                return new Response("No such command implemented!");

            try
            {
                return EventCommandDict[tgMessage.Command].Execute(tgMessage);
            }
            catch (ArgumentException)
            {
                return new Response("Incorrect command arguments!");
            }
        }
    }
}

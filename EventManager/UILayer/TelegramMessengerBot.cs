using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.AppLayer;
using TaskManager.DomainLayer;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TaskManager.UILayer
{
    public class TelegramMessengerBot : IMessengerBot
    {
        private TelegramBotClient Bot { get; set; }
        private MessageHandler Handler { get; }

        public TelegramMessengerBot()
        {
           Handler = new MessageHandler();
        }

        public void Initialize(string token)
        {
            Bot = new TelegramBotClient(token);
            Bot.OnMessage += BotOnMessageReceived;

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }
        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = AnalyzeIncomingMessage(messageEventArgs.Message);
            var answer = Handler.ProcessMessage(message).Text;
            await Bot.SendTextMessageAsync(message.Author.Id, answer);
        }

        private BaseRequest AnalyzeIncomingMessage(Message message)
        {
            var sender = message.Chat;
            var author = new Person(sender.Id, sender.FirstName, sender.LastName, sender.Username);
            return new BaseRequest(author, message.Text);
        }
    }
}

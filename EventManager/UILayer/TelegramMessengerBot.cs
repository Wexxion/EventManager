using System;
using TaskManager.AppLayer;
using TaskManager.DomainLayer;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
            var message = messageEventArgs.Message;
            try
            {
                var request = AnalyzeIncomingMessage(message);
                var answer = Handler.ProcessMessage(request).Text;
                await Bot.SendTextMessageAsync(message.Chat.Id, answer);
            }
            catch (ArgumentException e)
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, e.Message);
            }
        }

        private BaseRequest AnalyzeIncomingMessage(Message message)
        {
            if (message.Type != MessageType.TextMessage)
                throw new ArgumentException($"{message.Type} is not supported yet =[");
            var sender = message.Chat;
            var author = new Person(sender.Id, sender.FirstName, sender.LastName, sender.Username);
            return new BaseRequest(author, message.Text);
        }
    }
}

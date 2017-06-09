using System;
using AppLayer;
using DomainLayer;
using RepoLayer.MessengerInterfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace UILayer.Telegram
{
    public class TelegramMessengerBot : IMessengerBot
    {
        private TelegramBotClient Bot { get;}
        private SessionHandler SessionHandler { get; }
        private TelegramResponseHandler ResponseHandler { get; }
        private Reminder Reminder { get; }
        public event Action<IRequest> OnRequest;
        public event Action<Exception> OnError;
        public TelegramMessengerBot(TelegramBotClient bot, SessionHandler sessionHandler, Reminder reminder, TelegramResponseHandler responseHandler)
        {
            Reminder = reminder;
            Bot = bot;
            SessionHandler = sessionHandler;
            ResponseHandler = responseHandler;
        }

        public void Start()
        {
            Bot.StartReceiving();
            Bot.OnMessage += BotOnMessageReceived;
            Reminder.OnRemind += BotOnRemind;
            Reminder.Start();
        }
        public void Stop()
        {
            Bot.StopReceiving();
        }
        private async void BotOnRemind(IResponse response)
        {
            if (response is RemindResponse)
            {
                var remindResponse = (RemindResponse) response;
                await Bot.SendTextMessageAsync(remindResponse.RemindId, remindResponse.Text);
            }
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            try
            {
                var request = AnalyzeIncomingMessage(message);
                OnRequest?.Invoke(request);
                var response = ResponseHandler.ResponseAnalyzer(SessionHandler.ProcessMessage(request));
                await Bot.SendTextMessageAsync(message.Chat.Id, response.Text, replyMarkup: response.Markup);
            }
            catch (ArgumentException e)
            {
                OnError?.Invoke(e);
                await Bot.SendTextMessageAsync(message.Chat.Id, e.Message);
            }
            catch (Exception e)
            {
                OnError?.Invoke(e);
            }
        }
        public IRequest AnalyzeIncomingMessage(Message message)
        {
            if (message.Type != MessageType.TextMessage)
                throw new ArgumentException($"{message.Type} is not supported yet =[");
            var sender = message.Chat;
            var author = new Person(sender.Id, sender.FirstName, sender.LastName, sender.Username);
            return new BaseRequest(author, message.Text);
        }
    }
}

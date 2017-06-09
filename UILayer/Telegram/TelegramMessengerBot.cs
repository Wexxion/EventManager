using System;
using AppLayer;
using RepoLayer.MessengerInterfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace UILayer.Telegram
{
    public class TelegramMessengerBot : IMessengerBot
    {
        private TelegramBotClient Bot { get;}
        private SessionHandler Handler { get; }
        private Reminder Reminder { get; }
        public event Action<IRequest> OnRequest;
        public event Action<Exception> OnError;
        public TelegramMessengerBot(TelegramBotClient bot, SessionHandler handler, Reminder reminder)
        {
            Reminder = reminder;
            Bot = bot;
            Handler = handler;
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
                var request = TelegramResponseHandler.AnalyzeIncomingMessage(message);
                OnRequest?.Invoke(request);
                var response = TelegramResponseHandler.ResponseAnalyzer(Handler.ProcessMessage(request));
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
    }
}

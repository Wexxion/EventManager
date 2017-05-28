using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLayer;
using TaskManager.AppLayer;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.UILayer
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
            Reminder.OnRemind += BotOnRemind;
            Bot = bot;
            Handler = handler;
            Bot.OnMessage += BotOnMessageReceived;
        }

        public void Start()
        {
            Bot.StartReceiving();
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
            /* 
               по идее разных видов Response может быть много и каждый из них надо по особенному обрабатывать,
               думаю можно эту логику вынести в какой-нибудь responseHandler             
            */
            var message = messageEventArgs.Message;
            try
            {
                var request = AnalyzeIncomingMessage(message);
                OnRequest?.Invoke(request);
                var response = Handler.ProcessMessage(request);

                //TODO: проверка на null
                if (response is ButtonResponse)
                {
                    var buttonAnswer = (ButtonResponse) response;
                    await Bot.SendTextMessageAsync(message.Chat.Id, buttonAnswer.Text,
                        replyMarkup: GetKeyboard(buttonAnswer.ButtonNames));
                }
                else if (response is TextResponse)
                {
                    var textAnswer = (TextResponse) response;
                    await Bot.SendTextMessageAsync(message.Chat.Id, textAnswer.Text);
                }
                else
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, response.Text);
                }
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
        
        //TODO: это тоже нужно выпилить
        protected ReplyKeyboardMarkup GetKeyboard(IEnumerable<string> commandNames)
        {
            // 4 - это максимальная ширина панели кнопок
            var buttons = commandNames.Select(x => new KeyboardButton(x)).ToArray();
            var buttonRows = new List<KeyboardButton[]>();
            var rowCount = buttons.Length % 4 == 0 ? buttons.Length / 4 : buttons.Length / 4 + 1;
            for (var i = 0; i < rowCount; i++)
                buttonRows.Add(buttons.Skip(4 * i).Take(4).ToArray());
            return new ReplyKeyboardMarkup(buttonRows.ToArray(),true);
        }

        private IRequest AnalyzeIncomingMessage(Message message)
        {
            if (message.Type != MessageType.TextMessage)
                throw new ArgumentException($"{message.Type} is not supported yet =[");
            var sender = message.Chat;
            var author = new Person(sender.Id, sender.FirstName, sender.LastName, sender.Username);
            return new BaseRequest(author, message.Text);
        }
    }
}

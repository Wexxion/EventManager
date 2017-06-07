using System;
using System.Collections.Generic;
using System.Linq;
using AppLayer;
using DomainLayer;
using RepoLayer.MessengerInterfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace UILayer.Telegram
{
    class TelegramResponseHandler
    {
        public static BotData ResponseAnalyzer(IResponse response)
        {
            if (response is ButtonResponse)
            {
                var buttonAnswer = (ButtonResponse)response;
                return new BotData(buttonAnswer.Text, GetKeyboard(buttonAnswer.ButtonNames));
            }
            if (response is TextResponse)
            {
                var textAnswer = (TextResponse)response;
                return new BotData(textAnswer.Text);
            }
            return new BotData(response.Text);
        }
        public static ReplyKeyboardMarkup GetKeyboard(IEnumerable<string> commandNames)
        {
            var buttonsCount = 4;
            var buttons = commandNames.Select(x => new KeyboardButton(x)).ToArray();
            var buttonRows = new List<KeyboardButton[]>();
            var rowCount = buttons.Length % buttonsCount == 0 ?
                buttons.Length / buttonsCount : buttons.Length / buttonsCount + 1;
            for (var i = 0; i < rowCount; i++)
                buttonRows.Add(buttons.Skip(buttonsCount * i).Take(buttonsCount).ToArray());
            return new ReplyKeyboardMarkup(buttonRows.ToArray(), true);
        }

        public static IRequest AnalyzeIncomingMessage(Message message)
        {
            if (message.Type != MessageType.TextMessage)
                throw new ArgumentException($"{message.Type} is not supported yet =[");
            var sender = message.Chat;
            var author = new Person(sender.Id, sender.FirstName, sender.LastName, sender.Username);
            return new BaseRequest(author, message.Text);
        }
    }
}

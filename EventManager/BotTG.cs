using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.AppLayer;
using TaskManager.RepoLayer.Messages;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TaskManager
{
    class BotTG
    {
        private TelegramBotClient bot;
        private IMessageHandler handler;

        public BotTG(TelegramBotClient bot, IMessageHandler handler)
        {
            this.bot = bot;
            this.handler = handler;
        }

        public void Start()
        {
            bot.OnMessage += BotOnMessageReceived;
        }

        private  async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            try
            {
                var answer = handler.ProcessMessage(message).Text;
                await bot.SendTextMessageAsync(message.Chat.Id, answer);
            }
            catch (ArgumentException e)
            {
                await bot.SendTextMessageAsync(message.Chat.Id, e.Message);
            }

        }

    }
}

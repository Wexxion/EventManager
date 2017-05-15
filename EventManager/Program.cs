using TaskManager.AppLayer;
using TaskManager.UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            var telegramBot = new TelegramMessengerBot("token", new MessageHandler());
            telegramBot.Start();
        }
    }
}
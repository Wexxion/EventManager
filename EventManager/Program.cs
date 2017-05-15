using TaskManager.UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            var telegramBot = new TelegramMessengerBot();
            telegramBot.Initialize("token");
        }
    }
}
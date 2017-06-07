using System;
using DomainLayer;
using Ninject;
using UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            var config = new Configuration
            {
                DbName = "storage.db",
                Token = "Token",
                PathToPluginsFolder = "Plugins",
                RemindTimeOut = 1000 * 10
            };
            NinjectConfig.Configure(config);
            var bot = NinjectConfig.GetKernel().Get<IMessengerBot>();
            bot.OnRequest += request =>
            {
                var sender = (Person)request.Author;
                Console.WriteLine($"from: {sender.FirstName} {sender.LastName} {sender.TelegramId} text: {request.Command}");
            };
            bot.Start();
            Console.ReadLine();
            bot.Stop();
        }
    }

    
}
using System;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using AppLayer;
using DomainLayer;
using Newtonsoft.Json;
using Ninject;
using RepoLayer;
using UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {

            NinjectConfig.Configure(new Configuration(
                token: "Token", 
                dbName: "Storage.db", 
                pathToPlugins: "Plugins",
                remindTimeOut: 10 * 100));

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
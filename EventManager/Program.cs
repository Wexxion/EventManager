using System;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using DomainLayer;
using Newtonsoft.Json;
using Ninject;
using UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            NinjectConfig.Configure(Configuration.Load("Configuration.xml"));
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
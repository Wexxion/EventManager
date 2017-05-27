using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Ninject;
using RepoLayer;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer;
using TaskManager.UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            var config = new Configuration
            {
                DbName = "storage.db",
                Token = "token"
            };
            StorageFactory.dbName = config.DbName;
            NinjectConfig.Configure(config.Token);
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
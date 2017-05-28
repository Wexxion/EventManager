using System.Security.Cryptography.X509Certificates;
using AppLayer;
using LiteDB;
using Ninject;
using RepoLayer;
using RepoLayer.Session;
using TaskManager.App_Start;
using TaskManager.DomainLayer;
using TaskManager.UILayer;
using Telegram.Bot;

namespace TaskManager
{
    public static class NinjectConfig
    {
        private static IKernel kernel = new StandardKernel();
        public static void Configure(Configuration config)
        {
            kernel.Bind<LiteDatabase>().ToConstructor(x => new LiteDatabase(config.DbName,null));
            kernel.Bind<IRepository<VEvent>>().To<NoSqlDb<VEvent>>();
            kernel.Bind<IRepository<Person>>().To<NoSqlDb<Person>>();
            foreach (var command in new CommandLoader(config.PathToPluginsFile,
                kernel.Get<IRepository<VEvent>>(),
                kernel.Get<IRepository<Person>>()).GetCommands())
                kernel.Bind<BaseBotSession>().To(command.GetType());
            kernel.Bind<Reminder>().ToConstructor(x => new Reminder(config.RemindTimeOut, kernel.Get<IRepository<VEvent>>()));
            kernel.Bind<TelegramBotClient>().ToConstructor(x => new TelegramBotClient(config.Token));
            kernel.Bind<IMessengerBot>().To<TelegramMessengerBot>();
        }
        public static IKernel GetKernel()
        {
            return kernel;
        }
    }
}
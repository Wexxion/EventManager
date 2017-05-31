using System.Security.Cryptography.X509Certificates;
using AppLayer;
using DomainLayer;
using LiteDB;
using Ninject;
using RepoLayer;
using RepoLayer.Session;
using TaskManager.App_Start;
using Telegram.Bot;
using UILayer;

namespace TaskManager
{
    public static class NinjectConfig
    {
        private static readonly IKernel Kernel = new StandardKernel();
        public static void Configure(Configuration config)
        {
            Kernel.Bind<LiteDatabase>().ToConstructor(x => new LiteDatabase(config.DbName,null));
            Kernel.Bind<IRepository<VEvent>>().To<NoSqlDb<VEvent>>();
            Kernel.Bind<IRepository<Person>>().To<NoSqlDb<Person>>();
            foreach (var command in new CommandLoader(config.PathToPluginsFolder,
                Kernel.Get<IRepository<VEvent>>(),
                Kernel.Get<IRepository<Person>>()).GetCommands())
                Kernel.Bind<BaseBotSession>().To(command.GetType());
            Kernel.Bind<Reminder>().ToConstructor(x => new Reminder(config.RemindTimeOut, Kernel.Get<IRepository<VEvent>>()));
            Kernel.Bind<TelegramBotClient>().ToConstructor(x => new TelegramBotClient(config.Token));
            Kernel.Bind<IMessengerBot>().To<TelegramMessengerBot>();
        }
        public static IKernel GetKernel()
        {
            return Kernel;
        }
    }
}
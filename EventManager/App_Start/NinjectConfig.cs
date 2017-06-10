using AppLayer;
using DomainLayer;
using LiteDB;
using Ninject;
using RepoLayer;
using RepoLayer.Session;
using Telegram.Bot;
using UILayer;
using UILayer.Telegram;

namespace TaskManager
{
    public static class NinjectConfig
    {
        private static readonly IKernel Kernel = new StandardKernel();
        public static void Configure(Configuration config)
        {
            Kernel.Bind<ApiToken>().ToConstant(config.Token);
            Kernel.Bind<DbInfo>().ToConstant(config.Db);
            Kernel.Bind<ReminderTimeOut>().ToConstant(config.RemindTimeOut);
            Kernel.Bind<PluginsPath>().ToConstant(config.PathToPluginsFolder);
            Kernel.Bind<IRepository<VEvent>>().To<NoSqlDb<VEvent>>();
            Kernel.Bind<IRepository<Person>>().To<NoSqlDb<Person>>();
            Kernel.Bind<ICommandLoader>().To<CommandLoader>();
            Kernel.Bind<IMessengerBot>().To<TelegramMessengerBot>();
        }
        public static IKernel GetKernel()
        {
            return Kernel;
        }
    }
}
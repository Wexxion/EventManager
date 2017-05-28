using AppLayer;
using Ninject;
using RepoLayer.Session;
using TaskManager.App_Start;
using TaskManager.UILayer;
using Telegram.Bot;

namespace TaskManager
{
    public static class NinjectConfig
    {
        private static IKernel kernel = new StandardKernel();
        public static void Configure(Configuration config)
        {
            foreach (var command in new CommandLoader("Plugins").GetCommands())
                kernel.Bind<BaseBotSession>().To(command.GetType());
            kernel.Bind<Reminder>().ToConstructor(x => new Reminder(config.RemindTimeOut));
            kernel.Bind<TelegramBotClient>().ToConstructor(x => new TelegramBotClient(config.Token));
            kernel.Bind<IMessengerBot>().To<TelegramMessengerBot>();
        }
        public static IKernel GetKernel()
        {
            return kernel;
        }
    }
}
using System.Collections.Generic;
using Ninject;
using TaskManager.AppLayer;
using TaskManager.AppLayer.Commands;
using TaskManager.RepoLayer.Command;
using Telegram.Bot;

namespace TaskManager
{
    public static class NinjectConfig
    {
        private static IKernel kernel = new StandardKernel();
        public static void Configure(string token)
        {
            //тут добавлять команды
            kernel.Bind<BaseCommand>().To<EventCommand>();
            //------------------------------
            kernel.Bind<TelegramBotClient>()
                .ToConstructor(x => new TelegramBotClient(token));
        }
        public static IKernel GetKernel()
        {
            return kernel;
        }
    }
}
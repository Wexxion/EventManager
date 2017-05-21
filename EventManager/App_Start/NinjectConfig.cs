using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;
using TaskManager.App_Start;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer;
using TaskManager.RepoLayer.Command;
using TaskManager.UILayer;
using Telegram.Bot;

namespace TaskManager
{
    public static class NinjectConfig
    {
        private static IKernel kernel = new StandardKernel();
        public static void Configure(string token)
        {
            foreach (var command in new CommandLoader("Plugins").GetCommands())
                kernel.Bind<BaseCommand>().To(command.GetType());

            kernel.Bind<TelegramBotClient>().ToConstructor(x => new TelegramBotClient(token));

            kernel.Bind<IMessengerBot>().To<TelegramMessengerBot>();
        }
        public static IKernel GetKernel()
        {
            return kernel;
        }
    }
}
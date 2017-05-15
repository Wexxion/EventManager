using System.Collections.Generic;
using System.Linq;
using Ninject;
using TaskManager.AppLayer;
using TaskManager.AppLayer.Commands;
using TaskManager.RepoLayer.Command;
using TaskManager.UILayer;
using Telegram.Bot;

namespace TaskManager
{
    public class NinjectConfig
    {
        private static IKernel kernel = new StandardKernel();
        public static void Configure(string token)
        {
            kernel.Bind<IEnumerable<BaseCommand>>().ToConstant(new List<BaseCommand>
            {
                new EventCommand()
            });

            kernel.Bind<MessageHandler>()
                .ToConstructor(x => new MessageHandler(kernel.Get<IEnumerable<BaseCommand>>()));

            kernel.Bind<TelegramBotClient>()
                .ToConstructor(x => new TelegramBotClient(token));
        }
        public static IKernel GetKernel()
        {
            return kernel;
        }
    }
}
using Ninject;
using TaskManager.AppLayer;
using TaskManager.UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            NinjectConfig.Configure("token");
            NinjectConfig.GetKernel().Get<TelegramMessengerBot>().Start();
        }
    }
}
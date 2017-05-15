using Ninject;
using TaskManager.AppLayer;
using TaskManager.UILayer;

namespace TaskManager
{
    class Program
    {
        public static void Main(string[] args)
        {
            NinjectConfig.Configure("342936471:AAEgFkblOUKv2qJ8o30zG0Dji8trlo2sEKM");
            NinjectConfig.GetKernel().Get<TelegramMessengerBot>().Start();
        }
    }
}
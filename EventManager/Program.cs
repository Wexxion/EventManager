using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
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
            NinjectConfig.GetKernel().Get<IMessengerBot>().Start();

        }
    }
}
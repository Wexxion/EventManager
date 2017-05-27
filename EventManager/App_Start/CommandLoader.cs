using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using RepoLayer.Session;

namespace TaskManager.App_Start
{
    class CommandLoader
    {
        [ImportMany(typeof(BaseBotSession))]
        private IEnumerable<BaseBotSession> Commands { get; set; }

        public CommandLoader(string pathToPlugins)
        {
            var catalog = new AggregateCatalog();
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathToPlugins);
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public IEnumerable<BaseBotSession> GetCommands()
        {
            return Commands;
        }
    }
}

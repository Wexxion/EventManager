using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using TaskManager.RepoLayer.Command;

namespace TaskManager.App_Start
{
    class CommandLoader
    {
        [ImportMany(typeof(BaseCommand))]
        private IEnumerable<BaseCommand> Commands { get; set; }

        public CommandLoader(string pathToPlugins)
        {
            var catalog = new AggregateCatalog();
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathToPlugins);
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public IEnumerable<BaseCommand> GetCommands()
        {
            return Commands;
        }
    }
}

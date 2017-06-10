using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using DomainLayer;
using RepoLayer;
using RepoLayer.Session;

namespace TaskManager
{
    public class CommandLoader
    {
        [ImportMany(typeof(BaseBotSession))]
        private IEnumerable<BaseBotSession> Commands { get; set; }

        public CommandLoader(PluginsPath plugins, IRepository<VEvent> eventStorage, IRepository<Person> personStorage)
        {
            var catalog = new AggregateCatalog();
            var path = Path.Combine(Directory.GetCurrentDirectory(), plugins.Path);
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            var container = new CompositionContainer(catalog);
            container.ComposeExportedValue("PersonStorage", personStorage);
            container.ComposeExportedValue("EventStorage", eventStorage);
            container.ComposeParts(this);
        }

        public IEnumerable<BaseBotSession> GetCommands()
        {
            return Commands;
        }
    }

    public class PluginsPath
    {
        public PluginsPath(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}

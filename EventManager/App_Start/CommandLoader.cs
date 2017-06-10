using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using AppLayer;
using DomainLayer;
using RepoLayer;
using RepoLayer.Session;

namespace TaskManager
{
    public class CommandLoader : ICommandLoader
    {
        [ImportMany(typeof(BaseBotSession))]
        private IEnumerable<BaseBotSession> Commands { get; set; }

        private PluginsPath Plugins;
        private IRepository<VEvent> EventStorage;
        private IRepository<Person> PersonStorage;
        public CommandLoader(PluginsPath plugins, IRepository<VEvent> eventStorage, IRepository<Person> personStorage)
        {
            Plugins = plugins;
            EventStorage = eventStorage;
            PersonStorage = personStorage;
        }

        public IEnumerable<BaseBotSession> GetCommands()
        {
            var catalog = new AggregateCatalog();
            var path = Path.Combine(Directory.GetCurrentDirectory(), Plugins.Path);
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            var container = new CompositionContainer(catalog);
            container.ComposeExportedValue("PersonStorage", PersonStorage);
            container.ComposeExportedValue("EventStorage", EventStorage);
            container.ComposeParts(this);
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

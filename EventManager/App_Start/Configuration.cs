using System.IO;
using System.Xml.Serialization;
using AppLayer;
using RepoLayer;
using UILayer;

namespace TaskManager
{
    public class Configuration
    {
        public Configuration(string token, string dbName, string pathToPlugins, int remindTimeOut)
        {
            Token = new ApiToken(token);
            Db = new DbInfo(dbName);
            PathToPluginsFolder = new PluginsPath(pathToPlugins);
            RemindTimeOut = new ReminderTimeOut(remindTimeOut);
        }

        public ApiToken Token { get; set; }
        public DbInfo Db { get; set; }
        public ReminderTimeOut RemindTimeOut { get; set; }
        public PluginsPath PathToPluginsFolder { get; set; }
        public static Configuration Load(string confPath)
        {
            var ser = new XmlSerializer(typeof(Configuration));
            using (var reader = new StreamReader(confPath))
            {
                return (Configuration)ser.Deserialize(reader);
            }
        }
    }
}
using System.IO;
using System.Xml.Serialization;

namespace TaskManager
{
    public class Configuration
    {
        public string Token { get; set; }
        public string DbName { get; set; }

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
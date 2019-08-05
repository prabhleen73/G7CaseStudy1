using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    public class Configuration
    {
        private readonly string ConfigurationPath;

        private Dictionary<string, XElement> configurations = new Dictionary<string, XElement>();
        public IReadOnlyDictionary<string, XElement> Configurations => configurations;


        public Configuration(string configurationPath)
        {
            ConfigurationPath = configurationPath;

        }

        public void LoadConfiguration()
        {
            XElement root = XElement.Load(ConfigurationPath);
            var configurationRoot = from element in root.DescendantsAndSelf()
                                         where element.Name == "configuration"
                                         select element;
            foreach (var child in configurationRoot.Elements())
            {
                configurations.Add(child.Name.ToString(),child);
            }
        }
    }
}

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
        private readonly string _configurationPath;

        private Dictionary<string, XElement> _configurations = new Dictionary<string, XElement>();
        public IReadOnlyDictionary<string, XElement> Configurations => _configurations;


        public Configuration(string configurationPath)
        {
            _configurationPath = configurationPath;

        }

        public void LoadConfiguration()
        {
            XElement root = XElement.Load(_configurationPath);
            var configurationRoot = from element in root.DescendantsAndSelf()
                                         where element.Name == "configuration"
                                         select element;
            foreach (var child in configurationRoot.Elements())
            {
                _configurations.Add(child.Name.ToString(),child);
            }
            //read file
            //parse xml
            //Xm.FindNode(Configuration)
            //foreach node in lastfindnode
            //dictionary.Add(node.Name,node.Xelement)
            // _tools.Add(ToolName,ToolPath);

        }




        //ReadConfiguration

        //ParseConfigurationAnd

    }

}

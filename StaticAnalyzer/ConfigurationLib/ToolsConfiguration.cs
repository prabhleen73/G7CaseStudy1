using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfigurationLib
{
    public class ToolsConfiguration
    {
        private readonly List<ToolMeta> tools = new List<ToolMeta>();
        private string configurationFilepath;
        public IReadOnlyCollection<ToolMeta> Tools => tools;
        public ToolsConfiguration(string configurationFilepath)
        {
            this.configurationFilepath = configurationFilepath;
        }
        public void LoadConfiguration()
        {
            XElement configurationRoot = XElement.Load(configurationFilepath);
            if (configurationRoot != null)
            {
                var installedPlugins = from element in configurationRoot.DescendantsAndSelf()
                                       where element.Name == "installedPlugins"
                                       select element;
                foreach (var element in installedPlugins.Elements())
                {
                    ToolMeta.WrapperMeta wrapperMeta =
                        new ToolMeta.WrapperMeta(
                            element.Attribute("wrapperClassName").Value, element.Attribute("namespace").Value, element.Attribute("assembly").Value);

                    tools.Add(new ToolMeta
                      (
                        element.Attribute("name").Value,
                        element.Attribute("installationPath").Value,
                        wrapperMeta
                      ));
                }
            }
        }

    }
}

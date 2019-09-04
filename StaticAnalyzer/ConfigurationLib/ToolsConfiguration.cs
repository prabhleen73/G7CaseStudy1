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
        private XElement xElement;

        public IReadOnlyCollection<ToolMeta> Tools => tools;
        public ToolsConfiguration(XElement installedPlugins)
        {
            //GOTo this plugin Node
            //Load Tool meta
            //Foreach Node in In
            foreach (var element in installedPlugins.Elements())
            {
                ToolMeta.WrapperMeta wrapperMeta =
                    new ToolMeta.WrapperMeta(
                        element.Attribute("wrapperClassName").Value,element.Attribute("namespace").Value,element.Attribute("assembly").Value);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLib
{
    public class ToolMeta
    {
        public string Name { get; }
        public string InstallationPath { get; }
        public WrapperMeta Wrapper { get; }

        public ToolMeta(string name, string path, WrapperMeta wrapperDetails)
        {
            Name = name;
            InstallationPath = path;
            Wrapper = wrapperDetails;
        }

        public class WrapperMeta
        {
            public string ClassName { get; }
            public string Namespace { get; }
            public string Assembly { get; set; }

            public WrapperMeta(string className, string nameSpace,string assembly)
            {
                ClassName = className;
                Namespace = nameSpace;
                Assembly = assembly;
            }
        }
    }

}


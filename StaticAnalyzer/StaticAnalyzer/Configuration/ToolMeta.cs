using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzer
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
            public string AssemblyPath { get; }

            public WrapperMeta(string className, string assemblyPath = "")
            {
                ClassName = className;
                AssemblyPath = assemblyPath;
            }
        }
    }

}


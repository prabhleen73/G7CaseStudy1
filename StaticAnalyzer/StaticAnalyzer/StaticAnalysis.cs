using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzer
{
    class StaticAnalysisApplication
    {
        private IEnumerable<IStaticAnalysisTool> _staticAnalysisPlugins;
        public StaticAnalysisApplication(IEnumerable<ToolMeta> staticAnalysisPluginMeta)
        {
            _staticAnalysisPlugins = IntializeToolsList(staticAnalysisPluginMeta);
        }

        private IEnumerable<IStaticAnalysisTool> IntializeToolsList(IEnumerable<ToolMeta> configuredTools)
        {
            List<IStaticAnalysisTool> availableTools = new List<IStaticAnalysisTool>();

            foreach (var toolMeta in configuredTools)
            {
                var type = Type.GetType(/*"StaticAnalyzer."*/GetType().Namespace+"."+toolMeta.Wrapper.ClassName);
                if (!(type == null))
                {
                    object tool;

                    var constructor = type.GetConstructor(new Type[] { typeof(string) });
                    if (constructor != null)
                    {
                        tool = constructor.Invoke(new string[] { toolMeta.InstallationPath });
                    }
                    else
                    {
                        var defaultConstructor = type.GetConstructors(BindingFlags.Public).First();
                        tool = defaultConstructor.Invoke(null);
                    }

                    availableTools.Add((IStaticAnalysisTool)tool);
                }
                else
                {
                    Console.WriteLine($"Tool {toolMeta.Name} not found at {toolMeta.Wrapper}");
                }
            }

            return availableTools;

        }

        public int Run(string inputPath)
        {
            int exitCode = 0;
            try
            {
                foreach (var tool in _staticAnalysisPlugins)
                {
                    tool.prepareInput(inputPath);
                    tool.processOutput();
                    DisplayResult();
                }
            }
            catch
            {
                exitCode = -2;
            }
            return exitCode;
        }

        private void DisplayResult()
        {

        }
    }
}

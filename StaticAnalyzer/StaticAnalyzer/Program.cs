using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    public class Program
    {
        static int Main(string[] args)
        {
            string configurationFilePath;
            string inputPath = "";
            if (args.Length != 2)
            {
                Console.WriteLine("Input Path not provided");
                return -1;
            }
            configurationFilePath = args[0];
            inputPath = args[1];
            if (Helper.CheckDirectoryExists(inputPath))
            {
                if (Helper.CheckFileExists(configurationFilePath))
                {
                    Console.WriteLine("Welcome to Static Analysis Tool");
                    var configuration = LoadConfiguration(configurationFilePath);
                    var toolsConfiguration = new ToolsConfiguration(configuration["installedPlugins"]);

                    StaticAnalysisApplication staticAnalysis = new StaticAnalysisApplication(toolsConfiguration.Tools);

                    int errorcode = staticAnalysis.Run(inputPath);
                    if (errorcode == -2)
                    {
                        return -2;
                    }
                }
            }
            return 0;
        }

        private static IReadOnlyDictionary<string, XElement> LoadConfiguration(string ConfigurationFilePath)
        {
            Configuration config = new Configuration(ConfigurationFilePath);
            config.LoadConfiguration();
            return config.Configurations;
        }
    }
}

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
        static string ConfigurationFilePath = "C:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\StaticAnalyzer\\Configuration.xml";

        static public bool CheckFile(string Filename)
        {
            bool success = false;
            if (File.Exists(Filename))
            {
                success = true;
            }
            return success;
        }
        static public bool CheckDirectory(string Filename)
        {
            bool success = false;
            if (Directory.Exists(Filename))
            {
                success = true;
            }
            return success;
        }
        static int Main(string[] args)
        {
            string inputPath = "";
            if (args.Length != 1)
            {
                Console.WriteLine("Input Path not provided");
                return -1;
            }
            
            inputPath = args[0];
            if (CheckDirectory(inputPath))
            {
                if (CheckFile(ConfigurationFilePath))
                {
                    Console.WriteLine("Welcome to Static Analysis Tool");
                    var configuration = LoadConfiguration(ConfigurationFilePath);
                    var toolsConfiguration = new ToolsConfiguration(configuration["installedPlugins"]);

                    StaticAnalysisApplication staticAnalysis = new StaticAnalysisApplication(toolsConfiguration.Tools);

                    int errorcode = staticAnalysis.Run(inputPath);
                    if (errorcode == -2)
                    {
                        return -1;
                    }
                    return 0;
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

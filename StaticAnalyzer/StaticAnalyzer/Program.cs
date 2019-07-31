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
    class Program
    {
        static string ConfigurationFilePath = "C:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\StaticAnalyzer\\Configuration.xml";
        static int Main(string[] args)
        {
            string inputPath = "";
            if (args.Length != 1)
            {
                Console.WriteLine("Input Path not provided");
                return -1;
            }
            inputPath = args[0];

            Console.WriteLine("Welcome to Static Analysis Tool");
            var configuration = LoadConfiguration(ConfigurationFilePath);
            var toolsConfiguration = new ToolsConfiguration(configuration["installedPlugins"]);
            StaticAnalysisApplication staticAnalysis = new StaticAnalysisApplication(toolsConfiguration.Tools);
            staticAnalysis.Run(inputPath);         
            return 0;
        }


        //COnfirguration

        private static IReadOnlyDictionary<string, XElement> LoadConfiguration(string ConfigurationFilePath)
        {
            Configuration config = new Configuration(ConfigurationFilePath);
            config.LoadConfiguration();
            return config.Configurations;
        }

        public static void PerformMetrics()
        {
            // bool toolInputProcessing = true;






            //string exeFileAndLocation = @"C:\Users\320050767\Downloads\NDepend_2019.2.6.9270\NDepend.Console.exe";
            //string arguments = @"C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalyzer.ndproj  /LogTrendMetrics /OutDir C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalysisReports";
            //ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);

            //ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(xmllocation);
            //ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(xmllocation);

            //FxCop fxcop = new FxCop();
            //toolInputProcessing = fxcop.prepareInput(exePath);

            //exeFileAndLocation = @"C:\Program Files (x86)\Microsoft Fxcop 10.0\FxCopCmd.exe";
            //arguments = @"/p:C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\HelloWorld.FxCop /out:C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\StaticAnalysisReports\FxCopResults.xml";
            //ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);

            //csharpmetric.processOutput();

        }

    }
}

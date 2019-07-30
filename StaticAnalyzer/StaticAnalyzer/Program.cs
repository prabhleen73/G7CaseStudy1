using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool toolInputProcessing;

            string[] filename = { "C:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\HelloWorld\\HelloWorld\\Program.cs"};
            string[] solutionPath = { "C:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\HelloWorld\\HelloWorld.sln" };
            string[] exePath = { "C:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\HelloWorld\\HelloWorld\\bin\\Debug\\HelloWorld.exe" };

            CSharpMetrics csharpmetric = new CSharpMetrics();
            toolInputProcessing = true/*csharpmetric.prepareInput(filename)*/;
            if (toolInputProcessing)
            {
                string executableDirectory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                //exeFileAndLocation = @"C:\Program Files (x86)\SemanticDesigns\DMS\Executables\DMSSoftwareMetrics.cmd";
                string arguments = "CSharp~v6" + executableDirectory + "..\\..\\..\\CSharpMetricInput";
                ExecuteStaticAnalysisTool(csharpmetric.exePath, arguments);
            }

            FxCop fxcop = new FxCop();
            toolInputProcessing = fxcop.prepareInput(exePath);
            
            
            Ndepend ndependout=new Ndepend();
            string outpath=ndependout.prepareInput(filename);
            ndependout.processOutput();
            //csharpmetric.processOutput();

            //exeFileAndLocation = @"C:\Users\320050767\Downloads\NDepend_2019.2.6.9270\NDepend.Console.exe";
            //arguments = @"C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalyzer.ndproj  /LogTrendMetrics /OutDir C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalysisReports";
            //ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);

            ////ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(xmllocation);

            

            //xmllocation = "";
            ////ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(xmllocation);

            //exeFileAndLocation = @"C:\Program Files (x86)\Microsoft Fxcop 10.0\FxCopCmd.exe";
            //arguments = @"/p:C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\HelloWorld.FxCop /out:C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\StaticAnalysisReports\FxCopResults.xml";
            //ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);

        }

        public static void ExecuteStaticAnalysisTool(string exeFileAndLocation, string arguments)
        {
            using (Process ExeToExecute = Process.Start(exeFileAndLocation, arguments))
            {
                ExeToExecute.WaitForExit();
            }
        }
    }
}

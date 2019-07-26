using System.Diagnostics;

namespace StaticAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string exeFileAndLocation;
            string arguments;

            exeFileAndLocation = @"C:\Users\320050767\Downloads\NDepend_2019.2.6.9270\NDepend.Console.exe";
            arguments = @"C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalyzer.ndproj  /LogTrendMetrics /OutDir C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalysisReports";

            ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);

            //exeFileAndLocation = @"C:\Program Files (x86)\SemanticDesigns\DMS\Executables\DMSSoftwareMetrics.cmd";
            //arguments = "CSharp~v6";

            //ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);
            string xmllocation=" ";
            ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(xmllocation);


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

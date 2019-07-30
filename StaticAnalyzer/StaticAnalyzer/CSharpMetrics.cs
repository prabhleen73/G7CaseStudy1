using System;
using System.IO;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    class CSharpMetrics : StaticAnalysisTool
    {
        public string inputProjFile = @"..\..\..\CSharpMetricInput";
        public string exePath = @"C:\Program Files(x86)\SemanticDesigns\DMS\Executables\DMSSoftwareMetrics.cmd";

        public bool prepareInput(string[] filename)
        {
            bool success = true;
            if (!File.Exists(inputProjFile))
            {
                success = false;
                return success;
            }
            string requiredString = "CSharp~v6 Metrics 1.0\n<C:\\Users\\320050767\\documents\\visual - studio - 2015\\Projects\\StaticAnalyzer\\StaticAnalyzer\nC:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\StaticAnalyzer\\StaticAnalysisReports\\CSharpMetricReport";
            try
            {
                StreamWriter streamwriter = new StreamWriter(inputProjFile);
                streamwriter.WriteLine(requiredString);
                foreach (var str in filename)
                {
                    streamwriter.WriteLine(str);
                }
                streamwriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }
        public void processOutput()
        {
            XElement root = XElement.Load("..\\..\\..\\StaticAnalysisReports\\CSharpMetricReport.xml");
            //Processing of XML to get required data
        }

    }
}

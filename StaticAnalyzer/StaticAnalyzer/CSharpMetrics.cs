using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace StaticAnalyzer
{
    class CSharpMetrics : StaticAnalysisTool
    {
        public string inputProjFile = @"..\..\..\CSharpMetricInput";
        public string exePath = @"C:\Program Files(x86)\SemanticDesigns\DMS\Executables\DMSSoftwareMetrics.cmd";
        Dictionary<string, Dictionary<string, string>> MetricMap = new Dictionary<string, Dictionary<string, string>>();

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
            var fileElement = from elem in root.DescendantsAndSelf()
                              where elem.Name == "FileMetrics"
                              select elem;
            foreach (var elem in fileElement)
            {
                XElement filenameElement = elem.Descendants("FileName").FirstOrDefault();
                if (filenameElement != null) {
                    Dictionary<string, string> MetricOfEachFile = new Dictionary<string, string>();
                    foreach (var child in elem.Descendants("FileSummary").Elements())
                    {
                        string key = child.Name.ToString();
                        string value = child.Value;
                        MetricOfEachFile.Add(key, value);
                    }
                    string filename = filenameElement.Value.ToString();
                    MetricMap.Add(filename, MetricOfEachFile);
                }
            }
        }
        public void displayOutput()
        {
            foreach (var key in MetricMap.Keys)
            {
                Console.WriteLine(key);
                foreach(KeyValuePair<string,string> MetricMapValue in MetricMap[key])
                {
                    Console.WriteLine(MetricMapValue.Key + "\t" + MetricMapValue.Value);
                }

            }
        }

    }
}

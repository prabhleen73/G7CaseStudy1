using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace StaticAnalyzer
{
    class CSharpMetrics : IStaticAnalysisTool
    {
        public string inputProjFile = @"..\..\..\CSharpMetricInput";//private
        //public string exePath = @"C:\Program Files (x86)\SemanticDesigns\DMS\Executables\DMSSoftwareMetrics.cmd";
        Dictionary<string, Dictionary<string, string>> MetricMap = new Dictionary<string, Dictionary<string, string>>();
        private readonly string _installationPath;

        public CSharpMetrics(string installationPath)
        {
            _installationPath = installationPath;
        }
        public bool prepareInput( string inputDirectory)
        {
            bool success = true;
            if (!File.Exists(inputProjFile))
            {
                success = false;
                return success;
            }
            string requiredString = "CSharp~v6 Metrics 1.0\n<C:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\StaticAnalyzer\nC:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\StaticAnalyzer\\StaticAnalysisReports\\CSharpMetricReport";
            try
            {
                using (StreamWriter streamwriter = new StreamWriter(inputProjFile))
                {
                    streamwriter.WriteLine(requiredString);
                    //foreach (var str in filename)
                    //{
                    //    streamwriter.WriteLine(str);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;
        }

        public void processOutput()
        {
            string argument = @"CSharp~v6 C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\CSharpMetricInput";
            ExecuteStaticAnalysisTool(_installationPath, argument);


            XElement root = XElement.Load(@"C:\Users\320050767\OneDrive - Philips\Desktop\configuration.xml");
            //Processing of XML to get required data
            var fileElement = from elem in root.DescendantsAndSelf()
                              where elem.Name == "FileMetrics"
                              select elem;
            foreach (var elem in fileElement)
            {
                XElement filenameElement = elem.Descendants("FileName").FirstOrDefault();
                if (filenameElement != null)
                {
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
                foreach (KeyValuePair<string, string> MetricMapValue in MetricMap[key])
                {
                    Console.WriteLine(MetricMapValue.Key + "\t" + MetricMapValue.Value);
                }

            }
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

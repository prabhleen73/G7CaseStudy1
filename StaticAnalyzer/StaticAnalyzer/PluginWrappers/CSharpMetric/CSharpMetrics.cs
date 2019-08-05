using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace StaticAnalyzer
{
    public class CSharpMetrics : IStaticAnalysisTool
    {
        private string InputDirectory;
        private string Argument;
        private string OutputFileDirectory;
        private string InputProjFile;
        private string CurrentDirectory;
        Dictionary<string, Dictionary<string, string>> MetricMap = new Dictionary<string, Dictionary<string, string>>();
        private readonly string InstallationPath;

        public CSharpMetrics(string installationPath)
        {
            InstallationPath = installationPath;
        }
        public bool PrepareInput( string inputDirectory)
        {
            bool success = true;
            InputDirectory = inputDirectory;
            CurrentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            OutputFileDirectory = CurrentDirectory + "\\StaticAnalysisReports";
            InputProjFile = CurrentDirectory + "\\CSharpMetricInput";
            if (!File.Exists(InputProjFile))
            {
                success = false;
                return success;
            }
            List<string> filenames = GetFiles(InputDirectory);
            string requiredString = "CSharp~v6 Metrics 1.0\n" +"<" + CurrentDirectory + "\n"+ OutputFileDirectory + "\\CSharpMetricReport";
            try
            {
                using (StreamWriter streamwriter = new StreamWriter(InputProjFile))
                {
                    streamwriter.WriteLine(requiredString);
                    foreach (var str in filenames)
                    {
                        streamwriter.WriteLine(str);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;
        }
        static public bool CheckFile(string Filename)
        {
            bool success = false;
            if (File.Exists(Filename))
            {
                success = true;
            }
            return success;
        }
        public void ProcessOutput()
        {
            Argument = PrepareArgument(InputDirectory, InputProjFile);
            ExecuteStaticAnalysisTool(InstallationPath, Argument);

            if (CheckFile(OutputFileDirectory + "\\CSharpMetricReport.xml"))
            {
                XElement root = XElement.Load(OutputFileDirectory + "\\CSharpMetricReport.xml");
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
                            MetricOfEachFile[key]= value;
                        }
                        string filename = filenameElement.Value.ToString();
                        MetricMap.Add(filename, MetricOfEachFile);

                    }
                }
                displayOutput();
            }
        }

        private void displayOutput()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*******************CSharpMetrics Result********************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine();
            foreach (var key in MetricMap.Keys)
            {
                Console.WriteLine("***********************************************************");
                Console.WriteLine(key);
                foreach (KeyValuePair<string, string> MetricMapValue in MetricMap[key])
                {
                    Console.WriteLine(MetricMapValue.Key + "\t" + MetricMapValue.Value);
                }

            }
        }


        private static void ExecuteStaticAnalysisTool(string exeFileAndLocation, string arguments)
        {
            using (Process ExeToExecute = Process.Start(exeFileAndLocation, arguments))
            {
                ExeToExecute.WaitForExit();
            }
        }

        private static List<string> GetFiles(string _inputDirectory)
        {
            List<string> filenames = new List<string>();
            filenames = Directory.GetFiles(_inputDirectory,"*.cs",SearchOption.AllDirectories).ToList();
            return filenames; 
        }
        private static string PrepareArgument(string InputDirectory,string InputProjFile)
        {
            string argument = "CSharp~v6 " + InputProjFile;
            return argument;
        }
    }
}

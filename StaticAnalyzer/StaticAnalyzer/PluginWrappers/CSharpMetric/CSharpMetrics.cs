﻿using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace StaticAnalyzer
{
    public class CSharpMetrics : IStaticAnalysisTool
    {
        private string _inputDirectory;
        private string _argument;
        private string _outputFileDirectory;
        private string _inputProjFile;
        private string _currentDirectory;
        Dictionary<string, Dictionary<string, string>> MetricMap = new Dictionary<string, Dictionary<string, string>>();
        private readonly string _installationPath;

        public CSharpMetrics(string installationPath)
        {
            _installationPath = installationPath;
        }
        public bool PrepareInput( string inputDirectory)
        {
            bool success = true;
            _inputDirectory = inputDirectory;
            _currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            _outputFileDirectory = _currentDirectory + "\\StaticAnalysisReports";
            _inputProjFile = _currentDirectory + "\\CSharpMetricInput";
            if (!File.Exists(_inputProjFile))
            {
                success = false;
                return success;
            }
            List<string> filenames = GetFiles(_inputDirectory);
            string requiredString = "CSharp~v6 Metrics 1.0\n" +"<" + _currentDirectory + "\n"+ _outputFileDirectory + "\\CSharpMetricReport";
            try
            {
                using (StreamWriter streamwriter = new StreamWriter(_inputProjFile))
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
            _argument = PrepareArgument(_inputDirectory, _inputProjFile);
            ExecuteStaticAnalysisTool(_installationPath, _argument);

            if (CheckFile(_outputFileDirectory + "\\CSharpMetricReport.xml"))
            {
                XElement root = XElement.Load(_outputFileDirectory + "\\CSharpMetricReport.xml");
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

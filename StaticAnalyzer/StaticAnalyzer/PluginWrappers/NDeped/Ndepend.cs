using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    public class Ndepend : IStaticAnalysisTool
    {
        private string _installationPath;
        private string _inputDirectory;
        private string _argument;
        private string _outputDirectory;
        private string _inputProjFile;
        private string _currentDirectory;
        public Ndepend(string InstallationPath)
        {
            _installationPath = InstallationPath;
        }
        public bool PrepareInput(string inputDirectory)
        {
            bool success = true;
            _currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            _inputDirectory = inputDirectory;
            _outputDirectory = _currentDirectory + "\\StaticAnalysisReports";
            _inputProjFile = _currentDirectory + "\\NDependInput.ndproj";
           


            XElement root = XElement.Load(_inputProjFile);
            XElement requiredVal = (from elem in root.DescendantsAndSelf()
                                    where elem.Name == "IDEFile"
                                    select elem).FirstOrDefault();
            if (requiredVal == null)
            {
                success = false;
                return success;
            }
            string slnFilename = GetFiles(_inputDirectory);
            requiredVal.Attribute("FilePath").SetValue(slnFilename);
            root.Save(_inputProjFile);
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
           
            string argument = PrepareArgument(_inputProjFile, _outputDirectory);
            ExecuteStaticAnalysisTool(_installationPath, argument);
            if (CheckFile(_outputDirectory + "\\TrendMetrics\\NDependTrendData2019.xml"))
            {
                string outputFileLocation = _outputDirectory + "\\TrendMetrics\\NDependTrendData2019.xml";
                Console.WriteLine("***********************************************************");
                Console.WriteLine("*******************Ndepend Result********************");
                Console.WriteLine("***********************************************************");
                Console.WriteLine();
                ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(outputFileLocation);
            }
        }
        private static void ExecuteStaticAnalysisTool(string exeFileAndLocation, string arguments)
        {
            using (Process ExeToExecute = Process.Start(exeFileAndLocation, arguments))
            {
                ExeToExecute.WaitForExit();
            }
        }
        private static string GetFiles(string _inputDirectory)
        {
            string filename;
            filename = Directory.GetFiles(_inputDirectory, "*.sln", SearchOption.AllDirectories).First();
            return filename;
        }
        private static string PrepareArgument(string InputProjFile, string OutputDirectory)
        {
            string argument = InputProjFile + " /LogTrendMetrics /OutDir " + OutputDirectory;
            return argument;
        }

    }
}

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
        public Ndepend(string InstallationPath)
        {
            _installationPath = InstallationPath;
        }
        public bool prepareInput(string inputDirectory)
        {
            bool success = true;
            _inputDirectory = inputDirectory;
            _outputDirectory = _inputDirectory + "\\StaticAnalysisReports";
            _inputProjFile = _inputDirectory + "\\NDependInput.ndproj";

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

        public void processOutput()
        {
           
            string argument = PrepareArgument(_inputProjFile, _outputDirectory);
            ExecuteStaticAnalysisTool(_installationPath, argument);

            string outputFileLocation = _outputDirectory + "\\TrendMetrics\\NDependTrendData2019.xml";
            ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(outputFileLocation);
        }
        public static void ExecuteStaticAnalysisTool(string exeFileAndLocation, string arguments)
        {
            using (Process ExeToExecute = Process.Start(exeFileAndLocation, arguments))
            {
                ExeToExecute.WaitForExit();
            }
        }
        public static string GetFiles(string _inputDirectory)
        {
            string filename;
            filename = Directory.GetFiles(_inputDirectory, "*.sln", SearchOption.AllDirectories).First();
            return filename;
        }
        public static string PrepareArgument(string InputProjFile, string OutputDirectory)
        {
            string argument = InputProjFile + " /LogTrendMetrics /OutDir " + OutputDirectory;
            return argument;
        }

    }
}

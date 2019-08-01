using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Collections.Generic;

namespace StaticAnalyzer
{
    public class FxCop : IStaticAnalysisTool
    {
        private string _inputDirectory;
        private string _argument;
        private string _outputFileDirectory;
        private string _inputProjFile;
        private string _currentDirectory;
        private string _installationPath;
        public FxCop(string InstallationPath)
        {
            _installationPath = InstallationPath;
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
        public bool PrepareInput(string inputDirectory)
        {
            bool success = true;
            _inputDirectory = inputDirectory;
            _currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            _outputFileDirectory = _currentDirectory + "\\StaticAnalysisReports";
            _inputProjFile = _currentDirectory + "\\FxCopInput.FxCop";
            if (!CheckFile(_inputProjFile))
            {
                success = false;
                return success;
            }
            XElement root = XElement.Load(_inputProjFile);
            XElement requiredTag = (from elem in root.DescendantsAndSelf()
                                    where elem.Name == "Target"
                                    select elem).FirstOrDefault();
            if (requiredTag != null)
            {
                
            }
            return success;
        }

        public void ProcessOutput()
        {
            string argument = PrepareArgument(_inputProjFile, _outputFileDirectory);
            ExecuteStaticAnalysisTool(_installationPath, argument);
            if (CheckFile(_outputFileDirectory + "\\FxCopResults.xml"))
            {
                Parser obj = new Parser(_outputFileDirectory + "\\FxCopResults.xml");
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
            filename = Directory.GetFiles(_inputDirectory, "*.exe", SearchOption.AllDirectories).First();
            return filename;
        }
        private static string PrepareArgument(string InputFile, string OutputDirectory)
        {
            string argument = "/p:"+InputFile+ "/out:"+ OutputDirectory+"\\FxCopResults.xml";
            return argument;
        }
    }
}

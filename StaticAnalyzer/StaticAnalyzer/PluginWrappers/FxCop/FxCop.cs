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
        private string InputDirectory;
        private string Argument;
        private string OutputFileDirectory;
        private string InputProjFile;
        private string CurrentDirectory;
        private string InstallationPath;
        public FxCop(string InstallationPath)
        {
            this.InstallationPath = InstallationPath;
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
            InputDirectory = inputDirectory;
            CurrentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            OutputFileDirectory = CurrentDirectory + "\\StaticAnalysisReports";
            InputProjFile = CurrentDirectory + "\\FxCopInput.FxCop";
            if (!CheckFile(InputProjFile))
            {
                success = false;
                return success;
            }
            XElement root = XElement.Load(InputProjFile);
            XElement requiredTag = (from elem in root.DescendantsAndSelf()
                                    where elem.Name == "Target"
                                    select elem).FirstOrDefault();
            if (requiredTag != null)
            {
                requiredTag.SetValue(GetFiles(InputDirectory));
            }
            return success;
        }

        public void ProcessOutput()
        {
            string argument = PrepareArgument(InputProjFile, OutputFileDirectory);
            ExecuteStaticAnalysisTool(InstallationPath, argument);
            if (CheckFile(OutputFileDirectory + "\\FxCopResults.xml"))
            {
                Parser obj = new Parser(OutputFileDirectory + "\\FxCopResults.xml");
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
            string argument = "/p:"+InputFile+ " /out:"+ OutputDirectory+"\\FxCopResults.xml";
            return argument;
        }
    }
}

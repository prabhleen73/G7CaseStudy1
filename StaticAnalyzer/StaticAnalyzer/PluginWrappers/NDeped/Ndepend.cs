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
        private string InstallationPath;
        private string InputDirectory;
        private string Argument;
        private string OutputDirectory;
        private string InputProjFile;
        private string CurrentDirectory;
        public Ndepend(string InstallationPath)
        {
            this.InstallationPath = InstallationPath;
        }
        public bool PrepareInput(string inputDirectory)
        {
            bool success = true;
            CurrentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            InputDirectory = inputDirectory;
            OutputDirectory = CurrentDirectory + "\\StaticAnalysisReports";
            InputProjFile = CurrentDirectory + "\\NDependInput.ndproj";
           


            XElement root = XElement.Load(InputProjFile);
            XElement requiredVal = (from elem in root.DescendantsAndSelf()
                                    where elem.Name == "IDEFile"
                                    select elem).FirstOrDefault();
            if (requiredVal == null)
            {
                success = false;
                return success;
            }
            string slnFilename = GetFiles(InputDirectory);
            requiredVal.Attribute("FilePath").SetValue(slnFilename);
            root.Save(InputProjFile);
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
           
            string argument = PrepareArgument(InputProjFile, OutputDirectory);
            ExecuteStaticAnalysisTool(InstallationPath, argument);
            if (CheckFile(OutputDirectory + "\\TrendMetrics\\NDependTrendData2019.xml"))
            {
                string outputFileLocation = OutputDirectory + "\\TrendMetrics\\NDependTrendData2019.xml";
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

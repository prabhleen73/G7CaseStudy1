using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    class FxCop : IStaticAnalysisTool
    {
        private string _installationPath;
        public FxCop(string InstallationPath)
        {
            _installationPath = InstallationPath;
        }
        public bool prepareInput(string inputDirectory)
        {
            bool success = true;
            if (!File.Exists(@"C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\FxCopInput.FxCop"))
            {
                success = false;
                return success;
            }
            XElement root = XElement.Load(@"C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\FxCopInput.FxCop");
            XElement requiredTag = (from elem in root.DescendantsAndSelf()
                                    where elem.Name == "Target"
                                    select elem).FirstOrDefault();
            if (requiredTag != null)
            {
                Console.WriteLine(requiredTag.Attribute("Name").Value);
                //requiredTag.Attribute("Name").SetValue("check");
                root.Save(@"C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\FxCopInput.FxCop");
            }
            return success;
        }

        public void processOutput()
        {

            var exeFileAndLocation = @"C:\Program Files (x86)\Microsoft Fxcop 10.0\FxCopCmd.exe";
            var arguments = @"/p:C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\HelloWorld.FxCop /out:C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\StaticAnalysisReports\FxCopResults.xml";
            ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);

            //Path.GetFullPath(@"..\StaticAnalysisReports");
            string fileName = @"C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\StaticAnalysisReports\FxCopResults.xml";
            Parser obj = new Parser(fileName);
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

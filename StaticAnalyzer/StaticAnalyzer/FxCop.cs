using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StaticAnalyzer
{
    class FxCop : StaticAnalysisTool
    {
        string exeFileLocation;
        string args;
        public void prepareInput(string[] filename)
        {

            exeFileLocation = @"C:\Program Files (x86)\Microsoft Fxcop 10.0\FxCopCmd.exe";
            args = @"/p:C:\Users\320066545\source\repos\G7CaseStudy1\StaticAnalyzer\FxCopInput.FxCop /out:C:\Users\320066545\source\repos\G7CaseStudy1\StaticAnalyzer\StaticAnalysisReports\FxCopInput.xml";
            ExecuteStaticAnalysisTool(exeFileLocation, args);


        }

        public static void ExecuteStaticAnalysisTool(string exeFileAndLocation, string arguments)
        {
            using (Process ExeToExecute = Process.Start(exeFileAndLocation, arguments))
            {
                ExeToExecute.WaitForExit();
            }
        }

        public void processOutput()
        {
            string fileName = @"C:\Users\320066545\source\repos\G7CaseStudy1\StaticAnalyzer\StaticAnalysisReports\FxCopResults.xml";
            Parser obj = new Parser(fileName);
        }
    }
}

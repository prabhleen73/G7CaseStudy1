using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StaticAnalyzer
{
    class CSharpMetrics : StaticAnalysisTool
    {
        string inputProjFile = @"../../../CSharpMetricInput";

        public void prepareInput(string[] filename)
        {
            if (!File.Exists(inputProjFile))
            {
                Console.WriteLine("Exists");
            }
            string requiredString = "CSharp~v6 Metrics 1.0\n<C:\\Users\\320050767\\documents\\visual - studio - 2015\\Projects\\StaticAnalyzer\\StaticAnalyzer\nC:\\Users\\320050767\\Source\\Repos\\G7CaseStudy13\\StaticAnalyzer\\StaticAnalysisReports\\CSharpMetricReport";
            try
            {
                StreamWriter streamwriter = new StreamWriter(inputProjFile);
                streamwriter.WriteLine(requiredString);
                foreach (var str in filename)
                {
                    streamwriter.WriteLine(str);
                }
                streamwriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void processOutput()
        {
            
        }

    }
}

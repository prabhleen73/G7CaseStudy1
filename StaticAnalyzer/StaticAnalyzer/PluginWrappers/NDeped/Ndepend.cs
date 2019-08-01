using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    class Ndepend : IStaticAnalysisTool
    {
        string xmloutpath = " ";//initialize this to xmloutpath
        public bool prepareInput(string inputDirectory)
        {
            int i = 0;
            while (true)//foreach
            {
                XElement root = XElement.Load(@"C:\Users\320069097\source\repos\G7CaseStudy1-prev\StaticAnalysisTool\StaticAnalysisTool.ndproj");
                XElement requiredVal = (from elem in root.DescendantsAndSelf()
                                        where elem.Name == "IDEFile"
                                        select elem).FirstOrDefault();
                if (requiredVal != null)
                {
                    Console.WriteLine(requiredVal.Attribute("FilePath").Value);
                }
                requiredVal.Attribute("FilePath").SetValue(@"C:\Users\320069097\source\repos\G7CaseStudy1\StaticAnalysisTool\" + ".sln");

                root.Save(@"C:\Users\320069097\source\repos\G7CaseStudy1-prev\StaticAnalysisTool\StaticAnalysisTool.ndproj");
                i++;
            }

            return true;
        }

        public void processOutput()
        {
            string exeFileAndLocation = @"C:\Users\320050767\Downloads\NDepend_2019.2.6.9270\NDepend.Console.exe";
            string arguments = @"C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalyzer.ndproj  /LogTrendMetrics /OutDir C:\Users\320050767\documents\visual-studio-2015\Projects\StaticAnalyzer\StaticAnalysisReports";
            ExecuteStaticAnalysisTool(exeFileAndLocation, arguments);

            string xmllocation = "NDependTrendData2019.xml";//initialize this to xmloutpath
            ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(xmllocation);
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

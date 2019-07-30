using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzer
{
    class Ndepend : StaticAnalysisTool
    {
        public bool prepareInput(string[] filename)
        {
            int i = 0;
            while (filename[i])
            {
                XElement root = XElement.Load(@"C:\Users\320069097\source\repos\G7CaseStudy1-prev\StaticAnalysisTool\StaticAnalysisTool.ndproj");
                XElement requiredVal = (from elem in root.DescendantsAndSelf()
                                        where elem.Name == "IDEFile"
                                        select elem).FirstOrDefault();
                if (requiredVal != null)
                {
                    Console.WriteLine(requiredVal.Attribute("FilePath").Value);
                }
                requiredVal.Attribute("FilePath").SetValue(@"C:\Users\320069097\source\repos\G7CaseStudy1\StaticAnalysisTool\" +filename[i]+ ".sln");
                root.Save(@"C:\Users\320069097\source\repos\G7CaseStudy1-prev\StaticAnalysisTool\StaticAnalysisTool.ndproj");
                i++;
            }

            
        }

        public void processOutput()
        {
            ParsingXmlNDepend.ShowingResultsAfterParsingNDependXml(xmllocation);
            throw new NotImplementedException();
        }

        
    }
}

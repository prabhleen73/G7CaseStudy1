using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace StaticAnalyzer
{
    class FxCop : StaticAnalysisTool
    {
        public bool prepareInput(string[] filename)
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
            if(requiredTag != null)
            {
                Console.WriteLine(requiredTag.Attribute("Name").Value);
                //requiredTag.Attribute("Name").SetValue("check");
                root.Save(@"C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\FxCopInput.FxCop");
            }
            return success;
        }

        public void processOutput()
        {
            throw new NotImplementedException();
        }
    }
}

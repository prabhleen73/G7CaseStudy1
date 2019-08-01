using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace StaticAnalyzer
{
    internal class ParsingXmlNDepend
    {
        public static void ShowingResultsAfterParsingNDependXml(string argument)
        {
            //string ques = Console.ReadLine();

            XmlTextReader reader = new XmlTextReader(argument);
            int flag = 0;
            Dictionary<string, string> NDependMetrics = new Dictionary<string, string>();
            List<string> attributeName = new List<string>();
            string[] attributeValue = null;
             while (reader.Read() && flag == 0)
            {
                if(reader.NodeType==XmlNodeType.Element)      
                {
                        if (reader.Name == "Metric")
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name == "Name")
                                {
                                    attributeName.Add(reader.Value);
                                }
                            }
                        }

                        if (reader.Name == "R" && flag == 0)
                        {
                            int i = 0;
                            while (reader.MoveToNextAttribute()) 
                            {
                                if (i == 2 && reader.Name == "V")
                                {
                                   
                                    string ans = reader.Value;
                                  
                                    attributeValue = ans.Split('|');
                                }
                                i++;
                            }
                            flag = 1;
                        }    
                }
            }

            for (int k = 0; k < attributeValue.Length; k++)
            {
                NDependMetrics.Add(attributeName[k], attributeValue[k]);
            }

            foreach (var item in NDependMetrics)
            {
                Console.WriteLine(item);
            }

        }
    }
}

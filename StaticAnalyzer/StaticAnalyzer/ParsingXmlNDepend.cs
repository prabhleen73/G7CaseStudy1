using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace StaticAnalyzer
{
    class ParsingXmlNDepend
    {
        public static void ShowingResultsAfterParsingNDependXml(string argument)
        {
            //string ques = Console.ReadLine();

            XmlTextReader reader = new XmlTextReader(argument);
            int flag = 0;
            while (reader.Read()&&flag==0)
            {
                
                switch (reader.NodeType)
                {
                    

                    case XmlNodeType.Element: // The node is an element.
                        //Console.Write("<" + reader.Name);
                        if (reader.Name == "R"&&flag==0)
                        {
                            int i = 0;
                            while (reader.MoveToNextAttribute()) // Read the attributes.
                            {
                                if (i == 2 && reader.Name == "V")
                                {
                                    //Console.Write("Debt is " + reader.Name + "='" + reader.Value + "");
                                    string ans = reader.Value;
                                    // Console.WriteLine(ans);
                                    int x = 0;

                                    for (int j = 0; j < ans.Length; j++)
                                    {
                                        if (x == 11)
                                        {
                                            Console.Write("Total Rules:= ");
                                            while (x == 11 && ans[j] != '|')
                                            {
                                                Console.Write(ans[j++]);
                                            }
                                            Console.WriteLine("");
                                        }
                                        if (x == 12)
                                        {
                                            Console.Write("Rules Violated:= ");
                                            while (x == 12 && ans[j] != '|')
                                            {
                                                Console.Write(ans[j++]);
                                            }
                                            Console.WriteLine("");
                                        }

                                        if (x == 24)
                                        {
                                            Console.Write("Lines Of code:= ");
                                            while (x == 24 && ans[j] != '|')
                                            {
                                                Console.Write(ans[j++]);
                                            }
                                            Console.WriteLine("");
                                        }


                                        if (ans[j] == '|')
                                            x++;

                                    }

                                }
                                i++;
                            }
                            flag =1;
                        }
                        // Console.WriteLine(">");

                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                                                 // Console.Write("</" + reader.Name);
                                                 //Console.WriteLine(">");
                        break;
                }

            }
        
        }
    }
}

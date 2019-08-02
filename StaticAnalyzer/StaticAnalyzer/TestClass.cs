using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzer
{
    class TestClass
    {
        static void Main(string[] args)
        {
            string DirectoryPath = "";
            string FilePath = "";

            //TestCase1
            if (Program.CheckDirectory(DirectoryPath))
            {
                Console.WriteLine("CheckDirectory : Test Case Passed");
            }
            else
            {
                Console.WriteLine("CheckDirectory : Test Case Failed");
            }
            
            //TestCase2
            if (CSharpMetrics.CheckFile(FilePath))
            {
                Console.WriteLine("CSharpMetrics CheckFile : Test Case Passed");
            }
            else
            {
                Console.WriteLine("CSharpMetrics CheckFile : Test Case Failed");
            }

            //TestCase3
            if (Ndepend.CheckFile(FilePath))
            {
                Console.WriteLine("Ndepend CheckFile : Test Case Passed");
            }
            else
            {
                Console.WriteLine("Ndepend CheckFile : Test Case Failed");
            }

            //TestCase4
            if (FxCop.CheckFile(FilePath))
            {
                Console.WriteLine("FxCop CheckFile : Test Case Passed");
            }
            else
            {
                Console.WriteLine("FxCop CheckFile : Test Case Failed");
            }
        }
    }
}

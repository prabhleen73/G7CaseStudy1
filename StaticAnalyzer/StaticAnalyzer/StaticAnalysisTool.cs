using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzer
{
    interface StaticAnalysisTool
    {
        bool prepareInput(string[] filename);
        void processOutput();

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VulnerableApp
{
    class ProcessExec
    {
        static void Main(string[] args)
        {

            Process.Start("calc.exe");

            Process.Start(args[0]);

        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecurityGuard.Tests
{
    class CommandInjectionRuleTest : FxCopTest
    {
        [TestCase]
        public void testCommandInjection()
        {
            //System.Diagnostics.Debugger.Launch();

            //Vulnerable case
            getVerifier().assertIssueFound("CommandInjectionRule", "ProgramExec.cs",15);

            //False positive verification
            getVerifier().assertIssueCount(1, "CommandInjectionRule", "ProgramExec.cs");
        }
    }
}

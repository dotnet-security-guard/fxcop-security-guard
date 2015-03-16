using NUnit.Framework;
using SecurityGuard.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class ClearTextSocketRuleTest : FxCopTest
    {
        [TestCase]
        public void testClearTextSocket()
        {
            //Vulnerable case
            getVerifier().assertIssueFound("ClearTextSocketRule", "ClearTextSocket.cs", 14);

            getVerifier().assertIssueCount(1, "ClearTextSocketRule", "ClearTextSocket.cs");
        }
    }
}

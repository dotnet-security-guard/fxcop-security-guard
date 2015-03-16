using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class ClearTextHttpRuleTest  : FxCopTest
    {
        [TestCase]
        public void testClearTextHttp()
        {
            //Vulnerable case
            getVerifier().assertIssueFound("ClearTextHttpRule", "ClearTextHttp.cs", 14);
            getVerifier().assertIssueFound("ClearTextHttpRule", "ClearTextHttp.cs", 24);

            getVerifier().assertIssueCount(2, "ClearTextHttpRule", "ClearTextHttp.cs");
        }
    }
}

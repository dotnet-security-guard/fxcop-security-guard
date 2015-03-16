using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class XPathInjectionRuleTest : FxCopTest
    {
        [TestCase]
        public void testXPathInjection()
        {
            //System.Diagnostics.Debugger.Launch();

            //Vulnerable case
            getVerifier().assertIssueFound("XPathInjectionRule", "XPathInjection.cs", 19);
            getVerifier().assertIssueFound("XPathInjectionRule", "XPathInjection.cs", 20);

            //False positive verification
            getVerifier().assertIssueCount(2, "XPathInjectionRule", "XPathInjection.cs");
        }
    }
}

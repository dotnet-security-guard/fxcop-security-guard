using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityGuard.Tests
{
    class WeakRandomGeneratorRuleTest : FxCopTest
    {
        [TestCase]
        public void testWeakRandomGenerator()
        {
            //System.Diagnostics.Debugger.Launch();

            //Vulnerable case
            getVerifier().assertIssueFound("WeakRandomGeneratorRule", "WeakRandom.cs", 13);
            getVerifier().assertIssueFound("WeakRandomGeneratorRule", "WeakRandom.cs", 19);

            //False positive verification
            getVerifier().assertIssueCount(2, "WeakRandomGeneratorRule", "WeakRandom.cs");
        }
    }
}

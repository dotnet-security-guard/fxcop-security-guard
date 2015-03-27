using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class WeakHashingTest  : FxCopTest
    {
        [TestCase]
        public void testWeakHashing()
        {
            //System.Diagnostics.Debugger.Launch();

            //Vulnerable case
            getVerifier().assertIssueFound("WeakHashingRule", "WeakHashing.cs", 16);
            getVerifier().assertIssueFound("WeakHashingRule", "WeakHashing.cs", 32);

            //False positive verification
            getVerifier().assertIssueCount(2, "WeakHashingRule", "WeakHashing.cs");
        }        
    }
}

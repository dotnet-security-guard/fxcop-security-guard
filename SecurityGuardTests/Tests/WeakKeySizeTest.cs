using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class WeakKeySizeTest : FxCopTest
    {

        [TestCase]
        public void testWeakKeySize()
        {
            //System.Diagnostics.Debugger.Launch();

            //Vulnerable case
            getVerifier().assertIssueFound("WeakKeySizeRule", "WeakKeySize.cs", 26);

            //False positive verification
            getVerifier().assertIssueCount(1, "WeakKeySizeRule", "WeakKeySize.cs");
        } 

    }
}

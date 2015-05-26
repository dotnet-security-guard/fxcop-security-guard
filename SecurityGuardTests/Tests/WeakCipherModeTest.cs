using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class WeakCipherModeTest : FxCopTest
    {

        [TestCase]
        public void testWeakCipherMode()
        {
            //System.Diagnostics.Debugger.Launch();

            //Vulnerable case
            getVerifier().assertIssueFound("WeakCipherModeRule", "WeakCipherMode.cs", 17);

            //False positive verification
            getVerifier().assertIssueCount(1, "WeakCipherModeRule", "WeakCipherMode.cs");
        } 

    }
}

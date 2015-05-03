using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class WeakCipherAlgorithmTest : FxCopTest
    {

        [TestCase]
        public void testWeakCipherAlgorithm()
        {
            //System.Diagnostics.Debugger.Launch();

            //Vulnerable case
            getVerifier().assertIssueFound("WeakCipherAlgorithmRule", "WeakCipherAlgorithm.cs", 20);
            getVerifier().assertIssueFound("WeakCipherAlgorithmRule", "WeakCipherAlgorithm.cs", 58);

            //False positive verification
            getVerifier().assertIssueCount(2, "WeakCipherAlgorithmRule", "WeakCipherAlgorithm.cs");
        } 

    }
}

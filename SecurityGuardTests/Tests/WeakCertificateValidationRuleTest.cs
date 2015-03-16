using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Tests
{
    class WeakCertificateValidationRuleTest : FxCopTest
    {

        [TestCase]
        public void testWeakCertificateValidation()
        {
            //Vulnerable case
            getVerifier().assertIssueFound("WeakCertificateValidationRule", "WeakCertificateValidation.cs", 24);
            getVerifier().assertIssueFound("WeakCertificateValidationRule", "WeakCertificateValidation.cs", 40);
            getVerifier().assertIssueFound("WeakCertificateValidationRule", "WeakCertificateValidation.cs", 56);

            getVerifier().assertIssueCount(3, "WeakCertificateValidationRule", "WeakCertificateValidation.cs");
        }
    }
}

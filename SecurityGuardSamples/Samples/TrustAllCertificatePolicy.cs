using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuardSamples
{
    class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuardSamples
{
    class WeakCertificateValidation
    {
        //TODO : http://stackoverflow.com/questions/12506575/how-to-ignore-the-certificate-check-when-ssl

        static void Main(string[] args) {
            new WeakCertificateValidation().DoGetRequest1();
        }


        public void DoGetRequest1()
        {

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string url = "https://cra.cked.in/";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            String responseBody = StreamToString(response.GetResponseStream());
            Console.WriteLine(responseBody);
            Console.Read();
        }

        public void DoGetRequest2()
        {
            //Deprecated method that is equivalent
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

            string url = "https://cra.cked.in/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            String responseBody = StreamToString(response.GetResponseStream());
            Console.WriteLine(responseBody);
            Console.Read();
        }

        public void DoGetRequest3()
        {
            //Deprecated method that is equivalent
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(TrustAllRemoteCertificateValidationCallback.Validate);

            string url = "https://cra.cked.in/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            String responseBody = StreamToString(response.GetResponseStream());
            Console.WriteLine(responseBody);
            Console.Read();
        }

        public static string StreamToString(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

    }
}

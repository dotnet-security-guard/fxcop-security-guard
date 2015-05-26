using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuardSamples
{
    class ClearTextHttp
    {
        static void vulnerableWebRequest()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://google.ca");
        }

        static void safeWebRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://google.ca");
        }

        static void vulnerableUri()
        {
            Uri uri = new Uri("http://google.ca/");
            WebRequest http = HttpWebRequest.Create(uri);
        }

        static void safeUri()
        {
            Uri uri = new Uri("https://google.ca/");
            WebRequest http = HttpWebRequest.Create(uri);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecurityGuardSamples
{

    // Ref: https://msdn.microsoft.com/en-us/library/d271ytdx%28v=vs.110%29.aspx
    class XPathInjection
    {

        public void vulnerableCases(string input) {
            XmlDocument doc = new XmlDocument();
            doc.Load("/secret_config.xml");

            doc.SelectNodes("/Config/Devices/Device[id='"+input+"']");
            doc.SelectSingleNode("/Config/Devices/Device[type='" + input + "']");
        }

        public void falsePositives()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("/secret_config.xml");

            doc.SelectNodes("/Config/Devices/Device[id='1337']");
            doc.SelectSingleNode("/Config/Devices/Device[type='2600']");
        }
    }
}

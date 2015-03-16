using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuardSamples
{
    class ClearTextSocket
    {
        public static void vulnerable()
        {
            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect("google.ca", 80);

        }

        public static void safe()
        {
            //TODO
        }
    }
}

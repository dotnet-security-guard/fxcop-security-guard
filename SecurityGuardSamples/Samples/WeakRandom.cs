using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VulnerableApp
{
    class WeakRandom
    {
        static String generateWeakToken()
        {
            Random rnd = new Random();
            return rnd.Next().ToString();
        }

        static String generateWeakToken2()
        {
            Random rnd = new Random();
            byte[] buffer = new byte[16];
            rnd.NextBytes(buffer);
            return BitConverter.ToString(buffer);
        }

        static String generateSecureToken()
        {

            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            byte[] buffer = new byte[16];
            rnd.GetBytes(buffer);
            return BitConverter.ToString(buffer);
        }
    }
}

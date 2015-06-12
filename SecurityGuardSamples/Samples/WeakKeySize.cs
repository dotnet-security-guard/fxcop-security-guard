using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuardSamples
{
    class WeakKeySize
    {

        //exemple sur https://msdn.microsoft.com/en-us/library/zseyf239(v=vs.110).aspx
        public static void WeakRSAKey(){
            //Create a UnicodeEncoder to convert between byte array and string.
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //Create byte arrays to hold original, encrypted, and decrypted data. 
            byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
            byte[] encryptedData;
            byte[] decryptedData;
			
            //Create a new instance of RSACryptoServiceProvider to generate 
            //public and private key data.  Pass an integer specifying a key- 
            //length of 2048.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(1000);
        }

        //exemple sur https://msdn.microsoft.com/en-us/library/zseyf239(v=vs.110).aspx
        public static void GoodRSAKey()
        {
            //Create a UnicodeEncoder to convert between byte array and string.
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //Create byte arrays to hold original, encrypted, and decrypted data. 
            byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
            byte[] encryptedData;
            byte[] decryptedData;

            //Create a new instance of RSACryptoServiceProvider to generate 
            //public and private key data.  Pass an integer specifying a key- 
            //length of 2048.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048);
        }


    }
}

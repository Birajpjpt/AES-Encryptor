using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Data;

namespace AESEncrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the text:");
            decimal x = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter the key:");
            string key = Console.ReadLine();
            Console.WriteLine(Encryptor(key, x));
            Console.ReadLine();

        }

        public static string Encryptor(string key_aes, decimal x)
        {
            Rijndael computeRijndael = new RijndaelManaged();
            computeRijndael.Key = stringToByte(key_aes, 32);
            computeRijndael.IV = string2byte("0123456789ABCDEF");

            byte[] key = computeRijndael.Key;
            byte[] IV = computeRijndael.IV;

            ICryptoTransform encryptor = computeRijndael.CreateEncryptor(key, IV);

            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            csEncrypt.Write(string2byte(x.ToString()), 0, string2byte(x.ToString()).Length);
            csEncrypt.FlushFinalBlock();

            byte[] encrypted = msEncrypt.ToArray();

            return ByteToString(encrypted);


        }

        public static byte[] string2byte(string newString)
        {
            char[] CharArray = newString.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];

            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }

        public static byte[] stringToByte(string newString, int charLength)
        {
            char[] CharArray = newString.ToCharArray();
            byte[] ByteArray = new byte[charLength];
            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }

        public static string Byte2String(CryptoStream stream)
        {
            string x= "";
            int i = 0;
            do
            {
                i = stream.ReadByte();
                if (i != -1) x += ((char)i);
            } while (i != -1);
            return (x);
        }

        public static string ByteToString(byte[] stream)
        {
            string x = "";
            for (int i = 0; i < stream.Length; i++)
            {
                x += stream[i].ToString("X2");
            }
            return (x);
        }
    }
}

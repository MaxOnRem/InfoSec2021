﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Task7._1
{
    class AsymmetricEncryption
    {
        private static RSAParameters _publicKey, _privateKey;
        public static void AssignNewKey()
        {
            //алгоритм RSA
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        public static byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_publicKey);
                cipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cipherBytes;
        }
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plaintext;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                plaintext = rsa.Decrypt(dataToDecrypt, true);
            }
            return plaintext;
        }
    }
    class Program
    {
        public static void Main()
        {
            const string original = "This message is very secret";
            AsymmetricEncryption.AssignNewKey();
            var encrypted = AsymmetricEncryption.EncryptData(Encoding.UTF8.GetBytes(original));
            var decrypted = AsymmetricEncryption.DecryptData(encrypted);
            Console.WriteLine("Original Message: " + original);
            Console.WriteLine("Encrypted Message: " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Message: " + Encoding.UTF8.GetString(decrypted));
        }
    }
}
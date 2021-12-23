using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ExamTask
{
    class TripleDES
    {
        public byte[] GenerateRandomNumber(int length) // метод для генерації секретного ключа та вектора ініціалізації
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv) // метод для зашифровування
        {
            using (var tripledes = new TripleDESCryptoServiceProvider())
            {
                tripledes.Mode = CipherMode.CBC;
                tripledes.Padding = PaddingMode.PKCS7;
                tripledes.Key = key;
                tripledes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, tripledes.CreateEncryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv) // метод для розшифровування
        {
            using (var tripledes = new TripleDESCryptoServiceProvider())
            {
                tripledes.Mode = CipherMode.CBC;
                tripledes.Padding = PaddingMode.PKCS7;
                tripledes.Key = key;
                tripledes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, tripledes.CreateDecryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string message = "Hello World";
            var tripleDes = new TripleDES(); // ініціалізація екземпляру класу
            var tripleDesKey = tripleDes.GenerateRandomNumber(16); // генерація секретного ключа
            var tripleDesIv = tripleDes.GenerateRandomNumber(8); // генерація вектора ініціалізації
            var tripleDesEncrypted = tripleDes.Encrypt(Encoding.UTF8.GetBytes(message), tripleDesKey, tripleDesIv); // шифрування
            var tripleDesDecrypted = tripleDes.Decrypt(tripleDesEncrypted, tripleDesKey, tripleDesIv); // розшифрування
            var tripleDesDecryptedMessage = Encoding.UTF8.GetString(tripleDesDecrypted);
            Console.WriteLine("TripleDES Encryption");
            Console.WriteLine("--------------------");
            Console.WriteLine("Original Message = " + message);
            Console.WriteLine("Encrypted Message = " + Convert.ToBase64String(tripleDesEncrypted));
            Console.WriteLine("Decrypted Message = " + tripleDesDecryptedMessage);
        }
    }
}
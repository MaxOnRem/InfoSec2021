using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Task4
{
    //клас PBKDF2
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, hashAlgorithm))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            //число ітерацій - номер варіанта*10000 з кроком 50к (10шт)
            const string ToHashPassword= "My name is Max";
            HashPassword(ToHashPassword, 240000);
            HashPassword(ToHashPassword, 290000);
            HashPassword(ToHashPassword,340000);
            HashPassword(ToHashPassword, 390000);
            HashPassword(ToHashPassword,440000);
            HashPassword(ToHashPassword, 490000);
            HashPassword(ToHashPassword, 540000);
            HashPassword(ToHashPassword, 590000);
            HashPassword(ToHashPassword, 640000);
            HashPassword(ToHashPassword, 690000);
            Console.ReadLine();
        }
        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var SS = new Stopwatch();
            SS.Start();
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds, HashAlgorithmName.SHA512);
            SS.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + "> Time estimated: " + SS.ElapsedMilliseconds + "ms");
        }

    }
}
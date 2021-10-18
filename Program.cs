using System;
using System.Security.Cryptography;

namespace PZ1
{
    class Program
    {
        static byte[] GetCryptoRng(int length = 10)
        {
            var rndNumGen = new RNGCryptoServiceProvider();
            var rndNumb = new byte[length];
            rndNumGen.GetBytes(rndNumb);
            return rndNumb;
        }
        static void Main(string[] args)
        {
            Random random = new Random(1236);
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(random.Next(-25, 25));
            }
            Console.WriteLine("_______________");

            Random random1 = new Random(1236);
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(random1.Next(15, 75));
            }
            Console.WriteLine("________________");
            for (int i = 0; i < 6; i++)
            {
                string textStr = Convert.ToBase64String(GetCryptoRng());
                Console.WriteLine(textStr);
            }
        }
        
    }
}
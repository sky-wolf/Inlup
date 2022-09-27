using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    public static class Slummp
    {
        public static int Roll(byte numberSides)
        {
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[1];
                do
                {
                    rngCsp.GetBytes(randomNumber);
                }
                while (!IsFair(randomNumber[0], numberSides));
                return (int)((randomNumber[0] % numberSides) + 1);
            }
        }

        private static bool IsFair(byte roll, byte numSides)
        {

            int fullSetsOfValues = Byte.MaxValue / numSides;

            return roll < numSides * fullSetsOfValues;
        }
    }
}

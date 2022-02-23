using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame
{
    public static class Utils
    {
        public static Random Random = new Random();

        public static string GenerateBinaryString(int length)
        {
            var buffer = new byte[length / 8];
            Utils.Random.NextBytes(buffer);

            return string.Concat(buffer);
        }
    }
}

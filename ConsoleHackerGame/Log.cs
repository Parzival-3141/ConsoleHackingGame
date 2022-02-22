using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame
{
    public static class Log
    {
        public static void Msg(string msg)
        {
            WriteLog(msg, ConsoleColor.Gray);
        }

        public static void Warning(string warning)
        {
            WriteLog(warning, ConsoleColor.Yellow);
        }

        public static void Error(string error)
        {
            WriteLog(error, ConsoleColor.Red);
        }

        public static void WriteLog(string log, ConsoleColor color)
        {
            var ogCol = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine(log);
            
            Console.ForegroundColor = ogCol;
        }
    }
}

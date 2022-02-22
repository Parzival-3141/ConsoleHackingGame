using System;
using ConsoleHackerGame.CLI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame
{
    public static class Program
    {
        public const string Prompt = ">";
        public const string Title = @"
------------------------------------------------

        ██╗░░██╗░█████╗░░█████╗░██╗░░██╗
        ██║░░██║██╔══██╗██╔══██╗╚██╗██╔╝
        ███████║███████║██║░░╚═╝░╚███╔╝░
        ██╔══██║██╔══██║██║░░██╗░██╔██╗░
        ██║░░██║██║░░██║╚█████╔╝██╔╝╚██╗
        ╚═╝░░╚═╝╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝

              created by Parzival
------------------------------------------------
";

        public static bool quitting = false;

        public static Interpreter Interpreter { get; private set; }

        static void Main(string[] args)
        {
            Commands.ShowTitle.Invoke(args); // args doesnt matter here

            Interpreter = new Interpreter();

            while (!quitting)
            {
                Console.Write(Prompt);
                Interpreter.Parse(Console.ReadLine());
            }
        }

        public static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
    }
}

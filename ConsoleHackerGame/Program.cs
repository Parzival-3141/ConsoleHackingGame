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
        private const string PromptHeader = ">";

        public static bool quitting = false;
        public static string Prompt;

        public static bool echo = true;

        public static Interpreter Interpreter { get; private set; }

        static void Main(string[] args)
        {
            Interpreter = new Interpreter();

            Commands.ShowTitle.Invoke(args); // args doesnt matter here
            
            //  Gameloop
            while (!quitting)
            {
                //Prompt = CurrentUser + CurrentDirectory;
                Prompt = PromptHeader;
                
                if(echo)
                    Console.Write(Prompt);
                
                Interpreter.Parse(Console.ReadLine());
            }
        }
    }
}

using System;
using ConsoleHackerGame.CLI;
using ConsoleHackerGame.Network;
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

        public static string Prompt;
        public static bool quitting = false;
        public static bool echo = true;
        public static bool absolutePromptPath = true;

        public static Interpreter Interpreter { get; private set; }
        public static Computer PlayerComputer { get; private set; }
        public static NetworkedDevice ConnectedDevice { get; set; }
        public static Files.Folder CurrentFolder { get; set; }

        static void Main(string[] args)
        {
            NewFileSystem.Test.Run();
            return;

            Interpreter = new Interpreter();

            PlayerComputer = new Computer("player", "192.168.1.1", 256);
            ConnectedDevice = PlayerComputer;
            CurrentFolder = PlayerComputer.FileSystem.Root;
            
            var testCom = new Computer("test", "192.168.1.57", 256);
            PlayerComputer.LinkedDevices.Add(testCom);

            var logFolder = testCom.FileSystem.Root.GetSubFolder("log");
            TextFiles.IRC.GenerateIRCLog(logFolder);
            TextFiles.IRC.GenerateIRCLog(logFolder);
            TextFiles.IRC.GenerateIRCLog(logFolder);


            GeneratePrompt();
            Commands.ShowTitle.Invoke(args); // args doesnt matter here

            //  Gameloop
            while (!quitting)
            {
                if(echo)
                    Console.Write(Prompt);
                
                Interpreter.Parse(Console.ReadLine());
                Console.WriteLine();
            }
        }

        public static void GeneratePrompt()
        {
            string path = Files.FileSystem.GetFullPath(CurrentFolder);

            Prompt = $"[{ConnectedDevice.Name}@{ConnectedDevice.IP}] {path}>";
        }

        public static void ChangeConnectedDevice(NetworkedDevice device)
        {
            if (device == null)
            {
                Log.Warning("Tried to set ConnectedDevice to null!");
                return;
            }

            ConnectedDevice = device;
            CurrentFolder = ConnectedDevice.FileSystem.Root;
            GeneratePrompt();
        }

        internal static Files.FileSystem GetCurrentFileSystem() => ConnectedDevice.FileSystem;
    }
}

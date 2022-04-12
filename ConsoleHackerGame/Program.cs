using System;
using ConsoleHackerGame.CLI;
using ConsoleHackerGame.Network;
using FileSystem;

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

        public static Computer PlayerComputer { get; private set; }
        public static NetworkedDevice ConnectedDevice { get; set; }
        public static Directory CurrentDirectory { get; set; }

        static void Main(string[] args)
        {
            PlayerComputer = new Computer("player", "192.168.1.1", 256);
            ConnectedDevice = PlayerComputer;
            CurrentDirectory = PlayerComputer.FileSystem.Root;
            
            var testCom = new Computer("test", "192.168.1.57", 256);
            PlayerComputer.LinkedDevices.Add(testCom);

            var logDir = testCom.FileSystem.Root.Contents.Find(n => n.Name == "log") as Directory;
            TextFiles.IRC.GenerateIRCLog(logDir);
            TextFiles.IRC.GenerateIRCLog(logDir);
            TextFiles.IRC.GenerateIRCLog(logDir);


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
            Prompt = $"[{ConnectedDevice.Name}@{ConnectedDevice.IP}] {CurrentDirectory.GetPath()}>";
        }

        public static void ChangeConnectedDevice(NetworkedDevice device)
        {
            if (device == null)
            {
                Log.Warning("Tried to set ConnectedDevice to null!");
                return;
            }

            ConnectedDevice = device;
            CurrentDirectory = ConnectedDevice.FileSystem.Root;
            GeneratePrompt();
        }

        internal static FileSystem.FileSystem GetCurrentFileSystem() => ConnectedDevice.FileSystem;
    }
}

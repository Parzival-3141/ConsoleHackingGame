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

        public static Interpreter Interpreter { get; private set; }
        public static Computer PlayerComputer { get; private set; }
        public static NetworkedDevice ConnectedDevice { get; set; }

        /// <summary>
        /// Uses the indices of Subfolders to construct a path.
        /// </summary>
        public static List<int> SubfolderIndexPath { get; set; } = new List<int>();

        static void Main(string[] args)
        {
            Interpreter = new Interpreter();

            PlayerComputer = new Computer("player", "192.168.1.1", 256);
            ConnectedDevice = PlayerComputer;
            
            var testCom = new Computer("test", "192.168.1.57", 256);
            PlayerComputer.LinkedDevices.Add(testCom);

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
            string path = "/";
            for (int i = 0; i < SubfolderIndexPath.Count; i++)
            {
                path += GetCurrentFolderAtDepth(i + 1).name + "/";
            }

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
            SubfolderIndexPath.Clear(); // Sets path to root
            GeneratePrompt();
            //CurrentFolder = device.FileSystem.root;
        }

        public static Files.Folder GetCurrentFolder()
        {
            return GetCurrentFolderAtDepth(SubfolderIndexPath.Count);
        }

        public static Files.Folder GetCurrentFolderAtDepth(int depth)
        {
            var folder = ConnectedDevice.FileSystem.root;

            if(SubfolderIndexPath.Count > 0)
            {
                try
                {
                    for (int i = 0; i < depth; i++)
                    {
                        if (folder.SubFolders.Count > SubfolderIndexPath[i])
                            folder = folder.SubFolders[SubfolderIndexPath[i]];
                    }
                }
                catch { }
            }

            return folder;
        }

        public static List<int> GetSubFolderPathFromPath(string path, Files.Folder currentFolder = null)
        {
            var list = new List<int>();
            var folder = currentFolder ?? GetCurrentFolder();
            string[] pathSegments = path.Split(new char[] { '/', '\\' });

            int num = 0;
            foreach(var pSeg in pathSegments)
            {
                if(pSeg != "" && pSeg != " ")
                {
                    if(pSeg == "..")
                    {
                        list.Add(-1);
                        num++;
                        folder = GetCurrentFolderAtDepth(SubfolderIndexPath.Count - num);
                    }
                    else
                    {
                        bool foundSubFolder = false;
                        for (int i = 0; i < folder.SubFolders.Count; i++)
                        {
                            if (folder.SubFolders[i].name == pSeg)
                            {
                                folder = folder.SubFolders[i];
                                foundSubFolder = true;
                                list.Add(i);
                                break;
                            }
                        }

                        if (!foundSubFolder)
                        {
                            Console.WriteLine("Invalid Path");
                            list.Clear();
                            return list;
                        }
                    }

                }
            }
            
            return list;
        }
    }
}

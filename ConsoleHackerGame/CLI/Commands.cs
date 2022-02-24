using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Net = ConsoleHackerGame.Network;

namespace ConsoleHackerGame.CLI
{
    public static class Commands
    {
        static Commands()
        {
            SortCMDs();
        }

        public static readonly List<CMD> cmds = new List<CMD>()
        {
            new CMD("quit"      , (args) => Quit(args), "Quits the application"),
            new CMD("clear"     , (args) => Console.Clear(), "Clears the console"),
            new CMD("echo"      , (args) => Echo(args), "Prints text to the console", "echo {on|off} [text ...]"),
            new CMD("expr"      , (args) => Expr(args), "Evaluate integer expressions", "expr <expression>"),
            new CMD("help"      , (args) => Help(args), "Displays information about commands", "help <command> | command [-h]"),
            new CMD("title"     , (args) => ShowTitle(args), "Prints the title screen"),
            new CMD("whoami"    , (args) => WhoAmI(args), "Displays the name and IP of the current host"),
            new CMD("sysinfo"   , (args) => SystemInfo(args), "Displays information about the current host"),
            new CMD("connect"   , (args) => Connect(args), "Connect to a remote host.", "connect <IP>"),
            new CMD("disconnect", (args) => Disconnect(args), "Disconnect from a remote host"),
            new CMD("ls"        , (args) => LS(args), "Displays a list of files and subfolders in a directory"),
            new CMD("cd"        , (args) => CD(args), "Changes the working directory", "cd <path>\n\nuse '..' to go up a directory"),
            new CMD("mping"     , (args) => MPing(args), "Searches a network for devices", "mping [remote-host]"),
            new CMD("cat"       , (args) => Cat(args), "Display a file's contents", "cat <file-name>"),
        };

        #region CMD Methods

        public static void SortCMDs()
        {
            cmds.Sort(new Comparison<CMD>((c1, c2) => string.Compare(c1.Name, c2.Name)));
        }

        public static CMD GetCMD(string name)
        {
            return cmds.Find((c) => c.Name == name);
        }

        public static bool TryGetCMD(string name, out CMD cmd)
        {
            cmd = GetCMD(name);

            if (cmd == null)
                return false;

            return true;
        }

        #endregion

        #region CMD Implementations

        public delegate void CMDMethod(string[] args);

        public static CMDMethod Echo = (args) =>
        {
            if(args.Length == 1)
            {
                switch (args[0].ToLowerInvariant())
                {
                    case "on":  Program.echo = true;  return;
                    case "off": Program.echo = false; return;
                    default: break;
                }
            }

            if (Program.echo)
            {
                string output = "";
                foreach (var s in args)
                {
                    output += s + ' ';
                }
                Console.WriteLine(output);
            }
        };

        public static CMDMethod Quit = (args) =>
        {
            void AwaitYNInput()
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        Program.quitting = true;
                        Console.WriteLine();
                        break;

                    case ConsoleKey.N:
                        Console.WriteLine();
                        break;

                    default:
                        Console.Write("\nAnswer [y/n]. ");
                        AwaitYNInput();
                        break;
                }
            }

            Console.Write("Are you sure [y/n]? ");
            AwaitYNInput();
        };

        public static CMDMethod Expr = (args) =>
        {
            int Operation(int num1, int num2, char @operator)
            {
                switch (@operator)
                {
                    case '+': return num1 + num2;
                    case '-': return num1 - num2;
                    case '*': return num1 * num2;
                    case '/': return num1 / num2;
                    default: return 0;
                }
            }

            int PrecedenceOrder(char @operator)
            {
                if (@operator == '+' || @operator == '-')
                    return 1;

                if (@operator == '*' || @operator == '/')
                    return 2;

                return 0;
            }

            //string expr = args.Aggregate((s1, s2) => s1 + s2);
            string expr = string.Empty;

            if (args.Length < 1)
            {
                Console.WriteLine("An expression is required.");
                return;
            }

            foreach(var s in args)
            {
                if (s.Any((c) => char.IsLetter(c)))
                {
                    continue;
                }

                expr += s;
            }


            Stack<int> values = new Stack<int>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < expr.Length; i++)
            {
                if (expr[i] == '(')
                {
                    operators.Push(expr[i]);
                }

                else if (int.TryParse(expr[i].ToString(), out _))
                {
                    int val = 0;

                    while (i < expr.Length && int.TryParse(expr[i].ToString(), out int x))
                    {
                        val = (val * 10) + x;
                        i++;
                    }

                    values.Push(val);
                    i--;
                }

                else if (expr[i] == ')')
                {
                    while (operators.Count != 0 && operators.Peek() != '(')
                    {
                        int val2 = values.Pop();
                        int val1 = values.Pop();
                        char op = operators.Pop();

                        values.Push(Operation(val1, val2, op));
                    }

                    if (operators.Count != 0)
                        operators.Pop();
                }
                else
                {
                    while (operators.Count != 0 && PrecedenceOrder(operators.Peek()) >= PrecedenceOrder(expr[i]))
                    {
                        int val2 = values.Pop();
                        int val1 = values.Pop();
                        char op = operators.Pop();

                        values.Push(Operation(val1, val2, op));
                    }

                    operators.Push(expr[i]);
                }

            }

            while (operators.Count != 0)
            {
                int val2 = values.Pop();
                int val1 = values.Pop();
                char op = operators.Pop();

                values.Push(Operation(val1, val2, op));
            }

            Console.WriteLine(values.Peek());
        };

        public static CMDMethod Help = (args) =>
        {
            List<CMD> cmds = new List<CMD>();
            bool showAllCMDs = false;

            if (args.Length > 0 && TryGetCMD(args[0], out var cmd))
            {
                cmds.Add(cmd);
            }
            else
            {
                cmds = Commands.cmds;
                showAllCMDs = true;
            }

            Console.WriteLine();

            if(showAllCMDs)
                Console.WriteLine("------------------------------------------------");
            
            int gapLength = 16;
            string indent = @"    ";
            for (int i = 0; i < cmds.Count; i++)
            {
                var gap = new string(' ', gapLength - cmds[i].Name.Length);

                Console.WriteLine(indent + cmds[i].Name + gap + cmds[i].InfoText);
                
                if (!showAllCMDs && cmds[i].HelpText != "")
                    Console.WriteLine(indent + new string(' ', gapLength) + cmds[i].HelpText);
            }

            if (showAllCMDs)
            {
                Console.WriteLine();
                Console.WriteLine(indent + "Use 'command -h' for more info on the command");
                Console.WriteLine("------------------------------------------------");
            }
        };

        public static CMDMethod ShowTitle = (args) =>
        {
            Console.Write(Program.Title + "\n");
        };

        public static CMDMethod SystemInfo = (args) =>
        {
            var device = Program.ConnectedDevice;

            var sysInfo = new (string title, string value)[] 
            { 
                ("Name:", device.Name), 
                ("IP:", device.IP),
                ("RAM:", device.TotalRAM.ToString())
            };

            Console.WriteLine("\n------------------------------------------------");

            int gapLength = 12;
            string indent = @"    ";
            for (int i = 0; i < sysInfo.Length; i++)
            {
                var gap = new string(' ', gapLength - sysInfo[i].title.Length);

                Console.WriteLine(indent + sysInfo[i].title + gap + sysInfo[i].value);
            }

            Console.WriteLine("------------------------------------------------");
        };

        public static CMDMethod WhoAmI = (args) =>
        {
            Console.WriteLine(Program.ConnectedDevice.Name);
            Console.WriteLine(Program.ConnectedDevice.IP);
        };

        public static CMDMethod Connect = (args) =>
        {
            if(args.Length < 1)
            {
                Console.WriteLine("An IP is required");
                return;
            }

            //@Incomplete: subject to change
            if (!Net.Utils.TryGetLinkedDevice(args[0], out var device))
            {
                Thread.Sleep(2000);
                Console.WriteLine("Connection Refused.");
                return;
            }

            if (device == Program.ConnectedDevice)
            {
                Console.WriteLine("Already connected to this IP.");
                return;
            }

            Thread.Sleep(1000);
            Program.ChangeConnectedDevice(device);
            Console.WriteLine("Connection Successful.");
        };

        public static CMDMethod Disconnect = (args) =>
        {
            if(Program.ConnectedDevice == Program.PlayerComputer)
            {
                Console.WriteLine("Cannot disconnect from localhost.");
                return;
            }

            Program.ChangeConnectedDevice(Program.PlayerComputer);
            Console.WriteLine("Disconnected.");
        };

        public static CMDMethod LS = (args) =>
        {
            //var fs = Program.ConnectedDevice.FileSystem;
            //var directory = Enumerable.Concat<Files.IFileBase>(fs.root.SubFolders, fs.root.Files);
            var currentDir = Files.Utils.GetCurrentFolder();

            if (Program.SubfolderIndexPath.Count > 0)
                Console.WriteLine("..");

            foreach(var folder in currentDir.SubFolders)
            {
                Console.WriteLine(folder.name + '/');
            }

            foreach (var file in currentDir.Files)
            {
                Console.WriteLine(file.name);
            }
        };

        public static CMDMethod CD = (args) =>
        {
            var currentDir = Files.Utils.GetCurrentFolder();

            if(args.Length < 1)
            {
                Console.WriteLine("A path is required.");
                return;
            }

            if (args[0] == "/")
                Program.SubfolderIndexPath.Clear();
            else if(args[0] == "..")
            {
                if(Program.SubfolderIndexPath.Count > 0)
                {
                    Program.SubfolderIndexPath.RemoveAt(Program.SubfolderIndexPath.Count - 1);
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (args[0].StartsWith("/"))
                    Program.SubfolderIndexPath.Clear();

                List<int> tempIndexPath = Files.Utils.GetSubFolderPathFromPath(args[0]);
                for (int i = 0; i < tempIndexPath.Count; i++)
                {
                    if(tempIndexPath[i] == -1)
                        Program.SubfolderIndexPath.RemoveAt(Program.SubfolderIndexPath.Count - 1);
                    else
                        Program.SubfolderIndexPath.Add(tempIndexPath[i]);

                }
            }

            Program.GeneratePrompt();
        };

        public static CMDMethod MPing = (args) =>
        {
            var searchDevice = Program.ConnectedDevice;
            
            if (args.Length > 0)
            {
                if(Net.Utils.TryGetLinkedDevice(args[0], out var d))
                    searchDevice = d;
                else
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Invalid host.");
                    return;
                }
            }

            if(searchDevice.LinkedDevices.Count < 1)
            {
                Thread.Sleep(500);
                Console.WriteLine($"No linked devices found on '{searchDevice.Name}'.");
                return;
            }

            string indent = @"    ";
            foreach(var dev in searchDevice.LinkedDevices)
            {
                Thread.Sleep(500);
                var gap = new string(' ', 12 - dev.Name.Length);
                Console.WriteLine(indent + dev.Name + gap + dev.IP);
            }
        };

        public static CMDMethod Cat = (args) =>
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Requires a file path.");
                return;
            }

            //  @Incomplete:
            //  Ideally you could pass a path and it'll search the path for the file

            //List<int> indexPath = Files.Utils.GetSubFolderPathFromPath(args[0]);
            //Files.Utils.GetCurrentFolderAtDepth(indexPath.Last());

            var currentFolder = Files.Utils.GetCurrentFolder();
            if(!currentFolder.TryGetFile(args[0], out var file))
            {
                Console.WriteLine("Invalid file path.");
                return;
            }

            Console.WriteLine($"\n{file.name}\n\n{file.data}\n");
        };

        #endregion
    }
}

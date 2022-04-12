using System;
using System.Linq;

namespace ConsoleHackerGame.CLI
{
    public static class Interpreter
    {
        public static bool Parse(string line)
        {
            line = line.Replace(Program.Prompt, string.Empty).Trim(' ');

            string[] commands = line.Split(';'); 
           
            foreach(string command in commands)
            {
                string[] cmdSegments = command.Trim(' ').Split(' ');

                if (cmdSegments?.Length > 0)
                {
                    string cmdName = cmdSegments[0];

                    if (cmdName == string.Empty)
                        continue;

                    if (!Commands.TryGetCMD(cmdName, out var cmd))
                    {
                        Console.WriteLine($"Command '{cmdName}' not found.");
                        return false;
                    }

                    if (cmdSegments.Contains("-h"))
                    {
                        Commands.Help.Invoke(new string[] { cmd.Name });
                        continue;
                    }

                    try
                    {
                        cmd.Invoke(cmdSegments.Skip(1).ToArray());
                        
                        //@Incomplete:
                        //Program.ConnectedDevice.FileSystem.root.GetSubFolder("log")?.Files?.Add(new Files.File(cmd.Name, cmd.Name));
                    }
                    catch (Exception e)
                    {
                        Log.Error("Command Error: " + e.Message);
                        if (command.Contains("-v"))
                            Log.Error(e.StackTrace);
                        
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
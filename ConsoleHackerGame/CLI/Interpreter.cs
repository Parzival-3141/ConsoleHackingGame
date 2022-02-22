using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame.CLI
{
    public class Interpreter
    {
        public Interpreter()
        {
            Commands.SortCMDs();
        }

        public bool Parse(string line)
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

                    if (command.Contains("-h"))
                    {
                        Commands.Help.Invoke(new string[] { cmd.Name });
                        continue;
                    }

                    try
                    {
                        cmd.Invoke(cmdSegments.Skip(1).ToArray());
                    }
                    catch (Exception e)
                    {
                        Log.Error("Command Error: " + e.Message);
                        if (command.Contains("-v"))
                            Log.Error(e.StackTrace);
                    }
                }
            }

            return true;
        }
    }
}
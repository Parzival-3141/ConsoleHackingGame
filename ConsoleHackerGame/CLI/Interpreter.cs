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
            string[] lineSegments = line.Replace(Program.Prompt, string.Empty).Trim(' ').Split(' ');
           
            if (lineSegments?.Length > 0)
            {
                string cmdName = lineSegments[0];

                if (cmdName == string.Empty)
                    return true;

                if (!Commands.TryGetCMD(cmdName, out var cmd))
                {
                    Console.WriteLine($"Command '{cmdName}' not found.");
                    return false;
                }

                if (line.Contains("-h"))
                {
                    Commands.Help.Invoke(new string[] { cmd.Name });
                    return true;
                }

                try
                {
                    cmd.Invoke(lineSegments.Skip(1).ToArray());
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error("Command Error: " + e.Message);
                    if (line.Contains("-v"))
                        Log.Error(e.StackTrace);
                }
            }

            return false;
        }
    }
}
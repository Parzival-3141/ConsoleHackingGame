using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame
{
    public class Interpreter
    {
        public readonly List<CMD> cmds = new List<CMD>()
        {
            new CMD("quit", (args) => Commands.Quit(args), "Quits the application."),
            new CMD("clear", (args) => Console.Clear(), "Clears the console."),
            new CMD("echo", (args) => Commands.Echo(args), "Prints the arguements to the console."),
            new CMD("expr", (args) => Commands.Expr(args), "Evaluate integer expressions."),
            new CMD("help", (args) => Commands.Help(args), "List all Commands."),
            new CMD("title", (args) => Commands.ShowTitle(args), "Prints the title screen."),
        };

        public bool TryParse(string line)
        {
            string[] lineSegments = line.Replace(Program.Prompt, string.Empty).Trim(' ').Split(' ');
           
            if (lineSegments?.Length > 0)
            {
                string cmdName = lineSegments[0];

                if (cmdName == string.Empty)
                    return true;

                if (!TryGetCMD(cmdName, out var cmd))
                    return false;

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
                    Program.WriteError("Command Error: " + e.Message);
                    if (line.Contains("-v"))

                        Program.WriteError(e.StackTrace);
                }
            }

            return false;
        }

        public bool TryGetCMD(string name, out CMD cmd)
        {
            cmd = cmds.Find((c) => c.Name == name);

            if (cmd == null)
            {
                Console.WriteLine($"Command '{name}' not found.");
                return false;
            }

            return true;
        }


        public class CMD
        {
            public string Name { get; private set; }
            public string InfoText { get; private set; }
            public string HelpText { get; private set; }

            private Commands.CMDMethod action;

            public CMD(string name, Commands.CMDMethod action, string info, string help = "")
            {
                this.Name = name;
                this.InfoText = info;
                this.HelpText = help;
                this.action = action;
            }

            public void Invoke(string[] args)
            {
                action?.Invoke(args);
            }
        }
    }
}
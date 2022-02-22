using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame.CLI
{
    public static class Commands
    {
        public delegate void CMDMethod(string[] args);

        public static CMDMethod Echo => (args) =>
        {
            string output = "";
            foreach (var s in args)
            {
                output += s + ' ';
            }
            Console.WriteLine(output);
        };

        public static CMDMethod Quit => (args) =>
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

        public static CMDMethod Expr => (args) =>
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

        public static CMDMethod Help => (args) =>
        {
            List<Interpreter.CMD> cmds = new List<Interpreter.CMD>();
            bool showAllCMDs = false;

            if (args.Length > 0 && Program.Interpreter.TryGetCMD(args[0], out var cmd))
            {
                cmds.Add(cmd);
            }
            else
            {
                cmds = Program.Interpreter.cmds;
                showAllCMDs = true;
            }

            if(showAllCMDs)
                Console.WriteLine("------------------------------------------------");

            int gapLength = 12;
            string indent = @"    ";
            for (int i = 0; i < cmds.Count; i++)
            {
                string gap = "";
                for (int j = 0; j < gapLength - cmds[i].Name.Length; j++)
                {
                    gap += " ";
                }

                Console.WriteLine(indent + cmds[i].Name + gap + cmds[i].InfoText);
            }

            if(showAllCMDs)
                Console.WriteLine("------------------------------------------------");
        };

        public static CMDMethod ShowTitle = (args) =>
        {
            Console.Write(Program.Title + "\n");
        };
    }
}

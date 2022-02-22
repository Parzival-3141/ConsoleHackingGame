namespace ConsoleHackerGame.CLI
{
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

namespace ConsoleHackerGame.Files
{
    public class File : IFileBase
    {
        public string name;
        public string data;

        public File(string fileName, string data)
        {
            name = fileName;
            this.data = data;
        }

        public string GetName()
        {
            return name;
        }
    }
}

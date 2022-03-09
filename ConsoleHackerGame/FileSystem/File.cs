namespace ConsoleHackerGame.Files
{
    public class File : IFileBase
    {
        public string name;
        public string data;
        public Folder parent;

        public File(string name, string data, Folder parent)
        {
            this.name = name;
            this.data = data;
            this.parent = parent;
        }

        public string GetName()
        {
            return name;
        }

        public Folder GetParentFolder()
        {
            return parent;
        }
    }
}

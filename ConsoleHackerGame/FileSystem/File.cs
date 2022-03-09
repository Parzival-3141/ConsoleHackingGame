namespace ConsoleHackerGame.Files
{
    public class File : IFileBase
    {
        public string name;
        public string data;
        public Folder parent;

        public File(string fileName, string data, Folder parent)
        {
            name = fileName;
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

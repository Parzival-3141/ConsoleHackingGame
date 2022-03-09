using System.Collections.Generic;
using ConsoleHackerGame;

namespace ConsoleHackerGame.Files
{
    public class Folder : IFileBase
    {
        public List<IFileBase> Contents { get; set; } = new List<IFileBase>();

        public string name;
        public Folder parent;

        private Folder(string name, Folder parent)
        {
            this.name = name;
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

        public Folder GetSubFolder(string name)
        {
            return Contents.Find(f => f.GetName() == name) as Folder;
        }

        public File GetFile(string name)
        {
            return Contents.Find((f) => f.GetName() == name) as File;
        }

        public void AddFolder(string name)
        {
            if (name.Contains("."))
            {
                System.Console.WriteLine("Invalid folder name.");
                return;
            }

            name = name.Replace("\\", "/");
            name = name.Replace("/" , string.Empty);

            Contents.Add(new Folder(name, this));
        }

        public void AddFile(string name, string data)
        {
            Contents.Add(new File(name, data, this));
        }

        public static Folder CreateRootFolder()
        {
            return new Folder("/", null);
        }
    }
}

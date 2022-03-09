using System.Collections.Generic;

namespace ConsoleHackerGame.Files
{
    public class Folder : IFileBase
    {
        public List<IFileBase> Contents { get; set; } = new List<IFileBase>();

        public string name;
        public Folder parent;

        public Folder(string name, Folder parent)
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

        public Folder GetSubFolder(string folderName)
        {
            return Contents.Find(f => f.GetName() == folderName) as Folder;
        }

        public bool TryGetFile(string fileName, out File file)
        {
            file = Contents.Find((f) => f.GetName() == fileName) as File;
            return file != null;
        }
    }
}

using System.Collections.Generic;

namespace ConsoleHackerGame.Files
{
    public class Folder : IFileBase
    {
        public List<File> Files { get; private set; } = new List<File>();
        public List<Folder> SubFolders { get; private set; } = new List<Folder>();

        public string name;

        public Folder(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public Folder GetSubFolder(string folderName)
        {
            return SubFolders.Find(f => f.name == folderName);
        }

        public bool TryGetFile(string fileName, out File file)
        {
            file = Files.Find((f) => f.name == fileName);
            return file != null;
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHG = ConsoleHackerGame;

namespace ConsoleHackerGame.Files
{
    public class FileSystem
    {
        public Folder root;

        public FileSystem()
        {
            root = new Folder("/", null);
            root.Contents.Add(new Folder("sys", root));
            root.Contents.Add(new Folder("home", root));
            root.Contents.Add(new Folder("bin", root));
            root.Contents.Add(new Folder("log", root));

            root.GetSubFolder("home").Contents.Add(new Folder("test", root.GetSubFolder("home")));

            GenerateSysFiles();
        }

        private void GenerateSysFiles()
        {
            Folder sys = root.GetSubFolder("sys");
            sys.Contents.Add(new File("os-config.sys", CHG.Utils.GenerateBinaryString(1000), sys));
            sys.Contents.Add(new File("bootcfg.dll"  , CHG.Utils.GenerateBinaryString(1000), sys));
            sys.Contents.Add(new File("netcfgx.dll"  , CHG.Utils.GenerateBinaryString(1000), sys));
        }

        public IFileBase Lookup(string path)
        {
            Folder curDir;
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
                curDir = root;
            }
            else
            {
                curDir = Program.CurrentFolder;
            }

            IFileBase result = null;
            foreach (var pSeg in path.Split('/'))
            {
                if (curDir == null)
                    return null; // path not found
                
                if (pSeg == "" || pSeg == " ")
                    continue;

                if (pSeg == "..")
                    result = curDir.parent;
                else
                    result = curDir.Contents.Find(f => f.GetName() == pSeg); // again, put some error check here

                curDir = result as Folder;
            }

            return result;
        }

        public static string GetFullPath(IFileBase file)
        {
            Folder parentDir = file.GetParentFolder();
            string path = file.GetName();

            if (file.GetName() == "/" && parentDir == null)
                return path;

            if (!Path.HasExtension(file.GetName()))
                path += "/";
            
            while(parentDir.GetParentFolder() != null)
            {
                parentDir = parentDir.GetParentFolder();
                path = path.Insert(0, parentDir.name + "/");
            }

            return path;
        }


        //public static FileSystem Generate()
        //{
        //    return new FileSystem();
        //}
    }
}

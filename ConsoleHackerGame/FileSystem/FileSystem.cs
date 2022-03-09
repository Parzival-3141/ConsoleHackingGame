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
        public Folder Root { get; private set; }

        public FileSystem()
        {
            Root = Folder.CreateRootFolder();
            Root.AddFolder("sys");
            Root.AddFolder("home");
            Root.AddFolder("bin");
            Root.AddFolder("log");

            Root.GetSubFolder("home").AddFolder("test");

            GenerateSysFiles();
        }

        private void GenerateSysFiles()
        {
            Folder sys = Root.GetSubFolder("sys");
            sys.AddFile("os-config.sys", CHG.Utils.GenerateBinaryString(1000));
            sys.AddFile("bootcfg.dll"  , CHG.Utils.GenerateBinaryString(1000));
            sys.AddFile("netcfgx.dll"  , CHG.Utils.GenerateBinaryString(1000));
        }

        public IFileBase Lookup(string path)
        {
            Folder curDir;
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
                curDir = Root;
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

        public static bool TryPathLookup(string path, out IFileBase file)
        {
            file = null;
            Folder currentPath = path.StartsWith("/") ? Program.GetCurrentFileSystem().Root : Program.CurrentFolder;
            string[] pathSegments = path.Split(new char[] { '/', '\\' });

            for (int i = 0; i < pathSegments.Length; i++)
            {
                string pSeg = pathSegments[i];
                
                if (pSeg == "" || pSeg == " ")
                    continue;

                if (pSeg == "..")
                {
                    currentPath = currentPath.parent ?? currentPath;
                    file = currentPath;
                }
                else if (pSeg == ".")
                {
                    //  Stay at the current path, i.e. do nothing
                    //  currentDir = currentDir
                    file = currentPath;
                }
                else
                {
                    var f = currentPath.Contents.Find(x => x.GetName() == pSeg);

                    if (f == null)
                    {
                        Console.WriteLine("Cannot find path!");
                        return false;
                    }

                    if ((i != pathSegments.Length - 1 && Path.HasExtension(f.GetName())) && !(f is Folder))
                    {
                        Console.WriteLine("Invalid path!");
                        return false;
                    }

                    file = f;
                    currentPath = f as Folder;
                }
            }

            return true;
        }

        public static string GetFullPath(IFileBase file)
        {
            string path = file.GetName();
            Folder parentDir = file.GetParentFolder();

            if (file.GetName() == "/" && parentDir == null)
                return path;

            if (!Path.HasExtension(file.GetName()))
                path += "/";
            

            while(parentDir != null)
            {
                var s = parentDir.name == "/" ? string.Empty : "/";

                path = path.Insert(0, parentDir.name + s);
                parentDir = parentDir.GetParentFolder();
            }

            return path;
        }


        //public static FileSystem Generate()
        //{
        //    return new FileSystem();
        //}
    }
}

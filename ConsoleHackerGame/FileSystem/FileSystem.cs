using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame.Files
{
    internal class FileSystem
    {
        public Folder root;

        public FileSystem()
        {
            root = new Folder("/");
            root.SubFolders.Add(new Folder("sys"));
            root.SubFolders.Add(new Folder("home"));
            root.SubFolders.Add(new Folder("bin"));
            root.SubFolders.Add(new Folder("log"));
            GenerateSysFiles();
        }

        private void GenerateSysFiles()
        {
            Folder sys = root.GetSubFolder("sys");
            sys.Files.Add(new File("os-config.sys", Utils.GenerateBinaryString(50)));
            sys.Files.Add(new File("bootcfg.dll"  , Utils.GenerateBinaryString(50)));
            sys.Files.Add(new File("netcfgx.dll"  , Utils.GenerateBinaryString(50)));
        }


        //public static FileSystem Generate()
        //{
        //    return new FileSystem();
        //}
    }

    internal interface IFileBase
    {
        string GetName();
    }
}

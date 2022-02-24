using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHG = ConsoleHackerGame;

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
            sys.Files.Add(new File("os-config.sys", CHG.Utils.GenerateBinaryString(1000)));
            sys.Files.Add(new File("bootcfg.dll"  , CHG.Utils.GenerateBinaryString(1000)));
            sys.Files.Add(new File("netcfgx.dll"  , CHG.Utils.GenerateBinaryString(1000)));
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

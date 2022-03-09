using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame
{
    public static partial class Utils
    {
        public static Random Random = new Random();

        public static string GenerateBinaryString(int length)
        {
            var buffer = new byte[length / 8];
            Random.NextBytes(buffer);

            string s = "";
            foreach(var b in buffer)
                s += Convert.ToString(b, 2);
            return s;
        }
    }
}

namespace ConsoleHackerGame.Files
{
    public static partial class Utils
    {
        public static bool TryGetSubFolder(this Folder f, string name, out Folder folder)
        {
            folder = f.GetSubFolder(name);
            return folder != null;
        }

        public static bool TryGetFile(this Folder f, string name, out File file)
        {
            file = f.GetFile(name);
            return file != null;
        }


        //public static Folder GetCurrentFolder()
        //{
        //    return GetCurrentFolderAtDepth(Program.SubfolderIndexPath.Count);
        //}

        //public static Folder GetCurrentFolderAtDepth(int depth)
        //{
        //    var folder = Program.ConnectedDevice.FileSystem.root;

        //    if (Program.SubfolderIndexPath.Count > 0)
        //    {
        //        try
        //        {
        //            for (int i = 0; i < depth; i++)
        //            {
        //                if (folder.Contents.Count > Program.SubfolderIndexPath[i])
        //                    folder = folder.Contents[Program.SubfolderIndexPath[i]];
        //            }
        //        }
        //        catch { }
        //    }

        //    return folder;
        //}

        //public static List<int> GetSubFolderPathFromPath(string path, Folder currentFolder = null)
        //{
        //    var list = new List<int>();
        //    var folder = currentFolder ?? GetCurrentFolder();
        //    string[] pathSegments = path.Split(new char[] { '/', '\\' });

        //    int num = 0;
        //    foreach (var pSeg in pathSegments)
        //    {
        //        if (pSeg != "" && pSeg != " ")
        //        {
        //            if (pSeg == "..")
        //            {
        //                list.Add(-1);
        //                num++;
        //                folder = GetCurrentFolderAtDepth(Program.SubfolderIndexPath.Count - num);
        //            }
        //            else
        //            {
        //                bool foundSubFolder = false;
        //                for (int i = 0; i < folder.Contents.Count; i++)
        //                {
        //                    if (folder.Contents[i].name == pSeg)
        //                    {
        //                        folder = folder.Contents[i];
        //                        foundSubFolder = true;
        //                        list.Add(i);
        //                        break;
        //                    }
        //                }

        //                if (!foundSubFolder)
        //                {
        //                    Console.WriteLine("Invalid Path");
        //                    list.Clear();
        //                    return list;
        //                }
        //            }

        //        }
        //    }

        //    return list;
        //}
    }
}

namespace ConsoleHackerGame.Network
{
    public static partial class Utils
    {
        //@Incomplete: subject to change
        public static bool TryGetLinkedDevice(NetworkedDevice host, string searchIP, out NetworkedDevice device)
        {
            device = host.LinkedDevices.Find(d => d.IP == searchIP || d.Name == searchIP);
            return device != null;
        }

        /// <summary>
        /// Searches <seealso cref="Program.ConnectedDevice"/> for <paramref name="searchIP"/>
        /// </summary>
        public static bool TryGetLinkedDevice(string searchIP, out NetworkedDevice device)
        {
            return TryGetLinkedDevice(Program.ConnectedDevice, searchIP, out device);
        }
    }
}

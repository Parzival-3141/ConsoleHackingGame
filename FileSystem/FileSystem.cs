using System;
using IO = System.IO;
using System.Linq;

namespace FileSystem
{
    public class FileSystem
    {
        public const string ROOT_NAME = "/";
        public static readonly char[] PATH_SEPERATORS = { '/', '\\' };

        public Directory Root { get; }

        public FileSystem()
        {
            Root = new Directory(ROOT_NAME, null);
        }

        public FileSystem(Action<FileSystem> onCreate)
        {
            Root = new Directory(ROOT_NAME, null);
            onCreate?.Invoke(this);
        }

        //  @Incomplete:
        //  Maybe change console write/return null to throws
        //  and eave error handling up to the user?
        public INodeBase Search(string path, Directory currentDir)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new FormatException("Invalid path");
            }

            INodeBase result = null;

            if (IO.Path.IsPathRooted(path) || currentDir == null)
                currentDir = Root;

            string[] pathSegments 
                = path.Split(PATH_SEPERATORS)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            for (int i = 0; i < pathSegments.Length; i++)
            {
                string pSeg = pathSegments[i];

                //if (pSeg == "" || pSeg == " ")
                //    continue;

                if(pSeg == "..")
                {
                    currentDir = currentDir.ParentDirectory ?? currentDir;
                    result = currentDir;
                    continue;
                }
                else if(pSeg == ".")
                {
                    //  Stay at current directory
                    result = currentDir;
                    continue;
                }


                INodeBase node = currentDir.Contents.Find(n => n.Name == pSeg);

                if(node == null)
                {
                    throw new ArgumentException("Cannot find path");
                }

                switch (node.NodeType)
                {
                    case NodeType.Directory:
                        currentDir = node as Directory;
                        break;

                    case NodeType.File:
                        if(i != pathSegments.Length - 1)
                        {
                            throw new FormatException("Invalid path: cannot access file as a directory!");
                        }
                        break;

                    default:
                        throw new NotImplementedException($"NodeType '{node.NodeType}' not accounted for");
                }

                result = node;
            }

            return result;
        }

        public Directory CreateDirectory(string name, string absolutePath)
        {
            var parentDir = Search(absolutePath, Root) as Directory;
            return parentDir.CreateChildDirectory(name);
        }

        public File CreateFile(string name, string contents, string absolutePath)
        {
            var parentDir = Search(absolutePath, Root) as Directory;
            return parentDir.CreateChildFile(name, contents);
        }

        public void DeleteNode(string absolutePath)
        {
            Search(absolutePath, Root).Delete();
        }
    }
}

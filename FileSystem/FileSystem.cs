using System;
using IO = System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewFileSystem
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

        //  @Incomplete:
        //  Maybe change console write/return null to throws
        //  and eave error handling up to the user?
        public INodeBase Search(string path, Directory currentDir)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Invalid path");
                return null;
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
                    continue;
                }
                else if(pSeg == ".")
                {
                    //  Stay at current directory, i.e. do nothing
                    continue;
                }


                INodeBase node = currentDir.Contents.Find(n => n.Name == pSeg);

                if(node == null)
                {
                    Console.WriteLine("Cannot find path");
                    return null;
                }

                //if (node.NodeType == NodeType.File && i != pathSegments.Length - 1)
                //{
                //    Console.WriteLine("Invalid path: cannot access file as a directory!");
                //    return null;
                //}

                //if (node.NodeType == NodeType.Directory)
                //    currentDir = node as Directory;


                switch (node.NodeType)
                {
                    case NodeType.Directory:
                        currentDir = node as Directory;
                        break;

                    case NodeType.File:
                        if(i != pathSegments.Length - 1)
                        {
                            Console.WriteLine("Invalid path: cannot access file as a directory!");
                            return null;
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
            //  @Incomplete:
            //  translate path into the correlated directory
            //  i.e. path = '/bin/goodies/' return 'goodies' directory

            var parentDir = Search(absolutePath, Root) as Directory;

            //  create at path and return directory
            //  i.e. goodies.Contents.Add(new Directory(name, goodies);
            //  or goodies.CreateChildDirectory(name);

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

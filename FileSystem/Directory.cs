using System.Collections.Generic;

namespace NewFileSystem
{
    public class Directory : INode<Directory>
    {
        /// <param name="parent">Can only be <see langword="null"/> if creating a root <see cref="Directory"/></param>
        public Directory(string name, Directory parent)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException("Must contain alphanumeric characters", nameof(name));

            if (name == FileSystem.ROOT_NAME ^ parent == null)
                throw new System.ArgumentException("Invalid Root");

            if (parent?.Contents.Exists(dir => dir.Name == name) ?? false)
                throw new System.ArgumentException($"Parent Directory already contains Node '{name}'");

            this.Name = name;
            this.ParentDirectory = parent;
            this.Contents = new List<INodeBase>();
        }

        public string Name { get; private set; }
        public Directory ParentDirectory { get; private set; }
        public bool IsRoot => ParentDirectory == null && Name == "/";
        public NodeType NodeType => NodeType.Directory;
        public bool IsDeleted { get; private set; } = false;

        public List<INodeBase> Contents { get; } 

        //  @Incomplete: Not sure if I have a use for this yet
        //  Need to enforce IsDeleted on every open facing thing
        public void Delete()
        {
            if (IsDeleted) return;

            if (IsRoot)
            {
                System.Console.WriteLine("Cannot delete root directory!"); 
                return;
            }

            for(int i = Contents.Count - 1; i >= 0; i--)
            {
                Contents[i].Delete();
            }
            
            //Contents.Clear();
            ParentDirectory.Contents.Remove(this);
            IsDeleted = true;
        }


        public Directory CopyTo(Directory target, bool overwrite)
        {
            //if (IsDeleted) throw new System.InvalidOperationException("Cannot interact with deleted Dictionary");
            throw new System.NotImplementedException();
        }

        public Directory MoveTo(Directory target, bool overwrite)
        {
            throw new System.NotImplementedException();
        }

        public Directory RenameTo(string name, bool overwrite)
        {
            throw new System.NotImplementedException();
        }

        public Directory CreateChildDirectory(string name)
        {
            var dir = new Directory(name, this);
            Contents.Add(dir);
            return dir;
        }

        public File CreateChildFile(string name, string contents)
        {
            var file = new File(name, contents, this);
            Contents.Add(file);
            return file;
        }
    }
}
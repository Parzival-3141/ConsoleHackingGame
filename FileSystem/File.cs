namespace FileSystem
{
    public class File : INode<File>
    {
        public File(string name, string contents, Directory parent)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException("Must contain alphanumeric characters", nameof(name));

            if (parent == null)
                throw new System.ArgumentException("Cannot be null", nameof(parent));

            if (parent.Contents.Exists(dir => dir.Name == name))
                throw new System.ArgumentException($"Parent Directory already contains Node '{name}'");

            this.Name = name;
            this.ParentDirectory = parent;
            this.Contents = contents;
        }

        public string Name { get; private set; }
        public Directory ParentDirectory { get; private set; }
        public NodeType NodeType => NodeType.File;
        public bool IsDeleted { get; private set; } = false;

        public string Contents { get; }


        public void Delete()
        {
            if (IsDeleted) return;
            
            ParentDirectory.Contents.Remove(this);
            IsDeleted = true;
        }
        
        public File CopyTo(Directory target, bool overwrite)
        {
            throw new System.NotImplementedException();
        }

        public File MoveTo(Directory target, bool overwrite)
        {
            throw new System.NotImplementedException();
        }

        public File RenameTo(string name, bool overwrite)
        {
            throw new System.NotImplementedException();
        }
    }
}
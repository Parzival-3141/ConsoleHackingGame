//  Based off of the Platform.VirtualFileSystem
//  https://github.com/platformdotnet/Platform.VirtualFileSystem/blob/master/src/Platform.VirtualFileSystem/INode.cs

namespace FileSystem
{

    /// <summary>
    /// Represents the base type for files and directories
    /// </summary>
    public interface INodeBase 
    {
        string Name { get; }
        Directory ParentDirectory { get; }
        NodeType NodeType { get; }
        bool IsDeleted { get; }
        void Delete();
    }

    /// <inheritdoc/>
    public interface INode<T> /*where T : INode<T>*/ : INodeBase
    {
        T MoveTo(Directory target, bool overwrite);
        T CopyTo(Directory target, bool overwrite);
        T RenameTo(string name, bool overwrite);
    }
    
    public enum NodeType
    {
        File, Directory
    }
}

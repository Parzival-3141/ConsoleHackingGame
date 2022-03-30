namespace NewFileSystem
{
    public static class Utils
    {
        public static string GetPath(this INodeBase node)
        {
            string path = node.Name;
            var currentDir = node.ParentDirectory;
            
            while(currentDir != null)
            {
                path = path.Insert(0, currentDir.Name + (currentDir.IsRoot ? "" : "/"));
                currentDir = currentDir.ParentDirectory;
            }

            return path;
        }
    }
}

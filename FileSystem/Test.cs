using System;

namespace FileSystem
{
    public class Test
    {
        public static void Run()
        {
            var fs = new FileSystem();
            Console.WriteLine("ls /\n" + fs.Root.GetPath());
            
            Console.WriteLine();

            Console.WriteLine("Creating structure");
            var d1 = fs.Root.CreateChildDirectory("dir1");
            var f1 = fs.Root.CreateChildFile("file1", "yo momma");
            var f2 = d1.CreateChildFile("file2", "what the dog doin?");
            var f3 = d1.CreateChildFile("file3", "ligma");
            var d2 = d1.CreateChildDirectory("dir2");

            Console.WriteLine();

            Console.WriteLine("ls /");
            fs.Root.Contents.ForEach(n => Console.WriteLine($"<{n.NodeType}>  {n.Name}"));

            Console.WriteLine();

            Console.WriteLine($"cat {f2.GetPath()}\n{f2.Contents}");

            Console.WriteLine();

            Console.WriteLine("ls /dir1");
            d1.Contents.ForEach(n => Console.WriteLine($"<{n.NodeType}>  {n.Name}"));
            
            Console.WriteLine();
            
            Console.WriteLine("Deleting dir1");
            fs.DeleteNode(d1.GetPath());
            
            Console.WriteLine();

            Console.WriteLine("ls /");
            fs.Root.Contents.ForEach(n => Console.WriteLine($"<{n.NodeType}>  {n.Name}"));

            Console.ReadLine();
        }
    }
}

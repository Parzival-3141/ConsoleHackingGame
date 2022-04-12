using System;
using FileSystem;

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

        public static bool TrySearch(this FileSystem.FileSystem fs, string path, out INodeBase node)
        {
            try
            {
                node = fs.Search(path, Program.CurrentDirectory);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                node = null;
                return false;
            }
        }

        public static bool TrySearch<T>(this FileSystem.FileSystem fs, string path, out T result) where T : class, INodeBase
        {
            try
            {
                var node = fs.Search(path, Program.CurrentDirectory);
                if (!(node is T))
                    throw new ArgumentException($"Invalid path: cannot access {node.Name} as {typeof(T).Name}!");

                result =  node as T;
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                result = null;
                return false;
            }
        }
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

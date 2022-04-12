using CHG = ConsoleHackerGame;

namespace ConsoleHackerGame.Network
{
    public class Computer : NetworkedDevice
    {
        public Computer(string name, string ip, int totalRAM)
        {
            Name = name;
            IP = ip;
            TotalRAM = totalRAM;
            FileSystem = new FileSystem.FileSystem(FSInit);
        }

        private static void FSInit(FileSystem.FileSystem fileSystem)
        {
            //  Generate Hacknet file structure
            var sys = fileSystem.Root.CreateChildDirectory("sys");
            fileSystem.Root.CreateChildDirectory("home");
            fileSystem.Root.CreateChildDirectory("bin");
            fileSystem.Root.CreateChildDirectory("log");

            sys.CreateChildFile("os-config.sys", CHG.Utils.GenerateBinaryString(1000));
            sys.CreateChildFile("bootcfg.dll", CHG.Utils.GenerateBinaryString(1000));
            sys.CreateChildFile("netcfgx.dll", CHG.Utils.GenerateBinaryString(1000));

        }
    }
}

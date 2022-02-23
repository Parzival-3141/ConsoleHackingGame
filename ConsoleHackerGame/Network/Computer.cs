namespace ConsoleHackerGame.Network
{
    public class Computer : NetworkedDevice
    {
        public Computer(string name, string ip, int totalRAM)
        {
            Name = name;
            IP = ip;
            TotalRAM = totalRAM;
            FileSystem = new Files.FileSystem();
        }
    }
}

using System;

namespace ConsoleHackerGame.TextFiles
{
    public static class IRC
    {
        //  Public chats taken from the BONEWORKS discord server
        //  https://discord.gg/boneworks

        public const string elk1 = "<TheDarkElk#3500>: Sigma grindset: steal woman’s pizza, break her toilet, leave";
        public const string cam1 = "<Camobiwon#9295>: Under Articles 1 and 2 of the document Rules and Info, you are prohibited from sending homosexual porn in the general chat on the Unofficial BONEWORKS Discord Server";
        public const string cam2 =
            "Ethi#2413>: Hey Cam! I hope you're having a great day. Thanks for being a great friend!\n" +
            "<Camobiwon#9295>: fuck you shit bitch for thinking that dumbass thought idiot!!";
        public const string mara1 =
            "<Maranara#2877>: one more meme and GENERAL GO BYE BYE\n" +
            "<GunnZeug#1016>: e g";

        public const string chap1 = "<gnonme#2200>: dear brandon of jla, i am writing to humbly ask you how rotate player?? you see we try to rotate player, but it doen't owkrk so we need help. please replu hen you are free we will be very happy and give ford many rotations :catyes: he will enjoy it much. while youre at it might as well just send the entire bw sourec code i cant hurt to sen right i ormise i will not share with anyone pinky promise please send okthxbye";

        public static string[] IRCLogs = new string[] { elk1,cam1, cam2, mara1 };

        public static void GenerateIRCLog(string data, FileSystem.Directory parentDir)
        {
            var date = new DateTime
            (
                Utils.Random.Next(2020, 2022), // Y
                Utils.Random.Next(1, 12),      // M
                Utils.Random.Next(1, 30)       // D
            );

            parentDir.Contents.Add
            (
                new FileSystem.File
                (
                    $"IRC_log_{date.ToShortDateString()}"
                        .Replace(' ', '_')
                        .Replace('/','-'),
                    data, 
                    parentDir
                )
            );
        }

        public static void GenerateIRCLog(FileSystem.Directory parentDir)
        {
            GenerateIRCLog(IRCLogs[Utils.Random.Next(IRCLogs.Length)], parentDir);
        }
    }
}

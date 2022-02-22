﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHackerGame.Network 
{
    public abstract class NetworkedDevice
    {
        /*
         *  Contains:
         *      - FileSystem
         *          - Misc files & data
         *      - IP Address
         *      - Connected/Linked IPs
         *          - Phones, Computers, other Devices...
         *      - User Account
         *          - Admin user/password
         *          - Required for admin access
         *          - Whether the User is AFK
         *          - The User's computer literacy (grandma, average, skriptKiddie, hacker, MrRobot)
         *      - RAM
         *          - Limits how many processes can be run on the device
         *          - Realistically, devices should have at least a few GBs
         *          - For gameplay purposes, either upscale the RAM required by processes or downscale the amount on devices (or both!)
         *          
         *  Optional:
         *      - SSH Server
         *          - Allows player to ssh into the device if they have the credentials
         *          - Player can whitelist their IP
         *      
         * 
         */

        public string IP { get; protected set; }
        public List<string> LinkedIPs { get; protected set; }
        //public FileSystem FileSystem { get; protected set; }
        public int TotalRAM { get; protected set; }
        public int AvailableRAM { get => TotalRAM - UsedRAM; }

        protected int UsedRAM;
    }
}

using System;
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
         *      
         * 
         */

        public string IP { get; protected set; }
        public List<string> LinkedIPs { get; protected set; }
        //public FileSystem FileSystem { get; protected set; }

    }
}

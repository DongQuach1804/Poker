using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    internal class Player
    {
        public string name { get; set; }
        public int turn { get; set; }

        public bool chu = true;

        public int money = 1000;  
        public Socket plsocket { get; set; }
    }
}

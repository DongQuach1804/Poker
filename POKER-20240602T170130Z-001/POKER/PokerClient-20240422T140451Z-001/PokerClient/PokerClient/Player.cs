using System;
using System.Collections.Generic;

namespace PokerClient
{
    class ThisPlayer
    {
        public static string name { get; set; }
        public static int turn { get; set; }

        public static List<string> card = new List<string>();
    }
    class Other
    {
        public string name { get; set; }
        public string turn { get; set; }
    }
}

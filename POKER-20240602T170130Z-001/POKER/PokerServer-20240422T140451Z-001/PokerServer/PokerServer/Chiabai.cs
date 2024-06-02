using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerServer
{
    internal class Chiabai
    {
        public static string[] card_id =
            {
                "_10bich", "_10chuong", "_10co", "_10ro", "_1bich", "_1chuong", "_1co", "_1ro", "_2bich", "_2chuong", "_2co", "_2ro", 
                "_3bich", "_3chuong", "_3co", "_3ro", "_4bich", "_4chuong", "_4co", "_4ro", "_5bich", "_5chuong", "_5co", "_5ro",
                "_6bich", "_6chuong", "_6co", "_6ro", "_7bich", "_7chuong", "_7co", "_7ro", "_8bich", "_8chuong", "_8co", "_8ro", 
                "_9bich", "_9chuong", "_9co", "_9ro", "Jbich", "Jchuong", "Jco", "Jro", "Qbich", "Qchuong", "Qco", "Qro", "Kbich", 
                "Kchuong", "Kco", "Kro"
            };
        public enum CardID
        {
            _10bich = 101, _10chuong = 102, _10co = 104, _10ro = 103,
            _1bich = 141, _1chuong = 142, _1co = 144, _1ro = 143,
            _2bich = 21, _2chuong = 22, _2co = 24, _2ro = 23,
            _3bich = 31, _3chuong = 32, _3co = 34, _3ro = 33,
            _4bich = 41, _4chuong = 42, _4co = 44, _4ro = 43,
            _5bich = 51, _5chuong = 52, _5co = 54, _5ro = 53,
            _6bich = 61, _6chuong = 62, _6co = 64, _6ro = 63,
            _7bich = 71, _7chuong = 72, _7co = 74, _7ro = 73,
            _8bich = 81, _8chuong = 82, _8co = 84, _8ro = 83,
            _9bich = 91, _9chuong = 92, _9co = 94, _9ro = 93,
            Jbich = 111, Jchuong = 112, Jco = 114, Jro = 113,
            Qbich = 121, Qchuong = 122, Qco = 124, Qro = 123,
            Kbich = 131, Kchuong = 132, Kco = 134, Kro = 133
        }

        public static int CardValue(string id)
        {
            return (int)Enum.Parse(typeof(CardID), id);
        }
    }
}

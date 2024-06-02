using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PokerServer
{
    internal static class Program
    {
        private static Socket server;
        private static Socket client;
        private static Thread clientThread;
        private static int currentturn = 1;
        private static int pot = 0;
        private static int currentBet = 0;
        private static List<Player> clientlist = new List<Player>();
        private static bool defaultCalled = false;
        private static int count = 1;
        private static int countbobai = 0;
        private static List<Player> clientbobai = new List<Player>();
        private static List<string> nhom = new List<string>();
        private static int tien;
        private static int countcuochet = 0;
        private static string round;
        private static int countkiembai = 1;
        private static string hienbai;
        private static int countnangcuoc = 0;
        private static List<Tuple<string, string>> CardOfPlayer = new List<Tuple<string, string>>();
        private static string CardsCommon = "";
        static void Main(string[] e)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            server.Bind(serverEP);
            server.Listen(4);
            Console.WriteLine("[ Waiting for connection from players ... ]");
            while (true)
            {
                client = server.Accept();
                Console.WriteLine(">> Connection from " + client.RemoteEndPoint);
                clientThread = new Thread(() => ClientRecv(client));
                clientThread.Start();
            }
        }
        static void ClientRecv(Socket client)
        {
            Player p = new Player();
            p.plsocket = client;
            clientlist.Add(p);
            byte[] buffer = new byte[1024];
            while (p.plsocket.Connected)
            {
                if (p.plsocket.Available > 0)
                {
                    string msg = "";

                    while (p.plsocket.Available > 0)
                    {
                        int bRead = p.plsocket.Receive(buffer);
                        msg += Encoding.UTF8.GetString(buffer, 0, bRead);
                    }

                    Console.WriteLine(p.plsocket.RemoteEndPoint + ": " + msg);
                    AnalyzingMessage(msg, p);
                }
            }
        }
        static void AnalyzingMessage(string msg, Player p)
        {
            string[] arrPayload = msg.Split(';');
            switch (arrPayload[0])
            {
                case "CONNECT":
                    {
                        p.name = arrPayload[1];
                        foreach (var player in clientlist)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes("LOBBYINFO;" + player.name);
                            p.plsocket.Send(buffer);
                            Thread.Sleep(100);
                        }
                        foreach (var player in clientlist)
                        {
                            if (player.plsocket != p.plsocket)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes("LOBBYINFO;" + p.name);
                                player.plsocket.Send(buffer);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    break;
                case "DISCONNECTED":
                    {
                        foreach (var player in clientlist.ToList())
                        {
                            if (player.name == arrPayload[1])
                            {
                                player.plsocket.Shutdown(SocketShutdown.Both);
                                player.plsocket.Close();
                                clientlist.Remove(player);
                            }
                        }
                    }
                    break;
                case "START":
                    {
                        XaoBai();
                        foreach (var player in clientlist)
                        {
                            string temp = InitialCardsDeal();
                            CardOfPlayer.Add(Tuple.Create(player.name, temp));
                            string makemsg = "INIT;" + player.name + ";" + temp;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.plsocket.Send(buffer);
                            Console.WriteLine("Sendback: " + makemsg);
                            Thread.Sleep(100);
                        }
                        foreach (var player in clientlist)
                        {
                            foreach (var player_ in clientlist)
                            {
                                if (player.name != player_.name)
                                {
                                    string makemsg = "OTHERINFO;" + player_.name;
                                    byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                    player.plsocket.Send(buffer);
                                    Console.WriteLine("Sendback: " + makemsg);
                                    Thread.Sleep(100);
                                }
                            }
                        }
                        foreach (var player in clientlist)
                        {
                            string makemsg = "SETUP;" + player.name;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.plsocket.Send(buffer);
                            Console.WriteLine("Sendback: " + makemsg);
                            Thread.Sleep(100);
                        }

                        foreach (var player in clientlist)
                        {
                            if (player.name == clientlist[currentturn - 1].name)
                            {
                                string makemsg_ = "TURN;" + clientlist[currentturn - 1].name;
                                byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + makemsg_);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    currentturn++;
                    break;
                case "NANGCUOC":
                    {
                        int money = int.Parse(arrPayload[1]);
                        int cuoc = int.Parse(arrPayload[2]);
                        int tiencuocvonghientai = int.Parse(arrPayload[3]);
                        int demsolannangcuoc = int.Parse(arrPayload[4]);
                        if (demsolannangcuoc == 1)
                        {
                            if (cuoc < money && cuoc > currentBet)
                            {
                                tien = money - cuoc;
                                currentBet = currentBet + (cuoc - currentBet);
                                pot = pot + cuoc;
                                tiencuocvonghientai = cuoc;
                            }
                        }
                        else if (demsolannangcuoc > 1)
                        {
                            if (cuoc < money && cuoc > currentBet)
                            {
                                currentBet = currentBet + (cuoc - currentBet);
                                pot = pot + (currentBet - tiencuocvonghientai);
                                tien = money - (currentBet - tiencuocvonghientai);
                                tiencuocvonghientai = tiencuocvonghientai + (currentBet - tiencuocvonghientai);
                            }
                        }
                        foreach (var player in clientlist)
                        {
                            if (player.name == p.name)
                            {
                                string makesg = "NANGCUOCTHANHCONG;" + pot + ";" + currentBet + ";" + tien + ";" + tiencuocvonghientai;
                                byte[] buffer = Encoding.UTF8.GetBytes(makesg);
                                player.plsocket.Send(buffer);
                                Console.WriteLine("Sendback: " + makesg);
                                Thread.Sleep(100);
                            }
                            string makemsg_ = "UPDATE1;" + p.name + ";" + pot + ";" + currentBet;
                            byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                            player.plsocket.Send(buffer_);
                            Console.WriteLine("Sendback: " + makemsg_);
                            Thread.Sleep(100);
                        }
                        if (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn - 1].name))
                        {
                            ChooseNextPlayer();
                            while (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn].name))
                            {
                                ChooseNextPlayer();
                            }
                        }
                        SendNextPlayerMessage();
                        ChooseNextPlayer();
                        if (!defaultCalled)
                        {
                            CardsCommon = InitialCardsDeal2();
                            string message = "INITCHUNG;" + CardsCommon;
                            foreach (var player in clientlist)
                            {
                                byte[] buffer_ = Encoding.UTF8.GetBytes(message);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + message);
                                Thread.Sleep(100);
                            }
                            defaultCalled = true;
                        }
                    }
                    countnangcuoc++;
                    break;
                case "CUOCHET":
                    {
                        countcuochet++;
                        int money = int.Parse(arrPayload[1]);
                        if (money > currentBet)
                        {
                            currentBet = currentBet + (money - currentBet);
                            pot = pot + money;
                            money = 0;
                        }
                        else
                        {
                            pot = pot + money;
                            money = 0;
                        }
                        foreach (var player in clientlist)
                        {
                            if (player.name == p.name)
                            {
                                string makesg = "CUOCHET;" + pot + ";" + money + ";" + currentBet;
                                byte[] buffer = Encoding.UTF8.GetBytes(makesg);
                                player.plsocket.Send(buffer);
                                Console.WriteLine("Sendback: " + makesg);
                                Thread.Sleep(100);
                            }
                            string makemsg_ = "UPDATE2;" + p.name + ";" + pot + ";" + currentBet;
                            byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                            player.plsocket.Send(buffer_);
                            Console.WriteLine("Sendback: " + makemsg_);
                            Thread.Sleep(100);

                        }
                        if (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn - 1].name))
                        {
                            ChooseNextPlayer();
                            while (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn].name))
                            {
                                ChooseNextPlayer();
                            }
                        }
                        SendNextPlayerMessage();
                        ChooseNextPlayer();
                        if (!defaultCalled)
                        {
                            CardsCommon = InitialCardsDeal2();
                            string message = "INITCHUNG;" + CardsCommon;
                            foreach (var player in clientlist)
                            {
                                byte[] buffer_ = Encoding.UTF8.GetBytes(message);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + message);
                                Thread.Sleep(100);
                            }
                            defaultCalled = true;
                        }
                    }
                    break;
                case "BOBAI":
                    {
                        countbobai++;
                        clientbobai.Add(p);
                        foreach (var player in clientlist)
                        {
                            if (player.name == p.name)
                            {
                                string msg_ = "PLAYER_LEFT;" + player.name;
                                byte[] buffer_ = Encoding.UTF8.GetBytes(msg_);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + msg_);
                                Thread.Sleep(100);
                            }
                        }
                        foreach (var player1 in clientbobai)
                        {
                            string s = player1.name;
                            nhom.Add(s);
                        }
                        if (clientlist.Count == 2)
                        {
                            if (countbobai == 1)
                            {
                                foreach (var player in clientlist)
                                {
                                    if (player.name != p.name)
                                    {
                                        string msg_ = "PLAYER_LEFT1;" + player.name;
                                        byte[] buffer_ = Encoding.UTF8.GetBytes(msg_);
                                        player.plsocket.Send(buffer_);
                                        Console.WriteLine("Sendback: " + msg_);
                                        Thread.Sleep(100);
                                    }
                                }
                            }
                        }
                        else if (clientlist.Count == 3)
                        {
                            if (countbobai == 2)
                            {
                                foreach (var player in clientlist)
                                {
                                    bool isPlayerInGroup = false;
                                    for (int i = 0; i < nhom.Count; i++)
                                    {
                                        if (player.name == nhom[i])
                                        {
                                            isPlayerInGroup = true;
                                            break;
                                        }
                                    }
                                    if (!isPlayerInGroup)
                                    {
                                        string msg_ = "PLAYER_LEFT1;" + player.name;
                                        byte[] buffer_ = Encoding.UTF8.GetBytes(msg_);
                                        player.plsocket.Send(buffer_);
                                        Console.WriteLine("Sendback: " + msg_);
                                        Thread.Sleep(100);
                                    }
                                }
                            }
                        }
                        else if (clientlist.Count == 4)
                        {
                            if (countbobai == 3)
                            {
                                foreach (var player in clientlist)
                                {
                                    bool isPlayerInGroup = false;
                                    for (int i = 0; i < nhom.Count; i++)
                                    {
                                        if (player.name == nhom[i])
                                        {
                                            isPlayerInGroup = true;
                                            break;
                                        }
                                    }
                                    if (!isPlayerInGroup)
                                    {
                                        string msg_ = "PLAYER_LEFT1;" + player.name;
                                        byte[] buffer_ = Encoding.UTF8.GetBytes(msg_);
                                        player.plsocket.Send(buffer_);
                                        Console.WriteLine("Sendback: " + msg_);
                                        Thread.Sleep(100);
                                    }
                                }
                            }
                        }
                        if (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn - 1].name))
                        {
                            ChooseNextPlayer();
                            while (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn].name))
                            {
                                ChooseNextPlayer();
                            }
                        }
                        SendNextPlayerMessage();
                        ChooseNextPlayer();
                        if (!defaultCalled)
                        {
                            CardsCommon = InitialCardsDeal2();
                            string message = "INITCHUNG;" + CardsCommon;
                            foreach (var player in clientlist)
                            {
                                byte[] buffer_ = Encoding.UTF8.GetBytes(message);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + message);
                                Thread.Sleep(100);
                            }
                            defaultCalled = true;
                        }
                    }
                    break;
                case "KIEMBAI":
                    {
                        if (clientlist.Count == 2 && countkiembai == 2 && countnangcuoc == 0 ||
                            clientlist.Count == 3 && countkiembai == 3 && countnangcuoc == 0 ||
                            clientlist.Count == 3 && countkiembai == 2 && countnangcuoc == 0 && countbobai == 1 ||
                            clientlist.Count == 4 && countkiembai == 4 && countnangcuoc == 0 ||
                            clientlist.Count == 4 && countkiembai == 3 && countnangcuoc == 0 && countbobai == 1 ||
                            clientlist.Count == 4 && countkiembai == 2 && countnangcuoc == 0 && countbobai == 2)
                        {
                            if (round == "2")
                            {
                                hienbai = "initcard3";
                            }
                            else if (round == "3")
                            {
                                hienbai = "initcard4";
                            }
                            else
                            {
                                hienbai = "initcard2";
                            }
                            foreach (var player in clientlist)
                            {
                                string makemsg_ = "UPDATE3;" + hienbai;
                                byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + makemsg_);
                            }
                        }
                        if (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn - 1].name))
                        {
                            ChooseNextPlayer();
                            while (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn].name))
                            {
                                ChooseNextPlayer();
                            }
                        }
                        SendNextPlayerMessage();
                        ChooseNextPlayer();
                        if (!defaultCalled)
                        {
                            CardsCommon = InitialCardsDeal2();
                            string message = "INITCHUNG;" + CardsCommon;
                            foreach (var player in clientlist)
                            {
                                byte[] buffer_ = Encoding.UTF8.GetBytes(message);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + message);
                                Thread.Sleep(100);
                            }
                            defaultCalled = true;
                        }
                    }
                    countkiembai++;
                    break;
                case "THEO":
                    {
                        int money = int.Parse(arrPayload[1]);
                        int cuoc = currentBet;
                        int tiencuocvonghientai = int.Parse(arrPayload[2]);
                        if (cuoc < money && cuoc == currentBet)
                        {
                            tien = money - (currentBet - tiencuocvonghientai);
                            pot = pot + (currentBet - tiencuocvonghientai);
                            tiencuocvonghientai = tiencuocvonghientai + (currentBet - tiencuocvonghientai);
                        }
                        if (countcuochet == 1)
                        {
                            tien = 0;
                            pot = pot + money;
                        }
                        foreach (var player in clientlist)
                        {
                            if (player.name == p.name)
                            {
                                string makesg = "THEOBAI;" + pot + ";" + tien + ";" + count + ";" + clientlist.Count + ";" + countcuochet + ";" + round + ";" + countbobai;
                                byte[] buffer = Encoding.UTF8.GetBytes(makesg);
                                player.plsocket.Send(buffer);
                                Console.WriteLine("Sendback: " + makesg);
                                Thread.Sleep(100);
                            }
                            else
                            {
                                string makemsg_ = "UPDATE4;" + p.name + ";" + pot + ";" + count + ";" + clientlist.Count + ";" + countcuochet + ";" + round + ";" + countbobai;
                                byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + makemsg_);
                                Thread.Sleep(100);
                            }
                        }
                        if (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn - 1].name))
                        {
                            ChooseNextPlayer();
                            while (clientbobai.Any(bobaiPlayer => bobaiPlayer.name == clientlist[currentturn].name))
                            {
                                ChooseNextPlayer();
                            }
                        }
                        SendNextPlayerMessage();
                        ChooseNextPlayer();
                        if (!defaultCalled)
                        {
                            CardsCommon = InitialCardsDeal2();
                            string message = "INITCHUNG;" + CardsCommon;
                            foreach (var player in clientlist)
                            {
                                byte[] buffer_ = Encoding.UTF8.GetBytes(message);
                                player.plsocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + message);
                                Thread.Sleep(100);
                            }
                            defaultCalled = true;
                        }
                    }
                    count++;
                    break;
                case "RESET":
                    {
                        count = 1;
                        round = arrPayload[1];
                        currentBet = 0;
                        countkiembai = 1;
                        countnangcuoc = 0;
                        countcuochet = 0;
                    }
                    break;
                case "MESSAGE":
                    {
                        string message = "MESSAGE;" + p.name + ";" + arrPayload[1];
                        foreach (var player in clientlist)
                        {
                            byte[] buffer_ = Encoding.UTF8.GetBytes(message);
                            player.plsocket.Send(buffer_);
                            Console.WriteLine("Sendback: " + message);
                            Thread.Sleep(100);
                        }
                    }
                    break;
                case "RESULT":
                    {
                        //Lấy dữ liệu từ biến toàn cục là 5 lá bài chung
                        string[] Card = CardsCommon.Split(';');
                        List<int> player = new List<int>();
                        int[] tempP = new int[16];
                        int v = 0;

                        //Lấy 5 lá bài chung lưu vào list player
                        for (int i = 0; i < 5; i++)
                        {
                            player.Add(Chiabai.CardValue(Card[i]));
                        }

                        List<int> result = new List<int>();

                        //Lưu tiếp hai lá bài của người chơi vào và tiens hành đánh giá bài và thêm vào list result
                        foreach (var c in CardOfPlayer)
                        {
                            string[] temp = c.Item2.Split(new char[] { ';' });
                            for (int i = 0; i < 2; i++)
                            {
                                player.Add(Chiabai.CardValue(temp[i]));
                            }
                            result.Add(Result(player));
                            tempP[v] = player[player.Count - 1];
                            tempP[v + 1] = player[player.Count - 2];
                            v += 2;
                            player.RemoveRange(player.Count - 2, 2);
                        }

                        int min = result.Min();
                        int index = result.IndexOf(min);

                        //Tìm vị trí giá trị nhỏ nhất trong list result và lưu vào biến index
                        for (int i = 0; i < result.Count - 1; i++)
                        {
                            for (int j = i + 1; j < result.Count; j++)
                            {
                                if (min == result[j])
                                {
                                    for (int k = 0; k < tempP.Length; k++)
                                    {
                                        if (tempP.Max() == tempP[k])
                                            index = k / 2;
                                    }
                                }
                            }
                        }

                        //Tạo List lấy tên của người chơi rồi thêm và List name theo thứ tự. Thứ tự này cũng là thứ tự thêm vào List result
                        List<string> name = new List<string>();

                        foreach (var n in CardOfPlayer)
                        {
                            name.Add(n.Item1);
                        }

                        //Trả về kết quả là tên người chiến thắng ở vị trí index
                        string winner = name[index];

                        foreach (var players in clientlist)
                        {
                            string message = "RESULT;" + winner;
                            byte[] buffer_ = Encoding.UTF8.GetBytes(message);
                            players.plsocket.Send(buffer_);
                            Console.WriteLine("Sendback: " + message);
                            Thread.Sleep(100);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public static void XaoBai()
        {
            Random rand = new Random();
            Chiabai.card_id = Chiabai.card_id.OrderBy(x => rand.Next()).ToArray();
        }
        public static string InitialCardsDeal()
        {
            Random rand = new Random();
            string twocards = "";
            for (int i = 0; i < 2; i++)
            {
                int pick = rand.Next(Chiabai.card_id.Length);
                twocards += Chiabai.card_id[pick] + ";";
                Chiabai.card_id = Chiabai.card_id.Where(val => val != Chiabai.card_id[pick]).ToArray();
            }
            return twocards;
        }
        public static string InitialCardsDeal2()
        {
            Random rand = new Random();
            string fivecards = "";
            for (int i = 0; i < 5; i++)
            {
                int pick = rand.Next(Chiabai.card_id.Length);
                fivecards += Chiabai.card_id[pick] + ";";
                Chiabai.card_id = Chiabai.card_id.Where(val => val != Chiabai.card_id[pick]).ToArray();
            }
            return fivecards;
        }
        public static void ChooseNextPlayer()
        {
            currentturn++;
            if (currentturn > clientlist.Count)
            {
                currentturn = 1;
            }
        }
        public static void SendNextPlayerMessage()
        {
            try
            {
                foreach (var player in clientlist)
                {
                    if (player.name == clientlist[currentturn - 1].name)
                    {
                        string makemsg = "TURN;" + clientlist[currentturn - 1].name;
                        byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                        player.plsocket.Send(buffer);
                        Console.WriteLine("Sendback: " + makemsg);
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception) { }
        }
        public static int Result(List<int> p)
        {
            int RoyalFlush = 0, StraightFlush = 0, Quad = 0, FullHouse = 0, Flush = 0, Straight = 0, Three = 0, Pair = 0;

            //Tạo danh sách list tạm lấy dữ liệu của p và sắp xếp tăng dần
            List<int> list = p;
            list.Sort();

            for (int i = 0; i < list.Count - 1; i++)
            {
                //Khai báo các biến để xác định kiểu bài
                int cnt = 0;
                int sub = 1;
                int same = 0;
                int royal = 0;
                int tmp = 10;

                //Vòng lặp thao tác trên 7 lá bài
                for (int j = i + 1; j < list.Count; j++)
                {
                    //So sánh lá bài có giống nhau không, không quan tâm đến chất
                    if ((list[i] / 10) == (list[j] / 10))
                        cnt++;

                    //Xem bài có sảnh không
                    if (Math.Abs((list[i] / 10 - list[j] / 10)) == sub)
                        sub++;

                    //Xem bài có đồng chất hay không
                    if (list[j] % 10 == list[i] % 10)
                        same++;

                    //Xem bài có phải là thùng phá sảnh không
                    if (list[i] >= 101 && (list[i] + tmp) == list[j])
                    {
                        royal++;
                        tmp += 10;
                    }
                }

                //Có 1 || 2 đôi
                if (cnt == 1 && Pair < 2)
                    Pair++;

                //Có 3 lá giống nhau
                else if (cnt == 2)
                    Three = 1;

                //Có tứ quý
                else if (cnt == 3)
                    Quad = 1;

                //Có sảnh bình thường
                if (sub > 4) Straight = 1;

                //Đồng chất
                if (same == 5) Flush = 1;

                //Cù lủ: Có 1 đôi và 3 lá giống
                if (Pair == 2 && Three == 1) FullHouse = 1;

                //Sảnh thùng: Vừa đồng chất vừa sảnh
                if (Straight == 1 && Flush == 1) StraightFlush = 1;

                //Bài cao nhất: 10 J Q K A và đồng chất
                if (royal == 5) RoyalFlush = 1;
            }

            //Trả về kết quả ưu tiên theo bài lớn nhất
            if (RoyalFlush == 1)
                return 1;
            else if (StraightFlush == 1)
                return 2;
            else if (Quad == 1)
                return 3;
            else if (FullHouse == 1)
                return 4;
            else if (Flush == 1)
                return 5;
            else if (Straight == 1)
                return 6;
            else if (Three == 1)
                return 7;
            else if (Pair == 2)
                return 8;
            else if (Pair == 1)
                return 9;
            else return 10;
        }
    }

}

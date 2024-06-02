using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace PokerClient
{
    internal class Clientsk
    {
        public static Socket clientsocket;
        public static Thread thread;
        public static string datatype = "";
        private static int count = 0;
        private static int count1 = 0;
        private static string round;

        //Hàm kết nối
        public static void Connect(IPEndPoint ip)
        {
            clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientsocket.Connect(ip);
            thread = new Thread(() => RecieveData());
            thread.IsBackground = true;
            thread.Start();
        }

        //Hàm gửi thông điệp từ Client đến Server
        public static void Send (string s)
        {
            string message = datatype + ";" + s;
            byte[] msgtr = Encoding.UTF8.GetBytes(message);
            clientsocket.Send(msgtr);
        }
        public static void RecieveData()
        {
            byte[] buffer = new byte[1024];
            while (clientsocket.Connected)
            {
                if (clientsocket.Available > 0)
                {
                    string msg = "";

                    while (clientsocket.Available > 0)
                    {
                        int bRead = clientsocket.Receive(buffer);
                        msg += Encoding.UTF8.GetString(buffer, 0, bRead);
                    }
                    AnalyzingReturnMessage(msg);
                }
            }
        }
        public static GameTable gametable;
        public static List<Other> otherplayers;
        public static void AnalyzingReturnMessage(string msg)
        {
            string[] arrPayload = msg.Split(';');
            int pot;
            int currentbet;
            int money;
            switch (arrPayload[0])
            {
                case "LOBBYINFO":
                    {
                        Menu.wt.DisplayConnectedPlayer(arrPayload[1]);
                    }
                    break;
                case "INIT":
                    {
                        for (int i = 2; i <4; i++)
                        {
                            ThisPlayer.card.Add(arrPayload[i]);
                        }
                        gametable = new GameTable();
                        otherplayers = new List<Other>();
                        Menu.wt.Invoke((MethodInvoker)delegate ()
                        {
                            //Gọi hàm hiển thị bài của bản thân người chơi
                            gametable.InitCardFetch();
                            gametable.Show();
                        }
                        );
                    }
                    break;
                case "OTHERINFO":
                    {
                        Other otherplayer = new Other();
                        otherplayer.name = arrPayload[1];
                        otherplayers.Add(otherplayer);
                    }
                    break;
                case "SETUP":
                    {
                        //Gọi hàm hiển thị giao diện bàn chơi
                        gametable.InitDisplay();
                    }
                    break;
                case "TURN":
                    {   
                        //Gọi hàm kiểm tra nếu tên người chơi giống với lượt chơi được Server gửi về thì Enable bàn chơi
                        if (gametable.label4name(arrPayload[1]) == true)
                        {
                            gametable.Enable();
                        }
                    }
                    break;
                case "UPDATE1":
                    {
                        pot = int.Parse(arrPayload[2]);
                        currentbet = int.Parse(arrPayload[3]);
                        gametable.UPDATE1(pot,currentbet);
                    }
                    break;
                case "UPDATE2":
                    {
                        pot = int.Parse(arrPayload[2]);
                        currentbet = int.Parse(arrPayload[3]);
                        gametable.UPDATE2(pot, currentbet);
                    }
                    break;
                case "UPDATE3":
                    {
                        Menu.wt.Invoke((MethodInvoker)delegate ()
                        {
                            if (arrPayload[1] == "initcard2")
                            {
                                gametable.InitCardR2();
                                round = "2";
                                gametable.NextRound(round);
                                gametable.RESETCOUNTNUTNANGCUOC();
                            }
                            else if (arrPayload[1] == "initcard3")
                            {
                                gametable.InitCardR3();
                                round = "3";
                                gametable.NextRound(round);
                                gametable.RESETCOUNTNUTNANGCUOC();
                            }
                            else if (arrPayload[1] == "initcard4")
                            {
                                gametable.InitCardR4();
                                gametable.Ketqua();
                            }
                            gametable.Update();
                        }
                        );
                    }
                    break;
                case "UPDATE4":
                    {
                        pot = int.Parse(arrPayload[2]);
                        gametable.UPDATE4(pot);
                        Menu.wt.Invoke((MethodInvoker)delegate ()
                        {
                            //arrPayload[4] là số lượng người chơi, arrPayload[5] số người cược hết, arrPayload[3] là số lượng người theo bài, arrPayload[6] là vòng chơi, arrPayload[7] là số người bỏ bài
                            if (int.Parse(arrPayload[4]) == 2)
                            {
                                if (int.Parse(arrPayload[5]) == 0)
                                {
                                    if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "")
                                    {
                                        gametable.InitCardR2();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && int.Parse(arrPayload[6]) == 2)
                                    {
                                        gametable.InitCardR3();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && int.Parse(arrPayload[6]) == 3)
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                }
                                else if (int.Parse(arrPayload[5]) == 1)
                                {
                                    if (count1 == 1)
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (count1 == 2)
                                    {
                                        gametable.InitCardR4();
                                    }
                                    else
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                }

                            }
                            else if (int.Parse(arrPayload[4]) == 3)
                            {
                                if (int.Parse(arrPayload[5]) == 0)
                                {
                                    if (int.Parse(arrPayload[3]) == 2 && arrPayload[6] == "")
                                    {
                                        gametable.InitCardR2();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR2();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && int.Parse(arrPayload[6]) == 2)
                                    {
                                        gametable.InitCardR3();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "2" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && int.Parse(arrPayload[6]) == 3)
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "3" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                }
                                else if (int.Parse(arrPayload[5]) == 1)
                                {
                                    if (int.Parse(arrPayload[3]) == 2 && count1 == 1)
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "1" && count1 == 1 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "2" && count1 == 2)
                                    {
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "1" && count1 == 2 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                    }
                                    else if (count1 == 0 && arrPayload[3] == "2")
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (count1 == 0 && arrPayload[3] == "1" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                }
                            }
                            else if (int.Parse(arrPayload[4]) == 4)
                            {
                                if (int.Parse(arrPayload[5]) == 0)
                                {
                                    if (int.Parse(arrPayload[3]) == 3 && arrPayload[6] == "")
                                    {
                                        gametable.InitCardR2();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && arrPayload[6] == "" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR2();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "" && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR2();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 3 && int.Parse(arrPayload[6]) == 2)
                                    {
                                        gametable.InitCardR3();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && int.Parse(arrPayload[6]) == 2 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && int.Parse(arrPayload[6]) == 2 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR3();
                                        count1++;
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 3 && int.Parse(arrPayload[6]) == 3)
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && int.Parse(arrPayload[6]) == 3 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && int.Parse(arrPayload[6]) == 3 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                }
                                else if (arrPayload[5] == "1")
                                {
                                    if (int.Parse(arrPayload[3]) == 3 && count1 == 1)
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "2" && count1 == 1 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "1" && count1 == 1 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "3" && count1 == 2)
                                    {
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "2" && count1 == 2 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "1" && count1 == 2 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "3" && count1 == 0)
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "2" && count1 == 0 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                    else if (arrPayload[3] == "1" && count1 == 0 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                    }
                                }
                            }
                        });
                    }
                    break;
                case "PLAYER_LEFT":
                    {
                        gametable.PLAYERLEFT();
                    }
                    break;
                case "INITCHUNG":
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            ThisPlayer.card.Add(arrPayload[i]);
                        }
                    }
                    break;
                case "NANGCUOCTHANHCONG":
                    {
                        pot = int.Parse(arrPayload[1]);
                        int currentBet = int.Parse(arrPayload[2]);
                        money = int.Parse(arrPayload[3]);
                        int tiencuocvonghientai = int.Parse(arrPayload[4]);
                        gametable.NANGCUOCTHANHCONG(pot,money,currentBet, tiencuocvonghientai);
                    }
                    break;
                case "PLAYER_LEFT1":
                    {
                        MessageBox.Show("Người chơi " + arrPayload[1] + " là người chiến thắng", "Result", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    break;
                case "CUOCHET":
                    {
                        pot = int.Parse(arrPayload[1]);
                        money = int.Parse(arrPayload[2]);
                        int currentBet = int.Parse(arrPayload[3]);
                        gametable.CUOCHET(pot, money, currentBet);
                    }
                    break;
                case "THEOBAI":
                    {
                        pot = int.Parse(arrPayload[1]);
                        money = int.Parse(arrPayload[2]);
                        gametable.THEOBAI(pot, money);
                        Menu.wt.Invoke((MethodInvoker)delegate ()
                        {
                            //arrPayload[4] là số lượng người chơi, arrPayload[5] số người cược hết, arrPayload[3] là số lượng người theo bài, arrPayload[6] là vòng chơi, arrPayload[7] là số người bỏ bài
                            if (int.Parse(arrPayload[4]) == 2)
                            {
                                if (int.Parse(arrPayload[5]) == 0)
                                {
                                    if (int.Parse(arrPayload[3]) == 1 && arrPayload[6]=="")
                                    {
                                        gametable.InitCardR2();
                                        count++;
                                        round = "2";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && int.Parse(arrPayload[6]) == 2)
                                    {
                                        gametable.InitCardR3();
                                        count++;
                                        round = "3";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && int.Parse(arrPayload[6]) == 3)
                                    {
                                        gametable.InitCardR4();
                                        round = "4";
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                        gametable.Ketqua();
                                    }
                                }
                                else if (int.Parse(arrPayload[5])==1)
                                {
                                    if (count == 1)
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (count == 2)
                                    {
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                }
                            }
                            else if (int.Parse(arrPayload[4]) == 3)
                            {
                                if (int.Parse(arrPayload[5]) == 0)
                                {
                                    if (int.Parse(arrPayload[3]) == 2 && arrPayload[6]=="")
                                    {
                                        gametable.InitCardR2();
                                        count++;
                                        round = "2";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR2();
                                        count++;
                                        round = "2";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && int.Parse(arrPayload[6]) == 2)
                                    {
                                        gametable.InitCardR3();
                                        count++;
                                        round = "3";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "2" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        count++;
                                        round = "3";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && int.Parse(arrPayload[6]) == 3)
                                    {
                                        gametable.InitCardR4();
                                        round = "4";
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                        gametable.Ketqua();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "3" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                        gametable.Ketqua();
                                    }
                                }
                                else if (int.Parse(arrPayload[5]) == 1)
                                {
                                    if (count == 1 && int.Parse(arrPayload[3]) == 2)
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (count == 1 && arrPayload[3] == "1" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (count == 2 && int.Parse(arrPayload[3])==2)
                                    {
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (count == 2 && arrPayload[3] == "1" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (count == 0 && int.Parse(arrPayload[3])==2)
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (count == 0 && arrPayload[3] == "1" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                }
                            }
                            else if (int.Parse(arrPayload[4]) == 4)
                            {
                                if (arrPayload[5] == "0")
                                {
                                    if (int.Parse(arrPayload[3]) == 3 && arrPayload[6] == "")
                                    {
                                        gametable.InitCardR2();
                                        count++;
                                        round = "2";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && arrPayload[6] == "" && arrPayload[7]=="1")
                                    {
                                        gametable.InitCardR2();
                                        count++;
                                        round = "2";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "" && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR2();
                                        count++;
                                        round = "2";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 3 && int.Parse(arrPayload[6]) == 2)
                                    {
                                        gametable.InitCardR3();
                                        count++;
                                        round = "3";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && arrPayload[6] == "2" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        count++;
                                        round = "3";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "2" && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR3();
                                        count++;
                                        round = "3";
                                        gametable.NextRound(round);
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 3 && int.Parse(arrPayload[6]) == 3)
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                        gametable.Ketqua();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 1 && arrPayload[6] == "3" && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                        gametable.Ketqua();
                                    }
                                    else if (int.Parse(arrPayload[3]) == 2 && arrPayload[6] == "3" && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                        gametable.RESETCOUNTNUTNANGCUOC();
                                        gametable.Ketqua();
                                    }
                                }
                                else if (arrPayload[5] == "1")
                                {
                                    if (arrPayload[3] == "3" && count == 1)
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "2" && count == 1 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "1" && count == 1 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "3" && count == 2)
                                    {
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "2" && count == 2 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "1" && count == 2 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "3" && count == 0)
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "2" && count == 0 && arrPayload[7] == "1")
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                    else if (arrPayload[3] == "1" && count == 0 && arrPayload[7] == "2")
                                    {
                                        gametable.InitCardR2();
                                        gametable.InitCardR3();
                                        gametable.InitCardR4();
                                        gametable.Ketqua();
                                    }
                                }
                            }
                            gametable.Update();
                        }
                        );

                    }
                    break;
                case "MESSAGE":
                    {
                        string message = arrPayload[1] + ": " + arrPayload[2];
                        gametable.Addonlistview(message);
                    }
                    break;
                case "RESULT":
                    {
                        string name = arrPayload[1];
                        MessageBox.Show("Người chơi " + name + " là người chiến thắng");
                    }
                    break;
                default:
                    break;
            }
        }
        private static void gametable_FormClosed(object sender, EventArgs e)
        {
            Menu.wt.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PokerClient
{
    public partial class GameTable : Form
    {
        public string facecard = "";
        public List<List<CardButton>> Cardbt;
        public List<Label> lbname;
        public int round;
        public int row = 0;
        public static int count = 0;
        public static bool check = false;
        public GameTable()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            btnnangcuoc.Enabled = false;
            btncuochet.Enabled = false;
            btnbobai.Enabled = false;
            btnkiembai.Enabled = false;
            btntheo.Enabled = false;
            Cardbt = new List<List<CardButton>>();
            lbname = new List<Label>();
        }
        public class CardButton
        {
            public int x { get; set; }
            public int y { get; set; }
            public string id { get; set; }
            public Button btn = new Button();
        }
        public void Enable()
        {
            btnnangcuoc.Enabled = true;
            btnbobai.Enabled = true;
            btnkiembai.Enabled = true;
            btntheo.Enabled= true;
            btncuochet.Enabled= true;    
        }
        public void Disable()
        {
            btnnangcuoc.Enabled = false;
            btnbobai.Enabled = false;
            btnkiembai.Enabled = false;
            btntheo.Enabled= false;
            btncuochet.Enabled= false;
        }
        
        //Hiện bài của bản thân người chơi
        public void InitCardFetch()
        {
            int i = 0;
            string a = "";
            for (int j = 0; i < 2; j++)
            {
                a = ThisPlayer.card[j];
                switch (a)
                {
                    case "_1co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._1co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._1co;
                        }
                        break;
                    case "_1ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._1ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._1ro;
                        }
                        break;
                    case "_1bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._1bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._1bich;
                        }
                        break;
                    case "_1chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._1chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._1chuong;
                        }
                        break;

                    case "_2co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._2co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._2co;
                        }
                        break;
                    case "_2ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._2ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._2ro;
                        }
                        break;
                    case "_2bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._2bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._2bich;
                        }
                        break;
                    case "_2chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._2chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._2chuong;
                        }
                        break;

                    case "_3co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._3co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._3co;
                        }
                        break;
                    case "_3ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._3ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._3ro;
                        }
                        break;
                    case "_3bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._3bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._3bich;
                        }
                        break;
                    case "_3chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._3chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._3chuong;
                        }
                        break;

                    case "_4co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._4co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._4co;
                        }
                        break;
                    case "_4ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._4ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._4ro;
                        }
                        break;
                    case "_4bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._4bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._4bich;
                        }
                        break;
                    case "_4chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._4chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._4chuong;
                        }
                        break;

                    case "_5co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._5co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._5co;
                        }
                        break;
                    case "_5ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._5ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._5ro;
                        }
                        break;
                    case "_5bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._5bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._5bich;
                        }
                        break;
                    case "_5chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._5chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._5chuong;
                        }
                        break;

                    case "_6co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._6co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._6co;
                        }
                        break;
                    case "_6ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._6ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._6ro;
                        }
                        break;
                    case "_6bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._6bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._6bich;
                        }
                        break;
                    case "_6chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._6chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._6chuong;
                        }
                        break;

                    case "_7co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._7co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._7co;
                        }
                        break;
                    case "_7ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._7ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._7ro;
                        }
                        break;
                    case "_7bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._7bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._7bich;
                        }
                        break;
                    case "_7chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._7chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._7chuong;
                        }
                        break;

                    case "_8co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._8co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._8co;
                        }
                        break;
                    case "_8ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._8ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._8ro;
                        }
                        break;
                    case "_8bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._8bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._8bich;
                        }
                        break;
                    case "_8chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._8chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._8chuong;
                        }
                        break;

                    case "_9co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._9co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._9co;
                        }
                        break;
                    case "_9ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._9ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._9ro;
                        }
                        break;
                    case "_9bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._9bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._9bich;
                        }
                        break;
                    case "_9chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._9chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._9chuong;
                        }
                        break;

                    case "_10co":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._10co;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._10co;
                        }
                        break;
                    case "_10ro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._10ro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._10ro;
                        }
                        break;
                    case "_10bich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._10bich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._10bich;
                        }
                        break;
                    case "_10chuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources._10chuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources._10chuong;
                        }
                        break;

                    case "Jco":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Jco;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Jco;
                        }
                        break;
                    case "Jro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Jro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Jro;
                        }
                        break;
                    case "Jbich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Jbich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Jbich;
                        }
                        break;
                    case "Jchuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Jchuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Jchuong;
                        }
                        break;

                    case "Qco":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Qco;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Qco;
                        }
                        break;
                    case "Qro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Qro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Qro;
                        }
                        break;
                    case "Qbich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Qbich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Qbich;
                        }
                        break;
                    case "Qchuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Qchuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Qchuong;
                        }
                        break;

                    case "Kco":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Kco;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Kco;
                        }
                        break;
                    case "Kro":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Kro;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Kro;
                        }
                        break;
                    case "Kbich":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Kbich;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Kbich;
                        }
                        break;
                    case "Kchuong":
                        if (i == 0)
                        {
                            button5.BackgroundImage = Properties.Resources.Kchuong;
                        }
                        else if (i == 1)
                        {
                            button6.BackgroundImage = Properties.Resources.Kchuong;
                        }
                        break;
                }
                i++;
            }
        }

        //Hiển thị giao diện bàn chơi
        public void InitDisplay()
        {
            label4.Text = ThisPlayer.name;
            lbname.Add(label4);
            switch (Clientsk.otherplayers.Count)
            {
                case 1:
                    {
                        panelPlayer1.Visible = false;
                        panelPlayer3.Visible = false;
                        label3.Text = Clientsk.otherplayers[0].name;
                        lbname.Add(label3);
                    }
                    break;
                case 2:
                    {
                        panelPlayer3.Visible = false;
                        if (ThisPlayer.turn == 2)
                        {
                            label2.Text = Clientsk.otherplayers[1].name;
                            label3.Text = Clientsk.otherplayers[0].name;
                        }
                        else
                        {
                            label2.Text = Clientsk.otherplayers[0].name;
                            label3.Text = Clientsk.otherplayers[1].name;
                        }
                        lbname.Add(label2);
                        lbname.Add(label3);
                    }
                    break;
                case 3:
                    {
                        if (ThisPlayer.turn == 1 && ThisPlayer.turn == 4)
                        {
                            label1.Text = Clientsk.otherplayers[0].name;
                            label2.Text = Clientsk.otherplayers[1].name;
                            label3.Text = Clientsk.otherplayers[2].name;

                        }
                        else if (ThisPlayer.turn == 2)
                        {
                            label1.Text = Clientsk.otherplayers[1].name;
                            label2.Text = Clientsk.otherplayers[2].name;
                            label3.Text = Clientsk.otherplayers[0].name;
                        }
                        else
                        {
                            label1.Text = Clientsk.otherplayers[2].name;
                            label2.Text = Clientsk.otherplayers[0].name;
                            label3.Text = Clientsk.otherplayers[1].name;
                        }
                        lbname.Add(label1);
                        lbname.Add(label2);
                        lbname.Add(label3);
                    }
                    break;
            }
        }
        public string tempturnname = "";
        public void HighlightTurn(string name)
        {
            tempturnname = name;
            foreach (var n in lbname)
            {
                if (n.Text == name)
                {
                    n.Font = new Font(n.Font, FontStyle.Bold);
                    n.ForeColor = Color.Red;
                    break;
                }
            }
        }

        public void UndoHighlightTurn()
        {
            foreach (var n in lbname)
            {
                if (n.Text == tempturnname)
                {
                    n.Font = new Font(n.Font, FontStyle.Regular);
                    n.ForeColor = Color.Black;
                    break;
                }
            }
        }
        public void CardsIdle()
        {
            foreach (var row in Cardbt)
            {
                foreach (var cdbtn in row)
                {
                    cdbtn.btn.FlatAppearance.BorderColor = Color.Black;
                    cdbtn.btn.Enabled = false;
                }
            }
        }

        //Cập nhật lại các giá trị khi nhận được thông tin từ Server gửi về Client 
        public void NANGCUOCTHANHCONG(int pot, int money, int currentBet, int tiencuocvonghientai)
        {
            txtpot.Text = pot.ToString();
            txtmoney.Text = money.ToString();
            txtcurrentbet.Text = currentBet.ToString();
            txttiencuocvonghientai.Text = tiencuocvonghientai.ToString();
        }
        public void CUOCHET(int pot, int money, int currentBet)
        {
            txtpot.Text = pot.ToString();
            txtmoney.Text = money.ToString();
            txtcurrentbet.Text = currentBet.ToString() ;
        }
        public void THEOBAI(int pot, int money)
        {
            txtpot.Text = pot.ToString();
            txtmoney.Text = money.ToString();
        }
        public void UPDATE1(int pot,  int currentbet)
        {
            txtpot.Text = pot.ToString();
            txtcurrentbet.Text = currentbet.ToString();
        }
        public void UPDATE2(int pot, int currentbet)
        {
            txtpot.Text = pot.ToString();
            txtcurrentbet.Text = currentbet.ToString();
        }
        public void UPDATE3(int pot, int money, int currentbet)
        {
            txtpot.Text = pot.ToString();
            txtmoney.Text = money.ToString();
            txtcurrentbet.Text = currentbet.ToString();
        }
        public void UPDATE4(int pot)
        {
            txtpot.Text = pot.ToString();
        }
        public void PLAYERLEFT()
        {
            btnnangcuoc.Enabled = false;
            btncuochet.Enabled = false;
            btnkiembai.Enabled = false;
            btntheo.Enabled = false;
            btnbobai.Enabled = false;
        }
        public string selectedCardId = "";
        void cardbtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            selectedCardId = btn.Tag.ToString();
        }

        //Thực hiện nâng cược, gửi lên các thông tin cần thiết, count là số lần nâng cược
        private void btnnangcuoc_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtcuoc.Text) > int.Parse(txtcurrentbet.Text))
            {
                count++;
                string message = txtmoney.Text + ";" + txtcuoc.Text + ";" + txttiencuocvonghientai.Text + ";" + count;
                Clientsk.datatype = "NANGCUOC";
                Clientsk.Send(message);
                txtcuoc.Clear();
                Disable();
            }
            else
            {
                MessageBox.Show("Không hợp lệ tiền cược phải lớn hơn currentBet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Thực hiện cược hết, gửi lên các thông tin cần thiết
        private void btncuochet_Click(object sender, EventArgs e)
        {
            string message = txtmoney.Text;
            Clientsk.datatype = "CUOCHET";
            Clientsk.Send(message);
            Disable();
        }

        //Thực hiện bỏ bài 
        private void btnbobai_Click(object sender, EventArgs e)
        {
            Clientsk.datatype = "BOBAI";
            Clientsk.Send("");
            Disable();
        }

        //Thực hiện kiểm bài
        private void btnkiembai_Click(object sender, EventArgs e)
        {
            Clientsk.datatype = "KIEMBAI";
            Clientsk.Send("");
            Disable();
        }

        //Thực hiện theo bài, gửi lên các thông tin cần thiết
        private void btntheo_Click(object sender, EventArgs e)
        {
            string s =  txtmoney.Text + ";" + txttiencuocvonghientai.Text + ";" + count;
            Clientsk.datatype = "THEO";
            Clientsk.Send(s);
            Disable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            How2play how2Play = new How2play();
            how2Play.Show();
        }

        //Hàm gửi lên thông điệp RESET từ Client lên Server
        public void NextRound(string s) 
        {
            Clientsk.datatype = "RESET";
            Clientsk.Send(s);
        }

        //Reset lại biến count về 0, này được gọi khi kết thúc 1 vòng chơi và chuẩn bị bắt đầu vòng chơi mới 
        public void RESETCOUNTNUTNANGCUOC()
        {
            count = 0;
            txtcurrentbet.Text = "0";
        }

        //Hiện lên 3 lá bài chung
        public void InitCardR2()
        {
            txttiencuocvonghientai.Text = "0";
            int i = 0;
            string a = "";
            for (int j = 2; j < 5; j++)
            {
                a = ThisPlayer.card[j];
                switch (a)
                {
                    case "_1co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._1co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._1co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._1co;
                        }
                        break;
                    case "_1ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._1ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._1ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._1ro;
                        }
                        break;
                    case "_1bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._1bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._1bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._1bich;
                        }
                        break;
                    case "_1chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._1chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._1chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._1chuong;
                        }
                        break;

                    case "_2co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._2co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._2co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._2co;
                        }
                        break;
                    case "_2ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._2ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._2ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._2ro;
                        }
                        break;
                    case "_2bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._2bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._2bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._2bich;
                        }
                        break;
                    case "_2chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._2chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._2chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._2chuong;
                        }
                        break;

                    case "_3co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._3co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._3co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._3co;
                        }
                        break;
                    case "_3ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._3ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._3ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._3ro;
                        }
                        break;
                    case "_3bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._3bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._3bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._3bich;
                        }
                        break;
                    case "_3chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._3chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._3chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._3chuong;
                        }
                        break;

                    case "_4co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._4co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._4co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._4co;
                        }
                        break;
                    case "_4ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._4ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._4ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._4ro;
                        }
                        break;
                    case "_4bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._4bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._4bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._4bich;
                        }
                        break;
                    case "_4chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._4chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._4chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._4chuong;
                        }
                        break;

                    case "_5co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._5co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._5co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._5co;
                        }
                        break;
                    case "_5ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._5ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._5ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._5ro;
                        }
                        break;
                    case "_5bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._5bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._5bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._5bich;
                        }
                        break;
                    case "_5chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._5chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._5chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._5chuong;
                        }
                        break;

                    case "_6co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._6co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._6co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._6co;
                        }
                        break;
                    case "_6ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._6ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._6ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._6ro;
                        }
                        break;
                    case "_6bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._6bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._6bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._6bich;
                        }
                        break;
                    case "_6chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._6chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._6chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._6chuong;
                        }
                        break;

                    case "_7co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._7co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._7co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._7co;
                        }
                        break;
                    case "_7ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._7ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._7ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._7ro;
                        }
                        break;
                    case "_7bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._7bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._7bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._7bich;
                        }
                        break;
                    case "_7chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._7chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._7chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._7chuong;
                        }
                        break;

                    case "_8co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._8co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._8co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._8co;
                        }
                        break;
                    case "_8ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._8ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._8ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._8ro;
                        }
                        break;
                    case "_8bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._8bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._8bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._8bich;
                        }
                        break;
                    case "_8chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._8chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._8chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._8chuong;
                        }
                        break;

                    case "_9co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._9co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._9co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._9co;
                        }
                        break;
                    case "_9ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._9ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._9ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._9ro;
                        }
                        break;
                    case "_9bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._9bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._9bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._9bich;
                        }
                        break;
                    case "_9chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._9chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._9chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._9chuong;
                        }
                        break;

                    case "_10co":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._10co;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._10co;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._10co;
                        }
                        break;
                    case "_10ro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._10ro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._10ro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._10ro;
                        }
                        break;
                    case "_10bich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._10bich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._10bich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._10bich;
                        }
                        break;
                    case "_10chuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources._10chuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources._10chuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources._10chuong;
                        }
                        break;

                    case "Jco":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Jco;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Jco;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Jco;
                        }
                        break;
                    case "Jro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Jro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Jro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Jro;
                        }
                        break;
                    case "Jbich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Jbich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Jbich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Jbich;
                        }
                        break;
                    case "Jchuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Jchuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Jchuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Jchuong;
                        }
                        break;

                    case "Qco":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Qco;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Qco;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Qco;
                        }
                        break;
                    case "Qro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Qro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Qro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Qro;
                        }
                        break;
                    case "Qbich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Qbich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Qbich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Qbich;
                        }
                        break;
                    case "Qchuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Qchuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Qchuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Qchuong;
                        }
                        break;

                    case "Kco":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Kco;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Kco;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Kco;
                        }
                        break;
                    case "Kro":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Kro;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Kro;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Kro;
                        }
                        break;
                    case "Kbich":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Kbich;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Kbich;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Kbich;
                        }
                        break;
                    case "Kchuong":
                        if (i == 0)
                        {
                            btnchung1.BackgroundImage = Properties.Resources.Kchuong;
                        }
                        else if (i == 1)
                        {
                            btnchung2.BackgroundImage = Properties.Resources.Kchuong;
                        }
                        else if (i == 2)
                        {
                            btnchung3.BackgroundImage = Properties.Resources.Kchuong;
                        }
                        break;
                }
                i++;
            }
        }

        //Hiển thị lá bài chung thứ 4
        public void InitCardR3()
        {
            txttiencuocvonghientai.Text = "0";
            string a = "";
            for (int j = 5; j < 6; j++)
            {
                a = ThisPlayer.card[j];
                switch (a)
                {
                    case "_1co":
                        btnchung4.BackgroundImage = Properties.Resources._1co;
                        break;
                    case "_1ro":
                        btnchung4.BackgroundImage = Properties.Resources._1ro;
                        break;
                    case "_1bich":
                        btnchung4.BackgroundImage = Properties.Resources._1bich;
                        break;
                    case "_1chuong":
                        btnchung4.BackgroundImage = Properties.Resources._1chuong;
                        break;

                    case "_2co":
                        btnchung4.BackgroundImage = Properties.Resources._2co;
                        break;
                    case "_2ro":
                        btnchung4.BackgroundImage = Properties.Resources._2ro;
                        break;
                    case "_2bich":
                        btnchung4.BackgroundImage = Properties.Resources._2bich;
                        break;
                    case "_2chuong":
                        btnchung4.BackgroundImage = Properties.Resources._2chuong;
                        break;

                    case "_3co":
                        btnchung4.BackgroundImage = Properties.Resources._3co;
                        break;
                    case "_3ro":
                        btnchung4.BackgroundImage = Properties.Resources._3ro;
                        break;
                    case "_3bich":
                        btnchung4.BackgroundImage = Properties.Resources._3bich;
                        break;
                    case "_3chuong":
                        btnchung4.BackgroundImage = Properties.Resources._3chuong;
                        break;

                    case "_4co":
                        btnchung4.BackgroundImage = Properties.Resources._4co;
                        break;
                    case "_4ro":
                        btnchung4.BackgroundImage = Properties.Resources._4ro;
                        break;
                    case "_4bich":
                        btnchung4.BackgroundImage = Properties.Resources._4bich;
                        break;
                    case "_4chuong":
                        btnchung4.BackgroundImage = Properties.Resources._4chuong;
                        break;

                    case "_5co":
                        btnchung4.BackgroundImage = Properties.Resources._5co;
                        break;
                    case "_5ro":
                        btnchung4.BackgroundImage = Properties.Resources._5ro;
                        break;
                    case "_5bich":
                        btnchung4.BackgroundImage = Properties.Resources._5bich;
                        break;
                    case "_5chuong":
                        btnchung4.BackgroundImage = Properties.Resources._5chuong;
                        break;

                    case "_6co":
                        btnchung4.BackgroundImage = Properties.Resources._6co;
                        break;
                    case "_6ro":
                        btnchung4.BackgroundImage = Properties.Resources._6ro;
                        break;
                    case "_6bich":
                        btnchung4.BackgroundImage = Properties.Resources._6bich;
                        break;
                    case "_6chuong":
                        btnchung4.BackgroundImage = Properties.Resources._6chuong;
                        break;

                    case "_7co":
                        btnchung4.BackgroundImage = Properties.Resources._7co;
                        break;
                    case "_7ro":
                        btnchung4.BackgroundImage = Properties.Resources._7ro;
                        break;
                    case "_7bich":
                        btnchung4.BackgroundImage = Properties.Resources._7bich;
                        break;
                    case "_7chuong":
                        btnchung4.BackgroundImage = Properties.Resources._7chuong;
                        break;

                    case "_8co":
                        btnchung4.BackgroundImage = Properties.Resources._8co;
                        break;
                    case "_8ro":
                        btnchung4.BackgroundImage = Properties.Resources._8ro;
                        break;
                    case "_8bich":
                        btnchung4.BackgroundImage = Properties.Resources._8bich;
                        break;
                    case "_8chuong":
                        btnchung4.BackgroundImage = Properties.Resources._8chuong;
                        break;

                    case "_9co":
                        btnchung4.BackgroundImage = Properties.Resources._9co;
                        break;
                    case "_9ro":
                        btnchung4.BackgroundImage = Properties.Resources._9ro;
                        break;
                    case "_9bich":
                        btnchung4.BackgroundImage = Properties.Resources._9bich;
                        break;
                    case "_9chuong":
                        btnchung4.BackgroundImage = Properties.Resources._9chuong;
                        break;

                    case "_10co":
                        btnchung4.BackgroundImage = Properties.Resources._10co;
                        break;
                    case "_10ro":
                        btnchung4.BackgroundImage = Properties.Resources._10ro;
                        break;
                    case "_10bich":
                        btnchung4.BackgroundImage = Properties.Resources._10bich;
                        break;
                    case "_10chuong":
                        btnchung4.BackgroundImage = Properties.Resources._10chuong;
                        break;

                    case "Jco":
                        btnchung4.BackgroundImage = Properties.Resources.Jco;
                        break;
                    case "Jro":
                        btnchung4.BackgroundImage = Properties.Resources.Jro;
                        break;
                    case "Jbich":
                        btnchung4.BackgroundImage = Properties.Resources.Jbich;
                        break;
                    case "Jchuong":
                        btnchung4.BackgroundImage = Properties.Resources.Jchuong;
                        break;

                    case "Qco":
                        btnchung4.BackgroundImage = Properties.Resources.Qco;
                        break;
                    case "Qro":
                        btnchung4.BackgroundImage = Properties.Resources.Qro;
                        break;
                    case "Qbich":
                        btnchung4.BackgroundImage = Properties.Resources.Qbich;
                        break;
                    case "Qchuong":
                        btnchung4.BackgroundImage = Properties.Resources.Qchuong;
                        break;

                    case "Kco":
                        btnchung4.BackgroundImage = Properties.Resources.Kco;
                        break;
                    case "Kro":
                        btnchung4.BackgroundImage = Properties.Resources.Kro;
                        break;
                    case "Kbich":
                        btnchung4.BackgroundImage = Properties.Resources.Kbich;
                        break;
                    case "Kchuong":
                        btnchung4.BackgroundImage = Properties.Resources.Kchuong;
                        break;
                }
            }
        }

        //Hiển thị lá bài chung thứ 5
        public void InitCardR4()
        {
            txttiencuocvonghientai.Text = "0";
            string a = "";
            for (int j = 6; j < 7; j++)
            {
                a = ThisPlayer.card[j];
                switch (a)
                {
                    case "_1co":
                        btnchung5.BackgroundImage = Properties.Resources._1co;
                        break;
                    case "_1ro":
                        btnchung5.BackgroundImage = Properties.Resources._1ro;
                        break;
                    case "_1bich":
                        btnchung5.BackgroundImage = Properties.Resources._1bich;
                        break;
                    case "_1chuong":
                        btnchung5.BackgroundImage = Properties.Resources._1chuong;
                        break;

                    case "_2co":
                        btnchung5.BackgroundImage = Properties.Resources._2co;
                        break;
                    case "_2ro":
                        btnchung5.BackgroundImage = Properties.Resources._2ro;
                        break;
                    case "_2bich":
                        btnchung5.BackgroundImage = Properties.Resources._2bich;
                        break;
                    case "_2chuong":
                        btnchung5.BackgroundImage = Properties.Resources._2chuong;
                        break;

                    case "_3co":
                        btnchung5.BackgroundImage = Properties.Resources._3co;
                        break;
                    case "_3ro":
                        btnchung5.BackgroundImage = Properties.Resources._3ro;
                        break;
                    case "_3bich":
                        btnchung5.BackgroundImage = Properties.Resources._3bich;
                        break;
                    case "_3chuong":
                        btnchung5.BackgroundImage = Properties.Resources._3chuong;
                        break;

                    case "_4co":
                        btnchung5.BackgroundImage = Properties.Resources._4co;
                        break;
                    case "_4ro":
                        btnchung5.BackgroundImage = Properties.Resources._4ro;
                        break;
                    case "_4bich":
                        btnchung5.BackgroundImage = Properties.Resources._4bich;
                        break;
                    case "_4chuong":
                        btnchung5.BackgroundImage = Properties.Resources._4chuong;
                        break;

                    case "_5co":
                        btnchung5.BackgroundImage = Properties.Resources._5co;
                        break;
                    case "_5ro":
                        btnchung5.BackgroundImage = Properties.Resources._5ro;
                        break;
                    case "_5bich":
                        btnchung5.BackgroundImage = Properties.Resources._5bich;
                        break;
                    case "_5chuong":
                        btnchung5.BackgroundImage = Properties.Resources._5chuong;
                        break;

                    case "_6co":
                        btnchung5.BackgroundImage = Properties.Resources._6co;
                        break;
                    case "_6ro":
                        btnchung5.BackgroundImage = Properties.Resources._6ro;
                        break;
                    case "_6bich":
                        btnchung5.BackgroundImage = Properties.Resources._6bich;
                        break;
                    case "_6chuong":
                        btnchung5.BackgroundImage = Properties.Resources._6chuong;
                        break;

                    case "_7co":
                        btnchung5.BackgroundImage = Properties.Resources._7co;
                        break;
                    case "_7ro":
                        btnchung5.BackgroundImage = Properties.Resources._7ro;
                        break;
                    case "_7bich":
                        btnchung5.BackgroundImage = Properties.Resources._7bich;
                        break;
                    case "_7chuong":
                        btnchung5.BackgroundImage = Properties.Resources._7chuong;
                        break;

                    case "_8co":
                        btnchung5.BackgroundImage = Properties.Resources._8co;
                        break;
                    case "_8ro":
                        btnchung5.BackgroundImage = Properties.Resources._8ro;
                        break;
                    case "_8bich":
                        btnchung5.BackgroundImage = Properties.Resources._8bich;
                        break;
                    case "_8chuong":
                        btnchung5.BackgroundImage = Properties.Resources._8chuong;
                        break;

                    case "_9co":
                        btnchung5.BackgroundImage = Properties.Resources._9co;
                        break;
                    case "_9ro":
                        btnchung5.BackgroundImage = Properties.Resources._9ro;
                        break;
                    case "_9bich":
                        btnchung5.BackgroundImage = Properties.Resources._9bich;
                        break;
                    case "_9chuong":
                        btnchung5.BackgroundImage = Properties.Resources._9chuong;
                        break;

                    case "_10co":
                        btnchung5.BackgroundImage = Properties.Resources._10co;
                        break;
                    case "_10ro":
                        btnchung5.BackgroundImage = Properties.Resources._10ro;
                        break;
                    case "_10bich":
                        btnchung5.BackgroundImage = Properties.Resources._10bich;
                        break;
                    case "_10chuong":
                        btnchung5.BackgroundImage = Properties.Resources._10chuong;
                        break;

                    case "Jco":
                        btnchung5.BackgroundImage = Properties.Resources.Jco;
                        break;
                    case "Jro":
                        btnchung5.BackgroundImage = Properties.Resources.Jro;
                        break;
                    case "Jbich":
                        btnchung5.BackgroundImage = Properties.Resources.Jbich;
                        break;
                    case "Jchuong":
                        btnchung5.BackgroundImage = Properties.Resources.Jchuong;
                        break;

                    case "Qco":
                        btnchung5.BackgroundImage = Properties.Resources.Qco;
                        break;
                    case "Qro":
                        btnchung5.BackgroundImage = Properties.Resources.Qro;
                        break;
                    case "Qbich":
                        btnchung5.BackgroundImage = Properties.Resources.Qbich;
                        break;
                    case "Qchuong":
                        btnchung5.BackgroundImage = Properties.Resources.Qchuong;
                        break;

                    case "Kco":
                        btnchung5.BackgroundImage = Properties.Resources.Kco;
                        break;
                    case "Kro":
                        btnchung5.BackgroundImage = Properties.Resources.Kro;
                        break;
                    case "Kbich":
                        btnchung5.BackgroundImage = Properties.Resources.Kbich;
                        break;
                    case "Kchuong":
                        btnchung5.BackgroundImage = Properties.Resources.Kchuong;
                        break;
                }
            }
        }

        //Hiển thị thông tin nhận được lên trên listview khi được gọi
        public void Addonlistview(string s)
        {
            ListViewItem newItem = new ListViewItem(s);
            listView1.Items.Add(newItem);
            newItem.EnsureVisible();
        }

        //Gửi thông điệp tin nhắn đến Server 
        private void btnsend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            Clientsk.datatype = "MESSAGE";
            Clientsk.Send(message);
            txtMessage.Clear();
        }
        public bool label4name(string s)
        {
            if (s == label4.Text)
            {
                check = true;
            }
            return check;
        }

        //Hàm để gửi thông điệp RESULT tới Server khi hàm được gọi
        public void Ketqua()
        {
            Clientsk.datatype = "RESULT";
            Clientsk.Send("");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PokerClient
{
    public partial class Waiting : Form
    {
        public Waiting wt;
        public List<Label> PlayerName = new List<Label>();
        public int connectedPlayer = 0;
        public Waiting()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            wt = this;
            btnStart.Visible = false;

            PlayerName.Add(lbpl1);
            PlayerName.Add(lbpl2);
            PlayerName.Add(lbpl3);
            PlayerName.Add(lbpl4);
        }
        public void ShowStartButton()
        {
            btnStart.Visible = true;
        }
        public void DisplayConnectedPlayer(string name)
        {
            connectedPlayer++;

            switch (connectedPlayer)
            {
                case 1:
                    lbpl1.Text = name;
                    pictureBox1.Image = Properties.Resources.ga;
                    break;
                case 2:
                    lbpl2.Text = name;
                    pictureBox2.Image = Properties.Resources.ngua;
                    break;
                case 3:
                    lbpl3.Text = name;
                    pictureBox3.Image = Properties.Resources.khunglong;
                    break;
                case 4:
                    lbpl4.Text = name;
                    pictureBox4.Image = Properties.Resources.khunglongbeo;
                    break;
                default:
                    break;
            }
        }

        //Gửi thông điệp từ Client đến Server khi có người bấm nút Start
        private void btnStart_Click(object sender, EventArgs e)
        {
            Clientsk.datatype = "START";
            Clientsk.Send("");
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            How2play how2Play = new How2play();
            how2Play.Show();
        }
    }
}

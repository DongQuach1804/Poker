using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PokerClient
{
    public partial class Menu : Form
    {
        public static Waiting wt;
        public int serverport = 9999;
        public Menu()
        {
            InitializeComponent();
            ChangeFormTitle("Welcome to Poker");
        }
        private void ChangeFormTitle(string newTitle)
        {
            this.Text = newTitle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            How2play how = new How2play();
            how.Show();
        }
        
        //Khi bấm vào nút Create
        private void button2_Click(object sender, EventArgs e)
        {
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(txtIP.Text), serverport);
            Clientsk.datatype = "CONNECT";
            Clientsk.Connect(serverEP);
            wt = new Waiting();
            Clientsk.Send(txtPlayername.Text);
            ThisPlayer.name = txtPlayername.Text;
            wt.FormClosed += new FormClosedEventHandler(wt_FormClosed);
            wt.ShowStartButton();
            this.Hide();
            wt.Show();
        }
        void wt_FormClosed(object sender, EventArgs e)
        {
            Clientsk.datatype = "DISCONNECT";
            Clientsk.Send(ThisPlayer.name);
            Clientsk.clientsocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            Clientsk.clientsocket.Close();
            this.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Khi bấm vào nút Join
        private void button3_Click(object sender, EventArgs e)
        {
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(txtIP.Text), serverport);
            Clientsk.datatype = "CONNECT";
            Clientsk.Connect(serverEP);
            wt = new Waiting();
            Clientsk.Send(txtPlayername.Text);
            ThisPlayer.name = txtPlayername.Text;
            wt.FormClosed += new FormClosedEventHandler(wt_FormClosed);
            this.Hide();
            wt.Show();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpenClients_Click(object sender, EventArgs e)
        {

            Form formClient = new FormClient();
            formClient.Show();


        }

        private void buttonOpenAgents_Click(object sender, EventArgs e)
        {
            Form formAgents = new FormAgent();
            formAgents.Show();
        }

        private void buttonOpenDeal_Click(object sender, EventArgs e)
        {
            Form formDeal = new FormDeal();
            formDeal.Show();
        }
    }
}

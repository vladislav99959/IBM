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
    public partial class FormDeal : Form
    {
        public FormDeal()
        {
            InitializeComponent();
            ShowClients();
            ShowAgents();
            ShowDealSet();
        }

        void ShowClients()
        {

            comboBoxClient.Items.Clear();
            foreach (ClientsSet clientsSet in Program.wftDb.ClientsSet)
            {

                string[] item = { clientsSet.Id.ToString() + ". ", clientsSet.LastName,  clientsSet.BookName };

                comboBoxClient.Items.Add(string.Join(" ", item));

            }
        }

        void ShowAgents()
        {

            comboBoxAgent.Items.Clear();
            foreach (AgentsSet agentsSet in Program.wftDb.AgentsSet)
            {

                string[] item = { agentsSet.Id.ToString() + ".", agentsSet.FirstName, agentsSet.LastName };

                comboBoxAgent.Items.Add(string.Join(" ", item));

            }
        }

        void ShowDealSet()
        {

            listViewDeal.Items.Clear();
            foreach (DealSet deal in Program.wftDb.DealSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {

                    deal.Id.ToString(),
                    deal.Id_Agent.ToString(),
                    deal.AgentsSet.LastName + " " + deal.AgentsSet.FirstName + " " + deal.AgentsSet.MiddleName,
                    deal.Id_Client.ToString(),
                    deal.ClientsSet.LastName,
                    deal.ClientsSet.BookName,
                    deal.Price.ToString()

                }); ;
                item.Tag = deal;
                listViewDeal.Items.Add(item);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgent.SelectedItem != null && comboBoxClient.SelectedItem != null && textBoxSale.Text != "")
            {
                DealSet deal = new DealSet();

                deal.Id_Agent = Convert.ToInt32(comboBoxAgent.SelectedItem.ToString().Split('.')[0]);

                deal.Id_Client = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);

                deal.Price = Convert.ToInt64(textBoxSale.Text);

                Program.wftDb.DealSet.Add(deal);

                Program.wftDb.SaveChanges();
                
                ShowDealSet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewDeal.SelectedItems.Count == 1)
            {
                DealSet deal = listViewDeal.SelectedItems[0].Tag as DealSet;
                deal.Id_Agent = Convert.ToInt32(comboBoxAgent.SelectedItem.ToString().Split('.')[0]);
                deal.Id_Client = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]); 
                deal.Price = Convert.ToInt64(textBoxSale.Text);
                Program.wftDb.SaveChanges();
                ShowDealSet();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewDeal.SelectedItems.Count == 1)
                {
                    DealSet deal = listViewDeal.SelectedItems[0].Tag as DealSet;
                    Program.wftDb.DealSet.Remove(deal);
                    Program.wftDb.SaveChanges();
                    ShowDealSet();
                }
                    comboBoxAgent.SelectedItem = null;
                    comboBoxClient.SelectedItem = null;
                    textBoxSale.Text = "";
            }
        
        catch 
        {
            MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        }

        private void listViewDeal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDeal.SelectedItems.Count == 1)
            {
                DealSet deal = listViewDeal.SelectedItems[0].Tag as DealSet;
                comboBoxAgent.SelectedIndex = comboBoxAgent.FindString(deal.Id_Agent.ToString());
                comboBoxClient.SelectedIndex = comboBoxClient.FindString(deal.Id_Client.ToString());
                textBoxSale.Text = deal.Price.ToString();
            }
            else
            {
                comboBoxAgent.SelectedItem = null;
                comboBoxClient.SelectedItem = null;
                textBoxSale.Text = "";
            }
        }
    }
}

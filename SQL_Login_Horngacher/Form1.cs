using SQLLogin_Logik;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SQL_Login_Horngacher
{
    public partial class Form1 : Form
    {
        private ConnectionManager _connectionManager;

        public Form1()
        {
            InitializeComponent();
            _connectionManager = new ConnectionManager();
            cBox_Database.Enabled = false;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = _connectionManager.BuildConnectionString(
                    txtBox_Host.Text,
                    txtBox_User.Text,
                    txtBox_Password.Text,
                    chBox_WinAuth.Checked
                );

                _connectionManager.Connect(connectionString);

                // Datenbanken auflisten
                DataTable databases = _connectionManager.GetDatabases();

                cBox_Database.Items.Clear();
                foreach (DataRow database in databases.Rows)
                {
                    cBox_Database.Items.Add(database.Field<string>("database_name"));
                }

                cBox_Database.Enabled = true;  // <-- Jetzt aktivieren
                StatusLabel_Text.Text = "Connected to " + _connectionManager.GetCurrentDatabase() + " on " + txtBox_Host.Text;
                txtBox_Host.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message);
            }
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _connectionManager.Disconnect();
                cBox_Database.Items.Clear();
                // lblStatus.Text = "Disconnected";
                txtBox_Host.Enabled = true;
                cBox_Database.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message);
            }
        }

        private void cBox_Database_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cBox_Database.SelectedItem != null)
                {
                    _connectionManager.ChangeDatabase(cBox_Database.SelectedItem.ToString());
                    StatusLabel_Text.Text = "Connected to " + cBox_Database.SelectedItem.ToString() + " on " + txtBox_Host.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message);
            }
        }

        private void chBox_WinAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtBox_User.Enabled = !chBox_WinAuth.Checked;
            txtBox_Password.Enabled = !chBox_WinAuth.Checked;
        }

        
    }
}
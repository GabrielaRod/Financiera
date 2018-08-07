using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Financiera
{
    public partial class Activos : Form
    {
        MySqlConnection connection;
        private string connectionString = "DATASOURCE=localhost;PORT=3306;DATABASE=test;USERNAME=root;PASSWORD=;";
        DataTable table = new DataTable();

        public Activos()
        {
            InitializeComponent();
        }

        //open connection to database
        private bool OpenConnection()
        {
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;

                }
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Select all
            int client = int.Parse(ClientID.Text);
            string name = FirstName.Text;
            string lastname = LastName.Text;
            int loan = int.Parse(LoanNumb.Text);
            string query = "SELECT * FROM clients ClientID='" + client + "' OR FirstName='" + name + "' OR LastName='" + lastname + "' OR LoanNumb='" + loan + "'";

            try
            {     // Success, now list 
                if (this.OpenConnection() == true) { 

                    //Stablish connection with database
                    MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                    //Create Command
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.CommandTimeout = 60;
                    //Create a data reader 
                    MySqlDataReader reader; 
                    //Create adapter
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    //Execute the command
                    reader = commandDatabase.ExecuteReader();

                    // If there are available rows
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            client = int.Parse(reader.ToString());
                            name = reader.ToString();
                            lastname = reader.ToString();
                            loan = int.Parse(reader.ToString());

                            if (this.ClientID.Text == reader.GetString("ClientID") || this.FirstName.Text == reader.GetString("FirstName") || this.LastName.Text == reader.GetString("LastName") || this.LoanNumb.Text == reader.GetString("LoanNumb"))
                            {
                                adapter.Fill(table);
                                dataGridView1.DataSource = table;
                                //ID First name Last Name Address
                                //Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2) + " - " + reader.GetString(3));
                                // Example to save in the listView1 :
                                //string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                                //var listViewItem = new ListViewItem(row);
                                //listView1.Items.Add(listViewItem);
                            }
                        }
                        reader.Close();
                        
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    databaseConnection.Close();
                }
                else
                {
                    MessageBox.Show("THIS WILL BE CHANGE JUST TO REMEMBER THAT I HAVE TO DO IT.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
    }
}

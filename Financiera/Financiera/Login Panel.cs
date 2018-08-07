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
    public partial class Login : Form
    {
        MySqlConnection connection;
        private string connectionString = "DATASOURCE=localhost;PORT=3306;DATABASE=test;USERNAME=root;PASSWORD=;";
        DataTable table = new DataTable();
        
        public Login()
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

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;
            string query = "SELECT username, password FROM financierausers WHERE username='"+username+"' AND password='"+password+"'";

            //Open Connection and check if username and password match DB
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.CommandTimeout = 60;
                cmd.CommandText = query;
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    username = reader.ToString();
                    password = reader.ToString();

                    if(this.UsernameBox.Text == reader.GetString("username") && this.PasswordBox.Text == reader.GetString("password"))
                    {
                        LoginMessage.ForeColor = Color.Green;
                        LoginMessage.Text = "Login Successfully";
                    }
                  
                    else
                    {
                        LoginMessage.ForeColor = Color.Red;
                        LoginMessage.Text = "Username Or Password is Invalid";
                    }

                    //Once login credentials are valid, opens main window
                    FinancieraMain callform = new FinancieraMain();
                    callform.Show();
                    this.Hide();

                }
            }else
            {
                LoginMessage.Text = "Connection to server failed.";
            }
            
        }


        private void LoginMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


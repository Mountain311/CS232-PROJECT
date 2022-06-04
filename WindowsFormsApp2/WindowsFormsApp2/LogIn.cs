using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApp2
{
    public partial class LogIn : Form
    {
        public  static  string constring = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = xe)));User Id=ADAM;Password=adam";

        public LogIn()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text.Length != 0 && guna2TextBox2.Text.Length != 0)
            {
                try
                {
                    OracleConnection conn = new OracleConnection(constring);
                    conn.Open();
                    string query = " ";
                    query = "select id,password from ADMIN where id='" + guna2TextBox1.Text + "'";
                    OracleCommand cmdquery = new OracleCommand(query, conn);
                    OracleDataReader reader = cmdquery.ExecuteReader();
                    if (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string password = reader["password"].ToString();
                        if (password != guna2TextBox2.Text)
                        {
                            richTextBox1.Text = "*Wrong Password";
                            richTextBox1.ForeColor = Color.White;
                        }
                        else
                        {
                            //here further code come for testing i am showing some message
                            this.Hide();
                            MainMenu mainMenu = new MainMenu();
                            mainMenu.Show();
                            
                        }
                    }
                    else
                    {
                        richTextBox1.ForeColor = Color.White;
                        guna2TextBox1.ResetText();
                        guna2TextBox2.ResetText();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("FullFill requirments");
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp form2 = new SignUp();
            form2.Show();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

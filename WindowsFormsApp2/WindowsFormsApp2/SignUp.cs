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
using System.Configuration;

namespace WindowsFormsApp2
{
    public partial class SignUp : Form
    {
        public static SignUp instanceForm2;
        public string user_id;
        public string password;
        public string mobile;
        public SignUp()
        {
            InitializeComponent();
            instanceForm2 = this;
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text.Length == 0 || guna2TextBox3.Text.Length == 0 || guna2TextBox4.Text.Length == 0 || guna2TextBox2.Text.Length == 0)
            {
                
                richTextBox1.Text = "*Fill all required Fields";
                richTextBox1.ForeColor = Color.White;
            }
            else if (guna2TextBox2.Text != guna2TextBox3.Text)
            {
                richTextBox1.Text = "*Password does not match";
                richTextBox1.ForeColor = Color.White;
                guna2TextBox2.ResetText();
                guna2TextBox3.ResetText();
            }
            else if (!guna2CheckBox1.Checked)
            {
                richTextBox1.Text = "*Please Agree to Terms & Conditions";
                richTextBox1.ForeColor = Color.White;

            }
            else if (guna2TextBox2.Text == guna2TextBox3.Text)
            {
                try
                {
                    //this is the 1st way to assign conection string OracleConnection con=new OracleConnection(connectionstring);
                    //this is second way
                    //conn.ConnectionString = connectionstring;

                    OracleConnection conn = new OracleConnection(LogIn.constring);
                    conn.Open();
                    string query = " ";
                    query = "select id from ADMIN where id='" + guna2TextBox1.Text + "'";
                    OracleCommand cmdquery = new OracleCommand(query, conn);
                    OracleDataReader reader = cmdquery.ExecuteReader();
                    if (reader.Read())
                    {
                        richTextBox1.Text = "*This Id alreaady Exits.Please Choose different Id.";
                        richTextBox1.ForeColor = Color.White;
                        guna2TextBox1.ResetText();
                        guna2TextBox2.ResetText();
                        guna2TextBox3.ResetText();
                        guna2TextBox4.ResetText();
                    }
                    else
                    {
                        user_id = guna2TextBox1.Text;
                        password = guna2TextBox2.Text;
                        mobile = guna2TextBox4.Text;
                        this.Hide();
                        Auth form3 = new Auth();
                        form3.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn form1 = new LogIn();
            form1.Show();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
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

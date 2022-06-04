using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApp2
{
    public partial class Discount : Form
    {
        public Discount()
        {
            InitializeComponent();
            guna2Panel3.Hide();
            try
            {
                string query = "select discount.*,product.product_name from discount join product on product.category_id=discount.category_id and product.product_id=discount.product_id";
                guna2DataGridView1.DataSource = Dataload(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private DataTable Dataload(string q)//loads data into table
        {
            OracleConnection oracleConnection = new OracleConnection(LogIn.constring);
            oracleConnection.Open();
            string query = q;
            OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
            OracleDataReader reader = oracleCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            oracleConnection.Close();
            return dataTable;
        }
        private void QuerySender(string q)//send query
        {
            OracleConnection oracleConnection = new OracleConnection(LogIn.constring);
            oracleConnection.Open();
            string query = q;
            OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
            MessageBox.Show(query);
            oracleCommand.ExecuteNonQuery();
            oracleConnection.Close();
        }
        private bool CheckAlreadyProductId(string q)
        {
            OracleConnection oracleConnection = new OracleConnection(LogIn.constring);
            oracleConnection.Open();
            string query = q;
            OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
            MessageBox.Show(query);
            OracleDataReader reader = oracleCommand.ExecuteReader();
            return reader.Read();

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2Button6.Checked)
                {
                    string query = "select discount.*,product.product_name from discount join product on product.category_id=discount.category_id and product.product_id=discount.product_id";
                    guna2DataGridView1.DataSource = Dataload(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2Button6.Checked && guna2DataGridView1.RowCount != 0)
                {
                    string deletionId, query = " ";
                    deletionId = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                    query = "delete from Discount where discount_id=" + Int32.Parse(deletionId);
                    QuerySender(query);
                    guna2Button8_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Empty Table");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (guna2Button6.Checked && guna2DataGridView1.RowCount != 0)
            {

                Discount_id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                product_id.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                category_id.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                Discount_per.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            else
            {
                MessageBox.Show("Empty Table");
            }
        }

        private void AdminEditbtn_Click(object sender, EventArgs e)
        {
            if (Discount_id.Text.Length != 0)
            {
                try
                {
                    string query = "update Discount set discount_percent='" + Discount_per.Text + "'  where discount_id=" + Int32.Parse(Discount_id.Text);
                    QuerySender(query);
                    guna2Button8_Click(sender, e);
                    Discount_id.ResetText();
                    product_id.ResetText();
                    category_id.ResetText();
                    Discount_per.ResetText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Select Tuple");
            }
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            guna2Panel3.Show();
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            string query, query1;
            try
            {
                query = "select * from discount where discount_id=" + discount_idp2.Text;
                query1 = "select * from discount where product_id=" + Product.Text+"and category_id="+Category.Text ;
                if (discount_idp2.Text.Length != 0 && Product.Text.Length != 0 && Category.Text.Length != 0 && discount_per2.Text.Length != 0)
                {
                    if (CheckAlreadyProductId(query))
                    {
                        guna2HtmlLabel6.Text = "*This Discount Id already Exits. Please try Unique Id.";
                        guna2HtmlLabel6.ForeColor = Color.White;
                        discount_idp2.ResetText();
                    }
                    else if(CheckAlreadyProductId(query1))
                    {
                        guna2HtmlLabel6.Text = "*Discount on this Item already exist, you can update discount on previous screen";
                        guna2HtmlLabel6.ForeColor = Color.White;
                        discount_idp2.ResetText();
                    }
                    else 
                    {
                        query = "insert into discount values(" + discount_idp2.Text + "," + Product.Text + "," + Category.Text + ",'" + discount_per2.Text + "%')";
                        QuerySender(query);
                        guna2Button8_Click(sender, e);
                        guna2HtmlLabel6.Text = "*Successfully Inserted";
                        guna2HtmlLabel6.ForeColor = Color.White;
                        discount_idp2.ResetText();
                        discount_per2.ResetText();
                        Category.ResetText();
                        Product.ResetText();
                    }
                }
                else
                {
                    guna2HtmlLabel6.Text = "*Fill all required fields";
                    guna2HtmlLabel6.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2Panel3.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Product product1 = new Product();
            product1.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            stock stock1 = new stock();
            stock1.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sales sales1 = new Sales();
            sales1.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Payment payment = new Payment();
            payment.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Graphs graphs = new Graphs();
            graphs.Show();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Log Out?", "Log Out", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
                LogIn logIn = new LogIn();
                logIn.Show();
            }
            else
            {

            }
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

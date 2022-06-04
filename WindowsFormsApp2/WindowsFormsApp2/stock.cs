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
    public partial class stock : Form
    {
        public stock()
        {
            InitializeComponent();
            guna2Panel3.Hide();
            try
            {
                string query = "select * from stock";
                guna2DataGridView1.DataSource = Dataload(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void guna2Button4_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2Button4.Checked)
                {
                    string query = "select * from stock";
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
                if (guna2Button4.Checked && guna2DataGridView1.RowCount != 0)
                {
                    string deletionId, query = " ";
                    deletionId = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                    query = "delete from stock where stock_id=" + Int32.Parse(deletionId);
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
            if (guna2Button4.Checked && guna2DataGridView1.RowCount != 0)
            {
                stock_id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                product_id.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                category_id.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                Quantity.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
                string dd = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                guna2DateTimePicker1.Value = DateTime.Parse(dd);
            }
            
            else
            {
                MessageBox.Show("Empty Table");
            }
        }

        private void AdminEditbtn_Click(object sender, EventArgs e)
        {
            if (stock_id.Text.Length != 0)
            {
                try
                {
                    string query = "update stock set product_id=" + Int32.Parse(product_id.Text) + ",category_id=" + Int32.Parse(category_id.Text) + ",quantity=" + Int32.Parse(Quantity.Text) + ",date_of_purchase=TO_DATE('" + guna2DateTimePicker1.Text+ "' ,'mm/dd/yyyy') where stock_id="+stock_id.Text;
                    QuerySender(query);
                    guna2Button8_Click(sender, e);
                    stock_id.ResetText();
                    category_id.ResetText();
                    product_id.ResetText();
                    Quantity.ResetText();
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

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2Panel3.Hide();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            guna2Panel3.Show();
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
        private void guna2Button13_Click(object sender, EventArgs e)
        {
            string query;
            try
            {
                query = "select * from stock where stock_id=" + stock_idp2.Text;
                if (stock_idp2.Text.Length != 0&&Product.Text.Length!=0&&Category.Text.Length!=0  && quantityp2.Text.Length!=0)
                {
                    if (CheckAlreadyProductId(query))
                    {
                        guna2HtmlLabel6.Text = "*This Id already Exits. Please try different one.";
                        guna2HtmlLabel6.ForeColor = Color.White;
                        stock_idp2.ResetText();
                    }
                    else
                    {
                        query = "insert into stock values(" + stock_idp2.Text + "," + Product.Text + "," + Category.Text + "," + quantityp2.Text + ",TO_DATE('" + guna2DateTimePicker2.Text + "' ,'mm/dd/yyyy'))";
                        QuerySender(query);
                        guna2Button8_Click(sender, e);
                        guna2HtmlLabel6.Text = "Successfully Inserted";
                        guna2HtmlLabel6.ForeColor = Color.White;
                        stock_idp2.ResetText();
                        Category.ResetText();
                        quantityp2.ResetText();
                        Product.ResetText();
                        guna2DateTimePicker2.ResetText();
                    }
                }
                else
                {
                    guna2HtmlLabel6.Text = "*Fill all required fields";
                    guna2HtmlLabel6.ForeColor = Color.White;
                }
            }
            catch  (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

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
            this.Hide();
            Discount discount = new Discount();
            discount.Show();
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
    }
}

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
    public partial class Payment : Form
    {
        public Payment()
        {
            
            InitializeComponent();
            guna2Panel3.Hide();
            try
            {
                string query = "select * from Payment";
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

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2Panel3.Hide();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2Button5.Checked)
                {
                    string query = "select * from Payment";
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
                if (guna2Button5.Checked && guna2DataGridView1.RowCount != 0)
                {
                    string deletionId, query = " ";
                    deletionId = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                    query = "delete from Payment where Payment_id=" + Int32.Parse(deletionId);
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
            if (guna2Button5.Checked && guna2DataGridView1.RowCount != 0)
            {

                Payment_id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                sale_amount.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                cashier_name.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                customer_name.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
                string dd = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();//reading date from grid view and converting it into string
                guna2DateTimePicker1.Value = DateTime.Parse(dd);//converting date string into date datatype and toring it into calender
                Description.Text = guna2DataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            else
            {
                MessageBox.Show("Empty Table");
            }
        }

        private void AdminEditbtn_Click(object sender, EventArgs e)
        {
            if (Payment_id.Text.Length != 0)
            {
                try
                {
                    string query = "update Payment set amount=" + Int32.Parse(sale_amount.Text) + ",cashier_name='" + cashier_name.Text + "',Customer_name='" + customer_name.Text + "',Payment_Date=TO_DATE('" + guna2DateTimePicker1.Text + "' ,'mm/dd/yyyy'),description='" + Description.Text + "'  where Payment_id=" + Int32.Parse(Payment_id.Text);
                    QuerySender(query);
                    guna2Button8_Click(sender, e);
                    Payment_id.ResetText();
                    sale_amount.ResetText();
                    Description.ResetText();
                    cashier_name.ResetText();
                    customer_name.ResetText();
                    guna2DateTimePicker1.ResetText();
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
            string query;
            try
            {
                query = "select * from Payment where payment_id=" + sale_idp2.Text;
                if (sale_idp2.Text.Length != 0 && sale_amountp2.Text.Length != 0 && cashier.Text.Length != 0 && customer_namep2.Text.Length!=0)
                {
                    if (CheckAlreadyProductId(query))
                    {
                        guna2HtmlLabel6.Text = "*This Payment Id already Exits. Please try different Id.";
                        guna2HtmlLabel6.ForeColor = Color.White;
                        sale_idp2.ResetText();
                    }
                    else
                    {
                        query = "insert into payment values(" + sale_idp2.Text + "," + sale_amountp2.Text + ",'" + cashier.Text + "','" + customer_namep2.Text + "',TO_DATE('" + guna2DateTimePicker2.Text + "' ,'mm/dd/yyyy'),'" + sales_desc.Text + "')";
                        QuerySender(query);
                        guna2Button8_Click(sender, e);
                        guna2HtmlLabel6.Text = "*Successfully Inserted";
                        guna2HtmlLabel6.ForeColor = Color.White;
                        sale_amountp2.ResetText();
                        sale_idp2.ResetText();
                        sales_desc.ResetText();
                        cashier.ResetText();
                        customer_namep2.ResetText();
                        guna2DateTimePicker1.ResetText();
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Payment payment = new Payment();
            payment.Show();
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

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

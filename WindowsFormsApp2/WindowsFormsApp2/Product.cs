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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            string query = "select * from product natural join p_company  natural join p_price";
            guna2DataGridView1.DataSource = Dataload(query);
        }

        private void Product_Load(object sender, EventArgs e)
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
        private void moveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            imgSlide.Location = new Point(b.Location.X + 20, b.Location.Y - 25);
            imgSlide.SendToBack();
        }

        private void guna2Button2_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            if (guna2Button2.Checked)
            {
                string query = "select * from product natural join p_company  natural join p_price";
                guna2DataGridView1.DataSource = Dataload(query);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (guna2Button2.Checked && guna2DataGridView1.RowCount != 0)
            {
                string deletionId, query = " ",categoryid;
                deletionId = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                categoryid = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                query = "delete from product where product_id=" + Int32.Parse(deletionId) + " and category_id="+Int32.Parse(categoryid);
                QuerySender(query);
                guna2Button8_Click_1(sender, e);
            }
            else
            {
                MessageBox.Show("Empty Table");
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (guna2Button2.Checked && guna2DataGridView1.RowCount != 0)
            {
                Product_Id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                Category_Id.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                Product_Name.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                Product_Description.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
                Product_Company.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                Product_Price.Text = guna2DataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            else
            {
                MessageBox.Show("Empty Table");
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            


            if (Product_Id.Text.Length != 0 && Category_Id.Text.Length != 0)
            {
                string query = "update product set PRODUCT_NAME='" + Product_Name.Text + "',PRODUCT_DESC='" + Product_Description.Text + "' where product_id=" + Int32.Parse(Product_Id.Text) + " and category_id=" + Int32.Parse(Category_Id.Text);
                string query1 = "update p_company set COMPANY='" + Product_Company.Text + "' where product_id=" + Int32.Parse(Product_Id.Text) + " and category_id=" + Int32.Parse(Category_Id.Text);
                string query2 = "update p_price set PRICE='" + Product_Price.Text + "' where product_id=" + Int32.Parse(Product_Id.Text) + " and category_id=" + Int32.Parse(Category_Id.Text);
                QuerySender(query);
                QuerySender(query1);
                QuerySender(query2);
                
                guna2Button8_Click_1(sender, e);
                
            }
            else
            {
                MessageBox.Show("Select Tuple");
            }
            
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

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button8_Click_2(object sender, EventArgs e)
        {
            QRCode qr = new QRCode();
            qr.Show();
        }
    }
}

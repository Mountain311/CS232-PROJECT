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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            guna2Panel1.Show();//admin panel last layer
            guna2Panel3.Hide();//product admin panel
            string q = "Select * from admin";
            guna2DataGridView1.DataSource = Dataload(q);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void moveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            imgSlide.Location = new Point(b.Location.X+20, b.Location.Y - 25);
            imgSlide.SendToBack();
        }

        private void guna2Button2_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Panel3.Hide();
            guna2Panel1.Show();
          

        }
        private DataTable Dataload(string q)
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
        private void QuerySender(string q)
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
            if(guna2Button1.Checked)
            {
                string query = "Select * from admin";
                guna2DataGridView1.DataSource = Dataload(query);
            }
            
           
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Product product = new Product();
            product.Show();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (guna2Button1.Checked && guna2DataGridView1.RowCount != 0)
            {
                string deletionId,query=" ";
                deletionId = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                query = "delete from ADMIN where ID='" + deletionId + "'";
                QuerySender(query);
                guna2Button8_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Table is Empty");
            }
           
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (guna2Button1.Checked && guna2DataGridView1.RowCount!=0)
            {
               
                AdminEditid.Text= guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                AdminEditMob.Text= guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                AdminEditpass.Text= guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else
            {
                MessageBox.Show("Table is Empty");
            }
           
        }

      

        private void AdminEditid_Click(object sender, MouseEventArgs e)
        {
           
        }

        private void AdminEditid_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdminEditMob_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdminEditbtn_Click(object sender, EventArgs e)
        {
            if (AdminEditid.Text.Length != 0 && AdminEditMob.Text.Length != 0 && AdminEditpass.Text.Length != 0)
            {
                string query = " ";
                query = "update ADMIN set Password='" + AdminEditpass.Text + "',Mobile_No='" + AdminEditMob.Text + "'where ID='" + AdminEditid.Text + "'";
                QuerySender(query);
                guna2Button8_Click(sender, e);
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
            string query = "select * from product where product_id=" + Int32.Parse(Product_Id.Text) + " and category_id=" + Int32.Parse(Category_Id.Text);
            string query1 = "select * from p_company where product_id=" + Int32.Parse(Product_Id.Text) + " and category_id=" + Int32.Parse(Category_Id.Text) ;
            string query2 = "select * from p_price where product_id=" + Int32.Parse(Product_Id.Text) + " and category_id=" + Int32.Parse(Category_Id.Text) ;
            if(CheckAlreadyProductId(query)==true || CheckAlreadyProductId(query1)==true ||   CheckAlreadyProductId(query2)==true )
            {
                guna2HtmlLabel3.Text = "*This Id already Exits. Please try different one.";
                guna2HtmlLabel3.ForeColor = Color.White;
                Product_Id.ResetText();
                Category_Id.ResetText();
            }
            else
            {
                guna2HtmlLabel3.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(43)))));
                query = "insert into product(PRODUCT_ID,CATEGORY_ID,PRODUCT_NAME,PRODUCT_DESC) values (" + Int32.Parse(Product_Id.Text) + "," + Int32.Parse(Category_Id.Text) + ",'" + Product_Name.Text + "','" + Product_Description.Text + "')";
                query1 = "insert into p_price(PRODUCT_ID,CATEGORY_ID,PRICE) values (" + Int32.Parse(Product_Id.Text) + "," + Int32.Parse(Category_Id.Text) + "," + float.Parse(Product_Price.Text)+ ")";
                query2 = "insert into p_company(PRODUCT_ID,CATEGORY_ID,COMPANY) values (" + Int32.Parse(Product_Id.Text) + "," + Int32.Parse(Category_Id.Text) + ",'" + Product_Company.Text + "')";
                QuerySender(query);
                QuerySender(query1);
                QuerySender(query2);
                guna2HtmlLabel3.ForeColor = Color.White;
                guna2HtmlLabel3.Text = "Data Inserted Successfully";
                //QR code generation
                QRCoder.QRCodeGenerator QG = new QRCoder.QRCodeGenerator();
                var MyData = QG.CreateQrCode(Product_Id.Text, QRCoder.QRCodeGenerator.ECCLevel.H);
                var code = new QRCoder.QRCode(MyData);
                //pictureBox1.Image = code.GetGraphic(50);

            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2Panel3.Hide();
            guna2Panel1.Show();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

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
            Discount discount= new Discount();
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

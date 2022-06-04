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
using Guna.UI2.WinForms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Graphs : Form
    {
      
        public Graphs()
        {
            InitializeComponent();
            QuerySender();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void QuerySender()//send query
        {
            OracleConnection oracleConnection = new OracleConnection(LogIn.constring);
            oracleConnection.Open();
            string query = "select sale_date,Sum(amount) from sale group by sale_date";
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(query, oracleConnection);
            DataSet dataSet = new DataSet();
            oracleDataAdapter.Fill(dataSet);
            chart1.DataSource = dataSet;
            chart1.Series["Sales"].XValueMember = "sale_date";
            chart1.Series["Sales"].YValueMembers = "Sum(amount)";
            chart1.DataBind();
            oracleConnection.Close();
            //OracleCommand oracleCommand=new OracleCommand(query,oracleConnection);
            //OracleDataReader oracleDataReader=oracleCommand.ExecuteReader();
            //DataTable dataTable = new DataTable();
            //dataTable.Load(oracleDataReader);
            //chart1.DataSource=dataTable;
            ////chart1.DataBind();
            //chart1.Series["Sales"].XValueMember = "sale_date";
            //chart1.Series["Sales"].YValueMembers = "Sum(amount)";
            //oracleConnection.Close();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            QuerySender();
            chart1.Series["Sales"].ChartType = SeriesChartType.Bar;
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            QuerySender();
            chart1.Series["Sales"].ChartType = SeriesChartType.Area;
        }

        private void edit_Click(object sender, EventArgs e)
        {
            QuerySender();
            chart1.Series["Sales"].ChartType = SeriesChartType.Doughnut;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            QuerySender();
            chart1.Series["Sales"].ChartType = SeriesChartType.Line;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Product product = new Product();
            product.Show();
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

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

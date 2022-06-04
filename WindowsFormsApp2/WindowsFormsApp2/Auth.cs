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
using System.Net.Mail;
using System.Net;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace WindowsFormsApp2
{
    public partial class Auth : Form
    {
        int otpCode = 0;
        public Auth()
        {
            InitializeComponent();
            generatepassword(ref otpCode);
        }
        private void generatepassword(ref int parameter)
        {
            Random random = new Random();
            parameter = random.Next(111111, 999999);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {


            //WebRequest request = WebRequest.Create("http://localhost:8080/?username=john");
            //request.Method = "GET";
            //WebResponse response = request.GetResponse();

            //string email, pass, confirmpass,id;
            //email = SignUp.instanceForm2.mobile;
            //id = SignUp.instanceForm2.user_id;
            //pass = SignUp.instanceForm2.password;



            
               

            //    System.Random random = new System.Random();
            //    long randomCode = random.Next(10000, 99999);
            //    try
            //    {
            //        MailMessage mail = new MailMessage();
            //        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

             

            //    mail.From = new MailAddress("cricbuzz03@gmail.com");
            //        mail.To.Add(email);
            //        mail.Subject = "Create CricBuzz Account";


            //        mail.Body = "Write Given Code into the CricBuzz App for Confirmation\n" + randomCode.ToString();

            //        SmtpServer.Port = 587;
            //    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    SmtpServer.UseDefaultCredentials = false;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("cricbuzz03@gmail.com", "Fcbarcelona");
            //        SmtpServer.EnableSsl = true;

            //        SmtpServer.Send(mail);
            //        MessageBox.Show("Mail Sent");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
           

            

            //using twilio APIS
            try
            {
                TwilioClient.Init("AC0ad89e56b3321a66a7eb12cd882ba1a7", "021d81dac934f23a0a078cf040701f9a");

                var message = MessageResource.Create
                    (
                    to: SignUp.instanceForm2.mobile,
                    from: "+19897873650",
                    body: otpCode.ToString()
                    );
                guna2HtmlLabel1.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //WebClient client = new WebClient();
            //Stream s = client.OpenRead(String.Format("https://platform.clickatell.com/messages/http/send?apiKey=RJD-vLGpTbyM6I6AQc2k0w==&to={0}&content={1}", Form1.instanceForm1.mobile,otpCode.ToString()));
            //StreamReader reader= new StreamReader(s);
            //string result=reader.ReadToEnd();
            //MessageBox.Show(result, "Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
            //try
            {
                //MailMessage mailMessage = new MailMessage();
                //mailMessage.From = new MailAddress("stathom302@gmail.com");
                //mailMessage.To.Add(Form1.instanceForm1.mobile);
                //mailMessage.Subject = "OTP for your account";
                //mailMessage.Body = "Your One Time Password is " + otpCode.ToString();
                //mailMessage.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //System.Net.NetworkCredential ntwd = new NetworkCredential();
                //ntwd.UserName = "stathom302@gmail.com";
                //ntwd.Password = "databaseproject";
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = ntwd;
                //smtp.Port = 587;
                //smtp.EnableSsl = true;
                //smtp.Send(mailMessage);
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == otpCode.ToString())
            {
                try
                {
                    //this is the 1st way to assign conection string OracleConnection con=new OracleConnection(connectionstring);
                    //this is second way
                    //conn.ConnectionString = connectionstring;

                    OracleConnection conn = new OracleConnection(LogIn.constring);
                    conn.Open();
                    string query = " ";
                    //query = "select ID from ADMIN where ID='" + SignUp.instanceForm2.user_id + "'";
                    //OracleCommand cmdquery = new OracleCommand(query, conn);
                    //OracleDataReader reader = cmdquery.ExecuteReader();
                    //if (reader.Read())
                    //{
                    //    MessageBox.Show("This Id alreaady Exits");
                    //}
                    //else
                    //{
                        query = "insert into ADMIN(ID,Password,email) values ('" + SignUp.instanceForm2.user_id + "','" + SignUp.instanceForm2.password + "','" + SignUp.instanceForm2.mobile + "')";
                        OracleCommand cmdquery = new OracleCommand(query, conn);
                        //second way of writing a query
                        //cmdquery.CommandText = "insert into ADMIN(id,password,mobile) values ('" + bunifuMaterialTextbox1.Text + "','" + bunifuMaterialTextbox5.Text + "','" + bunifuMaterialTextbox4.Text + "')";
                        //cmdquery.Connection=conn;

                        cmdquery.ExecuteNonQuery();
                        conn.Close();
                        this.Hide();
                        LogIn logIn = new LogIn();
                        logIn.Show();
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("OTP doesn't match");
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.Show();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

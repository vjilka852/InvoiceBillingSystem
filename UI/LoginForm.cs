using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Invoice_Billing_System.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Use parameterized queries to prevent SQL injection
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM Login WHERE username = @username AND password = @password AND usertype = @usertype";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", txeuser.Text);
                cmd.Parameters.AddWithValue("@password", txtpassword.Text); // Consider hashing the password in the database and here
                cmd.Parameters.AddWithValue("@usertype", cmbUserType.SelectedItem.ToString());

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    // Successful login
                    MessageBox.Show("You are logged in as " + dt.Rows[0]["username"].ToString());
                    if (cmbUserType.SelectedIndex == 0) // Assuming 0 is Admin
                    {
                        AdminDashboard ad = new AdminDashboard();
                        ad.Show();
                        this.Hide();
                    }
                    else
                    {
                        UserDasboard ud = new UserDasboard();
                        ud.Show();
                        this.Hide();
                    }
                }
                else
                {
                    // Login failed
                    MessageBox.Show("Incorrect username, password, or user type.");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

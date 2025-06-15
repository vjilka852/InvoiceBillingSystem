using Invoice_Billing_System.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Invoice_Billing_System
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Example query for registering a new user
                    string sql = "INSERT INTO Regstration (user name, password, email) VALUES (@user name, @password, @email)";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@user name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text); // Hash and store securely in production
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Registration successful!");

                        // Navigate to the login form
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                        this.Hide(); // Hide the registration form
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

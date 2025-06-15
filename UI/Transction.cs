using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Invoice_Billing_System.UI
{
    public partial class Transction : Form
    {
        // Define your connection string
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;");

        public Transction()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            // When the "Show" button is clicked, fetch the data and display it
            BindData();
        }

        // Method to fetch and bind data to the DataGridView
        private void BindData()
        {
            try
            {
                // Open the connection
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Define your SQL query to fetch data from the Transactions table
                string query = "SELECT * FROM Products"; // Adjust based on your actual table structure
                SqlCommand cmd = new SqlCommand(query, con);

                // Use SqlDataAdapter to fill a DataTable with data from the database
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the data to the DataGridView
                dgvTransaction.DataSource = dt;
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during data fetching
                MessageBox.Show("Error fetching transaction data: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close(); // Closes the form
        }

        private void dgvTransaction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can add logic here if you want to perform any action when a cell is clicked
        }
    }
}

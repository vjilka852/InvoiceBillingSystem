using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using Guna.UI2.WinForms;

namespace Invoice_Billing_System.UI
{
    public partial class DealerCustomer : Form
    {
        SqlConnection conn;
        SqlDataReader sdr;//Connected Architecture
        SqlDataAdapter adapt;//Disconnected Achitecture
        int DeaCustId = 0;
        string type, name, email, contact, address;




        string query;

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;");


        public DealerCustomer()
        {
            InitializeComponent();
        }
      

        public DataTable GetDealerCustomerById(int deaCustId)
        {
            DataTable dt = new DataTable();

            try
            {
                // Open the connection
                con.Open();

                // Query to get the dealer/customer details by ID
                string query = "SELECT name, email, contact, address FROM deacust WHERE DeaCustId = @DeaCustId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DeaCustId", deaCustId);

                // Execute and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching details: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }

            return dt; // Return the data table with the results
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
        public DataTable GetProductById(int productId)
        {
            // Create a new DataTable to store the product details
            DataTable dt = new DataTable();

            try
            {
                // Open the connection
                con.Open();

                // SQL query to get the product details by ID
                string query = "SELECT name, email, address,contact FROM deacust WHERE DeaCustId = @DeacustId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", productId);

                // Fill the DataTable with the result of the query
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching product details: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }

            return dt; // Return the product details as a DataTable
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Open the connection
            con.Open();

            // Define the query for inserting data
            query = "insert into deacust (type, name, email, contact, address) values (@type, @name, @email, @contact, @address)";

            // Create a new SqlCommand
            SqlCommand cmd = new SqlCommand(query, con);

            // Add parameters with actual string values from the textboxes
            cmd.Parameters.AddWithValue("@type", cmbType.SelectedItem.ToString()); // Assuming cmbType is a ComboBox
            cmd.Parameters.AddWithValue("@name", txtName.Text);  // Accessing the Text property
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);  // Accessing the Text property
            cmd.Parameters.AddWithValue("@contact", txtContact.Text);  // Accessing the Text property
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);  // Accessing the Text property

            // Execute the query
            cmd.ExecuteNonQuery();

            // Show success message
            MessageBox.Show("Added Successfully!!!");

            // Close the connection
            con.Close();
            //DisplayData();
        }
        private void DealerCustomer_Load(object sender, EventArgs e)
        {
            //DisplayData();
        }

        private void DisplayData(string searchKeyword)
        {
            try
            {
                // Open the connection
                con.Open();

                // Check if the search keyword is an integer or a string
                bool isNumeric = int.TryParse(searchKeyword, out int numericValue);

                // Define your query based on the type of input (numeric vs. text)
                string sqlcmd;
                if (isNumeric)
                {
                    // If the searchKeyword is an integer, search by DeaCustId
                    sqlcmd = "SELECT * FROM deacust WHERE DeaCustId = @DeaCustId";
                }
                else
                {
                    // If the searchKeyword is a string, search by name or other fields
                    sqlcmd = "SELECT * FROM deacust WHERE name LIKE '%' + @searchKeyword + '%' OR contact LIKE '%' + @searchKeyword + '%'";
                }

                // Create SQL Command
                SqlCommand cmd = new SqlCommand(sqlcmd, con);

                // Add the appropriate parameter
                if (isNumeric)
                {
                    cmd.Parameters.AddWithValue("@DeaCustId", numericValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                }

                // Execute the command and fill the DataGridView
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                guna2DataGridView1.DataSource = dt;
                guna2DataGridView1.Refresh();

                // Close the connection
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message);
            }
            finally
            {
                // Ensure connection is closed in case of an error
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private DataTable GetDeaCustById(int searchId)
        {
            DataTable dt = new DataTable();

            try
            {
                // Open the connection
                con.Open();

                // Create the SQL query to fetch details based on the provided ID
                string query = "SELECT name, email, contact, address FROM deacust WHERE DeaCustId = @DeaCustId";

                // Use SqlCommand to execute the query
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DeaCustId", searchId);

                // Execute the query and fill the data table
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching details: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }

            return dt; // Return the data table containing the result
        }


    }
}

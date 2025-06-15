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

namespace Invoice_Billing_System.UI
{
    public partial class Product : Form
    {
        string query;

        public Product()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;");

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           con.Open();
            query = "INSERT INTO Products (name,category,description,rate) VALUES (@name,@category,@description,@rate) ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name",txtName.Text);
            cmd.Parameters.AddWithValue("@category",txtCategory.Text);
            cmd.Parameters.AddWithValue("@description",txtDescription.Text);
            cmd.Parameters.AddWithValue("@rate",txtRate.Text);
            cmd.ExecuteNonQuery();

            con.Close();
            BindData();
            MessageBox.Show("Products is created");
        }
        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from Products", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);    
            DataTable dt = new DataTable();
            sd.Fill(dt);
            guna2DataGridView1.DataSource = dt; 


        }

        private void Product_Load(object sender, EventArgs e)
        {
            BindData();

            guna2DataGridView1.CellClick += guna2DataGridView1_CellContentClick;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Define the connection string directly within the SqlConnection using statement
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    con.Open();

                    // Create the SQL command with parameters to prevent SQL injection
                    string query = "UPDATE Products SET name = @name, category = @category, description = @description, rate = @rate WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, con);

                    // Add parameters to SqlCommand object
                    command.Parameters.AddWithValue("@name", txtName.Text);
                    command.Parameters.AddWithValue("@category", txtCategory.Text);
                    command.Parameters.AddWithValue("@description", txtDescription.Text);
                    command.Parameters.AddWithValue("@rate", txtRate.Text);
                    command.Parameters.AddWithValue("@Id", int.Parse(txtProductID.Text));

                    // Execute the command
                    command.ExecuteNonQuery();

                    // Close the connection (optional here due to 'using' statement)
                    con.Close();

                    MessageBox.Show("Successfully Updated.");
                    BindData();
                }
                catch (Exception ex)
                {
                    // Display error message if an exception occurs
                    MessageBox.Show("Error during update: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Define the connection string directly within the SqlConnection using statement
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    con.Open();

                    // Create the SQL command with a parameter to prevent SQL injection
                    string query = "DELETE FROM Products WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, con);

                    // Add the parameter to SqlCommand object
                    command.Parameters.AddWithValue("@Id", int.Parse(txtProductID.Text));

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the delete was successful
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully Deleted.");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the given ID.");
                    }

                    BindData();
                }
                catch (Exception ex)
                {
                    // Display error message if an exception occurs
                    MessageBox.Show("Error during deletion: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    con.Close();
                }
            }
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
                string query = "SELECT name, category, rate FROM Products WHERE Id = @Id";
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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the user clicked on a valid row (not the header)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                // Assuming the DataGridView columns are as follows:
                // 0 = Product ID
                // 1 = Product Name
                // 2 = Category
                // 3 = Description
                // 4 = Rate

                // Populate the text boxes with values from the selected row
                txtProductID.Text = row.Cells["Id"].Value.ToString();   // Product ID
                txtName.Text = row.Cells["name"].Value.ToString();      // Product Name
                txtCategory.Text = row.Cells["category"].Value.ToString();  // Category
                txtDescription.Text = row.Cells["description"].Value.ToString(); // Description
                txtRate.Text = row.Cells["rate"].Value.ToString();      // Rate
            }
        }
    }
}

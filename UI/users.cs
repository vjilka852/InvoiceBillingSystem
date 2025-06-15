using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Invoice_Billing_System.UI
{
    public partial class users : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;");

        int Id = 0;
        public users()
        {
            InitializeComponent();
        }

        private void users_Load(object sender, EventArgs e)
        {
            BindData();  // Load data when form is loaded

            guna2DataGridView1.CellClick += guna2DataGridView1_CellContentClick;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Insert user record into the database
            string query = "INSERT INTO Users(first_name, last_name, email, username, password, contact, address, gender, user_type) " +
                           "VALUES(@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type)";
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@contact", txtContact.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@user_type", cmbUsertype.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
                MessageBox.Show("User created successfully!");

                // Refresh the DataGridView after adding a user
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        void BindData()
        {
            try
            {
                // Open the connection if it's not already open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Fetch all users from the Users table
                SqlCommand command = new SqlCommand("SELECT * FROM Users", con);
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);

                // Check if there are any records in the DataTable
                if (dt.Rows.Count > 0)
                {
                    // Bind the fetched data to the DataGridView
                    guna2DataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No data found.");
                    guna2DataGridView1.DataSource = null; // Reset the DataGridView if no data is present
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE Users SET first_name=@first_name,last_name=@last_name,email=@email,username=@username,password=@password,contact=@contact,address=@address,gender=@gender,user_type=@user_type WHERE Id=@Id";

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@contact", txtContact.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@user_type", cmbUsertype.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record with Id= " + Id + " is Updated!", "Update Confirmation");

                // Refresh the DataGridView after updating
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                textBox2.Text = row.Cells["Id"].Value.ToString();   // Product ID
                txtFirstName.Text = row.Cells["first_name"].Value.ToString();      // Product Name
                txtLastName.Text = row.Cells["last_name"].Value.ToString();  // Category
                txtEmail.Text = row.Cells["email"].Value.ToString(); // Description
                txtUsername.Text = row.Cells["username"].Value.ToString(); // Rate
               txtPassword.Text = row.Cells["password"].Value.ToString();
                txtContact.Text = row.Cells["contact"].Value.ToString();
               txtAddress.Text = row.Cells["address"].Value.ToString();
                cmbGender.Text = row.Cells["gender"].Value.ToString();
                cmbUsertype.Text = row.Cells["user_type"].Value.ToString();
            }
        }
    }
}

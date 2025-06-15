using Guna.UI2.WinForms;
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
using System.Xml.Linq;

namespace Invoice_Billing_System.UI
{
    public partial class Categories : Form
    {
        SqlConnection conn;
        SqlDataReader sdr;//Connected Architecture
        SqlDataAdapter adapt;//Disconnected Achitecture

        int Id = 0;
        string titel,description;
        

        string query;

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;");

        public Categories()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        } private void btnUpdate_Click(object sender, EventArgs e)
            {


            string query = "INSERT INTO Categories (title, description) VALUES (@titel, @description)";  // Corrected title column name

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@titel", txtTitel.Text);
                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("New category inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                BindData();  // Refresh DataGrid after insert
            }



        }


            private void btnAdd_Click(object sender, EventArgs e)
            {
            string query = "INSERT INTO Categories (title, description) VALUES (@titel, @description)";  // Corrected title column name

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@titel", txtTitel.Text);
                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("New category inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                BindData();  // Refresh DataGrid after insert
            }






        }
        void BindData()
            {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand command = new SqlCommand("SELECT * FROM Categories", con);
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                guna2DataGridView1.DataSource = dt;  // Assuming guna2DataGridView1 is your DataGridView control
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }


        }

            private void Categories_Load(object sender, EventArgs e)
            {

                BindData();

                guna2DataGridView1.CellClick += guna2DataGridView1_CellContentClick;

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
                textBox1.Text = row.Cells["Id"].Value.ToString();   // Product ID
                txtTitel.Text = row.Cells["titel"].Value.ToString();      // Product Name
               txtDescription.Text = row.Cells["description"].Value.ToString();  // Category
              
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }


        }
    }



  
        

  
 
      

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
   
    public partial class PurchaseAndSales : Form
    {
        string query;

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\.net projects\\Invoice Billing System\\InvoiceBillingSystem.mdf\";Integrated Security=True;Connect Timeout=30;");
        public PurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Get product details from the form
                string productName = txtName.Text;

                // Validate quantity and rate before proceeding
                decimal qty;
                decimal rate;

                if (string.IsNullOrEmpty(txtQuntity.Text) || !decimal.TryParse(txtQuntity.Text, out qty))
                {
                    MessageBox.Show("Please enter a valid quantity.");
                    return;
                }

                if (string.IsNullOrEmpty(txtRate.Text) || !decimal.TryParse(txtRate.Text, out rate))
                {
                    MessageBox.Show("Please enter a valid rate.");
                    return;
                }

                // Calculate total price
                decimal total = rate * qty;

                // Check if the product name is not empty
                if (string.IsNullOrEmpty(productName))
                {
                    // Display an error message if no product is selected
                    MessageBox.Show("Please select a product first.");
                }
                else
                {
                    // Add the product to the transaction DataTable
                    transactionDT.Rows.Add(productName, rate, qty, total);

                    // Bind the updated DataTable to the DataGridView
                    guna2DataGridView1.DataSource = transactionDT;

                    // Calculate the subtotal after adding the product
                    CalculateSubTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product to DataGridView: " + ex.Message);
            }

        }
        private void CalculateSubTotal()
        {
            decimal subTotal = 0;

            // Loop through each row of the transaction DataTable and sum the Total column
            foreach (DataRow row in transactionDT.Rows)
            {
                subTotal += Convert.ToDecimal(row["Total"]);
            }

            // Display the calculated subtotal in the txtSubTotal textbox
            txtSubTotal.Text = subTotal.ToString("0.00");
        }

        DealerCustomer dc = new DealerCustomer();
        Product product = new Product();

        DataTable transactionDT = new DataTable();

        private void PurchaseAndSales_Load(object sender, EventArgs e)
        {

            //get the transaction type value from the user dashboard
            string type = UserDasboard.transactionType;
            //set the value on lbl top
            lblTop.Text = type;

            //specify colums for our transactiondatatable
            transactionDT.Columns.Add("name");
            transactionDT.Columns.Add("rate");
            transactionDT.Columns.Add("qty");
            transactionDT.Columns.Add("Total");

           
               

                // Set the DataTable as the DataSource for the DataGridView
                guna2DataGridView1.DataSource = transactionDT;
            

        }

       

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //get the value from discount text box
            string value = txtDiscount.Text;

            if (value == "")
            {
                MessageBox.Show("Please Add Disconunt First");

            }
            else 
            {
                decimal subtotal = decimal.Parse(txtSubTotal.Text);    
                decimal discount = decimal.Parse(txtDiscount.Text);

                //calculate the grandtotal based on discount
                decimal grandTotal = ((100 - discount) / 100)* subtotal;

                //display the grand total in textbox
                txtGrandTotal.Text = grandTotal.ToString();
            }


        }

        private void txtSearch_TextChanged(object sender, EventArgs e, int DeaCustId)
        {
            
                // Check if the search textbox is not empty
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    try
                    {
                        // Convert the search text to integer (assuming it's a product ID)
                        int productId = int.Parse(txtSearch.Text);

                        // Call the method from the Product class to get the product details
                        DealerCustomer delarcustomer = new DealerCustomer();
                        DataTable dt = delarcustomer.GetDealerCustomerById(DeaCustId);

                        // Check if the product was found
                        if (dt.Rows.Count > 0)
                        {
                            // Display the deacust details in the respective textboxes
                            guna2TextBox3.Text = dt.Rows[0]["name"].ToString();
                            guna2TextBox1.Text = dt.Rows[0]["email"].ToString();
                            guna2TextBox2.Text = dt.Rows[0]["contact"].ToString();
                            guna2TextBox5.Text = dt.Rows[0]["address"].ToString();
                        }
                        else
                        {
                        // If no product was found, clear the textboxes
                        guna2TextBox3.Text = "";
                        guna2TextBox1.Text = "";
                        guna2TextBox2.Text = "";
                        guna2TextBox5.Text = "";
                        MessageBox.Show("Product not found.");
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Please enter a valid product ID.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                // If the search textbox is empty, clear the textboxes
                guna2TextBox3.Text = "";
                guna2TextBox1.Text = "";
                guna2TextBox2.Text = "";
                guna2TextBox5.Text = "";
            }
            

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            
                try
                {
                    // Get the paid amount and grand total from the textboxes
                    if (!string.IsNullOrEmpty(txtPaidAmount.Text) && !string.IsNullOrEmpty(txtGrandTotal.Text))
                    {
                        decimal paidAmount = decimal.Parse(txtPaidAmount.Text);
                        decimal grandTotal = decimal.Parse(txtGrandTotal.Text);

                        // Calculate the return amount
                        decimal returnAmount = paidAmount - grandTotal;

                        // Display the return amount in the textbox
                        txtvat.Text = returnAmount.ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error calculating return amount: " + ex.Message);
                }
            }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {
            string check = txtGrandTotal.Text;
            if (check == "")
            {
                MessageBox.Show("Caliculate the discount and set the Grand Total First");
            }
            else 
            {
                decimal PreviusGT = decimal.Parse(txtGrandTotal.Text);  
                decimal vat = decimal.Parse(txtvat.Text);
                decimal grandtotal = (100+vat)/100* PreviusGT;

                txtGrandTotal.Text = grandtotal.ToString();
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Transction transaction = new Transction();

           // transaction.type = lblTop.Text;

            string  deacustName = txtName.Text;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
                // Ensure the search textbox is not empty
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    try
                    {
                        // Convert the search text to integer (assuming it's a product ID)
                        int productId = int.Parse(txtSearch.Text);

                        // Call the method from the Product class to get the product details
                        Product product = new Product();
                        DataTable dt = product.GetProductById(productId);

                        // Check if the product was found
                        if (dt.Rows.Count > 0)
                        {
                            // Display the product details in the respective textboxes
                            txtName.Text = dt.Rows[0]["name"].ToString();
                            txtInventory.Text = dt.Rows[0]["category"].ToString();
                            txtRate.Text = dt.Rows[0]["rate"].ToString();
                        }
                        else
                        {
                            // If no product was found, clear the textboxes
                            txtName.Text = "";
                            txtInventory.Text = "";
                            txtRate.Text = "";
                            MessageBox.Show("Product not found.");
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Please enter a valid product ID.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    // If the search textbox is empty, clear the textboxes
                    txtName.Text = "";
                    txtInventory.Text = "";
                    txtRate.Text = "";
                }
            }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            // Ensure the search textbox is not empty
            if (!string.IsNullOrEmpty(guna2TextBox4.Text))
            {
                try
                {
                    // Convert the search text to integer (assuming it's a product ID)
                    int DeaCustId = int.Parse(guna2TextBox4.Text);

                    // Call the method from the Product class to get the product details
                   DealerCustomer deacust = new DealerCustomer();
                    DataTable dt = deacust.GetDealerCustomerById(DeaCustId);

                    // Check if the product was found
                    if (dt.Rows.Count > 0)
                    {
                        // Display the product details in the respective textboxes
                        guna2TextBox3.Text = dt.Rows[0]["name"].ToString();
                        guna2TextBox1.Text = dt.Rows[0]["email"].ToString();
                        guna2TextBox2.Text = dt.Rows[0]["contact"].ToString();
                        guna2TextBox5.Text = dt.Rows[0]["address"].ToString();
                    }
                    else
                    {
                        // If no product was found, clear the textboxes
                        guna2TextBox3.Text = "";
                        guna2TextBox2.Text = "";
                        guna2TextBox1.Text = "";
                        guna2TextBox5.Text = "";
                        MessageBox.Show("Product not found.");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid product ID.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                // If the search textbox is empty, clear the textboxes
                guna2TextBox3.Text = "";
                guna2TextBox2.Text = "";
                guna2TextBox1.Text = "";
                guna2TextBox5.Text = "";
            }
        }
    }
    }



    

    


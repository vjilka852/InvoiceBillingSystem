using Invoice_Billing_System.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invoice_Billing_System
{
    public partial class UserDasboard : Form
    {
        public UserDasboard()
        {
            InitializeComponent();
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DealerCustomer de = new DealerCustomer();
            de.Show();
        }

        //set a public static method specify wether the form is purchased or sales
        public static string transactionType;
        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set value on transactionType static method
            transactionType = "purchase";
            PurchaseAndSales purchase = new PurchaseAndSales(); 
            purchase.Show();
            
           
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set value on transactionType static method to selse
            transactionType = "sales";
            PurchaseAndSales sales = new PurchaseAndSales();
            sales.Show();
            

        }
    }
}

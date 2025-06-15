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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            users user = new users();
            user.Show();
        }

        private void catagoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DealerCustomer dc = new DealerCustomer();
            dc.Show();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transction transaction = new Transction();
            transaction.Show();
        }
    }
}

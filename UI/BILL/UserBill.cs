using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Billing_System.UI.BILL
{
    internal class UserBill
    {
       public int id { get; set; }
        public string first_Name { get; set; }

        public string last_Name { get; set; }

        public String email { get; set; }

        public String username { get; set; }

        public String password { get; set; }

        public String Contact { get; set; }

        public String address { get; set; }

        public String gender  { get; set; }

        public string user_type { get; set; }

        public DateTime added_date { get; set; }

        public int added_by { get; set; }

    }
}

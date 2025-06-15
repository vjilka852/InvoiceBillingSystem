/*using Invoice_Billing_System.UI.BILL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invoice_Billing_System.DAL
{
    internal class UserDal
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Select Data from Database

        public DataTable Select()
        {
            //Ststic method to connect Database
            SqlConnection conn = new SqlConnection(myconnstring);
            //To hold the data from the Database
            DataTable dt = new DataTable();
            try
            {
                //Sql Query to Get Data from Database 
                string sql = "SELECT * FROM tbl_users";
                //for Executing Command
                SqlCommand cmd = new SqlCommand(sql,conn);
                //Getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                //Fill Data in our Data Table
                adapter.Fill(dt);
            }
            
            catch (Exception ex )
            {
                //Throw message if any error occurs
                MessageBox.Show(ex.Message);
            }

            finally 
            {
                //Closing Connection
                conn.Close();

            }
            //Return the value in DataTable 
           // return dt;
            #endregion
            
            #region Insert Data in Database
            public bool Insert(UserBill u)
            {
                bool isSuccess = false;
                SqlConnection conn = new SqlConnection(myconnstring);
                try
                {
                    String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @user_type, @added_date, @added_by)";
                    SqlCommand cmd = new SqlCommand(sql,conn);

                    cmd.Parameters.AddWithValue("@first_name",u.first_name);
                    cmd.Parameters.AddWithValue("@last_name",u.last_Name);
                    cmd.Parameters.AddWithValue("@email", u.email);
                    cmd.Parameters.AddWithValue("@username", u.username);
                    cmd.Parameters.AddWithValue("@password", u.password);
                    cmd.Parameters.AddWithValue("@contact", u.Contact);
                    cmd.Parameters.AddWithValue("@address", u.address);
                    cmd.Parameters.AddWithValue("@gender", u.gender);
                    cmd.Parameters.AddWithValue("@user_type", u.user_type);
                    cmd.Parameters.AddWithValue("@added_date", u.added_date);
                    cmd.Parameters.AddWithValue("@added_by", u.added_by);
                    

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    //If the query is executed Successfully then the vlaue to rows will be genrate than 0 else it will be less than 0
                    if (rows > 0)
                    {
                        isSuccess = true;
                    }
                    else 
                    {
                        isSuccess = false;
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                    conn.Close();
                }
                return isSuccess;
            }
            #endregion
            #region update data in Database
            Public bool update(UserBill u)
            {
                bool isSuccess = false;
                SqlConnection conn = new SqlConnection(myconnstring);

                try
                {
                    string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name,email=@email, username=@username, password=@password, contact=@contact, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@first_name", u.first_name);
                    cmd.Parameters.AddWithValue("@last_name", u.last_Name);
                    cmd.Parameters.AddWithValue("@email", u.email);
                    cmd.Parameters.AddWithValue("@username", u.username);
                    cmd.Parameters.AddWithValue("@password", u.password);
                    cmd.Parameters.AddWithValue("@contact", u.Contact);
                    cmd.Parameters.AddWithValue("@address", u.address);
                    cmd.Parameters.AddWithValue("@gender", u.gender);
                    cmd.Parameters.AddWithValue("@user_type", u.user_type);
                    cmd.Parameters.AddWithValue("@added_date", u.added_date);
                    cmd.Parameters.AddWithValue("@added_by", u.added_by);
                    cmd.Parameters.AddWithValue("@id", u.id);

                    conn.Open();

                    int rows = cmd .ExecuteNonQuery();
                    if (rows > 0)
                    {
                        isSuccess = true;
                    }
                    else 
                    {
                        isSuccess = false;  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                     conn.Close();
                }
                return isSuccess;
            }
            #endregion
            #region Delete Data from Database
            Public bool Delete(UserBill u)
            {
                bool isSuccess = false;
                SqlConnection conn = new SqlConnection(myconnstring);
                try
                {
                    string sql = "DELETE FROM tbl_users WHERE id=@id";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id",u.id);

                    conn.Open ();
                    int rows = cmd .ExecuteNonQuery();
                    if (rows > 0)
                    {
                        isSuccess = true;
                    }
                    else 
                    {
                        isSuccess = false;
                    }

                }
                catch (Exception ex) 
                {
                    MessageBox.Show (ex.Message);
                }

                finally 
                {
                    conn.Close ();
                }
                                 
            }
               
            #endregion

        }
    }
}
*/
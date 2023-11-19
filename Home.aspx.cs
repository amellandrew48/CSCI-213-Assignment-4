using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MSDAssignment4
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string username = Login1.UserName;
            string password = Login1.Password;

            if (AuthenticateUser(username, password))
            {
                e.Authenticated = true;
            }
            else
            {             
                Login1.FailureText = "Invalid username/password.";
                e.Authenticated = false;
            }
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            string userType = Session["UserType"] as string;

            switch (userType)
            {
                case "Member":
                    Response.Redirect("Member.aspx");
                    break;
                case "Instructor":
                    Response.Redirect("Instructor.aspx");
                    break;
                case "Administrator":  
                    Response.Redirect("Admin.aspx");
                    break;
                default:                   
                    Response.Redirect("Home.aspx");
                    break;
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            bool authenticated = false;
            string connectionString = WebConfigurationManager.ConnectionStrings["KarateSchool_1_ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT UserID, UserType FROM NetUser WHERE UserName = @UserName AND UserPassword = @UserPassword";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@UserPassword", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                authenticated = true;
                                Session["UserID"] = reader["UserID"];
                                Session["UserType"] = reader["UserType"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    authenticated = false;
                }
            }

            return authenticated;
        }
    }
}

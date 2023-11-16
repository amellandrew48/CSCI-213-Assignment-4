using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace MSDAssignment4
{

    public partial class Admin : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["KarateSchool_1_ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMembersGrid();
                BindInstructorsGrid();
            }
        }

        private void BindMembersGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Member", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        AdminMemberView.DataSource = dt;
                        AdminMemberView.DataBind();
                    }
                }
            }
        }

        private void BindInstructorsGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Instructor", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        AdminInstructorView.DataSource = dt;
                        AdminInstructorView.DataBind();
                    }
                }
            }
        }
        protected void AddMemberButton_Click(object sender, EventArgs e)
        {
            
            int newUserId = InsertNetUserAndGetId(MemberFirstName.Text, MemberLastName.Text);
            if (newUserId > 0)
            {
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = @"INSERT INTO Member (Member_UserID, MemberFirstName, MemberLastName, MemberPhoneNumber, MemberDateJoined, MemberEmail)
                           VALUES (@UserId, @FirstName, @LastName, @Phone, @DateJoined, @Email)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", newUserId);
                        cmd.Parameters.AddWithValue("@FirstName", MemberFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", MemberLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Phone", MemberPhoneNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateJoined", DateTime.Now); 
                        cmd.Parameters.AddWithValue("@Email", $"{MemberFirstName.Text.Trim()}.{MemberLastName.Text.Trim()}@ndsu.edu".ToLower());

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                BindMembersGrid();

                MemberFirstName.Text = string.Empty;
                MemberLastName.Text = string.Empty;
                MemberPhoneNumber.Text = string.Empty;
                MemberDateJoined.Text = string.Empty;
                BindMembersGrid(); 
            }

        }
        private int InsertNetUserAndGetId(string firstName, string lastName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string username = $"{firstName}.{lastName}".ToLower();
                string password = $"{firstName}.{lastName}".ToLower();

                string sql = @"INSERT INTO NetUser (UserName, UserPassword, UserType)
                       VALUES (@UserName, @UserPassword, 'Member');
                       SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@UserPassword", password); 
                    cmd.Parameters.AddWithValue("@UserType", "Member");

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    return (result != null) ? Convert.ToInt32(result) : -1;
                }
            }
        }
    } }
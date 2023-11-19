using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

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
                BindSectionsDropDown();
            }
        }

        private void BindSectionsDropDown()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT SectionID, SectionName FROM Section", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SectionDropDownList.DataSource = reader;
                        SectionDropDownList.DataValueField = "SectionID";
                        SectionDropDownList.DataTextField = "SectionName";
                        SectionDropDownList.DataBind();
                    }
                }
            }            
            SectionDropDownList.Items.Insert(0, new ListItem("--Select Section--", "0"));
        }

        protected void AssignMembersButton_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedMemberUserID"] != null && SectionDropDownList.SelectedValue != "0")
            {
                int memberId = (int)ViewState["SelectedMemberUserID"];
                int sectionId = Convert.ToInt32(SectionDropDownList.SelectedValue);
                AssignMemberToSection(memberId, sectionId);
            }
            else
            {
                string script = "alert('Please select a member and a section.');";
                ShowMessage(script);
            }
        }

        private void AssignMemberToSection(int memberId, int sectionId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
               
                string sql = @"UPDATE Section SET Member_ID = @MemberId WHERE SectionID = @SectionId";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@MemberId", memberId);
                    cmd.Parameters.AddWithValue("@SectionId", sectionId);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();


                    if (rowsAffected == 0)
                    {
                        
                        ShowMessage("The section could not be found or no changes were made.");
                    }
                    else
                    {
                        
                        ShowMessage("Member has been successfully assigned to the section.");
                    }
                }
            }
        }

        private void ShowMessage(string message)
        {
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "popupMessage", script, true);
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
            
            int newUserId = InsertNetUserAndGetId(MemberFirstName.Text, MemberLastName.Text, "Member");
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
        private int InsertNetUserAndGetId(string firstName, string lastName, string userType)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string baseUsername = $"{firstName}.{lastName}".ToLower();
                string username = baseUsername;
                int userNumber = 1;

                //checking the username 
                string checkUserSql = @"SELECT COUNT(*) FROM NetUser WHERE UserName = @UserName";
                using (SqlCommand checkCmd = new SqlCommand(checkUserSql, con))
                {
                    con.Open();
                    checkCmd.Parameters.AddWithValue("@UserName", username);
                    while ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        //adding a digit if username already exists
                        username = $"{baseUsername}{userNumber++}";
                        checkCmd.Parameters["@UserName"].Value = username;
                    }
                }

                //adding the user
                string password = baseUsername; 
                string sql = @"INSERT INTO NetUser (UserName, UserPassword, UserType)
                       VALUES (@UserName, @UserPassword, @UserType);
                       SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@UserPassword", password);
                    cmd.Parameters.AddWithValue("@UserType", userType);

                    object result = cmd.ExecuteScalar();
                    return (result != null) ? Convert.ToInt32(result) : -1;
                }
            }
        }


        protected void AdminMemberView_SelectedIndexChanged(object sender, EventArgs e)
        {            
            GridViewRow row = AdminMemberView.SelectedRow;          
            int memberId = (int)AdminMemberView.DataKeys[row.RowIndex].Value;            
            ViewState["SelectedMemberUserID"] = memberId;
        }

        protected void AdminInstructorView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = AdminInstructorView.SelectedRow;
            int instructorId = (int)AdminInstructorView.DataKeys[row.RowIndex].Value;
            ViewState["SelectedInstructorID"] = instructorId;
        }

        protected void DeleteMemberButton_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedMemberUserID"] != null)
            {               
                int memberUserId = (int)ViewState["SelectedMemberUserID"];
                DeleteMember(memberUserId);
                BindMembersGrid();
            }
            else
            {                
                string script = "alert('Please select a member first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "popupMessage", script, true);
            }
        }

        protected void DeleteInstructorButton_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedInstructorID"] != null)
            {
                int InstructorID = (int)ViewState["SelectedInstructorID"];
                DeleteInstructor(InstructorID);
                BindInstructorsGrid();
            }
            else
            {
                string script = "alert('Please select a instructor first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "popupMessage", script, true);
            }
        }

        private void DeleteMember(int memberUserId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {                
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {                    
                    string deleteMemberSql = @"DELETE FROM Member WHERE Member_UserID = @MemberUserId";
                    using (SqlCommand cmd = new SqlCommand(deleteMemberSql, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MemberUserId", memberUserId);
                        cmd.ExecuteNonQuery();
                    }
                    
                    string deleteNetUserSql = @"DELETE FROM NetUser WHERE UserID = @UserId";
                    using (SqlCommand cmd = new SqlCommand(deleteNetUserSql, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserId", memberUserId);
                        cmd.ExecuteNonQuery();
                    }
                    
                    transaction.Commit();
                }
                catch
                {                    
                    transaction.Rollback();
                    throw; 
                }
            }
        }


        private void DeleteInstructor(int instructorId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {                
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {                    
                    string deleteInstructorSql = @"DELETE FROM Instructor WHERE InstructorID = @InstructorID";
                    using (SqlCommand cmd = new SqlCommand(deleteInstructorSql, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@InstructorID", instructorId);
                        cmd.ExecuteNonQuery();
                    }
                    
                    string deleteNetUserSql = @"DELETE FROM NetUser WHERE UserID = @UserId";
                    using (SqlCommand cmd = new SqlCommand(deleteNetUserSql, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserId", instructorId);
                        cmd.ExecuteNonQuery();
                    }                    
                    transaction.Commit();
                }
                catch
                {                   
                    transaction.Rollback();
                    throw; 
                }
            }
        }

        protected void AddInstructorButton_Click(object sender, EventArgs e)
        {
            
            int newUserId = InsertNetUserAndGetId(InstructorFirstName.Text, InstructorLastName.Text, "Instructor"); 
            if (newUserId > 0)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = @"INSERT INTO Instructor (InstructorID, InstructorFirstName, InstructorLastName, InstructorPhoneNumber)
                           VALUES (@UserId, @FirstName, @LastName, @Phone)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", newUserId);
                        cmd.Parameters.AddWithValue("@FirstName", InstructorFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", InstructorLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Phone", 0000000); //add TextBox ont he frontend for phone number InstructorPhoneNumber.Text.Trim()

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                BindInstructorsGrid();
                
                InstructorFirstName.Text = string.Empty;
                InstructorLastName.Text = string.Empty;
                //InstructorPhoneNumber.Text = string.Empty;  <<<<< remember to fix this on the frontend
            }
        }
    }
}
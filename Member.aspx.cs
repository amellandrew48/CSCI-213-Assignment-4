using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace MSDAssignment4
{
    public partial class Member : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["KarateSchool_1_ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                int memberId = GetLoggedInMemberId();
                BindMemberName(memberId);
                BindSectionGrid(memberId);
            }
        }

        private void BindMemberName(int memberId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT MemberFirstName, MemberLastName FROM Member WHERE Member_UserID = @MemberId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@MemberId", memberId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {                            
                            MemberFirstNameLabel.Text = reader["MemberFirstName"].ToString();
                            MemberLastNameLabel.Text = reader["MemberLastName"].ToString();
                        }
                    }
                }
            }
        }

        private void BindSectionGrid(int memberId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string sql = @"SELECT s.SectionID,s.SectionName, s.SectionStartDate, s.Member_ID, s.Instructor_ID, s.SectionFee
                               FROM Section s
                               INNER JOIN Section sec ON s.SectionID = sec.SectionID
                               WHERE sec.Member_ID = @MemberId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@MemberId", memberId);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        SectionGridView.DataSource = dt;
                        SectionGridView.DataBind();
                    }
                }
            }
        }

        private int GetLoggedInMemberId()
        {       
            return Convert.ToInt32(Session["UserId"]);
            
        }
    }
}

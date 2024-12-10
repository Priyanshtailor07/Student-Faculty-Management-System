using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace StudentFacultyManagementSystem
{
    public partial class FacultyLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('Please enter email and password.');</script>");
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM Faculty WHERE Email = @Email AND Password = @Password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Session["FacultyID"] = reader["FacultyID"].ToString();
                    Session["FacultyName"] = reader["FirstName"].ToString();
                    Response.Redirect("FacultyHome.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Invalid email or password.');</script>");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentFacultyManagementSystem
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate email and password input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            // Retrieve connection string from web.config
            string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                lblMessage.Text = "Database connection is not configured properly. Please contact the administrator.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AdminID FROM Admins WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    object adminId = cmd.ExecuteScalar();

                    if (adminId != null)
                    {
                        // Set session and redirect to AdminHome (which uses AdminMasterPage.Master)
                        Session["AdminID"] = adminId.ToString();
                        Response.Redirect("~/AdminHome.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email or password.";
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception if needed
                    lblMessage.Text = "An error occurred while logging in. Please try again later.";
                }
            }
        }
    }
}

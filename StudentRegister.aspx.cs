using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentFacultyManagementSystem
{
    public partial class StudentRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string department = txtDepartment.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('Please fill all the required fields.');</script>");
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Students (FirstName, LastName, Email, Password, PhoneNumber, Department, EnrollmentDate) " +
                               "VALUES (@FirstName, @LastName, @Email, @Password, @PhoneNumber, @Department, @EnrollmentDate)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    Response.Write("<script>alert('Registration successful. Please log in.');</script>");
                    Response.Redirect("StudentLogin.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Registration failed. Please try again.');</script>");
                }
            }
        }
    }
}

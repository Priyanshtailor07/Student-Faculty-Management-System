using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace StudentFacultyManagementSystem
{
    public partial class AdminFaculty : Page
    {
        private string connectionString = @"Server=DESKTOP-5IDJ2J4\SQLEXPRESS;Database=SFISDB;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFacultyData();
            }
        }

        private void LoadFacultyData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FacultyID, FirstName, LastName, Email, PhoneNumber, Department FROM Faculty WHERE IsActive = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvFaculty.DataSource = dt;
                gvFaculty.DataBind();
            }
        }

        protected void gvFaculty_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int facultyId = Convert.ToInt32(gvFaculty.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Faculty SET IsActive = 0 WHERE FacultyID = @FacultyID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FacultyID", facultyId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Faculty deleted successfully.";
                    lblMessage.CssClass = "text-success";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                }
            }

            // Reload the GridView
            LoadFacultyData();
        }

        protected void btnAddFaculty_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string department = txtDepartment.Text.Trim();
            string joiningDate = txtJoiningDate.Text.Trim();

            // Check if all fields are filled
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(phoneNumber) ||
                string.IsNullOrEmpty(department) || string.IsNullOrEmpty(joiningDate))
            {
                lblMessage.Text = "All fields are required.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                lblMessage.Text = "Password and Confirm Password do not match.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            // Check if password is at least 8 characters long
            if (password.Length < 8)
            {
                lblMessage.Text = "Password must be at least 8 characters long.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Faculty (FirstName, LastName, Email, Password, PhoneNumber, Department, JoiningDate, CreatedAt, UpdatedAt) " +
                               "VALUES (@FirstName, @LastName, @Email, @Password, @PhoneNumber, @Department, @JoiningDate, GETDATE(), GETDATE())";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@JoiningDate", DateTime.Parse(joiningDate));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Faculty added successfully.";
                    lblMessage.CssClass = "text-success";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                }
            }

            // Clear input fields
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            txtJoiningDate.Text = string.Empty;

            // Reload the GridView
            LoadFacultyData();
        }
    }
}

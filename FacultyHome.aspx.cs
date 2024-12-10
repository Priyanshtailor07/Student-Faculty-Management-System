using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StudentFacultyManagementSystem
{
    public partial class FacultyHome : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCreatedCourses();
            }
        }

        // Method to load the courses created by the logged-in faculty
        private void LoadCreatedCourses()
        {
            try
            {
                int facultyId = Convert.ToInt32(Session["FacultyID"]); // Assuming FacultyID is stored in session

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT CourseName, Department, CreatedAt FROM Courses WHERE FacultyID = @FacultyID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FacultyID", facultyId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvCreatedCourses.DataSource = dt;
                        gvCreatedCourses.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading courses: " + ex.Message;
            }
        }

        // Event handler for creating a course
        protected void btnCreateCourse_Click(object sender, EventArgs e)
        {
            string courseName = txtCourseName.Text.Trim();
            string department = txtDepartment.Text.Trim();

            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(department))
            {
                lblMessage.Text = "Please provide both course name and department.";
                return;
            }

            try
            {
                int facultyId = Convert.ToInt32(Session["FacultyID"]); // Assuming FacultyID is stored in session

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Courses (CourseName, Department, FacultyID) VALUES (@CourseName, @Department, @FacultyID)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CourseName", courseName);
                        cmd.Parameters.AddWithValue("@Department", department);
                        cmd.Parameters.AddWithValue("@FacultyID", facultyId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "Course created successfully!";
                txtCourseName.Text = "";
                txtDepartment.Text = "";

                // Reload the courses after a new one is created
                LoadCreatedCourses();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error creating course: " + ex.Message;
            }
        }

        // Event handler for viewing enrolled students of a specific course
        protected void btnSearchEnrolledStudents_Click(object sender, EventArgs e)
        {
            string courseName = txtCourseSearch.Text.Trim();

            if (string.IsNullOrEmpty(courseName))
            {
                lblMessage.Text = "Please enter a course name to search for enrolled students.";
                return;
            }

            try
            {
                int facultyId = Convert.ToInt32(Session["FacultyID"]); // Assuming FacultyID is stored in session

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT s.FirstName, s.LastName, s.Email, sc.EnrollmentDate 
                        FROM Students s
                        JOIN StudentCourses sc ON s.StudentID = sc.StudentID
                        JOIN Courses c ON sc.CourseID = c.CourseID
                        WHERE c.CourseName = @CourseName AND c.FacultyID = @FacultyID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CourseName", courseName);
                        cmd.Parameters.AddWithValue("@FacultyID", facultyId);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvEnrolledStudents.DataSource = dt;
                        gvEnrolledStudents.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching enrolled students: " + ex.Message;
            }
        }
    }
}


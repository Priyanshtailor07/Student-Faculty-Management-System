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
    public partial class StudentHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourses();
            }
        }

        private void LoadCourses()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT CourseID, CourseName, Department
                    FROM Courses
                    WHERE Department = (
                        SELECT Department FROM Students WHERE StudentID = @StudentID
                    )";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", Session["StudentID"]); // Ensure StudentID is stored in session.

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable courses = new DataTable();
                adapter.Fill(courses);

                gvCourses.DataSource = courses;
                gvCourses.DataBind();
            }
        }

        protected void EnrollCourse(object sender, EventArgs e)
        {
            string courseId = ((System.Web.UI.WebControls.Button)sender).CommandArgument;
            string studentId = Session["StudentID"].ToString(); // Ensure StudentID is in session.

            string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM StudentCourses WHERE StudentID = @StudentID AND CourseID = @CourseID";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@StudentID", studentId);
                checkCommand.Parameters.AddWithValue("@CourseID", courseId);

                connection.Open();
                int count = (int)checkCommand.ExecuteScalar();

                if (count == 0)
                {
                    string enrollQuery = "INSERT INTO StudentCourses (StudentID, CourseID) VALUES (@StudentID, @CourseID)";
                    SqlCommand enrollCommand = new SqlCommand(enrollQuery, connection);
                    enrollCommand.Parameters.AddWithValue("@StudentID", studentId);
                    enrollCommand.Parameters.AddWithValue("@CourseID", courseId);
                    enrollCommand.ExecuteNonQuery();

                    Response.Write("<script>alert('You have successfully enrolled in the course!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('You are already enrolled in this course!');</script>");
                }
                connection.Close();
            }
        }
    }
}

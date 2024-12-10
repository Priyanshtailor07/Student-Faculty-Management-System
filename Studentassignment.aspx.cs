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
    public partial class Studentassignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEnrolledCourses();
            }
        }

        private void LoadEnrolledCourses()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT c.CourseID, c.CourseName, c.Department
                    FROM Courses c
                    JOIN StudentCourses sc ON c.CourseID = sc.CourseID
                    WHERE sc.StudentID = @StudentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", Session["StudentID"]);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable courses = new DataTable();
                adapter.Fill(courses);

                gvCourses.DataSource = courses;
                gvCourses.DataBind();
            }
        }

        protected void ViewQuestions(object sender, EventArgs e)
        {
            string courseId = ((System.Web.UI.WebControls.Button)sender).CommandArgument;
            string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Fetch course name to display
                string courseNameQuery = "SELECT CourseName FROM Courses WHERE CourseID = @CourseID";
                SqlCommand courseNameCommand = new SqlCommand(courseNameQuery, connection);
                courseNameCommand.Parameters.AddWithValue("@CourseID", courseId);

                connection.Open();
                string courseName = courseNameCommand.ExecuteScalar().ToString();
                lblCourseName.Text = courseName;

                // Fetch questions for the course
                string questionsQuery = @"
    SELECT q.QuestionText, f.FirstName + ' ' + f.LastName AS FacultyName
    FROM Questions q
    JOIN Courses c ON q.CourseID = c.CourseID
    JOIN Faculty f ON c.FacultyID = f.FacultyID
    WHERE c.CourseID = @CourseID";

                SqlCommand questionsCommand = new SqlCommand(questionsQuery, connection);
                questionsCommand.Parameters.AddWithValue("@CourseID", courseId);

                SqlDataAdapter adapter = new SqlDataAdapter(questionsCommand);
                DataTable questions = new DataTable();
                adapter.Fill(questions);

                gvQuestions.DataSource = questions;
                gvQuestions.DataBind();

                connection.Close();
            }

            questionsSection.Visible = true; // Display the questions section
        }
    }
}

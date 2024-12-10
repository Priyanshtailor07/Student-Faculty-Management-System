using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentFacultyManagementSystem
{
    public partial class AdminStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindStudentsGrid();
            }
        }

        private void BindStudentsGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                s.StudentID, 
                CONCAT(s.FirstName, ' ', s.LastName) AS FullName,
                s.Department, 
                s.Email, 
                c.CourseName
            FROM Students s
            LEFT JOIN StudentCourses sc ON s.StudentID = sc.StudentID
            LEFT JOIN Courses c ON sc.CourseID = c.CourseID";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvStudents.DataSource = dt;
                    gvStudents.DataBind();
                }
            }
        }

        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteStudent")
            {
                if (e.CommandArgument != null)
                {
                    int studentID = Convert.ToInt32(e.CommandArgument);
                    string connectionString = WebConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;

                    if (connectionString == null)
                    {
                        throw new Exception("Connection string is not defined in web.config");
                    }

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        // First, delete the student from the StudentCourses table
                        string deleteCoursesQuery = "DELETE FROM StudentCourses WHERE StudentID = @StudentID";
                        using (SqlCommand cmd = new SqlCommand(deleteCoursesQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", studentID);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }

                        // Now, delete the student from the Students table
                        string deleteStudentQuery = "DELETE FROM Students WHERE StudentID = @StudentID";
                        using (SqlCommand cmd = new SqlCommand(deleteStudentQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", studentID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Rebind the GridView to reflect changes
                    BindStudentsGrid();
                }
            }
        }

    }
}

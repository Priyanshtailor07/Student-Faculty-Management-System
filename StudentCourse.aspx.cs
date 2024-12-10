using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.Data;
using System.Data.SqlClient;

namespace StudentFacultyManagementSystem
{
    public partial class Studentcourse : System.Web.UI.Page
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
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ToString();
            string studentID = Session["StudentID"].ToString(); // Assuming the student ID is stored in Session after login

            string query = @"
                SELECT c.CourseName, f.FirstName + ' ' + f.LastName AS FacultyName
                FROM StudentCourses sc
                JOIN Courses c ON sc.CourseID = c.CourseID
                JOIN Faculty f ON c.FacultyID = f.FacultyID
                WHERE sc.StudentID = @StudentID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@StudentID", studentID);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvCourses.DataSource = dt;
                gvCourses.DataBind();
            }
        }

        protected void gvCourses_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                // Custom logic for each row if needed (e.g., formatting)
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;



using System.Configuration; // For accessing connection strings

namespace StudentFacultyManagementSystem
{
    public partial class Courses : System.Web.UI.Page
    {
        // Retrieve the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["StudentFacultyDatabase"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourses();
            }
        }

        private void BindCourses()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CourseID, CourseName FROM Courses WHERE FacultyID = @FacultyID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FacultyID", Session["FacultyID"]); // Assuming FacultyID is stored in the session
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlCourses.DataSource = reader;
                ddlCourses.DataTextField = "CourseName";
                ddlCourses.DataValueField = "CourseID";
                ddlCourses.DataBind();
                ddlCourses.Items.Insert(0, new ListItem("--Select a Course--", "0"));
            }
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourses.SelectedValue != "0")
            {
                BindQuestions();
            }
        }

        private void BindQuestions()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT QuestionID, QuestionText FROM Questions WHERE CourseID = @CourseID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", ddlCourses.SelectedValue);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvQuestions.DataSource = dt;
                gvQuestions.DataBind();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ddlCourses.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                lblError.Text = "Please select a course and enter a question.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Questions (CourseID, QuestionText) VALUES (@CourseID, @QuestionText)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", ddlCourses.SelectedValue);
                cmd.Parameters.AddWithValue("@QuestionText", txtQuestion.Text.Trim());
                conn.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = "Question uploaded successfully.";
                txtQuestion.Text = string.Empty;
                BindQuestions();
            }
        }

        protected void gvQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int questionID = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Questions WHERE QuestionID = @QuestionID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@QuestionID", questionID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Question deleted successfully.";
                    BindQuestions();
                }
            }
        }
    }
}

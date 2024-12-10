<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterPage.Master" AutoEventWireup="true" CodeBehind="Studentassignment.aspx.cs" Inherits="StudentFacultyManagementSystem.Studentassignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .course-container {
            margin: 20px auto;
            width: 80%;
        }
        .course-table {
            width: 100%;
            border-collapse: collapse;
        }
        .course-table th, .course-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: center;
        }
        .course-table th {
            background-color: #f4f4f4;
        }
        .question-container {
            margin: 20px;
        }
        .question-table {
            width: 100%;
            border-collapse: collapse;
        }
        .question-table th, .question-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .question-table th {
            background-color: #f4f4f4;
        }
        .faculty-info {
            font-style: italic;
            margin-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="course-container">
        <h2>Your Enrolled Courses</h2>
        <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" CssClass="course-table">
            <Columns>
                <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                <asp:BoundField DataField="Department" HeaderText="Department" />
                <asp:TemplateField HeaderText="View Questions">
                    <ItemTemplate>
                        <asp:Button ID="btnViewQuestions" runat="server" Text="View Questions"
                            CommandArgument='<%# Eval("CourseID") %>' OnClick="ViewQuestions" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="question-container" runat="server" id="questionsSection" visible="false">
        <h3>Questions for Course: <asp:Label ID="lblCourseName" runat="server"></asp:Label></h3>
        <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" CssClass="question-table">
            <Columns>
                <asp:BoundField DataField="QuestionText" HeaderText="Question" />
                <asp:BoundField DataField="FacultyName" HeaderText="Faculty" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

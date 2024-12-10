<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterPage.Master" AutoEventWireup="true" CodeBehind="StudentHome.aspx.cs" Inherits="StudentFacultyManagementSystem.StudentHome" %>

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
        .btn-enroll {
            padding: 5px 10px;
            background-color: #28a745;
            color: white;
            border: none;
            cursor: pointer;
        }
        .btn-enroll:hover {
            background-color: #218838;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="course-container">
        <h2>Your Department's Courses</h2>
        <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" CssClass="course-table">
            <Columns>
                <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                <asp:BoundField DataField="Department" HeaderText="Department" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEnroll" runat="server" Text="Enroll" CssClass="btn-enroll"
                            CommandArgument='<%# Eval("CourseID") %>' OnClick="EnrollCourse" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

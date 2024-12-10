<%@ Page Title="Enrolled Courses" Language="C#" MasterPageFile="~/StudentMasterPage.Master" AutoEventWireup="true" CodeBehind="StudentCourse.aspx.cs" Inherits="StudentFacultyManagementSystem.Studentcourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .course-table {
            width: 100%;
            border-collapse: collapse;
        }

        .course-table th, .course-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        .course-table th {
            background-color: #f2f2f2;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Enrolled Courses</h2>
    <asp:GridView ID="gvCourses" runat="server" CssClass="course-table" AutoGenerateColumns="False" 
                  OnRowDataBound="gvCourses_RowDataBound">
        <Columns>
            <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
            <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" SortExpression="FacultyName" />
        </Columns>
    </asp:GridView>
</asp:Content>

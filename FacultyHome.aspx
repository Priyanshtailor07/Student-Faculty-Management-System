<%@ Page Title="" Language="C#" MasterPageFile="~/FacultyDashboard.Master" AutoEventWireup="true" CodeBehind="FacultyHome.aspx.cs" Inherits="StudentFacultyManagementSystem.FacultyHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- View All Created Courses Section -->
    <h2>Your Created Courses</h2>
    <asp:GridView ID="gvCreatedCourses" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
            <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
            <asp:BoundField DataField="CreatedAt" HeaderText="Created At" SortExpression="CreatedAt" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
    </asp:GridView>

    <br /><br />

    <!-- Course Creation Section -->
    <h2>Create New Course</h2>
    <asp:TextBox ID="txtCourseName" runat="server" placeholder="Enter Course Name" Width="200px"></asp:TextBox>
    <asp:TextBox ID="txtDepartment" runat="server" placeholder="Enter Department" Width="200px"></asp:TextBox>
    
    <asp:Button ID="btnCreateCourse" runat="server" Text="Create Course" OnClick="btnCreateCourse_Click" />
    <br /><br />
    
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>

    <br /><br />

    <!-- View Enrolled Students Section -->
    <h2>View Enrolled Students</h2>
    <asp:TextBox ID="txtCourseSearch" runat="server" placeholder="Enter Course Name to see Enrolled Students" Width="200px"></asp:TextBox>
    <asp:Button ID="btnSearchEnrolledStudents" runat="server" Text="View Enrolled Students" OnClick="btnSearchEnrolledStudents_Click" />
    
    <br /><br />
    <asp:GridView ID="gvEnrolledStudents" runat="server" AutoGenerateColumns="true"></asp:GridView>
</asp:Content>

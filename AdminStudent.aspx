<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminStudent.aspx.cs" Inherits="StudentFacultyManagementSystem.AdminStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-danger {
            background-color: #ff4d4d;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 5px;
        }
        .btn-danger:hover {
            background-color: #ff1a1a;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Student Details</h2>
    <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" OnRowCommand="gvStudents_RowCommand" CssClass="table table-striped">
    <Columns>
        <asp:BoundField DataField="StudentID" HeaderText="Student ID" />
        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
        <asp:BoundField DataField="Department" HeaderText="Department" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="CourseName" HeaderText="Course Enrolled" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteStudent" CommandArgument='<%# Eval("StudentID") %>' CssClass="btn btn-danger" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>

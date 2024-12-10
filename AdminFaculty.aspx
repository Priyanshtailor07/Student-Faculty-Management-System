<%@ Page Title="Admin Faculty Management" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminFaculty.aspx.cs" Inherits="StudentFacultyManagementSystem.AdminFaculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin Faculty Management</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">Faculty Management</h2>

        <!-- Table to Display Faculty Details -->
        <div class="mt-4">
            <asp:GridView 
                ID="gvFaculty" 
                runat="server" 
                CssClass="table table-striped table-bordered" 
                AutoGenerateColumns="False" 
                DataKeyNames="FacultyID"
                OnRowDeleting="gvFaculty_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="FacultyID" HeaderText="ID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                    <asp:BoundField DataField="Department" HeaderText="Department" />
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Actions" />
                </Columns>
            </asp:GridView>
        </div>

        <!-- Form to Add New Faculty -->
        <div class="mt-4">
            <h4>Add New Faculty</h4>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            <div class="mb-3">
                <label for="txtFirstName" class="form-label">First Name</label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtLastName" class="form-label">Last Name</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtPassword" class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtConfirmPassword" class="form-label">Confirm Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtPhoneNumber" class="form-label">Phone Number</label>
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtDepartment" class="form-label">Department</label>
                <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtJoiningDate" class="form-label">Joining Date</label>
                <asp:TextBox ID="txtJoiningDate" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="d-grid">
                <asp:Button ID="btnAddFaculty" runat="server" CssClass="btn btn-success" Text="Add Faculty" OnClick="btnAddFaculty_Click" />
            </div>
        </div>
    </div>
</asp:Content>

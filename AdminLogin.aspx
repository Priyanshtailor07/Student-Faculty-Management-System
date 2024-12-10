<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="StudentFacultyManagementSystem.AdminLogin" %>

<!DOCTYPE html>
<html>
<head>
    <title>Admin Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header text-center">
                        <h3>Admin Login</h3>
                    </div>
                    <div class="card-body">
                        <form id="LoginForm" runat="server">
                            <div class="mb-3">
                                <label for="txtEmail" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" />
                            </div>
                            <div class="mb-3">
                                <label for="txtPassword" class="form-label">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter Password" />
                            </div>
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                            <div class="d-grid">
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLogin_Click" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
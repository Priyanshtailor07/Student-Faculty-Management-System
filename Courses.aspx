    <%@ Page Title="" Language="C#" MasterPageFile="~/FacultyDashboard.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="StudentFacultyManagementSystem.Courses" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Manage Course Questions</h2>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

    <asp:Panel ID="pnlCourses" runat="server">
        <h3>Select a Course</h3>
        <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
        </asp:DropDownList>
        <br /><br />

        <h3>Upload a Question</h3>
        <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" Rows="5" Width="400px" placeholder="Enter your question here..."></asp:TextBox>
        <br /><br />
        <asp:Button ID="btnUpload" runat="server" Text="Upload Question" OnClick="btnUpload_Click" CssClass="btn btn-primary" />
    </asp:Panel>

    <br />
    <h3>Questions for Selected Course</h3>
    <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="QuestionID" HeaderText="Question ID" />
            <asp:BoundField DataField="QuestionText" HeaderText="Question" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("QuestionID") %>' CssClass="btn btn-danger" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

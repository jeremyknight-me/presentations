<%@ Page Title="Person Details" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true" CodeBehind="Person.aspx.cs" Inherits="WebApplication.Pages.Person" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Person Details</h1>
    
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstNameTextBox"
                CssClass="control-label required col-md-2 col-sm-3 col-xs-12" EnableViewState="False" Text="First Name" />
            <div class="col-md-4 col-sm-9 col-xs-12">
                <asp:TextBox runat="server" ID="FirstNameTextBox" CssClass="form-control" EnableViewState="False" />
                <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server"
                    ControlToValidate="FirstNameTextBox" CssClass="text-danger" ErrorMessage="First Name Required"
                    ValidationGroup="SaveGroup" Display="Dynamic" Text="First Name Required" />
            </div>
        
            <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastNameTextBox"
                CssClass="control-label required col-md-2 col-sm-3 col-xs-12" EnableViewState="False" Text="Last Name" />
            <div class="col-md-4 col-sm-9 col-xs-12">
                <asp:TextBox runat="server" ID="LastNameTextBox" CssClass="form-control" EnableViewState="False" />
                <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server"
                    ControlToValidate="LastNameTextBox" CssClass="text-danger" ErrorMessage="Last Name Required"
                    ValidationGroup="SaveGroup" Display="Dynamic" Text="Last Name Required" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="EmailTextBox"
                CssClass="control-label col-md-2 col-sm-3 col-xs-12" EnableViewState="False" Text="Email" />
            <div class="col-md-4 col-sm-9 col-xs-12">
                <asp:TextBox runat="server" ID="EmailTextBox" CssClass="form-control" EnableViewState="False" />
            </div>
            
            <asp:Label ID="PhoneLabel" runat="server" AssociatedControlID="PhoneTextBox"
                CssClass="control-label col-md-2 col-sm-3 col-xs-12" EnableViewState="False" Text="Phone" />
            <div class="col-md-4 col-sm-9 col-xs-12">
                <asp:TextBox runat="server" ID="PhoneTextBox" CssClass="form-control" EnableViewState="False" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="BirthdayLabel" runat="server" AssociatedControlID="BirthdayTextBox"
                CssClass="control-label col-md-2 col-sm-3 col-xs-12" EnableViewState="False" Text="Birthday" />
            <div class="col-md-4 col-sm-9 col-xs-12">
                <asp:TextBox runat="server" ID="BirthdayTextBox" CssClass="form-control" EnableViewState="False" />
            </div>
            
            <asp:Label ID="CompanyLabel" runat="server" AssociatedControlID="CompanyDropDownList"
                CssClass="control-label col-md-2 col-sm-3 col-xs-12" EnableViewState="False" Text="Company" />
            <div class="col-md-4 col-sm-9 col-xs-12">
                <asp:DropDownList ID="CompanyDropDownList" runat="server" CssClass="form-control" 
                    DataSourceID="CompanySqlDataSource" 
                    DataTextField="Name" 
                    DataValueField="Id" />
                <asp:SqlDataSource ID="CompanySqlDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DataStore %>" 
                    SelectCommand="SELECT [Id], [Name] FROM [Company] WHERE IsDeleted = 'False' ORDER BY [Name]" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="NotesLabel" runat="server" AssociatedControlID="NotesTextBox"
                CssClass="control-label col-md-12 col-sm-12 col-xs-12" EnableViewState="False" Text="Notes" />
            <div class="col-md-12 col-sm-12 col-xs-12">
                <asp:TextBox runat="server" ID="NotesTextBox" CssClass="form-control" EnableViewState="False" 
                    TextMode="MultiLine" Rows="6" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="SaveButton" ValidationGroup="SaveGroup" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="SaveButton_Click" />
            <asp:HyperLink ID="CloseHyperLink" runat="server" Text="Close" NavigateUrl="~/Pages/People.aspx" CssClass="btn btn-default" />
        </div>
        <div class="clearfix visible-xs-block"></div>
        <div class="col-md-1 col-md-offset-5 col-sm-2 col-sm-offset-4 col-xs-12">
            <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-danger" Text="Delete" Visible="False" 
                OnClientClick="return confirm('Are you sure you wish to delete this person?');"
                OnClick="DeleteButton_Click" />
        </div>
    </div>
</asp:Content>

<%@ Page Title="Company Details" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="WebApplication.Pages.Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Company Details</h1>
    
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label ID="NameLabel" runat="server" AssociatedControlID="NameTextBox"
                CssClass="control-label col-md-2 col-sm-4 col-xs-12" EnableViewState="False" Text="Name" />
            <div class="col-md-10 col-sm-8 col-xs-12">
                <asp:TextBox runat="server" ID="NameTextBox" CssClass="form-control" EnableViewState="False" />
                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server"
                    ControlToValidate="NameTextBox" CssClass="text-danger" ErrorMessage="Name Required"
                    ValidationGroup="SaveGroup" Display="Dynamic" Text="Name Required" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:Button ID="SaveButton" ValidationGroup="SaveGroup" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="SaveButton_Click" />
            <asp:HyperLink ID="CloseHyperLink" runat="server" Text="Close" NavigateUrl="~/Pages/Companies.aspx" CssClass="btn btn-default" />
        </div>
        <div class="clearfix visible-xs-block"></div>
        <div class="col-md-1 col-md-offset-5 col-sm-2 col-sm-offset-4 col-xs-12">
            <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-danger" Text="Delete" Visible="False" 
                OnClientClick="return confirm('Are you sure you wish to delete this company?');"
                OnClick="DeleteButton_Click" />
        </div>
    </div>
</asp:Content>

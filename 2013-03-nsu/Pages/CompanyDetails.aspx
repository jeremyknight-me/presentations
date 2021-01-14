<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyDetails.aspx.cs" Inherits="WebApplication.Pages.CompanyDetails" %>
<%@ MasterType TypeName="WebApplication.SiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2><asp:Literal ID="PageHeaderLiteral" runat="server" Text="Add Company" /></h2>

    <table>
        <tr>
            <td class="bold">Name:</td>
            <td>
                <asp:TextBox ID="CompanyNameTextBox" runat="server" />
                <span class="red">*</span>
                <asp:RequiredFieldValidator ID="CompanyNameRequiredFieldValidator" runat="server" 
                    ErrorMessage="Company name is required." CssClass="red"
                    ControlToValidate="CompanyNameTextBox" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="SaveUpdatePanel" runat="server" 
                    ChildrenAsTriggers="False" UpdateMode="Conditional" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Button ID="SaveButton" runat="server" Text="Save" 
                            CssClass="button positive" onclick="SaveButton_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="SaveButton" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:HyperLink ID="CancelHyperLink" runat="server" 
                                NavigateUrl="~/Pages/Companies.aspx" 
                                CssClass="button negative" Text="Close"
                                EnableViewState="False" />
            </td>
        </tr>
    </table>
    
</asp:Content>

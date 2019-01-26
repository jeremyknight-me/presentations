<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="WebApplication.Pages.Companies" %>
<%@ MasterType TypeName="WebApplication.SiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Company List</h2>

    <asp:HyperLink runat="server" CssClass="button positive" NavigateUrl="~/Pages/CompanyDetails.aspx" EnableViewState="False">
        <asp:Image runat="server" AlternateText="" ImageUrl="~/Images/icon-add.png" EnableViewState="False" /> Add Company
    </asp:HyperLink>
    
    <asp:ListView ID="CompanyListView" runat="server" DataKeyNames="Id" 
        onitemdeleting="CompanyListView_ItemDeleting">
        <LayoutTemplate>
            <table class="border-black lines-vertical lines-horizontal header-bg-dark-grey cells-even-down-grey">
                <tr>
                    <th></th>
                    <th>Name</th>
                </tr>
                <tr runat="server" id="itemPlaceholder">
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="control-column">
                    <asp:HyperLink ID="EditHyperLink" 
                        CssClass="icon" runat="server"
                        ToolTip="Edit" Text="Edit" 
                        ImageUrl="~/images/icon-edit.png" 
                        NavigateUrl='<%# string.Concat("~/Pages/CompanyDetails.aspx?id0=", Eval("Id")) %>' />
                    <asp:ImageButton ID="DeleteImageButton" runat="server" CssClass="icon" 
                        CommandName="Delete" Text="Delete" ToolTip="Delete" 
                        ImageUrl="~/images/icon-delete.png" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                </td>
                <td>
                    <asp:HiddenField ID="CompanyIdHiddenField" runat="server" EnableViewState="False" Value='<%# Eval("Id") %>' />
                    <asp:Literal ID="CompanyNameLiteral" runat="server" EnableViewState="False" Text='<%# Eval("Name") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="border-black lines-vertical lines-horizontal header-bg-dark-grey cells-even-down-grey">
                <tr>
                    <th></th>
                    <th>Name</th>
                </tr>
                <tr>
                    <td colspan="2">No Companies Found.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
            
    <asp:DataPager ID="CompanyListViewDataPager" runat="server" 
        PageSize="10" PagedControlID="CompanyListView">
        <Fields>
            <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
            <asp:NumericPagerField />
            <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
        </Fields>
    </asp:DataPager>

</asp:Content>

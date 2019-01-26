<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="People.aspx.cs" Inherits="WebApplication.Pages.People" %>
<%@ MasterType TypeName="WebApplication.SiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>People</h2>
    
    <asp:HyperLink ID="AddPersonHyperLink" runat="server" CssClass="button positive" NavigateUrl="~/Pages/PersonDetails.aspx" EnableViewState="False">
        <asp:Image ID="AddPersonImage" runat="server" AlternateText="" ImageUrl="~/Images/icon-add.png" EnableViewState="False" /> Add Person
    </asp:HyperLink>
       
    <asp:ListView ID="PersonListView" runat="server" DataKeyNames="Id" 
        onitemdeleting="PersonListView_ItemDeleting">
        <LayoutTemplate>
            <table class="border-black lines-vertical lines-horizontal header-bg-dark-grey cells-even-down-grey">
                <tr>
                    <th></th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Company</th>
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
                        NavigateUrl='<%# string.Concat("~/Pages/PersonDetails.aspx?id0=", Eval("Id")) %>' />
                    <asp:ImageButton ID="DeleteImageButton" runat="server" CssClass="icon" 
                        CommandName="Delete" Text="Delete" ToolTip="Delete" 
                        ImageUrl="~/images/icon-delete.png" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                </td>
                <td>
                    <asp:HiddenField ID="PersonIdHiddenField" runat="server" EnableViewState="False" Value='<%# Eval("Id") %>' />
                    <asp:Literal ID="PersonFirstNameLiteral" runat="server" EnableViewState="False" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Literal ID="PersonLastNameLiteral" runat="server" EnableViewState="False" Text='<%# Eval("LastName") %>' />
                </td>
                <td>
                    <asp:Literal ID="PersonCompanyNameLiteral" runat="server" EnableViewState="False" Text='<%# Eval("Company.Name") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="border-black lines-vertical lines-horizontal header-bg-dark-grey cells-even-down-grey">
                <tr>
                    <th></th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Company</th>
                </tr>
                <tr>
                    <td colspan="4">No Persons Found.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    
    <asp:DataPager ID="PersonListDataPager" runat="server" 
        PagedControlID="PersonListView" PageSize="10">
        <Fields>
            <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
            <asp:NumericPagerField />
            <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
        </Fields>
    </asp:DataPager>

</asp:Content>

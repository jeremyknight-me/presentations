<%@ Page Title="Companies" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="WebApplication.Pages.Companies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Companies</h1>
    
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 text-right">
            <asp:HyperLink ID="AddCompanyHyperLink" runat="server" NavigateUrl="~/Pages/Company.aspx" CssClass="btn btn-primary" EnableViewState="False">
                Add Company
            </asp:HyperLink>
        </div>
    </div>

    <div class="table-responsive">
        <asp:ListView ID="CompanyListView" runat="server" DataSourceID="CompanySqlDataSource" DataKeyNames="Id">
            <LayoutTemplate>
                <table class="table dark table-bordered table-striped width-100">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <table class="table dark table-bordered table-striped width-100">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="2">No data was found.</td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr style="">
                    <td><asp:HyperLink ID="ViewHyperLink" runat="server" Text="View" NavigateUrl='<%# string.Concat("~/Pages/Company.aspx?id0=", Eval("Id")) %>' EnableViewState="False" /></td>
                    <td><asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' EnableViewState="False" /></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>

    <asp:DataPager ID="ListViewDataPager" runat="server" PageSize="10" PagedControlID="CompanyListView">
        <Fields>
            <asp:NextPreviousPagerField ShowFirstPageButton="True" ButtonCssClass="btn btn-link" ShowNextPageButton="False" />
            <asp:NumericPagerField CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn btn-link"
                NumericButtonCssClass="btn btn-link" />
            <asp:NextPreviousPagerField ShowLastPageButton="True" ButtonCssClass="btn btn-link" ShowPreviousPageButton="False" />
        </Fields>
    </asp:DataPager>

    <asp:SqlDataSource ID="CompanySqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DataStore %>" 
        SelectCommand="SELECT * FROM [Company] WHERE IsDeleted = 'False' ORDER BY [Name]" />
</asp:Content>

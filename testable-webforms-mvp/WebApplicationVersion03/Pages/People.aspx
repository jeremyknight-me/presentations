<%@ Page Title="People" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true" CodeBehind="People.aspx.cs" Inherits="WebApplication.Pages.People" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>People</h1>
    
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 text-right">
            <asp:HyperLink ID="AddPersonHyperLink" runat="server" NavigateUrl="~/Pages/Person.aspx" CssClass="btn btn-primary" EnableViewState="False">
                Add Person
            </asp:HyperLink>
        </div>
    </div>

    <div class="table-responsive">
        <asp:ListView ID="PersonListView" runat="server" DataSourceID="PersonObjectDataSource" DataKeyNames="Id">
            <LayoutTemplate>
                <table class="table dark table-bordered table-striped width-100">
                    <thead>
                        <tr>
                            <th></th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Phone</th>
                            <th>Birthday</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <table class="table dark table-bordered table-striped width-100">
                    <thead>
                        <tr>
                            <th></th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Phone</th>
                            <th>Birthday</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="6">No data was found.</td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:HyperLink ID="ViewHyperLink" runat="server" Text="View" NavigateUrl='<%# string.Concat("~/Pages/Person.aspx?id0=", Eval("Id")) %>' EnableViewState="False" />
                    </td>
                    <td><asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' EnableViewState="False" /></td>
                    <td><asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' EnableViewState="False" /></td>
                    <td><asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("EmailAddress") %>' EnableViewState="False" /></td>
                    <td><asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("PhoneNumber") %>' EnableViewState="False" /></td>
                    <td><asp:Label ID="BirthdayLabel" runat="server" Text='<%# Eval("Birthday", "{0:MMM dd, yyyy}") %>' EnableViewState="False" /></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>

    <asp:DataPager ID="ListViewDataPager" runat="server" PageSize="10" PagedControlID="PersonListView">
        <Fields>
            <asp:NextPreviousPagerField ShowFirstPageButton="True" ButtonCssClass="btn btn-link" ShowNextPageButton="False" />
            <asp:NumericPagerField CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn btn-link"
                NumericButtonCssClass="btn btn-link" />
            <asp:NextPreviousPagerField ShowLastPageButton="True" ButtonCssClass="btn btn-link" ShowPreviousPageButton="False" />
        </Fields>
    </asp:DataPager>
   
    <asp:ObjectDataSource ID="PersonObjectDataSource" runat="server" EnablePaging="True"
        SelectMethod="ListAll" SelectCountMethod="ListAllCount"
        TypeName="ClassLibrary.Presentation.Presenters.PersonListPresenter">
        <SelectParameters>
            <asp:Parameter Name="maximumRows" Type="Int32" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

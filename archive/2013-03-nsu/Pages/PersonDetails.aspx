<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonDetails.aspx.cs" Inherits="WebApplication.Pages.PersonDetails" %>
<%@ MasterType TypeName="WebApplication.SiteMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2><asp:Literal ID="PageHeaderLiteral" runat="server" Text="Add Person" /></h2>
    
    <table>
        <tr>
            <td class="bold">Company</td>
            <td>
                <asp:DropDownList ID="CompanyDropDownList" runat="server" 
                    DataValueField="Id" DataTextField="Name" AppendDataBoundItems="True">
                    <asp:ListItem Text=" - Select Company - " Value="" />
                </asp:DropDownList>
                <span class="red">*</span>
                <asp:RequiredFieldValidator ID="CompanyRequiredFieldValidator" runat="server" 
                    ControlToValidate="CompanyDropDownList" ErrorMessage="Company is required." 
                    CssClass="red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td class="bold">First Name</td>
            <td>
                <asp:TextBox ID="FirstNameTextBox" runat="server" />
                <span class="red">*</span>
                <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" 
                    ControlToValidate="FirstNameTextBox" ErrorMessage="First name is required." 
                    CssClass="red" Display="Dynamic" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="FirstNameTextBoxWatermarkExtender" runat="server"
                    TargetControlID="FirstNameTextBox" WatermarkText="ex: Robert" WatermarkCssClass="watermark" />
                <ajaxToolkit:FilteredTextBoxExtender ID="FirstNameFilteredTextBoxExtender" runat="server" 
                    FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="FirstNameTextBox" />
            </td>
        </tr>
        <tr>
            <td class="bold">Middle Name</td>
            <td>
                <asp:TextBox ID="MiddleNameTextBox" runat="server" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="MiddleNameTextBoxWatermarkExtender" runat="server"
                    TargetControlID="MiddleNameTextBox" WatermarkText="ex: P; Paul" WatermarkCssClass="watermark" />
                <ajaxToolkit:FilteredTextBoxExtender ID="MiddleNameFilteredTextBoxExtender" runat="server" 
                    FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="MiddleNameTextBox" />
            </td>
        </tr>
        <tr>
            <td class="bold">Last Name</td>
            <td>
                <asp:TextBox ID="LastNameTextBox" runat="server" />
                <span class="red">*</span>
                <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" 
                    ControlToValidate="LastNameTextBox" ErrorMessage="Last name is required." 
                    CssClass="red" Display="Dynamic" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="LastNameTextBoxWatermarkExtender" runat="server"
                    TargetControlID="LastNameTextBox" WatermarkText="ex: Deaux" WatermarkCssClass="watermark" />
                <ajaxToolkit:FilteredTextBoxExtender ID="LastNameFilteredTextBoxExtender" runat="server" 
                    FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="LastNameTextBox" />
            </td>
        </tr>
        <tr>
            <td class="bold">Nickname</td>
            <td>
                <asp:TextBox ID="NicknameTextBox" runat="server" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="NicknameTextBoxWatermarkExtender" runat="server"
                    TargetControlID="NicknameTextBox" WatermarkText="ex: Bob" WatermarkCssClass="watermark" />
                <ajaxToolkit:FilteredTextBoxExtender ID="NicknameFilteredTextBoxExtender" runat="server" 
                    FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="NicknameTextBox" />
            </td>
        </tr>
        <tr>
            <td class="bold">Email Address</td>
            <td>
                <asp:TextBox ID="EmailTextBox" runat="server" />
                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" 
                    ErrorMessage="Must be a valid email." ControlToValidate="EmailTextBox" CssClass="red"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="EmailTextBoxWatermarkExtender" runat="server"
                    TargetControlID="EmailTextBox" WatermarkText="ex: name@domain.com" WatermarkCssClass="watermark" />
            </td>
        </tr>
        <tr>
            <td class="bold">Phone Number</td>
            <td>
                <asp:TextBox ID="PhoneNumberTextBox" runat="server" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="PhoneNumberTextBoxWatermarkExtender" runat="server"
                    TargetControlID="PhoneNumberTextBox" WatermarkText="ex: 555-321-9876" WatermarkCssClass="watermark" />
                <ajaxToolkit:MaskedEditExtender ID="PhoneNumberMaskedEditExtender" runat="server"
                    TargetControlID="PhoneNumberTextBox" MaskType="Number" Mask="999\-999\-9999" />
            </td>
        </tr>
        <tr>
            <td class="bold">Street Address</td>
            <td>
                <asp:TextBox ID="StreetAddressTextBox" runat="server" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="StreetTextBoxWatermarkExtender" runat="server"
                    TargetControlID="StreetAddressTextBox" WatermarkText="ex: 123 Some St." WatermarkCssClass="watermark" />
            </td>
        </tr>
        <tr>
            <td class="bold">City</td>
            <td>
                <asp:TextBox ID="CityTextBox" runat="server" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="CityTextBoxWatermarkExtender" runat="server"
                    TargetControlID="CityTextBox" WatermarkText="ex: New Orleans" WatermarkCssClass="watermark" />
            </td>
        </tr>
        <tr>
            <td class="bold">State</td>
            <td>
                <asp:DropDownList ID="StateDropDownList" runat="server" 
                    DataTextField="Display" DataValueField="Value" AppendDataBoundItems="True">
                    <asp:ListItem Text=" - Select State - " Value="" />
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td class="bold">Zip Code</td>
            <td>
                <asp:TextBox ID="ZipCodeTextBox" runat="server" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="ZipCodeTextBoxWatermarkExtender" runat="server"
                    TargetControlID="ZipCodeTextBox" WatermarkText="ex: 54321; 54321-9876" WatermarkCssClass="watermark" />
                
            </td>
        </tr>
        <tr>
            <td class="bold">Birthday</td>
            <td>
                <asp:TextBox ID="BirthdayTextBox" runat="server" />
                <ajaxToolkit:TextBoxWatermarkExtender ID="BirthdayTextBoxWatermarkExtender" runat="server"
                    TargetControlID="BirthdayTextBox" WatermarkText="ex: 02/28/1999" WatermarkCssClass="watermark" />
                <ajaxToolkit:MaskedEditExtender ID="BirthdayMaskedEditExtender" runat="server"
                    TargetControlID="BirthdayTextBox" MaskType="Date" Mask="99/99/9999" />
                <ajaxToolkit:CalendarExtender ID="BirthdayCalendarExtender" runat="server"
                    TargetControlID="BirthdayTextBox" Format="MM/dd/yyyy" />
                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="SaveUpdatePanel" runat="server"
                    UpdateMode="Conditional" ChildrenAsTriggers="False" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="SaveButton" Text="Save"
                            CssClass="button positive" onclick="SaveButton_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="SaveButton" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:HyperLink ID="CancelHyperLink" runat="server" 
                    NavigateUrl="~/Pages/People.aspx" 
                    CssClass="button negative" Text="Close"
                    EnableViewState="False" />
            </td>
        </tr>
    </table>

</asp:Content>

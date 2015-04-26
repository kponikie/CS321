<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="logIn">

        <table>
            <tr>
                <td style="text-align:right">
                    <asp:Label ID="lblUserName" runat="server" Text="User Name: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" SkinID="BlueTextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="!!!" ToolTip="Required Field!"
                        ControlToValidate="txtUserName">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" SkinID="BlueTextBox" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="!!!" ToolTip="Required Field!"
                        ControlToValidate="txtPassword">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr><td></td></tr>
            <tr>
                <td></td>
                <td style="text-align:left">
                    <asp:Button ID="btnLogIn" runat="server" Text="Log In" CssClass="standardButton" 
                    OnClick="btnLogIn_Click" ToolTip="Click to LogIn" />
                </td>
            </tr>
            <tr><td></td></tr>
            <tr>
                <td></td>
                <td style="font-size:small; color: red; text-align:left">
                    <asp:Label ID="lblLoginResults" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <asp:Label ID="lblResults" runat="server" Text=""></asp:Label>

</asp:Content>


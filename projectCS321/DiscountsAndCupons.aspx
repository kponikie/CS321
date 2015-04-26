<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="DiscountsAndCupons.aspx.cs" Inherits="DiscountsAndCupons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="guestMessage" runat="server" class="guestLock" visible="false">
        <h3>
            <asp:Label ID="lblCupons" runat="server" Text=""></asp:Label><br /><br />
            <asp:Image ID="imgLock" runat="server" ImageUrl="~/Images/lock.png"/>
        </h3>
    </div>

    <div id="memberMessage" runat="server" class="cuponCenter">
        <h3>  
            <asp:Label ID="lblResults" runat="server" Text=""></asp:Label>
        </h3>

        <asp:RadioButton ID="rdbNew" runat="server" Text="New" GroupName="cupons" OnCheckedChanged="rdbNew_CheckedChanged" AutoPostBack="True" />&nbsp &nbsp &nbsp
        <asp:RadioButton ID="rdbUsed" runat="server" Text="Used" GroupName="cupons" OnCheckedChanged="rdbUsed_CheckedChanged" AutoPostBack="True" />&nbsp &nbsp &nbsp
        <asp:RadioButton ID="rdbAll" runat="server" Text="All" GroupName="cupons" OnCheckedChanged="rdbAll_CheckedChanged" AutoPostBack="True" />
        <br /><br />

        <asp:Table ID="cuponTable" runat="server" class="cuponTableClass">
        </asp:Table>
        <br /><br />

   </div>
    

</asp:Content>


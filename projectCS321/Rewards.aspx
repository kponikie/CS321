<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Rewards.aspx.cs" Inherits="Rewards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="guestLock">
        <h3>
            <asp:Label ID="lblCupons" runat="server" Text=""></asp:Label><br /><br />
            <asp:Image ID="imgLock" runat="server" ImageUrl="~/Images/lock.png" Visible="false" />
        
        </h3>
    </div>

    <asp:Label ID="lblResults" runat="server" Text=""></asp:Label>

</asp:Content>


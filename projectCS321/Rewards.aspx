<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Rewards.aspx.cs" Inherits="Rewards" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
            <br /><br />
            Total Points:
            <span style="font-size:80px; color:green">
                <asp:Label ID="lblRewardsTotal" runat="server" Text=""></asp:Label>
            </span>
            <asp:Table ID="tableMeter" runat="server" CssClass="tableRewards" >

            </asp:Table>
           
        </h3>

        <div class="cuponTableClass" style="margin-left:auto; margin-right:auto">
            <asp:table ID="newCupon" runat="server"></asp:table>
        </div>

        <asp:Label ID="lblCuponResults" runat="server" Text=""></asp:Label>
            <br /><br />

   </div>

</asp:Content>


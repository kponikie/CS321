﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ForSale.aspx.cs" Inherits="ForSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="float:left; padding:20px">
        <asp:ListBox ID="lbxCars" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="lsbCars_SelectedIndexChanged" Rows="23" 
            SkinID="lbxGuide" CssClass="listBox" ViewStateMode="Enabled" ForeColor="Black"></asp:ListBox>
    </div>

    <div style="float:left; padding:20px" id="divVehicleGuide1" runat="server" visible="false">
    
    <asp:Label ID="lblMake" runat="server" Text="Make: "></asp:Label>
    <asp:TextBox ID="txtMake" runat="server" CssClass="txtBox" ReadOnly="True" 
        Font-Size="X-Large"></asp:TextBox>
    <br />
    <asp:Label ID="lblModel" runat="server" Text="Model: "></asp:Label>
    <asp:TextBox ID="txtModel" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblColor" runat="server" Text="Color: "></asp:Label>
    <asp:TextBox ID="txtColor" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblYear" runat="server" Text="Year: "></asp:Label>
    <asp:TextBox ID="txtYear" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblMilage" runat="server" Text="Milage: "></asp:Label>
    <asp:TextBox ID="txtMilage" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblTransmission" runat="server" Text="Transmission: "></asp:Label>
    <asp:TextBox ID="txtTransmission" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblDrivetrain" runat="server" Text="Drivetrain: "></asp:Label>
    <asp:TextBox ID="txtDrivetrain" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblPicture" runat="server" Text=""></asp:Label>
    <asp:Image ID="imgPicture" runat="server" width="300px" Height="200px" ToolTip=""/>
    <br />
    <asp:Label ID="lblSalePrice" runat="server" Text="Price: $"></asp:Label>
    <asp:TextBox ID="txtSalePrice" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblcarId" runat="server" Text="Vehicle Ref #: "></asp:Label>
    <asp:TextBox ID="txtcarId" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
    <asp:Label ID="lblAvailability" runat="server" Text="Available Today: "></asp:Label>
    <asp:TextBox ID="txtAvailability" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
    <br />
     
</div>

    <div style="float:left; padding:50px" id="divVehicleGuide2" runat="server" visible="true">
        <h2>Slightly used vehicles for sale.</h2>
        <asp:Label ID="lblVehTotal" runat="server" Text="Vehicle for Sale: "></asp:Label>
        <asp:TextBox ID="txtVehTotal" runat="server" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
        <br />
        <br />
        <p>For more information, select a vehicle on the left.<br />
        Call our main office for more detailes 1-800-666-6696
        </p>
    </div>

    <asp:Label runat="server" Text="" ID="lblResults"></asp:Label>

</asp:Content>


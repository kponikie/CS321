<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="VehicleGuide.aspx.cs" Inherits="VehicleGuide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="float:left; padding:20px">
    <asp:ListBox ID="lbxCars" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="lsbCars_SelectedIndexChanged" Rows="20" SkinID="lbxGuide"></asp:ListBox>
    </div>

<div style="float:left; padding:20px">
    <asp:Label ID="lblcarId" runat="server" Text="Car ID: "></asp:Label>
    <asp:Label ID="txtcarId" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblMake" runat="server" Text="Make: "></asp:Label>
    <asp:Label ID="txtMake" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblModel" runat="server" Text="Model: "></asp:Label>
    <asp:Label ID="txtModel" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblColor" runat="server" Text="Color: "></asp:Label>
    <asp:Label ID="txtColor" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblYear" runat="server" Text="Year: "></asp:Label>
    <asp:Label ID="txtYear" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblMilage" runat="server" Text="Milage: "></asp:Label>
    <asp:Label ID="txtMilage" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblTransmission" runat="server" Text="Transmission: "></asp:Label>
    <asp:Label ID="txtTransmission" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblDrivetrain" runat="server" Text="Drivetrain: "></asp:Label>
    <asp:Label ID="txtDrivetrain" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblPicture" runat="server" Text="Picture: "></asp:Label>
    <asp:Image ID="imgPicture" runat="server" />
    <br />
     <asp:Label runat="server" Text="" ID="lblResults"></asp:Label>
</div>
</asp:Content>


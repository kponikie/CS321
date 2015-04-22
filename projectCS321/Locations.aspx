<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Locations.aspx.cs" Inherits="Locations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br /><br />
    <h2 class="textAlignCenter">Luxurious Car Rental Locations</h2>

    <div id="divLocation">

        <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                
                <asp:Label ID="streetLabel" runat="server" Text='<%# Eval("street") %>' />
                <br />
                
                <asp:Label ID="cityLabel" runat="server" Text='<%# Eval("city") %>' />
                
                <asp:Label ID="stateLabel" runat="server" Text='<%# Eval("state") %>' />,
                <asp:Label ID="zip" runat="server" Text='<%# Eval("zip") %>' />
                <br />
                tel.
                <asp:Label ID="phone_numberLabel" runat="server" 
                    Text='<%# Eval("phone_number") %>' />
                <br />
            <br />
            </ItemTemplate>

        </asp:DataList>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:zzCS321_1ConnectionString %>" 
            SelectCommand="SELECT [street], [city], [zip], [state], [phone_number] FROM [locations]">
        </asp:SqlDataSource>

    </div>
    <br /><br /><br />
    <p class="textAlignCenter">All location are open Monday-Sunday 8AM - 8PM local time.</p>

</asp:Content>


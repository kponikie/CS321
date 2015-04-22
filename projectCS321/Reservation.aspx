<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Reservation.aspx.cs" Inherits="Reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="divReservation">

        <%--Pickup Location--%>
        Pickup Location<br />
        <asp:Label ID="lblLocation" runat="server" Text="Location: "></asp:Label>
        <asp:DropDownList ID="ddlLocation" runat="server" 
            OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        <br /><br />

        <div id="ReservationData" runat="server" visible="false">

            <%--Vehicle--%>
            Vehicle Selection<br />
            <asp:Label ID="lblVehicle" runat="server" Text="Vehicle: "></asp:Label>
            <asp:DropDownList ID="ddlCarList" runat="server" AutoPostBack="True" 
                OnSelectedIndexChanged="ddlCarList_SelectedIndexChanged"></asp:DropDownList>
            <br /><br />

        </div>

        <div id="ReservationData2" runat="server" visible="false">

            <%--Pickup Date--%>
            Pickup Date and Time<br />
            <asp:Label ID="lblPickupDate" runat="server" Text="Date: "></asp:Label>
            <asp:TextBox ID="txtPickupDate" runat="server" Width="80px" ReadOnly="true" ToolTip="Choose a day."></asp:TextBox>&nbsp
            <asp:ImageButton ID="btnSmallCalendar" runat="server" 
                ImageUrl="~/Images/Calendar.bmp" Width="15px" Height="15px" 
                OnClick="btnSmallCalendar_Click" />

            <br />
            <asp:Calendar ID="Calendar" runat="server" Visible="false" OnDayRender="Calendar_DayRender"
                OnSelectionChanged="Calendar_SelectionChanged"></asp:Calendar>
           

            <div id="ReservationData3" runat="server" visible="false">

                <%--Pickup Time--%>
                <asp:Label ID="lblPickupTime" runat="server" Text="Time: "></asp:Label>
                <asp:DropDownList ID="ddlPickupTime" runat="server" Width="80px" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlPickupTime_SelectedIndexChanged"></asp:DropDownList>
            
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ValidationExpression="[0-9]?[0-9]/?:[0][0]"
                ErrorMessage="* Required Field" ControlToValidate="ddlPickupTime">
                </asp:RegularExpressionValidator>

                <br /><br />

                <%--Return Date--%>
                <span style="font-size:120%">Return Date and Time</span><br />
                <asp:Label ID="lblReturnDate" runat="server" Text="Date: "></asp:Label>
                <asp:TextBox ID="txtReturnDate" runat="server" Width="80px" ReadOnly="true" ToolTip="Automatically Calculated Date"></asp:TextBox>&nbsp
                <br />
                <asp:Label ID="lblReturnTime" runat="server" Text="Time: "></asp:Label>
                <asp:TextBox ID="txtReturnTime" runat="server" Width="80px" ReadOnly="true" ToolTip="Automatically Calculated Time"></asp:TextBox>&nbsp
                <br /><br />

                <%--Email--%>
                <asp:Label ID="lblEmail" runat="server" Text="Email: " ></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" Width="200px" ToolTip="format: someone@domain.com" ></asp:TextBox>&nbsp
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="* Required Field" ControlToValidate="txtEmail" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="Invalid Email address." ForeColor="Red" ControlToValidate="txtEmail" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                    

                <br /><br />

                <%--Phone--%>
                <asp:Label ID="lblPhone" runat="server" Text="Phone: " ></asp:Label>
                <asp:TextBox ID="txtPhone" runat="server" Width="200px" ToolTip="format: 333-333-4444" ></asp:TextBox>&nbsp
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="* Required Field" ControlToValidate="txtPhone" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ErrorMessage="Invalid Phone Number." ForeColor="Red" 
                    ControlToValidate="txtPhone" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" 
                    ></asp:RegularExpressionValidator>

                    

                <br /><br /><br />

                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="26px" 
                    OnClick="btnSubmit_Click" />

            </div>


        </div>

        <asp:Label runat="server" Text="" ID="lblResults"></asp:Label>

    </div>

</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Reservation.aspx.cs" Inherits="Reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="divReservation">

        <div id="ReservationEdit" runat="server" visible="false">

            <asp:Label ID="lblReservationResults" runat="server" Text=""></asp:Label>
            <br /><br />
            
            <asp:Button ID="btnNewReservation" runat="server" Text="New" Width="160px" BackColor="LightGreen"
                OnClick="btnNewReservation_Click" />
            <br /><br />

            <asp:DropDownList ID="ddlReservationList" runat="server"  
             AutoPostBack="True" OnSelectedIndexChanged="ddlReservationList_SelectedIndexChanged"></asp:DropDownList>

            <br /><br />

            <div id="ReservationEdit2" runat="server" visible="false">

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
                <br /><br />

                <asp:Button ID="btnDelete" runat="server" Text="Cancel" BackColor="LightSalmon" OnClick="btnDelete_Click" />
                <asp:Label ID="lblCancellationFee" runat="server" Text=""></asp:Label>
                <br />
            </div>

        </div>

        <div id="ReservationData0" runat="server">
            <%--Pickup Location--%>
            Pickup Location<br />
            <asp:Label ID="lblLocation" runat="server" Text="Location: "></asp:Label>
            <asp:DropDownList ID="ddlLocation" runat="server" 
                OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <br /><br />
        </div>
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

                <%--First Name--%>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name: " ></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" Width="200px" ToolTip="First Name" ></asp:TextBox>&nbsp
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="* Required Field" ControlToValidate="txtFirstName" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <br />

                <%--Last Name--%>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name: " ></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" Width="200px" ToolTip="Last Name" ></asp:TextBox>&nbsp
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="* Required Field" ControlToValidate="txtLastName" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <br />

                <%--Email--%>
                <asp:Label ID="lblEmail" runat="server" Text="Email: " ></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" Width="200px" ToolTip="format: someone@domain.com" ></asp:TextBox>&nbsp
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="* Required Field" ControlToValidate="txtEmail" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="Invalid Email address." ForeColor="Red" ControlToValidate="txtEmail" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                <br />

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
                <br />

                <%--Cupon--%>
                <asp:Label ID="lblCupon" runat="server" Text="Cupon: " ></asp:Label>
                <asp:TextBox ID="txtCupon" runat="server" Width="200px" Enabled="false" ToolTip="Cupon number" ></asp:TextBox>&nbsp        
                <br /><br />

            <div id="ReservationData4" runat="server" visible="true">

                <%--Credit Card Number--%>
                Payment Info
                <br />

                <%--First Name--%>
                <asp:Label ID="lblCardName" runat="server" Text="Name: " ></asp:Label>
                <asp:TextBox ID="txtCardName" runat="server" Width="200px" ToolTip="Name on your Credit Card" ></asp:TextBox>&nbsp
                <asp:CheckBox ID="ckbName" runat="server" AutoPostBack="true" 
                    ToolTip="Same as Above" OnCheckedChanged="ckbName_CheckedChanged" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ErrorMessage="* Required Field" ControlToValidate="txtCardName" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <%--Card Type--%>
                <asp:RadioButton ID="rdoVisa" runat="server" Checked="true" Text="Visa"  GroupName="creditCard" />
                <asp:RadioButton ID="rdoMasterCard" runat="server" Text="Master Card"  GroupName="creditCard" />
                <br />

                 <%--Card Number--%>
                <asp:Label ID="lblCreditCard" runat="server" Text="Credit Card: " ></asp:Label>
                <asp:TextBox ID="txtCreditCard" runat="server" Width="200px" ToolTip="format: 4444-4444-4444-4444 (VISA starts with 4, MasterCard 51-55)" ></asp:TextBox>&nbsp
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ErrorMessage="* Required Field" ControlToValidate="txtCreditCard" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                    ErrorMessage="Invalid Card Number." ForeColor="Red" ControlToValidate="txtCreditCard"
                    ValidationExpression="^((4\d{3})|(5[1-5]\d{2})|(6011)|(34\d{1})|(37\d{1}))-?\d{4}-?\d{4}-?\d{4}|3[4,7][\d\s-]{15}$"    
                    ></asp:RegularExpressionValidator>
                <br />

                <%--Exp. date--%>
                <asp:Label ID="Label1" runat="server" Text="Exp: "></asp:Label>
                <asp:DropDownList ID="ddlExpYear" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlExpYear_SelectedIndexChanged">
                </asp:DropDownList>
 
                <asp:DropDownList ID="ddlExpMonth" runat="server" AutoPostBack="true" Enabled="false" >
                </asp:DropDownList>

                <br /><br />

                <%--Over 25--%>
                <asp:CheckBox ID="chkOver21" runat="server" Text="Over 25" Checked="false"/>
                <br /><br />

            </div>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="26px" 
                    OnClick="btnSubmit_Click" />
        
            </div>

        </div>

        <asp:Label runat="server" Text="" ID="lblResults"></asp:Label>
        <br />
        <asp:Label runat="server" Text="" ID="lblResults2"></asp:Label>

    </div>

</asp:Content>



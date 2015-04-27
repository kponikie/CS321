using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//addition:
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Reservation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillTimeList();
            fillExpYear();
            fillExpMonthAll();
            fillLocationList();
            if (Session["userName"] != null)
            {
                ReservationData0.Visible = false;
                ReservationEdit.Visible = true;
                chkOver21.Checked = true;
                FillReservationList();
                ddlCupon.Enabled = true;
                fillCuponList();

                if (Session["rentMe"] != null)
                {
                    if ((bool)Session["rentMe"] == true) //This selects location and vehicle automaticly with data from Vehicel Guide
                    {
                        ReservationData0.Visible = true;
                        ReservationEdit.Visible = false;
                        ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(Session["rentMeLocationId"].ToString()));
                        ReservationData.Visible = true;
                        fillCarList();
                        ddlCarList.SelectedIndex = ddlCarList.Items.IndexOf(ddlCarList.Items.FindByValue(Session["rentMeCarId"].ToString()));
                        ReservationData2.Visible = true;
                        btnSmallCalendar.Visible = false;
                        Calendar.Visible = true;
                        Session["rentMe"] = false;

                    }
                }
                
            }            
        }
    }

    public void fillTimeList()
    {
        ddlPickupTime.Items.Add("---");

        for(int i=8; i<=20; i++)
        {
            ddlPickupTime.Items.Add(i+":00");
        }    
    }

    public void resetForm()
    {
        lblResults.ForeColor = System.Drawing.Color.Black;
        txtPickupDate.Text = "";
        ddlPickupTime.SelectedIndex = 0;
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtReturnDate.Text = "";
        txtReturnTime.Text = "";
        Calendar.Visible = false;
        btnSmallCalendar.Visible = true;
    }


    protected void ddlPickupTime_SelectedIndexChanged(object sender, EventArgs e)
    {       
        txtReturnTime.Text = ddlPickupTime.SelectedValue.ToString();   
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        resetForm();
        lblResults.Text = "";
        ReservationData.Visible = false;
        ReservationData2.Visible = false;
        ReservationData3.Visible = false;

        if (ddlLocation.SelectedValue == "-1")
        {
            ReservationData2.Visible = false;
            ReservationData3.Visible = false;
            lblResults.Text = "Select a Location. ";
        }
        else
        {
            fillCarList();
        }      
    }

    private string connectionString = WebConfigurationManager.ConnectionStrings["zzCS321_1ConnectionString"].ConnectionString;


    private void fillLocationList()
    {
        ddlLocation.Items.Clear();

        ListItem newFirstItem = new ListItem();
        newFirstItem.Text = "-- PLEASE SELECT --";
        newFirstItem.Value = "-1";
        ddlLocation.Items.Add(newFirstItem);
        
        string selectSQL = "SELECT location_id, street FROM locations";
    
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text += reader["street"].ToString();
                newItem.Value = reader["location_id"].ToString();
                ddlLocation.Items.Add(newItem);
            }
            reader.Close();

        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database: ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }

    private void fillCarList()
    {
        ddlCarList.Items.Clear();
        lblResults.Text = "";

        string selectSQL = "SELECT car_id, carData.location_id, make, model FROM carData LEFT OUTER JOIN locations ON carData.location_id = locations.location_id WHERE carData.location_id = '" + ddlLocation.SelectedValue + "'";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            ListItem newFirstItem = new ListItem();
            newFirstItem.Text = "-- PLEASE SELECT --";
            newFirstItem.Value = "-1";
            ddlCarList.Items.Add(newFirstItem);

            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text += reader["make"].ToString();
                newItem.Text += " ";
                newItem.Text += reader["model"].ToString();
                newItem.Text += " # ";
                newItem.Text += reader["car_id"].ToString();
                newItem.Value = reader["car_id"].ToString();
                ddlCarList.Items.Add(newItem);
            }
            reader.Close();
            
            if (ddlCarList.Items.Count > 1)
            {
                ReservationData.Visible = true;
            }
            else
            {   
                ReservationData.Visible = false;
                lblResults.Text = "Selected location is currently out of stock. ";
                lblResults.Text += "Please check back soon.";
            }
 
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database: ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnSmallCalendar_Click(object sender, ImageClickEventArgs e)
    {
        btnSmallCalendar.Visible = false;
        Calendar.Visible = true;
    }
    protected void Calendar_SelectionChanged(object sender, EventArgs e)
    {
        txtPickupDate.Text = Calendar.SelectedDate.ToShortDateString();
        txtReturnDate.Text = Calendar.SelectedDate.AddDays(1).ToShortDateString();
        Calendar.Visible = false;
        btnSmallCalendar.Visible = true;
        ReservationData3.Visible = true;

        lblTXTRentalFee.Text = "TEST";
        findRentalPrice();
        

        if (Session["userName"] != null)
        {
            ddlExpMonth.Enabled = true;
            txtFirstName.Text = Session["firstName"].ToString();
            txtLastName.Text = Session["lastName"].ToString();
            txtPhone.Text = Session["phone"].ToString();
            txtEmail.Text = Session["email"].ToString();
            txtCardName.Text = Session["firstName"].ToString() + " " + Session["lastName"].ToString();
            ddlExpYear.SelectedValue = Session["expYear"].ToString();
            ddlExpMonth.SelectedValue = Session["expMonth"].ToString();
            txtCreditCard.Text = Session["creditCard"].ToString();

            if (Session["creditType"].ToString() == "m")
            {
                rdoMasterCard.Checked = true;
            }
            else
            {
                rdoVisa.Checked = true;
            }
        } 
    }

    protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
    {

        if (e.Day.Date.CompareTo(DateTime.Today) < 0)
        {
            e.Day.IsSelectable = false;
            e.Cell.ForeColor = System.Drawing.Color.Gray;
        }

        if (e.Day.Date.CompareTo(DateTime.Today.AddDays(60)) > 0)
        {
            e.Day.IsSelectable = false;
            e.Cell.ForeColor = System.Drawing.Color.Gray;
        }

        if (e.Day.Date.CompareTo(DateTime.Today) >= 0)
        {
            e.Cell.BackColor = System.Drawing.Color.LightGreen;
        }

        if (e.Day.Date.CompareTo(DateTime.Today) == 0)
        {
            e.Cell.ForeColor = System.Drawing.Color.Blue;
            //e.Cell.Controls.Add(new LiteralControl("<span font-size='8px'><br/> Today</span>"));
        }

        string selectSQL = "SELECT pickup_date FROM reservationForm WHERE status = 1 AND reservationForm.car_id = '" + ddlCarList.SelectedItem.Value.ToString() + "' AND pickup_date >= '" + DateTime.Today + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            DateTime reservedDate;

            while (reader.Read())
            {

                reservedDate = Convert.ToDateTime(reader["pickup_date"].ToString());
                
                if (e.Day.Date == reservedDate)
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Day.IsSelectable = false;
                }
                if (e.Day.Date == reservedDate.AddDays(1))
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Day.IsSelectable = false;
                }
                if (reservedDate != DateTime.Today)
                {
                    if (e.Day.Date == reservedDate.AddDays(-1))
                    {
                        e.Cell.BackColor = System.Drawing.Color.Red;
                        e.Day.IsSelectable = false;
                    }
                }      

            }
            reader.Close();

        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database: ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        
    }
    protected void ddlCarList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlCarList.Items.Count > 1)
        {
            ReservationData2.Visible = true;
            lblResults.Text = "";
        }

        if (ddlCarList.SelectedValue == "-1")
        {
            ReservationData2.Visible = false;
            ReservationData3.Visible = false;
            lblResults.Text = "Select a Vehicle. ";
        }

        else
        {
            ReservationData2.Visible = true;    
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //Checks for guest fields
        if (Session["userName"] == null)
        {

            if (ddlExpYear.SelectedValue == "--")
            {
                lblResults.Text = "Card Expiration Year Required.";
                lblResults.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (ddlExpMonth.SelectedValue == "--")
            {
                lblResults.Text = "Card Expiration Month Required.";
                lblResults.ForeColor = System.Drawing.Color.Red;
                return;
            }

        }

        if (!chkOver21.Checked)
        {
            lblResults.Text = "Please, confirm your age.";
            lblResults.ForeColor = System.Drawing.Color.Red;
            return;
        }

        //Define ADO.NET object
        string insertSQL;
        insertSQL = "INSERT INTO reservationForm (";
        insertSQL += "location_id, car_id, pickup_date, pickup_time, return_date, return_time, first_name, last_name, email, phone, customer_id, cupon_id, rental_price) ";
        insertSQL += "VALUES (";
        insertSQL += "@loci, @cari, @picd , @pict, @retd, @rett, @firs, @last, @emai, @phon, @cusi, @cupi, @rent)";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(insertSQL, con);

        //Add the parameter.
        cmd.Parameters.AddWithValue("@loci", ddlLocation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@cari", ddlCarList.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@picd", txtPickupDate.Text);
        cmd.Parameters.AddWithValue("@pict", ddlPickupTime.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@retd", txtReturnDate.Text);
        cmd.Parameters.AddWithValue("@rett", txtReturnTime.Text);
        cmd.Parameters.AddWithValue("@firs", txtFirstName.Text);
        cmd.Parameters.AddWithValue("@last", txtLastName.Text);
        cmd.Parameters.AddWithValue("@emai", txtEmail.Text);
        cmd.Parameters.AddWithValue("@phon", txtPhone.Text);
        

        if (Session["userName"] != null)
        {
            double newTotal = Convert.ToInt32(lblTXTRentalFee.Text);

            if (Convert.ToInt32(ddlCupon.SelectedItem.Value) > 0)
                {  
                    double cupon = Convert.ToInt32(findCuponValue());
                    double rental = Convert.ToInt32(lblTXTRentalFee.Text);

                    double discount = rental * (cupon / 100); 
                    newTotal =  rental - discount;
                 
                    lblResults.Text = "Thank You.<br/>Reservation complete. Your card has been charged $" + newTotal ;
                    lblResults.Text += "<br/>A discount of $" + discount + " has been applied to your reservation.";

                    clearUsedCupon();
                }
                else
                {
                    lblResults.Text = "Reservation complete. Your card has been charged $" + lblTXTRentalFee.Text + ".00";
                    cuponGenerator();
                }

            cmd.Parameters.AddWithValue("@rent", newTotal);
            cmd.Parameters.AddWithValue("@cusi", Convert.ToInt32(Session["userID"]));
            cmd.Parameters.AddWithValue("@cupi", ddlCupon.SelectedItem.Value);
        }
        else
        {
            lblResults.Text = "Thank You. <br/>Reservation complete. Your card has been charged $" + lblTXTRentalFee.Text + ".00";

            cmd.Parameters.AddWithValue("@rent", lblTXTRentalFee.Text);
            cmd.Parameters.AddWithValue("@cusi", 0);
            cmd.Parameters.AddWithValue("@cupi", 0);
        }
        
        

        //Try to open the database and execute the update.
        int added = 0; //counter

        try
        {
            con.Open();
            added = cmd.ExecuteNonQuery();
       
        }
        catch (Exception err)
        {
            lblResults.Text = "Error inserting record. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        //If insert succeeded, refresh the ddl
        if (added > 0)
        {
            resetForm();
            ddlLocation.SelectedIndex = 0;
            ddlCarList.SelectedIndex = 0;
            ReservationData.Visible = false;
            ReservationData0.Visible = false;
            ReservationData2.Visible = false;
            ReservationData3.Visible = false;

        }  
    
    }

    private void findRentalPrice()
    {

        string selectSQL = "SELECT rent_price FROM carData WHERE car_id = '" + Convert.ToInt32(ddlCarList.SelectedItem.Value) +  "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lblTXTRentalFee.Text = reader["rent_price"].ToString();
            }
            reader.Close();
        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database: ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnNewReservation_Click(object sender, EventArgs e)
    {

        ReservationEdit.Visible = false;
        ReservationEdit2.Visible = false;
        ReservationData0.Visible = true;

    }

    public void FillReservationList()
    {
        ddlReservationList.Items.Clear();

        ListItem newFirstItem = new ListItem();
        newFirstItem.Text = "-- PLEASE SELECT --";
        newFirstItem.Value = "-1";
        ddlReservationList.Items.Add(newFirstItem);

        clearCarInfo();
        clearOldDates();

        string selectSQL = "SELECT reservation_id, car_id, pickup_date, pickup_time FROM reservationForm WHERE status = 1 AND customer_id = '" + Session["userID"].ToString() + "' ORDER BY pickup_date ASC " ;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = Convert.ToDateTime(reader["pickup_date"]).ToShortDateString();
                newItem.Text += " @ ";
                newItem.Text += reader["pickup_time"].ToString();
                newItem.Value = reader["reservation_id"].ToString();
                ddlReservationList.Items.Add(newItem);
            }
            reader.Close();

            int reservationTotal = ddlReservationList.Items.Count -1;

            if (ddlReservationList.Items.Count == 1)
            {
                lblReservationResults.Text = "There are no reservations.";
            }
            else
            {
                lblReservationResults.Text = "You have " + reservationTotal.ToString() + " reservations.";
            }

        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database: ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

    }

    public void clearOldDates()
    {
        //Define ADO.NET objects
        string updateSQL;
        updateSQL = "UPDATE reservationForm SET status=@status, changed_by=@changeBy WHERE status = 1 AND pickup_date < '" + DateTime.Today + "' ";

        SqlConnection con = new SqlConnection (connectionString);
        SqlCommand cmd = new SqlCommand (updateSQL, con);

        //Add the parameters.
        cmd.Parameters.AddWithValue("@status", "0");
        cmd.Parameters.AddWithValue("@changeBy", "SYSTEM");
        

        //Try to open the database and execute the update
        int updated = 0; //counter

        try
        {
            con.Open();
            updated = cmd.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            lblResults.Text = "Error updating records. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    
    }

    public void clearCarInfo()
    {
        txtMake.Text = "";
        txtModel.Text = "";
        txtColor.Text = "";
        txtYear.Text = "";
        txtMilage.Text = "";
        txtTransmission.Text = "";
        txtDrivetrain.Text = "";
    
    }

    protected void ddlReservationList_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlReservationList.Items.Count > 1)
        {
            ReservationEdit2.Visible = true;
            lblResults.Text = "";
        }

        if (ddlReservationList.SelectedValue == "-1")
        {
            ReservationEdit2.Visible = false;
            lblResults.Text = "Select a reservation. ";
            return;
        }

        CarIdLookup(); //set Session var carID
        cancellationFeeCalculator();

        string selectSQL;
        selectSQL = "SELECT * FROM carData WHERE car_id = '" + Session["carID"].ToString() + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            string make = reader["make"].ToString();
            txtMake.Text = make;
            string model = reader["model"].ToString();
            txtModel.Text = model;
            txtColor.Text = reader["color"].ToString();
            txtYear.Text = reader["year"].ToString();
            txtMilage.Text = reader["milage"].ToString();
            txtTransmission.Text = reader["transmission"].ToString();
            txtDrivetrain.Text = reader["drivetrain"].ToString();
            reader.Close();

        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        //Define ADO.NET object
        string updateSQL;
        updateSQL = "UPDATE reservationForm SET status=@status, changed_by=@changeBy, cancellation_fee=@fee, rental_price=@rental FROM reservationForm WHERE status = 1 AND reservation_id = '" + ddlReservationList.SelectedItem.Value.ToString() + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, con);

        //Add the parameters.
        cmd.Parameters.AddWithValue("@status", "0");
        cmd.Parameters.AddWithValue("@changeBy", Session["userName"].ToString());
        cmd.Parameters.AddWithValue("@fee", Convert.ToInt32(Session["feeCharge"]));
        cmd.Parameters.AddWithValue("@rental", 0);

        //Try to open the database and execute the update
        int updated = 0; //counter

        try
        {
            con.Open();
            updated = cmd.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            lblResults.Text = "Error updating records. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        //If insert succeeded, refresh the ddl
        if (updated > 0)
        {
            Session["lblResults"] = "Reservation has been cancelled.";
            lblResults.Text = Session["lblResults"].ToString();
            ReservationEdit2.Visible = false;
            FillReservationList();
        }  

    }

    public void CarIdLookup()
    {
        string selectSQL;
        selectSQL = "SELECT car_id, pickup_date, rental_price FROM reservationForm WHERE reservation_id = '" + ddlReservationList.SelectedItem.Value.ToString() + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            Session["carID"] = reader["car_id"].ToString();
            Session["pickupDate"] = reader["pickup_date"].ToString();
            Session["rentalPrice"] = Convert.ToInt32(reader["rental_price"]);

            reader.Close();

        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }
  
    protected void ddlExpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExpMonth.Items.Clear();
        ddlExpMonth.Enabled = true;
        string currentYear = DateTime.Today.Year.ToString();

        if (ddlExpYear.SelectedValue == currentYear)
        {
            fillExpMonthPartial();
        }
        else
        {
            fillExpMonthAll();
        }
    }

    public void fillExpMonthAll()
    {
        ddlExpMonth.Items.Add("--");

        for (int i = 1; i < 10; i++)
        {
            ddlExpMonth.Items.Add("0" + i);
        }
        for (int i = 10; i <= 12; i++)
        {
            ddlExpMonth.Items.Add(i.ToString());
        }
    }

    public void fillExpMonthPartial()
    {
        ddlExpMonth.Items.Add("--");

        int currentMonth = Convert.ToInt32(DateTime.Today.Month);

        if (currentMonth < 10)
        {
            for (int i = currentMonth; i < 10; i++)
            {
                ddlExpMonth.Items.Add("0" + i);
            }
            for (int i = 10; i <= 12; i++)
            {
                ddlExpMonth.Items.Add(i.ToString());
            }
        }
        else
        {
            for (int i = currentMonth; i <= 12; i++)
            {
                ddlExpMonth.Items.Add(i.ToString());
            }
        }
    }

    public void fillExpYear()
    {
        ddlExpYear.Items.Add("--");

        for (int i = 2015; i <= 2020; i++)
        {
            ddlExpYear.Items.Add(i.ToString());
        }
    }

    protected void ckbName_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbName.Checked)
        {
            txtCardName.Text = txtFirstName.Text + " " + txtLastName.Text;
        }
        else
        {
            txtCardName.Text = "";
        }
    }

    public void cancellationFeeCalculator()
    {
        DateTime reservationDate = Convert.ToDateTime(Session["pickupDate"]);
        int orginalPrice = 0;

        if (Session["rentalPrice"] != null)
        {
            orginalPrice = Convert.ToInt32(Session["rentalPrice"]);
        }
 
        if (DateTime.Today == reservationDate || DateTime.Today.AddDays(1) == reservationDate)
        {
            lblCancellationFee.Text = "Cancellation fee is 75%.";           
            Session["feeCharge"] = orginalPrice * .75;
        }else if(DateTime.Today.AddDays(2) <= reservationDate && DateTime.Today.AddDays(6) >= reservationDate)
        {
            lblCancellationFee.Text = "Cancellation fee is 50%.";
            Session["feeCharge"] = orginalPrice * .50;
        }else if(DateTime.Today.AddDays(7) <= reservationDate && DateTime.Today.AddDays(14) >= reservationDate)
        {
            lblCancellationFee.Text = "Cancellation fee is 25%.";
            Session["feeCharge"] = orginalPrice * .25;
        }
        else
        {
            lblCancellationFee.Text = "Cancellation fee is 10%.";
            Session["feeCharge"] = orginalPrice * .10;
        }

        lblResults.Text = "Your Credit Card will be charge $" + Session["feeCharge"].ToString();

        Session["rentalPrice"] = null;

    }

    public void cuponGenerator()
    {
        //Generate random cupon 
        int discount = 0;
        var r = new Random();
        discount = (r.Next(5, 30));

        //Define ADO.NET object
        string insertSQL;
        insertSQL = "INSERT INTO cupons (customer_id, discount, description, created_date) VALUES (@customer, @discount, @description, @date)";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(insertSQL, con);

        //Add the parameter.
        cmd.Parameters.AddWithValue("@customer", Session["userID"].ToString());
        cmd.Parameters.AddWithValue("@discount", discount);
        cmd.Parameters.AddWithValue("@description", discount + "% DISCOUNT");
        cmd.Parameters.AddWithValue("@date", DateTime.Today);

        //Try to open the database and execute the update.
        int added = 0; //counter

        try
        {
            con.Open();
            added = cmd.ExecuteNonQuery();

        }
        catch (Exception err)
        {
            lblResults.Text = "Error inserting record. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        //If insert succeeded, refresh the ddl
        if (added > 0)
        {
            lblResults.Text += "<br/>Congratulations!<br/>You have received a new cupon.";
            TableRow tRow = new TableRow();
            newCupon.Rows.Add(tRow);
            TableCell tCell = new TableCell();
            tCell.Text = discount + "% DISCOUNT";
            tCell.Text += "<br /><p>Generated today";
            tCell.Text += "<br />Exclusively for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + "</p>";
            tRow.Cells.Add(tCell);

        }

    }

    protected void ddlCupon_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void fillCuponList()
    {
        ddlCupon.Items.Clear();

        string selectSQL = "SELECT * FROM cupons WHERE customer_id = '" + Session["userID"].ToString() + "' AND status = 1 ORDER BY created_date DESC";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            ListItem newFirstItem = new ListItem();
            newFirstItem.Text = "";
            newFirstItem.Value = "-1";
            ddlCupon.Items.Add(newFirstItem);

            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text += reader["description"].ToString();
                newItem.Value = reader["cupon_id"].ToString();
                ddlCupon.Items.Add(newItem);
            }
            reader.Close();

        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database: ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }

    public string findCuponValue()
    {
        string cupon = "";

        string selectSQL = "SELECT discount FROM cupons WHERE cupon_id = '" + ddlCupon.SelectedItem.Value + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cupon = reader["discount"].ToString();
            }
            reader.Close();

        }
        catch (Exception err)
        {
            lblResults.Text = "Error reading from database: ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

        return cupon;
    }

    public void clearUsedCupon()
    {
        //Define ADO.NET objects
        string updateSQL;
        updateSQL = "UPDATE cupons SET status=@status, used_date=@used WHERE cupon_id = '" + ddlCupon.SelectedItem.Value + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, con);

        //Add the parameters.
        cmd.Parameters.AddWithValue("@status", "0");
        cmd.Parameters.AddWithValue("@used", DateTime.Today);


        //Try to open the database and execute the update
        int updated = 0; //counter

        try
        {
            con.Open();
            updated = cmd.ExecuteNonQuery();
      
        }
        catch (Exception err)
        {
            lblResults.Text = "Error updating records. ";
            lblResults.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
    }
}




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
            fillLocationList();
        }
    }

    public void fillTimeList()
    {
        ddlPickupTime.Items.Add("SELECT");

        for(int i=8; i<=20; i++)
        {
            ddlPickupTime.Items.Add(i+":00");
        }    
    }

    public void resetForm()
    {
        txtPickupDate.Text = "";
        ddlPickupTime.SelectedIndex = 0;
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

        string selectSQL = "SELECT pickup_date FROM reservationForm WHERE reservationForm.car_id = '" + ddlCarList.SelectedItem.Value.ToString() + "' AND pickup_date >= '" + DateTime.Today + "' ";

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
        resetForm();

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
        //Define ADO.NET object
        string insertSQL;
        insertSQL = "INSERT INTO reservationForm (";
        insertSQL += "location_id, car_id, pickup_date, pickup_time, return_date, return_time, email, phone, customer_id, cupon_id) ";
        insertSQL += "VALUES (";
        insertSQL += "@loci, @cari, @picd , @pict, @retd, @rett, @emai, @phon, @cusi, @cupi)";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(insertSQL, con);

        //Add the parameter.
        cmd.Parameters.AddWithValue("@loci", ddlLocation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@cari", ddlCarList.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@picd", txtPickupDate.Text);
        cmd.Parameters.AddWithValue("@pict", ddlPickupTime.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@retd", txtReturnDate.Text);
        cmd.Parameters.AddWithValue("@rett", txtReturnTime.Text);
        cmd.Parameters.AddWithValue("@emai", txtEmail.Text);
        cmd.Parameters.AddWithValue("@phon", txtPhone.Text);
        cmd.Parameters.AddWithValue("@cusi", 0);
        cmd.Parameters.AddWithValue("@cupi", 0);

        //Try to open the database and execute the update.
        int added = 0; //counter

        try
        {
            con.Open();
            added = cmd.ExecuteNonQuery();
            lblResults.Text = added.ToString() + " record inserted.";
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
            ReservationData2.Visible = false;
            ReservationData3.Visible = false;
        }

        
    
    }
}
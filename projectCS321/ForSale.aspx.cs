﻿using System;
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

public partial class ForSale : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            fillList();
        }
    }

    private string connectionString = WebConfigurationManager.ConnectionStrings["zzCS321_1ConnectionString"].ConnectionString;

    private void fillList()
    {
        lbxCars.Items.Clear();

        string selectSQL = "SELECT * FROM carData WHERE status = 's' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            int totalVehicles = 0;

            while (reader.Read())
            {
                ListItem newItem = new ListItem();

                newItem.Text += reader["make"].ToString();
                newItem.Text += " ";
                newItem.Text += reader["model"].ToString();
                newItem.Text += ", ";
                newItem.Text += reader["year"].ToString();
                newItem.Value = reader["car_id"].ToString();
                lbxCars.Items.Add(newItem);

                totalVehicles++;     

            }
            reader.Close();

            txtVehTotal.Text = totalVehicles.ToString();

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


    protected void lsbCars_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectSQL;
        selectSQL = "SELECT * FROM carData " + "WHERE car_id = '" + lbxCars.SelectedItem.Value + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            string carId = reader["car_id"].ToString();
            txtcarId.Text = carId;
            string make = reader["make"].ToString();
            txtMake.Text = make;
            string model = reader["model"].ToString();
            txtModel.Text = model;
            txtColor.Text = reader["color"].ToString();
            txtYear.Text = reader["year"].ToString();
            txtMilage.Text = reader["milage"].ToString();
            txtTransmission.Text = reader["transmission"].ToString();
            txtDrivetrain.Text = reader["drivetrain"].ToString();
            txtSalePrice.Text = reader["sale_price"].ToString() + ".00";
            imgPicture.ImageUrl = "~/Images/vehicleImages/" + carId + ".jpg";
            imgPicture.ToolTip = make + " " + model;

            string availability = reader["location_id"].ToString();

            vehicleAvailability();

            reader.Close();
            divVehicleGuide2.Visible = false;
            divVehicleGuide1.Visible = true;
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

    public void vehicleAvailability()
    {

        string selectSQL = "SELECT pickup_date FROM reservationForm WHERE reservationForm.car_id = '" + lbxCars.SelectedItem.Value.ToString() + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        DateTime temp;
        txtAvailability.Text = "Yes";
        txtAvailability.ForeColor = System.Drawing.Color.Green;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                temp = Convert.ToDateTime(reader["pickup_date"].ToString());

                if (temp == DateTime.Today || temp == DateTime.Today.AddDays(1))
                {
                    txtAvailability.Text = "No";
                    txtAvailability.ForeColor = System.Drawing.Color.Red;
                }
            }
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

    public Boolean vehicleCount(string carID)
    {

        string selectSQL = "SELECT pickup_date FROM reservationForm WHERE reservationForm.car_id = '" + carID + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        DateTime temp;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                temp = Convert.ToDateTime(reader["pickup_date"].ToString());

                if (temp == DateTime.Today || temp == DateTime.Today.AddDays(1))
                {
                    return true;
                }
            }
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

        return false;
    }
}
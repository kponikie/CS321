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

public partial class VehicleGuide : System.Web.UI.Page
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

        string selectSQL = "SELECT car_id, make, model, color, year, milage, transmission, drivetrain FROM carData";

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
                newItem.Text = reader["make"].ToString();
                newItem.Text += " ";
                newItem.Text += reader["model"].ToString();
                newItem.Text += ", ";
                newItem.Text += reader["year"].ToString();
                newItem.Value = reader["car_id"].ToString();
                lbxCars.Items.Add(newItem);
                
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

            txtcarId.Text = reader["car_id"].ToString();
            txtMake.Text = reader["make"].ToString();
            txtModel.Text = reader["model"].ToString();
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
}

    

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

public partial class Rewards : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            guestMessage.Visible = true;
            memberMessage.Visible = false;
            lblCupons.Text = "For Customers Only.<br />Visit one of our locations to sign up!";
        }
        else
        {
            lblCupons.Text = "";
            guestMessage.Visible = false;
            memberMessage.Visible = true;
            lblResults.Text = "Reward progress for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + " .";
            calculateRewards();
        }
    }

    private string connectionString = WebConfigurationManager.ConnectionStrings["zzCS321_1ConnectionString"].ConnectionString;

    private void calculateRewards()
    {

        string selectSQL = "SELECT rental_price, cancellation_fee FROM reservationForm WHERE customer_id = '" + Session["userID"].ToString() + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        int totalReward = 0;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                totalReward += (int)reader["rental_price"];
                totalReward += (int)reader["cancellation_fee"];
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

        lblRewardsTotal.Text = totalReward.ToString();
        meter();
    
    }

    public void meter()
    {
        TableRow tRow = new TableRow();
        tableMeter.Rows.Add(tRow);

        int rewards = Convert.ToInt32(lblRewardsTotal.Text);

        if (rewards >= 10000 && cuponDiscountCheck())
        {
            cuponGenerator();
        }

        for (int i=1000; i<=10000; i+=1000)
        {
            TableCell tCell = new TableCell();
            //tCell.Text = i.ToString();

            if (rewards <= i && rewards > (i-1000))
            {
                tCell.ForeColor = System.Drawing.Color.White;
                tCell.Text = rewards.ToString();
                tCell.BackColor = System.Drawing.Color.ForestGreen;
            }else if (rewards > i)
            {
                tCell.BackColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                tCell.BackColor = System.Drawing.Color.LightGreen;
                tCell.ForeColor = System.Drawing.Color.White;
                tCell.Text = i.ToString();
            }
            tRow.Cells.Add(tCell);
            
        }
        
    }

    public void cuponGenerator()
    {
        //Generate random cupon 
        int discount = 100;

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
            lblCuponResults.Text += "<br/>Congratulations!<br/>You have received a new cupon.";
            TableRow tRow = new TableRow();
            newCupon.Rows.Add(tRow);
            TableCell tCell = new TableCell();
            tCell.Text = discount + "% DISCOUNT";
            tCell.Text += "<br /><p>Generated today";
            tCell.Text += "<br />Exclusively for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + "</p>";
            tRow.Cells.Add(tCell);

        }

    }

    public Boolean cuponDiscountCheck()
    {
        string selectSQL = "SELECT discount FROM cupons WHERE customer_id = '" + Session["userID"].ToString() + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (Convert.ToInt32(reader["discount"]) == 100)
                {
                    return false;
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

        return true;

    }

}
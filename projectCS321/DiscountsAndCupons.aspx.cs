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


public partial class DiscountsAndCupons : System.Web.UI.Page
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
            lblResults.Text = "Available discounts and Cupons for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + " only.";
            //generateNewCuponList();
            //rdbNew.Checked = true;
        }

    }

    private string connectionString = WebConfigurationManager.ConnectionStrings["zzCS321_1ConnectionString"].ConnectionString;

    public void generateNewCuponList()
    {
        string selectSQL = "SELECT description, created_date FROM cupons WHERE customer_id = '" + Session["userID"].ToString() + "' AND status = 1 ORDER BY created_date DESC";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TableRow tRow = new TableRow();
                cuponTable.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = reader["description"].ToString();
                tCell.Text += "<br /><p>Generated on:" + Convert.ToDateTime(reader["created_date"]).ToShortDateString();
                tCell.Text += "<br />Exclusively for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + "</p>";
                tRow.Cells.Add(tCell);

                if (reader.Read())
                {
                    tCell = new TableCell();
                    tCell.Text = reader["description"].ToString();
                    tCell.Text += "<br /><p>Generated on:" + Convert.ToDateTime(reader["created_date"]).ToShortDateString();
                    tCell.Text += "<br />Exclusively for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + "</p>";
                    tRow.Cells.Add(tCell);
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

    public void generateUsedCuponList()
    {
        string selectSQL = "SELECT description, created_date, used_date FROM cupons WHERE customer_id = '" + Session["userID"].ToString() + "' AND status = 0 ORDER BY used_date DESC";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TableRow tRow = new TableRow();
                cuponTable.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = reader["description"].ToString();
                tCell.Text += "<br />Used on: " + Convert.ToDateTime(reader["used_date"]).ToShortDateString();
                tRow.Cells.Add(tCell);

                if (reader.Read())
                {
                    tCell = new TableCell();
                    tCell.Text = reader["description"].ToString();
                    tCell.Text += "<br />Used on: " + Convert.ToDateTime(reader["used_date"]).ToShortDateString();
                    tRow.Cells.Add(tCell);
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

    protected void rdbNew_CheckedChanged(object sender, EventArgs e)
    {
        generateNewCuponList();
    }
    protected void rdbUsed_CheckedChanged(object sender, EventArgs e)
    {
        generateUsedCuponList();
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        generateNewCuponList();
        generateUsedCuponList();
    }
}
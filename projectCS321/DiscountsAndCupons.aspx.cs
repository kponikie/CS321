using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DiscountsAndCupons : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["userType"]) != 1)
        {
            lblCupons.Text = "For Customers Only.";
            lblCupons2.Text = "Become a customer to receive cupons";
        }
        else
        {
            lblCupons.Text = "Available discounts and Cupons for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + ".";
            lblCupons2.Text = "";
        }

    }
}
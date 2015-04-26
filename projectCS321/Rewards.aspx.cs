using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rewards : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            lblCupons.Text = "For Customers Only.<br />Visit one of our locations to sign up!";
            imgLock.Visible = true;
        }
        else
        {
            lblCupons.Text = "Available discounts and Cupons for " + Session["firstName"].ToString() + " " + Session["lastName"].ToString() + ".";
            imgLock.Visible = false;
            lblCupons.Text = "";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["userName"] == null)
            {
                lblWelcome.Text = "Guest";
                btnLogin.Visible = true;
                btnLogout.Visible = false;
            }
            else
            {
                lblWelcome.Text = "Welcome " + Session["userName"].ToString();
                btnLogout.Visible = true;
                btnLogin.Visible = false;
                
            }
        }
  
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        lblWelcome.Text = "";
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }

}

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

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblResults.Text = "";
        lblLoginResults.Text = "";
    }

    private string connectionString = WebConfigurationManager.ConnectionStrings["zzCS321_1ConnectionString"].ConnectionString;

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        string selectSQL = "SELECT * FROM userData WHERE user_name = '" + txtUserName.Text + "' AND password = '" + txtPassword.Text + "' ";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        int temp = 0;
        lblLoginResults.Text = "";

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                temp++;
                Session["userID"] = reader["user_id"].ToString();
                Session["userName"] = reader["user_name"].ToString();
                Session["firstName"] = reader["first_name"].ToString();
                Session["lastName"] = reader["last_name"].ToString();
                Session["email"] = reader["email"].ToString();
                Session["phone"] = reader["phone"].ToString();
                Session["userType"] = reader["type"].ToString();
                Session["creditCard"] = reader["credit_card"].ToString();
                Session["expMonth"] = reader["exp_month"].ToString();
                Session["expYear"] = reader["exp_year"].ToString();
                Session["creditType"] = reader["credit_type"].ToString();
                
            }
            reader.Close();

            if (temp == 1)
            {
                if (Session["userType"].ToString() == "2")
                {
                    Response.Redirect("mgmt.aspx");
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                lblLoginResults.Text = "User Name or Password incorect !";
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
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_STOCK : System.Web.UI.MasterPage
{
    private UtilityLibrary utl = new UtilityLibrary();
    public string valID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Name"] != null)
        {
            base.Response.ClearHeaders();
            base.Response.AppendHeader("Pragma", "no-cache");
            base.Response.AppendHeader("Pragma", "no-store");
            base.Response.AppendHeader("cache-control", "no-cache , no-store, private, must-revalidate");
            base.Response.AppendHeader("Expires", "0");
            DateTime now = DateTime.Now;
            this.lblLoginTime.Text = now.ToString("f");
            this.lblUser.Text = base.Session["Name"].ToString();
            this.Mlogout.NavigateUrl = string.Concat("~/Logout.aspx?val=", base.Session["AuthTokenPage"].ToString());

            if (base.Session["UserType"].ToString().Trim() == "OAIC")
            {
                this.Mhome.NavigateUrl = "../Masters/Home.aspx";
            }
            else if (base.Session["UserType"].ToString().Trim() == "OSSC")
            {
                this.Mhome.NavigateUrl = "../Masters/Home.aspx";
            }
            else
            {
                //base.Response.Redirect("DeptHome.aspx", false);
                this.Mlogout.NavigateUrl = string.Concat("~/Logout.aspx?val=", base.Session["AuthTokenPage"].ToString());
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
}

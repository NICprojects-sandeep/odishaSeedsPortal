﻿using System;

public partial class MasterPages_Inner : System.Web.UI.MasterPage
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
            if (base.Session["Name"].ToString().Trim() != "")
            {
                this.lblUser.Text = base.Session["Name"].ToString();
            }
            else
            {
                //base.Response.Redirect("DeptHome.aspx", false);
                this.Mlogout.NavigateUrl = string.Concat("../Logout.aspx?val=", base.Session["AuthTokenPage"].ToString());
            }
            this.Mlogout.NavigateUrl = string.Concat("../Logout.aspx?val=", base.Session["AuthTokenPage"].ToString());

            if (base.Session["UserType"].ToString().Trim() == "OAIC")
            {
                this.Mhome.NavigateUrl = "../Masters/Home.aspx";
            }
            else if (base.Session["UserType"].ToString().Trim() == "OSSC")
            {
                this.Mhome.NavigateUrl = "../Masters/Home.aspx";
            }
            else if (base.Session["UserType"].ToString().Trim() == "ADMI")
            {
                this.Mhome.NavigateUrl = "../Admin/AdminHome.aspx";
            }
            else
            {
                //base.Response.Redirect("DeptHome.aspx", false);
                this.Mlogout.NavigateUrl = string.Concat("../Logout.aspx?val=", base.Session["AuthTokenPage"].ToString());
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

}

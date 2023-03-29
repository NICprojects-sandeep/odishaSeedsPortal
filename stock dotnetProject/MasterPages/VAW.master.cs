using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_VAW : System.Web.UI.MasterPage
{
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
            
          

            if (base.Session["UserType"].ToString().Trim() == "AAOO")
            {
                this.Mhome.NavigateUrl = "../Admin/VAW_HOME.aspx";
            } 
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
}

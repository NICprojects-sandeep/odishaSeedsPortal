using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_Dealer : System.Web.UI.MasterPage
{
    private UtilityLibrary utl = new UtilityLibrary();
    public string valID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LIC_NO"] != null)
        {
            base.Response.ClearHeaders();
            base.Response.AppendHeader("Pragma", "no-cache");
            base.Response.AppendHeader("Pragma", "no-store");
            base.Response.AppendHeader("cache-control", "no-cache , no-store, private, must-revalidate");
            base.Response.AppendHeader("Expires", "0");
            DateTime now = DateTime.Now;
            lblName.Text = Session["APP_FIRMNAME"].ToString();
            
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
}

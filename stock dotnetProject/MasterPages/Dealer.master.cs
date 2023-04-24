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
    protected void btnClickRe_Click(object sender, EventArgs e)
    {
            //string UserId= Session["UserID"].ToString();
            //Response.Redirect("http://localhost:4300/public/transferOfSeedSubsidy?userID=" + UserId);
            Response.Redirect("http://164.100.140.77/users/#/public/transferOfSeedSubsidy" );
    }
}

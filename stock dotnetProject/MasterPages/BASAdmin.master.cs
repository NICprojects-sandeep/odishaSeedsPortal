using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_BASAdmin : System.Web.UI.MasterPage
{
    private UtilityLibrary utl = new UtilityLibrary();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (this.Session["UserID"] == null || this.Session["AuthToken"] == null || base.Request.Cookies["AuthToken"] == null)
        {
            //this.utl.SessionReset();
            base.Response.Redirect("../BankLogout.aspx", false);
        }
        else
        {
            if (!this.Session["AuthToken"].ToString().Equals(base.Request.Cookies["AuthToken"].Value))
            {
                //this.utl.SessionReset();
                base.Response.Redirect("../BankLogout.aspx", false);
                return;
            }
            if (this.Session["UserType"].ToString() != "NICA")
            {
                //this.utl.SessionReset();
                base.Response.Redirect("../BankLogout.aspx", false);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                this.utl.SetSessionCookie();

                LoggedinUser.InnerText = this.Session["UserID"].ToString();
                LoggedinUserDetails.InnerText = this.Session["UserID"].ToString() + " - BAS Control Panel";
            }
        }

    }
}

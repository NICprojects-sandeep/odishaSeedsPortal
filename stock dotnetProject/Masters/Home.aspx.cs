using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_Home : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();
    private UtilityLibrary utl = new UtilityLibrary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null || this.Session["AuthToken"] == null || base.Request.Cookies["AuthToken"] == null)
        {
            this.utl.SessionReset();
        }
        else
        {
            if (!this.Session["AuthToken"].ToString().Equals(base.Request.Cookies["AuthToken"].Value))
            {
                this.utl.SessionReset();
                return;
            }
            if (this.Session["UserType"].ToString() != "OAIC" && this.Session["UserType"].ToString() != "OSSC")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                this.hfMCode.Value = Session["UserID"].ToString();
                this.lblName.Text = Session["FullName"].ToString();
                Session["OBDate"] = "01/10/2015";
                return;
            }
        }
    }
}
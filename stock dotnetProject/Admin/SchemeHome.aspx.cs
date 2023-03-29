using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_SchemeHome : System.Web.UI.Page
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
            if ((this.Session["UserType"].ToString() != "NFSM") && !(this.Session["UserType"].ToString() == "RKVY") && !(this.Session["UserType"].ToString() == "NMAD") && !(this.Session["UserType"].ToString() == "SPAD"))
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
                if (this.Session["UserType"].ToString() == "NFSM")
                {
                    Session["SchemeCode"] = "OR1";
                    Session["SchemeName"] = "NFSM";
                }
                else if (this.Session["UserType"].ToString() == "RKVY")
                {
                    Session["SchemeCode"] = "OR7";
                    Session["SchemeName"] = "RKVY";
                }
                else if (this.Session["UserType"].ToString() == "NMAD")
                {
                    Session["SchemeCode"] = "OR43";
                    Session["SchemeName"] = "NMOOP";
                }
                else if (this.Session["UserType"].ToString() == "SPAD")
                {
                    Session["SchemeCode"] = "OR119";
                    Session["SchemeName"] = "STATE PLAN";
                }
                return;
            }
        }
    }
}
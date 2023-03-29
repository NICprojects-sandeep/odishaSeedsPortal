using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Reports_RPT_nfsmre : System.Web.UI.Page
{
    string userid = string.Empty;
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
            if (!(this.Session["UserType"].ToString() == "SPAD") && !(this.Session["UserType"].ToString() == "RKVY") && !(this.Session["UserType"].ToString() == "NMAD") && !(this.Session["UserType"].ToString() == "NFSM") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["UserType"].ToString() == "NFSM")
        {
            DataSet ds = new DataSet();
            string sql = "Report_NFSM";
            string[] param = { "@dt", "@schemcode" };
            object[] value = { Convert.ToDateTime(txtfrm.Text), "9140" };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_FarmerList.Attributes.Add("style", "display:;");
                litReport.Text = string.Empty;
                //GenerateFarmerList(ds.Tables[0]);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
            else
            {
                grd_FarmerList.Attributes.Add("style", "display:none;");
                litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
            }
        }
        else if (Session["UserType"].ToString() == "RKVY")
        {
            DataSet ds = new DataSet();
            string sql = "Report_NFSM";
            string[] param = { "@dt", "@schemcode" };
            object[] value = { Convert.ToDateTime(txtfrm.Text), "9145" };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_FarmerList.Attributes.Add("style", "display:;");
                litReport.Text = string.Empty;
                //GenerateFarmerList(ds.Tables[0]);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
            else
            {
                grd_FarmerList.Attributes.Add("style", "display:none;");
                litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
            }
        }
        else if (Session["UserType"].ToString() == "NMAD")
        {
            DataSet ds = new DataSet();
            string sql = "Report_NFSM";
            string[] param = { "@dt", "@schemcode" };
            object[] value = { Convert.ToDateTime(txtfrm.Text), "9143" };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_FarmerList.Attributes.Add("style", "display:;");
                litReport.Text = string.Empty;
                //GenerateFarmerList(ds.Tables[0]);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
            else
            {
                grd_FarmerList.Attributes.Add("style", "display:none;");
                litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
            }
        }
        else if (Session["UserType"].ToString() == "SPAD")
        {
            DataSet ds = new DataSet();
            string sql = "Report_NFSM";
            string[] param = { "@dt", "@schemcode" };
            object[] value = { Convert.ToDateTime(txtfrm.Text), "2054" };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_FarmerList.Attributes.Add("style", "display:;");
                litReport.Text = string.Empty;
                //GenerateFarmerList(ds.Tables[0]);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
            else
            {
                grd_FarmerList.Attributes.Add("style", "display:none;");
                litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_RTP_FARMERREDGDTL : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {

        if (Session["Name"] != null || Session["UserType"] != null)
        {
            if (this.Session["UserType"].ToString() == "ADMI")
            {
                this.Page.MasterPageFile = "../MasterPages/Admin.master";
                return;
            }
            if (this.Session["UserType"].ToString() == "SEAD")
            {
                this.Page.MasterPageFile = "../MasterPages/SeedAdmin.master";
                return;
            }
            if (this.Session["UserType"].ToString() == "RCSA")
            {
                this.Page.MasterPageFile = "../MasterPages/RCSA.master";
                return;
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
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
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") && !(this.Session["UserType"].ToString() == "RCSA") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                btnExport.Enabled = false;
                FillData();
            }
        }
    }

    private void FillData()
    {
        DataSet ds = new DataSet();

        string sql = "GET_FarmerRedg";
        string[] param = { };
        object[] value = { };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnExport.Enabled = true;
            GenerateReport(ds);
        }
        else
        {
            btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }


    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Dist Name  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>No. of farmer registered upto 20th June 2016   </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>No. of farmerid generated </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>No. of farmer registered from 21th June 2016  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>Total No. of farmer registered  </td>");
        str.Append("</tr>");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append(" <td style='padding-right:5px;'>" + dr["NICDIST_NAME"].ToString() + "</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + dr["NoofFarm"].ToString() + "</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + dr["FRMGEN"].ToString() + "</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + dr["NoofnicFarm"].ToString() + "</td>");

            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(dr["NoofFarm"].ToString()) + double.Parse(dr["NoofnicFarm"].ToString())).ToString() + "</td>");
            str.Append("</tr>");
        }

        string TotFarmer = ds.Tables[0].Compute("Sum(NoofFarm)", "").ToString();
        string TotFarmerGEN = ds.Tables[0].Compute("Sum(FRMGEN)", "").ToString();
        string TotNICFarmer = ds.Tables[0].Compute("Sum(NoofnicFarm)", "").ToString();

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append(" <td style='padding-right:5px;'>Total :</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + TotFarmer + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + TotFarmerGEN + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + TotNICFarmer + "</td>");
        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(TotFarmer) + double.Parse(TotNICFarmer)).ToString() + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=FarmerSaleList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            litReport.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex) { }
    }
}
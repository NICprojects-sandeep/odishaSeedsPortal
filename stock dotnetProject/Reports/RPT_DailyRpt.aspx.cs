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

public partial class Reports_RPT_DailyRpt : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
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
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                btnExport.Enabled = false;

            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "RPT_DailyRpt";
        string[] param = { "@Dt" };
        object[] value = { txtfromdate.Text.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
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
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; text-align:center; ' border='1' cellpadding='5' cellspacing='5' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td colspan='21' style='font-weight:bold; font-size:16px; text-align:center;'>Prepositioning,Dealer/PACS sale and farmers'sale for paddy seeds from 2016-kharif to 2019-kharif (As on <u>" + txtfromdate.Text.Trim() + "</u>)(Qtl.)</td>");
        str.Append("</tr>");
        str.Append("<tr  style='background-color:#CCCCCC; font-weight:bold;'> <td></td> <td colspan='5'> 2016 -Kharif</td><td colspan='5'>2017 -Kharif</td> <td colspan='5'>2018 -Kharif</td><td colspan='5'>2019 -Kharif</td></tr>");
        str.Append("<tr  style='background-color:#CCCCCC; font-weight:bold;'><td style=' text-align: Left; padding-right:5px;'> District </td> <td> Total receipt by OSSC/OAIC</td><td colspan='2'>Total lifting</td><td colspan='2'>Total farmer sale </td> <td> Total receipt by OSSC            </td>  <td colspan='2'> Total lifting</td> <td colspan='2'> Total farmer sale  </td><td>  Total receipt by OSSC</td> <td colspan='2'>Total lifting</td>   <td colspan='2'>Total farmer sale</td><td>  Total receipt by OSSC</td> <td colspan='2'>Total lifting</td>   <td colspan='2'>Total farmer sale</td> </tr>");


        str.Append("<tr><td></td><td></td><td>Dealer </td><td> PACS </td><td> Dealer</td><td>PACS</td><td></td><td>Dealer </td><td>PACS</td><td> Dealer</td><td>PACS </td><td></td><td> Dealer </td><td>  PACS </td> <td> Dealer </td><td> PACS </td><td></td><td> Dealer </td><td> PACS </td> <td> Dealer </td><td> PACS </td></tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td style=' text-align: Left; padding-right:5px;' >" + dr["Dist_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["RcvQty_16_17"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftDlr_16_17"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftPACS_16_17"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersaleDlr_16_17"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersalePACS_16_17"].ToString() + "</td>");


            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["RcvQty_17_18"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftDlr_17_18"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftPACS_17_18"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersaleDlr_17_18"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersalePACS_17_18"].ToString() + "</td>");


            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["RcvQty_18_19"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftDlr_18_19"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftPACS_18_19"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersaleDlr_18_19"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersalePACS_18_19"].ToString() + "</td>");

            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["RcvQty_19_20"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftDlr_19_20"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["lftPACS_19_20"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersaleDlr_19_20"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["farmersalePACS_19_20"].ToString() + "</td>");
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append("<td style=' text-align: Left; padding-right:5px;'>Grand Total</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(RcvQty_16_17)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftDlr_16_17)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftPACS_16_17)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersaleDlr_16_17)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersalePACS_16_17)", "").ToString() + "</td>");

        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(RcvQty_17_18)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftDlr_17_18)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftPACS_17_18)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersaleDlr_17_18)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersalePACS_17_18)", "").ToString() + "</td>");

        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(RcvQty_18_19)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftDlr_18_19)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftPACS_18_19)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersaleDlr_18_19)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersalePACS_18_19)", "").ToString() + "</td>");

        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(RcvQty_19_20)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftDlr_19_20)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(lftPACS_19_20)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersaleDlr_19_20)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(farmersalePACS_19_20)", "").ToString() + "</td>");

        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DailyReport.xls";
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
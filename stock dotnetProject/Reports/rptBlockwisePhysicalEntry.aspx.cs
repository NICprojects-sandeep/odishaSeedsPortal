using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Reports_rptBlockwisePhysicalEntry : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillData();
        }
    }

    private void FillData()
    {
        string sql = "STOCK_BLOCKWISESALE";
        string[] param = { "@DDA_CODE", "@Crop_Code", "@FIN_YEAR", "@SEASON" };
        object[] value = { Request.QueryString["DISTID"].Trim(), Request.QueryString["Crop_Code"].Trim(), Request.QueryString["Session"].Trim(), Request.QueryString["Season"].Trim() == "0" ? System.Convert.DBNull : Request.QueryString["Season"].Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds);
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }

    }
    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No.</td>");
        str.Append("<td>Block Name</td>");
        str.Append("<td style='text-align: Right;'>Lifting Done(Received) Quantity</td>");
        str.Append("<td style='text-align: Right;'>Physical Sale Quantity</td>");
        str.Append("<td style='text-align: Right;'>Online Sale Quantity</td>");
        str.Append("<td style='text-align: Right; padding-right:5px; width:50px;'>View Details </td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["BLK_NAME"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["ACTUAL_RCV"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SaleQty"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["ACTUAL_SALE"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;'>");

            string tmpstr111 = "<a href='javascript:popUp";
            string tempStr11 = "('rptDealerwisePhysicalEntry.aspx?AAO_CODE=" + dr["AAO_CODE"].ToString() + " &Crop_Code= " + Request.QueryString["Crop_Code"].Trim() + " &FIN_YEAR=" + Request.QueryString["Session"].Trim() + " &Season=" + Request.QueryString["Season"].Trim() + "')";
            string tmpstr211 = "'>";
            str.Append(tmpstr111.Replace('\'', '\"') + tempStr11 + tmpstr211.Replace('\'', '\"'));
            str.Append("<img src='../images/binoculars.png'  title='View Details' /></a>");

            str.Append("</td>");
            str.Append("</tr>");
            i++;
        }

        string SaleQty = ds.Tables[0].Compute("Sum(SaleQty)", "").ToString();
        string ACTUAL_SALE = ds.Tables[0].Compute("Sum(ACTUAL_SALE)", "").ToString();
        string ACTUAL_RCV = ds.Tables[0].Compute("Sum(ACTUAL_RCV)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Right;'></td>");
        str.Append("<td >Total : </td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(ACTUAL_RCV)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(SaleQty)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(ACTUAL_SALE)) + "</td>");
        str.Append("<td style='text-align: Right;'>&nbsp;</td>");


        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
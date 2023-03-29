using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_rptDealerwisePhysicalEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        DataSet ds = new DataSet();
        string sql = "STOCK_UPDTDEALER_STOCK";
        string[] param = { "@BLOCK_ID", "@CROP_ID", "@FIN_YEAR", "@SEASON" };

        object[] value = { Request.QueryString["AAO_CODE"].Trim(), Request.QueryString["Crop_Code"].Trim() == "--- ALL ---" ? System.Convert.DBNull : Request.QueryString["Crop_Code"].Trim(), Request.QueryString["FIN_YEAR"]==null?System.Convert.DBNull: Request.QueryString["FIN_YEAR"].Trim(), Request.QueryString["Season"].Trim() == "0" ? System.Convert.DBNull : Request.QueryString["Season"].Trim() };
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
        str.Append("<td>Dealer's Name</td>");
        str.Append("<td>Licence No.</td>");
        str.Append("<td>Mob No.</td>");
        str.Append("<td style='text-align: Right;'>Lifting Done(Received) Quantity</td>");
        str.Append("<td style='text-align: Right;'>Physical Sale Quantity</td>");
        str.Append("<td style='text-align: Right;'>Online Sale Quantity</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["APP_FIRMNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["LIC_NO"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["APPMOB_NO"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["ACTUAL_RECEIVE"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SALE_QTY"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["ACTUAL_SALE"].ToString() + "</td>");
            str.Append("</tr>");
            i++;
        }

        string SaleQty = ds.Tables[0].Compute("Sum(SALE_QTY)", "").ToString();
        string ACTUAL_SALE = ds.Tables[0].Compute("Sum(ACTUAL_SALE)", "").ToString();
        string ACTUAL_RCV = ds.Tables[0].Compute("Sum(ACTUAL_RECEIVE)", "").ToString();
        string balance = ds.Tables[0].Compute("Sum(balance)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Right;'></td>");
        str.Append("<td colspan='3' style='text-align: Right;'>Total : </td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(ACTUAL_RCV)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(SaleQty)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(ACTUAL_SALE)) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
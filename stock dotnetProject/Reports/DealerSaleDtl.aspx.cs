using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DealerSaleDtl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            SaleDtl();
        }
    }

    private void SaleDtl()
    {

        string APP_TYPE = Session["APP_TYPE"].ToString().Trim();
        string LIC_NO = Request.QueryString["LIC_NO"].ToString().Trim();
        string FRMDT = Session["FRMDT"].ToString().Trim();
        string TODT = Session["TODT"].ToString().Trim();
        string SCHEME_CODE = Session["SCHEME_CODE"].ToString().Trim();
        string type = Request.QueryString["TYPE"].ToString().Trim();
        string Crop_Code = Session["Crop_Code"].ToString().Trim();
        string SEASON = Session["SEASON"].ToString().Trim();
        string FIN_YEAR = Session["FIN_YEAR"].ToString().Trim();


        DataSet ds = new DataSet();
        string sql = "RPTDealerSaleDtl";
        string[] param = { "@APP_TYPE", "@LIC_NO", "@FRMDT", "@TODT", "@SCHEME_CODE", "@Crop_Code", "@SEASON", "@FIN_YEAR" };
        object[] value = { APP_TYPE, LIC_NO, FRMDT == "" ? System.Convert.DBNull : FRMDT, TODT == "" ? System.Convert.DBNull : TODT, SCHEME_CODE, Crop_Code == "--- All ---" ? "0" : Crop_Code, SEASON == "--- All ---" ? "0" : SEASON, FIN_YEAR == "--- All ---" ? "0" : FIN_YEAR };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // btnExport.Enabled = true;
            GenerateReport(ds);
        }
        else
        {
            // btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }

    private void GenerateReport(DataSet ds)
    {
        DataTable dt = ds.Tables[0];

        if (Request.QueryString["TYPE"].ToString().Trim() != "A")
        {
            DataRow[] result = dt.Select("TransIdP<>''");
            if (result.Length > 0)
            {
                dt = result.CopyToDataTable();
            }
            else
            {
                litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
                return;
            }
        }

        btnExport.Enabled = true;
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No. </td>");
        str.Append("<td>Transaction Id</td>");
        str.Append("<td>Farmer's Id</td>");
        str.Append("<td>Farmer's Name</td>");
        str.Append("<td>Crop Name</td>");
        str.Append("<td>Variety Name</td>");
        str.Append("<td style='text-align: Right;'>Amount</td>");
        str.Append("<td style='text-align: Right;'>Qty Taken</td>");
        str.Append("<td style='text-align: Right;'>Sale Date</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["TRANSACTION_ID"].ToString() + "</td>");
            str.Append("<td>" + dr["NICFARMERID"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHFARMERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(dr["Amount"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(dr["TOT_QTL"].ToString())) + "</td>");
            str.Append("<td>" + dr["UPDATED_ON"].ToString() + "</td>");
            str.Append("</tr>");
            i++;
        }
        string Amount = dt.Compute("Sum(Amount)", "").ToString();
        string TOT_QTL = dt.Compute("Sum(TOT_QTL)", "").ToString();
        str.Append("<td style='text-align: Right;' colspan='6'>Total : </td>");
        str.Append("<td style='text-align: Right;'>" + Amount + "</td>");

        str.Append("<td style='text-align: Right;'>" + TOT_QTL + "</td>");
        str.Append("<td style='text-align: Right;'>&nbsp;</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=Sale.xls";
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
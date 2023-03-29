using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_TransitOAICDtls : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        OAICDTLS();
    }
    private void OAICDTLS()
    {
        string Crop_Code = Request.QueryString["Crop_Code"].ToString().Trim();
        string Dist_Code = Request.QueryString["Dist_Code"].ToString().Trim();
        string FIN_YR = Request.QueryString["FIN_YR"].ToString().Trim();
        string CropCatg_ID = Request.QueryString["CropCatg_ID"].ToString().Trim();
        string Seassion = Request.QueryString["Seassion"].ToString().Trim();
        string Crop_ID = Request.QueryString["Crop_ID"].ToString().Trim();
        string Date = Request.QueryString["Date"].ToString().Trim();//Crop_ID

        DataSet ds = new DataSet();
        string sql = "STOCK_RPT_CropwiseStockPendingOAIC";
        string[] param = { "@FIN_YR ", "@CropCatg_ID", "@Crop_ID", "@DistCode", "@Seassion", "@Date" };
        object[] value = { FIN_YR == "--- All ---" ? "0" : FIN_YR, CropCatg_ID == "---SELECT---" ? "0" : CropCatg_ID, Crop_ID == "---SELECT---" ? "0" : Crop_ID, Dist_Code, Seassion == "--All--" ? "0" : Seassion, Date == "" ? System.Convert.DBNull : Date };
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
        DataTable dtData = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='5' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: left; padding-right:5px;'>DATE  </td>");
        str.Append("<td >DIST  </td>");

        str.Append("<td style='text-align: left; padding-right:5px;'>GODOWN NAME </td>");
      
       
        str.Append("<td style='text-align: left; padding-right:5px;'>CASH MEMO NO.  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>QUANTITY  </td>");
        foreach (DataRow dr in dtData.Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["UPDATED_ON"].ToString() + "</td>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Godown_Name"].ToString() + "</td>");

          
            str.Append("<td>" + dr["CASH_MEMO_NO"].ToString() + "</td>");
                str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(dr["Qty"].ToString())) + "</td>");
              

            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Grand Total</td>");
        str.Append("<td > </td>");
        str.Append("<td > </td>");
        str.Append("<td > </td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(Qty)", "").ToString()))  + "</td>");
       
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    //protected void btnToExcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string attachment = "attachment; filename=OAIC_TransitDTLS.xls";
    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", attachment);
    //        Response.ContentType = "application/ms-excel";
    //        StringWriter sWriter = new StringWriter();
    //        HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
    //        litReport.RenderControl(htwWriter);
    //        Response.Write(sWriter.ToString());
    //        Response.End();
    //    }
    //    catch (Exception ex) { }
    //}

}
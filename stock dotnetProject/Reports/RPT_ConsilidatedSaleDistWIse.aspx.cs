using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_ConsilidatedSaleDistWIse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DistwiseSale();
    }
    private void DistwiseSale()
    {

        string Crop_Code = Request.QueryString["Crop_Code"].ToString().Trim();
        string APP_TYPE = Request.QueryString["APP_TYPE"].ToString().Trim();
        string SEASON = Request.QueryString["SEASON"].ToString().Trim();
        string SCHEME_CODE = Request.QueryString["SCHEME_CODE"].ToString().Trim();
        string FIN_YEAR = Request.QueryString["FIN_YEAR"].ToString().Trim();
        string Agency = Request.QueryString["Agency"].ToString().Trim();
        string Dist_Code = Request.QueryString["Dist_Code"].ToString().Trim();
        string MONTH = Request.QueryString["MONTH"].ToString().Trim();
        string FRMDT = Request.QueryString["FRMDT"].ToString().Trim();
        string TODT = Request.QueryString["TODT"].ToString().Trim();


        DataSet ds = new DataSet();
        string sql = "RPTConsolidatedSale_DISTWISE";
        string[] param = { "@Crop_Code", "@APP_TYPE", "@SEASON", "@SCHEME_CODE", "@FIN_YEAR", "@Agency", "@Dist_Code", "@MONTH", "@FRMDT", "@TODT" };
        object[] value = { Crop_Code == "--- All ---" ? "0" : Crop_Code, APP_TYPE == "0" ? "0" : APP_TYPE, SEASON == "0" ? "0" : SEASON, SCHEME_CODE == "--- All ---" ? "0" : SCHEME_CODE, FIN_YEAR == "--- All ---" ? "0" : FIN_YEAR, Agency == "0" ? "0" : Agency, Dist_Code, MONTH == "0" ? System.Convert.DBNull : MONTH, FRMDT == "" ? System.Convert.DBNull : FRMDT, TODT == "" ? System.Convert.DBNull : TODT };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[1].Rows.Count > 0)
        {
            GenerateReport(ds);
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }
    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='10' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>Block Name  </td>");
        str.Append(" <td style='text-align: center; padding-right:5px;' colspan='3'>No. of Farmers</td>");
        str.Append(" <td style='text-align: center; padding-right:5px;' colspan='3'>Seed Sale in Qntl.</td>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Registered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>Registered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

        str.Append("</tr>");

        DataView view = new DataView(ds.Tables[0]);
       DataTable dtBlock = view.ToTable(true, "block_code", "block_name");

       foreach (DataRow dr in dtBlock.Rows)
        {
            str.Append("<tr>");

            str.Append("<td align='left' class='tbltd'>");

            str.Append("" + dr["block_name"].ToString() + "</td>");
            
            double RegdFarmer = 0;
            double UnRegdFarmer = 0;
            DataRow[] drRegdFarmer = ds.Tables[0].Select("block_code= " + dr["block_code"].ToString() + "");
            if (drRegdFarmer.Length > 0)
            {
                foreach (DataRow value in drRegdFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[5].ToString() + "</td>");
                    RegdFarmer = double.Parse(value[5].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }
            DataRow[] drUnRegdFarmer = ds.Tables[1].Select("block_code= " + dr["block_code"].ToString() + "");
            if (drUnRegdFarmer.Length > 0)
            {
                foreach (DataRow value in drUnRegdFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[5].ToString() + "</td>");
                    UnRegdFarmer = double.Parse(value[5].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (RegdFarmer + UnRegdFarmer) + "</td>");

            double RegdFarmerSale = 0;
            double UnRegdFarmerSale = 0;
            DataRow[] drRegdFarmerSale = ds.Tables[0].Select("block_code= " + dr["block_code"].ToString() + "");
            if (drRegdFarmer.Length > 0)
            {
                foreach (DataRow value in drRegdFarmerSale)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[4].ToString() + "</td>");
                    RegdFarmerSale = double.Parse(value[4].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }
            DataRow[] drUnRegdFarmerSale = ds.Tables[1].Select("block_code= " + dr["block_code"].ToString() + "");
            if (drUnRegdFarmerSale.Length > 0)
            {
                foreach (DataRow value in drUnRegdFarmerSale)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[4].ToString() + "</td>");
                    UnRegdFarmerSale = double.Parse(value[4].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", (RegdFarmerSale + UnRegdFarmerSale)) + "</td>");
            str.Append("</td>");
           
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Grand Total</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(REGFARMER)", "").ToString() + " *</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[1].Compute("Sum(UNREGFARMER)", "").ToString() + "</td>");

        double REGFARMER = ds.Tables[0].Rows.Count > 0 ? double.Parse(ds.Tables[0].Compute("Sum(REGFARMER)", "").ToString()) : 0;
        double UNREGFARMER = ds.Tables[1].Rows.Count > 0 ? double.Parse(ds.Tables[1].Compute("Sum(UNREGFARMER)", "").ToString()) : 0;

        double SALEQTY = ds.Tables[0].Rows.Count > 0 ? double.Parse(ds.Tables[0].Compute("Sum(SALEQTY)", "").ToString()) : 0;
        double UNREGSALE = ds.Tables[1].Rows.Count > 0 ? double.Parse(ds.Tables[1].Compute("Sum(UNREGSALE)", "").ToString()) : 0;

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (REGFARMER + UNREGFARMER) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + SALEQTY + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + UNREGSALE + "</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (SALEQTY + UNREGSALE) + "</td>");
       // str.Append(" <td style='text-align: Right; padding-right:5px;'>&nbsp;</td>");
        str.Append("</tr>");

        str.Append("</table>");
        str.Append("<div style='color:Red; padding-top:20px; font-family: Tahoma, Geneva, sans-serif;'>");
        str.Append("<b>* Note : </b>District is taken as a unit for seed sale, so a single farmer can purchase from multiple blocks. Total number of farmer from each block may not be equal to total distinct farmers as a whole.");
        str.Append("</div>");
        litReport.Text = str.ToString();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DistwiseConsolidatedSale.xls";
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
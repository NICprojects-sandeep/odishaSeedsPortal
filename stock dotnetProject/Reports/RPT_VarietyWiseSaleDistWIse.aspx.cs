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

public partial class Reports_RPT_VarietyWiseSaleDistWIse : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        DistWise();
    }
    private void DistWise()
    { 
        string Crop_Code = Request.QueryString["Crop_Code"].ToString().Trim();
        string Dist_Code = Request.QueryString["Dist_Code"].ToString().Trim();
        string FIN_YEAR = Request.QueryString["FIN_YEAR"].ToString().Trim();
        string Seassion = Request.QueryString["Seassion"].ToString().Trim();
        string SCHEME_CODE = Request.QueryString["SCHEME_CODE"].ToString().Trim();
        string USER_TYPE = Request.QueryString["USER_TYPE"].ToString().Trim();
        string MONTH = Request.QueryString["MONTH"].ToString().Trim();
        string FRMDT = Request.QueryString["FRMDT"].ToString().Trim();
        string TODT = Request.QueryString["TODT"].ToString().Trim();

        DataSet ds = new DataSet();
        string sql = "STOCK_RPT_SaleVarietywiseDistWise";
        string[] param = { "@Crop_Code", "@Dist_Code","@FIN_YR", "@Seassion", "@SCHEME_CODE", "@USER_TYPE", "@MONTH", "@FRMDT", "@TODT "};
        object[] value = { Crop_Code == "--- All ---" ? System.Convert.DBNull : Crop_Code, Dist_Code, FIN_YEAR == "" ? System.Convert.DBNull : FIN_YEAR, Seassion == "0" ? System.Convert.DBNull : Seassion, SCHEME_CODE == "--- All ---" ? System.Convert.DBNull : SCHEME_CODE, USER_TYPE == "0" ? System.Convert.DBNull : USER_TYPE, MONTH == "0" ? System.Convert.DBNull : MONTH, FRMDT == "" ? System.Convert.DBNull : FRMDT, TODT == "" ? System.Convert.DBNull : TODT };
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
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Block Name  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>No. of farmer  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>Unregistered Farmer  </td>");
        DataView view = new DataView(ds.Tables[0]);
        view.Sort = "CROPCATG_ID ASC";
        DataTable dtVariety = view.ToTable(true, "CROP_VERID", "Variety_Name");
        foreach (DataRow dr in dtVariety.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + dr["Variety_Name"].ToString() + "</td>");
        }
        DataView view1 = new DataView(ds.Tables[0]);
        DataTable dtBlock = view1.ToTable(true, "block_code", "block_name");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");
        str.Append("</tr>");


        foreach (DataRow dr in dtBlock.Rows)
        {
            str.Append("<tr>");

            str.Append("<td align='left' class='tbltd'>");
            str.Append("" + dr["block_name"].ToString() + "</td>");


            DataRow[] NoofFarmer = ds.Tables[1].Select("block_code= " + dr["block_code"].ToString() + "");
            if (NoofFarmer.Length > 0)
            {
                foreach (DataRow value in NoofFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] uNoofFarmer = ds.Tables[2].Select("block_code= " + dr["block_code"].ToString() + "");
            if (uNoofFarmer.Length > 0)
            {
                foreach (DataRow value in uNoofFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            double TotSale = 0;
            foreach (DataRow drv in dtVariety.Rows)
            {
                DataRow[] Data = ds.Tables[0].Select("block_code= " + dr["block_code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "'");
                if (Data.Length > 0)
                {
                    foreach (DataRow value in Data)
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[8].ToString() + "</td>");
                        TotSale = TotSale + double.Parse(value[8].ToString());
                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
                }
            }
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotSale) + "</td>");
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append("<td>Grand Total</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[1].Compute("Sum(NoofFarmer)", "").ToString() + "*</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[2].Compute("Sum(NoofFarmer)", "").ToString() + "</td>");
        foreach (DataRow dr in dtVariety.Rows)
        {
            str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "'").ToString())) + "</td>");
        }
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(SALE)", "").ToString())) + "</td>");
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
            string attachment = "attachment; filename=DistwiseVarietyWiseSale.xls";
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
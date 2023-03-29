using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_DatewiseSale : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            DatewiseSale();
        }
    }

    private void DatewiseSale()
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
        string sql = "RPTDatewiseSale";
        string[] param = { "@Crop_Code", "@APP_TYPE", "@SEASON", "@SCHEME_CODE", "@FIN_YEAR", "@Agency", "@Dist_Code", "@MONTH", "@FRMDT", "@TODT" };
        object[] value = { Crop_Code == "--- All ---" ? "0" : Crop_Code, APP_TYPE == "0" ? "0" : APP_TYPE, SEASON == "0" ? "0" : SEASON, SCHEME_CODE == "--- All ---" ? "0" : SCHEME_CODE, FIN_YEAR == "--- All ---" ? "0" : FIN_YEAR, Agency == "0" ? "0" : Agency, Dist_Code , MONTH =="0" ? System.Convert.DBNull : MONTH, FRMDT == "" ? System.Convert.DBNull : FRMDT, TODT == "" ?System.Convert.DBNull : TODT };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[1].Rows.Count > 0)
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
        DataTable dtUnion = ds.Tables[1].AsEnumerable()
      .Union(ds.Tables[2].AsEnumerable()).CopyToDataTable<DataRow>();

        DataView view = new DataView(dtUnion);
        view.Sort = "ENTRY_DATE DESC";
        DataTable distinctValues = view.ToTable(true, "ENTRY_DATE");

        StringBuilder str = new StringBuilder("");
        str.Append("<div class='tbltxt' style='font-weight:bold;'> Dist Name : ");
        str.Append(ds.Tables[0].Rows[0][1].ToString());
        str.Append("</div>");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>Date  </td>");
        str.Append(" <td style='text-align: center; padding-right:5px;' colspan='3'>No. of Transaction</td>");
        str.Append(" <td style='text-align: center; padding-right:5px;' colspan='3'>Seed Sale in Qntl.</td>");
        str.Append("</tr>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Registered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>Registered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

        str.Append("</tr>");
        foreach (DataRow dr in distinctValues.Rows)
        {
            str.Append("<tr>");
            DataRow[] drDATE = ds.Tables[1].Select("ENTRY_DATE= '" + dr["ENTRY_DATE"].ToString() + "'");
            if (drDATE.Length > 0)
            {

                str.Append(" <td style='padding-right:5px;'>" + drDATE[0][2].ToString() + "</td>");

            }
            else
            {
                DataRow[] drDATE2 = ds.Tables[2].Select("ENTRY_DATE= '" + dr["ENTRY_DATE"].ToString() + "'");
                str.Append(" <td style='padding-right:5px;'>" + drDATE2[0][2].ToString() + "</td>");
            }

            double RegdFarmer = 0;
            double UnRegdFarmer = 0;
            //-------------------
            string drRegdFarmer = ds.Tables[1].Compute("Sum(REGFARMER)", "ENTRY_DATE= '" + dr["ENTRY_DATE"].ToString() + "'").ToString();
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + drRegdFarmer + "</td>");
            RegdFarmer = drRegdFarmer != "" ? double.Parse(drRegdFarmer) : 0;
            //-------------------
            string drUnRegdFarmer = ds.Tables[2].Compute("Sum(UNREGFARMER)", "ENTRY_DATE= '" + dr["ENTRY_DATE"].ToString() + "'").ToString();
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + drUnRegdFarmer + "</td>");
            UnRegdFarmer = drUnRegdFarmer != "" ? double.Parse(drUnRegdFarmer) : 0;


            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (RegdFarmer + UnRegdFarmer) + "</td>");

            double RegdFarmerSale = 0;
            double UnRegdFarmerSale = 0;
            string drRegdFarmerSale = ds.Tables[1].Compute("Sum(SALEQTY)", "ENTRY_DATE= '" + dr["ENTRY_DATE"].ToString() + "'").ToString();

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + drRegdFarmerSale + "</td>");
            RegdFarmerSale = drRegdFarmerSale != "" ? double.Parse(drRegdFarmerSale) : 0;

            string drUnRegdFarmerSale = ds.Tables[2].Compute("Sum(UNREGSALE)", "ENTRY_DATE= '" + dr["ENTRY_DATE"].ToString() + "'").ToString();

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + drUnRegdFarmerSale + "</td>");
            UnRegdFarmerSale = drUnRegdFarmerSale != "" ? double.Parse(drUnRegdFarmerSale) : 0;


            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", (RegdFarmerSale + UnRegdFarmerSale)) + "</td>");


            str.Append("</tr>");
        }

        double REGFARMER = ds.Tables[1].Compute("Sum(REGFARMER)", "").ToString() != "" ? double.Parse(ds.Tables[1].Compute("Sum(REGFARMER)", "").ToString()) : 0;
        double UNREGFARMER = ds.Tables[2].Compute("Sum(UNREGFARMER)", "").ToString() != "" ? double.Parse(ds.Tables[2].Compute("Sum(UNREGFARMER)", "").ToString()) : 0;

        double SALEQTY = ds.Tables[1].Compute("Sum(SALEQTY)", "").ToString() != "" ? double.Parse(ds.Tables[1].Compute("Sum(SALEQTY)", "").ToString()) : 0;
        double UNREGSALE = ds.Tables[2].Compute("Sum(UNREGSALE)", "").ToString() != "" ? double.Parse(ds.Tables[2].Compute("Sum(UNREGSALE)", "").ToString()) : 0;




        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total : </td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(REGFARMER)", "").ToString() + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[2].Compute("Sum(UNREGFARMER)", "").ToString() + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (REGFARMER + UNREGFARMER) + "</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALEQTY)", "").ToString() + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[2].Compute("Sum(UNREGSALE)", "").ToString() + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", (SALEQTY + UNREGSALE)) + "</td>");

        str.Append("</tr>");

        str.Append("</table>");
        litReport.Text = str.ToString();
    }

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DatewiseSale.xls";
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
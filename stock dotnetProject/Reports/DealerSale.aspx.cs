using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DealerSale : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();
    private UtilityLibrary utl = new UtilityLibrary();
    dbsAppStock dbsApp = new dbsAppStock();
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
            if (this.Session["UserType"].ToString() != "DIST")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                btnExport.Enabled = false;
                BindDealerSale();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
    }

    private void BindDealerSale()
    {
        DataSet dt = new DataSet();
        string sql = "DatewiseSale";
        string[] param = { "@LIC_NO", "@FrmDt", "@Todate" };
        object[] value = { Session["LIC_NO"].ToString(), "", "" };
        dt = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (dt.Tables[0].Rows.Count > 0)
        {

            GenerateReport(dt.Tables[0]);
            btnExport.Enabled = true;

        }
    }

    private void GenerateReport(DataTable dt)
    {
        btnExport.Enabled = true;
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No. </td>");
        str.Append("<td>Sale Date</td>");
        str.Append("<td>Transaction Id</td>");
        str.Append("<td>Farmer's Id</td>");
        str.Append("<td>Farmer's Name</td>");
        str.Append("<td>CropCategory</td>");
        str.Append("<td>Crop Name</td>");
        str.Append("<td>Variety Name</td>");
        str.Append("<td style='text-align: Right;'>Bag Size(In kg.)</td>");
        str.Append("<td style='text-align: Right;'>No. of Bags</td>");
        str.Append("<td style='text-align: Right;'>Sale Quantity</td>");
        str.Append("<td style='text-align: Right;'>Subsidy Amt</td>");
        str.Append("<td style='text-align: Right;'>Govt. of India</td>");
        str.Append("<td style='text-align: Right;'>State Plan</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["SALE_DATE"].ToString() + "</td>");
            str.Append("<td>" + dr["TRANSACTION_ID"].ToString() + "</td>");
            str.Append("<td>" + dr["NICFARMERID"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHFARMERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["Category_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["BAG_SIZE_KG"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["NO_OF_BAGS"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_QTL"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(dr["SUBSIDY_AMOUNT"].ToString())) + "</td>");
            str.Append("<td>" + dr["GOI"].ToString() + "</td>");
            str.Append("<td>" + dr["SP"].ToString() + "</td>");
            str.Append("</tr>");
            i++;
        }
        string TotBag = dt.Compute("Sum(NO_OF_BAGS)", "").ToString();
        string TotsalQty = dt.Compute("Sum(TOT_QTL)", "").ToString();
        string TotQty = dt.Compute("Sum(SUBSIDY_AMOUNT)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;' colspan='9'>Total : </td>");
        str.Append("<td style='text-align: Right;'>" + TotBag + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(TotsalQty)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(TotQty)) + "</td>");
        str.Append("<td>&nbsp;</td>");
        str.Append("<td>&nbsp;</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DealerSaleDtl.xls";
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet dt = new DataSet();
        string sql = "DatewiseSale";
        string[] param = { "@LIC_NO", "@FrmDt", "@Todate" };
        object[] value = { Session["LIC_NO"].ToString(), txtFromDate.Text.ToString() == "" ? "" : DateTime.Parse(txtFromDate.Text.ToString()).ToString("yyyy/MM/dd")+" 00:00:000", DateTime.Parse(txtToDate.Text.ToString()).ToString("yyyy/MM/dd")+" 23:59:59" };
        dt = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (dt.Tables[0].Rows.Count > 0)
        {

            GenerateReport(dt.Tables[0]);
            btnExport.Enabled = true;

        }
    }
}
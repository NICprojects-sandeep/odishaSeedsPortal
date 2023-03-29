using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Reports_RPT_SaleDtl : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = Session["DistCode"].ToString();
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            DataSet ds = new DataSet();
            ds = objUserDLL.FillGoDownByDistCodeUserType(objUserBEL);
            drpGodown.DataSource = ds.Tables[0];
            drpGodown.DataValueField = "GODOWN_ID";
            drpGodown.DataTextField = "GODOWN_NAME";
            drpGodown.DataBind();
            drpGodown.Items.Insert(0, "---All---");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "STOCK_RPT_Saledtl";
        string[] param = { "@Dist_Code", "@Godown_ID", "@User_Type", "@FromDt", "@ToDt", "@Season" };
        object[] value = { Session["DistCode"].ToString(), drpGodown.SelectedValue.Trim() == "---All---" ? "0" : drpGodown.SelectedValue.Trim(), Session["UserType"].ToString(), txtFrmDt.Text.Trim(), txtToDt.Text.Trim(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull
 : drpSeassion.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds);
            btnExport.Enabled = true;

        }
        else
        {
            btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";

        }

    }

    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No. </td>");
        str.Append("<td>Sale Date</td>");
        str.Append("<td>Godown Name</td>");
        str.Append("<td>Cash Memo No.</td>");
        str.Append("<td>Lot No.</td>");
        str.Append("<td>Supplier Name</td>");
        str.Append("<td>Crop Name</td>");
        str.Append("<td>Variety Name</td>");
        str.Append("<td style='text-align: Right;'>Bag Size(In kg.)</td>");
        str.Append("<td style='text-align: Right;'>No. of Bags</td>");
        str.Append("<td style='text-align: Right;'>Sale Quantity</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["SALE_DATE"].ToString() + "</td>");
            str.Append("<td>" + dr["Godown_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["CASH_MEMO_NO"].ToString() + "</td>");
            str.Append("<td>" + dr["LOT_NUMBER"].ToString() + "</td>");
            str.Append("<td>" + dr["SUPPLY_NAME"].ToString() + "</td>");
            str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["BAG_SIZE_KG"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SALE_NO_OF_BAG"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(dr["Sale_Quantity"].ToString())) + "</td>");
            str.Append("</tr>");
            i++;

        }
        string TotBag = ds.Tables[0].Compute("Sum(SALE_NO_OF_BAG)", "").ToString();
        string TotQty = ds.Tables[0].Compute("Sum(Sale_Quantity)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;' colspan='9'>Total : </td>");
        str.Append("<td style='text-align: Right;'>" + TotBag + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(TotQty)) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=SaleDtl.xls";
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
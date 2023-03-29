using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class Dealer_RPT_DealerSale : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnExport.Enabled = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //string strFDate = txtFrmDt.Text.Substring(9, 2) + "/" +   txtFrmDt.Text.Substring(6, 2) + "/" +    txtFrmDt.Text.Substring(0, 4);
        //string strTDate = txtToDt.Text.Substring(9, 2) + "/" + txtToDt.Text.Substring(6, 2) + "/" + txtToDt.Text.Substring(0, 4);
        //DateTime strFDate=Convert.ToDateTime(txtFrmDt.Text.Trim());
        //string strFDate1 = strFDate.ToString("dd/MM/yyyy");
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserBELDIST.FDate = txtFrmDt.Text.Trim();
        objUserBELDIST.TDate = txtToDt.Text.Trim();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.RptDateWiseSale(objUserBELDIST);
        if (ds != null)
        {
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
    }

    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        StringBuilder str = new StringBuilder("");
        str.Append("<table width='100%' border='1' cellpadding='5' cellspacing='0' class='table table-bordered'>");

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
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["SALE_DATE"].ToString() + "</td>");
            str.Append("<td><a  href='RptSaleVoucher.aspx?Tid=" + dr["TRANSACTION_ID_Q"].ToString() + " &FId=" + dr["NICFARMERID"].ToString() + " &LicNo=" + Session["LIC_NO"].ToString() + "' target='_blank'>" + dr["TRANSACTION_ID"].ToString() + "</a></td>");
            str.Append("<td>" + dr["NICFARMERID"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHFARMERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["Category_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["BAG_SIZE_KG"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["NO_OF_BAGS"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(dr["TOT_QTL"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(dr["SUBSIDY_AMOUNT"].ToString())) + "</td>");
            str.Append("</tr>");
            i++;
        }
        string TotBag = ds.Tables[0].Compute("Sum(NO_OF_BAGS)", "").ToString();
        string TotsalQty = ds.Tables[0].Compute("Sum(TOT_QTL)", "").ToString();
        string TotQty = ds.Tables[0].Compute("Sum(SUBSIDY_AMOUNT)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;' colspan='9'>Total : </td>");
        str.Append("<td style='text-align: Right;'>" + TotBag + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(TotsalQty)) + "</td>");
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
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dealer_RecvDetails : System.Web.UI.Page
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
        ds = objUserDLLDIST.RECPTDEALER(objUserBELDIST);
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
        str.Append("<td>Cash Memo No Id</td>");
        str.Append("<td>DD Number</td>");
        str.Append("<td>DD Amount</td>");
        str.Append("<td>Fin Year</td>");
        str.Append("<td>Season</td>");
        str.Append("<td>Updated On</td>");        
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["SALE_DATE"].ToString() + "</td>");            
            str.Append("<td><a  href='RptRcptVoucher.aspx?Tid=" + dr["CASH_MEMO_NO"].ToString() + "'>" + dr["CASH_MEMO_NO"].ToString() + "</a></td>");
            str.Append("<td>" + dr["DD_NUMBER"].ToString() + "</td>");
            str.Append("<td>" + dr["DD_AMOUNT"].ToString() + "</td>");
            str.Append("<td>" + dr["F_YEAR"].ToString() + "</td>");
            str.Append("<td>" + dr["SEASONS"].ToString() + "</td>");
            str.Append("<td>" + dr["UPDATED_ON"].ToString() + "</td>");
            
            str.Append("</tr>");
            i++;
        }        
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=ReceivedDtl.xls";
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
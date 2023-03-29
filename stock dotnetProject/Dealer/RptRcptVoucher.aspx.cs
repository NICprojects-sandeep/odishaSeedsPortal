using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dealer_RptRcptVoucher : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request.QueryString["Tid"] != null && Request.QueryString["Tid"] != string.Empty)
                lblCashmemono.Text = Request.QueryString["Tid"];
            GetCashMemoDtl(lblCashmemono.Text);
        }
    }

    private void GetCashMemoDtl(string CashMemoNo)
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.CASH_MEO_NO = CashMemoNo;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.RECVDTL_CASHMEMONO(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GenerateReport(ds);
            }
        }
    }
    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table width='100%' border='1' cellpadding='5' cellspacing='0' class='table table-bordered'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No. </td>");
        str.Append("<td>Crop Name</td>");
        str.Append("<td>Variety Name</td>");
        str.Append("<td>Lot Number</td>");
        str.Append("<td>Bag Size</td>");
        str.Append("<td>No Of Bags</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["LOT_NUMBER"].ToString() + "</td>");
            str.Append("<td>" + dr["BAG_SIZE_KG"].ToString() + "</td>");
            str.Append("<td>" + dr["SALE_NO_OF_BAG"].ToString() + "</td>");

            str.Append("</tr>");
            i++;
        }
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RejectionofRejectecFRMR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RjctnReason();
    }

    private void RjctnReason()
    {

       
        string VCHFARMERCODE = Request.QueryString["VCHFARMERCODE"].ToString().Trim();
        
        DataSet ds = new DataSet();
        string sql = "SHOW_RejectionReason";
        string[] param = { "@VCHFARMERCODE" };
        object[] value = { VCHFARMERCODE};
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GenerateReport(ds);
            }
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>Account Number is not Valid</div>";
        }
    }
    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='0' cellspacing='0' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Reasons of Rejection </td>");
        str.Append("<td>Date of Rejection </td>");
        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["Rejection_Reason_Narration"].ToString() + "</td>");
            str.Append("<td>" + dr["DATE"].ToString() + "</td>");
            str.Append("</tr>");
        }
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
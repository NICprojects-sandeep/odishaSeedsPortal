using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_rptCCBResponse : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillData();
        }
    }

    private void FillData()
    {
        string sql = "Sp_DistwiseCCBResponsePayment";
        string[] param = {  };
        object[] value = {  };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds);
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }

    }
    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No.</td>");
        str.Append("<td>Dist Name</td>");
        str.Append("<td style='text-align: Right;'>STATE PLAN</td>");
        str.Append("<td style='text-align: Right;'>NFSM</td>");
        str.Append("<td style='text-align: Right;'>NMOOP</td>");
        str.Append("<td style='text-align: Right;'>RKVY</td>");
        str.Append("<td style='text-align: Right;'>TOTAL</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SPAMNT"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["NFSMAMNT"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["NMOOPAMNT"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["RKVYAMNT"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["PAIDAMNT"].ToString() + "</td>");
            str.Append("</tr>");
            i++;
        }

        string SPAMNT = ds.Tables[0].Compute("Sum(SPAMNT)", "").ToString();
        string NFSMAMNT = ds.Tables[0].Compute("Sum(NFSMAMNT)", "").ToString();
        string NMOOPAMNT = ds.Tables[0].Compute("Sum(NMOOPAMNT)", "").ToString();
        string RKVYAMNT = ds.Tables[0].Compute("Sum(RKVYAMNT)", "").ToString();
        string PAIDAMNT = ds.Tables[0].Compute("Sum(PAIDAMNT)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Right;'></td>");
        str.Append("<td >Total : </td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(SPAMNT)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(NFSMAMNT)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(NMOOPAMNT)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(RKVYAMNT)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(PAIDAMNT)) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DataTransaction : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ds = new DataSet();
            string sql = "DataTransaction";
            string[] param = { };
            object[] value = { };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            GenerateReport(ds);
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (this.Session["UserType"].ToString() == "ADMI")
        {
            this.Page.MasterPageFile = "../MasterPages/Admin.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "SEAD")
        {
            this.Page.MasterPageFile = "../MasterPages/SeedAdmin.master";
            return;
        }
    }
    private void GenerateReport(DataSet ds)
    {
        double Tot = 0;
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        if (ds.Tables[0].Rows.Count > 0)
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Data Received From CSM</td>");
            str.Append("<td>" + ds.Tables[0].Rows[0][0].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;Data Received from CSM as on 1st June 2016&nbsp;)</td>");
            str.Append("</tr>");

        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Data send to PFMS for Account Validation</td>");
            str.Append("<td>" + ds.Tables[1].Rows[0][0].ToString() + "</td>");
            str.Append("</tr>");
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Data returned from PFMS</td>");
            str.Append("<td>" + ds.Tables[2].Rows[0][0].ToString() + "</td>");
            str.Append("</tr>");
        }
        if (ds.Tables[3].Rows.Count > 0)
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Validated  Data from PFMS</td>");
            str.Append("<td>" + ds.Tables[3].Rows[0][0].ToString() + "</td>");
            str.Append("</tr>");
        }

        if (ds.Tables[4].Rows.Count > 0)
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Validated  Data from CCB</td>");
            str.Append("<td>" + ds.Tables[4].Rows[0][0].ToString() + "</td>");
            str.Append("</tr>");
        }

        if (ds.Tables[5].Rows.Count > 0)
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Rejected  Data from CCB</td>");
            str.Append("<td>" + ds.Tables[5].Rows[0][0].ToString() + "</td>");
            str.Append("</tr>");
        }
        if (ds.Tables[7].Rows.Count > 0)
        {

            double a = double.Parse(ds.Tables[7].Rows[0][0].ToString());
            double b = double.Parse(ds.Tables[3].Rows[0][0].ToString());
            double c = double.Parse(ds.Tables[4].Rows[0][0].ToString());
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Rejected by NIC</td>");
            str.Append("<td>" + ((b + c) - a) + "</td>");
            str.Append("</tr>");
        }
        if (ds.Tables[6].Rows.Count > 0)
        {
            str.Append("<tr style='background-color:#A6B639; font-weight:bold;'>");
            str.Append("<td>No of Rejection</td>");
            str.Append("<td>Reason for Rejection</td>");
            str.Append("</tr>");

            foreach (DataRow item in ds.Tables[6].Rows)
            {
                str.Append("<tr>");
                str.Append("<td style='text-align: left;'>" + item["NoofRejection"].ToString() + "</td>");
                str.Append("<td style='text-align: left;'>" + item["Rejection_Reason_Narration"].ToString() + "</td>");
                str.Append("</tr>");
                Tot = Tot + double.Parse(item["NoofRejection"].ToString());
            }
        }
        str.Append("<tr>");
        str.Append("<td style='text-align: left; font-size: 14px; color:#8C2108; font-weight: bold;'>" + Tot + "</td>");
        str.Append("<td style='text-align: left;'>Total No. of Rejection</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
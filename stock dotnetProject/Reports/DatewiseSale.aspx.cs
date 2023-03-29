using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DatewiseSale : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ds = new DataSet();
            string sql = "Datewisecompare1";
            string[] param = { };
            object[] value = { };
            ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
            GenerateReport(ds);
        }
    }
    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        str.Append("<tr>");
        str.Append("<td style='text-align: left; font-size: 14px; color:#8C2108; font-weight: bold;'>Date-Month</td>");
        str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>Total Sale in Qntl. (2016-17)</td>");
        str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>Total Sale in Qntl. (2017-18)</td>");
        str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>Total Sale in Qntl. (2018-19)</td>");
        str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>Total Sale in Qntl. (2019-20)</td>");
        str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>Total Sale in Qntl. (2020-21)</td>");
        str.Append("</tr>");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            str.Append("<tr>");
            str.Append("<td style='text-align: left; font-size: 14px; color:#8C2108; font-weight: bold;'>" + dr["date"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>" + string.Format("{0:f2}", double.Parse(dr["frst"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>" + string.Format("{0:f2}", double.Parse(dr["scnd"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>" + string.Format("{0:f2}", double.Parse(dr["thrd"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>" + string.Format("{0:f2}", double.Parse(dr["fourth"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>" + string.Format("{0:f2}", double.Parse(dr["fifth"].ToString())) + "</td>");
            str.Append("</tr>");


        }
        str.Append("<tr style='text-align: left; font-size: 14px; color:#8C2108; font-weight: bold;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(frst)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(scnd)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(thrd)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(fourth)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[0].Compute("Sum(fifth)", "").ToString())) + "</td>");
        str.Append("</tr>");

        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
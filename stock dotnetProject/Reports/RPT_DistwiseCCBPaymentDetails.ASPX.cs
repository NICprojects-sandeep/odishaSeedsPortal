using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class Reports_RPT_DistwiseCCBPaymentDetails : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
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
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                FillData();
            }
        }
    }
    private void FillData()
    {
        string sql = "Sp_DistwiseCCBPayment";
        string[] param = { };
        object[] value = { };
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
        DataView view = new DataView(ds.Tables[0]);


        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");

        str.Append("<td rowspan='2'>District Name</td>");
        str.Append("<td rowspan='2'>No. of Transaction</td>");
        str.Append("<td rowspan='2'>Total No. of Farmer</td>");
        str.Append("<td rowspan='2'>Total Amount</td>");
        str.Append("<td colspan='3' style='text-align: center;'>Request</td>");
        str.Append("<td colspan='3'style='text-align: center;'>Subsidy Paid</td>");
        str.Append("<td></td>");
        str.Append("</tr>");

        //Transaction
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");

        str.Append("<td>No. of Transaction</td>");
        str.Append("<td >Total No. of Farmer</td>");
        str.Append("<td >Total Amount</td>");

        //Subsidiy

        str.Append("<td>No.of Transaction</td>");
        str.Append("<td >Total No. of Farmer</td>");
        str.Append("<td >Total Amount ");
        string tmpstr111 = "<a href='javascript:popUp";
        string tempStr11 = "('rptCCBResponse.aspx')";
        string tmpstr211 = "'>";
        str.Append(tmpstr111.Replace('\'', '\"') + tempStr11 + tmpstr211.Replace('\'', '\"'));
        str.Append("<img src='../images/binoculars.png'  title='View Details' /></a></td>");
        str.Append("<td >Balance Amount</td>");
        str.Append("</tr>");


        foreach (DataRow item in ds.Tables[0].Rows)
        {
            str.Append("<tr>");

            str.Append("<td>" + item["Dist_Name"].ToString() + "</td>");
            str.Append("<td>" + item["TotTrans"].ToString() + "</td>");
            str.Append("<td>" + item["TotFarmer"].ToString() + "</td>");
            str.Append("<td>" + item["TOTAMNT"].ToString() + "</td>");
            str.Append("<td>" + item["ReqTrans"].ToString() + "</td>");
            str.Append("<td>" + item["ReqFarmer"].ToString() + "</td>");
            str.Append("<td>" + item["REQAMNT"].ToString() + "</td>");
            str.Append("<td>" + item["PaidTrans"].ToString() + "</td>");
            str.Append("<td>" + item["PaidFarmer"].ToString() + "</td>");
            str.Append("<td>" + item["PAIDAMNT"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (double.Parse(item["TOTAMNT"].ToString()) - double.Parse(item["REQAMNT"].ToString()))) + "</td>");
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Right;'>Total :</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(TotTrans)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(TotFarmer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(TOTAMNT)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(ReqTrans)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(ReqFarmer)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(REQAMNT)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(PaidTrans)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(PaidFarmer)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Tables[0].Compute("Sum(PAIDAMNT)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (double.Parse(ds.Tables[0].Compute("Sum(TOTAMNT)", "").ToString()) - double.Parse(ds.Tables[0].Compute("Sum(REQAMNT)", "").ToString()))) + "</td>");

        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
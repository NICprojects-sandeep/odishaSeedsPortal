using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DataTransaction1 : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ds = new DataSet();
            string sql = "DataTransaction1";
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
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>District Name </td>");
        str.Append("<td style='text-align: Right;'>No. of Registration</td>");
        str.Append("<td style='text-align: Right;'>No. of Farmer Account Validation Successfully By PFMS</td>");
        str.Append("<td style='text-align: Right;'>No. of Rejection</td>");

        str.Append("<td style='text-align: Right;'>No. of Farmer Account Validation Successfully By CCB</td>");
        str.Append("<td style='text-align: Right;'>No. of Rejection By CCB</td>");
        str.Append("<td style='text-align: Right;'>No. of Rejection By NIC</td>");

        str.Append("<td style='text-align: Right;'>No. of FarmerId Generated After Internal Validation</td>");
        str.Append("</tr>");
        double NoofRegd = 0;
        double Accp = 0;
        double Reject = 0;
        double Accpccb = 0;
        double Rejectccb = 0;
        double RejectNIC = 0;
        double NoofValidFarm = 0;
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td style='text-align: left;'>" + item["Dist_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["NoofRegd"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["NoofAcpt"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["NoofRjct"].ToString() + "</td>");


            str.Append("<td style='text-align: Right;'>" + item["NoofCCPAccpt"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["NoofCCPRjct"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + ((double.Parse(item["NoofAcpt"].ToString()) + double.Parse(item["NoofCCPAccpt"].ToString())) - double.Parse(item["NooffARMER"].ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + item["NoofValidFarm"].ToString() + "</td>");
            str.Append("</tr>");
            NoofRegd = NoofRegd + double.Parse(item["NoofRegd"].ToString());
            Accp = Accp + double.Parse(item["NoofAcpt"].ToString());
            Reject = Reject + double.Parse(item["NoofRjct"].ToString());

            Accpccb = Accpccb + double.Parse(item["NoofCCPAccpt"].ToString());
            Rejectccb = Rejectccb + double.Parse(item["NoofCCPRjct"].ToString());

            RejectNIC = RejectNIC + ((double.Parse(item["NoofAcpt"].ToString()) + double.Parse(item["NoofCCPAccpt"].ToString())) - double.Parse(item["NooffARMER"].ToString()));

            NoofValidFarm = NoofValidFarm + double.Parse(item["NoofValidFarm"].ToString());
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: left;'>Total</td>");
        str.Append("<td style='text-align: Right;'>" + NoofRegd + "</td>");
        str.Append("<td style='text-align: Right;'>" + Accp + "</td>");
        str.Append("<td style='text-align: Right;'>" + Reject + "</td>");
        str.Append("<td style='text-align: Right;'>" + Accpccb + "</td>");
        str.Append("<td style='text-align: Right;'>" + Rejectccb + "</td>");
        str.Append("<td style='text-align: Right;'>" + RejectNIC + "</td>");

        str.Append("<td style='text-align: Right;'>" + NoofValidFarm + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
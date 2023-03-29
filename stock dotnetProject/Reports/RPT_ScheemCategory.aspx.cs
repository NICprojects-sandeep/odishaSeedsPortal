using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_ScheemCategory : System.Web.UI.Page
{
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

            filldata();
        }

    }

    private void filldata()
    {
        DataSet ds = new DataSet();
        string sql = "STOCK_RPT_ScheemCategorySeasonwisePayment";
        string[] param = { };
        object[] value = { };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds.Tables[0]);

        }
    }

    private void GenerateReport(DataTable dtData)
    {
        DataView view = new DataView(dtData);
        DataTable distinctType = view.ToTable(true, "Type");
        DataTable distinctSCHEME_CODE = view.ToTable(true, "SCHEME_CODE");
        StringBuilder str = new StringBuilder("");
        foreach (DataRow dr in distinctType.Rows)
        {

            str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; margin-top:20px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td colspan='9' style='color:RED;'>" + dr["Type"].ToString() + "</td>");

            str.Append("</tr>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'><td rowspan='2'>Scheme Name </td>");
            str.Append("<td colspan='4' style='text-align: center;'>Kharif</td>");
            str.Append("<td colspan='4' style='text-align: center;'>Rabi</td>");
            str.Append("</tr>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td style='text-align: Right;'>General</td>");
            str.Append("<td style='text-align: Right;'>SC</td>");
            str.Append("<td style='text-align: Right;'>ST</td>");
            str.Append("<td style='text-align: Right;'>Total</td>");
            str.Append("<td style='text-align: Right;'>General</td>");
            str.Append("<td style='text-align: Right;'>SC</td>");
            str.Append("<td style='text-align: Right;'>ST</td>");
            str.Append("<td style='text-align: Right;'>Total</td>");
            str.Append("</tr>");
            foreach (DataRow scheems in distinctSCHEME_CODE.Rows)
            {

                str.Append("<tr>");
                str.Append("<td>" + scheems["SCHEME_CODE"].ToString() + "</td>");
                string KGen = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='K' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='General'").ToString();

                string KSC = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='K' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='SC'").ToString();

                string KST = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='K' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='ST'").ToString();

                string KTot = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='K' AND Type='" + dr["Type"].ToString() + "'").ToString();

                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(KGen == "" ? "0" : KGen)) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(KSC == "" ? "0" : KSC)) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(KST == "" ? "0" : KST)) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(KTot == "" ? "0" : KTot)) + "</td>");

                string RGen = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='R' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='General'").ToString();

                string RSC = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='R' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='SC'").ToString();

                string RST = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='R' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='ST'").ToString();

                string RTot = dtData.Compute("Sum(PAIDAMNT)", "SCHEME_CODE='" + scheems["SCHEME_CODE"].ToString() + "' AND SEASON='R' AND Type='" + dr["Type"].ToString() + "'").ToString();


                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(RGen == "" ? "0" : RGen)) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(RSC == "" ? "0" : RSC)) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(RST == "" ? "0" : RST)) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(RTot == "" ? "0" : RTot)) + "</td>");
                str.Append("</tr>");


            }

            string GKGen = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='General'").ToString();

            string GKSC = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='SC'").ToString();

            string GKST = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='ST'").ToString();

            string GKTot = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K' AND Type='" + dr["Type"].ToString() + "'").ToString();

            string GRGen = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='General'").ToString();

            string GRSC = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='SC'").ToString();

            string GRST = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R' AND Type='" + dr["Type"].ToString() + "' AND CATEGOGY='ST'").ToString();

            string GRTot = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R' AND Type='" + dr["Type"].ToString() + "'").ToString();


            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>" + dr["Type"].ToString() + " Total</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GKGen == "" ? "0" : GKGen)) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GKSC == "" ? "0" : GKSC)) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GKST == "" ? "0" : GKST)) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GKTot == "" ? "0" : GKTot)) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GRGen == "" ? "0" : GRGen)) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GRSC == "" ? "0" : GRSC)) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GRST == "" ? "0" : GRST)) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(GRTot == "" ? "0" : GRTot)) + "</td>");

            str.Append("</tr>");
            str.Append("</table>");

        }

        string AGKGen = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K' AND CATEGOGY='General'").ToString();

        string AGKSC = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K'  AND CATEGOGY='SC'").ToString();

        string AGKST = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K' AND CATEGOGY='ST'").ToString();

        string AGKTot = dtData.Compute("Sum(PAIDAMNT)", "SEASON='K'").ToString();

        string AGRGen = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R' AND CATEGOGY='General'").ToString();

        string AGRSC = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R' AND CATEGOGY='SC'").ToString();

        string AGRST = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R' AND CATEGOGY='ST'").ToString();

        string AGRTot = dtData.Compute("Sum(PAIDAMNT)", "SEASON='R'").ToString();

        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; margin-top:20px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        str.Append("<tr style='background-color:#CCCCCC; color:red; margin-top:20px; font-weight:bold; '>");
        str.Append("<td>Grand Total</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGKGen == "" ? "0" : AGKGen)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGKSC == "" ? "0" : AGKSC)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGKST == "" ? "0" : AGKST)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGKTot == "" ? "0" : AGKTot)) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGRGen == "" ? "0" : AGRGen)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGRSC == "" ? "0" : AGRSC)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGRST == "" ? "0" : AGRST)) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(AGRTot == "" ? "0" : AGRTot)) + "</td>");

        str.Append("</tr>");

        str.Append("</table>");
        litReport.Text = str.ToString();
    }

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=Bankwisepayment.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            litReport.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex)
        { }
    }

}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_rpt_MonthwiseStock : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
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
            if (!(this.Session["UserType"].ToString() == "OAIC") && !(this.Session["UserType"].ToString() == "OSSC") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                fillMonth();
                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                return;
            }
        }
    }

    private void fillMonth()
    {
        string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        int monthCode = 0;
        this.drpMonth.Items.Insert(monthCode, "--- SELECT ---");
        foreach (string m in monthNames) // writing out
        {
            monthCode++;
            if (monthCode < 13)
            {
                this.drpMonth.Items.Add(new ListItem(m, monthCode.ToString()));
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "STOCK_RPT_MonthwiseTransaction";
        string[] param = { "@Dist", "@FIN_YR", "@MonthCode", "@User_Type", "@Season" };
        object[] value = { Session["DistCode"], drpSession.SelectedValue.Trim(), drpMonth.SelectedValue.Trim(), Session["UserType"].ToString(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull
 : drpSeassion.SelectedValue.Trim() };
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
        DataTable dtData = new DataTable();
        dtData = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        int days = DateTime.DaysInMonth(int.Parse(drpSession.SelectedValue.Trim()), int.Parse(drpMonth.SelectedValue.Trim()));
        double rcv = 0;
        double Sale = 0;
        for (int i = 1; i <= days; i++)
        {
            string date = string.Format("{0:00}", i) + "." + string.Format("{0:00}", int.Parse(drpMonth.SelectedValue.Trim())) + "." + drpSession.SelectedValue.Trim();

            string DtFilter = drpSession.SelectedValue.Trim() + "-" + string.Format("{0:00}", int.Parse(drpMonth.SelectedValue.Trim())) + "-" + string.Format("{0:00}", i);

            DataRow[] result = dtData.Select("Recv_Date= '" + DtFilter + "' OR SALE_DATE = '" + DtFilter + "'");

            if (result.Length > 0)
            {
                str.Append("<fieldset>");
                str.Append("<legend style='font-size: 14px;'>" + date + "   ( " + DateTime.Parse(date).DayOfWeek + " ) ");
                str.Append("</legend>");
                str.Append("<table style='width:100%; border-collapse:collapse; font-size: 12px; ' border='0' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
                str.Append("<td>Crop Name </td>");
                str.Append("<td>Variety Name</td>");
                str.Append("<td style='text-align: Right;  width: 100px;'>Received</td>");
                str.Append("<td style='text-align: Right;  width: 100px;'>Sale</td>");
                str.Append("</tr>");
                foreach (DataRow row in result)
                {
                    str.Append("<tr>");
                    str.Append("<td>" + row["Crop_Name"].ToString() + "</td>");
                    str.Append("<td>" + row["Variety_Name"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + row["Recv_Quantity"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(row["SALE_Qty"].ToString())) + "</td>");
                    str.Append("</tr>");
                    rcv = rcv + double.Parse(row["Recv_Quantity"].ToString());
                    Sale = Sale + double.Parse(row["SALE_Qty"].ToString());
                }
                str.Append("<tr>");
                str.Append("<td colspan='2' style='text-align: right;   font-size: 14px; color:#8C2108; font-weight: bold;'>Running Total : </td>");
                str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>" + string.Format("{0:f2}", rcv) + "</td>");
                str.Append("<td style='text-align: Right; font-size: 14px; color:#8C2108; font-weight: bold;'>" + string.Format("{0:f2}", Sale) + "</td>");
                str.Append("</tr>");
                str.Append("</table>");
                str.Append("</fieldset>");

            }
            else
            {

            }

        }
        litReport.Text = str.ToString();
    }
}
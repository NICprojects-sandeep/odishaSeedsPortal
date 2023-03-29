using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_rptGodownWiseStock : System.Web.UI.Page
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
        string sql = "STOCK_RPT_GodownwiseStock";
        string[] param = { "@FIN_YR", "@User_Type", "@CropCatg_ID", "@Crop_ID", "@Dist_Code", "@season" };
        object[] value = { Request.QueryString["FIN_YR"].Trim(), Request.QueryString["User_Type"].Trim(), Request.QueryString["CropCatg_ID"].Trim(), Request.QueryString["Crop_ID"].Trim(), Request.QueryString["Dist_Code"].Trim(), Request.QueryString["Season"].Trim() };
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

        DataTable dtData = ds.Tables[0];

        DataView viewDist = new DataView(dtData);
        DataTable distinctGodown = viewDist.ToTable(true, "Godown_ID", "Godown_Name");

        //Distinct Variety Code and Name
        DataView view = new DataView(dtData);
        DataTable distinctValues = view.ToTable(true, "Crop_Verid", "Variety_Name");



        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>GoDown Name</td>");
        str.Append("<td colspan='" + distinctValues.Rows.Count + "' style='text-align: center;'>Variety</td>");
        str.Append("<td rowspan='2'  style='text-align: Right;'>Total</td>");
        str.Append("</tr>");


        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        foreach (DataRow dr in distinctValues.Rows)
        {
            str.Append(" <td  style='width:150px;'>" + dr["Variety_Name"].ToString() + "</td>");
        }
        str.Append("</tr>");

        double TotDistwise = 0;
        foreach (DataRow dr in distinctGodown.Rows)
        {
            str.Append(" <tr>");
            str.Append(" <td>" + dr["Godown_Name"].ToString() + "</td>");

            foreach (DataRow drv in distinctValues.Rows)
            {
                DataRow[] result = dtData.Select("Godown_ID= " + dr["Godown_ID"].ToString() + " AND Crop_Verid = '" + drv["Crop_Verid"].ToString() + "'");
                if (result.Length > 0)
                {
                    foreach (DataRow row in result)
                    {

                        str.Append(" <td style='text-align: Right;'>" + row[6].ToString() + "</td>");
                        TotDistwise = TotDistwise + double.Parse(row[6].ToString());
                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right;'>--</td>");
                }

            }
            string value = TotDistwise > 0 ? TotDistwise.ToString() : "--";
            str.Append("<td style='text-align: Right;'>" + value + "</td>");
            TotDistwise = 0;



            str.Append("</tr>");
        }
        str.Append(" <tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append(" <td>Varietywise Total</td>");
        foreach (DataRow drv in distinctValues.Rows)
        {
            string TotVar = dtData.Compute("Sum(STOCK)", "Crop_Verid = '" + drv["Crop_Verid"].ToString() + "'").ToString();
            str.Append("<td style='text-align: Right;'>" + TotVar + "</td>");
        }
        str.Append("<td style='text-align: Right;'>" + dtData.Compute("Sum(STOCK)", "").ToString() + "</td>");

        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
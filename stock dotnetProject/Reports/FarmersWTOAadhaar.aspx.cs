using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FarmersWTOAadhaar : System.Web.UI.Page
{
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.Session["DistCode"]
            FillFarmers(Session["LGDistrict"].ToString());
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=FarmersWithOutAadhaar.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            litReport.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex) { }
    }
    private void FillFarmers(string DistCode)
    {
        ds = new DataSet();
        DAL = new DLL_DropDown();
        BAL = new BLL_DropDown();
        BAL.DistCode = DistCode;
        ds = DAL.FillDistWiseFarmerWTOAadhaar(BAL);
        DataTable dtFarmer = ds.Tables[0];
        int i = 0;        
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 12px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Left;'>Block </td>");
        str.Append("<td style='text-align: Left;'>GP</td>");
        str.Append("<td style='text-align: Left;'>Village</td>");
        str.Append("<td style='text-align: Left;'>Farmer ID </td>");
        str.Append("<td style='text-align: Left;'>Farmer Name</td>");
        str.Append("<td style='text-align: Left;'>Father Name</td>");
        str.Append("<td style='text-align: Left;'>Age </td>");
        str.Append("<td style='text-align: Left;'>Gender</td>");
        str.Append("<td style='text-align: Left;'>Mobile Number</td>");
        str.Append("</tr>");
        foreach (DataRow dr in dtFarmer.Rows)
        {
            i++;
            str.Append("<tr style='background-color:#CCCCCC;'>");
            str.Append("<td style='text-align: Left;'>" + dr["vch_blockname"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["vch_gpname"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["vch_villagename"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["nicfarmerid"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["vchfarmername"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["vchfathername"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["INTAGE"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["Gender_Value"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;'>" + dr["VCHMOBILENO"].ToString() + "</td>");
            str.Append("</tr>");
        }
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
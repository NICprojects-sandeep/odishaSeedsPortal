using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Rep_FarmerWTOAadhaar : System.Web.UI.Page
{
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlContents.Visible = false;
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
    private void GenerateReport(DataSet ds)
    {
        DataTable dtDist = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        str.Append("<strong>District Wise Block Wise Report for Farmers without Aadhaar As On : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</strong>");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 12px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        int i = 0;
        foreach (DataRow dr in dtDist.Rows)
        {
            i++;
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td style='text-align: Left;width: 30%;'>District Name : " + dr["vch_districtname"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;width: 35%;'>Total Farmer : " + dr["TotFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;width: 35%;'>Farmers without Aadhaar : " + dr["NoAadhaar"].ToString() + "</td>");
            str.Append("</tr>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td colspan='3' style='text-align: Left;'>");
            str.Append("<table style='width:100%; border-collapse:collapse; font-size: 12px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td style='text-align: Left;width: 30%;'>Block Name</td>");
            str.Append("<td style='text-align: Left;width: 35%;'>Total Farmer</td>");
            str.Append("<td style='text-align: Left;width: 35%;'>Farmers without Aadhaar</td>");
            str.Append("</tr>");
            ds = new DataSet();
            DAL = new DLL_DropDown();
            BAL = new BLL_DropDown();
            BAL.DistCode = dr["int_DistrictID"].ToString();
            ds = DAL.FillBlockAadhaar(BAL);
            DataTable dtBlock = ds.Tables[0];
            int j = 0;
            foreach (DataRow dr1 in dtBlock.Rows)
            {
                j++;
                str.Append("<tr style='background-color:#CCCCCC;'>");
                str.Append("<td style='text-align: Left;width: 30%;'>" + dr1["vch_blockname"].ToString() + "</td>");
                str.Append("<td style='text-align: Left;width: 35%;'>" + dr1["TotFarmer"].ToString() + "</td>");
                str.Append("<td style='text-align: Left;width: 35%;'>" + dr1["NoAadhaar"].ToString() + "</td>");
                str.Append("</tr>");
            }
            str.Append("</table>");
            str.Append("</td>");
            str.Append("</tr>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td style='text-align: Left;width: 30%;'>Grand Total </td>");
            str.Append("<td style='text-align: Left;width: 35%;'>" + dtDist.Compute("Sum(TotFarmer)", "").ToString() + "</td>");
            str.Append("<td style='text-align: Left;width: 35%;'>" + dtDist.Compute("Sum(NoAadhaar)", "").ToString() + "</td>");
            str.Append("</tr>");

        }
        str.Append("</table>");

        litReport.Text = str.ToString();
    }
    protected void btnDist_Click(object sender, EventArgs e)
    {        
        ds = new DataSet();
        DAL = new DLL_DropDown();
        ds = DAL.FillDistrictAadhaar();
        GenerateDistReport(ds);
        pnlContents.Visible = true;
    }
    private void GenerateDistReport(DataSet ds)
    {
        DataTable dtDist = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        str.Append("<strong>District Wise Report for Farmers without Aadhaar As On : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</strong>");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 12px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Left;width: 30%;'>District Name</td>");
        str.Append("<td style='text-align: Left;width: 35%;'>Total Farmer </td>");
        str.Append("<td style='text-align: Left;width: 35%;'>Farmers without Aadhaar</td>");
        str.Append("</tr>");
        int i = 0;
        foreach (DataRow dr in dtDist.Rows)
        {
            i++;
            str.Append("<tr style='background-color:#CCCCCC;'>");
            str.Append("<td style='text-align: Left;width: 30%;'>" + dr["vch_districtname"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;width: 35%;'>" + dr["TotFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Left;width: 35%;'>" + dr["NoAadhaar"].ToString() + "</td>");
            str.Append("</tr>");            
            

        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Left;width: 30%;'>Grand Total </td>");
        str.Append("<td style='text-align: Left;width: 35%;'>" + dtDist.Compute("Sum(TotFarmer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Left;width: 35%;'>" + dtDist.Compute("Sum(NoAadhaar)", "").ToString() + "</td>");
        str.Append("</tr>");
        str.Append("</table>");

        litReport.Text = str.ToString();   
    }
    protected void btnBlock_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        DAL = new DLL_DropDown();
        ds = DAL.FillDistrictAadhaar();
        GenerateReport(ds);
        pnlContents.Visible = true;
    }
}
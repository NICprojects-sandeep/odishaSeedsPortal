using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RptCropDistSale : System.Web.UI.Page
{
    DataSet ds;
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            btnExport.Enabled = false;            
        }
    }
    void Page_PreInit(Object sender, EventArgs e)
    {
        if (this.Session["UserID"].ToString() == "fmn_director")
        {
            this.MasterPageFile = "~/MasterPages/Director.master";
        }
        else
        {
            this.MasterPageFile = "~/MasterPages/Admin.master";
        }
    }
    private void GenerateReport(DataSet ds)
    {
        string totPACS_RCV = "";
        string totDEALER_RCV = "";
        string totTOT_RCV = "";
        string totPACS_SALE = "";
        string totDEALER_SALE = "";
        string totTOT_SALE = "";
        string totFARMERCNT = "";
        DataTable dt = ds.Tables[0];
        DataTable dt1 = ds.Tables[1];
        DataTable dt2 = ds.Tables[2];
        btnExport.Enabled = true;
        StringBuilder str = new StringBuilder("");
        str.Append("DPR for "+ddlFinYear.SelectedItem.Text+" ("+ddlSeason.SelectedItem.Text+") As On "+DateTime.Now.ToString("dd/MM/yyyy")+" "+ DateTime.Now.ToShortTimeString());
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>District Name</td>");

        foreach (DataRow dr1 in dt1.Rows)
        {
            str.Append("<td colspan='7' style='text-align:center;' > " + dr1["CROP_NAME"].ToString() + "</td>");
        }
        str.Append("</tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        foreach (DataRow dr1 in dt1.Rows)
        {            
            str.Append("<td>DEALER RCV</td>");
            str.Append("<td>PACS RCV</td>");            
            str.Append("<td>TOT RCV</td>");
            str.Append("<td>DEALER SALE</td>");
            str.Append("<td>PACS SALE</td>");            
            str.Append("<td>TOT SALE</td>");
            str.Append("<td>NO OF BENEFICIARIES</td>");
        }

        str.Append("</tr>");
        foreach (DataRow dr in dt.Rows)
        {
            str.Append("<tr style='font-weight:bold;'>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");
            foreach (DataRow dr1 in dt1.Rows)
            {
                DataRow[] Datad = ds.Tables[2].Select("dist_code= " + dr["Dist_Code"].ToString() + " and CROP_ID='" + dr1["Crop_Code"].ToString() + "'");
                if (Datad.Length > 0)
                {
                    foreach (DataRow value in Datad)
                    {
                        str.Append("<td>" + value["DEALER_RCV"] + " </td>");
                        str.Append("<td>" + value["PACS_RCV"] + "</td>");                        
                        str.Append("<td>" + value["TOT_RCV"] + " </td>");
                        str.Append("<td>" + value["DEALER_SALE"] + " </td>");
                        str.Append("<td>" + value["PACS_SALE"] + " </td>");                        
                        str.Append("<td>" + value["TOT_SALE"] + " </td>");
                        str.Append("<td>" + value["FARMER_CNT"] + " </td>");
                    }
                }
                else
                {
                    str.Append("<td>0.00</td>");
                    str.Append("<td>0.00</td>");
                    str.Append("<td>0.00</td>");
                    str.Append("<td>0.00</td>");
                    str.Append("<td>0.00</td>");
                    str.Append("<td>0.00</td>");
                    str.Append("<td>0.00</td>");
                }
            }
            str.Append("</tr>");
        }
        str.Append("<tr style='font-weight:bold;'>");
        str.Append("<td> TOTAL </td>");
        foreach (DataRow dr1 in dt1.Rows)
        {
            totPACS_RCV =ds.Tables[2].Compute("Sum(PACS_RCV)", "CROP_ID='" + dr1["Crop_Code"].ToString() + "'").ToString();
            totDEALER_RCV = ds.Tables[2].Compute("Sum(DEALER_RCV)", "CROP_ID='" + dr1["Crop_Code"].ToString() + "'").ToString();
            totTOT_RCV = ds.Tables[2].Compute("Sum(TOT_RCV)", "CROP_ID='" + dr1["Crop_Code"].ToString() + "'").ToString();
            totPACS_SALE = ds.Tables[2].Compute("Sum(PACS_SALE)", "CROP_ID='" + dr1["Crop_Code"].ToString() + "'").ToString();
            totDEALER_SALE = ds.Tables[2].Compute("Sum(DEALER_SALE)", "CROP_ID='" + dr1["Crop_Code"].ToString() + "'").ToString();
            totTOT_SALE = ds.Tables[2].Compute("Sum(TOT_SALE)", "CROP_ID='" + dr1["Crop_Code"].ToString() + "'").ToString();
            totFARMERCNT = ds.Tables[2].Compute("Sum(FARMER_CNT)", "CROP_ID='" + dr1["Crop_Code"].ToString() + "'").ToString();
            if (string.IsNullOrEmpty(totPACS_RCV))
            {
                totPACS_RCV = "0.00";
            }
            if (string.IsNullOrEmpty(totDEALER_RCV))
            {
                totDEALER_RCV = "0.00";
            }
            if (string.IsNullOrEmpty(totTOT_RCV))
            {
                totTOT_RCV = "0.00";
            }
            if (string.IsNullOrEmpty(totPACS_SALE))
            {
                totPACS_SALE = "0.00";
            }
            if (string.IsNullOrEmpty(totDEALER_SALE))
            {
                totDEALER_SALE = "0.00";
            }
            if (string.IsNullOrEmpty(totTOT_SALE))
            {
                totTOT_SALE = "0.00";
            }
            if (string.IsNullOrEmpty(totFARMERCNT))
            {
                totFARMERCNT = "0.00";
            }

            str.Append("<td>" + totDEALER_RCV + "</td>");
            str.Append("<td>" + totPACS_RCV + "</td>");            
            str.Append("<td>" + totTOT_RCV + "</td>");
            str.Append("<td>" + totDEALER_SALE + "</td>");
            str.Append("<td>" + totPACS_SALE + "</td>");            
            str.Append("<td>" + totTOT_SALE + "</td>");
            str.Append("<td>" + totFARMERCNT + "</td>");
        }
        btnExport.Enabled = true;
        str.Append("</tr>");        
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DPR.xls";
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        btnExport.Enabled = false;
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FIN_YR = ddlFinYear.SelectedValue;
        objUserBELDIST.SEASON = ddlSeason.SelectedValue;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.RptCropDistSale1(objUserBELDIST);
        GenerateReport(ds);
    }
}

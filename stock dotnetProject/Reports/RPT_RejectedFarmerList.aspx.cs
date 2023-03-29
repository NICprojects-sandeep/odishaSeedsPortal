using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_RejectedFarmerList : System.Web.UI.Page
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
        if (this.Session["UserType"].ToString() == "RCSA")
        {
            this.Page.MasterPageFile = "../MasterPages/RCSA.master";
            return;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillDist();
            btnExport.Enabled = false;
            btnPrint.Enabled = false;
        }
       
        
    }

    private void fillDist()
    {
        DataSet ds = new DataSet();
        string sql = "select Dist_Name, NDist_Code from dbo.Tbl_Dist order by Dist_Name";
        string[] param = { };
        object[] value = { };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            drpDist.Items.Clear();
            drpDist.DataSource = ds.Tables[0];
            drpDist.DataTextField = "Dist_Name";
            drpDist.DataValueField = "NDist_Code";
            drpDist.DataBind();
            this.drpDist.Items.Insert(0, "--- SELECT ---");
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            this.drpDist.Items.Insert(0, "--- Data Not Found ---");
            this.drpBlock.Items.Insert(0, "--- Data Not Found ---");
            this.drpGP.Items.Insert(0, "--- Data Not Found ---");
            this.drpVillage.Items.Insert(0, "--- Data Not Found ---");
        }
    }
    protected void drpDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDist.SelectedIndex > 0)
        {
            drpBlock.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select  Nblock_code,PB.vch_BlockName  from block B INNER JOIN [stock].[dbo].[PDS_BLOCKMASTER] PB ON PB.int_BlockID collate SQL_Latin1_General_CP1_CI_AS =B.Nblock_code where left(Nblock_code,2)=@PFMS_Dist_Code order by block_name";
            string[] param = { "@PFMS_Dist_Code" };
            object[] value = { string.Format("{0:00}", int.Parse(drpDist.SelectedValue.Trim())) };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpBlock.DataSource = ds.Tables[0];
            drpBlock.DataTextField = "vch_BlockName";
            drpBlock.DataValueField = "Nblock_code";
            drpBlock.DataBind();
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpBlock.Items.Clear();
            drpGP.Items.Clear();
            drpVillage.Items.Clear();
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
    protected void drpBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBlock.SelectedIndex > 0)
        {
            drpGP.Items.Clear();
            drpVillage.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select int_GPID ,vch_GPName FROM [stock].[dbo].[PDS_GPMASTER]  where int_BlockID=@Block_Code order by vch_GPName";
            string[] param = { "@Block_Code" };
            object[] value = { drpBlock.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpGP.DataSource = ds.Tables[0];
            drpGP.DataTextField = "vch_GPName";
            drpGP.DataValueField = "int_GPID";
            drpGP.DataBind();
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpGP.Items.Clear();
            drpVillage.Items.Clear();
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
    protected void drpGP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpGP.SelectedIndex > 0)
        {
            drpVillage.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select int_VillageID,vch_VillageName from  dbo.PDS_VILLAGEMASTER where int_GPID=@gp_code order by vch_VillageName";
            string[] param = { "@gp_code" };
            object[] value = { drpGP.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpVillage.DataSource = ds.Tables[0];
            drpVillage.DataTextField = "vch_VillageName";
            drpVillage.DataValueField = "int_VillageID";
            drpVillage.DataBind();
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpVillage.Items.Clear();
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
            string sql = "GET_FarmerListRjct";
        string[] param = { "@VCHDISTID", "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
        object[] value = { drpDist.SelectedValue.Trim() == "---All---" ? "0" : string.Format("{0:00}", int.Parse(drpDist.SelectedValue.Trim())), drpBlock.SelectedValue.Trim() == "---All---" ? "0" : drpBlock.SelectedValue.Trim(), drpGP.SelectedValue.Trim() == "---All---" ? "0" : drpGP.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnExport.Enabled = true;
                GenerateReport(ds);
                btnPrint.Enabled = true;
                btnExport.Enabled = true;
            }
        }
        else
        {
            btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }

    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No.</td>");
        str.Append("<td>Name  </td>");
        str.Append("<td style=' padding-right:5px;'>Father's/Husband's Name </td>");
        str.Append("<td style=' padding-right:5px;'>Bank</td>");
        str.Append("<td style=' padding-right:5px; width:50px;'>Account No </td>");
        str.Append("<td style=' padding-right:5px;'>Accountholdername </td>");
        str.Append("<td style=' padding-right:5px;'>Reasons of Rejection</td>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["VCHFARMERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHFATHERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["Bank_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHACCOUNTNO"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHACCHOLDERNM"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;'>");

            string tmpstr111 = "<a href='javascript:popUp";
            string tempStr11 = "('RejectionofRejectecFRMR.aspx?&VCHFARMERCODE="+dr["VCHFARMERCODE"].ToString()+"')";
            string tmpstr211 = "'>";
            str.Append(tmpstr111.Replace('\'', '\"') + tempStr11 + tmpstr211.Replace('\'', '\"'));
            str.Append("<img src='../images/binoculars.png'  title='View Details' /></a>");
            str.Append("</td>");

            str.Append("</tr>");
            i++;
        }

        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=RejectedFarmersDetail.xls";
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
}
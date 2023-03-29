﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Farmer_FarmerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillDist();
            ViewState["dt"] = string.Empty;
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
            this.drpGp.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            this.drpDist.Items.Insert(0, "--- Data Not Found ---");
            this.drpBlock.Items.Insert(0, "--- Data Not Found ---");
            this.drpGp.Items.Insert(0, "--- Data Not Found ---");
            this.drpVillage.Items.Insert(0, "--- Data Not Found ---");
        }
    }
    protected void drpDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDist.SelectedIndex > 0)
        {
            drpBlock.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select  Nblock_code,PB.vch_BlockName  from block B INNER JOIN [dbo].[PDS_BLOCKMASTER] PB ON PB.int_BlockID collate SQL_Latin1_General_CP1_CI_AS =B.Nblock_code where left(Nblock_code,2)=@PFMS_Dist_Code order by block_name";
            string[] param = { "@PFMS_Dist_Code" };
            object[] value = { string.Format("{0:00}", int.Parse(drpDist.SelectedValue.Trim())) };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpBlock.DataSource = ds.Tables[0];
            drpBlock.DataTextField = "vch_BlockName";
            drpBlock.DataValueField = "Nblock_code";
            drpBlock.DataBind();
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGp.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpBlock.Items.Clear();
            drpGp.Items.Clear();
            drpVillage.Items.Clear();
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGp.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
    protected void drpBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBlock.SelectedIndex > 0)
        {
            drpGp.Items.Clear();
            drpVillage.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select int_GPID ,vch_GPName FROM [dbo].[PDS_GPMASTER]  where int_BlockID=@Block_Code order by vch_GPName";
            string[] param = { "@Block_Code" };
            object[] value = { drpBlock.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpGp.DataSource = ds.Tables[0];
            drpGp.DataTextField = "vch_GPName";
            drpGp.DataValueField = "int_GPID";
            drpGp.DataBind();
            this.drpGp.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpGp.Items.Clear();
            drpVillage.Items.Clear();
            this.drpGp.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
    protected void drpGp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpGp.SelectedIndex > 0)
        {
            drpVillage.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select int_VillageID,vch_VillageName from  dbo.PDS_VILLAGEMASTER where int_GPID=@gp_code order by vch_VillageName";
            string[] param = { "@gp_code" };
            object[] value = { drpGp.SelectedValue.Trim() };
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
        string sql = string.Empty;
        if (drpStatus.SelectedValue == "0")
        {
            sql = "GET_FarmerListNew";
        }
        else if (drpStatus.SelectedValue == "1")
        {
            sql = "GET_RjctFarmerListNew";
        }
        else
        {
            sql = "GET_AllFarmerListNew";
        }

        string[] param = { "@VCHDISTID", "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
        object[] value = { drpDist.SelectedValue.Trim() == "---All---" ? "0" : string.Format("{0:00}", int.Parse(drpDist.SelectedValue.Trim())), drpBlock.SelectedValue.Trim() == "---All---" ? "0" : drpBlock.SelectedValue.Trim(), drpGp.SelectedValue.Trim() == "---All---" ? "0" : drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (drpStatus.SelectedValue == "0")
            {
                grd_Frm.Attributes.Add("style", "display:;");
                grdAll.Attributes.Add("style", "display:none;");
                GrdRjct.Attributes.Add("style", "display:none;");
                litReport.Text = string.Empty;
                grd_Frm.DataSource = ds.Tables[0];
                grd_Frm.DataBind();
            }
            else if (drpStatus.SelectedValue == "1")
            {
                grd_Frm.Attributes.Add("style", "display:none;");
                grdAll.Attributes.Add("style", "display:none;");
                ViewState["dt"] = ds.Tables[1];
                GrdRjct.Attributes.Add("style", "display:;");
                litReport.Text = string.Empty;
                GrdRjct.DataSource = ds.Tables[0];
                GrdRjct.DataBind();


            }
            else {
                grd_Frm.Attributes.Add("style", "display:none;");
                grdAll.Attributes.Add("style", "display:;");
                GrdRjct.Attributes.Add("style", "display:none;");
                litReport.Text = string.Empty;
                grdAll.DataSource = ds.Tables[0];
                grdAll.DataBind();
            
            }
        }
        else
        {
            grd_Frm.Attributes.Add("style", "display:none;");
            GrdRjct.Attributes.Add("style", "display:none;");
            grdAll.Attributes.Add("style", "display:none;");
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }

    protected void lnkVCard_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        HiddenField lnlbtn = (HiddenField)row.FindControl("hfVCard");

        Session["VCHVOTERIDCARDNO"] = lnlbtn.Value.Trim();
        Response.Redirect("FarmerIdentification.aspx");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=FarmerList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            grd_Frm.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex) { }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var lblReason = (Label)e.Row.FindControl("lblReason");
            var hfCode = (HiddenField)e.Row.FindControl("hfVCHFARMERCODE");
            DataTable dt = (DataTable)ViewState["dt"];
            DataRow[] reason = dt.Select("Scheme_Specific_ID= '" + hfCode.Value.Trim() + "'", "Date desc");
            if (reason.Length > 0)
            {
                if (reason[0][4].ToString() == "RJCT")
                {
                    lblReason.Text = reason[0][2].ToString();
                }
                if (reason[0][4].ToString() == "ACCP")
                {
                    lblReason.Text = "A/C holder Name Missmatched";
                }
            }
            else {
                lblReason.Text = "Sent to PFMS";
            }
         
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FarmerListUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillDist();
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
            string sql = "select  Nblock_code,PB.vch_BlockName  from block B INNER JOIN [stock].[dbo].[PDS_BLOCKMASTER] PB ON PB.int_BlockID collate SQL_Latin1_General_CP1_CI_AS =B.Nblock_code where left(Nblock_code,2)=@PFMS_Dist_Code order by block_name";
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
            string sql = "select int_GPID ,vch_GPName FROM [stock].[dbo].[PDS_GPMASTER]  where int_BlockID=@Block_Code order by vch_GPName";
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
        string sql = "GET_FarmerList";
        string[] param = { "@VCHDISTID", "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
        object[] value = { drpDist.SelectedValue.Trim() == "---All---" ? "0" : string.Format("{0:00}", int.Parse(drpDist.SelectedValue.Trim())), drpBlock.SelectedValue.Trim() == "---All---" ? "0" : drpBlock.SelectedValue.Trim(), drpGp.SelectedValue.Trim() == "---All---" ? "0" : drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grd_FarmerList.Attributes.Add("style", "display:;");
            litReport.Text = string.Empty;
            //GenerateFarmerList(ds.Tables[0]);
            grd_FarmerList.DataSource = ds.Tables[0];
            grd_FarmerList.DataBind();
        }
        else
        {
            grd_FarmerList.Attributes.Add("style", "display:none;");
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }

    protected void lnkVCard_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        HiddenField lnlbtn = (HiddenField)row.FindControl("hfVCard");

        Session["VCHVOTERIDCARDNO"] = lnlbtn.Value.Trim();
        Response.Redirect("../Farmer/FarmerIdentification.aspx");
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../Farmer/FarmerIdentification.aspx','_newtab');", true);
    }
}
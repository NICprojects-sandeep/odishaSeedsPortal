using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AAO_Farmer_FarmerList : System.Web.UI.Page
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
            if (!(this.Session["UserType"].ToString() == "AAOO") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                fillDist();               
                string BlockCode1 = "select BLK_CODE from [Stock].dbo.JALANIDHI_DAO_AAO where AAO_CODE=@AAOCode AND PP='1'";
                string[] paramBlk1 = { "@AAOCode" };
                object[] valueBlk1 = { Session["UserID"].ToString().Substring(4, 6) };
                DataSet dsBlk1 = dbsAppStock.param_passing_fetch(BlockCode1, paramBlk1, valueBlk1);
                if (dsBlk1.Tables[0].Rows.Count > 0)
                {
                    Session["BlockCode"] = dsBlk1.Tables[0].Rows[0]["BLK_CODE"].ToString();
                }                
                fillBlock();
                ViewState["dt"] = string.Empty;
                this.hfSession.Value = HttpUtility.HtmlEncode(this.Session["AuthTokenPage"].ToString());
                return;
            }
        }

    }

    private void fillDist()
    {
        DataSet ds = new DataSet();
        string sql = "select Dist_Name,NDist_Code from [farmerdb].[dbo].Tbl_Dist where NICDIST_CODE=left(@AAOCode,2)";
        string[] paramBlk1 = { "@AAOCode" };
        object[] valueBlk1 = { Session["UserID"].ToString().Substring(4, 6) };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, paramBlk1, valueBlk1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblDist.Text = ds.Tables[0].Rows[0]["Dist_Name"].ToString();
            hfDistCode.Value = ds.Tables[0].Rows[0]["NDist_Code"].ToString();
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGp.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            this.drpBlock.Items.Insert(0, "--- Data Not Found ---");
            this.drpGp.Items.Insert(0, "--- Data Not Found ---");
            this.drpVillage.Items.Insert(0, "--- Data Not Found ---");
        }
    }
    private void fillBlock()
    {
        DataSet ds = new DataSet();
        string sql = " select int_BlockID,vch_BlockName from PDS_BLOCKMASTER where NIC_BlockCode=@BlockCode";
        string[] paramBlk1 = { "@BlockCode" };
        object[] valueBlk1 = { Session["BlockCode"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, paramBlk1, valueBlk1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            drpBlock.DataSource = ds.Tables[0];
            drpBlock.DataTextField = "vch_BlockName";
            drpBlock.DataValueField = "int_BlockID";
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
    //protected void drpDist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (drpDist.SelectedIndex > 0)
    //    {
    //        drpBlock.Items.Clear();
    //        DataSet ds = new DataSet();
    //        string sql = "select  Nblock_code,PB.vch_BlockName  from block B INNER JOIN [stock].[dbo].[PDS_BLOCKMASTER] PB ON PB.int_BlockID collate SQL_Latin1_General_CP1_CI_AS =B.Nblock_code where left(Nblock_code,2)=@PFMS_Dist_Code order by block_name";
    //        string[] param = { "@PFMS_Dist_Code" };
    //        object[] value = { string.Format("{0:00}", int.Parse(drpDist.SelectedValue.Trim())) };
    //        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
    //        drpBlock.DataSource = ds.Tables[0];
    //        drpBlock.DataTextField = "vch_BlockName";
    //        drpBlock.DataValueField = "Nblock_code";
    //        drpBlock.DataBind();
    //        this.drpBlock.Items.Insert(0, "---All---");
    //        this.drpGp.Items.Insert(0, "---All---");
    //        this.drpVillage.Items.Insert(0, "---All---");
    //    }
    //    else
    //    {
    //        drpBlock.Items.Clear();
    //        drpGp.Items.Clear();
    //        drpVillage.Items.Clear();
    //        this.drpBlock.Items.Insert(0, "---All---");
    //        this.drpGp.Items.Insert(0, "---All---");
    //        this.drpVillage.Items.Insert(0, "---All---");
    //    }
    //}
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
        string sql = string.Empty;
        if (drpStatus.SelectedValue == "0")
        {
            sql = "GET_FarmerListNew";
        }
        else
        {
            sql = "GET_RjctFarmerListNew";
        }

        string[] param = { "@VCHDISTID", "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
        object[] value = { hfDistCode.Value, drpBlock.SelectedValue.Trim() == "---All---" ? "0" : drpBlock.SelectedValue.Trim(), drpGp.SelectedValue.Trim() == "---All---" ? "0" : drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (drpStatus.SelectedValue == "0")
            {
                grd_Frm.Attributes.Add("style", "display:;");
                GrdRjct.Attributes.Add("style", "display:none;");
                litReport.Text = string.Empty;
                grd_Frm.DataSource = ds.Tables[0];
                grd_Frm.DataBind();
            }
            else
            {
                grd_Frm.Attributes.Add("style", "display:none;");
                ViewState["dt"] = ds.Tables[1];
                GrdRjct.Attributes.Add("style", "display:;");
                litReport.Text = string.Empty;
                GrdRjct.DataSource = ds.Tables[0];
                GrdRjct.DataBind();


            }
        }
        else
        {
            grd_Frm.Attributes.Add("style", "display:none;");
            GrdRjct.Attributes.Add("style", "display:none;");
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }

    protected void lnkVCard_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        HiddenField lnlbtn = (HiddenField)row.FindControl("hfVCard");

        Session["VCHVOTERIDCARDNO"] = lnlbtn.Value.Trim();
        Response.Redirect("AAO_FarmerIdentification.aspx");
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
            else
            {
                lblReason.Text = string.Empty;
            }

        }
    }
}
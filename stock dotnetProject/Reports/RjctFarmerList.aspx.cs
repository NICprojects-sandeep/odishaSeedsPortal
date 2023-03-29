using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RjctFarmerList : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
                if (!(this.Session["UserType"].ToString() == "DRCS") && !(this.Session["UserType"].ToString() == "DIST") || !(this.Session["FirstLogin"].ToString() == "N"))
                {
                    base.Response.Redirect("../Unauthorised.aspx", true);
                    return;
                }
                if (!this.Page.IsPostBack)
                {
                    litDist.Text = Session["DistName"].ToString();
                    fillBlock();
                }
            }

        }

    }

    private void fillBlock()
    {
        drpBlock.Items.Clear();
        DataSet ds = new DataSet();
        //string sql = "select  block_name,Nblock_code  from block where left(Nblock_code,2)=@PFMS_Dist_Code order by block_name";

        string sql = "  select  block_name,Nblock_code  from [FARMERDB].[dbo].[block]  where left(Nblock_code,2)=(SELECT NDist_Code FROM [FARMERDB].[dbo].[Tbl_Dist] WHERE NICDIST_CODE=@PFMS_Dist_Code) order by block_name";
        string[] param = { "@PFMS_Dist_Code" };
        object[] value = { string.Format("{0:00}", int.Parse(Session["DistCode"].ToString())) };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        drpBlock.DataSource = ds.Tables[0];
        drpBlock.DataTextField = "block_name";
        drpBlock.DataValueField = "Nblock_code";
        drpBlock.DataBind();
        this.drpBlock.Items.Insert(0, "---All---");
        this.drpGp.Items.Insert(0, "---All---");
        this.drpVillage.Items.Insert(0, "---All---");
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (this.Session["UserType"].ToString() == "DIST")
        {
            this.Page.MasterPageFile = "../MasterPages/Dist.master";
            return;
        }

    }

    protected void drpBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBlock.SelectedIndex > 0)
        {
            drpGp.Items.Clear();
            drpVillage.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select GP_Code,  GP_Name from  dbo.Tbl_GP where Block_Code=@Block_Code order by GP_Name";
            string[] param = { "@Block_Code" };
            object[] value = { drpBlock.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpGp.DataSource = ds.Tables[0];
            drpGp.DataTextField = "GP_Name";
            drpGp.DataValueField = "GP_Code";
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
            string sql = "select villg_code, villg_name from dbo.Village where gp_code=@gp_code order by villg_name";
            string[] param = { "@gp_code" };
            object[] value = { drpGp.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpVillage.DataSource = ds.Tables[0];
            drpVillage.DataTextField = "villg_name";
            drpVillage.DataValueField = "villg_code";
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
        string sql = "GET_RjctFarmerList";
        string[] param = { "@VCHDISTID", "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
        object[] value = {string.Format("{0:00}", int.Parse(Session["DistCode"].ToString())), drpBlock.SelectedValue.Trim() == "---All---" ? "0" : drpBlock.SelectedValue.Trim(), drpGp.SelectedValue.Trim() == "---All---" ? "0" : drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
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
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_FarmerList_Block : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillDist();
            fillBLock();
            fillGP();
        }
    }
    private void fillGP()
    {


        drpGp.Items.Clear();
        drpVillage.Items.Clear();
        DataSet ds = new DataSet();
        string sql = "select GP_Code,  GP_Name from  dbo.Tbl_GP where Block_Code=@Block_Code order by GP_Name";
        string[] param = { "@Block_Code" };
        object[] value = { Session["Block"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        drpGp.DataSource = ds.Tables[0];
        drpGp.DataTextField = "GP_Name";
        drpGp.DataValueField = "GP_Code";
        drpGp.DataBind();
        this.drpGp.Items.Insert(0, "---All---");
        this.drpVillage.Items.Insert(0, "---All---");
    }
    private void fillDist()
    {
        DataSet ds = new DataSet();
        string sql = "select Dist_Name, NDist_Code from dbo.Tbl_Dist where NDist_Code=@NDist_Code order by Dist_Name";
        string[] param = { "@NDist_Code" };
        object[] value = { Session["Dist"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_dst.Text = ds.Tables[0].Rows[0]["Dist_Name"].ToString();
        }
        else
        {

            lbl_dst.Text = "-Data Not Found-";
        }
    }
    private void fillBLock()
    {
        DataSet ds = new DataSet();
        string sql = "select  block_name,Nblock_code  from block where Nblock_code=@PFMS_Dist_Code order by block_name";
        string[] param = { "@PFMS_Dist_Code" };
        object[] value = { Session["Block"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_blck.Text = ds.Tables[0].Rows[0]["block_name"].ToString();
        }
        else
        {

            lbl_blck.Text = "-Data Not Found-";
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
        string id = Request.QueryString["id"].ToString();
        if (id == "2")
        {
            DataSet ds = new DataSet();
            string sql = "SELECT INTFARMERID,  VCHFARMERCODE, VCHEXISTFRCODE, VCHDISTID, VCHBLOCKID, VCHGPID, VCHVILLAGEID, INTTITLE, VCHFARMERNAME, VCHFATHERNAME,  INTAGE, CHAGESTATUS, LEFT(VCHVOTERIDCARDNO,LEN(VCHVOTERIDCARDNO)-2)+'**' as VCHVOTERIDCARDNOV,VCHVOTERIDCARDNO, VCHAADHARNO,case when  LEN(VCHMOBILENO)>2 then LEFT(VCHMOBILENO,LEN(VCHMOBILENO)-2)+'**' else '<i style=color:red>-- n/a --</i>' end as VCHMOBILENOV,VCHMOBILENO, INTFAMILYMEMBERS,   VCHPINCODE,   NICFARMERID,D.Dist_Name,B.block_name,GP.GP_Name,V.villg_name,GEN.Gender_Value,CAT.Category_Value FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] FR INNER JOIN [FARMERDB].[DBO].[Tbl_FarmerApprove] FA ON FA.Farmer_Code=FR.VCHFARMERCODE   INNER JOIN [FARMERDB].[DBO].[Tbl_Dist] D ON D.NDist_Code=FR.VCHDISTID INNER JOIN [FARMERDB].[DBO].[block] B ON B.Nblock_code=FR.VCHBLOCKID  INNER JOIN [FARMERDB].[DBO].[Tbl_GP] GP ON GP.GP_Code=FR.VCHGPID  LEFT JOIN [FARMERDB].[DBO].[Village] V ON V.villg_code=FR.VCHVILLAGEID  INNER JOIN [FARMERDB].[DBO].[Tbl_Gender] GEN ON GEN.Gender_ID=FR.INTGENDER  INNER JOIN [FARMERDB].[DBO].[Tbl_Category] CAT ON CAT.Category_ID=FR.INTCATEGOGY  WHERE  (@VCHBLOCKID =0 OR VCHBLOCKID=@VCHBLOCKID)  AND (@VCHGPID =0 OR VCHGPID=@VCHGPID)  AND (@VCHVILLAGEID =0 OR VCHVILLAGEID=@VCHVILLAGEID)  order by VCHFARMERNAME  ";
            string[] param = { "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
            object[] value = { Session["Block"].ToString(), drpGp.SelectedValue.Trim() == "---All---" ? "0" : drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
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
        else if (id == "1")
        {
            DataSet ds = new DataSet();
            string sql = "SELECT INTFARMERID,  VCHFARMERCODE, VCHEXISTFRCODE, VCHDISTID, VCHBLOCKID, VCHGPID, VCHVILLAGEID, INTTITLE, VCHFARMERNAME, VCHFATHERNAME,  INTAGE, CHAGESTATUS, LEFT(VCHVOTERIDCARDNO,LEN(VCHVOTERIDCARDNO)-2)+'**' as VCHVOTERIDCARDNOV,VCHVOTERIDCARDNO, VCHAADHARNO,case when  LEN(VCHMOBILENO)>2 then LEFT(VCHMOBILENO,LEN(VCHMOBILENO)-2)+'**' else '<i style=color:red>-- n/a --</i>' end as VCHMOBILENOV,VCHMOBILENO, INTFAMILYMEMBERS,   VCHPINCODE,   NICFARMERID,D.Dist_Name,B.block_name,GP.GP_Name,V.villg_name,GEN.Gender_Value,CAT.Category_Value FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] FR   INNER JOIN [FARMERDB].[DBO].[Tbl_Dist] D ON D.NDist_Code=FR.VCHDISTID INNER JOIN [FARMERDB].[DBO].[block] B ON B.Nblock_code=FR.VCHBLOCKID  INNER JOIN [FARMERDB].[DBO].[Tbl_GP] GP ON GP.GP_Code=FR.VCHGPID  LEFT JOIN [FARMERDB].[DBO].[Village] V ON V.villg_code=FR.VCHVILLAGEID  INNER JOIN [FARMERDB].[DBO].[Tbl_Gender] GEN ON GEN.Gender_ID=FR.INTGENDER  INNER JOIN [FARMERDB].[DBO].[Tbl_Category] CAT ON CAT.Category_ID=FR.INTCATEGOGY  WHERE  (@VCHBLOCKID =0 OR VCHBLOCKID=@VCHBLOCKID)  AND (@VCHGPID =0 OR VCHGPID=@VCHGPID)  AND (@VCHVILLAGEID =0 OR VCHVILLAGEID=@VCHVILLAGEID)  order by VCHFARMERNAME  ";
            string[] param = { "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
            object[] value = { Session["Block"].ToString(), drpGp.SelectedValue.Trim() == "---All---" ? "0" : drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
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
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAO_REPORT_LIST.aspx");
    }
}
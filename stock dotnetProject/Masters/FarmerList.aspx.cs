using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_FarmerList : System.Web.UI.Page
{
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] param = { "@DIST_CODE" };
            object[] value = { Session["DistCode"].ToString() };
            ds = dbsFarmer.param_passing_fetchDataSet("SELECT NDIST_CODE FROM [FARMERDB].[DBO].[Tbl_Dist] WHERE NICDIST_CODE = @DIST_CODE", param, value);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["PFMS_DIST_CODE"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["NDIST_CODE"].ToString());
                }
            }
            fillBlock();
        }
    }
    private void fillBlock()
    {
        drpBlock.Items.Clear();
        DataSet ds = new DataSet();
        string sql = "SELECT BLOCK_NAME,NBLOCK_CODE FROM [FARMERDB].[DBO].[BLOCK] WHERE LEFT(NBLOCK_CODE,2) = @PFMS_DIST_CODE ORDER BY BLOCK_NAME";
        string[] param = { "@PFMS_Dist_Code" };
        object[] value = { Session["PFMS_DIST_CODE"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        drpBlock.DataSource = ds.Tables[0];
        drpBlock.DataTextField = "block_name";
        drpBlock.DataValueField = "Nblock_code";
        drpBlock.DataBind();
        this.drpBlock.Items.Insert(0, "---All---");
        this.drpGp.Items.Insert(0, "---All---");
        this.drpVillage.Items.Insert(0, "---All---");
    }

    protected void drpBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBlock.SelectedIndex > 0)
        {
            drpGp.Items.Clear();
            drpVillage.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select GP_Code,GP_Name from [FARMERDB].[DBO].[Tbl_GP] where Block_Code = @Block_Code order by GP_Name";
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
            string sql = "select villg_code,villg_name from [FARMERDB].[DBO].[Village] where gp_code=@gp_code order by villg_name";
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
        string sql = "GET_FarmerList";
        string[] param = { "@VCHDISTID", "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
        object[] value = { Session["PFMS_DIST_CODE"].ToString(), drpBlock.SelectedValue.Trim() == "---All---" ? "0" : drpBlock.SelectedValue.Trim(), drpGp.SelectedValue.Trim() == "---All---" ? "0" : drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim() };
        ds = dbsFarmer.param_passing_fetchDataSet("SELECT INTFARMERID,  VCHFARMERCODE, VCHEXISTFRCODE, VCHDISTID, VCHBLOCKID, VCHGPID, VCHVILLAGEID, INTTITLE, VCHFARMERNAME, VCHFATHERNAME,  INTAGE, CHAGESTATUS, LEFT(VCHVOTERIDCARDNO,LEN(VCHVOTERIDCARDNO)-2)+'**' as VCHVOTERIDCARDNOV,VCHVOTERIDCARDNO,   " +
        "VCHAADHARNO, LEFT(VCHMOBILENO,LEN(VCHMOBILENO)-2)+'**' as VCHMOBILENOV,VCHMOBILENO, INTFAMILYMEMBERS,   VCHPINCODE,   NICFARMERID,D.Dist_Name,B.block_name,GP.GP_Name,V.villg_name,GEN.Gender_Value,CAT.Category_Value      " +
        "FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] FR      " +
        "INNER JOIN [FARMERDB].[DBO].[Tbl_FarmerApprove] FA ON FA.Farmer_Code=FR.VCHFARMERCODE  " +
        "INNER JOIN [FARMERDB].[DBO].[Tbl_Dist] D ON D.NDist_Code=FR.VCHDISTID      " +
        "INNER JOIN [FARMERDB].[DBO].[block] B ON B.Nblock_code=FR.VCHBLOCKID      " +
        "INNER JOIN [FARMERDB].[DBO].[Tbl_GP] GP ON GP.GP_Code=FR.VCHGPID      " +
        "INNER JOIN [FARMERDB].[DBO].[Village] V ON V.villg_code=FR.VCHVILLAGEID " +
        "INNER JOIN [FARMERDB].[DBO].[Tbl_Gender] GEN ON GEN.Gender_ID=FR.INTGENDER " +
        "INNER JOIN [FARMERDB].[DBO].[Tbl_Category] CAT ON CAT.Category_ID=FR.INTCATEGOGY   " +
        "WHERE Status in ('ACCP','CCB') AND INTDELETEDFLAG=0 AND  INTREJECTED=0    " +
        "and    LEN(VCHMOBILENO)>2  " +
        "AND (@VCHDISTID =0 OR VCHDISTID=@VCHDISTID)      " +
        " AND (@VCHBLOCKID =0 OR VCHBLOCKID=@VCHBLOCKID)    " +
        "  AND (@VCHGPID =0 OR VCHGPID=@VCHGPID)      " +
        "   AND (@VCHVILLAGEID =0 OR VCHVILLAGEID=@VCHVILLAGEID)      " +
        "   order by VCHFARMERNAME", param, value);
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
    }
}

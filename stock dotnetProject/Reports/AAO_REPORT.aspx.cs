using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_AAO_REPORT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillDist();
            fillBLock();
            fillGP();
            string id = Request.QueryString["id"].ToString();
            HiddenField1.Value = id;
            HiddenField2.Value = Session["UserID"].ToString();
            if (id == "1")
            {

                Literal1.Text = "Rejection By CCB";
            }
            else if (id == "2")
            {
                Literal1.Text = "Rejection By PFMS";
            }
            else if (id == "3")
            {
                Literal1.Text = "Rejection By DEPT";
            }
            else
            {
                Response.Redirect("AAO_REPORT_LIST.aspx");
            }

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
        if (id == "1")
        {
            DataSet ds = new DataSet();
            if (drpGp.SelectedValue.Trim() == "---All---")
            {
                string sql = "select b.VCHFARMERCODE,b.VCHFARMERNAME,b.VCHFATHERNAME,b.VCHACCOUNTNO,b.INTBANKID,b.INTBRANCHID,b.VCHACCHOLDERNM,'Rej. By CCB' as status,(select count(*) from [FARMERDB].[dbo].[TBL_FARMER_CHANGEBLOCK] where VCHFARMERCODE=b.VCHFARMERCODE) as upst from  [dbo].[Response_CCB_Rjct] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.VCHFARMERCODE=b.VCHFARMERCODE where b.INTREJECTED=0 and b.INTDELETEDFLAG=0 and b.VCHBLOCKID=@block ";
                string[] param = { "@block" };
                object[] value = { Session["Block"].ToString() };
                ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
            else
            {
                string sql = "select b.VCHFARMERCODE,b.VCHFARMERNAME,b.VCHFATHERNAME,b.VCHACCOUNTNO,b.INTBANKID,b.INTBRANCHID,b.VCHACCHOLDERNM,'Rej. By CCB' as status ,(select count(*) from [FARMERDB].[dbo].[TBL_FARMER_CHANGEBLOCK] where VCHFARMERCODE=b.VCHFARMERCODE) as upstfrom  [dbo].[Response_CCB_Rjct] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.VCHFARMERCODE=b.VCHFARMERCODE where b.INTREJECTED=0 and b.INTDELETEDFLAG=0 and b.VCHBLOCKID=@block AND VCHGPID=@GPID AND VCHVILLAGEID=@VILLAGE";
                string[] param = { "@block", "@GPID", "@VILLAGE" };
                object[] value = { Session["Block"].ToString(), drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() };
                ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
        }
        else if (id == "2")
        {
            DataSet ds = new DataSet();
            if (drpGp.SelectedValue.Trim() == "---All---")
            {
                string sql = "select b.VCHFARMERCODE,b.VCHFARMERNAME,b.VCHFATHERNAME,b.VCHACCOUNTNO,b.INTBANKID,b.INTBRANCHID,b.VCHACCHOLDERNM,(select top 1 Rejection_Reason_Narration from [FARMERDB].[dbo].[Response_tbl_Beneficiary_Ack_Message] where Scheme_Specific_ID=b.VCHFARMERCODE ) as status,(select count(*) from [FARMERDB].[dbo].[TBL_FARMER_CHANGEBLOCK] where VCHFARMERCODE=b.VCHFARMERCODE) as upst from [dbo].[M_FARMER_REGISTRATION] b where b.VCHFARMERCODE in(select * from(select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in (select  distinct(Scheme_Specific_ID) from (select a.Scheme_Specific_ID,b.INTBANKID from Response_tbl_Beneficiary_Ack_Message a inner join M_FARMER_REGISTRATION b on a.Scheme_Specific_ID=b.VCHFARMERCODE where b.INTDELETEDFLAG=0 and b.INTREJECTED=0 and a.Beneficiary_Status='RJCT'and b.VCHBLOCKID=@block ) as x) and INTBANKID  not in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') and INTBANKID  not in (54,20,65) except (select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in (select  distinct(Scheme_Specific_ID) from (select a.Scheme_Specific_ID,b.INTBANKID from Response_tbl_Beneficiary_Ack_Message a inner join M_FARMER_REGISTRATION b on a.Scheme_Specific_ID=b.VCHFARMERCODE where b.INTDELETEDFLAG=0 and b.INTREJECTED=0 and a.Beneficiary_Status='ACCP'and b.VCHBLOCKID=@block ) as x) and INTBANKID  not in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') and INTBANKID  not in (54,20,65))) as x)";
                string[] param = { "@block" };
                object[] value = { Session["Block"].ToString() };
                ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
            else
            {
                string sql = "select b.VCHFARMERCODE,b.VCHFARMERNAME,b.VCHFATHERNAME,b.VCHACCOUNTNO,b.INTBANKID,b.INTBRANCHID,b.VCHACCHOLDERNM,(select top 1 Rejection_Reason_Narration from [FARMERDB].[dbo].[Response_tbl_Beneficiary_Ack_Message] where Scheme_Specific_ID=b.VCHFARMERCODE ) as status ,(select count(*) from [FARMERDB].[dbo].[TBL_FARMER_CHANGEBLOCK] where VCHFARMERCODE=b.VCHFARMERCODE) as upst from [dbo].[M_FARMER_REGISTRATION] b where b.VCHFARMERCODE in(select * from(select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in (select  distinct(Scheme_Specific_ID) from (select a.Scheme_Specific_ID,b.INTBANKID from Response_tbl_Beneficiary_Ack_Message a inner join M_FARMER_REGISTRATION b on a.Scheme_Specific_ID=b.VCHFARMERCODE where b.INTDELETEDFLAG=0 and b.INTREJECTED=0 and a.Beneficiary_Status='RJCT'and b.VCHBLOCKID=@block ) as x) and INTBANKID  not in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') and INTBANKID  not in (54,20,65) except (select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in (select  distinct(Scheme_Specific_ID) from (select a.Scheme_Specific_ID,b.INTBANKID from Response_tbl_Beneficiary_Ack_Message a inner join M_FARMER_REGISTRATION b on a.Scheme_Specific_ID=b.VCHFARMERCODE where b.INTDELETEDFLAG=0 and b.INTREJECTED=0 and a.Beneficiary_Status='ACCP'and b.VCHBLOCKID=@block ) as x) and INTBANKID  not in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') and INTBANKID  not in (54,20,65))) as x) AND VCHGPID=@GPID AND VCHVILLAGEID=@VILLAGE";
                string[] param = { "@block", "@GPID", "@VILLAGE" };
                object[] value = { Session["Block"].ToString(), drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() };
                ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
        }
        else if (id == "3")
        {
            DataSet ds = new DataSet();
            if (drpGp.SelectedValue.Trim() == "---All---")
            {
                string sql = "select c.VCHFARMERCODE,c.VCHFARMERNAME,c.VCHFATHERNAME,c.VCHACCOUNTNO,c.INTBANKID,c.INTBRANCHID,c.VCHACCHOLDERNM,(select top 1 Rejection_Reason_Narration from [FARMERDB].[dbo].[Response_tbl_Beneficiary_Ack_Message] where Scheme_Specific_ID=c.VCHFARMERCODE ) as status,(select count(*) from [FARMERDB].[dbo].[TBL_FARMER_CHANGEBLOCK] where VCHFARMERCODE=c.VCHFARMERCODE) as upst from M_FARMER_REGISTRATION c where c.VCHFARMERCODE in (select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in(select distinct(Scheme_Specific_ID) from [dbo].[Response_tbl_Beneficiary_Ack_Message] a inner join [M_FARMER_REGISTRATION] b on a.Scheme_Specific_ID=b.VCHFARMERCODE where VCHBLOCKID=@block and Beneficiary_Status='ACCP'  and b.INTDELETEDFLAG=0 and b.INTREJECTED=0 except(select Farmer_Code from Tbl_FarmerApprove where Status='ACCP' or Status='CCB')) union select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in(select distinct(Scheme_Specific_ID) from [dbo].[Response_CCB1]a inner join [M_FARMER_REGISTRATION] b on a.Scheme_Specific_ID=b.VCHFARMERCODE where VCHBLOCKID=@block except(select Farmer_Code from Tbl_FarmerApprove where Status='ACCP' or Status='CCB')))";
                string[] param = { "@block" };
                object[] value = { Session["Block"].ToString() };
                ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
            else
            {
                string sql = "select c.VCHFARMERCODE,c.VCHFARMERNAME,c.VCHFATHERNAME,c.VCHACCOUNTNO,c.INTBANKID,c.INTBRANCHID,c.VCHACCHOLDERNM,(select top 1 Rejection_Reason_Narration from [FARMERDB].[dbo].[Response_tbl_Beneficiary_Ack_Message] where Scheme_Specific_ID=c.VCHFARMERCODE ) as status,(select count(*) from [FARMERDB].[dbo].[TBL_FARMER_CHANGEBLOCK] where VCHFARMERCODE=b.VCHFARMERCODE) as upst from M_FARMER_REGISTRATION c where c.VCHFARMERCODE in (select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in(select distinct(Scheme_Specific_ID) from [dbo].[Response_tbl_Beneficiary_Ack_Message] a inner join [M_FARMER_REGISTRATION] b on a.Scheme_Specific_ID=b.VCHFARMERCODE where VCHBLOCKID=@block and Beneficiary_Status='ACCP'  and b.INTDELETEDFLAG=0 and b.INTREJECTED=0 except(select Farmer_Code from Tbl_FarmerApprove where Status='ACCP' or Status='CCB')) union select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in(select distinct(Scheme_Specific_ID) from [dbo].[Response_CCB1]a inner join [M_FARMER_REGISTRATION] b on a.Scheme_Specific_ID=b.VCHFARMERCODE where VCHBLOCKID=@block except(select Farmer_Code from Tbl_FarmerApprove where Status='ACCP' or Status='CCB'))) AND VCHGPID=@GPID AND VCHVILLAGEID=@VILLAGE";
                string[] param = { "@block", "@GPID", "@VILLAGE" };
                object[] value = { Session["Block"].ToString(), drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() };
                ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
                grd_FarmerList.DataSource = ds.Tables[0];
                grd_FarmerList.DataBind();
            }
        }
    }

    protected void grd_FarmerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl_bank = (DropDownList)e.Row.FindControl("ddl_bank");
            DropDownList ddl_branch = (DropDownList)e.Row.FindControl("ddl_branch");
            DataSet ds = new DataSet();
            string sql = "SELECT [Bank_Code] ,[Bank_Name]  FROM [FARMERDB].[dbo].[Tbl_Bank]";
            string[] param = { };
            object[] value = { };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            ddl_bank.DataSource = ds.Tables[0];
            ddl_bank.DataTextField = "Bank_Name";
            ddl_bank.DataValueField = "Bank_Code";
            ddl_bank.DataBind();

            ddl_bank.Items.FindByValue((e.Row.FindControl("hidbank") as HiddenField).Value).Selected = true;
            DataSet ds1 = new DataSet();
            string sql1 = "SELECT [Branch_Code],[Branch_Name]FROM [FARMERDB].[dbo].[Tbl_Branch]  where [Bank_Code]=@bankcode";
            string[] param1 = { "@bankcode" };
            object[] value1 = { ddl_bank.SelectedValue.Trim() };
            ds1 = dbsFarmer.param_passing_fetchDataSet(sql1, param1, value1);
            ddl_branch.DataSource = ds1.Tables[0];
            ddl_branch.DataTextField = "Branch_Name";
            ddl_branch.DataValueField = "Branch_Code";
            ddl_branch.DataBind();
            if (ddl_branch.Items.FindByValue((e.Row.FindControl("hidbranch") as HiddenField).Value) != null)
                ddl_branch.Items.FindByValue((e.Row.FindControl("hidbranch") as HiddenField).Value).Selected = true;

            //ddl_bank.Items.FindByValue((e.Row.FindControl("lblCity") as Label).Text).Selected = true;
        }
    }
    protected void ddl_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_bank = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl_bank.Parent.Parent;
        int idx = row.RowIndex;

        DropDownList ddl_branch = (DropDownList)row.FindControl("ddl_branch");


        if (ddl_bank.SelectedIndex > 0)
        {
            DataSet ds1 = new DataSet();
            string sql1 = "SELECT [Branch_Code],[Branch_Name]FROM [FARMERDB].[dbo].[Tbl_Branch]  where [Bank_Code]=@bankcode";
            string[] param1 = { "@bankcode" };
            object[] value1 = { ddl_bank.SelectedValue.Trim() };
            ds1 = dbsFarmer.param_passing_fetchDataSet(sql1, param1, value1);
            ddl_branch.DataSource = ds1.Tables[0];
            ddl_branch.DataTextField = "Branch_Name";
            ddl_branch.DataValueField = "Branch_Code";
            ddl_branch.DataBind();
        }

    }
    [WebMethod]
    public static void UpdateCustomer(string fcode, string accno, string bank, string branch, string holder, string chby, string types)
    {
        string constr = ConfigurationManager.ConnectionStrings["SqlConFarmer"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("update [FARMERDB].[dbo].[M_FARMER_REGISTRATION] set VCHACCOUNTNO=@VCHACCOUNTNO,INTBANKID=@INTBANKID,INTBRANCHID=@INTBRANCHID,VCHACCHOLDERNM=@VCHACCHOLDERNM where VCHFARMERCODE=@VCHFARMERCODE"))
            {
                cmd.Parameters.AddWithValue("@VCHACCOUNTNO", accno);
                cmd.Parameters.AddWithValue("@INTBANKID", bank);
                cmd.Parameters.AddWithValue("@INTBRANCHID", branch);
                cmd.Parameters.AddWithValue("@VCHACCHOLDERNM", holder);
                cmd.Parameters.AddWithValue("@VCHFARMERCODE", fcode);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("insert into [FARMERDB].[dbo].[TBL_FARMER_CHANGEBLOCK]([VCHFARMERCODE],[USTATUS],[UDATE],[CHANGEDBY]) values(@VCHFARMERCODE,@types,@UDATE,@CHANGEDBY)"))
            {
                cmd.Parameters.AddWithValue("@CHANGEDBY", chby);
                cmd.Parameters.AddWithValue("@UDATE", DateTime.Now);
                cmd.Parameters.AddWithValue("@VCHFARMERCODE", fcode);
                cmd.Parameters.AddWithValue("@types", types);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAO_REPORT_LIST.aspx");
    }
}
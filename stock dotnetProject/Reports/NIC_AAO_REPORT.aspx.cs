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

public partial class Reports_NIC_AAO_REPORT : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    SqlConnection con;
    SqlCommand objCmd = new SqlCommand();
    DataTable dt;
    DataRow dtrow;
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
            if (!Page.IsPostBack)
            {

                //string id = Request.QueryString["id"].ToString();
                HiddenField1.Value = Session["fid"].ToString();
                string BlockCode1 = "select VCHFARMERCODE,VCHFARMERNAME,VCHBLOCKID,VCHDISTID,VCHGPID,VCHVILLAGEID,VCHPINCODE,INTBANKID,INTBRANCHID,VCHIFSCCODE,VCHFATHERNAME,INTGENDER,VCHMOBILENO,VCHAADHARNO,VCHACCHOLDERNM,VCHACCOUNTNO,VCHVOTERIDCARDNO,INTCATEGOGY,INTAGE,convert(varchar(12),DTMDOB, 106) as DTMDOB,INTRELATION,INTFAMILYMEMBERS from [farmerdb].dbo.M_FARMER_REGISTRATION where VCHFARMERCODE=@fc";
                string[] paramBlk1 = { "@fc" };
                object[] valueBlk1 = { HiddenField1.Value };
                DataSet dsBlk1 = dbsAppStock.param_passing_fetch(BlockCode1, paramBlk1, valueBlk1);
                if (dsBlk1.Tables[0].Rows.Count > 0)
                {
                    Session["MDistCode"] = dsBlk1.Tables[0].Rows[0]["VCHDISTID"].ToString();
                    Session["MBlockCode"] = dsBlk1.Tables[0].Rows[0]["VCHBLOCKID"].ToString();
                    Session["MGPCode"] = dsBlk1.Tables[0].Rows[0]["VCHGPID"].ToString();
                    Session["MVlCode"] = dsBlk1.Tables[0].Rows[0]["VCHVILLAGEID"].ToString();
                    txtPinCode.Text = dsBlk1.Tables[0].Rows[0]["VCHPINCODE"].ToString();
                    txtHofNm.Text = dsBlk1.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                    if (dsBlk1.Tables[0].Rows[0]["INTGENDER"].ToString() == "1")
                        txtPinCode0.Text = "Male";
                    else
                        txtPinCode0.Text = "Female";
                    if (dsBlk1.Tables[0].Rows[0]["INTCATEGOGY"].ToString() == "1")
                        txtPinCode1.Text = "General";
                    else if (dsBlk1.Tables[0].Rows[0]["INTCATEGOGY"].ToString() == "2")
                        txtPinCode1.Text = "SC";
                    else
                        txtPinCode1.Text = "ST";
                    txtDtBirth.Text = dsBlk1.Tables[0].Rows[0]["DTMDOB"].ToString();
                    txtAge.Text = dsBlk1.Tables[0].Rows[0]["INTAGE"].ToString();
                    if (dsBlk1.Tables[0].Rows[0]["INTRELATION"].ToString() == "1")
                        txtDtBirth0.Text = "Father";
                    else
                        txtDtBirth0.Text = "Husband";

                    txtHusbandNm.Text = dsBlk1.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                    txtVoterId.Text = dsBlk1.Tables[0].Rows[0]["VCHVOTERIDCARDNO"].ToString();
                    txtAAdhar.Text = dsBlk1.Tables[0].Rows[0]["VCHAADHARNO"].ToString();
                    txtMobileNo.Text = dsBlk1.Tables[0].Rows[0]["VCHMOBILENO"].ToString();
                    txtDtBirth1.Text = dsBlk1.Tables[0].Rows[0]["INTFAMILYMEMBERS"].ToString();
                    txtAccNo0.Text = dsBlk1.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
                    HiddenField2.Value = dsBlk1.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
                    txtAccNo.Text = dsBlk1.Tables[0].Rows[0]["VCHACCOUNTNO"].ToString();
                    fillDist();
                    fillBlock();

                    fillBankNm();
                    ddlBankNm.SelectedValue = dsBlk1.Tables[0].Rows[0]["INTBANKID"].ToString();
                    ddlBankNm_SelectedIndexChanged(sender, e);
                    ddlBranchNm.SelectedValue = dsBlk1.Tables[0].Rows[0]["INTBRANCHID"].ToString();
                    txtIfscCd.Text = dsBlk1.Tables[0].Rows[0]["VCHIFSCCODE"].ToString();
                    HiddenField3.Value = dsBlk1.Tables[0].Rows[0]["VCHFARMERCODE"].ToString();
                    string strBnk = "SELECT VCHNAME  FROM [FARMERDB].[dbo].[T_FARMER_FAMILYDETAILS] WHERE VCHFARMERCODE=@FCODE union select  VCHACCHOLDERNM from [farmerdb].dbo.M_FARMER_REGISTRATION where VCHFARMERCODE=@fc";
                    string[] paramBnk = { "@FCODE", "@fc" };
                    object[] valueBnk = { dsBlk1.Tables[0].Rows[0]["VCHFARMERCODE"].ToString(), HiddenField1.Value.ToString() };
                    DataSet dsBnkNm = dbsApp.param_passing_fetch(strBnk, paramBnk, valueBnk);
                    if (dsBnkNm.Tables[0].Rows.Count > 0)
                    {
                        ddlacch.DataSource = dsBnkNm;
                        ddlacch.DataTextField = "VCHNAME";
                        ddlacch.DataValueField = "VCHNAME";
                        ddlacch.DataBind();
                        ddlacch.SelectedValue = dsBlk1.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
                    }
                    string strBnk1 = "SELECT * FROM [FARMERDB].[dbo].[NIC_accounthname_updated_by_aao] WHERE VCHFARMERCODE=@fc";
                    string[] paramBnk1 = { "@fc" };
                    object[] valueBnk1 = { HiddenField1.Value.ToString() };
                    DataSet dsBnkNm1 = dbsApp.param_passing_fetch(strBnk1, paramBnk1, valueBnk1);
                    if (dsBnkNm1.Tables[0].Rows.Count > 0)
                    {
                        lnk.Visible = false;
                    }

                }
            }
        }
    }

    protected void fillDist()
    {
        try
        {
            string str = "select distinct b.int_DistrictID,(vch_DistrictName)vch_DistrictName from PDS_BLOCKMASTER as a inner join PDS_DistrictMaster as b on b.int_DistrictID=@BlkCode and a.int_DistrictID=b.int_DistrictID";
            string[] paramDist = { "@BlkCode" };
            object[] valueDist = { Session["MDistCode"].ToString() };
            DataSet dsDist = dbsApp.param_passing_fetch(str, paramDist, valueDist);

            if (dsDist.Tables[0].Rows.Count > 0)
            {
                lblDist.Text = dsDist.Tables[0].Rows[0]["vch_DistrictName"].ToString();
                ViewState["DistId"] = dsDist.Tables[0].Rows[0]["int_DistrictID"].ToString();
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void fillBlock()
    {
        try
        {
            string blk = "Select int_BlockID,Upper(vch_BlockName)vch_BlockName  From PDS_BlockMaster Where int_BlockID=@BlockCode Order By vch_BlockName";
            string[] paramBlk = { "@BlockCode" };
            object[] valueBlk = { Session["MBlockCode"].ToString() };
            DataSet dsBlk = dbsApp.param_passing_fetch(blk, paramBlk, valueBlk);
            if (dsBlk.Tables[0].Rows.Count > 0)
            {
                lblBlock.Text = dsBlk.Tables[0].Rows[0]["vch_BlockName"].ToString();
                ViewState["BlockId"] = dsBlk.Tables[0].Rows[0]["int_BlockID"].ToString();
                fillGp();
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void fillBankNm()
    {
        try
        {
            DataSet dsBnk = dbsApp.returnDataset("SELECT INTID,VCHBANKNAME FROM M_PDS_FARMERBANK WHERE INTDELETEDFLAG=0 ORDER BY VCHBANKNAME");
            ddlBankNm.DataSource = dsBnk;
            ddlBankNm.DataTextField = "VCHBANKNAME";
            ddlBankNm.DataValueField = "INTID";
            ddlBankNm.DataBind();
            ddlBankNm.Items.Insert(0, "--select--");
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }


    }

    protected void fillGp()
    {
        try
        {
            string gp = "Select int_GPID,Upper(vch_GPName)vch_GPName From PDS_GPMaster Where int_GPID=@GpCode Order By vch_GPName";
            string[] paramGp = { "@GpCode" };
            object[] valueGp = { Session["MGPCode"].ToString() };
            DataSet dsGp = dbsApp.param_passing_fetch(gp, paramGp, valueGp);
            if (dsGp.Tables[0].Rows.Count > 0)
            {
                lblBlock0.Text = dsGp.Tables[0].Rows[0]["vch_GPName"].ToString();
                ViewState["gpId"] = dsGp.Tables[0].Rows[0]["int_GPID"].ToString();
                fillVillage(ViewState["gpId"].ToString());
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }

    }

    protected void fillVillage(string vill)
    {
        try
        {
            string village = "Select int_VillageID,Upper(vch_VillageName)vch_VillageName From PDS_VillageMaster Where int_VillageID= @GpCode Order By vch_VillageName";
            string[] paramVllg = { "@GpCode" };
            object[] valueVllg = { Session["MVlCode"].ToString() };
            DataSet dsVllg = dbsApp.param_passing_fetch(village, paramVllg, valueVllg);
            if (dsVllg.Tables[0].Rows.Count > 0)
            {
                lblBlock1.Text = dsVllg.Tables[0].Rows[0]["vch_VillageName"].ToString();
                ViewState["vlId"] = dsVllg.Tables[0].Rows[0]["int_VillageID"].ToString();
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }

    }
    protected void txtHofNm_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //lblAccountHolderNm.Text = txtHofNm.Text;
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void ddlBankNm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strBnk = "SELECT INTBRANCHID,(VCHBRANCHNAME+','+VCHDISTRICT +' (' + isnull(NEW_IFSC_CODE,'-')  +')') VCHBRANCHNAME  FROM M_PDS_BANKBRANCH WHERE INTBANKID=@BnkCode AND INTDELETEDFLAG=0 AND NEW_IFSC_CODE IS NOT NULL AND NEW_IFSC_CODE<>'' ORDER BY VCHBRANCHNAME";
            string[] paramBnk = { "@BnkCode" };
            object[] valueBnk = { ddlBankNm.SelectedValue };
            DataSet dsBnkNm = dbsApp.param_passing_fetch(strBnk, paramBnk, valueBnk);
            if (dsBnkNm.Tables[0].Rows.Count > 0)
            {
                ddlBranchNm.DataSource = dsBnkNm;
                ddlBranchNm.DataTextField = "VCHBRANCHNAME";
                ddlBranchNm.DataValueField = "INTBRANCHID";
                ddlBranchNm.DataBind();
                ddlBranchNm.Items.Insert(0, "--select--");
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void ddlBranchNm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = "select NEW_IFSC_CODE from M_PDS_BANKBRANCH where intBranchId=@BnchId";
            string[] strParam = { "@BnchId" };
            object[] Value = { ddlBranchNm.SelectedValue };
            DataSet dsIfcs = dbsApp.param_passing_fetch(str, strParam, Value);
            if (dsIfcs.Tables[0].Rows.Count > 0)
            {
                txtIfscCd.Text = dsIfcs.Tables[0].Rows[0]["NEW_IFSC_CODE"].ToString();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtAccNo.Text))
            {
                string pop = "alert('Please Enter Account No. !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
            if (string.IsNullOrEmpty(txtAccNo0.Text))
            {
                string pop = "alert('Please Enter Account NAme. !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
            else if (ddlBankNm.SelectedIndex == 0)
            {
                string pop = "alert('Please Select Bank Name !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
            else if (ddlBranchNm.SelectedIndex == 0)
            {
                string pop = "alert('Please Select Branch Name !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
            else
            {
                //string strAcc = "select VCHVOTERIDCARDNO,VCHAADHARNO,VCHACCOUNTNO from M_FARMER_REGISTRATION where "
                //            + " REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(VCHACCOUNTNO,'/',''),' ',''),'-',''),'\',''),'.','')=REPLACE(REPLACE(REPLACE(@AccNo,'/',''),' ',''),'-','')  and VCHFARMERCODE!=@hid";
                //string[] paramAcc = { "@AccNo", "@hid" };
                //object[] valueAcc = { txtAccNo.Text.Trim(), HiddenField1.Value.ToString() };
                //DataSet dsAcc = dbsApp.param_passing_fetch(strAcc, paramAcc, valueAcc);
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
                objCmd = new SqlCommand("get_ISDupAcc", con);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@AccNo", SqlDbType.VarChar).Value = txtAccNo.Text.Trim();
                objCmd.Parameters.Add("@hid", SqlDbType.VarChar).Value = HiddenField1.Value.ToString();
                DataSet dsAcc = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(objCmd);
                da.Fill(dsAcc);
                if (dsAcc.Tables[0].Rows.Count > 0)
                {
                    string pop = "alert('Account No. Already Registered!!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else
                {
                    //NIC_AAO_RJCT_FARMER_update
                     con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());

                    objCmd = new SqlCommand("NIC_AAO_RJCT_FARMER_update1", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@FID", SqlDbType.VarChar).Value = HiddenField1.Value.ToString();
                    objCmd.Parameters.Add("@acchldnm", SqlDbType.NVarChar).Value = txtAccNo0.Text;
                    objCmd.Parameters.Add("@accountno", SqlDbType.NVarChar).Value = txtAccNo.Text;
                    objCmd.Parameters.Add("@bank", SqlDbType.Int).Value = Convert.ToInt32(ddlBankNm.SelectedValue);
                    objCmd.Parameters.Add("@branch", SqlDbType.Int).Value = Convert.ToInt32(ddlBranchNm.SelectedValue);
                    objCmd.Parameters.Add("@ifsc", SqlDbType.NVarChar).Value = txtIfscCd.Text;
                    objCmd.Parameters.Add("@adhar", SqlDbType.VarChar).Value = txtAAdhar.Text;
                    objCmd.Parameters.Add("@mob", SqlDbType.VarChar).Value = txtMobileNo.Text;
                    objCmd.Parameters.Add("@updateby", SqlDbType.NVarChar).Value = Session["UserID"].ToString();
                    objCmd.Parameters.Add("@output", SqlDbType.Int);
                    objCmd.Parameters["@output"].Direction = ParameterDirection.Output;
                    con.Open();
                    objCmd.ExecuteNonQuery();
                    string output = objCmd.Parameters["@output"].Value.ToString();
                    con.Close();
                    if (output == "0")
                    {
                        string pop = "alert('Successfully  Updated!!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        btnSave.Enabled = false;
                    }
                    else if (output == "1")
                    {
                        string pop = "alert('Not Updated Any Field.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }
                    else if (output == "2")
                    {
                        string pop = "alert('Already updated.Please go Back');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }

                }
            }
        }
        catch (Exception exception1)
        {
            con.Close();
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }

    protected void btnSave0_Click(object sender, EventArgs e)
    {
        Response.Redirect("NIC_AAO_REPORT_LIST.aspx");
    }
    protected void ddlacch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAccNo0.Text = ddlacch.SelectedValue.ToString();
        txtAccNo.Text = "";
        txtIfscCd.Text = "";
        ddlBankNm.SelectedIndex = 0;
        ddlBranchNm.SelectedIndex = 0;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (HiddenField2.Value == ddlacch.SelectedValue)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());

                objCmd = new SqlCommand("[dbo].[NIC_AAO_RJCT_ACCHOLD_update]", con);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@FID", SqlDbType.VarChar).Value = HiddenField1.Value.ToString();
                objCmd.Parameters.Add("@oldacchldnm", SqlDbType.NVarChar).Value = ddlacch.SelectedValue;
                objCmd.Parameters.Add("@acchldnm", SqlDbType.NVarChar).Value = txtAccNo0.Text;
                objCmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = "B";
                objCmd.Parameters.Add("@updateby", SqlDbType.NVarChar).Value = Session["UserID"].ToString();
                objCmd.Parameters.Add("@output", SqlDbType.Int);
                objCmd.Parameters["@output"].Direction = ParameterDirection.Output;
                con.Open();
                objCmd.ExecuteNonQuery();
                string output = objCmd.Parameters["@output"].Value.ToString();
                con.Close();
                if (output == "0")
                {
                    string pop = "alert('Successfully  Updated!!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    //btnSave.Enabled = false;
                }
                else if (output == "1")
                {
                    string pop = "alert('Successfully  Updated.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }



            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
                objCmd = new SqlCommand("[dbo].[NIC_AAO_RJCT_ACCHOLD_update]", con);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@FID", SqlDbType.VarChar).Value = HiddenField1.Value.ToString();
                objCmd.Parameters.Add("@oldacchldnm", SqlDbType.NVarChar).Value = ddlacch.SelectedValue;
                objCmd.Parameters.Add("@acchldnm", SqlDbType.NVarChar).Value = txtAccNo0.Text;
                objCmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = "F";
                objCmd.Parameters.Add("@updateby", SqlDbType.NVarChar).Value = Session["UserID"].ToString();
                objCmd.Parameters.Add("@output", SqlDbType.Int);
                objCmd.Parameters["@output"].Direction = ParameterDirection.Output;
                con.Open();
                objCmd.ExecuteNonQuery();
                string output = objCmd.Parameters["@output"].Value.ToString();
                con.Close();
                if (output == "0")
                {
                    string pop = "alert('Successfully  Updated!!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    //btnSave.Enabled = false;
                }
                else if (output == "1")
                {
                    string pop = "alert('Successfully  Updated.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }
            }


            string strBnk = "SELECT VCHNAME  FROM [FARMERDB].[dbo].[T_FARMER_FAMILYDETAILS] WHERE VCHFARMERCODE=@FCODE union select  VCHACCHOLDERNM from [farmerdb].dbo.M_FARMER_REGISTRATION where VCHFARMERCODE=@fc";
            string[] paramBnk = { "@FCODE", "@fc" };
            object[] valueBnk = { HiddenField3.Value.ToString(), HiddenField1.Value.ToString() };
            DataSet dsBnkNm = dbsApp.param_passing_fetch(strBnk, paramBnk, valueBnk);
            if (dsBnkNm.Tables[0].Rows.Count > 0)
            {
                ddlacch.DataSource = dsBnkNm;
                ddlacch.DataTextField = "VCHNAME";
                ddlacch.DataValueField = "VCHNAME";
                ddlacch.DataBind();
                ddlacch.SelectedValue = txtAccNo0.Text;
            }
            string strBnk1 = "SELECT * FROM [FARMERDB].[dbo].[NIC_accounthname_updated_by_aao] WHERE VCHFARMERCODE=@fc";
            string[] paramBnk1 = { "@fc" };
            object[] valueBnk1 = { HiddenField1.Value.ToString() };
            DataSet dsBnkNm1 = dbsApp.param_passing_fetch(strBnk1, paramBnk1, valueBnk1);
            if (dsBnkNm1.Tables[0].Rows.Count > 0)
            {
                lnk.Visible = false;
            }
        }
        catch (Exception exception1)
        {
            con.Close();
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
    }
}
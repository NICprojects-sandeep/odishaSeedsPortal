using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public partial class DBT_DBTFarmerReg : System.Web.UI.Page
{
    private const string key = "0cwiza8@ms/e_-3d";

    private UtilityLibrary utl = new UtilityLibrary();
    SqlConnection con;
    SqlCommand objCmd = new SqlCommand();
    DataTable dt;
    DataRow dtrow;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if ((this.Session["UserType"].ToString() == "AAOO"))
        {
            this.MasterPageFile = "~/MasterPages/AAO_.master";
        }
        if ((this.Session["UserType"].ToString() == "AHOO"))
        {
            this.MasterPageFile = "~/MasterPages/AHO.master";
        }

    }
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
            if ((this.Session["UserType"].ToString() == "AAOO") || (this.Session["UserType"].ToString() == "AHOO"))
            {

            }
            else
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                string BlockCode1 = "select BLK_CODE from [Stock].dbo.JALANIDHI_DAO_AAO where AAO_CODE=@AAOCode AND PP='1'";
                string[] paramBlk1 = { "@AAOCode" };
                object[] valueBlk1 = { Session["UserID"].ToString().Substring(4, 6) };
                DataSet dsBlk1 = dbsAppStock.param_passing_fetch(BlockCode1, paramBlk1, valueBlk1);
                if (dsBlk1.Tables[0].Rows.Count > 0)
                {
                    Session["BlockCode"] = dsBlk1.Tables[0].Rows[0]["BLK_CODE"].ToString();
                }
                //Session["BlockCode"] = Session["UserID"].ToString().Substring(4,6);
                tbFarmdetails.Visible = false;
                trFarmdetails.Visible = true;
                trFamilyDetails.Visible = true;
                trBankDetails.Visible = true;
                fillDist();
                fillBlock();
                ddlNoFamily_SelectedIndexChanged(sender, e);
                fillBankNm();
                this.utl.SetSessionCookie();
                this.hfSession.Value = HttpUtility.HtmlEncode(this.Session["AuthTokenPage"].ToString());
                return;
            }
            else
            {
                ddlNoFamily_SelectedIndexChanged(null, null);
                txtHofNm_TextChanged(null, null);
            }
        }
    }
    
    protected void fillDist()
    {
        try
        {
            string str = "select distinct b.int_DistrictID,(vch_DistrictName)vch_DistrictName from PDS_BLOCKMASTER as a inner join PDS_DistrictMaster as b on NIC_BlockCode=@BlkCode and a.int_DistrictID=b.int_DistrictID";
            string[] paramDist = { "BlkCode" };
            object[] valueDist = { Session["BlockCode"].ToString() };
            DataSet dsDist = dbsApp.param_passing_fetch(str, paramDist, valueDist);

            if (dsDist.Tables[0].Rows.Count > 0)
            {
                lblDist.Text = dsDist.Tables[0].Rows[0]["vch_DistrictName"].ToString();
                ViewState["DistId"] = dsDist.Tables[0].Rows[0]["int_DistrictID"].ToString();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void fillBlock()
    {
        string blk = "Select int_BlockID,Upper(vch_BlockName)vch_BlockName  From PDS_BlockMaster Where NIC_BlockCode=@BlockCode Order By vch_BlockName";
        string[] paramBlk = { "@BlockCode" };
        object[] valueBlk = { Session["BlockCode"].ToString() };
        DataSet dsBlk = dbsApp.param_passing_fetch(blk, paramBlk, valueBlk);
        if (dsBlk.Tables[0].Rows.Count > 0)
        {
            ddlBlock.DataSource = dsBlk;
            ddlBlock.DataTextField = "vch_BlockName";
            ddlBlock.DataValueField = "int_BlockID";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, "--select--");
        }
    }
    protected void fillBankNm()
    {
        DataSet dsBnk = dbsApp.returnDataset("SELECT INTID,VCHBANKNAME FROM M_PDS_FARMERBANK WHERE INTDELETEDFLAG=0 and apicolFlag is null ORDER BY VCHBANKNAME");
        ddlBankNm.DataSource = dsBnk;
        ddlBankNm.DataTextField = "VCHBANKNAME";
        ddlBankNm.DataValueField = "INTID";
        ddlBankNm.DataBind();
        ddlBankNm.Items.Insert(0, "--select--");
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBlock.SelectedIndex == 0)
            {
                ddlGp.Items.Clear();
                ddlVillage.Items.Clear();
            }
            else
            {
                fillGp();
                ddlVillage.Items.Clear();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void fillGp()
    {
        string gp = "Select distinct int_GPID,Upper(vch_GPName)vch_GPName From PDS_GPMaster Where int_BlockID=@GpCode Order By vch_GPName";
        string[] paramGp = { "@GpCode" };
        object[] valueGp = { ddlBlock.SelectedValue };
        DataSet dsGp = dbsApp.param_passing_fetch(gp, paramGp, valueGp);
        if (dsGp.Tables[0].Rows.Count > 0)
        {
            ddlGp.DataSource = dsGp;
            ddlGp.DataTextField = "vch_GPName";
            ddlGp.DataValueField = "int_GPID";
            ddlGp.DataBind();
            ddlGp.Items.Insert(0, "--select--");
        }
    }
    protected void ddlGp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlGp.SelectedIndex == 0)
            {
                ddlVillage.Items.Clear();
            }
            else
            {
                fillVillage();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void fillVillage()
    {
        string village = "Select int_VillageID,Upper(vch_VillageName)vch_VillageName From PDS_VillageMaster Where int_GPID= @GpCode Order By vch_VillageName";
        string[] paramVllg = { "@GpCode" };
        object[] valueVllg = { ddlGp.SelectedValue };
        DataSet dsVllg = dbsApp.param_passing_fetch(village, paramVllg, valueVllg);
        if (dsVllg.Tables[0].Rows.Count > 0)
        {
            ddlVillage.DataSource = dsVllg;
            ddlVillage.DataTextField = "vch_VillageName";
            ddlVillage.DataValueField = "int_VillageID";
            ddlVillage.DataBind();
            ddlVillage.Items.Insert(0, "--select--");
        }
    }
    protected void ddlNoFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        AddRow();
    }
    public void datatable()
    {
        dt = new DataTable();
        if (dt.Rows.Count == 0)
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            column.ColumnName = "Id";
            dt.Columns.Add(column);
            dt.Columns.Add(new DataColumn("txt1", typeof(string)));
            dt.Columns.Add(new DataColumn("dd", typeof(string)));
            dt.Columns.Add(new DataColumn("txt2", typeof(string)));
            dt.Columns.Add(new DataColumn("chk", typeof(string)));
        }
    }
    public void AddRow()
    {
        datatable();
        int rowCount = Convert.ToInt32(ddlNoFamily.SelectedValue);
        for (int i = 0; i < rowCount; i++)
        {
            if (ViewState["CurrentTable"] == null)
            {
                dtrow = dt.NewRow();
                dtrow["txt1"] = "";
                dtrow["dd"] = "";
                dtrow["txt2"] = "";
                dtrow["chk"] = "";
                dt.Rows.Add(dtrow);
                ViewState["CurrentTable"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["CurrentTable"];
                dtrow = dt.NewRow();
                dtrow["txt1"] = "";
                dtrow["dd"] = "";
                dtrow["txt2"] = "";
                dtrow["chk"] = "";
                dt.Rows.Add(dtrow);
                ViewState["CurrentTable"] = dt;
            }
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ViewState["CurrentTable"] = null;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string dcode = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            string txt = e.Row.Cells[1].Text;
            if (txt == string.Empty)
            {
                TextBox tt = new TextBox();
                tt.ID = "text_" + e.Row.RowIndex.ToString();
                e.Row.Cells[1].Controls.Add(tt);
            }
            string dd = e.Row.Cells[2].Text;
            if (dd == string.Empty)
            {
                DropDownList drp = new DropDownList();
                drp.ID = "dd_" + e.Row.RowIndex.ToString();
                DataSet dsRel = dbsApp.returnDataset("select INTRELATIONID,VCHORIYAREL from M_PDS_RELATIONSHIP order by VCHORIYAREL asc");
                if (dsRel.Tables[0].Rows.Count > 0)
                {
                    drp.DataSource = dsRel;
                    drp.DataValueField = "INTRELATIONID";
                    drp.DataTextField = "VCHORIYAREL";
                    drp.DataBind();
                    drp.Items.Insert(0, "--Select--");
                    drp.SelectedIndex = -1;
                }
                e.Row.Cells[2].Controls.Add(drp);
            }
            string txt2 = e.Row.Cells[2].Text;
            if (txt2 == string.Empty)
            {
                TextBox tt = new TextBox();
                tt.ID = "text2_" + e.Row.RowIndex.ToString();
                e.Row.Cells[3].Controls.Add(tt);
            }
            string chk = e.Row.Cells[3].Text;
            if (chk == string.Empty)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.ID = "CheckBox_" + e.Row.RowIndex.ToString();
                checkBox.AutoPostBack = true;
                checkBox.CheckedChanged += checkBox_CheckedChanged;
                e.Row.Cells[4].Controls.Add(checkBox);
            }
        }
    }
    void checkBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow gr = (GridViewRow)chk.Parent.Parent;
        if (chk.Checked == true)
        {
            int k = Convert.ToInt16(gr.RowIndex);
            string text1_id = "text_" + k.ToString();
            TextBox txt = (TextBox)gr.Cells[1].FindControl(text1_id);
            lblAccountHolderNm.Text = txt.Text;
        }
        else
        {
            lblAccountHolderNm.Text = "";
            lblAccountHolderNm.Text = txtHofNm.Text;
        }
    }
    protected void txtHofNm_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblAccountHolderNm.Text = txtHofNm.Text;
        }
        catch (Exception ex)
        { }
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
        catch (Exception ex)
        { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid == true)
            {
                if (ddlBlock.SelectedIndex == 0)
                {
                    string pop = "alert('Please Select Block !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (ddlGp.SelectedIndex == 0)
                {
                    string pop = "alert('Please Select GP !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (ddlVillage.SelectedIndex == 0 || ddlVillage.SelectedItem.Value == "")
                {
                    string pop = "alert('Please Select Village !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (string.IsNullOrEmpty(txtPinCode.Text))
                {
                    string pop = "alert('Please Enter Pin Code !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (string.IsNullOrEmpty(txtHofNm.Text))
                {
                    string pop = "alert('Please Enter HOF !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (ddlCategory.SelectedIndex == 0)
                {
                    string pop = "alert('Please Select Category !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (string.IsNullOrEmpty(txtAge.Text))
                {
                    string pop = "alert('Please Enter Age !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (ddlRelation.SelectedIndex == 0)
                {
                    string pop = "alert('Please Select relationship !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (string.IsNullOrEmpty(txtHusbandNm.Text))
                {
                    string pop = "alert('Please enter Father/Husband Name !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (string.IsNullOrEmpty(txtVoterId.Text))
                {
                    string pop = "alert('Please Enter Voter Id !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else if (string.IsNullOrEmpty(txtMobileNo.Text))
                {
                    string pop = "alert('Please Enter Mobile No. !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                //else if (string.IsNullOrEmpty(txtAAdhar.Text))
                //{
                //    string pop = "alert('Please Enter Aadhaar No. !!!');";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                //    return;
                //}
                else if (string.IsNullOrEmpty(txtAccNo.Text))
                {
                    string pop = "alert('Please Enter Account No. !!!');";
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
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    int k = Convert.ToInt16(gr.RowIndex);
                    string text1_id = "text_" + k.ToString();
                    string text2_id = "text2_" + gr.RowIndex.ToString();
                    string drp_id = "dd_" + gr.RowIndex.ToString();
                    string chk_id = "CheckBox_" + gr.RowIndex.ToString();
                    CheckBox chkbox = (CheckBox)gr.Cells[4].FindControl(chk_id);

                    TextBox _familyNm = (TextBox)gr.Cells[1].FindControl(text1_id);
                    TextBox _familyAge = (TextBox)gr.Cells[3].FindControl(text2_id);
                    DropDownList _familyRelation = (DropDownList)gr.Cells[2].FindControl(drp_id);
                    if (string.IsNullOrEmpty(_familyNm.Text))
                    {
                        string pop = "alert('Please Enter Family Member Name !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        return;
                    }
                    else if (string.IsNullOrEmpty(_familyAge.Text))
                    {
                        string pop = "alert('Please Enter Family Member Age !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        return;
                    }
                    else if (_familyRelation.SelectedIndex == 0)
                    {
                        string pop = "alert('Please Select Family Member Relationship !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        return;
                    }
                }
                /////Check voter Id
                string strChk = "select VCHVOTERIDCARDNO,VCHAADHARNO,VCHACCOUNTNO from M_FARMER_REGISTRATION where "
                      + " REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(VCHVOTERIDCARDNO,'/',''),' ',''),'-',''),'\',''),'.','')=REPLACE(REPLACE(REPLACE(@VoterId,'/',''),' ',''),'-','') and INTDELETEDFLAG=0 ";

                string[] paramChk = { "@VoterId" };
                object[] valueChk = { txtVoterId.Text.Trim() };
                DataSet dsChk = dbsApp.param_passing_fetch(strChk, paramChk, valueChk);
                if (dsChk.Tables[0].Rows.Count > 0)
                {
                    txtVoterId.Text = "";
                    string pop = "alert('Voter Id Already Registered!!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                else
                {
                    /////Check Account No.
                    string strAcc = "select VCHVOTERIDCARDNO,VCHAADHARNO,VCHACCOUNTNO from M_FARMER_REGISTRATION where "
                             + " REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(VCHACCOUNTNO,'/',''),' ',''),'-',''),'\',''),'.','')=REPLACE(REPLACE(REPLACE(@AccNo,'/',''),' ',''),'-','') and INTDELETEDFLAG=0 ";
                    string[] paramAcc = { "@AccNo" };
                    object[] valueAcc = { txtAccNo.Text.Trim() };
                    DataSet dsAcc = dbsApp.param_passing_fetch(strAcc, paramAcc, valueAcc);
                    if (dsAcc.Tables[0].Rows.Count > 0)
                    {
                        txtAccNo.Text = "";
                        string pop = "alert('Account No. Already Registered!!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        return;
                    }
                    else
                    {
                        tbFarmdetails.Visible = true;
                        trFarmdetails.Visible = false;
                        trFamilyDetails.Visible = false;
                        trBankDetails.Visible = false;
                        foreach (GridViewRow gr in GridView1.Rows)
                        {
                            int k = Convert.ToInt16(gr.RowIndex);
                            string text1_id = "text_" + k.ToString();
                            string text2_id = "text2_" + gr.RowIndex.ToString();
                            string drp_id = "dd_" + gr.RowIndex.ToString();
                            string chk_id = "CheckBox_" + gr.RowIndex.ToString();
                            CheckBox chkbox = (CheckBox)gr.Cells[4].FindControl(chk_id);

                            TextBox _familyNm = (TextBox)gr.Cells[1].FindControl(text1_id);
                            TextBox _familyAge = (TextBox)gr.Cells[3].FindControl(text2_id);
                            DropDownList _familyRelation = (DropDownList)gr.Cells[2].FindControl(drp_id);
                            string family = _familyNm.Text + ":" + _familyRelation.SelectedValue + ":" + _familyAge.Text;
                            if (Session["Family1"] == null)
                            {
                                Session["Family1"] = family;
                            }
                            else
                            {
                                Session["Family1"] = Session["Family1"].ToString() + "\n" + family;
                            }
                        }
                        tdBlock.InnerText = ddlBlock.SelectedItem.Text;
                        tdGp.InnerText = ddlGp.SelectedItem.Text;
                        tdvillage.InnerText = ddlVillage.SelectedItem.Text;
                        tdPin.InnerText = txtPinCode.Text;
                        tdHofNm.InnerText = txtHofNm.Text;
                        if (string.IsNullOrEmpty(txtDtBirth.Text))
                        {
                            tdDOB.InnerText = "*********";
                        }
                        else
                        {
                            tdDOB.InnerText = txtDtBirth.Text;
                        }
                        tdAge.InnerText = txtAge.Text;
                        tdMobileNo.InnerText = txtMobileNo.Text;
                        if (string.IsNullOrEmpty(txtAAdhar.Text))
                        {
                            tdAadherNo.InnerText = "********";
                        }
                        else
                        {
                            tdAadherNo.InnerText = txtAAdhar.Text;
                        }
                        tdRWHOF.InnerText = ddlRelation.SelectedItem.Text;
                        tdGender.InnerText = ddlGender.SelectedItem.Text;
                        tdFtherNm.InnerText = txtHusbandNm.Text;
                        tdHofNm.InnerText = txtHofNm.Text;
                        tdCategory.InnerText = ddlCategory.SelectedItem.Text;
                        tdVoterId.InnerText = txtVoterId.Text;
                        tdAccHNm.InnerText = lblAccountHolderNm.Text;
                        tdAccNo.InnerText = txtAccNo.Text;
                        tdBankNm.InnerText = ddlBankNm.SelectedItem.Text;
                        tdBranchNm.InnerText = ddlBranchNm.SelectedItem.Text;
                        tdIFSC.InnerText = txtIfscCd.Text;
                    }
                }
            }
        }
        catch (Exception)
        { }
    }
    protected void txtDtBirth_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.Now;
            int _currentYear = Convert.ToInt32(dt.Year.ToString());
            string strDob = txtDtBirth.Text;
            int _dob = Convert.ToInt32(strDob.Substring(6, 4));

            int _age = _currentYear - _dob;
            //if (_age <= 0)
            //{
            //    _age = 1;
            //}
            if (_age < 18)
            {
                string pop = "alert('Age should be more than 18 !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                txtDtBirth.Text = "";
                return;
            }

            DateTime dt1 = Convert.ToDateTime(txtDtBirth.Text);
            if (dt < dt1)
            {
                string pop = "alert('Please Select a Valid Date !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                txtDtBirth.Text = "";
                return;
            }
            txtAge.Text = _age.ToString();
        }
        catch (Exception ex)
        { }
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
    protected void btnConform_Click(object sender, EventArgs e)
    {


        var fiv = new FARMER_REGISTRATION_IV();

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
        try
        {
            string __Dob = string.Empty;
            if (string.IsNullOrEmpty(txtDtBirth.Text))
            {
                DateTime dt = DateTime.Now;
                int _currentYear = Convert.ToInt32(dt.Year.ToString());
                int _currentAge = Convert.ToInt32(txtAge.Text);
                int _DOBAge = _currentYear - _currentAge;
                __Dob = "01/04/" + _DOBAge;
            }
            else
            {
                __Dob = txtDtBirth.Text;
            }
            if (Regex.IsMatch(txtAAdhar.Text.Trim(), @"^\d+$") && txtAAdhar.Text.Trim().Length == 12 && txtAAdhar.Text != "" && txtAAdhar.Text != null)
            {
                
                    fiv.IV = Aes256CbcEncryptDecrypt.IV();
                    fiv.VCHAADHARNO_H = Aes256CbcEncryptDecrypt.GenerateSHA512String(txtAAdhar.Text);
                
                byte[] iv = Encoding.UTF8.GetBytes(fiv.IV);
                fiv.VCHAADHARNO_E = Aes256CbcEncryptDecrypt.Encrypt(txtAAdhar.Text, key, iv);
                //fiv.HMAC = BitConverter.ToString(Aes256CbcEncryptDecrypt.HmacSHA256(Convert.ToBase64String(iv) + aadhaarNo, key)).Replace("-", "").ToLower();
                
            }
            else
            {
                string pop12 = "alert('Enter AAdhaar No. correctly !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop12", pop12, true);
                // return null;
            }

            objCmd = new SqlCommand("PDS_spInsFarmerRegData", con);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@CHFARMERTYPE", SqlDbType.Char).Value = ddlFarmerType.SelectedValue;

            objCmd.Parameters.Add("@DistCode", SqlDbType.VarChar).Value = ViewState["DistId"].ToString();
            if (this.Session["UserType"].ToString() == "AAOO")
            {
                objCmd.Parameters.Add("@AAoCode", SqlDbType.VarChar).Value = Session["UserID"].ToString().Substring(4, 6);
            }
            else if (this.Session["UserType"].ToString() == "AHOO")
            {
                objCmd.Parameters.Add("@AAoCode", SqlDbType.VarChar).Value = Session["UserID"].ToString();
            }
            objCmd.Parameters.Add("@BlockCode", SqlDbType.VarChar).Value = ddlBlock.SelectedValue;
            objCmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = ddlGp.SelectedValue;
            objCmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = ddlVillage.SelectedValue;
            objCmd.Parameters.Add("@TITLE", SqlDbType.VarChar).Value = "";

            objCmd.Parameters.Add("@FARMERNAME", SqlDbType.VarChar).Value = txtHofNm.Text;
            objCmd.Parameters.Add("@FATHERNAME", SqlDbType.VarChar).Value = txtHusbandNm.Text;
            objCmd.Parameters.Add("@RELATION", SqlDbType.Int).Value = ddlRelation.SelectedValue;
            objCmd.Parameters.Add("@GENDER", SqlDbType.Int).Value = ddlGender.SelectedValue;

            objCmd.Parameters.Add("@CATEGOGY", SqlDbType.Int).Value = ddlCategory.SelectedValue;
            objCmd.Parameters.Add("@DTMDOB", SqlDbType.VarChar).Value = __Dob;
            objCmd.Parameters.Add("@AGE", SqlDbType.Int).Value = Convert.ToInt32(txtAge.Text);
            objCmd.Parameters.Add("@VOTERIDCARDNO", SqlDbType.VarChar).Value = txtVoterId.Text;
            objCmd.Parameters.Add("@AADHARNO", SqlDbType.VarChar).Value = txtAAdhar.Text;

            objCmd.Parameters.Add("@AADHARNOIV", SqlDbType.VarChar).Value = fiv.IV;
            objCmd.Parameters.Add("@VCHAADHARNO_E", SqlDbType.VarChar).Value = fiv.VCHAADHARNO_E;
            objCmd.Parameters.Add("@VCHAADHARNO_H", SqlDbType.VarChar).Value = fiv.VCHAADHARNO_H;

            objCmd.Parameters.Add("@MOBILENO", SqlDbType.VarChar).Value = txtMobileNo.Text;
            objCmd.Parameters.Add("@FAMILYMEMBERS", SqlDbType.Int).Value = ddlNoFamily.SelectedValue;

            objCmd.Parameters.Add("@ACCHOLDERNM", SqlDbType.VarChar).Value = lblAccountHolderNm.Text;
            objCmd.Parameters.Add("@ACCOUNTNO", SqlDbType.VarChar).Value = txtAccNo.Text;
            objCmd.Parameters.Add("@BANKID", SqlDbType.VarChar).Value = ddlBankNm.SelectedValue;
            objCmd.Parameters.Add("@BRANCHID", SqlDbType.VarChar).Value = ddlBranchNm.SelectedValue;
            objCmd.Parameters.Add("@IFSCCODE", SqlDbType.VarChar).Value = txtIfscCd.Text;
            objCmd.Parameters.Add("@PINCODE", SqlDbType.VarChar).Value = txtPinCode.Text;

            SqlParameter pm = new SqlParameter("@msg", SqlDbType.VarChar, 500);
            pm.Direction = ParameterDirection.Output;
            objCmd.Parameters.Add(pm);

            con.Open();
            objCmd.ExecuteNonQuery();
            string Id = objCmd.Parameters["@msg"].Value.ToString();
            ViewState["FaramId"] = Id;
            //con.Close();
            if (Session["Family1"] != null)
            {
                string details = Session["Family1"].ToString();
                string[] array = details.Split('\n');
                for (int k = 0; k < array.Length; k++)
                {
                    string[] subarray = array[k].Split(':');
                    objCmd = new SqlCommand("PDS_spInsFamilyDetails", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@VCHFARMERCODE", SqlDbType.VarChar).Value = Id;
                    objCmd.Parameters.Add("@VCHNAME", SqlDbType.VarChar).Value = subarray[0];
                    objCmd.Parameters.Add("@INTRELATION", SqlDbType.Int).Value = subarray[1];
                    objCmd.Parameters.Add("@INTAGE", SqlDbType.Int).Value = Convert.ToInt32(subarray[2]);
                    objCmd.Parameters.Add("@INTBANKACCSTATUS", SqlDbType.Int).Value = 0;
                    objCmd.ExecuteNonQuery();
                }
                con.Close();
                if (Id != "Farmer Id Already Exist !!!")
                {
                    dbsApp.SendSMS("91" + txtMobileNo.Text.Trim(), "Dear " + txtHofNm.Text.Trim() + ", you have successfully registered in seed dbt with reg no:- " + ViewState["FaramId"].ToString());
                    Session["Family1"] = null;
                    Session.Remove("Family1");
                    clearText();
                    string pop = "alert('Data Save Successfully !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);

                }
                else
                {
                    string pop1 = "alert('Data Already Exist !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop1, true);
                }
                //dbsApp.SendSMS("91" + txtMobileNo.Text.Trim(), "Dear " + txtHofNm.Text.Trim() + ", you have successfully registered in seed dbt with reg no:- " + ViewState["FaramId"].ToString());
                //Session["Family1"] = null;
                //Session.Remove("Family1");
                //clearText();
                //string pop = "alert('Data Save Successfully !!!');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
            else
            {
                con.Close();
                string strDel = "delete from M_FARMER_REGISTRATION where VCHFARMERCODE=@FarmId ";
                string strFamily = "delete from T_FARMER_FAMILYDETAILS where VCHFARMERCODE=@FarmId ";
                string[] paramDel = { "@FarmId" };
                object[] valueDel = { ViewState["FaramId"].ToString() };
                dbsApp.param_passing_fetch(strDel, paramDel, valueDel);
                dbsApp.param_passing_fetch(strFamily, paramDel, valueDel);

                Session["Family1"] = null;
                Session.Remove("Family1");
                tbFarmdetails.Visible = false;
                trFarmdetails.Visible = true;
                trFamilyDetails.Visible = true;
                trBankDetails.Visible = true;
            }
        }
        catch (Exception ex)
        {
            string strDel = "delete from M_FARMER_REGISTRATION where VCHFARMERCODE=@FarmId ";
            string strFamily = "delete from T_FARMER_FAMILYDETAILS where VCHFARMERCODE=@FarmId ";
            string[] paramDel = { "@FarmId" };
            object[] valueDel = { ViewState["FaramId"].ToString() };
            dbsApp.param_passing_fetch(strDel, paramDel, valueDel);
            dbsApp.param_passing_fetch(strFamily, paramDel, valueDel);

            Session["Family1"] = null;
            Session.Remove("Family1");
            tbFarmdetails.Visible = false;
            trFarmdetails.Visible = true;
            trFamilyDetails.Visible = true;
            trBankDetails.Visible = true;
        }
        finally
        {

            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            tbFarmdetails.Visible = false;
            trFarmdetails.Visible = true;
            trFamilyDetails.Visible = true;
            trBankDetails.Visible = true;
        }
        catch (Exception ex)
        { }
    }
    protected void clearText()
    {
        ddlBlock.SelectedIndex = 0;
        ddlGp.Items.Clear();
        ddlVillage.Items.Clear();
        txtHofNm.Text = "";
        txtPinCode.Text = "";
        ddlCategory.SelectedIndex = 0;
        txtDtBirth.Text = "";
        txtAge.Text = "";
        ddlRelation.SelectedIndex = 0;
        txtHusbandNm.Text = "";
        txtVoterId.Text = "";
        txtAAdhar.Text = "";
        txtMobileNo.Text = "";
        lblAccountHolderNm.Text = "";
        txtAccNo.Text = "";
        ddlBankNm.SelectedIndex = 0;
        ddlBranchNm.Items.Clear();
        txtIfscCd.Text = "";
        ddlNoFamily.SelectedIndex = 0;
        ddlNoFamily_SelectedIndexChanged(null, null);
        txtHofNm_TextChanged(null, null);

        tbFarmdetails.Visible = false;
        trFarmdetails.Visible = true;
        trFamilyDetails.Visible = true;
        trBankDetails.Visible = true;
    }

    protected void txtVoterId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string strChk = "select VCHVOTERIDCARDNO,VCHAADHARNO,VCHACCOUNTNO from M_FARMER_REGISTRATION where "
                      + " REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(VCHVOTERIDCARDNO,'/',''),' ',''),'-',''),'\',''),'.','')=REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@VoterId,'/',''),' ',''),'-',''),'.',''),'\','') and INTDELETEDFLAG=0 ";
            string[] paramChk = { "@VoterId" };
            object[] valueChk = { txtVoterId.Text.Trim() };
            DataSet dsChk = dbsApp.param_passing_fetch(strChk, paramChk, valueChk);
            if (dsChk.Tables[0].Rows.Count > 0)
            {
                txtVoterId.Text = "";
                string pop = "alert('Voter Id Already Registered!!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string strAcc = "select VCHVOTERIDCARDNO,VCHAADHARNO,VCHACCOUNTNO from M_FARMER_REGISTRATION where "
                 + " REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(VCHACCOUNTNO,'/',''),' ',''),'-',''),'\',''),'.','')=REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@AccNo,'/',''),' ',''),'-',''),'.',''),'\','') and INTDELETEDFLAG=0 ";
            string[] paramAcc = { "@AccNo" };
            object[] valueAcc = { txtAccNo.Text.Trim() };
            DataSet dsAcc = dbsApp.param_passing_fetch(strAcc, paramAcc, valueAcc);
            if (dsAcc.Tables[0].Rows.Count > 0)
            {
                txtAccNo.Text = "";
                string pop = "alert('Account No. Already Registered!!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
        }
        catch (Exception EX)
        { }
    }
    protected void txtAAdhar_TextChanged(object sender, EventArgs e)
    {
        if (txtAAdhar.Text.Trim() != "")
        {
            string strAcc = "select VCHAADHARNO from M_FARMER_REGISTRATION where VCHAADHARNO=@AADHARNO and INTDELETEDFLAG=0 and INTREJECTED=0";
            string[] paramAcc = { "@AADHARNO" };
            object[] valueAcc = { txtAAdhar.Text.Trim() };
            DataSet dsAcc = dbsApp.param_passing_fetch(strAcc, paramAcc, valueAcc);
            if (dsAcc.Tables[0].Rows.Count > 0)
            {
                txtAAdhar.Text = "";
                string pop = "alert('Aadhar No. Already Registered!!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
        }
    }
    protected void txtMobileNo_TextChanged(object sender, EventArgs e)
    {
        if (txtMobileNo.Text.Trim() != "")
        {
            string strAcc = "select VCHMOBILENO from M_FARMER_REGISTRATION where VCHMOBILENO=@MobileNo and INTDELETEDFLAG=0 and INTREJECTED=0";
            string[] paramAcc = { "@MobileNo" };
            object[] valueAcc = { txtMobileNo.Text.Trim() };
            DataSet dsAcc = dbsApp.param_passing_fetch(strAcc, paramAcc, valueAcc);
            if (dsAcc.Tables[0].Rows.Count > 0)
            {
                txtMobileNo.Text = "";
                string pop = "alert('Mobile No. Already Registered!!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
        }
    }
}


public partial class FARMER_REGISTRATION_IV
{
    public string IV { get; set; }
    public string VCHAADHARNO_E { get; set; }
    public string VCHAADHARNO_H { get; set; }
}

public class Aes256CbcEncryptDecrypt
{
    private static readonly Encoding encoding = Encoding.UTF8;

    internal static string Encrypt(string plainText, string key, byte[] iv)
    {
        try
        {
            var aes = new AesCryptoServiceProvider
            {
                KeySize = 256,
                BlockSize = 128,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                Key = encoding.GetBytes(key)
            };
            //aes.GenerateIV();
            ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, iv);
            byte[] buffer = encoding.GetBytes(plainText);
            string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            return encryptedText;
            //var keyValues = new Dictionary<string, object>
            //{
            //    { "iv", Convert.ToBase64String(iv) },
            //    { "value", encryptedText },
            //    { "mac", mac }
            //};
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //return Convert.ToBase64String(encoding.GetBytes(serializer.Serialize(keyValues)));
        }
        catch (Exception e)
        {
            throw new Exception("Error encrypting: " + e.Message);
        }
    }

    internal static string Decrypt(string encryptedText, string key, byte[] iv)
    {
        try
        {
            var aes = new AesCryptoServiceProvider
            {
                KeySize = 256,
                BlockSize = 128,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                Key = encoding.GetBytes(key)
            };
            // Base 64 decode
            //byte[] base64Decoded = Convert.FromBase64String(encryptedText);
            //string base64DecodedStr = encoding.GetString(base64Decoded);
            // JSON Decode base64Str
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //var payload = ser.Deserialize<Dictionary<string, string>>(base64DecodedStr);
            //aes.IV = Convert.FromBase64String(payload["iv"]);
            ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, iv);
            //byte[] buffer = Convert.FromBase64String(payload["value"]);
            byte[] buffer = Convert.FromBase64String(encryptedText);
            return encoding.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }
        catch (Exception e)
        {
            throw new Exception("Error decrypting: " + e.Message);
        }
    }

    private static string ByteArrayToHexString(byte[] ba)
    {
        var hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }

    internal static string GenerateSHA512String(string inputString)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        //SHA512Managed sha512hasstring = new SHA512Managed();
        var sha512hasstring = new SHA512CryptoServiceProvider();
        byte[] hash = sha512hasstring.ComputeHash(bytes);
        return ByteArrayToHexString(hash);
    }

    //internal static byte[] HmacSHA256(string data, string key)
    //{
    //    using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(key)))
    //    {
    //        return hmac.ComputeHash(encoding.GetBytes(data));
    //    }
    //}

    private static Random random = new Random();

    internal static string IV()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789/-_@#!$%&";
        return new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;


public partial class Masters_FarmerDetailsUpdate : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();


    SqlConnection objConn = new SqlConnection();
    SqlCommand objCmd = new SqlCommand();


    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["FarmerDBConnectionString"].ConnectionString;
    private UtilityLibrary utl = new UtilityLibrary();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    fillBankNm();
                    divDetails.Visible = false;
                    divfarmDetails.Visible = false;
                    trGndApicol.Visible = false;
                    trGndFmn.Visible = false;
                    trCatApicol.Visible = false;
                    trCatFmn.Visible = false;
                    trCatJAL.Visible = false;
                    trCatSolar.Visible = false;
                    trBankApicol.Visible = false;
                    trBankFmn.Visible = false;
                    trBankJal.Visible = false;
                    trBankSolar.Visible = false;

                    //trBankDetails.Visible = false;

                    lblAccNo.Visible = false;
                    lblBankNm.Visible = false;
                    lblBranchNM.Visible = false;
                    lblfsc.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {    }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            string str1 = "select b.NIC_BlockCode,int_BlockID from M_FARMER_REGISTRATION as a inner join PDS_BLOCKMASTER as b on a.VCHBLOCKID=b.int_BlockID where NICFARMERID=@FarmerId";
            string[] paramDist1 = { "@FarmerId" };
            object[] valueDist1 = { txtfarmerid.Text };
            DataSet dsFarmDetails1 = dbsFarmer.param_passing_fetchDataSet(str1, paramDist1, valueDist1);
            if (dsFarmDetails1.Tables[0].Rows.Count > 0)
            {
                if (dsFarmDetails1.Tables[0].Rows[0]["NIC_BlockCode"].ToString() == Session["BlockCode"].ToString())
                {
                    string str = "EXEC sp_SelectFarmerDetails @FarmerId";
                    string[] paramDist = { "@FarmerId" };
                    object[] valueDist = { txtfarmerid.Text };
                    DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str, paramDist, valueDist);
                    if (dsFarmDetails.Tables[0].Rows.Count > 0)
                    {
                        divDetails.Visible = true;
                        divfarmDetails.Visible = true;
                        ViewState["VCHBLOCKID"] = dsFarmDetails.Tables[0].Rows[0]["VCHBLOCKID"].ToString();
                        ViewState["VCHFARMERCODE"] = dsFarmDetails.Tables[0].Rows[0]["VCHFARMERCODE"].ToString();
                        tdFarmerName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                        tdFatherName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                        tdDistrict.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_DistrictName"].ToString();
                        tdBlock.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_BlockName"].ToString();
                        tdGp.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_GPName"].ToString();
                        tdVillage.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_VillageName"].ToString();
                        tdGender.InnerText = dsFarmDetails.Tables[0].Rows[0]["Gender"].ToString();
                        tdCategory.InnerText = dsFarmDetails.Tables[0].Rows[0]["Category_Value"].ToString();
                        tdEFarmName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                        tdEFatherName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                        tdEGP.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_GPName"].ToString();
                        tdEVillage.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_VillageName"].ToString();
                        tdEGender.InnerText = dsFarmDetails.Tables[0].Rows[0]["Gender"].ToString();
                        tdEMobileNo.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHMOBILENO"].ToString();
                        tdECategory.InnerText = dsFarmDetails.Tables[0].Rows[0]["Category_Value"].ToString();
                        tdAadhaarNo0.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHAADHARNO"].ToString();



                        tdEAccHolderName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
                        lblAccHolderName.Text = dsFarmDetails.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
                        //tdUpAccHolderNm.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();txtAccHolderName
                        tdEAccNo.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHACCOUNTNO"].ToString();
                        tdEBranchName.InnerText = dsFarmDetails.Tables[0].Rows[0]["Branch_Name"].ToString();
                        tdEBrankName.InnerText = dsFarmDetails.Tables[0].Rows[0]["Bank_Name"].ToString();
                        tdEIfsc.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHIFSCCODE"].ToString();
                        //lblAccHolderName.Text = dsFarmDetails.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
                        lblAccNo.Text = dsFarmDetails.Tables[0].Rows[0]["VCHACCOUNTNO"].ToString();
                        lblBankNm.Text = dsFarmDetails.Tables[0].Rows[0]["Bank_Name"].ToString();
                        lblBranchNM.Text = dsFarmDetails.Tables[0].Rows[0]["Branch_Name"].ToString();
                        lblfsc.Text = dsFarmDetails.Tables[0].Rows[0]["VCHIFSCCODE"].ToString();

                        fillGp();


                        bool Status = GetSolarPump(txtfarmerid.Text);
                        if (Status == true)
                        {
                            trCatSolar.Visible = true;
                            trBankSolar.Visible = true;
                        }
                        else
                        {
                            trCatSolar.Visible = false;
                            trBankSolar.Visible = false;
                        }
                        bool Status1 = GetFMN(txtfarmerid.Text);
                        if (Status1 == true)
                        {
                            trCatFmn.Visible = true;
                            trBankFmn.Visible = true;
                            //trGndFmn.Visible = true;
                        }
                        else
                        {
                            trCatFmn.Visible = false;
                            trBankFmn.Visible = false;
                            //trGndFmn.Visible = false;
                        }

                        bool Status11 = GetFMNGnd(txtfarmerid.Text);
                        if (Status11 == true)
                        {
                            trGndFmn.Visible = true;
                        }
                        else
                        {
                            trGndFmn.Visible = false;
                        }

                        bool statusJal = GetJAL(txtfarmerid.Text);
                        if (statusJal == true)
                        {
                            trCatJAL.Visible = true;
                            trBankJal.Visible = true;
                        }
                        else
                        {
                            trCatJAL.Visible = false;
                            trBankJal.Visible = false;
                        }
                        bool statusApicol = GetApicol(txtfarmerid.Text);
                        if (statusApicol == true)
                        {
                            trCatApicol.Visible = true;
                            trBankApicol.Visible = true;
                            trGndApicol.Visible = true;
                        }
                        else
                        {
                            trCatApicol.Visible = false;
                            trBankApicol.Visible = false;
                            trGndApicol.Visible = false;
                        }
                        int StatusF = GetSubsidyFalureCase(txtfarmerid.Text);
                        if (StatusF == 1)
                        {
                            trTrnFalure.Visible = true;
                            trBankFmn.Visible = false;
                            trBankApicol.Visible = false;
                            trBankJal.Visible = false;
                            trBankSolar.Visible = false;
                        }
                        else
                        {
                            trTrnFalure.Visible = false;

                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Record Not Found')</script>");

                        divDetails.Visible = false;
                        divfarmDetails.Visible = false;
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Record Not Found')</script>");
                    divDetails.Visible = false;
                    divfarmDetails.Visible = false;
                    return;
                }
            }
            else
            {
                Response.Write("<script>alert('Record Not Found')</script>");
                divDetails.Visible = false;
                divfarmDetails.Visible = false;
                return;
            }

        }
        catch (Exception ex)
        { }
    }
    protected void fillBankNm()
    {
        DataSet dsBnk = dbsApp.returnDataset("SELECT INTID,VCHBANKNAME FROM M_PDS_FARMERBANK WHERE INTDELETEDFLAG=0 ORDER BY VCHBANKNAME");
        ddlBankNm.DataSource = dsBnk;
        ddlBankNm.DataTextField = "VCHBANKNAME";
        ddlBankNm.DataValueField = "INTID";
        ddlBankNm.DataBind();
        ddlBankNm.Items.Insert(0, "--select--");
    }
    protected void ddlBankNm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strBnk = "SELECT INTBRANCHID,(VCHBRANCHNAME+','+VCHDISTRICT +' (' + isnull(NEW_IFSC_CODE,'-')  +')') VCHBRANCHNAME  FROM M_PDS_BANKBRANCH WHERE INTBANKID=@BnkCode AND INTDELETEDFLAG=0 AND NEW_IFSC_CODE IS NOT NULL AND NEW_IFSC_CODE<>'' ORDER BY VCHBRANCHNAME";
            string[] paramBnk = { "@BnkCode" };
            object[] valueBnk = { ddlBankNm.SelectedValue };
            DataSet dsBnkNm = dbsFarmer.param_passing_fetchDataSet(strBnk, paramBnk, valueBnk);
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
    protected void ddlBranchNm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = "select NEW_IFSC_CODE from M_PDS_BANKBRANCH where intBranchId=@BnchId";
            string[] strParam = { "@BnchId" };
            object[] Value = { ddlBranchNm.SelectedValue };
            DataSet dsIfcs = dbsFarmer.param_passing_fetchDataSet(str, strParam, Value);
            if (dsIfcs.Tables[0].Rows.Count > 0)
            {
                txtIfscCd.Text = dsIfcs.Tables[0].Rows[0]["NEW_IFSC_CODE"].ToString();
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
            DataSet dsAcc = dbsFarmer.param_passing_fetchDataSet(strAcc, paramAcc, valueAcc);
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
    protected void fillGp()
    {
        string gp = "Select distinct int_GPID,Upper(vch_GPName)vch_GPName From PDS_GPMaster Where int_BlockID=@GpCode Order By vch_GPName";
        string[] paramGp = { "@GpCode" };
        object[] valueGp = { ViewState["VCHBLOCKID"].ToString() };
        DataSet dsGp = dbsFarmer.param_passing_fetchDataSet(gp, paramGp, valueGp);
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
        DataSet dsVllg = dbsFarmer.param_passing_fetchDataSet(village, paramVllg, valueVllg);
        if (dsVllg.Tables[0].Rows.Count > 0)
        {
            ddlVillage.DataSource = dsVllg;
            ddlVillage.DataTextField = "vch_VillageName";
            ddlVillage.DataValueField = "int_VillageID";
            ddlVillage.DataBind();
            ddlVillage.Items.Insert(0, "--select--");
        }
    }
    protected void btnNameSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strNm = "select * from FARMER_UPDATES where ChangeType='NM' and AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId";
            string[] paramNm = { "@FarmerId" };
            object[] valueNm = { txtfarmerid.Text };
            DataSet dsNm = dbsFarmer.param_passing_fetchDataSet(strNm, paramNm, valueNm);
            if (dsNm.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Previous request already pending')</script>");
                return;
            }

            if (string.IsNullOrEmpty(txtEFarmerName.Text))
            {
                Response.Write("<script>alert('Please Enter Farmer Name')</script>");
                return;
            }
            else if (string.IsNullOrEmpty(txtEFatherName.Text))
            {
                Response.Write("<script>alert('Please Enter Father/Husband Name')</script>");
                return;
            }
            else
            {
                #region

                string strFileName = string.Empty;
                string strFileExtension = string.Empty;
                strFileName = Path.GetFileName(updName.PostedFile.FileName);
                strFileExtension = Path.GetExtension(updName.PostedFile.FileName);
                if (strFileName == "")
                {
                    string pop = "alert('Please upload your scanned Aadhaar/Voter Id');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }

                string[] count_null = updName.PostedFile.FileName.Split('%');
                string[] count_null_ext = updName.PostedFile.FileName.Split('.');

                if (count_null_ext.Length > 2)
                {
                    string pop = "alert('Upload file with proper extension!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                if (count_null.Length > 1)
                {
                    string pop = "alert('Null Bytes are not allowed!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                int intlength = updName.PostedFile.ContentLength;

                /////AAdhaar Certificate
                Byte[] byteData = new Byte[intlength];
                updName.PostedFile.InputStream.Read(byteData, 0, intlength);
                Session["byteData"] = byteData;

                string cont = updName.PostedFile.ContentType;
                dbsApp.FileType f = dbsApp.FileType.PDF;
                if (dbsApp.isValidFile(byteData, f, cont) == true)
                {
                    if (intlength / 1024 > 151)
                    {
                        string pop1 = "alert('File size should not be more than 150 KB !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                    }

                #endregion
                    SqlConnection con = new SqlConnection(_connstrStock);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                    cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = txtEFarmerName.Text;
                    cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = txtEFatherName.Text;//Session["UserID"]
                    cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                    cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    objCmd = new SqlCommand("SP_FarmerCertUpdate", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    objCmd.Parameters.Add("@CertificateName", SqlDbType.VarChar).Value = strFileName;
                    objCmd.Parameters.Add("@Certificate", SqlDbType.VarBinary).Value = (Byte[])Session["byteData"];
                    objCmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 0;
                    objCmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Successfully Forward To CDAO For Confirmation')</script>");

                    txtEFarmerName.Text = "";
                    txtEFatherName.Text = "";
                }
                else
                {
                    Response.Write("<script>alert('Please upload only .PDF file')</script>");
                }
            }
    }
        catch (Exception ex)
        {

        }
    }
    protected void btnGp_Click(object sender, EventArgs e)
    {
        try
        {
            string strNm = "select * from FARMER_UPDATES where ChangeType='GP' and AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId";
            string[] paramNm = { "@FarmerId" };
            object[] valueNm = { txtfarmerid.Text };
            DataSet dsNm = dbsFarmer.param_passing_fetchDataSet(strNm, paramNm, valueNm);
            if (dsNm.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Previous request already pending')</script>");
                return;
            }
            if (ddlGp.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please Select GP Name')</script>");
                return;
            }
            else if (ddlVillage.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please Select Village Name')</script>");
                return;
            }
            else
            {
                #region
                string strFileName = string.Empty;
                string strFileExtension = string.Empty;

                strFileName = Path.GetFileName(UpdVillage.PostedFile.FileName);
                strFileExtension = Path.GetExtension(UpdVillage.PostedFile.FileName);
               
                if (strFileName == "")
                {
                    string pop = "alert('Please upload your scanned AAdhaar/Voter Id');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }
                string[] count_null = UpdVillage.PostedFile.FileName.Split('%');
                string[] count_null_ext = UpdVillage.PostedFile.FileName.Split('.');
               

                if (count_null_ext.Length > 2)
                {
                    string pop = "alert('Upload file with proper extension!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                if (count_null.Length > 1)
                {
                    string pop = "alert('Null Bytes are not allowed!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }

                int intlength = UpdVillage.PostedFile.ContentLength;

                /////GP Certificate
                Byte[] byteData = new Byte[intlength];
                UpdVillage.PostedFile.InputStream.Read(byteData, 0, intlength);
                Session["byteData"] = byteData;

                string cont = UpdVillage.PostedFile.ContentType;
                dbsApp.FileType f = dbsApp.FileType.PDF;
                if (dbsApp.isValidFile(byteData, f, cont) == true)
                {
                    
                   if (intlength / 1024 > 151)
                    {
                        string pop1 = "alert('File size should not be more than 150 KB !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                    }
                #endregion
                    SqlConnection con = new SqlConnection(_connstrStock);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                    cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = ddlGp.SelectedValue;
                    cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = ddlVillage.SelectedValue;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                    cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    objCmd = new SqlCommand("SP_FarmerCertUpdate", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    objCmd.Parameters.Add("@CertificateName", SqlDbType.VarChar).Value = strFileName;
                    objCmd.Parameters.Add("@Certificate", SqlDbType.VarBinary).Value = (Byte[])Session["byteData"];
                    objCmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 1;
                    objCmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Successfully Forward To CDAO For Confirmation')</script>");
                    ddlVillage.Items.Clear();
                    ddlGp.SelectedIndex = 1;
                }
                else
                {
                    Response.Write("<script>alert('Please upload only .PDF file')</script>");
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnGender_Click(object sender, EventArgs e)
    {
        try
        {
            string strNm = "select * from FARMER_UPDATES where ChangeType='GND' and AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId";
            string[] paramNm = { "@FarmerId" };
            object[] valueNm = { txtfarmerid.Text };
            DataSet dsNm = dbsFarmer.param_passing_fetchDataSet(strNm, paramNm, valueNm);
            if (dsNm.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Previous request already pending')</script>");
                return;
            }
            bool Status1 = GetFMNGnd(txtfarmerid.Text);
            if (Status1 == true)
            {
                Response.Write("<script>alert('Can`t be changed due to farm mechinary subsidy availed')</script>");
                return;
            }
            bool statusApicol = GetApicol(txtfarmerid.Text);
            if (statusApicol == true)
            {
                Response.Write("<script>alert('Can`t be changed due to apicol subsidy availed')</script>");
                return;
            }
            else
            {
                #region

                string strFileName = string.Empty;
                string strFileExtension = string.Empty;
                strFileName = Path.GetFileName(updGender.PostedFile.FileName);
                strFileExtension = Path.GetExtension(updGender.PostedFile.FileName);
                if (strFileName == "")
                {
                    string pop = "alert('Please upload your scanned Aadhaar/Voter Id');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }

                string[] count_null = updGender.PostedFile.FileName.Split('%');
                string[] count_null_ext = updGender.PostedFile.FileName.Split('.');

                if (count_null_ext.Length > 2)
                {
                    string pop = "alert('Upload file with proper extension!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                if (count_null.Length > 1)
                {
                    string pop = "alert('Null Bytes are not allowed!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                

                int intlength = updGender.PostedFile.ContentLength;

                /////Gender Certificate
                Byte[] byteData = new Byte[intlength];
                updGender.PostedFile.InputStream.Read(byteData, 0, intlength);
                Session["byteData"] = byteData;

                string cont = updGender.PostedFile.ContentType;
                dbsApp.FileType f = dbsApp.FileType.PDF;
                if (dbsApp.isValidFile(byteData, f, cont) == true)
                {
                    
                     if (intlength / 1024 > 151)
                    {
                        string pop1 = "alert('File size should not be more than 150 KB !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                    }
                #endregion
                    SqlConnection con = new SqlConnection(_connstrStock);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                    cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = ddlGender.SelectedValue;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                    cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    objCmd = new SqlCommand("SP_FarmerCertUpdate", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    objCmd.Parameters.Add("@CertificateName", SqlDbType.VarChar).Value = strFileName;
                    objCmd.Parameters.Add("@Certificate", SqlDbType.VarBinary).Value = (Byte[])Session["byteData"];
                    objCmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 2;
                    objCmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Successfully Forward To CDAO For Confirmation')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please upload only .PDF file')</script>");
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnMobileNo_Click(object sender, EventArgs e)
    {
        try
        {
            string strNm = "select * from FARMER_UPDATES where ChangeType='MOB' and AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId";
            string[] paramNm = { "@FarmerId" };
            object[] valueNm = { txtfarmerid.Text };
            DataSet dsNm = dbsFarmer.param_passing_fetchDataSet(strNm, paramNm, valueNm);
            if (dsNm.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Previous request already pending')</script>");
                return;
            }
            if (txtMobileNo.Text.Length == 10)
            {
                string str1 = "select * from M_FARMER_REGISTRATION as a inner join Tbl_FarmerApprove as b on a.VCHFARMERCODE=b.Farmer_Code where  VCHMOBILENO=@MobileNo and b.Status in('ACCP','CCB')";
                string[] paramDist1 = { "@MobileNo" };
                object[] valueDist1 = { txtMobileNo.Text };
                DataSet dsFarmDetails1 = dbsFarmer.param_passing_fetchDataSet(str1, paramDist1, valueDist1);
                if (dsFarmDetails1.Tables[0].Rows.Count > 0)
                {
                    Response.Write("<script>alert('Duplicate Mobile Number')</script>");
                    return;
                }
                else
                {
                    string str = "SELECT FarmerId FROM  mobileupdate where FarmerId= @FarmerId union select FarmerId from FARMER_UPDATES_LOG where farmerid=@FarmerId and changetype='MOB'";
                    string[] paramDist = { "@FarmerId" };
                    object[] valueDist = { txtfarmerid.Text };
                    DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str, paramDist, valueDist);
                    if (dsFarmDetails.Tables[0].Rows.Count > 0)
                    {
                        string str11 = " select FarmerId from FARMER_UPDATES_LOG where farmerid=@FarmerId and changetype='MOB'";
                        string[] paramDist11 = { "@FarmerId" };
                        object[] valueDist11 = { txtfarmerid.Text };
                        DataSet dsFarmDetails11 = dbsFarmer.param_passing_fetchDataSet(str11, paramDist11, valueDist11);
                        if (dsFarmDetails11.Tables[0].Rows.Count > 1)
                        {
                            Response.Write("<script>alert('Can`t change due to more than 2 changes updatted')</script>");
                            return;
                        }
                        else
                        {
                            SqlConnection con = new SqlConnection(_connstrStock);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                            cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                            cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = txtMobileNo.Text;
                            cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                            cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = 3;
                            cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            con.Close();
                            Response.Write("<script>alert('Successfully Forward To CDAO For Confirmation')</script>");
                        }
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(_connstrStock);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdate", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                        cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                        cmd.Parameters.Add("@MobileNo1 ", SqlDbType.VarChar).Value = txtMobileNo.Text;
                        cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = tdEMobileNo.InnerText;
                        cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Close();
                        Response.Write("<script>alert('Mobile No Updated Successfully')</script>");

                    }
                    txtMobileNo.Text = "";
                }
            }
            else
            {
                Response.Write("<script>alert('Please enter valid mobile no')</script>");
                return;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnAadhaar_Click(object sender, EventArgs e)
    {
        try
        {
            string strNm = "select * from FARMER_UPDATES where ChangeType='AADH' and AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId";
            string[] paramNm = { "@FarmerId" };
            object[] valueNm = { txtfarmerid.Text };
            DataSet dsNm = dbsFarmer.param_passing_fetchDataSet(strNm, paramNm, valueNm);
            if (dsNm.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Previous request already pending')</script>");
                return;
            }
            if (txtAadhaar.Text.Length != 12 || string.IsNullOrEmpty(txtAadhaar.Text))
            {


                Response.Write("<script>alert('Please Enter Valid AADHAAR')</script>");
                return;
            }
            else
            {
                string str1 = "select * from M_FARMER_REGISTRATION as a inner join Tbl_FarmerApprove as b on a.VCHFARMERCODE=b.Farmer_Code where  VCHAADHARNO=@Aadhaar and b.Status in('ACCP','CCB') ";
                string[] paramDist1 = { "@Aadhaar" };
                object[] valueDist1 = { txtAadhaar.Text };
                DataSet dsFarmDetails1 = dbsFarmer.param_passing_fetchDataSet(str1, paramDist1, valueDist1);
                if (dsFarmDetails1.Tables[0].Rows.Count > 0)
                {
                    Response.Write("<script>alert('Duplicate AADHAAR Number')</script>");
                    return;
                }
                else
                {
                    #region
                    string strFileName = string.Empty;
                    string strFileExtension = string.Empty;
                    strFileName = Path.GetFileName(UpdaAadhar.PostedFile.FileName);
                    strFileExtension = Path.GetExtension(UpdaAadhar.PostedFile.FileName);
                    if (strFileName == "")
                    {
                        string pop = "alert('Please upload your scanned AADHAAR');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }

                    string[] count_null = UpdaAadhar.PostedFile.FileName.Split('%');
                    string[] count_null_ext = UpdaAadhar.PostedFile.FileName.Split('.');

                    if (count_null_ext.Length > 2)
                    {
                        string pop = "alert('Upload file with proper extension!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        return;
                    }
                    if (count_null.Length > 1)
                    {
                        string pop = "alert('Null Bytes are not allowed!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        return;
                    }

                    

                    int intlength = UpdaAadhar.PostedFile.ContentLength;

                    /////EPF Certificate
                    Byte[] byteData = new Byte[intlength];
                    UpdaAadhar.PostedFile.InputStream.Read(byteData, 0, intlength);
                    Session["byteData"] = byteData;

                    string cont = UpdaAadhar.PostedFile.ContentType;
                    dbsApp.FileType f = dbsApp.FileType.PDF;
                    if (dbsApp.isValidFile(byteData, f, cont) == true)
                    {
                       
                        if (intlength / 1024 > 151)
                        {
                            string pop1 = "alert('File size should not be more than 150 KB !!!');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                        }
                    #endregion
                        SqlConnection con = new SqlConnection(_connstrStock);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                        cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                        cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = txtAadhaar.Text;
                        cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = "";


                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                        cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = 4;
                        cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        objCmd = new SqlCommand("SP_FarmerCertUpdate", con);
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                        objCmd.Parameters.Add("@CertificateName", SqlDbType.VarChar).Value = strFileName;
                        objCmd.Parameters.Add("@Certificate", SqlDbType.VarBinary).Value = (Byte[])Session["byteData"];
                        objCmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 4;
                        objCmd.ExecuteNonQuery();
                        con.Close();
                        Response.Write("<script>alert('Successfully forward to CDAO for confirmation')</script>");
                        txtAadhaar.Text = "";
                    }
                    else
                    {
                        Response.Write("<script>alert('Please upload only .PDF file')</script>");
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnCategory_Click(object sender, EventArgs e)
    {
        try
        {
            string strNm = "select * from FARMER_UPDATES where ChangeType='CATGRY' and AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId";
            string[] paramNm = { "@FarmerId" };
            object[] valueNm = { txtfarmerid.Text };
            DataSet dsNm = dbsFarmer.param_passing_fetchDataSet(strNm, paramNm, valueNm);
            if (dsNm.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Previous request already pending')</script>");
                return;
            }
            bool Status = GetSolarPump(txtfarmerid.Text);
            if (Status == true)
            {
                Response.Write("<script>alert('Can`t be changed due to solar pump subsidy availed')</script>");
                return;
            }
            bool Status1 = GetFMN(txtfarmerid.Text);
            if (Status1 == true)
            {
                Response.Write("<script>alert('Can`t be changed due to farm mechinary subsidy availed')</script>");
                return;
            }
            bool statusJal = GetJAL(txtfarmerid.Text);
            if (statusJal == true)
            {
                Response.Write("<script>alert('Can`t be changed due to jalanidhi subsidy availed')</script>");
                return;
            }
            bool statusApicol = GetApicol(txtfarmerid.Text);
            if (statusApicol == true)
            {
                Response.Write("<script>alert('Can`t be changed due to apicol subsidy availed')</script>");
                return;
            }
            else
            {
                if (ddlCategory.SelectedIndex == 0)
                {
                    Response.Write("<script>alert('Please Select Category')</script>");
                    return;
                }
                #region

                string strFileName = string.Empty;
                string strFileExtension = string.Empty;
                strFileName = Path.GetFileName(file_EPF.PostedFile.FileName);
                strFileExtension = Path.GetExtension(file_EPF.PostedFile.FileName);
                if (strFileName == "")
                {
                    string pop = "alert('Please upload your scanned Cast certificate');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }
                string[] count_null = file_EPF.PostedFile.FileName.Split('%');
                string[] count_null_ext = file_EPF.PostedFile.FileName.Split('.');



                if (count_null_ext.Length > 2)
                {
                    string pop = "alert('Upload file with proper extension!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }
                if (count_null.Length > 1)
                {
                    string pop = "alert('Null Bytes are not allowed!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    return;
                }

               

                int intlength = file_EPF.PostedFile.ContentLength;

                /////EPF Certificate
                Byte[] byteData = new Byte[intlength];
                file_EPF.PostedFile.InputStream.Read(byteData, 0, intlength);
                Session["byteData"] = byteData;

                string cont = file_EPF.PostedFile.ContentType;
                dbsApp.FileType f = dbsApp.FileType.PDF;
                if (dbsApp.isValidFile(byteData, f, cont) == true)
                {
                    
                     if (intlength / 1024 > 151)
                    {
                        string pop1 = "alert('File size should not be more than 150 KB !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                    }
                #endregion
                    SqlConnection con = new SqlConnection(_connstrStock);
                    con.Open();

                    SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                    cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = ddlCategory.SelectedValue;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                    cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = 5;
                    cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    objCmd = new SqlCommand("SP_FarmerCertUpdate", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    objCmd.Parameters.Add("@CertificateName", SqlDbType.VarChar).Value = strFileName;
                    objCmd.Parameters.Add("@Certificate", SqlDbType.VarBinary).Value = (Byte[])Session["byteData"];
                    objCmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 5;
                    objCmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Successfully forward to CDAO for confirmation')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please upload only .PDF file')</script>");
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnBankDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string strNm = "select * from FARMER_UPDATES where (ChangeType='BANK' OR ChangeType='BANKNM') and AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId";
            string[] paramNm = { "@FarmerId" };
            object[] valueNm = { txtfarmerid.Text };
            DataSet dsNm = dbsFarmer.param_passing_fetchDataSet(strNm, paramNm, valueNm);
            if (dsNm.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Previous request already pending')</script>");
                return;
            }

            int StatusF = GetSubsidyFalureCase(txtfarmerid.Text);
            if (StatusF == 1)
            {
                ViewState["Chk"] = 7;
                UpdBankDetails();

            }
            else
            {
                bool Status = GetSolarPump(txtfarmerid.Text);
                if (Status == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to solar pump subsidy availed')</script>");
                    return;
                }
                bool Status1 = GetFMN(txtfarmerid.Text);
                if (Status1 == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to farm mechinary subsidy availed')</script>");
                    return;
                }
                bool statusJal = GetJAL(txtfarmerid.Text);
                if (statusJal == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to jalanidhi subsidy availed')</script>");
                    return;
                }
                bool statusApicol = GetApicol(txtfarmerid.Text);
                if (statusApicol == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to apicol subsidy availed')</script>");
                    return;
                }
                else
                {
                    //if (radBank.SelectedValue == "a1")
                    //{
                    ViewState["Chk"] = 6;
                    UpdBankDetails();
                    //}

                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(txtAccHolderName.Text))
                    //    {
                    //        Response.Write("<script>alert('Please Enter Account Holder Name')</script>");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        #region
                    //        string strFileName = string.Empty;
                    //        string strFileExtension = string.Empty;

                    //        string[] count_null = updPassbook.PostedFile.FileName.Split('%');
                    //        string[] count_null_ext = updPassbook.PostedFile.FileName.Split('.');

                    //        if (count_null_ext.Length > 2)
                    //        {
                    //            string pop = "alert('Upload file with proper extension!!');";
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    //            return;
                    //        }
                    //        if (count_null.Length > 1)
                    //        {
                    //            string pop = "alert('Null Bytes are not allowed!!');";
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    //            return;
                    //        }

                    //        strFileName = Path.GetFileName(updPassbook.PostedFile.FileName);
                    //        strFileExtension = Path.GetExtension(updPassbook.PostedFile.FileName);
                    //        int intlength = updPassbook.PostedFile.ContentLength;
                    //        /////EPF Certificate
                    //        Byte[] byteData = new Byte[intlength];
                    //        updPassbook.PostedFile.InputStream.Read(byteData, 0, intlength);
                    //        Session["byteData"] = byteData;

                    //        string cont = updPassbook.PostedFile.ContentType;
                    //        dbsApp.FileType f = dbsApp.FileType.PDF;
                    //        if (dbsApp.isValidFile(byteData, f, cont) == true)
                    //        {
                    //            if (strFileName == "")
                    //            {
                    //                string pop = "alert('Please upload your scanned Bank Passbook');";
                    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    //            }
                    //            else if (intlength / 1024 > 151)
                    //            {
                    //                string pop1 = "alert('File size should not be more than 150 KB !!!');";
                    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                    //            }
                    //        #endregion
                    //            SqlConnection con = new SqlConnection(_connstrStock);
                    //            con.Open();

                    //            //if (txtadharno.Text.Length == 12)
                    //            //{
                    //            SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                    //            cmd.CommandType = CommandType.StoredProcedure;
                    //            cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                    //            cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    //            cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = txtAccHolderName.Text;
                    //            cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = "";
                    //            cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                    //            cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = 7;
                    //            cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //            DataSet ds = new DataSet();
                    //            da.Fill(ds);

                    //            objCmd = new SqlCommand("SP_FarmerCertUpdate", con);
                    //            objCmd.CommandType = CommandType.StoredProcedure;
                    //            objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                    //            objCmd.Parameters.Add("@CertificateName", SqlDbType.VarChar).Value = strFileName;
                    //            objCmd.Parameters.Add("@Certificate", SqlDbType.VarBinary).Value = (Byte[])Session["byteData"];
                    //            objCmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 7;
                    //            objCmd.ExecuteNonQuery();
                    //            con.Close();
                    //            Response.Write("<script>alert('Successfully Forward To CDAO For Confirmation')</script>");
                    //        }
                    //    }
                    //}
                   
                }
                txtAccHolderName.Text = "";
                txtAccNo.Text = "";
                //ViewState["Chk"] = Chk;
            }

        }
        catch (Exception ex)
        { }
    }

    protected void UpdBankDetails()
    {
        if (string.IsNullOrEmpty(txtAccHolderName.Text))
        {
            Response.Write("<script>alert('Please Enter Account Holder Name')</script>");
            return;
        }
        else if (string.IsNullOrEmpty(txtAccHolderName.Text))
        {
            Response.Write("<script>alert('Please Enter Account Holder Name')</script>");
            return;
        }
        else if (string.IsNullOrEmpty(txtAccNo.Text))
        {
            Response.Write("<script>alert('Please Enter Account No')</script>");
            return;
        }
        else if (ddlBankNm.SelectedIndex == 0)
        {
            Response.Write("<script>alert('Please Select Bank Name')</script>");
            return;
        }
        else if (ddlBranchNm.SelectedIndex == 0)
        {
            Response.Write("<script>alert('Please Select Branch Name')</script>");
            return;
        }
        else if (string.IsNullOrEmpty(txtIfscCd.Text))
        {
            Response.Write("<script>alert('Please Enter IFSC code')</script>");
            return;
        }
        else
        {
            string str1 = "select * from M_FARMER_REGISTRATION as a inner join Tbl_FarmerApprove as b on a.VCHFARMERCODE=b.Farmer_Code where  VCHACCOUNTNO=@AccNo and INTBANKID=@BANKID and INTBRANCHID=@BRANCHID and VCHIFSCCODE=@IFSCCODE and b.Status in('ACCP')  ";
            string[] paramDist1 = { "@AccNo", "@BANKID", "@BRANCHID", "@IFSCCODE" };
            object[] valueDist1 = { txtAccNo.Text, ddlBankNm.SelectedValue, ddlBranchNm.SelectedValue, txtIfscCd.Text };
            DataSet dsFarmDetails1 = dbsFarmer.param_passing_fetchDataSet(str1, paramDist1, valueDist1);

            if (dsFarmDetails1.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Duplicate Account No')</script>");
                return;
            }
            #region

            string strFileName = string.Empty;
            string strFileExtension = string.Empty;
            strFileName = Path.GetFileName(updPassbook.PostedFile.FileName);
            strFileExtension = Path.GetExtension(updPassbook.PostedFile.FileName);
            if (strFileName == "")
            {
                string pop = "alert('Please upload your scanned Bank Passbook');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            }

            string[] count_null = updPassbook.PostedFile.FileName.Split('%');
            string[] count_null_ext = updPassbook.PostedFile.FileName.Split('.');

            if (count_null_ext.Length > 2)
            {
                string pop = "alert('Upload file with proper extension!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
            if (count_null.Length > 1)
            {
                string pop = "alert('Null Bytes are not allowed!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }


            int intlength = updPassbook.PostedFile.ContentLength;
            /////EPF Certificate
            Byte[] byteData = new Byte[intlength];
            updPassbook.PostedFile.InputStream.Read(byteData, 0, intlength);
            Session["byteData"] = byteData;

            string cont = updPassbook.PostedFile.ContentType;
            dbsApp.FileType f = dbsApp.FileType.PDF;
            if (dbsApp.isValidFile(byteData, f, cont) == true)
            {

                if (intlength / 1024 > 151)
                {
                    string pop1 = "alert('File size should not be more than 150 KB !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                }
            #endregion
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();

                //if (txtadharno.Text.Length == 12)
                //{
                SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FarmerCode", SqlDbType.VarChar).Value = ViewState["VCHFARMERCODE"].ToString();
                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = txtAccNo.Text;
                cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = txtAccHolderName.Text;
                cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = ddlBankNm.SelectedValue;
                cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = txtIfscCd.Text;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = ddlBranchNm.SelectedValue;
                cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@GPCode", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Chk", SqlDbType.Int).Value = ViewState["Chk"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                objCmd = new SqlCommand("SP_FarmerCertUpdate", con);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = txtfarmerid.Text;
                objCmd.Parameters.Add("@CertificateName", SqlDbType.VarChar).Value = strFileName;
                objCmd.Parameters.Add("@Certificate", SqlDbType.VarBinary).Value = (Byte[])Session["byteData"];
                objCmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 6;
                objCmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Successfully Forward To CDAO For Confirmation')</script>");
            }
            else
            {
                Response.Write("<script>alert('Please upload only .PDF file')</script>");
            }
        }

    }

    public int GetSubsidyFalureCase(string Fid)
    {

        int status = 0;
        try
        {
            string apiUrl = "http://www.apicol.nic.in";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/FMJAL/Get_Rejected_Ben?FID=" + Fid).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                status = JsonConvert.DeserializeObject<int>(responseString);
            }


        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);

        }
        return status;
    }

    public bool GetSolarPump(string Fid)
    {

        bool status = false;
        try
        {
            string apiUrl = "https://odishasolarpump.nic.in";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/chksolarpump?fid=" + Fid).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                status = JsonConvert.DeserializeObject<bool>(responseString);
            }


        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);

        }
        return status;
    }
    public bool GetFMN(string Fid)
    {

        bool status1 = false;
        try
        {
            string apiUrl = "http://www.apicol.nic.in";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/FMN/GetIMPtaken?fmid=" + Fid).Result;
            //HttpResponseMessage response = client.GetAsync("/chksolarpump?fid=" + Fid).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

             
               Dictionary<string, string> res1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

               if (res1["Message"] == "true")
               {
                   status1 = true;
               }
               else
               {
                   status1 = false;
               }
            }


        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);

        }
        return status1;
    }
    public bool GetFMNGnd(string Fid)
    {

        bool status1 = false;
        try
        {
            string apiUrl = "http://www.apicol.nic.in";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/FMN/GetIsGenderUpdate?fid1=" + Fid).Result;
        //HttpResponseMessage response = client.GetAsync("/chksolarpump?fid=" + Fid).Result;
      
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;


                Dictionary<string, string> res1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                if (res1["Message"] == "true")
                {
                    status1 = true;
                }
                else
                {
                    status1 = false;
                }
            }


        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);

        }
        return status1;
    }
    public bool GetJAL(string Fid)
    {

        bool statusJal = false;
        try
        {
            string apiUrl = "http://www.apicol.nic.in";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/Jalanidhi/CheckFidExist?fid=" + Fid).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

               string res = JsonConvert.DeserializeObject<string>(responseString);
               if (res == "true")
               {
                   statusJal = true;
               }
               else
               {
                   statusJal = false;
               }
            }
        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);

        }
        return statusJal;
    }
    public bool GetApicol(string Fid)
    {

        bool statusApicol = false;
        try
        {
            string apiUrl = "http://www.apicol.nic.in";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/apicol?farmerid=" + Fid).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                string res = JsonConvert.DeserializeObject<string>(responseString);
                if (res == "true")
                {
                    statusApicol = true;
                }
                else
                {
                    statusApicol = false;
                }
            }
        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);

        }
        return statusApicol;
    }
    //protected void radBank_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    trBankDetails.Visible = true;

    //    if (radBank.SelectedValue == "a0")
    //    {
    //        lblAccNo.Visible = true;
    //        lblBankNm.Visible = true;
    //        lblBranchNM.Visible = true;
    //        lblfsc.Visible = true;
            
    //        txtAccNo.Visible = false;
    //        txtIfscCd.Visible = false;
    //        ddlBankNm.Visible = false;
    //        ddlBranchNm.Visible = false;
    //    }
    //    else if (radBank.SelectedValue == "a1")
    //    {
            
    //        lblAccNo.Visible = false;
    //        lblBankNm.Visible = false;
    //        lblBranchNM.Visible = false;
    //        lblfsc.Visible = false;
            
    //        txtAccNo.Visible = true;
    //        txtIfscCd.Visible = true;
    //        ddlBankNm.Visible = true;
    //        ddlBranchNm.Visible = true;
    //    }
    //}
}



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
using System.Net.Http.Headers;
using System.Security.Cryptography;

public partial class Admin_FarmerDetailsUpdByDDA : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();
    SqlConnection objConn = new SqlConnection();
    SqlCommand objCmd = new SqlCommand();
    DataSet ds = new DataSet();
    private const string key = "0cwiza8@ms/e_-3d";
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
                if (this.Session["UserType"].ToString() == "DIST")
                {

                }
                else
                {
                    base.Response.Redirect("../Unauthorised.aspx", true);
                    return;
                }
                if (!IsPostBack)
                {
                    fillFarmerId();
                    fillVisible();
                    fillVisible1();
                    divfarmDetails.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void ddlFarmerId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillData();
        }
        catch (Exception ex)
        { }
    }
    protected void fillData()
    {
        string str = "EXEC sp_SelectFarmerDetails @FarmerId";
        string[] paramDist = { "@FarmerId" };
        object[] valueDist = { ddlFarmerId.SelectedValue };
        DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str, paramDist, valueDist);
        if (dsFarmDetails.Tables[0].Rows.Count > 0)
        {
            divfarmDetails.Visible = true;
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
            tdEAadhaar.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHAADHARNO"].ToString();


            tdEAccHolderName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
            tdEAccNo.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHACCOUNTNO"].ToString();
            tdEBranchName.InnerText = dsFarmDetails.Tables[0].Rows[0]["Branch_Name"].ToString();
            tdEBrankName.InnerText = dsFarmDetails.Tables[0].Rows[0]["Bank_Name"].ToString();
            tdEIfsc.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHIFSCCODE"].ToString();

            //ViewState["ACCHOLDNM"][=dsFarmDetails.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
            ViewState["GPID"] = dsFarmDetails.Tables[0].Rows[0]["VCHGPID"].ToString();
            ViewState["VillageID"] = dsFarmDetails.Tables[0].Rows[0]["VCHVILLAGEID"].ToString();
            ViewState["GenderID"] = dsFarmDetails.Tables[0].Rows[0]["GenderValue"].ToString();
            ViewState["BankID"] = dsFarmDetails.Tables[0].Rows[0]["INTBANKID"].ToString();
            ViewState["BranchID"] = dsFarmDetails.Tables[0].Rows[0]["INTBRANCHID"].ToString();
            ViewState["CategoryID"] = dsFarmDetails.Tables[0].Rows[0]["INTBRANCHID"].ToString();
            //ViewState["BankChangeType"] = dsFarmDetails.Tables[0].Rows[0]["ChangeType"].ToString();


            fetchUpdates();

        }
        else
        {
            divDetails.Visible = false;
            divfarmDetails.Visible = false;
        }
    }

    protected void fetchUpdates()
    {
        //name & father name
        #region
        string strNM = "select FarmerName,FatherName from FARMER_UPDATES where AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId and ChangeType='NM'";
        string[] paramNM = { "@FarmerId" };
        object[] valueNM = { ddlFarmerId.SelectedValue };
        DataSet dsFarmNM = dbsFarmer.param_passing_fetchDataSet(strNM, paramNM, valueNM);
        if (dsFarmNM.Tables[0].Rows.Count > 0)
        {
            divCNm.Visible = true;
            divDNm.Visible = true;
            tdUpdFarmName.InnerText = dsFarmNM.Tables[0].Rows[0]["FarmerName"].ToString();
            tdUpdFatherNm.InnerText = dsFarmNM.Tables[0].Rows[0]["FatherName"].ToString();
        }
        else
        {
            divCNm.Visible = false;
            divDNm.Visible = false;
        }
        #endregion

        //Gender
        #region
        string strGND = "select (Case when Gender=1 then 'Male' else 'Female' end)CGender,Gender from FARMER_UPDATES where AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId and ChangeType='GND'";
        string[] paramGND = { "@FarmerId" };
        object[] valueGND = { ddlFarmerId.SelectedValue };
        DataSet dsFarmGND = dbsFarmer.param_passing_fetchDataSet(strGND, paramGND, valueGND);
        if (dsFarmGND.Tables[0].Rows.Count > 0)
        {

            divGender.Visible = true;
            divGender1.Visible = true;
            tdCGender.InnerText = dsFarmGND.Tables[0].Rows[0]["CGender"].ToString();
            ViewState["Gender"] = dsFarmGND.Tables[0].Rows[0]["Gender"].ToString();
        }
        else
        {
            divGender.Visible = false;
            divGender1.Visible = false;
        }
        #endregion

        //Village
        #region
        string strVLG = "select b.vch_GPName,GPCode,vch_VillageName,VillageCode from FARMER_UPDATES as a inner join PDS_GPMASTER as b on a.GPCode=b.int_GPID inner join PDS_VILLAGEMASTER as c on a.VillageCode=c.int_VillageID  where AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId and ChangeType='GP'";
        string[] paramVLG = { "@FarmerId" };
        object[] valueVLG = { ddlFarmerId.SelectedValue };
        DataSet dsFarmVLG = dbsFarmer.param_passing_fetchDataSet(strVLG, paramVLG, valueVLG);
        if (dsFarmVLG.Tables[0].Rows.Count > 0)
        {

            divVill.Visible = true;
            divVill1.Visible = true;


            tdUpdGp.InnerText = dsFarmVLG.Tables[0].Rows[0]["vch_GPName"].ToString();
            tdUpdVillage.InnerText = dsFarmVLG.Tables[0].Rows[0]["vch_VillageName"].ToString();
            ViewState["VLG"] = dsFarmVLG.Tables[0].Rows[0]["VillageCode"].ToString();
            ViewState["GP"] = dsFarmVLG.Tables[0].Rows[0]["GPCode"].ToString();
        }
        else
        {
            divVill.Visible = false;
            divVill1.Visible = false;
        }
        #endregion

        //Mobile No
        #region
        string strMob = "select MobileNo from FARMER_UPDATES where AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId and ChangeType='MOB'";
        string[] paramMob = { "@FarmerId" };
        object[] valueMob = { ddlFarmerId.SelectedValue };
        DataSet dsFarmMob = dbsFarmer.param_passing_fetchDataSet(strMob, paramMob, valueMob);
        if (dsFarmMob.Tables[0].Rows.Count > 0)
        {

            divMobile.Visible = true;
            divMobile1.Visible = true;
            tdUpdMobile.InnerText = dsFarmMob.Tables[0].Rows[0]["MobileNo"].ToString();
        }
        else
        {
            divMobile.Visible = false;
            divMobile1.Visible = false;
        }
        #endregion

        //AAdhaare No
        #region
        string strADH = "select Aadhaar from FARMER_UPDATES where AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId and ChangeType='AADH'";
        string[] paramADH = { "@FarmerId" };
        object[] valueADH = { ddlFarmerId.SelectedValue };
        DataSet dsFarmADH = dbsFarmer.param_passing_fetchDataSet(strADH, paramADH, valueADH);
        if (dsFarmADH.Tables[0].Rows.Count > 0)
        {

            divAadhar.Visible = true;
            divAadha1.Visible = true;
            tdUpdAadhaar.InnerText = dsFarmADH.Tables[0].Rows[0]["Aadhaar"].ToString();
        }
        else
        {
            divAadhar.Visible = false;
            divAadha1.Visible = false;
        }
        #endregion

        //Category
        #region
        string strCAT = "select (case when Category=1 then 'General' when Category=2 then 'SC' else 'ST' end)CategoryType,Category from FARMER_UPDATES where AAOStatus is null and DDAStatus is null and FarmerId=@FarmerId and ChangeType='CATGRY'";
        string[] paramCAT = { "@FarmerId" };
        object[] valueCAT = { ddlFarmerId.SelectedValue };
        DataSet dsFarmCAT = dbsFarmer.param_passing_fetchDataSet(strCAT, paramCAT, valueCAT);
        if (dsFarmCAT.Tables[0].Rows.Count > 0)
        {

            divVatagory.Visible = true;
            divVatagory1.Visible = true;
            tdUpdCategory.InnerText = dsFarmCAT.Tables[0].Rows[0]["CategoryType"].ToString();
            ViewState["Category"] = dsFarmCAT.Tables[0].Rows[0]["Category"].ToString();
        }
        else
        {
            divVatagory.Visible = false;
            divVatagory1.Visible = false;
        }
        #endregion


        //Bank
        #region
        string strBank = "EXEC sp_SelectFarmUpdatedValue @FarmerId";
        string[] paramBank = { "@FarmerId" };
        object[] valueBank = { ddlFarmerId.SelectedValue };
        DataSet dsFarmBank = dbsFarmer.param_passing_fetchDataSet(strBank, paramBank, valueBank);
        if (dsFarmBank.Tables[0].Rows.Count > 0)
        {

            divBank.Visible = true;
            divBank1.Visible = true;

            tdUpAccHolderNm.InnerText = dsFarmBank.Tables[0].Rows[0]["ACCHOLDERNAME"].ToString();
            tdUpdAccNo.InnerText = dsFarmBank.Tables[0].Rows[0]["AccountNo"].ToString();
            tdUpdBankName.InnerText = dsFarmBank.Tables[0].Rows[0]["vchBankName"].ToString();
            tdUpdBranchNm.InnerText = dsFarmBank.Tables[0].Rows[0]["vchBranchName"].ToString();
            tdUpdIfsc.InnerText = dsFarmBank.Tables[0].Rows[0]["IFSC"].ToString();


            ViewState["Branch"] = dsFarmBank.Tables[0].Rows[0]["Branch"].ToString();
            ViewState["BankName"] = dsFarmBank.Tables[0].Rows[0]["BANKNAME"].ToString();
            ViewState["BankChangeType"] = dsFarmBank.Tables[0].Rows[0]["ChangeType"].ToString();//"BANK";//
        }
        else
        {
            divBank.Visible = false;
            divBank1.Visible = false;
        }
        #endregion
    }
    protected void fillFarmerId()
    {
        string str = "select distinct a.FarmerId from FARMER_UPDATES as a inner join M_FARMER_REGISTRATION as b on a.FarmerId=b.NICFARMERID inner join PDS_BLOCKMASTER as c on b.VCHBLOCKID=c.int_BlockID where a.AAOStatus is null and DDAStatus is null and FarmerId!='bar/9180' and left(c.NIC_BlockCode,2)=@DDAID";
        // and left(c.NIC_BlockCode,2)=@DDAID
        string[] paramDist = { "@DDAID" };
        object[] valueDist = { Session["FMDDACD"].ToString() };
        DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str, paramDist, valueDist);
        if (dsFarmDetails.Tables[0].Rows.Count > 0)
        {
            ddlFarmerId.DataSource = dsFarmDetails;
            ddlFarmerId.DataValueField = "FarmerId";
            ddlFarmerId.DataTextField = "FarmerId";
            ddlFarmerId.DataBind();
            ddlFarmerId.Items.Insert(0, "-select-");
        }
        else
        {
            ddlFarmerId.Items.Insert(0, "-select-");
        }
    }
    
    protected void fillVisible()
    {
        divCNm.Visible = false;
        divDNm.Visible = false;
        divVill.Visible = false;
        divVill1.Visible = false;
        divVatagory.Visible = false;
        divVatagory1.Visible = false;
        divMobile.Visible = false;
        divMobile1.Visible = false;
        divGender1.Visible = false;
        divGender.Visible = false;
        divBank1.Visible = false;
        divBank.Visible = false;
        divAadha1.Visible = false;
        divAadhar.Visible = false;
    }

    protected void btnNameSave_Click(object sender, EventArgs e)
    {
        try
        {
        SqlConnection con = new SqlConnection(_connstrStock);
        con.Open();
        SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateNM", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
        cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
        cmd.Parameters.Add("@FarmerNameUpd", SqlDbType.VarChar).Value = tdUpdFarmName.InnerText;
        cmd.Parameters.Add("@FatmerName ", SqlDbType.VarChar).Value = tdFatherName.InnerText;
        cmd.Parameters.Add("@FatmerNameUpd ", SqlDbType.VarChar).Value = tdUpdFatherNm.InnerText;
        cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
        cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        //Response.Write("<script>alert('Name & Father Name Updated Successfully')</script>");
        string pop = "alert('Name & Father Name Updated Successfully !!!.');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        fillData();
       
        }
        catch (Exception ex)
        { }
    }
    protected void btnNameSave0_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtRejName.Text))
            {
                Response.Write("<script>alert('Please enere reason of rejection.')</script>");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_FarmerUpdateReject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                cmd.Parameters.Add("@UserId ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                cmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pop = "alert('Rejected Successfully !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                fillData();
                //Response.Write("<script>alert('Rejected Successfully')</script>");
                //fetchUpdates();

            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnGender_Click(object sender, EventArgs e)
    {
        try
        {
            bool Status1 = GetFMNGnd(ddlFarmerId.SelectedValue);
            if (Status1 == true)
            {
                Response.Write("<script>alert('Can`t be changed due to farm mechinary subsidy availed')</script>");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateGND", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = ViewState["GenderID"].ToString();
                cmd.Parameters.Add("@GenderUpd", SqlDbType.VarChar).Value = ViewState["Gender"].ToString();
                cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string pop = "alert('Gender Change Successfully !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                fillData();

                //Response.Write("<script>alert('Gender Change Successfully')</script>");
                //fillData();
            }
            
        }
        catch (Exception ex)
        {  }
    }
    protected void btnGender0_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtGenderRej.Text))
            {
                Response.Write("<script>alert('Please enere reason of rejection.')</script>");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_FarmerUpdateReject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = txtGenderRej.Text;
                cmd.Parameters.Add("@UserId ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                cmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 2;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string pop = "alert('Rejected Successfully !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                fillData();
                //Response.Write("<script>alert('Rejected Successfully')</script>");
                //fetchUpdates();

            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnGp_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(_connstrStock);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateGP", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
            cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
            cmd.Parameters.Add("@Gp", SqlDbType.VarChar).Value = ViewState["GPID"].ToString();
            cmd.Parameters.Add("@GpUpd", SqlDbType.VarChar).Value = ViewState["GP"].ToString();
            cmd.Parameters.Add("@Village", SqlDbType.VarChar).Value = ViewState["VillageID"].ToString();
            cmd.Parameters.Add("@VillageUpd", SqlDbType.VarChar).Value = ViewState["VLG"].ToString();
            cmd.Parameters.Add("@GpUpdNm", SqlDbType.VarChar).Value = tdUpdGp.InnerText;
            cmd.Parameters.Add("@VillageUpdNM", SqlDbType.VarChar).Value = tdUpdVillage.InnerText;

            cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
            cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string pop = "alert('GP & Village Change Successfully !!!.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            fillData();
            //Response.Write("<script>alert('GP & Village Change Successfully')</script>");
            //fillData();
            

        }
        catch (Exception ex)
        {  }

    }
    protected void btnMobileNo_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(_connstrStock);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateMOB", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
            cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = tdEMobileNo.InnerText;
            cmd.Parameters.Add("@MobileUpd", SqlDbType.VarChar).Value = tdUpdMobile.InnerText;
            cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
            cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string pop = "alert('Mobile No Change Successfully !!!.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            fillData();
            //Response.Write("<script>alert('Mobile No Change Successfully')</script>");
            //fillData();

        }
        catch (Exception ex)
        {  }

    }
    protected void btnMobileNo0_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtRejMobile.Text))
            {
                Response.Write("<script>alert('Please enter reason for rejection')</script>");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_FarmerUpdateReject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = txtRejMobile.Text;
                cmd.Parameters.Add("@UserId ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                cmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 8;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string pop = "alert('Rejected Successfully !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                fillData();
                //Response.Write("<script>alert('Rejected Successfully')</script>");
                //fetchUpdates();

            }
           
        }
        catch (Exception ex)
        { }
    }
    protected void btnAadhaar_Click(object sender, EventArgs e)
    {
        try
        {
            string IV = Aes256CbcEncryptDecrypt.IV();
            byte[] iv = Encoding.UTF8.GetBytes(IV);
            string HMAC = BitConverter.ToString(Aes256CbcEncryptDecrypt.HmacSHA256(Convert.ToBase64String(iv) + tdUpdAadhaar.InnerText, key)).Replace("-", "").ToLower();
            string _enAadhaarNo = Aes256CbcEncryptDecrypt.Encrypt(tdUpdAadhaar.InnerText, key, iv);

            SqlConnection con = new SqlConnection(_connstrStock);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateAADH", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
            cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
            cmd.Parameters.Add("@Aadhaar", SqlDbType.VarChar).Value = tdEAadhaar.InnerText;
            cmd.Parameters.Add("@AadhaarUpd", SqlDbType.VarChar).Value = tdUpdAadhaar.InnerText;
            cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
            cmd.Parameters.Add("@IV ", SqlDbType.VarChar).Value = IV;
            cmd.Parameters.Add("@HMAC ", SqlDbType.VarChar).Value = HMAC;
            cmd.Parameters.Add("@_enAadhaarNo ", SqlDbType.VarChar).Value = _enAadhaarNo;

            cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string pop = "alert('AADHAAR No Updatted Successfully !!!.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            fillData();
            //Response.Write("<script>alert('AADHAAR No Change Successfully')</script>");
            //fillData();
        }
        catch (Exception ex)
        {    }
    }
    protected void btnCategory_Click(object sender, EventArgs e)
    {
        try
        {
            bool Status = GetSolarPump(ddlFarmerId.SelectedValue);
            if (Status == true)
            {
                Response.Write("<script>alert('Can`t be changed due to solar pump subsidy availed')</script>");
                return;
            }
            bool Status1 = GetFMN(ddlFarmerId.SelectedValue);
            if (Status1 == true)
            {
                Response.Write("<script>alert('Can`t be changed due to farm mechinary subsidy availed')</script>");
                return;
            }
            bool statusJal = GetJAL(ddlFarmerId.SelectedValue);
            if (statusJal == true)
            {
                Response.Write("<script>alert('Can`t be changed due to jalanidhi subsidy availed')</script>");
                return;
            }
            bool statusApicol = GetApicol(ddlFarmerId.SelectedValue);
            if (statusApicol == true)
            {
                Response.Write("<script>alert('Can`t be changed due to apicol subsidy availed')</script>");
                return;
            }
            else
            {


                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateCATGRY", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = ViewState["CategoryID"].ToString();
                cmd.Parameters.Add("@CategoryUpd", SqlDbType.VarChar).Value = ViewState["Category"].ToString();
                cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string pop = "alert('Category Change Successfully !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                fillData();

                
                //Response.Write("<script>alert('Category Changes Successfully')</script>");
                //fillData();

            }
        }
        catch (Exception ex)
        {    }
    }
    protected void btnCategory0_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtRejCategory.Text))
            {
                Response.Write("<script>alert('Please enter reason of rejection')</script>");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_FarmerUpdateReject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = txtRejCategory.Text;
                cmd.Parameters.Add("@UserId ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                cmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 5;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pop = "alert('Rejected Successfully !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                fillData();
                //Response.Write("<script>alert('Rejected Successfully')</script>");
                //fetchUpdates();

            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnBankDetails_Click(object sender, EventArgs e)
    {
        try
        {
            int StatusF = GetSubsidyFalureCase(ddlFarmerId.SelectedValue);
            if (StatusF == 1)
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateBANK", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = tdEAccHolderName.InnerText;
                cmd.Parameters.Add("@ACCHOLDERNAMEUpd", SqlDbType.VarChar).Value = tdUpAccHolderNm.InnerText;
                cmd.Parameters.Add("@AccNO", SqlDbType.VarChar).Value = tdEAccNo.InnerText;
                cmd.Parameters.Add("@AccNOUpd", SqlDbType.VarChar).Value = tdUpdAccNo.InnerText;
                cmd.Parameters.Add("@BankNm", SqlDbType.VarChar).Value = ViewState["BankID"].ToString();
                cmd.Parameters.Add("@BankNmUpd", SqlDbType.VarChar).Value = ViewState["BankName"].ToString();
                cmd.Parameters.Add("@BranchNm", SqlDbType.VarChar).Value = ViewState["BranchID"].ToString();
                cmd.Parameters.Add("@BranchNmUpd", SqlDbType.VarChar).Value = ViewState["Branch"].ToString();
                cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = tdEIfsc.InnerText;
                cmd.Parameters.Add("@IFSCUpd", SqlDbType.VarChar).Value = tdUpdIfsc.InnerText;

                cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                cmd.Parameters.Add("@Chk ", SqlDbType.Int).Value = 0;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string pop = "alert('Updatted Successfully !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                fillData();

            }
            else
            {
                bool Status = GetSolarPump(ddlFarmerId.SelectedValue);
                if (Status == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to solar pump subsidy availed')</script>");
                    return;
                }
                bool Status1 = GetFMN(ddlFarmerId.SelectedValue);
                if (Status1 == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to farm mechinary subsidy availed')</script>");
                    return;
                }
                bool statusJal = GetJAL(ddlFarmerId.SelectedValue);
                if (statusJal == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to jalanidhi subsidy availed')</script>");
                    return;
                }
                bool statusApicol = GetApicol(ddlFarmerId.SelectedValue);
                if (statusApicol == true)
                {
                    Response.Write("<script>alert('Can`t be changed due to apicol subsidy availed')</script>");
                    return;
                }
                else
                {
                    if (ViewState["BankChangeType"].ToString() == "BANK")
                    {
                        SqlConnection con = new SqlConnection(_connstrStock);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateBANK", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                        cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                        cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = tdEAccHolderName.InnerText;
                        cmd.Parameters.Add("@ACCHOLDERNAMEUpd", SqlDbType.VarChar).Value = tdUpAccHolderNm.InnerText;
                        cmd.Parameters.Add("@AccNO", SqlDbType.VarChar).Value = tdEAccNo.InnerText;
                        cmd.Parameters.Add("@AccNOUpd", SqlDbType.VarChar).Value = tdUpdAccNo.InnerText;
                        cmd.Parameters.Add("@BankNm", SqlDbType.VarChar).Value = ViewState["BankID"].ToString();
                        cmd.Parameters.Add("@BankNmUpd", SqlDbType.VarChar).Value = ViewState["BankName"].ToString();
                        cmd.Parameters.Add("@BranchNm", SqlDbType.VarChar).Value = ViewState["BranchID"].ToString();
                        cmd.Parameters.Add("@BranchNmUpd", SqlDbType.VarChar).Value = ViewState["Branch"].ToString();
                        cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = tdEIfsc.InnerText;
                        cmd.Parameters.Add("@IFSCUpd", SqlDbType.VarChar).Value = tdUpdIfsc.InnerText;

                        cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                        cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                        cmd.Parameters.Add("@Chk ", SqlDbType.Int).Value = 0;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        string pop1 = "alert('Updatted Successfully !!!.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                        fillData();
                    }
                    else
                    {
                        //SqlConnection con = new SqlConnection(_connstrStock);
                        //con.Open();
                        //SqlCommand cmd = new SqlCommand("sp_FarmDetailsUpdateBANK", con);
                        //cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                        //cmd.Parameters.Add("@FarmerName", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                        //cmd.Parameters.Add("@ACCHOLDERNAME", SqlDbType.VarChar).Value = tdEAccHolderName.InnerText;
                        //cmd.Parameters.Add("@ACCHOLDERNAMEUpd", SqlDbType.VarChar).Value = tdUpAccHolderNm.InnerText;
                        //cmd.Parameters.Add("@AccNO", SqlDbType.VarChar).Value = "";
                        //cmd.Parameters.Add("@AccNOUpd", SqlDbType.VarChar).Value = "";
                        //cmd.Parameters.Add("@BankNm", SqlDbType.VarChar).Value = "";
                        //cmd.Parameters.Add("@BankNmUpd", SqlDbType.VarChar).Value = "";
                        //cmd.Parameters.Add("@BranchNm", SqlDbType.VarChar).Value = "";
                        //cmd.Parameters.Add("@BranchNmUpd", SqlDbType.VarChar).Value = "";
                        //cmd.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = "";
                        //cmd.Parameters.Add("@IFSCUpd", SqlDbType.VarChar).Value = "";

                        //cmd.Parameters.Add("@User_Id ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                        //cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                        //cmd.Parameters.Add("@Chk ", SqlDbType.Int).Value = 1;

                        //SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //DataSet ds = new DataSet();
                        //da.Fill(ds);
                        //Response.Write("<script>alert('Update Successfully')</script>");
                        //fetchUpdates();

                    }
                }
            }
        }
        catch (Exception ex)
        {   }

    }
    protected void LinkGnd_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
            con.Open();

            objCmd = new SqlCommand("select CertificateName,Certificate from FARMER_UPDATES as a inner join FARMER_UPDATE_CERTIFICATE as b on a.FarmerId=b.FarmerId and a.Id=b.ReferalId  where ChangeType='GND' and A.DDAStatus is null and AAOStatus is null and a.FarmerId='" + ddlFarmerId.SelectedValue + "'", con);
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            Byte[] byteDoc = new Byte[(dr.GetBytes(1, 0, null, 0, int.MaxValue))];
            dr.GetBytes(1, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            con.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.End();
           
        }
        catch (Exception ex)
        { }
    }
    protected void lnkGP_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
            con.Open();

            objCmd = new SqlCommand("select CertificateName,Certificate from FARMER_UPDATES as a inner join FARMER_UPDATE_CERTIFICATE as b on a.FarmerId=b.FarmerId and a.Id=b.ReferalId  where ChangeType='GP' and A.DDAStatus is null and AAOStatus is null and a.FarmerId='" + ddlFarmerId.SelectedValue + "'", con);
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            Byte[] byteDoc = new Byte[(dr.GetBytes(1, 0, null, 0, int.MaxValue))];
            dr.GetBytes(1, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            con.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.End();
        }
        catch (Exception ex)
        { }
    }
    protected void lnkAadhar_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
            con.Open();

            objCmd = new SqlCommand("select CertificateName,Certificate from FARMER_UPDATES as a inner join FARMER_UPDATE_CERTIFICATE as b on a.FarmerId=b.FarmerId and a.Id=b.ReferalId  where ChangeType='AADH' and A.DDAStatus is null and AAOStatus is null and a.FarmerId='" + ddlFarmerId.SelectedValue + "'", con);
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            Byte[] byteDoc = new Byte[(dr.GetBytes(1, 0, null, 0, int.MaxValue))];
            dr.GetBytes(1, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            con.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.End();
        }
        catch (Exception ex)
        { }
    }
    protected void lnkCast_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
            con.Open();

            objCmd = new SqlCommand("select CertificateName,Certificate from FARMER_UPDATES as a inner join FARMER_UPDATE_CERTIFICATE as b on a.FarmerId=b.FarmerId and a.Id=b.ReferalId  where ChangeType='CATGRY' and A.DDAStatus is null and AAOStatus is null and a.FarmerId='" + ddlFarmerId.SelectedValue + "'", con);
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            Byte[] byteDoc = new Byte[(dr.GetBytes(1, 0, null, 0, int.MaxValue))];
            dr.GetBytes(1, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            con.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.End();
        }
        catch (Exception ex)
        { }
    }
    protected void lnkBank_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
            con.Open();

            objCmd = new SqlCommand("select CertificateName,Certificate from FARMER_UPDATES as a inner join FARMER_UPDATE_CERTIFICATE as b on a.FarmerId=b.FarmerId and a.Id=b.ReferalId  where ChangeType='BANK' and A.DDAStatus is null and AAOStatus is null and a.FarmerId='" + ddlFarmerId.SelectedValue + "'", con);
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            Byte[] byteDoc = new Byte[(dr.GetBytes(1, 0, null, 0, int.MaxValue))];
            dr.GetBytes(1, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            con.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.End();
        }
        catch (Exception ex)
        { }
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
    protected void linknm_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
            con.Open();

            objCmd = new SqlCommand("select CertificateName,Certificate from FARMER_UPDATES as a inner join FARMER_UPDATE_CERTIFICATE as b on a.FarmerId=b.FarmerId and a.Id=b.ReferalId  where ChangeType='NM' and A.DDAStatus is null and AAOStatus is null and a.FarmerId='" + ddlFarmerId.SelectedValue + "'", con);
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            Byte[] byteDoc = new Byte[(dr.GetBytes(1, 0, null, 0, int.MaxValue))];
            dr.GetBytes(1, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            con.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.End();
        }
        catch (Exception ex)
        { }
    }
    protected void radName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radName.SelectedValue == "a0")
        {
            trRejNm.Visible = false;
            btnNameSave0.Visible = false;
            btnNameSave.Visible = true;
        }
        else
        {
            trRejNm.Visible = true;
            btnNameSave0.Visible = true;
            btnNameSave.Visible = false;
        }

    }
    protected void radGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radGender.SelectedValue == "a0")
        {
            trRejGender.Visible = false;
            btnGender0.Visible = false;
            btnGender.Visible = true;
        }
        else
        {
            trRejGender.Visible = true;
            btnGender0.Visible = true;
            btnGender.Visible = false;
        }

    }
    protected void radGP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radGP.SelectedValue == "a0")
        {
            trRejGp.Visible = false;
            btnGp0.Visible = false;
            btnGp.Visible = true;
        }
        else
        {
            trRejGp.Visible = true;
            btnGp0.Visible = true;
            btnGp.Visible = false;
        }
    }
    protected void radMobile_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radMobile.SelectedValue == "a0")
        {
            trRejMobile.Visible = false;
            btnMobileNo0.Visible = false;
            btnMobileNo.Visible = true;
        }
        else
        {
            trRejMobile.Visible = true;
            btnMobileNo0.Visible = true;
            btnMobileNo.Visible = false;
        }
    }
    protected void radAadhaar_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radAadhaar.SelectedValue == "a0")
        {
            trRejAadhaar.Visible = false;
            btnAadhaar0.Visible = false;
            btnAadhaar.Visible = true;
        }
        else
        {
            trRejAadhaar.Visible = true;
            btnAadhaar0.Visible = true;
            btnAadhaar.Visible = false;
        }
    }
    protected void radCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radCategory.SelectedValue == "a0")
        {
            trRejCategory.Visible = false;
            btnCategory0.Visible = false;
            btnCategory.Visible = true;
        }
        else
        {
            trRejCategory.Visible = true ;
            btnCategory0.Visible = true;
            btnCategory.Visible = false;
        }
    }
    protected void radBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radBank.SelectedValue == "a0")
        {
            trRejBank.Visible = false;
            btnBankDetails0.Visible = false;
            btnBankDetails.Visible = true;
        }
        else
        {
            trRejBank.Visible = false;
            btnBankDetails0.Visible = false;
            btnBankDetails.Visible = true;
        }
    }
    protected void btnGp0_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtRejGP.Text))
            {
                Response.Write("<script>alert('Please enter reason of rejection')</script>");
                return;
            }
            else
            {
           
            SqlConnection con = new SqlConnection(_connstrStock);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_FarmerUpdateReject", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
            cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = txtRejGP.Text;
            cmd.Parameters.Add("@UserId ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
            cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
            cmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 1;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            fetchUpdates();
            Response.Write("<script>alert('Rejected Successfully')</script>");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnAadhaar0_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtRejAadhaar.Text))
            {
                Response.Write("<script>alert('Please enter reason of rejection')</script>");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_FarmerUpdateReject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = tdFarmerName.InnerText;
                cmd.Parameters.Add("@UserId ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                cmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 4;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                fetchUpdates();
                Response.Write("<script>alert('Rejected Successfully')</script>");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnBankDetails0_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtRejBank.Text))
            {
                Response.Write("<script>alert('Please enter reason of rejection')</script>");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(_connstrStock);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_FarmerUpdateReject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = txtRejBank.Text;
                cmd.Parameters.Add("@UserId ", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                cmd.Parameters.Add("@Ip ", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                cmd.Parameters.Add("@chk1", SqlDbType.Int).Value = 6;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                fetchUpdates();
                Response.Write("<script>alert('Rejected Successfully')</script>");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void fillVisible1()
    {
        trRejNm.Visible = false;
        btnNameSave.Visible = false;
        btnNameSave0.Visible = false;
        trRejGender.Visible = false;
        btnGender0.Visible = false;
        btnGender.Visible = false;
        trRejGp.Visible = false;
        btnGp0.Visible = false;
        btnGp.Visible = false;
        trRejMobile.Visible = false;
        btnMobileNo0.Visible = false;
        btnMobileNo.Visible = false;
        trRejAadhaar.Visible = false;
        btnAadhaar0.Visible = false;
        btnAadhaar.Visible = false;
        trRejCategory.Visible = false;
        btnCategory0.Visible = false;
        btnCategory.Visible = false;
        trRejBank.Visible = false;
        btnBankDetails0.Visible = false;
        btnBankDetails.Visible = false;
        radName.SelectedIndex = -1;
        radGender.SelectedIndex = -1;
        radGP.SelectedIndex = -1;
        radMobile.SelectedIndex = -1;
        radAadhaar.SelectedIndex = -1;
        radCategory.SelectedIndex = -1;
        radBank.SelectedIndex = -1;

    }
    public class Aes256CbcEncryptDecrypt
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        internal static string Encrypt(string plainText, string key, byte[] iv)
        {
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider
                {
                    KeySize = 256,
                    BlockSize = 128,
                    Padding = PaddingMode.PKCS7,
                    Mode = CipherMode.CBC,
                    Key = encoding.GetBytes(key)
                };
                ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, iv);
                byte[] buffer = encoding.GetBytes(plainText);
                string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return encryptedText;
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
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider
                {
                    KeySize = 256,
                    BlockSize = 128,
                    Padding = PaddingMode.PKCS7,
                    Mode = CipherMode.CBC,
                    Key = encoding.GetBytes(key)
                };
                ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, iv);
                byte[] buffer = Convert.FromBase64String(encryptedText);
                return encoding.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                throw new Exception("Error decrypting: " + e.Message);
            }
        }

        internal static byte[] HmacSHA256(string data, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(key)))
            {
                return hmac.ComputeHash(encoding.GetBytes(data));
            }
        }

        private static Random random = new Random();

        internal static string IV()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789/-_@#!$%&";
            return new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
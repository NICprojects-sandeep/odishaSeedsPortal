using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Xml.XPath;

public partial class Dealer_Dealer_Sale : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    Regex RegDec = new Regex(@"^[-]?((\d+)|(\d+\.\d+)|(\.\d+))$");
    Regex RegNum = new Regex("^[0-9]*$");
    SqlConnection conStock = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDLLDIST = new DLL_Dealer();
        int Val = 0;

        Val = objUserDLLDIST.CheckDate();
        if (Val == 1)
        {
            if (Session["REF_NO"] != null)
            {
                objUserBELDIST = new BLL_Dealer();
                objUserBELDIST.REF_NO = Session["REF_NO"].ToString();
                objUserDLLDIST = new DLL_Dealer();
                ds = new DataSet();
                ds = objUserDLLDIST.CheckLicMob(objUserBELDIST);
                //string[] param = { "@REF_NO" };
                //object[] value = { Session["REF_NO"].ToString() };
                //ds = dbsApp.param_passing_fetch("SELECT COUNT(*)CNT FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE REF_NO = @REF_NO AND ACCESS_MODE = 'W'", param, value);

                string Cnt = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["CNT"].ToString());
                if (Convert.ToInt32(Cnt) == 0)
                {
                    string pop = "alert('Oops you have opted for Mobile Apps.')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    Response.Redirect("Home.aspx");
                }
            }
            if (!IsPostBack)
            {
                pnlHeader.Visible = true;
                pnlDetails.Visible = false;
                CreteDataTable();
                btnSave.Visible = false;
                btnConfirm.Enabled = false;
                FillFinYr();
                FillSeassion();
                FillCrops();
                FillVariety();
                GetUserType();
                GetDistCodeByLiceneNo();
                GetPrefix();

                txtOTP.Visible = false;
                lblOTPMsg.Visible = false;
                btnConfirmOTP.Visible = false;
                btnChangeMobNo.Visible = false;
                btnSendOTP.Visible = false;
                txtCMobNo.Visible = false;
                btnUpdateMobNo.Visible = false;
                btnUpdateMobNoCancel.Visible = false;
            }
        }
        else
        {
            divview.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Online seed transactions for Kharif 2022-23 ll be started soon.'); window.location='" + Request.ApplicationPath + "/Home.aspx';", true);
        }
    }
    private void GetDistCodeByLiceneNo()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetDistCodeByLic(objUserBELDIST);

        //string[] param = { "@LIC_NO" };
        //object[] val = { Session["LIC_NO"].ToString() };

        //ds = dbsApp.param_passing_fetch("SELECT DIST_CODE FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO = @LIC_NO", param, val);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["DIST_CODE"] = ds.Tables[0].Rows[0]["DIST_CODE"].ToString();
            }
        }
    }
    private void GetPrefix()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.DIST_CODE = Session["DIST_CODE"].ToString();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetShortNameByDistCode(objUserBELDIST);

        //string[] param = { "@DIST_CODE" };
        //object[] val = { Session["DIST_CODE"].ToString() };

        //ds = dbsApp.param_passing_fetch("SELECT SHORT_NAME FROM [DAFPSEED].[DBO].[dist] WHERE DIST_CODE = @DIST_CODE", param, val);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Pre = ds.Tables[0].Rows[0]["SHORT_NAME"].ToString();
                lblPre.Text = Pre + "/";
            }
        }
    }
    private void GetUserType()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetUserType(objUserBELDIST);

        //string[] param = { "@LIC_NO" };
        //object[] val = { Session["LIC_NO"].ToString() };
        //ds = dbsApp.param_passing_fetch("SELECT COMP_NAME FROM [DAFPSEED].[DBO].[SEED_LIC_COMP_DIST] WHERE SEED_LIC_DIST_ID = (SELECT SEED_LIC_DIST_ID FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO = @LIC_NO) AND COMP_TYPE = 1", param, val);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                hfUserType.Value = ds.Tables[0].Rows[0]["COMP_NAME"].ToString();
            }
        }
    }
    private void FillSeassion()
    {
        ddlSeassion.Items.Clear();
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.F_Year = ddlFinYr.SelectedValue;
        objUserBELDIST.STATUS = "T";
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.FillSeassion(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSeassion.DataSource = ds;
                ddlSeassion.DataTextField = "SEASSION_NAME";
                ddlSeassion.DataValueField = "SHORT_NAME";
                ddlSeassion.DataBind();
            }
        }
        //ddlSeassion.Items.Insert(0, "--Select--");
    }
    private void FillFinYr()
    {
        ddlFinYr.Items.Clear();
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.STATUS = "T";
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.FillFinYr(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlFinYr.DataSource = ds;
                ddlFinYr.DataTextField = "FIN_YR";
                ddlFinYr.DataValueField = "FIN_YR";
                ddlFinYr.DataBind();
            }
        }
        //ddlFinYr.Items.Insert(0, "--Select--");
    }
    private void CreteDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[13]
                {
                    new DataColumn("CROP_ID", typeof(string)),
                    new DataColumn("Crop_Name", typeof(string)),
                    new DataColumn("CROP_VERID", typeof(string)),
                    new DataColumn("Crop_VerName", typeof(string)),
                    new DataColumn("LOT_NO", typeof(string)),
                    new DataColumn("Receive_Unitcd",typeof(string)),
                    new DataColumn("Receive_Unitname",typeof(string)),
                    new DataColumn("BAG_SIZE_KG",typeof(string)),
                    new DataColumn("NO_OF_BAGS",typeof(string)),
                    new DataColumn("QUANTITY",typeof(string)),
                    new DataColumn("PRICE_QTL",typeof(string)),
                    new DataColumn("SUBSIDY_QTL",typeof(string)),
                    new DataColumn("AMOUNT",typeof(string))
                });
        Session.Add("dt", dt);
    }
    private void FillVariety()
    {
        ddlVariety.Items.Clear();
        if (ddlCropName.SelectedValue == "--Select--")
        {
            ddlVariety.Items.Insert(0, "--Select--");
        }
        else
        {
            if (Session["LIC_NO"] != null)
            {
                objUserBELDIST = new BLL_Dealer();
                objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
                objUserBELDIST.CROP_ID = ddlCropName.SelectedValue;
                objUserBELDIST.SESSION = ddlSeassion.SelectedValue;
                objUserBELDIST.F_Year = ddlFinYr.SelectedValue;
                objUserDLLDIST = new DLL_Dealer();
                ds = new DataSet();
                ds = objUserDLLDIST.FillCropVarietyByCropIdDealerStock(objUserBELDIST);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlVariety.DataSource = ds;
                        ddlVariety.DataTextField = "Variety_Name";
                        ddlVariety.DataValueField = "Variety_Code";
                        ddlVariety.DataBind();
                    }
                }
                ddlVariety.Items.Insert(0, "--Select--");
            }
            else
            {
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }
    private void FillCrops()
    {
        ddlCropName.Items.Clear();

        if (Session["LIC_NO"] != null)
        {
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();

            objUserBELDIST.SESSION = ddlSeassion.SelectedValue;
            objUserBELDIST.F_Year = ddlFinYr.SelectedValue;
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.FillCropDealerStock(objUserBELDIST);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCropName.DataSource = ds;
                    ddlCropName.DataTextField = "Crop_Name";
                    ddlCropName.DataValueField = "Crop_Code";
                    ddlCropName.DataBind();
                }
            }
            ddlCropName.Items.Insert(0, "--Select--");
        }
        else
        {
            Response.Redirect("~/Seed/District/LogIn.aspx");
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCrops();
        FillVariety();
    }
    protected void ddlCropName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillVariety();
    }
    protected void ddlVariety_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    private void FillGrid()
    {
        Grd_Sale.DataSource = null;
        Grd_Sale.DataBind();

        if (ddlCropName.SelectedValue != "--Select--" && ddlVariety.SelectedValue != "--Select--")
        {
            if (Session["LIC_NO"] != null)
            {
                objUserBELDIST = new BLL_Dealer();
                objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
                objUserBELDIST.CROP_ID = ddlCropName.SelectedValue;
                objUserBELDIST.CROP_VERID = ddlVariety.SelectedValue;
                objUserBELDIST.SESSION = ddlSeassion.SelectedValue;
                objUserBELDIST.F_Year = ddlFinYr.SelectedValue;
                objUserBELDIST.USER_TYPE = hfUserType.Value;
                objUserDLLDIST = new DLL_Dealer();
                ds = new DataSet();
                ds = objUserDLLDIST.FillSaleDealerGrid(objUserBELDIST);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_Sale.DataSource = ds;
                        Grd_Sale.DataBind();
                    }
                }
            }
            else
            {
                Response.Redirect("~/Seed/District/LogIn.aspx");
            }
        }
    }
    protected void ddlClassOfSeed_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void SetSelectedRecord()
    {
        for (int i = 0; i < Grd_Sale.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)Grd_Sale.Rows[i].Cells[0].FindControl("rbtn_lot");
            if (rb != null)
            {
                HiddenField hfCropVerid = (HiddenField)Grd_Sale.Rows[i].Cells[0].FindControl("hfSTOCK_ID");
                if (hfCropVerid != null && ViewState["SelectedContact"] != null)
                {
                    if (hfCropVerid.Value.Equals(ViewState["SelectedContact"].ToString()))
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    private void GetSelectedRecord()
    {
        for (int i = 0; i < Grd_Sale.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)Grd_Sale.Rows[i].Cells[0].FindControl("rbtn_lot");
            if (rb != null)
            {
                if (rb.Checked)
                {
                    HiddenField hf = (HiddenField)Grd_Sale.Rows[i].Cells[0].FindControl("hfSTOCK_ID");
                    if (hf != null)
                    {
                        ViewState["SelectedContact"] = hf.Value;
                    }

                    break;
                }
            }
        }
    }
    protected void btnStockEnrty_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["LIC_NO"].ToString() != null || Session["LIC_NO"].ToString() != "")
        {
            if (grd_lottrans.Rows.Count > 0)
            {
                objUserBELDIST = new BLL_Dealer();
                objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
                objUserDLLDIST = new DLL_Dealer();
                string DistCode = objUserDLLDIST.GetDistCodeByLicNo(objUserBELDIST);
                objUserDLLDIST = new DLL_Dealer();
                string DAOCode = objUserDLLDIST.GetDAOCodeByLicNo(objUserBELDIST);
                Session["Farmer_ID"] = lblPre.Text + txtFarmerId.Text; ;
                DataTable dt = (DataTable)Session["dt"];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.TableName = "A";
                        string A = ConvertDatatableToXML(dt);
                        int Val = 0;
                        string RefNo = "";

                        objUserBELDIST.FARMER_ID = Session["Farmer_ID"].ToString();
                        objUserBELDIST.DIST_CODE = DistCode;
                        objUserBELDIST.DAO_CD = DAOCode;
                        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
                        objUserBELDIST.SESSION = ddlSeassion.SelectedValue;
                        objUserBELDIST.F_Year = ddlFinYr.SelectedValue;
                        objUserBELDIST.XMLVALUE = ConvertDatatableToXML(dt);
                        objUserBELDIST.UPDATED_BY = Session["LIC_NO"].ToString();
                        objUserBELDIST.USERIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                        objUserDLLDIST = new DLL_Dealer();
                        objUserDLLDIST.InsertSaleDealer(objUserBELDIST, out Val, out RefNo);

                        if (Val == 1)
                        {
                            Session["TRANSACTION_NO"] = RefNo;
                            Reset();
                            string pop = "alert('Transaction Completed!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);


                            PrintReport();
                            //mpeReport.Show();

                        }
                        else if (Val == 0)
                        {
                            string pop = "alert('Some Errors Occurred!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        }
                    }
                    else
                    {
                        string pop = "alert('Please add some variety!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }
                }
                else
                {
                    string pop = "alert('Please add some variety!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }
            }
        }
        else
        {

            string pop = "alert('Session expired!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
    }
    private void Reset()
    {
        pnlHeader.Visible = true;
        pnlDetails.Visible = false;
        CreteDataTable();
        btnSave.Visible = false;
        btnConfirm.Enabled = false;
        FillFinYr();
        FillSeassion();
        FillCrops();
        FillVariety();
        txtFarmerId.Text = "";
        txtFarmerId.Enabled = true;
        td_FatherName.InnerText = "";
        td_FarmerName.InnerText = "";
        td_Category.InnerText = "";
        td_Gender.InnerText = "";

        Grd_Sale.DataSource = null;
        Grd_Sale.DataBind();

        grd_lottrans.DataSource = null;
        grd_lottrans.DataBind();
    }
    public string ConvertDatatableToXML(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    private void PrintReport()
    {
        //string url = "RptSaleVoucher.aspx?Tid=" + Session["TRANSACTION_NO"] + "&FId=" + Session["Farmer_ID"] + "&LicNo=" + Session["LIC_NO"];
        //string popicon = "window.open('" + url + "','_blank','Report','width=1020,height=900,toolbar=1,resizable=1,location=0, status=0, menubar=0, scrollbars=1');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popicon", popicon, true);
        //this.Page_Load(null, null);


        Response.Redirect("RptSaleVoucher.aspx?Tid=" + Session["TRANSACTION_NO"] + "&FId=" + Session["Farmer_ID"] + "&LicNo=" + Session["LIC_NO"]);
        
        
        //if (Session["TRANSACTION_NO"] != null && Session["LIC_NO"] != null && Session["Farmer_ID"] != null)
        //{
        //    lbl_TransactionId.Text = Session["TRANSACTION_NO"].ToString();
        //    lbl_LicNo.Text = Session["LIC_NO"].ToString().ToString();
        //    lbl_LicNo0.Text = Session["LIC_NO"].ToString().ToString();
        //    GetFirmName();
        //    lbl_recvdt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    FillFarmerDtls();
        //    FillTransactions();
        //}
    }
    private void FillTransactions()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = this.Session["Farmer_ID"].ToString();
        objUserBELDIST.TRANSACTION_ID = lbl_TransactionId.Text;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFarmerDtl(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_stockdtls.DataSource = ds;
                grd_stockdtls.DataBind();
            }
        }

        ds = new DataSet();
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.TRANSACTION_ID = lbl_TransactionId.Text;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFarmerInv(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                td_TotAmt.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["TOT_AMT"].ToString());
                td_SubAmt.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SUB_AMT"].ToString());
            }
        }
    }
    private void GetFirmName()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFirmName(objUserBELDIST);

        //string[] param = { "@LIC_NO" };
        //object[] value = { Session["LIC_NO"].ToString() };
        //ds = dbsApp.param_passing_fetch("SELECT APP_FIRMNAME FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO = @LIC_NO", param, value);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_LicName.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
                lbl_LicName0.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
            }
        }
    }
    private void FillFarmerDtls()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = this.Session["Farmer_ID"].ToString();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFarmerInvHdr(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                td_Name.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString());
                td_FName.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHFATHERNAME"].ToString());
                td_MobNo.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHMOBILENO"].ToString());
                td_Village.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["villg_name"].ToString());
                td_GP.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["GP_Name"].ToString());
                td_Block.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BLOCK_NAME"].ToString());
                td_Dist.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["Dist_Name"].ToString());
            }
        }

        lblReceiver.Text = td_Name.InnerHtml;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void btnUCancel_Click(object sender, EventArgs e)
    {

    }
    protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {

    }
    protected void txtUNoofBags_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtFarmerId_TextChanged(object sender, EventArgs e)
    {
        txtFarmerId.Text = txtFarmerId.Text.Trim();
        gvStore.DataSource = null;
        gvStore.DataBind();
        if (txtFarmerId.Text == "0")
        {
            btnConfirm.Enabled = false;
            td_FarmerName.InnerHtml = "";
            td_FatherName.InnerHtml = "";
            td_Category.InnerHtml = "";
            td_Gender.InnerHtml = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", "alert('0 Farmer Id can not do any transaction.')", true);
        }
        else
        {
            string mStatus = "";

            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.FARMER_ID = lblPre.Text + txtFarmerId.Text;
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.GetFarmerStatus(objUserBELDIST);

            //string[] param = { "@FARMER_ID" };
            //object[] value = { lblPre.Text + txtFarmerId.Text };
            //ds = dbsApp.param_passing_fetch("SELECT B.STATUS FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] A " +
            //"LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_FarmerApprove] B ON A.VCHFARMERCODE = B.Farmer_Code " +
            //"WHERE A.NICFARMERID = @FARMER_ID ", param, value);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    mStatus = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["STATUS"].ToString());
                }
            }
            if (mStatus == "UIID")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", "alert('Your Adhar No is Mapped With a Valid Bank A/c. But Your Submitted Bank A/c Details During Registration is Invalid. Contact dbt help desk (9439874288) to Confirm Your Adhar No or Modify Your A/c Information.')", true);
                txtFarmerId.Focus();
            }
            else if (mStatus == "AAO" || mStatus == "AAO1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", "alert('Your Farmer Registration is InComplete, Although A/c Verification is Completed. Contact Your AAO To Complete the Approval of the Registration With out Which the Subsidy will Not be Released. Contact AAO for approval in food odisha portal')", true);
                txtFarmerId.Focus();
            }
            else
            {
                string VCHPACSCODE = "";
                btnConfirm.Enabled = false;
                td_FarmerName.InnerHtml = "";
                td_FatherName.InnerHtml = "";
                td_Category.InnerHtml = "";
                td_Gender.InnerHtml = "";
                //string[] param = { "@FARMER_ID" };
                //object[] value = { lblPre.Text + txtFarmerId.Text };

                //objUserBELDIST.FARMER_ID = lblPre.Text + txtFarmerId.Text;
                objUserDLLDIST = new DLL_Dealer();
                ds = new DataSet();
                ds = objUserDLLDIST.GetFarmerDetails(objUserBELDIST);

                //ds = dbsApp.param_passing_fetch("SELECT A.NICFARMERID,A.VCHFARMERNAME,A.VCHFATHERNAME,D.Category_Value,E.Gender_Value,VCHPACSCODE " +
                //"FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] A " +
                //"LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_FarmerApprove] B ON A.VCHFARMERCODE = B.Farmer_Code " +
                //"LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_Category] D ON A.INTCATEGOGY = D.Category_ID " +
                //"LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_Gender] E ON A.INTGENDER = E.Gender_ID " +
                //"WHERE A.NICFARMERID = @FARMER_ID AND B.Status IN ('ACCP','CCB') ", param, value);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        VCHPACSCODE = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHPACSCODE"].ToString());
                        //txtFarmerId.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["FARMER_ID"].ToString());                    
                        if (VCHPACSCODE == "1" && (mStatus == "AAO" || mStatus == "AAO1"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", "alert('Ask Your AAO To Approve Your Farmer Deatails')", true);
                            txtFarmerId.Focus();
                        }
                        else
                        {
                            btnChangeMobNo.Visible = true;
                            btnSendOTP.Visible = true;

                            hfFarmerId.Value= HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["NICFARMERID"].ToString());
                            td_FarmerName.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString());
                            td_FatherName.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHFATHERNAME"].ToString());
                            td_Category.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["Category_Value"].ToString());
                            td_Gender.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["Gender_Value"].ToString());
                            lblFarmerMobNo.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHMOBILENO"].ToString());
                            hfMobNo.Value = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHMOBILENO"].ToString());

                            objUserBELDIST = new BLL_Dealer();
                            objUserBELDIST.FARMER_ID = lblPre.Text + txtFarmerId.Text;
                            objUserBELDIST.FIN_YR = ddlFinYr.SelectedValue;
                            objUserBELDIST.SEASON = ddlSeassion.SelectedValue;
                            objUserDLLDIST = new DLL_Dealer();
                            ds1 = new DataSet();
                            ds1 = objUserDLLDIST.GetMobNoOTP(objUserBELDIST);
                            if (ds1 != null)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    lblFarmerMobNo.Text = HttpUtility.HtmlEncode(ds1.Tables[0].Rows[0]["VCHMOBNO"].ToString());
                                }
                            }

                            objUserDLLDIST = new DLL_Dealer();
                            ds1 = new DataSet();
                            ds1 = objUserDLLDIST.GetCropReceived(objUserBELDIST);

                            //string[] param1 = { "@FARMER_ID", "@FIN_YR", "@SEASON" };
                            //object[] value1 = { lblPre.Text + txtFarmerId.Text, ddlFinYr.SelectedValue, ddlSeassion.SelectedValue };
                            //ds1 = dbsApp.param_passing_fetch("SELECT B.Crop_Name,CONVERT(VARCHAR(50),SUM(TOT_QTL))+' Qtl'TOT_QTL FROM [STOCK].[DBO].[STOCK_FARMER] A LEFT OUTER JOIN [STOCK].[DBO].[mCrop] B ON A.CROP_ID = B.Crop_Code WHERE FARMER_ID = @FARMER_ID AND FIN_YEAR = @FIN_YR AND SEASON = @SEASON GROUP BY B.Crop_Name", param1, value1);
                            if (ds1 != null)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    gvStore.DataSource = ds1;
                                    gvStore.DataBind();
                                }
                            }

                            btnConfirm.Enabled = true;
                            FillFarmerPreBooking(hfFarmerId.Value);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", "alert('Please Enter A Valid Firmer ID')", true);
                        txtFarmerId.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", "alert('Please Enter A Valid Firmer ID')", true);
                    txtFarmerId.Focus();
                }
            }
        }
    }
    private void FillFarmerPreBooking(string Farmerid)
    {
        StringBuilder str = new StringBuilder("");
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserBELDIST.FARMER_ID = Farmerid;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetPrebookingFarmer(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {                
                str.Append("<div> Pre-Booking details of the farmer");
                str.Append("<table width='100%' border='1' cellpadding='5' cellspacing='0' class='table table-bordered'>");
                str.Append("<tr>");
                str.Append("<td><b>Crop Name</b></td>");
                str.Append("<td><b>Variety Name</b></td>");
                str.Append("<td><b>Qty(in qtl)</b></td>");
                str.Append("</tr>");
                int i = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str.Append("<tr>");
                    str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
                    str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
                    str.Append("<td>" + dr["QUANTITY"].ToString() + "</td>");
                    str.Append("</tr>");
                    i++;
                }
                str.Append("</table>");
                str.Append("</div>");

                
            }
        }
        litFarmer.Text = str.ToString();
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    mpeReport.Show();
    //}
    protected void rbtn_lot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow i in Grd_Sale.Rows)
            {
                RadioButton chk = (RadioButton)i.FindControl("rbtn_lot");
                TextBox txt_NoOfBag = (TextBox)i.FindControl("txt_NoOfBag");
                Button btn_Add = (Button)i.FindControl("btn_Add");

                if (chk.Checked == true)
                {
                    txt_NoOfBag.Enabled = true;
                    btn_Add.Enabled = true;

                    txt_NoOfBag.BackColor = System.Drawing.Color.LemonChiffon;
                    btn_Add.BackColor = System.Drawing.Color.SeaGreen;
                    txt_NoOfBag.Focus();
                }
                else
                {
                    txt_NoOfBag.Enabled = false;
                    btn_Add.Enabled = false;

                    txt_NoOfBag.BackColor = System.Drawing.Color.White;
                    btn_Add.BackColor = System.Drawing.Color.Gray;
                }
            }
        }
        catch (ApplicationException applicationException)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
        }
    }
    protected void txt_NoOfBag_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            Label lbl_BagSize = (Label)row.FindControl("lbl_BagSize");
            Label lbl_PricePerQtl = (Label)row.FindControl("lbl_PricePerQtl");
            Label lbl_AvlQtyBank = (Label)row.FindControl("lbl_AvlQtyBank");
            Label lbl_AvlQty = (Label)row.FindControl("lbl_AvlQty");
            TextBox txt_NoOfBag = (TextBox)row.FindControl("txt_NoOfBag");
            Label lbl_QtyQtl = (Label)row.FindControl("lbl_QtyQtl");
            Label lbl_Amt = (Label)row.FindControl("lbl_Amt");

            if (txt_NoOfBag.Text != string.Empty)
            {
                if (Convert.ToDouble(txt_NoOfBag.Text) > 0)
                {
                    if (Convert.ToDouble(lbl_AvlQtyBank.Text) < Convert.ToDouble(txt_NoOfBag.Text))
                    {
                        string pop = "alert('Insufficient Stock')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        txt_NoOfBag.Focus();
                        return;
                    }
                    lbl_QtyQtl.Text = (Convert.ToDouble(txt_NoOfBag.Text) * Convert.ToDouble(lbl_BagSize.Text) / 100).ToString();
                    lbl_Amt.Text = (Convert.ToDouble(lbl_PricePerQtl.Text) * Convert.ToDouble(lbl_QtyQtl.Text)).ToString();
                }
            }
            else
            {
                //string popp = "alert('Please Enter Number of Bags!!!')";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                txt_NoOfBag.Text = string.Empty;
                txt_NoOfBag.Focus();
            }

        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
        }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        RadioButton rbtn_lot = (RadioButton)btn.Parent.FindControl("rbtn_lot");
        Label lbl_LotNo = (Label)btn.Parent.FindControl("lbl_LotNo");
        Label lbl_SorceName = (Label)btn.Parent.FindControl("lbl_SorceName");
        Label lbl_SorceID = (Label)btn.Parent.FindControl("lbl_SorceID");
        Label lbl_BagSize = (Label)btn.Parent.FindControl("lbl_BagSize");

        Label lbl_PricePerQtl = (Label)btn.Parent.FindControl("lbl_PricePerQtl");
        Label lbl_SubsidyPerQtl = (Label)btn.Parent.FindControl("lbl_SubsidyPerQtl");
        Label lbl_AvlQty = (Label)btn.Parent.FindControl("lbl_AvlQty");

        TextBox txt_NoOfBag = (TextBox)btn.Parent.FindControl("txt_NoOfBag");
        Label lbl_QtyQtl = (Label)btn.Parent.FindControl("lbl_QtyQtl");
        Label lbl_Amt = (Label)btn.Parent.FindControl("lbl_Amt");
        Button btn_Add = (Button)btn.Parent.FindControl("btn_Add");

        double QUANTITY = 0;
        if (string.IsNullOrWhiteSpace(txt_NoOfBag.Text))
        {
            string pop = "alert('Please insert some value')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            txt_NoOfBag.Focus();
        }
        else
        {
            DataTable dt = (DataTable)Session["dt"];
            if (dt.Rows.Count > 0)
            {
                int COUNT = dt.Rows.Count;
                for (int r = 0; r < COUNT; ++r)
                {
                    DataRow row = dt.Rows[r];
                    if (row["CROP_ID"].ToString() == ddlCropName.SelectedValue && row["CROP_VERID"].ToString() == ddlVariety.SelectedValue && row["Receive_Unitcd"].ToString() == lbl_SorceID.Text && row["LOT_NO"].ToString() == lbl_LotNo.Text && row["BAG_SIZE_KG"].ToString() == lbl_BagSize.Text && row["PRICE_QTL"].ToString() == lbl_PricePerQtl.Text)
                    {
                        QUANTITY = Convert.ToDouble(row["QUANTITY"].ToString()) + Convert.ToDouble(lbl_QtyQtl.Text);
                        if (Convert.ToDouble(lbl_AvlQty.Text) < QUANTITY)
                        {
                            string pop = "alert('Insufficient Stock!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            lbl_QtyQtl.Text = "0";
                            lbl_Amt.Text = "0";
                            txt_NoOfBag.Text = "";
                            rbtn_lot.Checked = false;
                            txt_NoOfBag.Enabled = false;
                            btn_Add.Enabled = false;
                            txt_NoOfBag.BackColor = System.Drawing.Color.White;
                            btn_Add.BackColor = System.Drawing.Color.Gray;
                        }
                        else
                        {
                            row["NO_OF_BAGS"] = Convert.ToDouble(row["NO_OF_BAGS"]) + Convert.ToDouble(txt_NoOfBag.Text);
                            row["QUANTITY"] = QUANTITY;
                            row["AMOUNT"] = Convert.ToDouble(row["PRICE_QTL"]) * QUANTITY;
                            string pop = "alert('Data Updated Sucessfully!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            Session.Add("dt", dt);
                            lbl_QtyQtl.Text = "0";
                            lbl_Amt.Text = "0";
                            txt_NoOfBag.Text = "";
                            rbtn_lot.Checked = false;
                            txt_NoOfBag.Enabled = false;
                            btn_Add.Enabled = false;
                            FillGrid2();
                            txt_NoOfBag.BackColor = System.Drawing.Color.White;
                            btn_Add.BackColor = System.Drawing.Color.Gray;
                            return;
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(txt_NoOfBag.Text))
                {
                    string pop = "alert('Insufficient Stock')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    txt_NoOfBag.Focus();
                    return;
                }
                else
                {
                    QUANTITY = Convert.ToDouble(lbl_QtyQtl.Text);
                    if (Convert.ToDouble(lbl_AvlQty.Text) < QUANTITY)
                    {
                        string pop = "alert('Insufficient Stock!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        lbl_QtyQtl.Text = "0";
                        lbl_Amt.Text = "0";
                        txt_NoOfBag.Text = "";
                        rbtn_lot.Checked = false;
                        txt_NoOfBag.Enabled = false;
                        btn_Add.Enabled = false;
                        txt_NoOfBag.BackColor = System.Drawing.Color.White;
                        btn_Add.BackColor = System.Drawing.Color.Gray;
                    }
                    else
                    {
                        dt.Rows.Add(ddlCropName.SelectedValue, ddlCropName.SelectedItem.Text, ddlVariety.SelectedValue, ddlVariety.SelectedItem.Text, lbl_LotNo.Text, lbl_SorceID.Text, lbl_SorceName.Text, lbl_BagSize.Text, txt_NoOfBag.Text, lbl_QtyQtl.Text, lbl_PricePerQtl.Text, lbl_SubsidyPerQtl.Text, lbl_Amt.Text);
                        Session.Add("dt", dt);
                    }
                }
            }
            else
            {
                if (Convert.ToDouble(lbl_AvlQty.Text) < (Convert.ToDouble(lbl_BagSize.Text) * Convert.ToDouble(txt_NoOfBag.Text)) / 100)
                {
                    string pop = "alert('Insufficient Stock!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    lbl_QtyQtl.Text = "0";
                    lbl_Amt.Text = "0";
                    txt_NoOfBag.Text = "";
                    rbtn_lot.Checked = false;
                    txt_NoOfBag.Enabled = false;
                    btn_Add.Enabled = false;
                    txt_NoOfBag.BackColor = System.Drawing.Color.White;
                    btn_Add.BackColor = System.Drawing.Color.Gray;
                }
                else
                {
                    dt.Rows.Add(ddlCropName.SelectedValue, ddlCropName.SelectedItem.Text, ddlVariety.SelectedValue, ddlVariety.SelectedItem.Text, lbl_LotNo.Text, lbl_SorceID.Text, lbl_SorceName.Text, lbl_BagSize.Text, txt_NoOfBag.Text, lbl_QtyQtl.Text, lbl_PricePerQtl.Text, lbl_SubsidyPerQtl.Text, lbl_Amt.Text);
                }
            }
            Session.Add("dt", dt);
        }

        lbl_QtyQtl.Text = "0";
        lbl_Amt.Text = "0";
        txt_NoOfBag.Text = "";
        rbtn_lot.Checked = false;
        txt_NoOfBag.Enabled = false;
        btn_Add.Enabled = false;
        FillGrid2();
        txt_NoOfBag.BackColor = System.Drawing.Color.White;
        btn_Add.BackColor = System.Drawing.Color.Gray;

        string pop1 = "alert('Data Saved Sucessfully!!!')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop1, true);
    }
    private void FillGrid2()
    {
        DataTable dt = (DataTable)Session["dt"];

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                grd_lottrans.DataSource = dt;
                grd_lottrans.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                grd_lottrans.DataSource = null;
                grd_lottrans.DataBind();
                btnSave.Visible = false;
            }
        }
    }
    double TotQty = 0;
    double TotAmt = 0;
    protected void grd_lottrans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "QUANTITY"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotQty = (Label)e.Row.FindControl("lblTotQty");
            lblTotQty.Text = TotQty.ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotAmt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AMOUNT"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotAmt = (Label)e.Row.FindControl("lblTotAmt");
            lblTotAmt.Text = TotAmt.ToString();
        }
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Label lbl_Cat = (Label)btn.Parent.FindControl("lbl_Cat");
        Label lbl_Crop = (Label)btn.Parent.FindControl("lbl_Crop");
        Label lbl_Variety = (Label)btn.Parent.FindControl("lbl_Variety");
        Label lbl_Class = (Label)btn.Parent.FindControl("lbl_Class");
        Label lbl_Source = (Label)btn.Parent.FindControl("lbl_Source");
        Label lbl_BagSize = (Label)btn.Parent.FindControl("lbl_BagSize");
        Label lbl_BagReceive = (Label)btn.Parent.FindControl("lbl_BagReceive");
        Label lbl_LotNo = (Label)btn.Parent.FindControl("lbl_LotNo");

        DataTable dt = (DataTable)Session["dt"];
        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            DataRow dr = dt.Rows[i];
            if (dr["LOT_NO"].ToString() == lbl_LotNo.Text)
            {
                dr.Delete();
                Session.Add("dt", dt);
                break;
            }
        }

        FillGrid2();

        string pop = "alert('Item Deleted Sucessfully!!!')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SaleStock.aspx");
    }
    protected void ddlSeassion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCrops();
        FillVariety();
    }
    protected void ddlFinYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSeassion();
        FillCrops();
        FillVariety();
    }
    protected void Grd_Sale_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Receive_Unitname = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.RECEIVE_UNITCD = DataBinder.Eval(e.Row.DataItem, "RECEIVE_UNITCD").ToString();
            objUserBELDIST.SESSION = ddlSeassion.SelectedValue;
            objUserBELDIST.F_Year = ddlFinYr.SelectedValue;
            objUserDLLDIST = new DLL_Dealer();
            string PRICE_RECEIVE_UNITCD = objUserDLLDIST.GetPRICE_RECEIVE_UNITCD(objUserBELDIST);

            if (PRICE_RECEIVE_UNITCD == "0000")
            {
                Receive_Unitname = DataBinder.Eval(e.Row.DataItem, "Receive_Unitname").ToString();//"OSSC";//hfUserType.Value;
            }
            else
            {
                objUserBELDIST = new BLL_Dealer();
                objUserBELDIST.RECEIVE_UNITCD = PRICE_RECEIVE_UNITCD;
                objUserDLLDIST = new DLL_Dealer();
                Receive_Unitname = objUserDLLDIST.GetReceive_UnitnameByCode(objUserBELDIST);
            }
            Label lbl_SorceName = (Label)e.Row.FindControl("lbl_SorceName");
            lbl_SorceName.Text = Receive_Unitname;
        }
    }
    protected void grd_stockdtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Receive_Unitname = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.RECEIVE_UNITCD = DataBinder.Eval(e.Row.DataItem, "RECEIVE_UNITCD").ToString();
            objUserBELDIST.SESSION = ddlSeassion.SelectedValue;
            objUserBELDIST.F_Year = ddlFinYr.SelectedValue;
            objUserDLLDIST = new DLL_Dealer();
            string PRICE_RECEIVE_UNITCD = objUserDLLDIST.GetPRICE_RECEIVE_UNITCD(objUserBELDIST);

            if (PRICE_RECEIVE_UNITCD == "0000")
            {
                Receive_Unitname = DataBinder.Eval(e.Row.DataItem, "Receive_Unitname").ToString();//hfUserType.Value;
            }
            else
            {
                objUserBELDIST = new BLL_Dealer();
                objUserBELDIST.RECEIVE_UNITCD = PRICE_RECEIVE_UNITCD;
                objUserDLLDIST = new DLL_Dealer();
                Receive_Unitname = objUserDLLDIST.GetReceive_UnitnameByCode(objUserBELDIST);
            }
            Label lbl_Source1 = (Label)e.Row.FindControl("lbl_Source1");
            lbl_Source1.Text = Receive_Unitname;
        }
    }

    protected void btnChangeMobNo_Click(object sender, EventArgs e)
    {
        btnChangeMobNo.Visible = false;
        btnSendOTP.Visible = false;
        txtCMobNo.Visible = true;
        btnUpdateMobNo.Visible = true;
        btnUpdateMobNoCancel.Visible = true;
    }
    protected void btnUpdateMobNo_Click(object sender, EventArgs e)
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = hfFarmerId.Value;
        objUserBELDIST.Mob = txtCMobNo.Text;
        int CntFarmerMob = 0;
        objUserDLLDIST = new DLL_Dealer();
        CntFarmerMob = objUserDLLDIST.CountFarmerMob(objUserBELDIST);
        if(CntFarmerMob>10)
        {
            //string pop = "alert('Mobile Numner already exists with another farmer')";
            string pop = "alert('One Mobile Numner can be used for 10 farmers. You are trying for the 11th one.')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
        else
        {
            lblFarmerMobNo.Text = txtCMobNo.Text;
            btnChangeMobNo.Visible = true;
            btnSendOTP.Visible = true;
            txtCMobNo.Visible = false;
            btnUpdateMobNo.Visible = false;
            btnUpdateMobNoCancel.Visible = false;
        }        
    }
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string sRandomOTP = string.Empty;

        sRandomOTP = GenerateOtp(lblFarmerMobNo.Text.ToString());

        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.Mob = lblFarmerMobNo.Text.ToString();
        objUserBELDIST.Msg = "Your Transaction OTP for Seed Purchase is " + sRandomOTP + " . DAFP AGRIOR";
        objUserBELDIST.SMSMode = "T";
        objUserBELDIST.SType = "O";
        objUserBELDIST.TempId= "1107165150759207123";
        string ResponseText = objUserBELDIST.Send_OTP_SMS(objUserBELDIST);
        txtOTP.Visible = true;
        lblOTPMsg.Visible = true;
        btnConfirmOTP.Visible = true;
    }

    private string GenerateOtp(string mob)
    {
        string val = string.Empty;
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = hfFarmerId.Value;
        objUserBELDIST.Mob = lblFarmerMobNo.Text;
        objUserBELDIST.UPDATED_BY = Session["LIC_NO"].ToString();
        ds = new DataSet();
        objUserDLLDIST = new DLL_Dealer();
        ds = objUserDLLDIST.CheckOTP(objUserBELDIST);
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            val = ds.Tables[0].Rows[0]["OTP_NO"].ToString();
        }
        else
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            val = GenerateRandomOTP(6, saAllowedCharacters);
            
            objUserBELDIST.OTP = val;
            int ValCnt = 0;
            objUserDLLDIST = new DLL_Dealer();
            objUserDLLDIST.CreateOTP(objUserBELDIST, out ValCnt);

            if (ValCnt != 1)
            {
                val = "0";
            }
        }
        return val;
    }

    private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
    {
        string sOTP = String.Empty;
        string sTempChars = String.Empty;

        Random rand = new Random();

        for (int i = 0; i < iOTPLength; i++)
        {
            int p = rand.Next(0, saAllowedCharacters.Length);
            sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
            sOTP += sTempChars;
        }

        return sOTP;
    }
    protected void btnUpdateMobNoCancel_Click(object sender, EventArgs e)
    {
        btnChangeMobNo.Visible = true;
        btnSendOTP.Visible = true;
        txtCMobNo.Visible = false;
        btnUpdateMobNo.Visible = false;
        btnUpdateMobNoCancel.Visible = false;
    }
    protected void btnConfirmOTP_Click(object sender, EventArgs e)
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = hfFarmerId.Value;
        objUserBELDIST.OTP = txtOTP.Text;
        objUserBELDIST.UPDATED_BY = Session["LIC_NO"].ToString();
        int ValOTP = 0;
        objUserDLLDIST = new DLL_Dealer();
        objUserDLLDIST.ValidateOTP(objUserBELDIST, out ValOTP);
        txtOTP.Text = "";
        if(ValOTP==1)
        {
            pnlHeader.Enabled = false;
            pnlDetails.Visible = true;
        }
        else if(ValOTP==2)
        {
            string pop = "alert('Invalid OTP')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
        
    }
}
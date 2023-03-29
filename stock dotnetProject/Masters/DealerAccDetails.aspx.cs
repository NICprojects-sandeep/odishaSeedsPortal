using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_DealerAccDetails : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds;
    DataSet ds1;
    protected void Page_PreInit(object sender, EventArgs e)
    {

        if (Session["Name"] != null || Session["UserType"] != null)
        {

        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["LicNo"] != null && Request.QueryString["Name"] != string.Empty)
            {
                objUserBEL = new BLL_DropDown();
                objUserBEL.DistCode = Session["DistCode"].ToString();
                objUserBEL.LIC_NO = Request.QueryString["LicNo"];
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillRejectedDealers(objUserBEL);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    FillDetails();
                }
                //DataRow[] foundRows;
                //foundRows = ds.Tables[0].Select("LIC_NO = '" + objUserBEL.LIC_NO + "'");
            }
        }
    }

    private void FillDetails()
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.DistCode = Session["DistCode"].ToString();
        objUserBEL.LIC_NO = Request.QueryString["LicNo"];
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillRejectedDealers(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblLicNo.Text = ds.Tables[0].Rows[0]["LIC_NO"].ToString();
                lblFirmName.Text = ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString();
                lblDealerType.Text = ds.Tables[0].Rows[0]["APP_TYPE"].ToString();
                FillBanks();
                FillBranches();
                FillIFSC();
                //txtName.Text = ds.Tables[0].Rows[0]["ACC_HOLDERNAME"].ToString();
                //txtAccNo.Text = ds.Tables[0].Rows[0]["ACC_NO"].ToString();
                //ddlBankName.SelectedValue = ds.Tables[0].Rows[0]["BANK_ID"].ToString();
                //ddlBankName.SelectedValue = ds.Tables[0].Rows[0]["BRANCH_ID"].ToString();                
            }
        }
    }
    private void FillBanks()
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.DEPT_TYPE = lblDealerType.Text;
        objUserBEL.BANK_ID = (ddlBankName.SelectedValue == "--Select--" ? "0" : ddlBankName.SelectedValue);
        objUserBEL.BRANCH_ID = (ddlBranchName.SelectedValue == "--Select--" ? "0" : ddlBranchName.SelectedValue);
        objUserDLL = new DLL_DropDown();
        ds1 = new DataSet();
        ds1 = objUserDLL.FillBankBranch(objUserBEL);
        if (ds1 != null)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.ddlBankName.DataSource = ds1;
                this.ddlBankName.DataValueField = "intBankId";
                this.ddlBankName.DataTextField = "vchBankName";
                this.ddlBankName.DataBind();
            }
        }
        ddlBankName.Items.Insert(0, "--Select--");
    }
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranches();
    }   

    private void FillBranches()
    {
        ddlBranchName.Items.Clear();
        objUserBEL = new BLL_DropDown();
        objUserBEL.DEPT_TYPE = lblDealerType.Text;
        objUserBEL.BANK_ID = (ddlBankName.SelectedValue == "--Select--" ? "0" : ddlBankName.SelectedValue);
        objUserBEL.BRANCH_ID = (ddlBranchName.SelectedValue == "--Select--" ? "0" : ddlBranchName.SelectedValue);
        objUserDLL = new DLL_DropDown();
        ds1 = new DataSet();
        if (Convert.ToInt32(objUserBEL.BANK_ID) == 0)
        {
            ddlBranchName.Items.Clear();
            lblIFSC.Text = "";
        }
        else
        {
            ds1 = objUserDLL.FillBankBranch(objUserBEL);
            if (ds1 != null)
            {
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    this.ddlBranchName.DataSource = ds1.Tables[1];
                    this.ddlBranchName.DataValueField = "intBranchId";
                    this.ddlBranchName.DataTextField = "vchBranchName";
                    this.ddlBranchName.DataBind();
                }
            }
        }
        ddlBranchName.Items.Insert(0, "--Select--");
    }
    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillIFSC();
    }

    private void FillIFSC()
    {
        lblIFSC.Text = "";
        objUserBEL = new BLL_DropDown();
        objUserBEL.DEPT_TYPE = lblDealerType.Text;
        objUserBEL.BANK_ID = (ddlBankName.SelectedValue == "--Select--" ? "0" : ddlBankName.SelectedValue);
        objUserBEL.BRANCH_ID = (ddlBranchName.SelectedValue == "--Select--" ? "0" : ddlBranchName.SelectedValue);
        objUserDLL = new DLL_DropDown();
        ds1 = new DataSet();
        if (Convert.ToInt32(objUserBEL.BANK_ID) == 0 || Convert.ToInt32(objUserBEL.BRANCH_ID) == 0)
        {
            lblIFSC.Text = "";
        }
        else
        {
            ds1 = objUserDLL.FillBankBranch(objUserBEL);
            if (ds1 != null)
            {
                if (ds1.Tables[2].Rows.Count > 0)
                {
                    lblIFSC.Text = ds1.Tables[2].Rows[0]["IFSC_CODE"].ToString();
                }
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            string pop = "alert('Please enter Account Holder Name');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (string.IsNullOrWhiteSpace(txtAccNo.Text))
        {
            string pop = "alert('Please enter Account No');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (ddlBankName.SelectedIndex == 0)
        {
            string pop = "alert('Please select Bank Name');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (ddlBranchName.SelectedIndex == 0)
        {
            string pop = "alert('Please select Branch Name');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (string.IsNullOrWhiteSpace(lblIFSC.Text))
        {
            string pop = "alert('Please select Branch Name');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else
        {

            try
            {
                objUserBEL = new BLL_DropDown();
                objUserBEL.LICENCENO = lblLicNo.Text;
                objUserBEL.ACC_HOLDERNAME = txtName.Text;
                objUserBEL.ACC_NO = txtAccNo.Text;
                objUserBEL.BANK_ID = (ddlBankName.SelectedValue == "--Select--" ? "0" : ddlBankName.SelectedValue);
                objUserBEL.BRANCH_ID = (ddlBranchName.SelectedValue == "--Select--" ? "0" : ddlBranchName.SelectedValue);
                objUserBEL.IFSC_CODE = lblIFSC.Text.ToUpper();
                objUserBEL.UPDATED_BY = Session["UserID"].ToString();
                objUserDLL = new DLL_DropDown();
                int Val = objUserDLL.UpdatePACSBankDetails(objUserBEL);
                if (Val == 1)
                {                    
                    string popp = "alert('Your Requset Has Been Updated Successfully');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    Response.Redirect("UpdateRejected.aspx");
                }
                else
                {
                    string popp = "alert('Error occured');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                }
            }
            catch
            {
                string pop1 = "alert('Due to Some Error The Applicant's Information Couldn't be Entered');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
            }

        }
    }
}
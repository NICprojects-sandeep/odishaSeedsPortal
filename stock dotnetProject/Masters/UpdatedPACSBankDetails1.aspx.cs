using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_UpdatedPACSBankDetails : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    protected void Page_PreInit(object sender, EventArgs e)
    {

        if (Session["Name"] != null || Session["UserType"] != null)
        {
            if (this.Session["UserType"].ToString() == "ADMI")
            {
                this.Page.MasterPageFile = "../MasterPages/Admin.master";
                return;
            }
            if (this.Session["UserType"].ToString() == "SEAD")
            {
                this.Page.MasterPageFile = "../MasterPages/SeedAdmin.master";
                return;
            }
            if (this.Session["UserType"].ToString() == "FINA")
            {
                this.Page.MasterPageFile = "../MasterPages/Finance.master";
                return;
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
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
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "OSSC") && !(this.Session["UserType"].ToString() == "SEAD") && !(this.Session["UserType"].ToString() == "FINA") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                FillDist();
                if (this.Session["UserType"].ToString() == "OSSC")
                {
                    ddlDistrict.Enabled = false;                                    
                    ddlDistrict.Items.FindByText(Session["DistName"].ToString()).Selected = true;
                }
                FillOSSCDealers();
                FillBankName();
            }
        }
    }
    private void FillOSSCDealers()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = ddlDistrict.SelectedValue;
            //objUserBEL.UserType = rbtnlDealerType.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillUpdatedPACS(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvPACS.DataSource = ds;
                    gvPACS.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void FillDist()
    {
        try
        {
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillDistrict();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.ddlDistrict.DataSource = ds;
                    this.ddlDistrict.DataValueField = "Dist_Code";
                    this.ddlDistrict.DataTextField = "Dist_Name";
                    this.ddlDistrict.DataBind();
                }
            }
            ddlDistrict.Items.Insert(0, "--All--");
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOSSCDealers();
    }
    protected void gvPACS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPACS.Rows[rowIndex];

            string FarmName = (row.FindControl("lbl_FirmName") as Label).Text;
            string LicenceNo = (row.FindControl("lbl_LICNO") as Label).Text;
            lblLicNo.Text = LicenceNo;
            lblDealerName.Text = FarmName;
            lblDealer.Text = FarmName + "(" + LicenceNo + ")";
            objUserBEL = new BLL_DropDown();
            objUserBEL.LICENCENO = LicenceNo;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillDetailsPACS(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAadhaar.Text = ds.Tables[0].Rows[0]["AADHAAR_NO"].ToString();
                    txtAccHolderName.Text = ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString();
                    txtAccNo.Text = ds.Tables[0].Rows[0]["ACC_NO"].ToString();
                    ddlBankName.SelectedValue = ds.Tables[0].Rows[0]["BANK_ID"].ToString();
                    FillBranchName();
                    ddlBranchName.SelectedValue = ds.Tables[0].Rows[0]["BRANCH_ID"].ToString();
                    txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC_CODE"].ToString();
                }
            }
        }
        ModalPopUp.Show();
    }
    private void FillBankName()
    {
        DataSet dsBnk = dbsApp.returnDatasetFarmer("SELECT INTID,VCHBANKNAME FROM M_PDS_FARMERBANK WHERE INTDELETEDFLAG = 0 ORDER BY VCHBANKNAME");
        ddlBankName.DataSource = dsBnk;
        ddlBankName.DataTextField = "VCHBANKNAME";
        ddlBankName.DataValueField = "INTID";
        ddlBankName.DataBind();
        ddlBankName.Items.Insert(0, "--select--");
    }
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranchName();
        ModalPopUp.Show();
    }
    private void FillBranchName()
    {
        try
        {
            ddlBranchName.Items.Clear();
            txtIFSC.Text = "";
            string strBnk = "SELECT INTBRANCHID,(VCHBRANCHNAME+','+VCHDISTRICT +' (' + isnull(NEW_IFSC_CODE,'-')  +')') VCHBRANCHNAME  FROM M_PDS_BANKBRANCH WHERE INTBANKID=@BnkCode AND INTDELETEDFLAG=0 AND IFSC_CODE IS NOT NULL AND IFSC_CODE<>'' ORDER BY VCHBRANCHNAME";
            string[] paramBnk = { "@BnkCode" };
            object[] valueBnk = { ddlBankName.SelectedValue };
            DataSet dsBnkNm = dbsApp.param_passing_fetchFarmer(strBnk, paramBnk, valueBnk);
            if (dsBnkNm.Tables[0].Rows.Count > 0)
            {
                ddlBranchName.DataSource = dsBnkNm;
                ddlBranchName.DataTextField = "VCHBRANCHNAME";
                ddlBranchName.DataValueField = "INTBRANCHID";
                ddlBranchName.DataBind();
                ddlBranchName.Items.Insert(0, "--select--");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = "select NEW_IFSC_CODE from M_PDS_BANKBRANCH where intBranchId=@BnchId";
            string[] strParam = { "@BnchId" };
            object[] Value = { ddlBranchName.SelectedValue };
            DataSet dsIfcs = dbsApp.param_passing_fetchFarmer(str, strParam, Value);
            if (dsIfcs.Tables[0].Rows.Count > 0)
            {
                txtIFSC.Text = dsIfcs.Tables[0].Rows[0]["NEW_IFSC_CODE"].ToString();
            }
            ModalPopUp.Show();
        }
        catch (Exception ex)
        { }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAccHolderName.Text))
        {
            string pop = "alert('Please Enter Account Holder No. !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (string.IsNullOrEmpty(txtAccNo.Text))
        {
            string pop = "alert('Please Enter Account No. !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (ddlBankName.SelectedIndex == 0)
        {
            string pop = "alert('Please Select Bank Name !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (ddlBranchName.SelectedIndex == 0)
        {
            string pop = "alert('Please Select Branch Name !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }

        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.LICENCENO = lblLicNo.Text;
            objUserBEL.AADHAAR_NO = txtAadhaar.Text;
            objUserBEL.ACC_HOLDERNAME = txtAccHolderName.Text;
            objUserBEL.ACC_NO = txtAccNo.Text;
            objUserBEL.BANK_ID = ddlBankName.SelectedValue;
            objUserBEL.BRANCH_ID = ddlBranchName.SelectedValue;
            objUserBEL.IFSC_CODE = txtIFSC.Text;
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserDLL = new DLL_DropDown();
            int Val = objUserDLL.UpdateDealerBankDetails(objUserBEL);
            if (Val == 1)
            {
                FillDist();
                FillOSSCDealers();
                FillBankName();
                string popp = "alert('Your Requset Has Been Updated Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }
            else
            {
                string popp = "alert('Provided Data Exists.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }
        }
        catch
        {
            string pop1 = "alert('Due to Some Error The Applicant's Information Couldn't be Entered');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ModalPopUp.Hide();
    }
    //protected void rbtnlDealerType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillOSSCDealers();
    //}
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_UpdatePACSBankDetails : System.Web.UI.Page
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
                    //this.Session["DistCode"].ToString();                    
                    ddlDistrict.Items.FindByText(Session["DistName"].ToString()).Selected = true;


                }
                FillOSSCDealers();
                lblBankId.Text = "";
                lblBankName.Text = "";
                lblBranchId.Text = "";
                lblBranchName.Text = "";
            }
        }
    }

    private void FillOSSCDealers()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = ddlDistrict.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillPACS(objUserBEL);
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
        }
        ModalPopUp.Show();
    }
    //private void FillBankName()
    //{
    //    DataSet dsBnk = dbsApp.returnDatasetFarmer("SELECT INTID,VCHBANKNAME FROM M_PDS_FARMERBANK WHERE INTDELETEDFLAG=0 ORDER BY VCHBANKNAME");
    //    ddlBankName.DataSource = dsBnk;
    //    ddlBankName.DataTextField = "VCHBANKNAME";
    //    ddlBankName.DataValueField = "INTID";
    //    ddlBankName.DataBind();
    //    ddlBankName.Items.Insert(0, "--select--");
    //}
    //protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillBranchName();
    //    ModalPopUp.Show();
    //}
    //private void FillBranchName()
    //{
    //    try
    //    {
    //        ddlBranchName.Items.Clear();
    //        txtIFSC.Text = "";
    //        string strBnk = "SELECT INTBRANCHID,(VCHBRANCHNAME+','+VCHDISTRICT +' (' + isnull(IFSC_CODE,'-')  +')') VCHBRANCHNAME  FROM M_PDS_BANKBRANCH WHERE INTBANKID=@BnkCode AND INTDELETEDFLAG=0 AND IFSC_CODE IS NOT NULL AND IFSC_CODE<>'' ORDER BY VCHBRANCHNAME";
    //        string[] paramBnk = { "@BnkCode" };
    //        object[] valueBnk = { ddlBankName.SelectedValue };
    //        DataSet dsBnkNm = dbsApp.param_passing_fetchFarmer(strBnk, paramBnk, valueBnk);
    //        if (dsBnkNm.Tables[0].Rows.Count > 0)
    //        {
    //            ddlBranchName.DataSource = dsBnkNm;
    //            ddlBranchName.DataTextField = "VCHBRANCHNAME";
    //            ddlBranchName.DataValueField = "INTBRANCHID";
    //            ddlBranchName.DataBind();
    //            ddlBranchName.Items.Insert(0, "--select--");
    //        }
    //    }
    //    catch (Exception ex)
    //    { }
    //}
    //protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    //{        
    //    try
    //    {
    //        string str = "select IFSC_CODE from M_PDS_BANKBRANCH where intBranchId=@BnchId";
    //        string[] strParam = { "@BnchId" };
    //        object[] Value = { ddlBranchName.SelectedValue };
    //        DataSet dsIfcs = dbsApp.param_passing_fetchFarmer(str, strParam, Value);
    //        if (dsIfcs.Tables[0].Rows.Count > 0)
    //        {
    //            txtIFSC.Text = dsIfcs.Tables[0].Rows[0]["IFSC_CODE"].ToString();
    //        }
    //        ModalPopUp.Show();
    //    }
    //    catch (Exception ex)
    //    { }
    //}
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
        else if (string.IsNullOrEmpty(lblBankId.Text))
        {
            string pop = "alert('Please Enter Valid IFSC !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else if (string.IsNullOrEmpty(lblBranchId.Text))
        {
            string pop = "alert('Please Enter Valid IFSC !!!');";
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
            objUserBEL.BANK_ID = lblBankId.Text;
            objUserBEL.BRANCH_ID = lblBranchId.Text;
            objUserBEL.IFSC_CODE = txtIFSC.Text.ToUpper();
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserDLL = new DLL_DropDown();
            int Val = objUserDLL.UpdateDealerBankDetails(objUserBEL);
            if (Val == 1)
            {                
                FillOSSCDealers();
                txtAadhaar.Text = "";
                txtAccHolderName.Text = "";
                txtAccNo.Text = "";
                txtIFSC.Text = "";
                lblBankId.Text = "";
                lblBankName.Text = "";
                lblBranchId.Text = "";
                lblBranchName.Text = "";
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
    protected void txtIFSC_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtIFSC.Text)) 
        {
            string pop = "alert('Please Enter Correct IFSC');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;
        }
        else
        {
            string strBnk = "SELECT INTBANKID,INTBRANCHID,B.VCHBANKNAME,VCHBRANCHNAME FROM M_PDS_BANKBRANCH A " +
            "INNER JOIN M_PDS_FARMERBANK B ON A.INTBANKID = B.INTID " +
            "WHERE IFSC_CODE = @IFSC_CODE OR NEW_IFSC_CODE = @IFSC_CODE OR NEW_IFSC_CODE_FM = @IFSC_CODE AND A.INTDELETEDFLAG = 0 AND IFSC_CODE IS NOT NULL AND IFSC_CODE<>''";
            string[] paramBnk = { "@IFSC_CODE" };
            object[] valueBnk = { txtIFSC.Text };
            DataSet dsBnkNm = dbsApp.param_passing_fetchFarmer(strBnk, paramBnk, valueBnk);
            if (dsBnkNm.Tables[0].Rows.Count > 0)
            {
                lblBankId.Text = dsBnkNm.Tables[0].Rows[0]["INTBANKID"].ToString();
                lblBankName.Text = dsBnkNm.Tables[0].Rows[0]["VCHBANKNAME"].ToString();
                lblBranchId.Text = dsBnkNm.Tables[0].Rows[0]["INTBRANCHID"].ToString();
                lblBranchName.Text = dsBnkNm.Tables[0].Rows[0]["VCHBRANCHNAME"].ToString();
            }
            else
            {
                string pop = "alert('Please Enter Correct IFSC');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                return;
            }
        }
        ModalPopUp.Show();
    }
}
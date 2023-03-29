using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dealer_UpdateAccDtls : System.Web.UI.Page
{
    DataSet ds;
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            string AccessMode = "";
            tbl1.Visible = false;
            tbl2.Visible = false;
            //pnlBankInfo.Visible = false;
            //pnlBankInfo1.Visible = false;
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.GetDealerInfo(objUserBELDIST);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tdlicno.InnerText = ds.Tables[0].Rows[0]["LIC_NO1"].ToString();
                    tdFirmName.InnerText = ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString();
                    tdContactNo.InnerText = ds.Tables[0].Rows[0]["APPMOB_NO"].ToString();
                    tdLicValidity.InnerText = ds.Tables[0].Rows[0]["APR_UPTO"].ToString();

                    if (ds.Tables[0].Rows[0]["ACCESS_MODE"].ToString() == "W")
                    {
                        AccessMode = "Web";
                    }
                    else
                    {
                        AccessMode = "Mobile";
                    }
                    tdAccessMode.InnerText = ds.Tables[0].Rows[0]["ACCESS_MODE"].ToString();

                    //tdAadhaar.InnerText = ds.Tables[0].Rows[0]["AADHAAR_NO"].ToString();
                    //tdAccName.InnerText = ds.Tables[0].Rows[0]["ACC_HOLDERNAME"].ToString();
                    //tdAccNo.InnerText = ds.Tables[0].Rows[0]["ACC_NO"].ToString();
                    //tdBankName.InnerText = ds.Tables[0].Rows[0]["vchBankName"].ToString();
                    //tdBranchName.InnerText = ds.Tables[0].Rows[0]["vchBranchName"].ToString();
                    //tdIFSC.InnerText = ds.Tables[0].Rows[0]["IFSC_CODE"].ToString();
                }
            }
            FillBanks();

            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            int Cnt = objUserDLLDIST.CntLic(objUserBELDIST);

            if (Cnt > 0)
            {
                tbl1.Visible = true;
            }
            else
            {
                tbl2.Visible = true;
                FillBankDetails();
            }
        }
    }

    private void FillBankDetails()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetDealerInfo(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["Aadhaar_No"] = ds.Tables[0].Rows[0]["AADHAAR_NO"].ToString();
                //tdAadhaar.InnerText = ds.Tables[0].Rows[0]["AADHAAR_NO"].ToString();
                tdAccName.InnerText = ds.Tables[0].Rows[0]["ACC_HOLDERNAME"].ToString();
                tdAccNo.InnerText = ds.Tables[0].Rows[0]["ACC_NO"].ToString();
                tdBankName.InnerText = ds.Tables[0].Rows[0]["vchBankName"].ToString();
                tdBranchName.InnerText = ds.Tables[0].Rows[0]["vchBranchName"].ToString();
                tdIFSC.InnerText = ds.Tables[0].Rows[0]["IFSC_CODE"].ToString();
            }
        }
    }
    private void FillBanks()
    {
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.FillBank();

        //DataSet dsBnk = dbsApp.returnDatasetFarmer("SELECT INTID,VCHBANKNAME FROM M_PDS_FARMERBANK WHERE INTDELETEDFLAG=0 ORDER BY VCHBANKNAME");
        ddlBankName.DataSource = ds;
        ddlBankName.DataTextField = "VCHBANKNAME";
        ddlBankName.DataValueField = "INTID";
        ddlBankName.DataBind();
        ddlBankName.Items.Insert(0, "--select--");
    }

    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranchName();
    }
    private void FillBranchName()
    {
        try
        {
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.BANK_CODE = ddlBankName.SelectedValue;
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.FillBranchByBankCode(objUserBELDIST);
            //string strBnk = "SELECT INTBRANCHID,(VCHBRANCHNAME+','+VCHDISTRICT +' (' + isnull(NEW_IFSC_CODE,'-')  +')') VCHBRANCHNAME  FROM M_PDS_BANKBRANCH WHERE INTBANKID=@BnkCode AND INTDELETEDFLAG=0 AND IFSC_CODE IS NOT NULL AND IFSC_CODE<>'' ORDER BY VCHBRANCHNAME";
            //string[] paramBnk = { "@BnkCode" };
            //object[] valueBnk = { ddlBankName.SelectedValue };
            //DataSet dsBnkNm = dbsApp.param_passing_fetchFarmer(strBnk, paramBnk, valueBnk);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranchName.DataSource = ds;
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
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.BRANCH_CODE = ddlBranchName.SelectedValue;
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.FillIFSC(objUserBELDIST);

            //string str = "select NEW_IFSC_CODE from M_PDS_BANKBRANCH where intBranchId=@BnchId";
            //string[] strParam = { "@BnchId" };
            //object[] Value = { ddlBranchName.SelectedValue };
            //DataSet dsIfcs = dbsApp.param_passing_fetchFarmer(str, strParam, Value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtIFSC.Text = ds.Tables[0].Rows[0]["NEW_IFSC_CODE"].ToString();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (RegAadhaar.IsMatch(txtAadhaar.Text))
        //{

        //}
        //else
        //{
        //    string popp = "alert('Provide Aadhaar Number.');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
        //    txtAadhaar.BorderColor = Color.Red;
        //    ScriptManager.GetCurrent(Page).SetFocus(txtAadhaar);
        //    return;
        //}

        //if (string.IsNullOrEmpty(txtAadhaar.Text))
        //{
        //    string pop = "alert('Please Enter Aadhaar No !!!');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        //    return;
        //}
        //else 
        if (string.IsNullOrEmpty(txtAccHolderName.Text))
        {
            string pop = "alert('Please Enter Account Holder Name !!!');";
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
            string AadhaarNo="";
           if( Session["Aadhaar_No"] != null)
            {
                AadhaarNo = Session["Aadhaar_No"].ToString();
            }
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.LICENCENO = tdlicno.InnerText;
            objUserBELDIST.AADHAAR_NO = AadhaarNo;
            objUserBELDIST.ACC_HOLDERNAME = txtAccHolderName.Text;
            objUserBELDIST.ACC_NO = txtAccNo.Text;
            objUserBELDIST.BANK_CODE = ddlBankName.SelectedValue;
            objUserBELDIST.BRANCH_CODE = ddlBranchName.SelectedValue;
            objUserBELDIST.IFSC_CODE = txtIFSC.Text;
            objUserBELDIST.UPDATED_BY = tdlicno.InnerText;
            objUserDLLDIST = new DLL_Dealer();
            int Val = objUserDLLDIST.UpdateDealerBankDetails(objUserBELDIST);
            if (Val == 1)
            {
                FillBankDetails();
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
}
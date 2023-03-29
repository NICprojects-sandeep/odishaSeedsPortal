using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_UpdateRejected : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    protected void Page_PreInit(object sender, EventArgs e)
    {

        if (Session["Name"] != null || Session["UserType"] != null)
        {
            //if (this.Session["UserType"].ToString() == "OSSC")
            //{
            //    this.Page.MasterPageFile = "../MasterPages/Admin.master";
            //    return;
            //}            
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
            //btnEdit.Visible = true;
            //btnUpdate.Visible = false;
            //btnCancel.Visible = false;

            //txtLicNo.Enabled = false;
            //txtFirmName.Enabled = false;
            //txtAadhaar.Enabled = false;
            //txtAcName.Enabled = false;
            //txtAcNo.Enabled = false;
            //txtIFSC.Enabled = false;
            //txtBankName.Enabled = false;
            //txtBranchName.Enabled = false;

            FillDealers();
            //ddlDealer.DataSource = ds;
            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlDealer.DataValueField = "LIC_NO";
            //        ddlDealer.DataTextField = "LIC_NO";
            //        ddlDealer.DataBind();
            //        ddlDealer.Items.Insert(0, "--- All ---");
            //    }
            //}
        }
    }

    private void FillDealers()
    {
        ds = new DataSet();
        objUserBEL = new BLL_DropDown();
        objUserBEL.DistCode = Session["DistCode"].ToString();
        objUserDLL = new DLL_DropDown();
        ds = objUserDLL.FillRejectedDealers(objUserBEL);
        StringBuilder str = new StringBuilder("");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                str.Append("<table class='table table-bordered'>");
                str.Append("<thead>");
                str.Append("<tr>");
                str.Append("<th>Licence Number</th>");
                str.Append("<th>Firm Name</th>");
                str.Append("<th>Dealer / PACS</th>");
                str.Append("<th>Reject Reason</th>");
                str.Append("<th>Action</th>");
                str.Append("</tr>");
                str.Append("</thead>");
                str.Append("<tbody>");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str.Append("<tr>");
                    str.Append("<td>" + dr["LIC_NO"].ToString() + "</td>");
                    str.Append("<td>" + dr["APP_FIRMNAME"].ToString() + "</td>");
                    str.Append("<td>" + dr["APP_TYPE"].ToString() + "</td>");
                    str.Append("<td>" + dr["RJCT_REASON"].ToString() + "</td>");
                    str.Append("<td><a href='DealerAccDetails.aspx?LicNo=" + dr["LIC_NO"].ToString() + "'>Edit</td>");
                    str.Append("</tr>");
                }
                str.Append("</tbody>");
                str.Append("</table>");
            }
        }
        litReport.Text = str.ToString();
    }
    
    //public DataSet FillDealers()
    //{
        //SqlConnection con = new SqlConnection(_connstrSeed);

        //SqlCommand cmd = new SqlCommand("SHOW_DEALER", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //try
        //{
        //    cmd.Parameters.AddWithValue("@DIST_CODE", Session["DistCode"].ToString());
        //    //cmd.Parameters.AddWithValue("@LIC_NO", Session["DistCode"].ToString());
        //    SqlDataAdapter ada = new SqlDataAdapter(cmd);
        //    ds = new DataSet();
        //    ada.Fill(ds);
        //    return ds;
        //}
        //catch (Exception exception)
        //{
        //    ExceptionHandler.WriteEx(exception);
        //    return null;
        //}
        //finally
        //{
        //    cmd.Dispose();
        //    if (con.State != ConnectionState.Closed)
        //        con.Close();
        //    con.Dispose();
        //}
    //}
    //protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btnEdit.Visible = true;
    //    btnUpdate.Visible = false;
    //    btnCancel.Visible = false;

    //    txtLicNo.Enabled = false;
    //    txtFirmName.Enabled = false;
    //    txtAadhaar.Enabled = false;
    //    txtAcName.Enabled = false;
    //    txtAcNo.Enabled = false;
    //    txtIFSC.Enabled = false;
    //    txtBankName.Enabled = false;
    //    txtBranchName.Enabled = false;
    //    ds = FillDealerDetails();
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtLicNo.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["LIC_NO"].ToString());
    //            txtFirmName.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
    //            txtAadhaar.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["AADHAAR_NO"].ToString());
    //            txtAcName.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["ACC_HOLDERNAME"].ToString());
    //            txtAcNo.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["ACC_NO"].ToString());
    //            txtIFSC.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["IFSC_CODE"].ToString());
    //            txtBankName.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BANK_NAME"].ToString());
    //            txtBranchName.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString()); 
    //            lblError.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["RJCT_REASON"].ToString());
    //            ddlAppType.SelectedItem.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_TYPE"].ToString());
    //        }
    //    }
    //}    
    //public DataSet FillDealerDetails()
    //{
    //    SqlConnection con = new SqlConnection(_connstrSeed);

    //    SqlCommand cmd = new SqlCommand("SHOW_DEALER", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@LIC_NO", ddlDealer.SelectedValue.ToString());
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}    

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    if (ddlDealer.SelectedIndex == 0)
    //    {

    //    }
    //    else
    //    {
    //        btnEdit.Visible = false;
    //        btnUpdate.Visible = true;
    //        btnCancel.Visible = true;

    //        txtLicNo.Enabled = true;
    //        txtFirmName.Enabled = true;
    //        txtAadhaar.Enabled = true;
    //        txtAcName.Enabled = true;
    //        txtAcNo.Enabled = true;
    //        txtIFSC.Enabled = true;
    //        txtBankName.Enabled = false;
    //        txtBranchName.Enabled =false;
    //    }
    //}
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrWhiteSpace(txtBankName.Text))
    //    {
    //        string pop = "alert('Please Enter Correct IFSC');";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
    //        return;
    //    }
    //    if (string.IsNullOrWhiteSpace(txtBranchName.Text))
    //    {
    //        string pop = "alert('Please Enter Correct IFSC');";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
    //        return;
    //    }
    //    SqlConnection con = new SqlConnection(_connstrSeed);

    //    SqlCommand cmd = new SqlCommand("sp_UPDBANKDETAILS", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@LIC_NO", txtLicNo.Text);
    //        cmd.Parameters.AddWithValue("@APP_TYPE_AFT", ddlAppType.SelectedValue);
    //        cmd.Parameters.AddWithValue("@AADHAAR_NO", txtAadhaar.Text);
    //        cmd.Parameters.AddWithValue("@ACC_HOLDERNAME", txtAcName.Text);
    //        cmd.Parameters.AddWithValue("@ACC_NO", txtAcNo.Text);
    //        cmd.Parameters.AddWithValue("@BANK_ID", lblBankId.Text);
    //        cmd.Parameters.AddWithValue("@BRANCH_ID", lblBranchId.Text);
    //        cmd.Parameters.AddWithValue("@IFSC_CODE", txtIFSC.Text);
    //        cmd.Parameters.AddWithValue("@BANK_UPDATED_BY", Session["UserID"].ToString());
    //        cmd.Parameters.Add("@VAL", SqlDbType.Int);
    //        cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);            
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);            
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }

    //    ds = FillDealers();
    //    ddlDealer.DataSource = ds;
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDealer.DataValueField = "LIC_NO";
    //            ddlDealer.DataTextField = "LIC_NO";
    //            ddlDealer.DataBind();
    //            ddlDealer.Items.Insert(0, "--- All ---");
    //        }
    //    }

    //    btnEdit.Visible = true;
    //    btnUpdate.Visible = false;
    //    btnCancel.Visible = false;

    //    txtLicNo.Enabled = false;
    //    txtFirmName.Enabled = false;
    //    txtAadhaar.Enabled = false;
    //    txtAcName.Enabled = false;
    //    txtAcNo.Enabled = false;
    //    txtIFSC.Enabled = false;
    //    txtBankName.Enabled = false;
    //    txtBranchName.Enabled = false;

    //    txtLicNo.Text = "";
    //    txtFirmName.Text = "";
    //    txtAadhaar.Text = "";
    //    txtAcName.Text = "";
    //    txtAcNo.Text = "";
    //    txtIFSC.Text = "";
    //    txtBankName.Text = "";
    //    txtBranchName.Text = "";
    //}
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    btnEdit.Visible = true;
    //    btnUpdate.Visible = false;
    //    btnCancel.Visible = false;

    //    txtLicNo.Enabled = false;
    //    txtFirmName.Enabled = false;
    //    txtAadhaar.Enabled = false;
    //    txtAcName.Enabled = false;
    //    txtAcNo.Enabled = false;
    //    txtIFSC.Enabled = false;
    //    txtBankName.Enabled = false;
    //    txtBranchName.Enabled = false;

    //    txtLicNo.Text = "";
    //    txtFirmName.Text = "";
    //    txtAadhaar.Text = "";
    //    txtAcName.Text = "";
    //    txtAcNo.Text = "";
    //    txtIFSC.Text = "";
    //    txtBankName.Text = "";
    //    txtBranchName.Text = "";

    //    ds = FillDealers();
    //    ddlDealer.DataSource = ds;
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDealer.DataValueField = "LIC_NO";
    //            ddlDealer.DataTextField = "LIC_NO";
    //            ddlDealer.DataBind();
    //            ddlDealer.Items.Insert(0, "--- All ---");
    //        }
    //    }
    //}
    //protected void txtIFSC_TextChanged(object sender, EventArgs e)
    //{
    //    //select * from [dbo].[M_PDS_FARMERBANK] where intDeletedFlag=0 and (vchBankName like '%CCB%' or vchBankName like '%CENTRAL COOP%' or vchBankName like '%OSCB%' or vchBankName like '%CCC%')
    //    lblBankId.Text = "";
    //    txtBankName.Text = "";
    //    lblBranchId.Text = "";
    //    txtBranchName.Text = "";

    //    if (string.IsNullOrEmpty(txtIFSC.Text))
    //    {
    //        string pop = "alert('Please Enter Correct IFSC');";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
    //        return;
    //    }
    //    else
    //    {
    //        string strBnk = "";
    //        if (ddlAppType.SelectedValue == "Dealer")
    //        {
    //            strBnk = "SELECT A.INTBANKID,A.INTBRANCHID,B.VCHBANKNAME,A.VCHBRANCHNAME FROM M_PDS_BANKBRANCH A " +
    //           "INNER JOIN M_PDS_FARMERBANK B ON A.INTBANKID = B.INTID " +
    //           "WHERE A.IFSC_CODE = @IFSC_CODE OR A.NEW_IFSC_CODE = @IFSC_CODE OR A.NEW_IFSC_CODE_FM = @IFSC_CODE AND A.INTDELETEDFLAG = 0 AND A.IFSC_CODE IS NOT NULL AND A.IFSC_CODE<>''";
    //        }
    //        else if (ddlAppType.SelectedValue == "PACS")
    //        {
    //            strBnk = "SELECT A.INTBANKID,A.INTBRANCHID,B.VCHBANKNAME,A.VCHBRANCHNAME FROM M_PDS_BANKBRANCH A " +
    //           "INNER JOIN M_PDS_FARMERBANK B ON A.INTBANKID = B.INTID " +
    //           "WHERE A.IFSC_CODE = @IFSC_CODE OR A.NEW_IFSC_CODE = @IFSC_CODE OR A.NEW_IFSC_CODE_FM = @IFSC_CODE AND A.INTDELETEDFLAG = 0 AND A.IFSC_CODE IS NOT NULL AND A.IFSC_CODE<>'' AND A.INTBANKID collate Latin1_General_CI_AI in(select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%')";
    //        }
    //        else
    //        {
    //            string pop = "alert('Please Enter Correct IFSC Code');";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
    //            return;
    //        }
    //        string[] paramBnk = { "@IFSC_CODE" };
    //        object[] valueBnk = { txtIFSC.Text };
    //        DataSet dsBnkNm = dbsApp.param_passing_fetchFarmer(strBnk, paramBnk, valueBnk);
    //        if (dsBnkNm != null)
    //        {
    //            if (dsBnkNm.Tables[0].Rows.Count > 0)
    //            {
    //                lblBankId.Text = dsBnkNm.Tables[0].Rows[0]["INTBANKID"].ToString();
    //                txtBankName.Text = dsBnkNm.Tables[0].Rows[0]["VCHBANKNAME"].ToString();
    //                lblBranchId.Text = dsBnkNm.Tables[0].Rows[0]["INTBRANCHID"].ToString();
    //                txtBranchName.Text = dsBnkNm.Tables[0].Rows[0]["VCHBRANCHNAME"].ToString();
    //            }
    //            else
    //            {
    //                string pop = "alert('Please Enter Correct IFSC');";
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
    //                return;
    //            }
    //        }
    //    }
    //}
    //protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //}
}
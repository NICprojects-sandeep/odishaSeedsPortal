using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Masters_ReturnToOSSC : System.Web.UI.Page
{
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            trDtls.Visible = false;
            btnCancel.Enabled = false;
            txtBags.Enabled = false;
            txtBags.Text = "0";
            lblSALETRANSID1.Text = "";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillSearchDtls();
    }
    private void FillSearchDtls()
    {
        trDtls.Visible = false;
        //string SUPPLY_TYPE = "";
        //string SUPPLY_NAME = "";
        objUserBEL = new BLL_DropDown();
        objUserBEL.LIC_NO = txtLicNo.Text;
        objUserBEL.UPDATED_BY = Session["UserID"].ToString();
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillDealerDtls(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                trDtls.Visible = true;
                td_APP_FIRMNAME.Visible = true;
                td_APP_FIRMNAME.InnerText = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());                
                gvLotDtls.DataSource = ds;
                gvLotDtls.DataBind();
            }
            else
            {
                string popp = "alert('Please Enter a Valid Licence No!!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                gvLotDtls.DataSource = null;
                gvLotDtls.DataBind();
                td_APP_FIRMNAME.InnerText = "";
                txtLicNo.Text = "";
                txtLicNo.Focus();
            }
        }
        else
        {
            string popp = "alert('Please Enter a Valid Licence No!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            gvLotDtls.DataSource = null;
            gvLotDtls.DataBind();
            td_APP_FIRMNAME.InnerText = "";
            txtLicNo.Text = "";
            txtLicNo.Focus();
        }
        //lblReceiver.Text = lblReceiverType.Text = SUPPLY_NAME;
        //lblSeller.Text = lblSellerType.Text = SUPPLY_TYPE;
    }
    protected void lbtnLOT_NUMBER_Click(object sender, EventArgs e)
    {
        btnCancel.Enabled = false;
        txtBags.Enabled = false;
        txtBags.Text = "0";
        lblError.Text = "";
        lblSALETRANSID1.Text = "";
        LinkButton btnEdit = (LinkButton)sender;
        GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
        Label lblLOT_NUMBER = (Label)Grow.FindControl("lblLOT_NUMBER");
        Label lblLIC_NO = (Label)Grow.FindControl("lblLIC_NO");
        Label lblCrop = (Label)Grow.FindControl("lblCrop");
        Label lblVariety = (Label)Grow.FindControl("lblVariety");
        Label lblAvlQty = (Label)Grow.FindControl("lblAvlQty");

        lblLotNo1.Text = lblLOT_NUMBER.Text;
        td_Crop.InnerHtml = lblCrop.Text;
        td_Variety.InnerHtml = lblVariety.Text;
        td_LotNo.InnerHtml = lblLOT_NUMBER.Text;
        lblAvlQnty.Text = lblAvlQty.Text;
        
        //td_SALE_DATE.InnerHtml = HttpUtility.HtmlEncode(tdSALE_DATE.InnerHtml);
        //td_APP_FIRMNAME.InnerHtml = HttpUtility.HtmlEncode(tdAPP_FIRMNAME.InnerHtml);
        //td_GODOWN_NAME.InnerHtml = HttpUtility.HtmlEncode(tdGODOWN_NAME.InnerHtml);
        //td_LOTNO.InnerHtml = HttpUtility.HtmlEncode(tdUPDATED_BY.InnerHtml);

        lblError.Visible = false;
        btnCancel.Enabled = true;

        objUserBEL = new BLL_DropDown();
        objUserBEL.LOT_NO = lblLOT_NUMBER.Text;
        objUserBEL.LIC_NO = lblLIC_NO.Text;
        objUserBEL.UPDATED_BY = Session["UserID"].ToString();
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillLotTransDtls(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCashmemoDtls.DataSource = ds;
                gvCashmemoDtls.DataBind();                
            }
        }

        //if (Convert.ToInt32(tdNoofBags1.InnerHtml) < Convert.ToInt32(tdAvlNoofBags3.InnerHtml))
        //{
        //    lblError.Text = "Due to insufficient " + lblReceiver.Text + " Stock the transaction can't be deleted";
        //    lblError.Visible = true;
        //    btnCancel.Enabled = false;
        //}
        this.ModalPopUp.Show();
    }
    protected void rbtnCashMemo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            btnCancel.Enabled = false;
            txtBags.Enabled = false;
            txtBags.Text = "0";
            lblSALETRANSID1.Text = "";

            foreach (GridViewRow i in gvCashmemoDtls.Rows)
            {
                RadioButton chk = (RadioButton)i.FindControl("rbtnCashMemo");
                Label lblSALE_NO_OF_BAG = (Label)i.FindControl("lblSALE_NO_OF_BAG");
                Label lblSALETRANSID = (Label)i.FindControl("lblSALETRANSID");
                

                if (chk.Checked == true)
                {
                    btnCancel.Enabled = true;
                    txtBags.Enabled = true;
                    txtBags.Text = lblSALE_NO_OF_BAG.Text;
                    lblSALETRANSID1.Text = lblSALETRANSID.Text;
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
        this.ModalPopUp.Show();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(lblAvlQnty.Text) >= Convert.ToDecimal(txtBags.Text))
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.SALETRANSID = lblSALETRANSID1.Text;
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            objUserBEL.Recv_No_Of_Bags = txtBags.Text;
            objUserDLL = new DLL_DropDown();
            int Val = 0;

            objUserDLL.ReturnToOSSC(objUserBEL, out Val);
            if (Val == 1)//SUCCESS
            {
                FillSearchDtls();
                string popp = "alert('Transaction Completed Successfull !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }
            else if (Val == 2)//NO OF BAG ISSUE
            {
                string popp = "alert('Oops Transaction Can Not be Deleted !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }
            else if (Val == 3)//UNSUCCESS
            {
                string popp = "alert('Oops Transaction Un-Successfull !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }
            lblSALETRANSID1.Text = "";
        }
        else
        {
            string popp = "alert('Required no of bags not available !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
        }
    }
}
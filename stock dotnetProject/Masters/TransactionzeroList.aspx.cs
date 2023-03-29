using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_TransactionzeroList : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
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
            if (!(this.Session["UserType"].ToString() == "AAOO") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                //BindGrid();
                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
            }
        }
    }

    private void BindGrid()
    {
        DataSet ds = new DataSet();
        if (!txtTransIDSearch.Text.Trim().Equals(string.Empty))
        {
            string[] param = { "@DIST_CODE", "@TRANSACTION_ID" };
            object[] value = { Session["UserID"].ToString().Substring(4, 2).Trim(), txtTransIDSearch.Text.Trim() };
            ds = dbsApp.param_passing_fetchDataSetSP("Sp_Get_Trans", param, value);
        }
        else
        {
            string[] param = { "@DIST_CODE", "@TRANSACTION_ID" };
            object[] value = { Session["UserID"].ToString().Substring(4, 2).Trim(), null };
            ds = dbsApp.param_passing_fetchDataSetSP("Sp_Get_Trans", param, value);
        }
        if (ds != null)
        {
            grd_TransactionList.DataSource = ds.Tables[0];
            grd_TransactionList.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void lnkTransId_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);

            HiddenField hfTransId = (HiddenField)row.FindControl("hfTransId");
            HiddenField hfFARMERID = (HiddenField)row.FindControl("hfFARMERID");
            HiddenField hfDISTCODE = (HiddenField)row.FindControl("hfDISTCODE");
            lblBeneficiaryName.Text = string.Empty;
            lblVillgeName.Text = string.Empty;
            lblTransIDFarm.Text = hfTransId.Value;
            lblFarmID.Text = hfFARMERID.Value;
            lblDistCodeFarm.Text = hfDISTCODE.Value;
            Label lblDistName = (Label)row.FindControl("lbl_FARMERID");
            lblDistname.Text = lblDistName.Text.Substring(0, 4).Trim();
            txtFarmerId.Text = string.Empty;

            DataSet ds12 = new DataSet();
            string[] param = { "@TRANSACTION_ID" };
            object[] value = { hfTransId.Value };
            ds12 = dbsApp.param_passing_fetchDataSetSP("SP_ZeroTransDetails", param, value);
            if (ds12 != null)
            {
                fillgridDetails(ds12);

            }
            this.popupEdit.Show();
        }
        catch (Exception ex)
        {
            //throw;
        }
    }
    public void fillgridDetails(DataSet ds12)
    {
        gvDtls.DataSource = ds12.Tables[0];
        gvDtls.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //if (this.hfSession.Value != this.Session["AuthTokenPage"].ToString())
        //{
        //    ExceptionHandler.WriteException("Session Value in Cookie And Hidden Field Does not Match");
        //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Error Executing the Request. Please Contact Site Administrator for Details !');", true);
        //    return;
        //}
        if (string.IsNullOrWhiteSpace(txtFarmerId.Text.Trim()))
        {
            string pop = "alert('Please enter Farmer Id !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            ScriptManager.GetCurrent(Page).SetFocus(txtFarmerId);
            this.popupEdit.Show();
            return;
        }
        else
        {
            objUserBEL.DistCode = lblDistCodeFarm.Text.Trim();
            objUserBEL.TRANSACTIONID = lblTransIDFarm.Text.Trim();
            objUserBEL.FARMERID = txtFarmerId.Text.Trim();
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            objUserDLL = new DLL_DropDown();
            int Val = 0;
            objUserDLL.UpdateZeroTrans(objUserBEL, out Val);
            if (Val == 0)//UNSUCCESS
            {
                string popp1 = "alert('TRANSACTION FAILED !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp1", popp1, true);
            }
            else if (Val == 1)//SUCCESS
            {
                BindGrid();
                string popp1 = "alert('Farmer Id Updated Successfully !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp1", popp1, true);
            }
            else if (Val == 2)//UN VALID 
            {
                string popp1 = "alert('InValid Farmer Id !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp1", popp1, true);
            }
            //else if (Val == 3)//Already Exits
            //{
            //    string popp1 = "alert('Farmer Id Already Exits !!!');";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp1", popp1, true);
            //}
            else if (Val == 3)//Already Exits
            {
                string popp1 = "alert('Pending For AAO Approval. !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp1", popp1, true);
            }

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtTransIDSearch.Text = string.Empty;
        grd_TransactionList.DataSource = null;
        grd_TransactionList.DataBind();
    }
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        txtTransIDSearch.Text = string.Empty;
        //BindGrid();
    }
    protected void grd_TransactionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");

            string rowID = e.Row.ClientID;
            LinkButton rb = (LinkButton)e.Row.FindControl("lnkTransId");
            rb.Attributes.Add("onclick", "document.all['" + rowID + "'].style.backgroundColor = 'orange';");
        }
    }
    protected void btnGoSearch_Click(object sender, EventArgs e)
    {
        lblBeneficiaryName.Text = string.Empty;
        lblVillgeName.Text = string.Empty;
        DataSet dsname = new DataSet();
        string[] param = { "@NICFARMERID" };
        object[] value = { lblDistname.Text.Trim() + txtFarmerId.Text.Trim() };
        dsname = dbsApp.param_passing_fetchDataSetSP("GetFarmerAddress", param, value);
        DataTable dtName = dsname.Tables[0];
        if (dtName.Rows.Count > 0)
        {
            lblBeneficiaryName.Text = dsname.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
            lblVillgeName.Text = dsname.Tables[0].Rows[0]["villg_name"].ToString();
            btnUpdate.Enabled = true;
            this.popupEdit.Show();
        }
        else
        {
            this.popupEdit.Show();
            lblBeneficiaryName.Text = "N/A";
            lblVillgeName.Text = "N/A";
            btnUpdate.Enabled = false;
        }
    }
}
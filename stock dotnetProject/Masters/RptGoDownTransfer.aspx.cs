using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_RptGoDownTransfer : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
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
            if (!(this.Session["UserType"].ToString() == "OAIC") && !(this.Session["UserType"].ToString() == "OSSC") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                //objUserBEL = new BLL_DropDown();
                //objUserBEL.DistCode = Session["DistCode"].ToString();
                //objUserBEL.UserType = Session["UserType"].ToString();
                //objUserDLL = new DLL_DropDown();

                //this.utl.SetSessionCookie();
                //this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                //return;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
    private void FillData()
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.DistCode = Session["DistCode"].ToString();
        objUserBEL.Sale_Date = txtSaleDate.Text;
        objUserBEL.UserType = Session["UserType"].ToString();

        objUserDLL = new DLL_DropDown();
        ds = objUserDLL.FillSaleDtlsByDateGODown(objUserBEL);
        gvSaleDtls.DataSource = ds;
        gvSaleDtls.DataBind();
    }
    protected void lnk_CASH_MEMO_NO_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        Session["CashMemo"] = grdrow.Cells[1].Text;
    }
    protected void gvSaleDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            Session["CashMemo"] = lnkView.Text;

            string popicon = "window.open('RptSaleVoucher.aspx','Report','width=1020,height=900,toolbar=1,resizable=1,location=0, status=0, menubar=0, scrollbars=1');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popicon", popicon, true);
        }
    }
}
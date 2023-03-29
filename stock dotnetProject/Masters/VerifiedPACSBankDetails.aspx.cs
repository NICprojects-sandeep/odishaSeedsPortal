using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_VerifiedPACSBankDetails : System.Web.UI.Page
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
                FillOSSCDealers();
            }
        }
    }
    private void FillOSSCDealers()
    {
        try
        {
            gvPACS.DataSource = null;
            gvPACS.DataBind();
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = ddlDistrict.SelectedValue;
            objUserBEL.UserType = rbtnlDealerType.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillVerifiedPACS(objUserBEL);
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
    protected void gvPACS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Status = e.Row.Cells[6].Text;
            //e.Row.ForeColor = Color.White;            
            if (Status == "VERIFIED")
            {
                e.Row.ForeColor = Color.Green;
                //e.Row.BackColor = Color.Green;
            }
            else if (Status == "REJECTED")
            {
                e.Row.ForeColor = Color.Red;
                //e.Row.BackColor = Color.Red;                
            }
            else
            {
                e.Row.ForeColor = Color.Blue;
                //e.Row.BackColor = Color.Blue;
            }
        }
    }
    protected void rbtnlDealerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOSSCDealers();
    }
}
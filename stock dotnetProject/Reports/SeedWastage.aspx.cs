using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_SeedWastage : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillFinYear();
            FillSeassion(ddlFinYear.SelectedValue);
        }
    }
    private void FillSeassion(string FinYear)
    {
        try
        {
            ddlSeassion.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = FinYear;
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSeassion(objUserBEL);
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
            //ddlSeassion.Items.FindByText(ds.Tables[0].Rows[0]["SEASSION_NAME"].ToString()).Selected = true;
            ddlSeassion.Items.Insert(0, "--Select--");
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
    }
    private void FillFinYear()
    {
        try
        {
            ddlFinYear.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFinYear.DataSource = ds.Tables[0];
                    ddlFinYear.DataTextField = "FIN_YR";
                    ddlFinYear.DataValueField = "FIN_YR";
                    ddlFinYear.DataBind();
                }
            }
            ddlFinYear.Items.Insert(0, "--SELECT--");
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
    }    
    protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnDearer.Text = "0.00";
        btnSupplier.Text = "0.00";
        FillSeassion(ddlFinYear.SelectedValue);
        lblDealerFinYr.Text = lblSupplierFinYr.Text = ddlFinYear.SelectedItem.Text;
    }
    protected void ddlSeassion_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnDearer.Text = "0.00";
        btnSupplier.Text = "0.00";
        lblDealerSeassion.Text = lblSupplierSeassion.Text = ddlSeassion.SelectedItem.Text;
        FillData();
    }
    private void FillData()
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.FIN_YR = ddlFinYear.SelectedValue;
        objUserBEL.SEASON = ddlSeassion.SelectedValue;
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillSeedWastage(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnDearer.Text = ds.Tables[0].Rows[0]["DEALER_STOCK"].ToString();
                btnSupplier.Text = ds.Tables[0].Rows[0]["SUPPLIER_STOCK"].ToString();
            }
        }
    }
    protected void btnDearer_Click(object sender, EventArgs e)
    {
        Response.Redirect("WastageDistDealers.aspx?FIN_YR=" + lblDealerFinYr.Text + "&SEASSION_NAME=" + ddlSeassion.SelectedValue);
    }
    protected void btnSupplier_Click(object sender, EventArgs e)
    {
        Response.Redirect("WastageStateDealers.aspx?FIN_YR=" + lblSupplierFinYr.Text + "&SEASSION_NAME=" + ddlSeassion.SelectedValue);
    }
}
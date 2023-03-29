using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Subsidy25Per :  System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            pnlHeader.Visible = true;
            pnlDetail.Visible = false;
            FillCrop();
            FillAge();
            FillSource();
        }        
    }
    private void FillSource()
    {
        try
        {
            ddlSource.Items.Clear();
            ddlSource.DataSource = null;
            ddlSource.DataBind();

            objUserBEL = new BLL_DropDown();
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserBEL.AGE = Convert.ToInt32(ddlAge.SelectedValue);
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSubsidyCropSource(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSource.DataSource = ds;
                    ddlSource.DataTextField = "SOURCE";
                    ddlSource.DataValueField = "SOURCE";
                    ddlSource.DataBind();
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
        pnlDetail.Visible = false;
    }
    private void FillAge()
    {
        try
        {
            ddlAge.Items.Clear();
            ddlAge.DataSource = null;
            ddlAge.DataBind();

            objUserBEL = new BLL_DropDown();
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSubsidyCropAge(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAge.DataSource = ds;
                    ddlAge.DataTextField = "VARIETY_AGENAME";
                    ddlAge.DataValueField = "VARIETY_AGE";
                    ddlAge.DataBind();
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
    }
    private void FillCrop()
    {
        try
        {
            ddlCrop.Items.Clear();
            ddlCrop.DataSource = null;
            ddlCrop.DataBind();

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSubsidyCrop();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCrop.DataSource = ds;
                    ddlCrop.DataTextField = "Crop_Name";
                    ddlCrop.DataValueField = "Crop_Code";
                    ddlCrop.DataBind();
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
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.CROP_ID = ddlCrop.SelectedValue;
        objUserBEL.AGE = Convert.ToInt32(ddlAge.SelectedValue);
        objUserBEL.SOURCE = ddlSource.SelectedValue;
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillSubsidyDtl(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlHeader.Visible = true;
                pnlDetail.Visible = true;
                GenerateReport(ds);
            }
        }
    }
    protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAge();
        FillSource();
    }
    protected void ddlAge_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSource();
    }
    private void GenerateReport(DataSet ds)
    {
        DataTable dtData = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:900px; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Left;'>Sl No</td>");
        str.Append("<td style='text-align: Left;'>Farmer ID</td>");
        str.Append("<td style='text-align: Right;'>Subsidy Amount</td>");
        str.Append("<td style='text-align: Right;'>25% Amount</td>");
        str.Append("</tr>");
        int i = 0;
        foreach (DataRow dr in dtData.Rows)
        {
            i++;
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["FARMER_ID"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SUBSIDY_AMOUNT"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SUB_AMOUNT"].ToString() + "</td>");
            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Total</td>");
        str.Append("<td></td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(SUBSIDY_AMOUNT)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(SUB_AMOUNT)", "").ToString())) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserBEL.AGE = Convert.ToInt32(ddlAge.SelectedValue);
            objUserBEL.SOURCE = ddlSource.SelectedValue;
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserDLL = new DLL_DropDown();
            int Val = objUserDLL.UpdSubsidyInvolved25_1(objUserBEL);
            if (Val == 1)//SUCCESS
            {
                string popp = "alert('Approval process completed successfully!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }
            else
            {
                string popp = "alert('Error Occured!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }
            pnlHeader.Visible = true;
            pnlDetail.Visible = false;
            FillCrop();
            FillAge();
            FillSource();
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = true;
        pnlDetail.Visible = false;
        FillCrop();
        FillAge();
        FillSource();
    }
}
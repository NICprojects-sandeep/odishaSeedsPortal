﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_PaymentCheck : System.Web.UI.Page
{
    DLL_DropDown objUserDLL;
    BLL_DropDown objUserBAL;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillFinYear();
            fillSeason();
        }
    }
    private void fillFinYear()
    {
        try
        {
            objUserDLL = new DLL_DropDown();
            objUserBAL = new BLL_DropDown();
            objUserBAL.STATUS = "A";
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBAL);
            ddlfinyear.DataSource = ds;
            ddlfinyear.DataTextField = "FIN_YR";
            ddlfinyear.DataBind();
            ddlfinyear.Items.Insert(0, "Select");
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
    private void fillSeason()
    {
        try
        {
            objUserDLL = new DLL_DropDown();
            objUserBAL = new BLL_DropDown();
            objUserBAL.STATUS = "A";
            objUserBAL.FIN_YR = ddlfinyear.SelectedValue;
            ds = new DataSet();
            ds = objUserDLL.FillSeassion(objUserBAL);
            ddlseason.DataSource = ds;
            ddlseason.DataTextField = "SEASSION_NAME";
            ddlseason.DataValueField = "SHORT_NAME";
            ddlseason.DataBind();
            ddlseason.Items.Insert(0, "Select");
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            gvdlrrpt.DataSource = null;
            gvdlrrpt.DataBind();

            objUserDLL = new DLL_DropDown();
            objUserBAL = new BLL_DropDown();
            ds = new DataSet();
            objUserBAL.LICENCENO = txt_dlrcode.Text;
            objUserBAL.FIN_YR = ddlfinyear.SelectedValue;
            objUserBAL.SESSION = ddlseason.SelectedValue;
            ds = objUserDLL.DLRPaymentSTS(objUserBAL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvdlrrpt.DataSource = ds;
                    gvdlrrpt.DataBind();

                    gvdlrrpt.FooterRow.Cells[1].Text = "Total";
                    gvdlrrpt.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;                    
                    decimal total1 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NO_OF_BAGS"));
                    gvdlrrpt.FooterRow.Cells[6].Text = total1.ToString("N2");
                    decimal total2 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TOT_QTL"));
                    gvdlrrpt.FooterRow.Cells[7].Text = total2.ToString("N2");
                    decimal total3 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Total_Payment_Amount"));
                    gvdlrrpt.FooterRow.Cells[9].Text = total3.ToString("N2");
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
    protected void ddlfinyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSeason();
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Reports_RPTPayment : System.Web.UI.Page
{
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillRequest();
        }
    }
    private void FillRequest()
    {
        try
        {                          
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillDealerRequest();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvRequest.DataSource = ds;
                    gvRequest.DataBind();
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
    protected void gvRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvRequest.Rows[rowIndex];

            string CPAID = (row.FindControl("lblCPAID") as Label).Text;
            Session["CPAID"] = CPAID;
            FillSaleDtls(CPAID);
            popupEdit.Show();
        }
    }
    private void FillSaleDtls(string CPAID)
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.CPAID = CPAID;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.Fillouter(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    generateReport(ds);
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
    private void generateReport(DataSet ds)
    {
        DataView dv = new DataView(ds.Tables[0]);
        DataTable dt = dv.ToTable(true, "LIC_NO", "AccountNumber", "APP_FIRMNAME");
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        //str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");

        //str.Append("<td>Account No.</td>");
        //str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            str.Append("<tr>");
            str.Append("<td colspan='2' style='width:40%'>License Number :<b>" + dr["LIC_NO"].ToString() + "</b></td>");
            str.Append("<td colspan='2'  style='width:30%'>Name :<b>" + dr["APP_FIRMNAME"].ToString() + "</b></td>");
            str.Append("<td colspan='2' style='width:30%'>Account No. :<b>" + dr["AccountNumber"].ToString() + "</b></td>");
            str.Append("</tr>");
            i++;
            DataRow[] dtl = ds.Tables[0].Select("LIC_NO='" + dr["LIC_NO"].ToString() + "'");
            str.Append("<tr>");
            str.Append("<td  colspan='6'>");
            str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td>Sl. No. </td>");
            str.Append("<td>Transaction No. </td>");
            str.Append("<td>FARMER ID </td>");
            str.Append("<td>Farmer Name </td>");
            str.Append("<td>Crop Name </td>");
            str.Append("<td>Variety</td>");
            str.Append("<td>Total Sale</td>");
            str.Append("<td>Subsidy</td>");
            str.Append("<td>GOI Amount</td>");
            str.Append("<td>SP Amount</td>");
            str.Append("</tr>");

            int sl = 1;
            foreach (DataRow drdtl in dtl)
            {
                str.Append("<tr>");
                str.Append("<td>" + sl.ToString() + "</td>");
                str.Append("<td>" + drdtl["NewTrans"].ToString() + "</td>");
                str.Append("<td>" + drdtl["FARMER_ID"].ToString() + "</td>");
                str.Append("<td>" + drdtl["VCHFARMERNAME"].ToString() + "</td>");
                str.Append("<td>" + drdtl["Crop_Name"].ToString() + "</td>");
                str.Append("<td>" + drdtl["Variety_Name"].ToString() + "</td>");
                str.Append("<td>" + drdtl["TOT_QTL"].ToString() + "</td>");
                str.Append("<td>" + drdtl["ADMISSIBLE_SUBSIDY"].ToString() + "</td>");
                str.Append("<td>" + drdtl["TOT_SUB_AMOUNT_GOI"].ToString() + "</td>");
                str.Append("<td>" + drdtl["TOT_SUB_AMOUNT_SP"].ToString() + "</td>");
                str.Append("</tr>");
                sl++;
            }
            str.Append("</table>");
            str.Append("</tr>");
            str.Append("</td>");
        }
        //string Amount = dt.Compute("Sum(Amount)", "").ToString();
        //string TOT_QTL = dt.Compute("Sum(TOT_QTL)", "").ToString();
        //str.Append("<td style='text-align: Right;' colspan='7'>Total : </td>");
        //str.Append("<td style='text-align: Right;'>" + Amount + "</td>");

        //str.Append("<td style='text-align: Right;'>" + TOT_QTL + "</td>");
        //str.Append("<td style='text-align: Right;'>&nbsp;</td>");
        //str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        popupEdit.Hide();
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_InputSubsidy : System.Web.UI.Page
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
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                FillFinYr();
                btnExport.Enabled = false;
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
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
    }
    private void FillFinYr()
    {
        try
        {
            drpSession.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSession.DataSource = ds;
                    drpSession.DataTextField = "FIN_YR";
                    drpSession.DataValueField = "FIN_YR";
                    drpSession.DataBind();
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
        //ddlFinYr.Items.Insert(0, "--Select--");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "STOCK_RPT_InputSubsidy";
        string[] param = { "@FIN_YEAR", "@SEASON" };
        object[] value = { drpSession.SelectedValue.Trim(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        GenerateReport(ds);
    }
    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:1325px; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' >");
        str.Append("<td colspan='3' style='text-align: center'><strong>Normal</strong></td><td colspan='3' style='text-align: center'><strong>SCP</strong></td><td colspan='3' style='text-align: center'><strong>TASP</strong></td><td colspan='3' style='text-align: center'><strong>Total</strong></td>");        
        str.Append("</tr>");

        str.Append("<tr>");
                    str.Append("<td>");
                        str.Append("Quantity Sold</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(GOI)</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(SP)</td>");
                    str.Append("<td>");
                        str.Append("Quantity Sold</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(GOI)</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(SP)</td>");
                    str.Append("<td>");
                        str.Append("Quantity Sold</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(GOI)</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(SP)</td>");
                    str.Append("<td>");
                        str.Append("Quantity Sold</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(GOI)</td>");
                    str.Append("<td>");
                        str.Append("Subsidy Involved(SP)</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        
        DataTable dtData1 = ds.Tables[1];
        foreach (DataRow dr1 in dtData1.Rows)
        {
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr1["TOT_QTL1"].ToString())) + " </td>");
                str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr1["TOT_SUB_AMOUNT_GOI1"].ToString())) + "</td>");
                str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr1["TOT_SUB_AMOUNT_SP1"].ToString())) + "</td>");
        }
        DataTable dtData2 = ds.Tables[2];
        foreach (DataRow dr2 in dtData2.Rows)
        {
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr2["TOT_QTL2"].ToString())) + "</td>");
                str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr2["TOT_SUB_AMOUNT_GOI2"].ToString())) + "</td>");
                str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr2["TOT_SUB_AMOUNT_SP2"].ToString())) + "</td>");
        }
           DataTable dtData3 = ds.Tables[3];
        foreach (DataRow dr3 in dtData3.Rows)
        {
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr3["TOT_QTL3"].ToString())) + " </td>");
                str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr3["TOT_SUB_AMOUNT_GOI3"].ToString())) + " </td>");
                str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr3["TOT_SUB_AMOUNT_SP3"].ToString())) + " </td>");
        }
        DataTable dtData = ds.Tables[0];
        foreach (DataRow dr in dtData.Rows)
        {

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["TOT_QTL0"].ToString())) + " </td>");
            str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr["TOT_SUB_AMOUNT_GOI0"].ToString())) + " </td>");
            str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true'></i>" + string.Format("{0:f2}", double.Parse(dr["TOT_SUB_AMOUNT_SP0"].ToString())) + " </td>");
        }
       
        str.Append("</tr>"); 
        //str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        //str.Append("<td>Grand Total</td>");
        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData1.Compute("Sum(TOT_QTL1)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData1.Compute("Sum(TOT_SUB_AMOUNT_GOI1)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData1.Compute("Sum(TOT_SUB_AMOUNT_SP1)", "").ToString())) + "</td>");

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData2.Compute("Sum(TOT_QTL2)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData2.Compute("Sum(TOT_SUB_AMOUNT_GOI2)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData2.Compute("Sum(TOT_SUB_AMOUNT_SP2)", "").ToString())) + "</td>");

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData3.Compute("Sum(TOT_QTL3)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData3.Compute("Sum(TOT_SUB_AMOUNT_GOI3)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData3.Compute("Sum(TOT_SUB_AMOUNT_SP3)", "").ToString())) + "</td>");

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(TOT_QTL0)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(TOT_SUB_AMOUNT_GOI0)", "").ToString())) + "</td>");
        //str.Append("<td style='text-align: Right;'><i class='fa fa-rupee' aria-hidden='true' style='color:red;'></i>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(TOT_SUB_AMOUNT_SP0)", "").ToString())) + "</td>");
        
        //str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=InputSubsidy.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            litReport.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex) { }
    }
}
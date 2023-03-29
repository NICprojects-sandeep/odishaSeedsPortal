using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_BGREIPaymentStatus : System.Web.UI.Page
{
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillFinYr();
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
    private void GenerateReport()
    {
        DataSet ds = new DataSet();

        string sql = "STOCK_BGREIPaymentRpt";
        string[] param = { "@FIN_YEAR", "@SEASON" };
        object[] value = {  drpSession.SelectedValue.Trim(), drpSeassion.SelectedValue.Trim()=="0" ? System.Convert.DBNull
 : drpSeassion.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateHTML(ds);
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }


    private void GenerateHTML(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='3' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' >");
        str.Append("<td rowspan='2'>Year</td>");
        str.Append("<td colspan='3'>Amount utilized</td>");
        str.Append("<td colspan='3'>Amount pending for utilization</td>");
        str.Append("<td colspan='3'>No of farmers got subsidy</td>");
        str.Append("<td colspan='3' >No of farmers payment pending</td>");
        str.Append("<td rowspan='2'>Quantity of seed sold</td>");
        str.Append("</tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' >");

        str.Append("<td>PFMS</td>");
        str.Append("<td>DCCB</td>");
        str.Append("<td>Total</td>");
        str.Append("<td>PFMS</td>");
        str.Append("<td>DCCB</td>");
        str.Append("<td>Total</td>");
        str.Append("<td>PFMS</td>");
        str.Append("<td>DCCB</td>");
        str.Append("<td>Total</td>");
        str.Append("<td>PFMS</td>");
        str.Append("<td>DCCB</td>");
        str.Append("<td>Total</td>");

        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["FIN_YEAR"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["PFMS_Utilized"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["CCB_Utilized"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["Tot_Utilized"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["PFMS_Pending"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["CCB_Pending"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["Tot_Pending"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["PaidpfmsFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["PaidccbFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TotPaid"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["PFMS_PendingFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["CCB_PendingFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["Tot_PendingFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_QTL"].ToString() + "</td>");
            str.Append("</tr>");
        }


        str.Append("</table>");
        litReport.Text = str.ToString();


    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=CropWiseInvolvement.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            litReport.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GenerateReport();
    }
}
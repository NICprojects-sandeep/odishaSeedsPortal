﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
public partial class Reports_Rpt_DistwiseCurrent_Stock : System.Web.UI.Page
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
            if (!(this.Session["UserType"].ToString() == "DRCS") && !(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") && !(this.Session["UserType"].ToString() == "DIST") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                btnExport.Enabled = false;
                FillCropCatg();
                FillFinYr();
            }
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
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //this.FirstLogin = this.Session["FirstLogin"].ToString();

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
        if (this.Session["UserType"].ToString() == "DIST")
        {
            this.Page.MasterPageFile = "../MasterPages/Dist.master";
            return;
        }
    }
    protected void FillCropCatg()
    {
        try
        {

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropCategory();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpCropCatagory.DataSource = ds;
                    this.drpCropCatagory.DataValueField = "Category_Code";
                    this.drpCropCatagory.DataTextField = "Category_Name";
                    this.drpCropCatagory.DataBind();
                    this.drpCropCatagory.Items.Insert(0, "--- SELECT ---");
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
    protected void ddl_CropCatagory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        FillCrop();
    }

    protected void FillCrop()
    {
        try
        {
            Regex regex = new Regex("^\\d{2}$");
            string CropCatgID = this.drpCropCatagory.SelectedValue;
            if (!regex.IsMatch(CropCatgID))
            {
                throw new ApplicationException("Invalid Characters!10");
            }

            objUserBEL = new BLL_DropDown();
            objUserBEL.CROPCATG_ID = drpCropCatagory.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropByCategoryId(objUserBEL);
            drpCropName.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpCropName.DataSource = ds;
                    this.drpCropName.DataValueField = "Crop_Code";
                    this.drpCropName.DataTextField = "Crop_Name";
                    this.drpCropName.DataBind();
                }
            }
            this.drpCropName.Items.Insert(0, "--- SELECT ---");

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "STOCK_RPT_GetPresentStock";
        string[] param = { "@FIN_YR", "@User_Type", "@CropCatg_ID", "@Crop_ID", "@Dist_Code", "@Session" };
        object[] value = { drpSession.SelectedValue.Trim(), drpUser.SelectedValue.Trim(), drpCropCatagory.SelectedValue.Trim(), drpCropName.SelectedValue.Trim(), Session["DistCode"].ToString(), drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds);
        }
        else
        {
            btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }

    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        DataTable dtData = ds.Tables[0];
        DataTable dtDist = ds.Tables[1];

        //Distinct Variety Code and Name
        DataView view = new DataView(dtData);
        DataTable distinctValues = view.ToTable(true, "Crop_Verid", "Variety_Name");


        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>Dist Name</td>");
        str.Append("<td colspan='" + distinctValues.Rows.Count + "' style='text-align: center;'>Variety</td>");
        str.Append("<td rowspan='2'  style='text-align: Right;'>Total</td>");
        str.Append("<td rowspan='2'  style='text-align: Right;'>View Details</td>");
        str.Append("</tr>");


        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        foreach (DataRow dr in distinctValues.Rows)
        {
            str.Append(" <td  style='width:150px;'>" + dr["Variety_Name"].ToString() + "</td>");
        }
        str.Append("</tr>");

        double TotDistwise = 0;
        foreach (DataRow dr in dtDist.Rows)
        {
            str.Append(" <tr>");
            str.Append(" <td>" + dr["Dist_Name"].ToString() + "</td>");

            foreach (DataRow drv in distinctValues.Rows)
            {
                DataRow[] result = dtData.Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Verid = '" + drv["Crop_Verid"].ToString() + "'");
                if (result.Length > 0)
                {
                    foreach (DataRow row in result)
                    {

                        str.Append(" <td style='text-align: Right;'>" + row[4].ToString() + "</td>");
                        TotDistwise = TotDistwise + double.Parse(row[4].ToString());
                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right;'>--</td>");
                }

            }
            string value = TotDistwise > 0 ? TotDistwise.ToString() : "--";
            str.Append("<td style='text-align: Right;'>" + value + "</td>");
            TotDistwise = 0;

            str.Append("<td align='right' class='tbltd'>");
            string tmpstr11 = "<a href='javascript:popUp";
            string tempStr1 = "('rptGodownWiseStock.aspx?FIN_YR=" + drpSession.SelectedValue.Trim() + " &User_Type= " + drpUser.SelectedValue.Trim() + "&CropCatg_ID=" + drpCropCatagory.SelectedValue.Trim() + " &Crop_ID= " + drpCropName.SelectedValue.Trim() + "&Dist_Code= " + dr["Dist_Code"].ToString() + "&Season= " + drpSeassion.SelectedValue.Trim() + "')";
            string tmpstr21 = "'>";
            str.Append(tmpstr11.Replace('\'', '\"') + tempStr1 + tmpstr21.Replace('\'', '\"'));
            str.Append("<img src='../images/binoculars.png'  title='View Details' /></a>");
            str.Append("</td>");

            str.Append("</tr>");
        }
        str.Append(" <tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append(" <td>Varietywise Total</td>");
        foreach (DataRow drv in distinctValues.Rows)
        {
            string TotVar = dtData.Compute("Sum(STOCK)", "Crop_Verid = '" + drv["Crop_Verid"].ToString() + "'").ToString();
            str.Append("<td style='text-align: Right;'>" + TotVar + "</td>");
        }
        str.Append("<td style='text-align: Right;'>" + dtData.Compute("Sum(STOCK)", "").ToString() + "</td>");

        str.Append("<td style='text-align: Right;'>&nbsp;</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=CurrentStockPosition.xls";
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
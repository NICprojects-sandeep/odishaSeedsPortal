using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Rpt_DistwiseUserwiseStock2 : System.Web.UI.Page
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

        string sql = "STOCK_RPT_CropwiseStock2";
        string[] param = { "@FIN_YR", "@CropCatg_ID", "@Crop_ID", "@DistCode", "@Seassion" };
        object[] value = { drpSession.SelectedValue.Trim(), drpCropCatagory.SelectedValue.Trim(), drpCropName.SelectedValue.Trim(), Session["DistCode"].ToString(),drpSeassion.SelectedValue.Trim()=="0"?System.Convert.DBNull:drpSeassion.SelectedValue.Trim()};
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        GenerateReport(ds);

    }

    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        DataTable dtData = ds.Tables[0];


        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' ><td rowspan='2' >Dist Name</td><td colspan='5' style='text-align:center;'>OSSC</td><td colspan='3' style='text-align:center;'>OAIC</td><td colspan='4' style='text-align:center;'>STATE</td>");



        str.Append("</tr>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;'>Receive</td>");
        str.Append("<td style='text-align: Right;'>Dealer Sale</td>");
        str.Append("<td style='text-align: Right;'>Pacs Sale</td>");
        str.Append("<td style='text-align: Right;'>Total Sale</td>");

        str.Append("<td style='text-align: Right;'>Balance</td>");
        str.Append("<td style='text-align: Right;'>Receive</td>");
        str.Append("<td style='text-align: Right;'>Pacs Sale</td>");
        str.Append("<td style='text-align: Right;'>Balance</td>");
        str.Append("<td style='text-align: Right;'>State Total Received</td>");

        str.Append("<td style='text-align: Right;'>State Total Dealer Sale</td>");

        str.Append("<td style='text-align: Right;'>State Total Pacs Sale</td>");

        str.Append("<td style='text-align: Right;'>State Total Balance</td>");
        str.Append("</tr>");
        foreach (DataRow dr in dtData.Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Recv"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SaleDealer"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SalePacks"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SaleDealer"].ToString()) + double.Parse(dr["OSSC_SalePacks"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Stock"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OAIC_Recv"].ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OAIC_SalePacks"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OAIC_Stock"].ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Recv"].ToString()) + double.Parse(dr["OAIC_Recv"].ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SaleDealer"].ToString())) + "</td>");


            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SalePacks"].ToString()) + double.Parse(dr["OAIC_SalePacks"].ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Stock"].ToString()) + double.Parse(dr["OAIC_Stock"].ToString())) + "</td>");



            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Grand Total</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Recv)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString()) + double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Stock)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OAIC_Recv)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OAIC_SalePacks)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OAIC_Stock)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Recv)", "").ToString()) + double.Parse(dtData.Compute("Sum(OAIC_Recv)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString()) + double.Parse(dtData.Compute("Sum(OAIC_SalePacks)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Stock)", "").ToString()) + double.Parse(dtData.Compute("Sum(OAIC_Stock)", "").ToString())) + "</td>");

        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=StateStockPosition.xls";
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
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

public partial class Rpt_OsscStock : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (this.Session["UserType"].ToString() == "ADMI")
        {
            this.Page.MasterPageFile = "../MasterPages/Admin.master";
            return;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            btnExport.Enabled = false;
            FillCropCatg();
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


    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=OsscStockDetails.xls";
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "STOCK_RPT_OSSCStock";
        string[] param = { "@FIN_YR", "@CropCatg_ID", "@Crop_ID", "@Date" };
        object[] value = { drpSession.SelectedValue.Trim(), drpCropCatagory.SelectedValue.Trim(), drpCropName.SelectedValue.Trim(), txtdate.Text.Trim() == "" ? "" : Convert.ToDateTime(txtdate.Text).ToString("MM/dd/yyyy") };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        GenerateReport(ds);
    }
    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        DataTable dtData = ds.Tables[0];


        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' ><td rowspan='2' style='text-align: center;'>Dist Name</td><td colspan='5' style='text-align:center;'>OSSC</td>");



        str.Append("</tr>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Center;'>Receive</td>");
        str.Append("<td style='text-align: Center;'>Dealer Sale</td>");
        str.Append("<td style='text-align: Center;'>Pacs Sale</td>");
        str.Append("<td style='text-align: Center;'>Total Sale</td>");

        str.Append("<td style='text-align: Center;'>Balance</td>");
        str.Append("</tr>");
        foreach (DataRow dr in dtData.Rows)
        {
            str.Append("<tr>");
            str.Append("<td style='text-align: Left;'>" + dr["Dist_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Recv"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SaleDealer"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SalePacks"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SaleDealer"].ToString()) + double.Parse(dr["OSSC_SalePacks"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Stock"].ToString())) + "</td>");

            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;'>Grand Total</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Recv)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString()) + double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString())) + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Stock)", "").ToString())) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }


}
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

public partial class Reports_Rpt_OAICOSSCSTOCK : System.Web.UI.Page
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
            if (this.Session["UserType"].ToString() != "OAIC" && this.Session["UserType"].ToString() != "OSSC")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                btnExport.Enabled = false;
                      FillCropCatg();
                      FillDist();
                this.utl.SetSessionCookie();
                //this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                //this.hfMCode.Value = Session["UserID"].ToString();
                //this.lblName.Text = Session["FullName"].ToString();
                Session["OBDate"] = "01/10/2015";
                return;
            }
        }
       
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
       
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
                    this.drpDistrict.DataSource = ds;
                    this.drpDistrict.DataValueField = "Dist_Code";
                    this.drpDistrict.DataTextField = "Dist_Name";
                    this.drpDistrict.DataBind();
                    this.drpDistrict.Items.Insert(0, "--- All ---");
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

    //protected void FillSchme()
    //{
    //    try
    //    {

    //        objUserDLL = new DLL_DropDown();
    //        ds = new DataSet();
    //        ds = objUserDLL.FillScheme();
    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                this.drpScheme.DataSource = ds;
    //                this.drpScheme.DataValueField = "SCHEME_CODE";
    //                this.drpScheme.DataTextField = "SCHEME_NAME";
    //                this.drpScheme.DataBind();
    //                this.drpScheme.Items.Insert(0, "--- All ---");
    //            }
    //        }
    //    }
    //    catch (ApplicationException applicationException1)
    //    {
    //        ApplicationException applicationException = applicationException1;
    //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
    //    }
    //    catch (Exception exception1)
    //    {
    //        Exception exception = exception1;
    //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
    //        ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
    //    }
    //}
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
        string[] param = { "@FIN_YR", "@CropCatg_ID", "@Crop_ID", "@Seassion", "@DistCode", "@Date" };
        object[] value = { drpSession.SelectedValue.Trim(), drpCropCatagory.SelectedValue.Trim(), drpCropName.SelectedValue.Trim(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull
 : drpSeassion.SelectedValue.Trim(),drpDistrict.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpDistrict.SelectedValue.Trim(),txtDate.Text.Trim()==""?System.Convert.DBNull:txtDate.Text.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        GenerateReport(ds);

    }

    private void GenerateReport(DataSet ds)
    {
        
        btnExport.Enabled = true;
        DataTable dtData = ds.Tables[0];

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        if (this.Session["UserType"].ToString() == "OSSC")
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' ><td rowspan='2' >Dist Name</td><td colspan='5' style='text-align:center;'>OSSC</td>");
        }
        else
        {
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' ><td rowspan='2' >Dist Name</td><td colspan='3' style='text-align:center;'>OAIC</td>");
        }
        



        str.Append("</tr>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        if (this.Session["UserType"].ToString() == "OSSC")
        {
            str.Append("<td style='text-align: Right;'>Receive</td>");
            str.Append("<td style='text-align: Right;'>Dealer Sale</td>");
            str.Append("<td style='text-align: Right;'>Pacs Sale</td>");
            str.Append("<td style='text-align: Right;'>Total Sale</td>");
            str.Append("<td style='text-align: Right; width:250px;'>Balance</td>");
        }
        else
        {
           
            str.Append("<td style='text-align: Right;'>Receive</td>");
            str.Append("<td style='text-align: Right;'>Pacs Sale</td>");
            str.Append("<td style='text-align: Right; width:250px;'>Balance</td>");
        }
        
        str.Append("</tr>");
        foreach (DataRow dr in dtData.Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");
            if (this.Session["UserType"].ToString() == "OSSC")
            {
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Recv"].ToString())) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SaleDealer"].ToString())) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SalePacks"].ToString())) + "</td>");
                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SaleDealer"].ToString()) + double.Parse(dr["OSSC_SalePacks"].ToString())) + "</td>");
                // str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Stock"].ToString())) + "</td>");

                if (double.Parse(dr["OSSC_GtransOwnTrPend"].ToString()) > 0 || double.Parse(dr["OSSC_OthrGtransOwnTrPend"].ToString()) > 0)
                {



                    str.Append("<td style='text-align: Right;'>");
                    string tmpstr11 = "<a href='javascript:popUp";
                    string tempStr1 = "('RPT_TransitDtls.aspx?Crop_Code=" + drpCropName.SelectedValue.Trim() + " &Dist_Code= " + dr["Dist_Code"].ToString() + "&FIN_YR=" + drpSession.SelectedValue.ToString() + "&CropCatg_ID=" + drpCropCatagory.SelectedValue.ToString() + "&Seassion=" + drpSeassion.SelectedValue.ToString() + "&Date=" + txtDate.Text + "&Crop_ID=" + drpCropName.SelectedValue.ToString() + "')";
                    string tmpstr21 = "'>";
                    str.Append(tmpstr11.Replace('\'', '\"') + tempStr1 + tmpstr21.Replace('\'', '\"'));
                    str.Append("<div style='float:left; width:28%; text-align:left;'><span style='color:red;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_GtransOwnTrPend"].ToString()) + double.Parse(dr["OSSC_OthrGtransOwnTrPend"].ToString())) + "</span><i class='fa fa-truck' aria-hidden='true'></i></div>");

                    str.Append("</a>");

                    str.Append(string.Format("{0:f2}", double.Parse(dr["OSSC_Stock"].ToString())) + "</td>");
                }
                else
                {
                    str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_Stock"].ToString())) + "</td>");
                }

            }
            else
            {

                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OAIC_Recv"].ToString())) + "</td>");

                str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OAIC_SalePacks"].ToString())) + "</td>");
                if (double.Parse(dr["OAIC_GtransOwnTrPend"].ToString()) > 0 || double.Parse(dr["OAIC_OthrGtransOwnTrPend"].ToString()) > 0)
                {
                    str.Append("<td style='text-align: Right;'>");
                    string tmpstr11 = "<a href='javascript:popUp";
                    string tempStr1 = "('RPT_TransitOAICDtls.aspx?Crop_Code=" + drpCropName.SelectedValue.Trim() + " &Dist_Code= " + dr["Dist_Code"].ToString() + "&FIN_YR=" + drpSession.SelectedValue.ToString() + "&CropCatg_ID=" + drpCropCatagory.SelectedValue.ToString() + "&Seassion=" + drpSeassion.SelectedValue.ToString() + "&Date=" + txtDate.Text + "&Crop_ID=" + drpCropName.SelectedValue.ToString() + "')";
                    string tmpstr21 = "'>";
                    str.Append(tmpstr11.Replace('\'', '\"') + tempStr1 + tmpstr21.Replace('\'', '\"'));
                    str.Append("<div style='float:left; width:28%; text-align:left;'><span style='color:red;'>" + string.Format("{0:f2}", double.Parse(dr["OAIC_GtransOwnTrPend"].ToString()) + double.Parse(dr["OAIC_OthrGtransOwnTrPend"].ToString())) + "</span><i class='fa fa-truck' aria-hidden='true'></i></div>");

                    str.Append("</a>");

                    str.Append(string.Format("{0:f2}", double.Parse(dr["OAIC_Stock"].ToString())) + "</td>");
                }
                else
                {
                    str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OAIC_Stock"].ToString())) + "</td>");
                }

            }

        



            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Grand Total</td>");
        if (this.Session["UserType"].ToString() == "OSSC")
        {
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Recv)", "").ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString()) + double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Stock)", "").ToString())) + "</td>");
        }
        else
        {
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OAIC_Recv)", "").ToString())) + "</td>");

            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OAIC_SalePacks)", "").ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OAIC_Stock)", "").ToString())) + "</td>");
        }

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Recv)", "").ToString()) + double.Parse(dtData.Compute("Sum(OAIC_Recv)", "").ToString())) + "</td>");

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString())) + "</td>");

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString()) + double.Parse(dtData.Compute("Sum(OAIC_SalePacks)", "").ToString())) + "</td>");

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SalePacks)", "").ToString()) + double.Parse(dtData.Compute("Sum(OAIC_SalePacks)", "").ToString()) + double.Parse(dtData.Compute("Sum(OSSC_SaleDealer)", "").ToString())) + "</td>");

        //str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_Stock)", "").ToString()) + double.Parse(dtData.Compute("Sum(OAIC_Stock)", "").ToString())) + "</td>");

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
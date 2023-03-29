using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_RPT_VarietywiseLift : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {

        if (Session["Name"] != null || Session["UserType"] != null)
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
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
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
                FillCrop();
                FillDist();
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
    protected void FillCrop()
    {
        try
        {

            drpCropName.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = drpSession.SelectedValue;
            objUserBEL.SEASON = drpSeassion.SelectedValue;
            objUserBEL.CROPCATG_ID = "";
            objUserBEL.CROP_ID = drpCropName.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FILLFINWISECROPVARIETY(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpCropName.DataSource = ds;
                    this.drpCropName.DataValueField = "Crop_ID";
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
    public DataSet DsCrop()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Crop_Code,Crop_Name FROM mCrop WHERE is_active = 1 ORDER BY Crop_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            return ds;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return null;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "RPT_VarietywiseLift";
        string[] param = { "@Crop_ID", "@USER_TYPE", "@FIN_YR", "@Seassion", "@DistCode", "@MONTH", "@FRMDT", "@TODT" };
        object[] value = { drpCropName.SelectedValue.Trim(), drpUser.SelectedValue.Trim() ,drpSession.SelectedValue.Trim(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull
 : drpSeassion.SelectedValue.Trim() ,drpDistrict.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpDistrict.SelectedValue.Trim(),drpMonth.SelectedValue.Trim()=="0"?System.Convert.DBNull:drpMonth.SelectedValue.Trim(),txtfromdate.Text.Trim()==""?System.Convert.DBNull:txtfromdate.Text.Trim(),txtTodate.Text.Trim()==""?System.Convert.DBNull:txtTodate.Text.Trim()};
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[1].Rows.Count > 0)
        {
            btnExport.Enabled = true;
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
        double TotDealer = 0;
        double TotPacs = 0;
        double mTotDealer = 0;
        double mTotPacs = 0;
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px;' border='1' cellpadding='15' cellspacing='10' class='tbltxt'> <tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>Dist Name  </td>");
        DataView view = new DataView(ds.Tables[1]);
        view.Sort = "CROP_VERID ASC";
        DataTable dtVariety = view.ToTable(true, "CROP_VERID", "Variety_Name");
        foreach (DataRow dr in dtVariety.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;' colspan='2'>" + dr["Variety_Name"].ToString() + "</td>");

        }
        str.Append(" <td style='text-align: center; padding-center:5px;' colspan='2'>Total</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;' rowspan='2'>Total</td>");
        str.Append("</tr>");

        str.Append("<tr  style='background-color:#CCCCCC; font-weight:bold;'>");

        foreach (DataRow dr in dtVariety.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Dealer</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>PACS</td>");
        }
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Dealer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>PACS</td>");
        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");

            TotDealer = 0;
            TotPacs = 0;
            double TotSale = 0;
            foreach (DataRow drv in dtVariety.Rows)
            {
                double DealerSale = 0;
                double PacsSale = 0;

                DataRow[] Datadealer = ds.Tables[1].Select("Dist_Code = " + dr["Dist_Code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "' AND Type = 'Dealer'");
                if (Datadealer.Length > 0)
                {
                    foreach (DataRow value in Datadealer)
                    {
                        DealerSale = DealerSale + double.Parse(value[5].ToString());
                        TotSale = TotSale + double.Parse(value[5].ToString());
                    }
                    TotDealer += DealerSale;
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", DealerSale) + "</td>");
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>&nbsp;</td>");
                }

                DataRow[] DataPACS = ds.Tables[1].Select("Dist_Code = " + dr["Dist_Code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "' AND Type = 'PACS'");
                if (DataPACS.Length > 0)
                {
                    foreach (DataRow value in DataPACS)
                    {
                        PacsSale = PacsSale + double.Parse(value[5].ToString());
                        TotSale = TotSale + double.Parse(value[5].ToString());
                    }
                    TotPacs += PacsSale;
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", PacsSale) + "</td>");
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>&nbsp;</td>");
                }
            }

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotDealer) + "</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotPacs) + "</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotSale) + "</td>");
            str.Append("</tr>");
            mTotDealer += TotDealer;
            mTotPacs += TotPacs;
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append("<td>Grand Total</td>");
        foreach (DataRow dr in dtVariety.Rows)
        {
            string a = ds.Tables[1].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "' AND Type='Dealer'").ToString();
            string b = ds.Tables[1].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "' AND Type='PACS'").ToString();
            //str.Append("<td style='text-align: Right; padding-right:5px;'>" +  string.Format("{0:f2}", double.Parse(a)) + "</td>");
            if (a == "")
            {
                str.Append("<td style='text-align: Right; padding-right:5px;'>--</td>");
            }
            else
            {

                str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(a)) + "</td>");
            }
            if (b == "")
            {
                str.Append("<td style='text-align: Right; padding-right:5px;'>--</td>");
            }
            else
            {

                str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(b)) + "</td>");
            }

        }
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", mTotDealer) + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", mTotPacs) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SALE)", "").ToString())) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    //private void GenerateReport(DataSet ds)
    //{

    //    StringBuilder str = new StringBuilder("");
    //    str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
    //    str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
    //    str.Append("<td rowspan='2'>Dist Name  </td>");
    //    DataView view = new DataView(ds.Tables[1]);
    //    view.Sort = "CROP_VERID ASC";
    //    DataTable dtVariety = view.ToTable(true, "CROP_VERID", "Variety_Name");
    //    foreach (DataRow dr in dtVariety.Rows)
    //    {
    //        str.Append(" <td style='text-align: Right; padding-right:5px;' colspan='2'>" + dr["Variety_Name"].ToString() + "</td>");

    //    }
    //    str.Append(" <td style='text-align: Right; padding-right:5px;' rowspan='2'>Total</td>");
    //    str.Append("</tr>");

    //    str.Append("<tr  style='background-color:#CCCCCC; font-weight:bold;'>");

    //    foreach (DataRow dr in dtVariety.Rows)
    //    {
    //        str.Append(" <td style='text-align: Right; padding-right:5px;'>Dealer</td>");
    //        str.Append(" <td style='text-align: Right; padding-right:5px;'>PACS</td>");
    //    }
    //    str.Append("</tr>");
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        str.Append("<tr>");
    //        str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");

    //        double TotSale = 0;
    //        foreach (DataRow drv in dtVariety.Rows)
    //        {
    //            double DealerSale = 0;
    //            double PacsSale = 0;

    //            DataRow[] Datadealer = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "' AND Type='Dealer'");
    //            if (Datadealer.Length > 0)
    //            {
    //                foreach (DataRow value in Datadealer)
    //                {
    //                    DealerSale = DealerSale + double.Parse(value[5].ToString());
    //                    TotSale = TotSale + double.Parse(value[5].ToString());
    //                }
    //                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", DealerSale) + "</td>");
    //            }
    //            else
    //            {
    //                str.Append(" <td style='text-align: Right; padding-right:5px;'>&nbsp;</td>");
    //            }
    //            DataRow[] DataPACS = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "' AND Type='PACS'");
    //            if (DataPACS.Length > 0)
    //            {
    //                foreach (DataRow value in DataPACS)
    //                {
    //                    PacsSale = PacsSale + double.Parse(value[5].ToString());
    //                    TotSale = TotSale + double.Parse(value[5].ToString());
    //                }
    //                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", PacsSale) + "</td>");
    //            }
    //            else
    //            {
    //                str.Append(" <td style='text-align: Right; padding-right:5px;'>&nbsp;</td>");
    //            }
    //        }
    //        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotSale) + "</td>");
    //        str.Append("</tr>");
    //    }

    //    str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
    //    str.Append("<td>Grand Total</td>");
    //    foreach (DataRow dr in dtVariety.Rows)
    //    {
    //        string a = ds.Tables[1].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "' AND Type='Dealer'").ToString();
    //        string b = ds.Tables[1].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "' AND Type='PACS'").ToString();
    //        //str.Append("<td style='text-align: Right; padding-right:5px;'>" +  string.Format("{0:f2}", double.Parse(a)) + "</td>");
    //        if (a == "")
    //        {
    //            str.Append("<td style='text-align: Right; padding-right:5px;'>--</td>");
    //        }
    //        else
    //        {

    //            str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(a)) + "</td>");
    //        }
    //        if (b == "")
    //        {
    //            str.Append("<td style='text-align: Right; padding-right:5px;'>--</td>");
    //        }
    //        else
    //        {

    //            str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(b)) + "</td>");
    //        }

    //    }
    //    str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SALE)", "").ToString())) + "</td>");
    //    str.Append("</tr>");
    //    str.Append("</table>");
    //    litReport.Text = str.ToString();
    //}

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=VarierywiseLifting.xls";
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
    protected void drpSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCrop();
    }

    protected void drpSeassion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCrop();
    }
}
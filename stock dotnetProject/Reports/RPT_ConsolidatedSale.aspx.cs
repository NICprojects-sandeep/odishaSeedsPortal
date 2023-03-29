using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class Reports_RPT_ConsolidatedSale : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
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
        catch
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
                FillScheme();
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

            ds = new DataSet();
            ds = DsCrop();
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
            this.drpCropName.Items.Insert(0, "--- All ---");

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
    private void FillScheme()
    {
        try
        {

            ds = new DataSet();
            ds = DsScheme();
            drpScheme.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpScheme.DataSource = ds;
                    this.drpScheme.DataValueField = "SCHEME_CODE";
                    this.drpScheme.DataTextField = "SCHEME_NAME";
                    this.drpScheme.DataBind();
                }
            }
            this.drpScheme.Items.Insert(0, "--- All ---");

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
    private DataSet DsScheme()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT [SCHEME_CODE],[SCHEME_NAME] FROM [stock].[dbo].[mSCHEME] ORDER BY SCHEME_NAME ASC", con);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string sql = "RPTConsolidatedSale";
        string[] param = { "@Crop_Code", "@APP_TYPE", "@SEASON", "@SCHEME_CODE", "@FIN_YEAR", "@Agency", "@Dist_Code", "@MONTH", "@FRMDT", "@TODT" };
        object[] value = { drpCropName.SelectedValue.Trim() == "--- All ---" ? "0" : drpCropName.SelectedValue.Trim(), drpDealer.SelectedValue.ToString() == "--- All ---" ? "0" : drpDealer.SelectedValue.ToString(), drpSeason.SelectedValue.ToString() == "--- All ---" ? "0" : drpSeason.SelectedValue.ToString(), drpScheme.SelectedValue.ToString() == "--- All ---" ? "0" : drpScheme.SelectedValue.ToString(), drpSession.SelectedValue.ToString() == "--- All ---" ? "0" : drpSession.SelectedValue.ToString(), drpAgency.SelectedValue.ToString() == "--- All ---" ? "0" : drpAgency.SelectedValue.ToString(), drpDistrict.SelectedValue.Trim() == "--- All ---" ? System.Convert.DBNull : drpDistrict.SelectedValue.Trim(), drpMonth.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpMonth.SelectedValue.Trim(), txtfromdate.Text.Trim() == "" ? System.Convert.DBNull : txtfromdate.Text.Trim(), txtTodate.Text.Trim() == "" ? System.Convert.DBNull : txtTodate.Text.Trim() };
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
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='10' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>Dist Name  </td>");
        str.Append(" <td style='text-align: center; padding-right:5px;' colspan='3'>No. of Farmers</td>");
        str.Append(" <td style='text-align: center; padding-right:5px;' colspan='3'>Seed Sale in Qntl.</td>");
        str.Append(" <td style='text-align: center; padding-right:5px; width:50px;' rowspan='2'>View Details</td>");
        str.Append("</tr>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Registered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>Registered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td align='left' class='tbltd'>");
            string tmpstr111 = "<a href='javascript:popUp";
            string tempStr10 = "('RPT_ConsilidatedSaleDistWIse.aspx?Crop_Code=" + drpCropName.SelectedValue.Trim() + " &APP_TYPE= " + drpDealer.SelectedValue.Trim() + "&SEASON=" + drpSeason.SelectedValue.Trim() + " &SCHEME_CODE= " + drpScheme.SelectedValue.Trim() + " &Agency= " + drpAgency.SelectedValue.Trim() + " &Dist_Code= " + dr["Dist_Code"].ToString() + " &FIN_YEAR= " + drpSession.SelectedValue.ToString() + " &MONTH= " + drpMonth.SelectedValue.ToString() + "&FRMDT= " + txtfromdate.Text.Trim() + " &TODT= " + txtTodate.Text.Trim() + "')";
            string tmpstr211 = "'>";
            str.Append(tmpstr111.Replace('\'', '\"') + tempStr10 + tmpstr211.Replace('\'', '\"'));
            str.Append("" + dr["Dist_Name"].ToString() + "</td>");

            double RegdFarmer = 0;
            double UnRegdFarmer = 0;
            DataRow[] drRegdFarmer = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            if (drRegdFarmer.Length > 0)
            {
                foreach (DataRow value in drRegdFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    RegdFarmer = double.Parse(value[3].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }
            DataRow[] drUnRegdFarmer = ds.Tables[2].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            if (drUnRegdFarmer.Length > 0)
            {
                foreach (DataRow value in drUnRegdFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    UnRegdFarmer = double.Parse(value[3].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (RegdFarmer + UnRegdFarmer) + "</td>");

            double RegdFarmerSale = 0;
            double UnRegdFarmerSale = 0;
            DataRow[] drRegdFarmerSale = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            if (drRegdFarmer.Length > 0)
            {
                foreach (DataRow value in drRegdFarmerSale)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[2].ToString() + "</td>");
                    RegdFarmerSale = double.Parse(value[2].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }
            DataRow[] drUnRegdFarmerSale = ds.Tables[2].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            if (drUnRegdFarmerSale.Length > 0)
            {
                foreach (DataRow value in drUnRegdFarmerSale)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[2].ToString() + "</td>");
                    UnRegdFarmerSale = double.Parse(value[2].ToString());
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
            }

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", (RegdFarmerSale + UnRegdFarmerSale)) + "</td>");

            str.Append("<td align='right' class='tbltd'>");
            string tmpstr11 = "<a href='javascript:popUp";
            string tempStr1 = "('RPT_DatewiseSale.aspx?Crop_Code=" + drpCropName.SelectedValue.Trim() + " &APP_TYPE= " + drpDealer.SelectedValue.Trim() + "&SEASON=" + drpSeason.SelectedValue.Trim() + " &SCHEME_CODE= " + drpScheme.SelectedValue.Trim() + " &Agency= " + drpAgency.SelectedValue.Trim() + " &Dist_Code= " + dr["Dist_Code"].ToString() + " &FIN_YEAR= " + drpSession.SelectedValue.ToString() +  " &MONTH= " + drpMonth.SelectedValue.ToString() + "&FRMDT= " + txtfromdate.Text.Trim() + " &TODT= " + txtTodate.Text.Trim() + "')";
            string tmpstr21 = "'>";
            str.Append(tmpstr11.Replace('\'', '\"') + tempStr1 + tmpstr21.Replace('\'', '\"'));
            str.Append("<img src='../images/binoculars.png'  title='View Details' /></a>");
            str.Append("</td>");
            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Grand Total</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[1].Compute("Sum(REGFARMER)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[2].Compute("Sum(UNREGFARMER)", "").ToString() + "</td>");

        double REGFARMER = ds.Tables[1].Rows.Count > 0 ? double.Parse(ds.Tables[1].Compute("Sum(REGFARMER)", "").ToString()) : 0;
        double UNREGFARMER = ds.Tables[2].Rows.Count > 0 ? double.Parse(ds.Tables[2].Compute("Sum(UNREGFARMER)", "").ToString()) : 0;

        double SALEQTY = ds.Tables[1].Rows.Count > 0 ? double.Parse(ds.Tables[1].Compute("Sum(SALEQTY)", "").ToString()) : 0;
        double UNREGSALE = ds.Tables[2].Rows.Count > 0 ? double.Parse(ds.Tables[2].Compute("Sum(UNREGSALE)", "").ToString()) : 0;

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (REGFARMER + UNREGFARMER) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + SALEQTY + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + UNREGSALE + "</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (SALEQTY + UNREGSALE) + "</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>&nbsp;</td>");
        str.Append("</tr>");

        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=ConsolidatedSeedSale.xls";
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
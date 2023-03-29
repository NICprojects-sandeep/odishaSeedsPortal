using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
public partial class Reports_RPT_ScheemwisePaymentDtl : System.Web.UI.Page
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
                btnPrint.Enabled = false;
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

        SqlCommand cmd = new SqlCommand("SELECT [SCHEME_CODE] ,[SCHEME_NAME] FROM [stock].[dbo].[mSCHEME] ORDER BY SCHEME_NAME ASC", con);
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
        string sql = "STOCK_SCHEMEWISEPAYMENTDTL";
        string[] param = { "@CROP_ID1", "@SCHEME_CODE1", "@FIN_YR", "@Seassion", "@MONTH", "@FRMDT", "@TODT", "@USER_TYPE", "@Dist_Code" };
        object[] value = { drpCropName.SelectedValue.Trim(), drpScheme.SelectedValue.Trim(), drpSession.SelectedValue.Trim(), drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim(), drpMonth.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpMonth.SelectedValue.Trim(), txtfromdate.Text.Trim() == "" ? System.Convert.DBNull : txtfromdate.Text.Trim(), txtTodate.Text.Trim() == "" ? System.Convert.DBNull : txtTodate.Text.Trim(), drpUser.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpUser.SelectedValue.Trim(), drpDistrict.SelectedValue.Trim() == "--- All ---" ? System.Convert.DBNull : drpDistrict.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[1].Rows.Count > 0)
        {
            btnExport.Enabled = true;
            btnPrint.Enabled = true;
            GenerateReport(ds);
        }
        else
        {
            btnExport.Enabled = false;
            btnPrint.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }

    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='10' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        if (drpScheme.SelectedValue.Trim() != "2054")
        {
            str.Append("<td  rowspan='3' >Sl. No.  </td>");
            str.Append("<td  rowspan='3' >Dist Name  </td>");
            str.Append(" <td style='text-align: center; padding-right:5px;'  colspan='4' rowspan='2'>Quantity Distributed (In Qtl.)</td>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='8' >" + drpScheme.SelectedItem.Text.ToString() + "</td>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='8' >STATE PLAN</td>");
            str.Append("</tr>");

            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='4'>No. of Farmers Benifited</td>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='4'>Subsidy Amount</td>");

            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='4'>No. of Farmers Benifited</td>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='4'>Subsidy Amount</td>");
            str.Append("</tr>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");


            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");


            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");


            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");


        }
        else
        {
            str.Append("<td  rowspan='3' >Sl. No.  </td>");
            str.Append("<td  rowspan='3' >Dist Name  </td>");
            str.Append(" <td style='text-align: center; padding-right:5px;'  colspan='4' rowspan='2'>Quantity Distributed (In Qtl.)</td>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='8' >STATE PLAN</td>");

            str.Append("</tr>");

            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='4'>No. of Farmers Benifited</td>");
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='4'>Subsidy Amount</td>");

            str.Append("</tr>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>Gen</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SC</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>ST</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");

        }


        str.Append("</tr>");
        int sl = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");

            DataRow[] drData = ds.Tables[1].Select("VCHDISTID= " + dr["NDist_Code"].ToString() + "");
            if (drData.Length > 0)
            {
                foreach (DataRow value in drData)
                {
                    str.Append("<td style='width:50px;'>" + sl.ToString() + "</td>");
                    sl++;
                    if (drpScheme.SelectedValue.Trim() != "2054")
                    {
                        str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["Sale_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["Sale_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["Sale_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["Sale_GEN"].ToString()) + double.Parse(value["Sale_SC"].ToString()) + double.Parse(value["Sale_ST"].ToString())).ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["GOIFRM_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["GOIFRM_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["GOIFRM_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["GOIFRM_GEN"].ToString()) + double.Parse(value["GOIFRM_SC"].ToString()) + double.Parse(value["GOIFRM_ST"].ToString())).ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["GOIAMNT_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["GOIAMNT_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["GOIAMNT_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["GOIAMNT_GEN"].ToString()) + double.Parse(value["GOIAMNT_SC"].ToString()) + double.Parse(value["GOIAMNT_ST"].ToString())).ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["SPFRM_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SPFRM_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SPFRM_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["SPFRM_GEN"].ToString()) + double.Parse(value["SPFRM_SC"].ToString()) + double.Parse(value["SPFRM_ST"].ToString())).ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["SP_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SP_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SP_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["SP_GEN"].ToString()) + double.Parse(value["SP_SC"].ToString()) + double.Parse(value["SP_ST"].ToString())).ToString() + "</td>");

                    }
                    else
                    {
                        str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["Sale_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["Sale_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["Sale_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["Sale_GEN"].ToString()) + double.Parse(value["Sale_SC"].ToString()) + double.Parse(value["Sale_ST"].ToString())).ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["SPFRM_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SPFRM_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SPFRM_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["SPFRM_GEN"].ToString()) + double.Parse(value["SPFRM_SC"].ToString()) + double.Parse(value["SPFRM_ST"].ToString())).ToString() + "</td>");

                        str.Append("<td style='text-align: Right;'>" + value["SP_GEN"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SP_SC"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SP_ST"].ToString() + "</td>");
                        str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["SP_GEN"].ToString()) + double.Parse(value["SP_SC"].ToString()) + double.Parse(value["SP_ST"].ToString())).ToString() + "</td>");

                        //str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(value["TOTSPAMNT"].ToString()) - double.Parse(value["SP"].ToString())).ToString() + "</td>");

                    }
                }
            }
            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        if (drpScheme.SelectedValue.Trim() != "2054")
        {
            str.Append("<td style='text-align: Right;' colspan='2'>Total :</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(Sale_GEN)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(Sale_SC)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(Sale_ST)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(Sale_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(Sale_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(Sale_ST)", "").ToString())).ToString() + "</td>");



            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(GOIFRM_GEN)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(GOIFRM_SC)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(GOIFRM_ST)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(GOIFRM_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(GOIFRM_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(GOIFRM_ST)", "").ToString())).ToString() + "</td>");


            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(GOIAMNT_GEN)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(GOIAMNT_SC)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(GOIAMNT_ST)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(GOIAMNT_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(GOIAMNT_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(GOIAMNT_ST)", "").ToString())).ToString() + "</td>");

            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_GEN)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_SC)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_ST)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(SPFRM_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_ST)", "").ToString())).ToString() + "</td>");

            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SP_GEN)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SP_SC)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SP_ST)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(SP_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SP_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SP_ST)", "").ToString())).ToString() + "</td>");
        }
        else
        {
            str.Append("<td style='text-align: Right;' colspan='2'>Total :</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(Sale_GEN)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(Sale_SC)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(Sale_ST)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(Sale_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(Sale_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(Sale_ST)", "").ToString())).ToString() + "</td>");

            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_GEN)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_SC)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_ST)", "").ToString()) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(SPFRM_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SPFRM_ST)", "").ToString())).ToString() + "</td>");

            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SP_GEN)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SP_SC)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SP_ST)", "").ToString())) + "</td>");
            str.Append("<td  style='text-align: Right; padding-right:5px;'>" + (double.Parse(ds.Tables[1].Compute("Sum(SP_GEN)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SP_SC)", "").ToString()) + double.Parse(ds.Tables[1].Compute("Sum(SP_ST)", "").ToString())).ToString() + "</td>");

        }
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=SchemewisePaymentDtl.xls";
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
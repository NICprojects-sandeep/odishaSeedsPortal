using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
public partial class Reports_RPT_Schemewisedtl : System.Web.UI.Page
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
                btnExport.Enabled = false;
                FillFinYr();
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

        SqlCommand cmd = new SqlCommand("SELECT SCHEME_CODE1 [SCHEME_CODE],[SCHEME_NAME] FROM [stock].[dbo].[mSCHEME] ORDER BY SCHEME_NAME ASC", con);
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
        string sql = "RPT_SchemewiseConsol_New";
        string[] param = { "@SCHEME_CODESP", "@FIN_YEAR", "@SEASON", "@CROP_ID", "@VCHDISTID", "@APP_TYPE" };
        object[] value = { drpScheme.SelectedValue.Trim(), drpSession.SelectedValue.ToString(), drpSeason.SelectedValue.ToString() == "0" ? System.Convert.DBNull : drpSeason.SelectedValue.ToString(), drpCropName.SelectedValue.Trim() == "--- All ---" ? System.Convert.DBNull : drpCropName.SelectedValue.Trim(), drpDistrict.SelectedValue.Trim() == "--- All ---" ? System.Convert.DBNull : drpDistrict.SelectedValue.Trim(), drpDealer.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpDealer.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
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
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 12px; ' border='1' cellpadding='5' cellspacing='5' class='tbltxt'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'><td rowspan='3'>Dist.</td><td rowspan='3' style='text-align: Right;'>No.of dealers</td><td rowspan='3' style='text-align: Right;'>No.of transactions</td>");
        str.Append("<td rowspan='3' style='text-align: Right;'>No.of distinct farmers</td><td colspan='3' style='text-align: center;'>Total subsidy involved</td><td colspan='3' style='text-align: center;'>Total subsidy paid</td></tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'><td colspan='2' style='text-align: center;'>" + drpScheme.SelectedItem.Text.Trim() + "</td><td rowspan='2' style='text-align: Right;'>Total</td style='text-align: Right;'><td colspan='2' style='text-align: center;'>" + drpScheme.SelectedItem.Text.Trim() + "</td><td style='text-align: Right;' rowspan='2'>Total</td></tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'><td style='text-align: Right;'>GOI</td><td style='text-align: Right;'>State Plan</td><td style='text-align: Right;'>GOI</td><td style='text-align: Right;'>State Plan</td></tr>");
        DataTable dt = ds.Tables[0];
        //Distinct Variety Code and Name
        DataView view = new DataView(dt);
        DataTable dist = view.ToTable(true, "Dist_Name", "NDist_Code");


        foreach (DataRow dr in dist.Rows)
        {
            string filter = string.Empty;
           
            if (filter != string.Empty)
            {
                filter = filter + "NDist_Code='" + dr["NDist_Code"].ToString() + "'";
            }
            else
            {
                filter = "NDist_Code=" + dr["NDist_Code"].ToString();
            }

            str.Append("<tr>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");
            string TotDealer = dt.Compute("Sum(TotDealer)", filter).ToString();
            str.Append("<td style='text-align: Right;'>" + TotDealer + "</td>");

            string Tottrans = dt.Compute("Sum(Tottrans)", filter).ToString();
            str.Append("<td style='text-align: Right;'>" + Tottrans + "</td>");

            string TotFarmer = dt.Compute("Sum(TotFarmer)", filter).ToString();
            str.Append("<td style='text-align: Right;'>" + TotFarmer + "</td>");

            string TOTGOIAMNT = dt.Compute("Sum(TOTGOIAMNT)", filter).ToString();
            str.Append("<td style='text-align: Right;'>" + TOTGOIAMNT + "</td>");

            string TOTSPAMNT = dt.Compute("Sum(TOTSPAMNT)", filter).ToString();
            str.Append("<td style='text-align: Right;'>" + TOTSPAMNT + "</td>");

            double goi = double.Parse(TOTGOIAMNT == "" ? "0" : TOTGOIAMNT);
            double sp = double.Parse(TOTSPAMNT == "" ? "0" : TOTSPAMNT);

            string GOIpaid = dt.Compute("Sum(GOIpaid)", filter).ToString();
            double pgoi = double.Parse(GOIpaid == "" ? "0" : GOIpaid);

            string SPpaid = dt.Compute("Sum(SPpaid)", filter).ToString();
            double psp = double.Parse(SPpaid == "" ? "0" : SPpaid);


            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (goi + sp)).ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + GOIpaid + "</td>");
            str.Append("<td style='text-align: Right;'>" + SPpaid + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (pgoi + psp)).ToString() + "</td>");
            str.Append("</tr>");
        }
        //if(drpDistrict.SelectedIndex>0)
        //{

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Grand Total</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + dt.Compute("Sum(TotDealer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + dt.Compute("Sum(Tottrans)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + dt.Compute("Sum(TotFarmer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + dt.Compute("Sum(TOTGOIAMNT)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + dt.Compute("Sum(TOTSPAMNT)", "").ToString() + "</td>");

        double GOI_tot = double.Parse(dt.Compute("Sum(TOTGOIAMNT)", "").ToString());
        double SP_tot = double.Parse(dt.Compute("Sum(TOTSPAMNT)", "").ToString());
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + string.Format("{0:f2}", (GOI_tot + SP_tot)) + "</td>");


        str.Append("<td style='text-align: Right; padding-right:5px;' >" + dt.Compute("Sum(GOIpaid)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + dt.Compute("Sum(SPpaid)", "").ToString() + "</td>");
        double PGOI_tot = double.Parse(dt.Compute("Sum(GOIpaid)", "").ToString());
        double PSP_tot = double.Parse(dt.Compute("Sum(SPpaid)", "").ToString());
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + string.Format("{0:f2}", (PGOI_tot + PSP_tot)) + "</td>");

        str.Append("</tr>");
        // }


        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=SchemewiseConsolidated.xls";
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
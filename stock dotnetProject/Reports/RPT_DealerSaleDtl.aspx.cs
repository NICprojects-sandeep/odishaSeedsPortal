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


public partial class Reports_RPT_DealerSaleDtl : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;

    //protected void Page_PreInit(object sender, EventArgs e)
    //{

    //    if (Session["Name"] != null || Session["UserType"] != null)
    //    {
    //        if (this.Session["UserType"].ToString() == "ADMI")
    //        {
    //            this.Page.MasterPageFile = "../MasterPages/Admin.master";
    //            return;
    //        }
    //        if (this.Session["UserType"].ToString() == "SEAD")
    //        {
    //            this.Page.MasterPageFile = "../MasterPages/SeedAdmin.master";
    //            return;
    //        }

    //    }
    //    else
    //    {
    //        Response.Redirect("../Login.aspx");
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (this.Session["UserID"] == null || this.Session["AuthToken"] == null || base.Request.Cookies["AuthToken"] == null)
        //{
        //    this.utl.SessionReset();
        //}
        //else
        //{
        //if (!this.Session["AuthToken"].ToString().Equals(base.Request.Cookies["AuthToken"].Value))
        //{
        //    this.utl.SessionReset();
        //    return;
        //}
        //if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") || !(this.Session["FirstLogin"].ToString() == "N"))
        //{
        //    base.Response.Redirect("../Unauthorised.aspx", true);
        //    return;
        //}
        if (!this.Page.IsPostBack)
        {
            btnExport.Enabled = false;
            FillFinYr();
            FillDist();
            drpDistrict_SelectedIndexChanged(null, null);
            FillScheme();
        }
        //}
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

        SqlCommand cmd = new SqlCommand("SELECT b.New_Scheme_Code [SCHEME_CODE],A.[SCHEME_NAME] FROM [stock].[dbo].[mSCHEME] a inner join [FARMERDB].[dbo].[SchemeCode_Master] b on a.SCHEME_CODE collate Latin1_General_CI_AI=b.Scheme_Code ORDER BY A.SCHEME_NAME ASC", con);
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
    private void FillDist()
    {
        DataTable ds = new DataTable();
        string sql = "select dist_code,Mod_dist_name from [dafpseed].[dbo].[dist]";
        string[] param = { };
        object[] value = { };
        ds = dbsAppStock.param_passing_fetchDataTable(sql, param, value);
        this.drpDistrict.DataSource = ds;
        this.drpDistrict.DataValueField = "Dist_Code";
        this.drpDistrict.DataTextField = "Mod_dist_name";
        this.drpDistrict.DataBind();
        this.drpDistrict.Items.Insert(0, "--- All ---");
    }

    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();
        string sql = "select block_code,block_name  from  [dafpseed].[dbo].[block] where left(block_code,2)=@dist order by block_name";
        string[] param = { "@dist" };
        object[] value = { drpDistrict.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataTable(sql, param, value);
        this.drpBlock.DataSource = ds;
        this.drpBlock.DataValueField = "block_code";
        this.drpBlock.DataTextField = "block_name";
        this.drpBlock.DataBind();
        this.drpBlock.Items.Insert(0, "--- All ---");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "RPTDealerSale";
        string[] param = { "@APP_TYPE", "@Dist_Code", "@Blk_Code", "@FRMDT", "@TODT", "@SCHEME_CODE", "@Crop_Code", "@SEASON", "@FIN_YEAR" };
        object[] value = { drpDealer.SelectedValue.ToString() == "--- All ---" ? "0" : drpDealer.SelectedValue.ToString(), drpDistrict.SelectedValue.ToString() == "--- All ---" ? System.Convert.DBNull : drpDistrict.SelectedValue.ToString(), drpBlock.SelectedValue.ToString() == "--- All ---" ? System.Convert.DBNull : drpBlock.SelectedValue.ToString(), txtfromdate.Text.Trim() == "" ? System.Convert.DBNull : txtfromdate.Text.Trim(), txtTodate.Text.Trim() == "" ? System.Convert.DBNull : txtTodate.Text.Trim(), drpScheme.SelectedValue.ToString() == "--- All ---" ? "0" : drpScheme.SelectedValue.ToString(), drpCrop.SelectedValue.Trim() == "--- All ---" ? "0" : drpCrop.SelectedValue.Trim(), drpSeason.SelectedValue.ToString() == "--- All ---" ? "0" : drpSeason.SelectedValue.ToString(), drpSession.SelectedValue.ToString() == "--- All ---" ? "0" : drpSession.SelectedValue.ToString() };
        Session["APP_TYPE"] = drpDealer.SelectedValue.ToString();
        Session["FRMDT"] = txtfromdate.Text.Trim();
        Session["TODT"] = txtTodate.Text.Trim();
        Session["SCHEME_CODE"] = drpScheme.SelectedValue.ToString();
        Session["Crop_Code"] = drpCrop.SelectedValue.ToString();
        Session["SEASON"] = drpSeason.SelectedValue.ToString();
        Session["FIN_YEAR"] = drpSession.SelectedValue.ToString();
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds != null)
        {
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
        else
        {
            btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }
    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; text-align:center; ' border='1' cellpadding='5' cellspacing='5' class='tbltxt'>");
        str.Append("<tr  style='background-color:#CCCCCC; font-weight:bold;'><td>Sl. No.</td><td style=' text-align: Left; padding-right:5px;'>LIC No.</td> <td style=' text-align: Left; padding-right:5px;'>Name</td><td>No. of Transactions</td><td>No. of Transaction Paid </td> <td>Amount to be paid  </td>  <td>Amount paid</td>  </tr>");

        int sl = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td style='text-align: left; padding-right:5px;' >" + sl.ToString() + "</td>");
            str.Append("<td style='text-align: left; padding-right:5px; white-space:nowrap;' >" + dr["LIC_NO"].ToString() + "</td>");
            str.Append("<td style='text-align: left; padding-right:5px;' >" + dr["APP_FIRMNAME"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' ><div style='float:left;'>");
            string tmpstr11 = "<a href='javascript:popUp";
            string tempStr1 = "('DealerSaleDtl.aspx?LIC_NO=" + dr["LIC_NO"].ToString() + "&TYPE=A')";
            string tmpstr21 = "'>";
            str.Append(tmpstr11.Replace('\'', '\"') + tempStr1 + tmpstr21.Replace('\'', '\"'));
            str.Append("<img src='../images/binoculars.png'  title='View Details' /></a></div>");

            str.Append("" + dr["NOOFTRANS"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' ><div style='float:left;'>");
            string tmpstr111 = "<a href='javascript:popUp";
            string tempStr11 = "('DealerSaleDtl.aspx?LIC_NO=" + dr["LIC_NO"].ToString() + "&TYPE=P')";
            string tmpstr211 = "'>";
            str.Append(tmpstr111.Replace('\'', '\"') + tempStr11 + tmpstr211.Replace('\'', '\"'));
            str.Append("<img src='../images/binoculars.png'  title='View Details' /></a></div>");

            str.Append("" + dr["PAIDTRANS"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["AMNT_PAID"].ToString() + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;' >" + dr["PAID_AMNT"].ToString() + "</td>");
            str.Append("</tr>");
            sl++;
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append("<td style=' text-align: right; padding-right:5px;' colspan='3'>Grand Total</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(NOOFTRANS)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(PAIDTRANS)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(AMNT_PAID)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[0].Compute("Sum(PAID_AMNT)", "").ToString() + "</td>");

        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DailyReport.xls";
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
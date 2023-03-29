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

public partial class Reports_RPT_DistwisePACS : System.Web.UI.Page
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
            if (this.Session["UserType"].ToString() == "RCSA")
            {
                this.Page.MasterPageFile = "../MasterPages/RCSA.master";
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
            if (!(this.Session["UserType"].ToString() == "RCSA") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                btnExport.Enabled = false;
                FillCrop();
                FillDist();
            }
        }
    }

    private void FillDist()
    {
        try
        {

            ds = new DataSet();
            ds = DsDist();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpDist.DataSource = ds;
                    this.drpDist.DataValueField = "dist_code";
                    this.drpDist.DataTextField = "Mod_dist_name";
                    this.drpDist.DataBind();
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

    private DataSet DsDist()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT [dist_code],[Mod_dist_name] FROM [dafpSeed].[dbo].[dist] order by Mod_dist_name", con);
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
    protected void FillCrop()
    {
        try
        {

            ds = new DataSet();
            ds = DsCrop();
            drpCropName.Items.Clear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.drpCropName.DataSource = ds;
                this.drpCropName.DataValueField = "Crop_Code";
                this.drpCropName.DataTextField = "Crop_Name";
                this.drpCropName.DataBind();
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
        string sql = "STOCK_PacsDetails";
        string[] param = { "@CROP_ID", "@DIST_CODE" };
        object[] value = { drpCropName.SelectedValue.Trim(), drpDist.SelectedValue.Trim() };
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
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");

        str.Append("<td>Pacs Name</td>");
        str.Append("<td>Variety Name  </td>");

        str.Append("<td style='text-align: Right;'>Receive Qty (in Qntl.)  </td>");
        str.Append("<td style='text-align: Right;'>No. of Transaction</td>");
        str.Append("<td style='text-align: Right;'>Sale Qty (in Qntl.)   </td>");
        str.Append("<td style='text-align: Right;'>Balance (in Qntl.)   </td>");
        str.Append("</tr>");
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + item["APP_FIRMNAME"].ToString() + "</td>");
            str.Append("<td>" + item["Variety_Name"].ToString() + "</td>");

            str.Append("<td style='text-align: Right;'>" + item["RECEIVE_QTY"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + ds.Tables[1].Compute("Sum(FARMER)", "APP_FIRMNAME='" + item["APP_FIRMNAME"].ToString() + "' AND CROP_VERID='" + item["CROP_VERID"].ToString() + "'").ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["SALE_QTY"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (double.Parse(item["RECEIVE_QTY"].ToString()) - double.Parse(item["SALE_QTY"].ToString()))) + "</td>");
            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Total :</td>");
        str.Append("<td>&nbsp;</td>");

        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(RECEIVE_QTY)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[1].Compute("Sum(FARMER)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(SALE_QTY)", "").ToString() + "</td>");

        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (double.Parse(ds.Tables[0].Compute("Sum(RECEIVE_QTY)", "").ToString()) - double.Parse(ds.Tables[0].Compute("Sum(SALE_QTY)", "").ToString()))) + "</td>");

        str.Append("</tr>");
        str.Append("</table>");

        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=PacsDetails.xls";
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
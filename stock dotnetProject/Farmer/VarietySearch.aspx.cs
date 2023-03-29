using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Farmer_VarietySearch : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCrop();
            FillDist();
        }
    }

    private void FillDist()
    {

        try
        {

            ds = new DataSet();
            ds = DsDist();
            drpDist.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpDist.DataSource = ds;
                    this.drpDist.DataValueField = "Dist_Name";
                    this.drpDist.DataTextField = "Dist_Name";
                    this.drpDist.DataBind();
                }
            }
            this.drpDist.Items.Insert(0, "--- SELECT ---");

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

        SqlCommand cmd = new SqlCommand("SELECT [Dist_Code],[Dist_Name] FROM [Stock].[dbo].[Stock_District]", con);
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
    protected void drpCropName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpCropName.SelectedIndex > 0)
        {
            FillCropVar();
        }

        else
        {

            drpCropVar.Items.Clear();

        }
    }

    private void FillCropVar()
    {
        try
        {

            ds = new DataSet();
            ds = DsVariety();
            drpCropVar.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpCropVar.DataSource = ds;
                    this.drpCropVar.DataValueField = "Variety_Code";
                    this.drpCropVar.DataTextField = "Variety_Name";
                    this.drpCropVar.DataBind();
                }
            }
            this.drpCropVar.Items.Insert(0, "--- SELECT ---");

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

    private DataSet DsVariety()
    {

        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("select Variety_Code, Variety_Name from dbo.mCropVariety where Crop_Code=@Crop_Code AND is_active = 1 ORDER BY Variety_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Crop_Code", drpCropName.SelectedValue.Trim());
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

        string sql = "STOCK_RPT_DealerwiseStock";
        string[] param = { "@Dist", "@Crop_ID", "@CROP_VERID" };
        object[] value = { drpDist.SelectedValue.Trim().Substring(0, 3), drpCropName.SelectedValue.Trim(), drpCropVar.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds);
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }

    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Contact Details </td>");
        str.Append("<td>Licence No.</td>");
        str.Append("<td style='text-align: Right;'>Available (Stock in qtl.)</td>");
        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>Dealer Name : " + dr["APPNAME"].ToString() + " </br> Farm Name : " + dr["APP_FIRMNAME"].ToString() + " </br> Mobile No. : " + dr["APPMOB_NO"].ToString() + " </br> Address : " + dr["APPADDRESS"].ToString() + "</td>");
            str.Append("<td>" + dr["LIC_NO"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["QTY"].ToString() + "</td>");
            str.Append("</tr>");
        }
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
}
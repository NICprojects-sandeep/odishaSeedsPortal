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
public partial class Reports_RPT_DealerwiseTransactioDTL : System.Web.UI.Page
{
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillFinYr();
            FillCrop();
            FillDist();
            btnPrint.Enabled = false;
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
                    this.drpDist.DataValueField = "Dist_Code";
                    this.drpDist.DataTextField = "Dist_Name";
                    this.drpDist.DataBind();
                }
            }
            this.drpDist.Items.Insert(0, "--- ALL ---");

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
            this.drpCropName.Items.Insert(0, "--- Select ---");

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
        if (drpDist.SelectedIndex > 0)
        {
            btnPrint.Enabled = true;
        }
        else
        {
            btnPrint.Enabled = false;
        }

        string sql = "STOCK_BLOCKWISESALE";
        string[] param = { "@DDA_CODE", "@CROP_Code", "@FIN_YEAR", "@SEASON" };
        object[] value = { drpDist.SelectedValue.Trim() == "--- ALL ---" ? System.Convert.DBNull : drpDist.SelectedValue.Trim(), drpCropName.SelectedValue.Trim() == "--- ALL ---"?System.Convert.DBNull:drpCropName.SelectedValue.Trim(), drpSession.SelectedValue.Trim(), drpSeason.SelectedValue.ToString() == "0" ? System.Convert.DBNull : drpSeason.SelectedValue.Trim() };
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
        DataView view = new DataView(ds.Tables[0]);
        DataTable dtDist = view.ToTable(true, "DDA_CODE", "DDA_NAME");

        StringBuilder str = new StringBuilder("");
        foreach (DataRow dr in dtDist.Rows)
        {
            str.Append("<div style='font-size:20px'>" + dr["DDA_NAME"].ToString() + "</div>");
            DataRow[] Data = ds.Tables[0].Select("DDA_CODE= " + dr["DDA_CODE"].ToString() + "");
            {
                if (Data.Length > 0)
                {
                    str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
                    str.Append("<td>Sl. No.</td>");
                    str.Append("<td>Block Name</td>");
                    str.Append("<td style='text-align: Right;'>Lifting Done(Received) Quantity</td>");
                    str.Append("<td style='text-align: Right;'>Physical Sale Quantity</td>");
                    str.Append("<td style='text-align: Right;'>Online Sale Quantity</td>");
                    str.Append("<td style='text-align: Right; padding-right:5px; width:50px;'>View Details </td>");
                    str.Append("</tr>");
                    int i = 1;
                    foreach (DataRow value in Data)
                    {


                        str.Append("<tr>");
                        str.Append("<td>" + i.ToString() + "</td>");
                        str.Append("<td>" + value["BLK_NAME"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["ACTUAL_RCV"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["SaleQty"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right;'>" + value["ACTUAL_SALE"].ToString() + "</td>");
                        str.Append("<td style='text-align: Right; padding-right:5px;'>");

                        string tmpstr111 = "<a href='javascript:popUp";
                        string tempStr11 = "('rptDealerwisePhysicalEntry.aspx?AAO_CODE=" + value["AAO_CODE"].ToString() + " &Crop_Code= " + drpCropName.SelectedValue.Trim() + " &Season=" + drpSeason.SelectedValue.ToString() + " &FIN_YEAR="+drpSession.SelectedValue.Trim()+"')";
                        string tmpstr211 = "'>";
                        str.Append(tmpstr111.Replace('\'', '\"') + tempStr11 + tmpstr211.Replace('\'', '\"'));
                        str.Append("<img src='../images/binoculars.png'  title='View Details' /></a>");

                        str.Append("</td>");
                        str.Append("</tr>");
                        i++;
                    }

                    string SaleQty = ds.Tables[0].Compute("Sum(SaleQty)", "DDA_CODE= " + dr["DDA_CODE"].ToString() + "").ToString();
                    string ACTUAL_SALE = ds.Tables[0].Compute("Sum(ACTUAL_SALE)", "DDA_CODE= " + dr["DDA_CODE"].ToString() + "").ToString();
                    string ACTUAL_RCV = ds.Tables[0].Compute("Sum(ACTUAL_RCV)", "DDA_CODE= " + dr["DDA_CODE"].ToString() + "").ToString();
                    str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
                    str.Append("<td style='text-align: Right;'></td>");
                    str.Append("<td >Total : </td>");
                    str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(ACTUAL_RCV)) + "</td>");
                    str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(SaleQty)) + "</td>");
                    str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", Convert.ToDecimal(ACTUAL_SALE)) + "</td>");
                    str.Append("<td style='text-align: Right;'>&nbsp;</td>");

                    str.Append("</tr>");
                    str.Append("</table>");
                }

            }
        }


        litReport.Text = str.ToString();
    }
}
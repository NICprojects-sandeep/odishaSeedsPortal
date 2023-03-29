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

public partial class Reports_RPT_SchmCatStock : System.Web.UI.Page
{
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            littime.Text = "<div style='font-family:Tahoma; font-size:12px;   font-weight:bold;'>" + littime.Text + " " + String.Format("{0:D}", dt) + " Time : " + String.Format("{0:T}", dt) + "</div>";
            FillCrop();
            FillFinYr();
        }
        btnExport.Enabled = false;
        btnPrint.Enabled = false;
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
                    this.drpCropName.Items.Insert(0, "--- All ---");
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

        SqlCommand cmd = new SqlCommand("SELECT distinct Crop_Code,Crop_Name FROM STOCK_FARMER a inner join mCrop b on a.CROP_ID=b.Crop_Code WHERE is_active = 1 ORDER BY Crop_Name ASC", con);
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
    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        btnPrint.Enabled = true;

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Scheme Name</td>");
        str.Append("<td style='text-align: Right;'>No of Distinct Farmer</td>");
        str.Append("<td style='text-align: Right;'>Total Sale (in Qtl)</td>");
        str.Append("<td style='text-align: Right;'>Total Amount (in Rs.)</td>");
        str.Append("</tr>");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["SCHEME"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TotFarmer"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_QTL"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOTGOIAMNT"].ToString() + "</td>");
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Right;'>Total :</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(TotFarmer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(TOT_QTL)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(TOTGOIAMNT)", "").ToString() + "</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td colspan='4' style='font-weight:bold;'>Total Distinct Farmer irrespective of schemes  : " + ds.Tables[1].Rows[0][0].ToString() + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    private void GenerateReport1(DataSet ds)
    {

        btnExport.Enabled = true;
        btnPrint.Enabled = true;
        DataView view = new DataView(ds.Tables[0]);
        DataTable scheem = view.ToTable(true, "SCHEME");
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Center;'>Scheme Name</td>");
        str.Append("<td colspan='4' style='text-align: Center;'>No of distinct Farmer</td>");
        str.Append("<td colspan='4' style='text-align: Center;'>Total Seed Sold</td>");
        str.Append("<td colspan='4' style='text-align: Center;'>Total Subsidy involved</td>");
        str.Append("<td colspan='4' style='text-align: Center;'>Total Subsidy Paid</td>");
        str.Append("</tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td></td>");
        str.Append("<td style='text-align: Center;'>Normal</td>");
        str.Append("<td style='text-align: Center;'>SCP</td>");
        str.Append("<td style='text-align: Center;'>TASP</td>");
        str.Append("<td style='text-align: Center;'>TOTAL</td>");
        str.Append("<td style='text-align: Center;'>Normal</td>");
        str.Append("<td style='text-align: Center;'>SCP</td>");
        str.Append("<td style='text-align: Center;'>TASP</td>");
        str.Append("<td style='text-align: Center;'>TOTAL</td>");
        str.Append("<td style='text-align: Center;'>Normal</td>");
        str.Append("<td style='text-align: Center;'>SCP</td>");
        str.Append("<td style='text-align: Center;'>TASP</td>");
        str.Append("<td style='text-align: Center;'>TOTAL</td>");
        str.Append("<td style='text-align: Center;'>Normal</td>");
        str.Append("<td style='text-align: Center;'>SCP</td>");
        str.Append("<td style='text-align: Center;'>TASP</td>");
        str.Append("<td style='text-align: Center;'>TOTAL</td>");
        str.Append("</tr>");
        decimal TotFarmerGen = 0;
        decimal TotFarmerSC = 0;
        decimal TotFarmerST = 0;
        decimal TotSoldGen = 0;
        decimal TotSoldSC = 0;
        decimal TotSoldST = 0;
        decimal TotSALEGen = 0;
        decimal TotSALESC = 0;
        decimal TotSALEST = 0;
        decimal TotSubGen = 0;
        decimal TotSubSC = 0;
        decimal TotSubST = 0;
        foreach (DataRow dr in scheem.Rows)
        {
            decimal FarmerGen = 0;
            decimal FarmerSC = 0;
            decimal FarmerST = 0;
            decimal QTLGen = 0;
            decimal QtlSC = 0;
            decimal QtlST = 0;
            decimal SALEGen = 0;
            decimal SALESC = 0;
            decimal SALEST = 0;
            decimal SubGen = 0;
            decimal SubSC = 0;
            decimal SubST = 0;

            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");

            str.Append("<td style='text-align: Left;'>" + dr["SCHEME"].ToString() + "</td>");

            DataRow[] DistFarmerGen = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='GENERAL'");
            if (DistFarmerGen.Length > 0)
            {
                foreach (DataRow value in DistFarmerGen)
                {
                    FarmerGen = Convert.ToDecimal(value[2].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[2].ToString() + "</td>");
                    TotFarmerGen += FarmerGen;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] DistFarmerSC = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='SC'");
            if (DistFarmerSC.Length > 0)
            {
                foreach (DataRow value in DistFarmerSC)
                {
                    FarmerSC = Convert.ToDecimal(value[2].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[2].ToString() + "</td>");
                    TotFarmerSC += FarmerSC;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            DataRow[] DistFarmerST = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='ST'");
            if (DistFarmerST.Length > 0)
            {
                foreach (DataRow value in DistFarmerST)
                {
                    FarmerST = Convert.ToDecimal(value[2].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[2].ToString() + "</td>");
                    TotFarmerST += FarmerST;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }            
            str.Append("<td style='text-align: Right;'>" + (FarmerGen + FarmerSC + FarmerST).ToString() + "</td>");
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            DataRow[] DistQTLGen = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='GENERAL'");
            if (DistQTLGen.Length > 0)
            {
                foreach (DataRow value in DistQTLGen)
                {
                    QTLGen = Convert.ToDecimal(value[4].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[4].ToString() + "</td>");
                    TotSoldGen += QTLGen;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] DistQTLSC = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='SC'");
            if (DistQTLSC.Length > 0)
            {
                foreach (DataRow value in DistQTLSC)
                {
                    QtlSC = Convert.ToDecimal(value[4].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[4].ToString() + "</td>");
                    TotSoldSC += QtlSC;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            DataRow[] DistQTLST = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='ST'");
            if (DistQTLST.Length > 0)
            {
                foreach (DataRow value in DistQTLST)
                {
                    QtlST = Convert.ToDecimal(value[4].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[4].ToString() + "</td>");
                    TotSoldST += QtlST;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            str.Append("<td style='text-align: Right;'>" + (QTLGen + QtlSC + QtlST).ToString() + "</td>");
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            DataRow[] DistSALEGen = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='GENERAL'");
            if (DistSALEGen.Length > 0)
            {
                foreach (DataRow value in DistSALEGen)
                {
                    SALEGen = Convert.ToDecimal(value[3].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    TotSALEGen += SALEGen;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] DistSALESC = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='SC'");
            if (DistSALESC.Length > 0)
            {
                foreach (DataRow value in DistSALESC)
                {
                    SALESC = Convert.ToDecimal(value[3].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    TotSALESC += SALESC;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            DataRow[] DistSALEST = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='ST'");
            if (DistSALEST.Length > 0)
            {
                foreach (DataRow value in DistSALEST)
                {
                    SALEST = Convert.ToDecimal(value[3].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    TotSALEST += SALEST;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            str.Append("<td style='text-align: Right;'>" + (SALEGen + SALESC + SALEST).ToString() + "</td>");
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            DataRow[] DistSUBGen = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='GENERAL'");
            if (DistSUBGen.Length > 0)
            {
                foreach (DataRow value in DistSUBGen)
                {
                    SubGen = Convert.ToDecimal(value[5].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[5].ToString() + "</td>");
                    TotSubGen += SubGen;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] DistSUBSC = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='SC'");
            if (DistSUBSC.Length > 0)
            {
                foreach (DataRow value in DistSUBSC)
                {
                    SubSC = Convert.ToDecimal(value[3].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    TotSubSC += SubSC;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            DataRow[] DistSUBST = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND CATEGORY='ST'");
            if (DistSUBST.Length > 0)
            {
                foreach (DataRow value in DistSUBST)
                {
                    SubST = Convert.ToDecimal(value[3].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    TotSubST += SubST;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            str.Append("<td style='text-align: Right;'>" + (SubGen + SubSC + SubST).ToString() + "</td>");
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Left;'>Total :</td>");
        str.Append("<td style='text-align: Right;'>" + TotFarmerGen.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotFarmerSC.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotFarmerST.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(TotFarmer)", ""))).ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSoldGen.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSoldSC.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSoldST.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(TOT_SALE)", ""))).ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSALEGen.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSALESC.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSALEST.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(SUBSIDY_INVOLVED)", ""))).ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSubGen.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSubSC.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSubST.ToString("N2") + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(TOT_PAYMENT)", ""))).ToString("N2") + "</td>");
        
        str.Append("</tr>");
        //str.Append("<tr>");
        //str.Append("<td colspan='4' style='font-weight:bold;'>Total Distinct Farmer irrespective of schemes  : " + ds.Tables[1].Rows[0][0].ToString() + "</td>");
        //str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();

        string sql = "STOCK_SCHEMEWISE_RPT2";
        string[] param = { "@FIN_YR", "@Seassion", "@CROP_ID" };
        object[] value = { drpSession.SelectedValue.Trim(), drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim(), drpCropName.SelectedValue.Trim() == "--- All ---" ? System.Convert.DBNull : drpCropName.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GenerateReport1(ds);
            }
            else
            {
                litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
            }
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }

        litCrop.Text = "<div style='font-family:Tahoma; font-size:12px;   font-weight:bold;'>Session Year : " + drpSession.SelectedItem.Text + " , Season : " + drpSeassion.SelectedItem.Text + " , Crop : " + drpCropName.SelectedItem.Text + "</div>";

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=SchemeCatWiseDetails.xls";
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
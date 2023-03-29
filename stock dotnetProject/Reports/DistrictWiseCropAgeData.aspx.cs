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

public partial class Reports_DistrictWiseCropAgeData :  System.Web.UI.Page
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

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT CROP_CODE,CROP_NAME FROM STOCK_FARMER A INNER JOIN MCROP B ON A.CROP_ID = B.CROP_CODE ORDER BY CROP_NAME ASC", con);
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
        string sql = "STOCK_DISTCROPAGE";
        string[] param = { "@FIN_YEAR", "@SEASON", "@CROP_ID" };
        object[] value = { drpSession.SelectedValue.Trim(), drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim(), drpCropName.SelectedValue.Trim() == "--- All ---" ? System.Convert.DBNull : drpCropName.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GenerateReport(ds);
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
    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        btnPrint.Enabled = true;

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<tr>");
        str.Append("<td rowspan='2' style='text-align: Center; font-weight:bold;'>District Name</td>");
        str.Append("<td colspan='5' style='text-align: Center; font-weight:bold;'>BELOW 10 YEARS</td>");
        str.Append("<td colspan='5' style='text-align: Center; font-weight:bold;'>ABOVE 10 YEARS</td>");
        str.Append("<td colspan='4' style='text-align: Center; font-weight:bold;'>TOTAL</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>SOLD QTY (IN QTL)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>SUB QTY (IN QTL)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOT GOI SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOT SP SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOTAL SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>SOLD QTY (IN QTL)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>SUB QTY (IN QTL) (in Qtl)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOT GOI SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOT SP SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOTAL SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>SOLD QTY (IN QTL)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>SUB QTY (IN QTL) (in Qtl)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOT GOI SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>TOT SP SUBSIDY AMOUNT(IN RS)</td>");
        str.Append("</tr>");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td style='font-weight:bold;'>" + dr["Dist_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_QTL1"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["ADMISSIBLE_SUBSIDY1"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_SUB_AMOUNT_GOI1"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_SUB_AMOUNT_SP1"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SUBSIDY_AMOUNT1"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_QTL2"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["ADMISSIBLE_SUBSIDY2"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_SUB_AMOUNT_GOI2"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_SUB_AMOUNT_SP2"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["SUBSIDY_AMOUNT2"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_QTL"].ToString() + "</td>");;
            str.Append("<td style='text-align: Right;'>" + dr["ADMISSIBLE_SUBSIDY"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_SUB_AMOUNT_GOI"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["TOT_SUB_AMOUNT_SP"].ToString() + "</td>");            
            str.Append("</tr>");
        }
        str.Append("<tr>");
        str.Append("<td style='font-weight:bold;'>TOTAL</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_QTL1)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(ADMISSIBLE_SUBSIDY1)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_SUB_AMOUNT_GOI1)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_SUB_AMOUNT_SP1)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(SUBSIDY_AMOUNT1)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_QTL2)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(ADMISSIBLE_SUBSIDY2)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_SUB_AMOUNT_GOI2)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_SUB_AMOUNT_SP2)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(SUBSIDY_AMOUNT2)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_QTL)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(ADMISSIBLE_SUBSIDY)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_SUB_AMOUNT_GOI)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TOT_SUB_AMOUNT_SP)", "").ToString() + "</td>");        
        str.Append("</tr>");
        //str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        //str.Append("<td style='text-align: Right;'>Total :</td>");
        //str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(TotFarmer)", "").ToString() + "</td>");
        //str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(TOT_QTL)", "").ToString() + "</td>");
        //str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(TOTGOIAMNT)", "").ToString() + "</td>");
        //str.Append("</tr>");
        //str.Append("<tr>");
        //str.Append("<td colspan='4' style='font-weight:bold;'>Total Distinct Farmer irrespective of schemes  : " + ds.Tables[1].Rows[0][0].ToString() + "</td>");
        //str.Append("</tr>");
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
        str.Append("<td colspan='3' style='text-align: Center;'>No of distinct Farmer</td>");
        str.Append("<td colspan='3' style='text-align: Center;'>Total Seed Sold</td>");
        str.Append("<td colspan='3' style='text-align: Center;'>Total Subsidy involved</td>");
        str.Append("<td colspan='3' style='text-align: Center;'>Total Subsidy Paid</td>");
        str.Append("</tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td></td>");
        str.Append("<td style='text-align: Center;'>Above 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Below 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Total</td>");
        str.Append("<td style='text-align: Center;'>Above 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Below 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Total</td>");
        str.Append("<td style='text-align: Center;'>Above 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Below 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Total</td>");
        str.Append("<td style='text-align: Center;'>Above 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Below 10 Years</td>");
        str.Append("<td style='text-align: Center;'>Total</td>");
        str.Append("</tr>");
        decimal TotFarmerPacs = 0;
        decimal TotFarmerDealer = 0;
        decimal TotSoldPacs = 0;
        decimal TotSoldDealer = 0;
        decimal TotSubPacs = 0;
        decimal TotSubDealer = 0;
        decimal TotSubsPacs = 0;
        decimal TotSubsDealer = 0;

        decimal nfsm1a = 0;
        decimal nfsm1b = 0;
        decimal rkvy1a = 0;
        decimal rkvy1b = 0;
        decimal nfsm2a = 0;
        decimal nfsm2b = 0;
        decimal rkvy2a = 0;
        decimal rkvy2b = 0;
        decimal nfsm3a = 0;
        decimal nfsm3b = 0;
        decimal rkvy3a = 0;
        decimal rkvy3b = 0;
        foreach (DataRow dr in scheem.Rows)
        {
            decimal FarmerPACS = 0;
            decimal FarmerDealer = 0;
            decimal QTLPACS = 0;
            decimal QtlDealer = 0;
            decimal SubPACS = 0;
            decimal SubDealer = 0;
            decimal SubsPACS = 0;
            decimal SubsDealer = 0;

            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");

            str.Append("<td style='text-align: Left;'>" + dr["SCHEME"].ToString() + "</td>");

            DataRow[] pscsDistFarmer = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' and APP_TYPE='AFTER'");
            if (pscsDistFarmer.Length > 0)
            {
                foreach (DataRow value in pscsDistFarmer)
                {
                    FarmerPACS = Convert.ToDecimal(value[2].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[2].ToString() + "</td>");
                    TotFarmerPacs += FarmerPACS;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] DealDistFarmer = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' and APP_TYPE='BEFORE'");
            if (DealDistFarmer.Length > 0)
            {
                foreach (DataRow value in DealDistFarmer)
                {
                    FarmerDealer = Convert.ToDecimal(value[2].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[2].ToString() + "</td>");
                    TotFarmerDealer += FarmerDealer;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            //str.Append("<td style='text-align: Right;'>0</td>");
            str.Append("<td style='text-align: Right;'>" + (FarmerPACS + FarmerDealer).ToString() + "</td>");

            DataRow[] pscsDistQtl = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' AND APP_TYPE='AFTER'");
            DataRow[] QtlA = ds.Tables[1].Select("SCHEME= '" + dr["SCHEME"].ToString() + "'");
            DataRow[] QtlB = ds.Tables[2].Select("SCHEME= '" + dr["SCHEME"].ToString() + "'");
            if (pscsDistQtl.Length > 0)
            {
                foreach (DataRow value in pscsDistQtl)
                {
                    QTLPACS = Convert.ToDecimal(value[3].ToString());
                    if (dr["SCHEME"].ToString() == "NFSM")
                    {
                        if (QtlA.Length > 0)
                        {
                            foreach (DataRow rowQtlA in QtlA)
                            {

                                nfsm1a = Convert.ToDecimal(rowQtlA[1].ToString());
                                QTLPACS = nfsm1a;
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + QTLPACS.ToString() + "</td>");
                            }
                        }
                    }
                    else if (dr["SCHEME"].ToString() == "RKVY")
                    {
                        if (QtlA.Length > 0)
                        {
                            foreach (DataRow rowQtlA in QtlA)
                            {
                                //str.Append(" <td style='text-align: Right; padding-right:5px;'>20000.00" + rowQtlA[1].ToString() + "</td>");
                                //QTLPACS = Convert.ToDecimal(rowQtlA[1].ToString());


                                rkvy1a = Convert.ToDecimal(rowQtlA[1].ToString());
                                //QTLPACS = 20000;
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + QTLPACS.ToString() + "</td>");
                            }
                        }
                    }
                    else
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    }
                    TotSoldPacs += QTLPACS;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] DealDistQtl = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' and APP_TYPE='BEFORE'");
            if (DealDistQtl.Length > 0)
            {
                foreach (DataRow value in DealDistQtl)
                {
                    QtlDealer = Convert.ToDecimal(value[3].ToString());

                    if (dr["SCHEME"].ToString() == "NFSM")
                    {
                        if (QtlB.Length > 0)
                        {
                            foreach (DataRow rowQtlB in QtlB)
                            {

                                nfsm1b = Convert.ToDecimal(rowQtlB[1].ToString());
                                QtlDealer = nfsm1b;
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + QtlDealer.ToString() + "</td>");
                            }
                        }
                    }
                    else if (dr["SCHEME"].ToString() == "RKVY")
                    {
                        if (QtlB.Length > 0)
                        {
                            foreach (DataRow rowQtlB in QtlB)
                            {

                                rkvy1b = Convert.ToDecimal(rowQtlB[1].ToString());
                                //QtlDealer = rkvy1b + (rkvy1a - 20000);
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + QtlDealer.ToString() + "</td>");
                            }
                        }
                    }
                    else
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[3].ToString() + "</td>");
                    }
                    TotSoldDealer += QtlDealer;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            str.Append("<td style='text-align: Right;'>" + (QTLPACS + QtlDealer).ToString() + "</td>");

            DataRow[] pscsSub = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' and APP_TYPE='AFTER'");
            if (pscsSub.Length > 0)
            {
                foreach (DataRow value in pscsSub)
                {
                    SubPACS = Convert.ToDecimal(value[4].ToString());
                    if (dr["SCHEME"].ToString() == "NFSM")
                    {
                        if (QtlA.Length > 0)
                        {
                            foreach (DataRow rowQtlA in QtlA)
                            {

                                nfsm2a = Convert.ToDecimal(rowQtlA[2].ToString());
                                SubPACS = nfsm1a;
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + SubPACS.ToString() + "</td>");
                            }
                        }
                    }
                    else if (dr["SCHEME"].ToString() == "RKVY")
                    {
                        if (QtlA.Length > 0)
                        {
                            foreach (DataRow rowQtlA in QtlA)
                            {
                                //str.Append(" <td style='text-align: Right; padding-right:5px;'>20000.00" + rowQtlA[1].ToString() + "</td>");
                                //QTLPACS = Convert.ToDecimal(rowQtlA[1].ToString());


                                rkvy2a = Convert.ToDecimal(rowQtlA[2].ToString());
                                //SubPACS = 20000000;
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + SubPACS.ToString() + "</td>");
                            }
                        }
                    }
                    else
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[4].ToString() + "</td>");
                    }
                    TotSubPacs += SubPACS;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] DealSub = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' and APP_TYPE='BEFORE'");
            if (DealSub.Length > 0)
            {
                foreach (DataRow value in DealSub)
                {
                    SubDealer = Convert.ToDecimal(value[4].ToString());
                    if (dr["SCHEME"].ToString() == "NFSM")
                    {
                        if (QtlB.Length > 0)
                        {
                            foreach (DataRow rowQtlB in QtlB)
                            {

                                nfsm2b = Convert.ToDecimal(rowQtlB[2].ToString());
                                SubDealer = nfsm1b;
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + SubDealer.ToString() + "</td>");
                            }
                        }
                    }
                    else if (dr["SCHEME"].ToString() == "RKVY")
                    {
                        if (QtlB.Length > 0)
                        {
                            foreach (DataRow rowQtlB in QtlB)
                            {

                                rkvy2b = Convert.ToDecimal(rowQtlB[2].ToString());
                                //SubDealer = rkvy2b + (rkvy2a - 20000000);
                                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + SubDealer.ToString() + "</td>");
                            }
                        }
                    }
                    else
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[4].ToString() + "</td>");
                    }
                    TotSubDealer += SubDealer;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            str.Append("<td style='text-align: Right;'>" + (SubPACS + SubDealer).ToString() + "</td>");
            DataRow[] PACSSubs = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' and APP_TYPE='AFTER'");
            if (PACSSubs.Length > 0)
            {
                foreach (DataRow value in PACSSubs)
                {
                    SubsPACS = Convert.ToDecimal(value[5].ToString());
                    if (dr["SCHEME"].ToString() == "NFSM")
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[5].ToString() + "</td>");
                    }
                    else if (dr["SCHEME"].ToString() == "RKVY")
                    {
                        rkvy3a = Convert.ToDecimal(value[5].ToString());
                        //SubsPACS = 20000000;
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + SubsPACS.ToString() + "</td>");
                    }
                    else
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[5].ToString() + "</td>");
                    }
                    TotSubsPacs += SubsPACS;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            DataRow[] DealSubs = ds.Tables[0].Select("SCHEME= '" + dr["SCHEME"].ToString() + "' and APP_TYPE='BEFORE'");
            if (DealSubs.Length > 0)
            {
                foreach (DataRow value in DealSubs)
                {
                    SubsDealer = Convert.ToDecimal(value[5].ToString());
                    if (dr["SCHEME"].ToString() == "NFSM")
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[5].ToString() + "</td>");
                    }
                    else if (dr["SCHEME"].ToString() == "RKVY")
                    {
                        rkvy3b = Convert.ToDecimal(value[5].ToString());
                        //SubsDealer = rkvy3b + (rkvy3a - 20000000);
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + SubsDealer.ToString() + "</td>");
                    }
                    else
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[5].ToString() + "</td>");
                    }
                    TotSubsDealer += SubsDealer;
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }
            str.Append("<td style='text-align: Right;'>" + (SubsPACS + SubsDealer).ToString() + "</td>");
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Left;'>Total :</td>");
        str.Append("<td style='text-align: Right;'>" + TotFarmerPacs.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotFarmerDealer.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(TotFarmer)", ""))).ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSoldPacs.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSoldDealer.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(TOT_QTL)", ""))).ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSubPacs.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSubDealer.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(TOT_SUB)", ""))).ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSubsPacs.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotSubsDealer.ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + (Convert.ToDecimal(ds.Tables[0].Compute("Sum(RESAMOUNT)", ""))).ToString() + "</td>");
        str.Append("</tr>");
        //str.Append("<tr>");
        //str.Append("<td colspan='4' style='font-weight:bold;'>Total Distinct Farmer irrespective of schemes  : " + ds.Tables[1].Rows[0][0].ToString() + "</td>");
        //str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=SchemeWiseDetails.xls";
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
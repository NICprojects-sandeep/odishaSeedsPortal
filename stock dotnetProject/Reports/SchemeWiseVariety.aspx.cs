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

public partial class Reports_SchemeWiseVariety : System.Web.UI.Page
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
    //        if (this.Session["UserType"].ToString() == "FINA")
    //        {
    //            this.Page.MasterPageFile = "../MasterPages/Finance.master";
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
            //if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") && !(this.Session["UserType"].ToString() == "FINA") || !(this.Session["FirstLogin"].ToString() == "N"))
            //{
            //    base.Response.Redirect("../Unauthorised.aspx", true);
            //    return;
            //}
            if (!this.Page.IsPostBack)
            {
                FillFinYr();
                btnExport.Enabled = false;
                FillCrop();
                FillDist();
                FillScheme();
                drpScheme.Enabled = false;
                
            }
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
    public DataSet DsCrop()
    {
        //SqlConnection con = new SqlConnection(_connstrStock);

        //SqlCommand cmd = new SqlCommand("SELECT Crop_Code,Crop_Name FROM mCrop WHERE is_active = 1 ORDER BY Crop_Name ASC", con);

        //SqlCommand cmd = new SqlCommand("SELECT DISTINCT B.Crop_Code,B.Crop_Name FROM STOCK_FARMER A "+
        //"INNER JOIN mCrop B ON A.CROP_ID = B.Crop_Code "+
        //"WHERE FIN_YEAR =  and SEASON='K' ORDER BY Crop_Name ASC", con);
        //cmd.CommandType = CommandType.Text;
        try
        {
            //SqlDataAdapter ada = new SqlDataAdapter(cmd);
            //ds = new DataSet();
            //ada.Fill(ds);
            string sql = "SELECT DISTINCT B.Crop_Code,B.Crop_Name FROM STOCK_FARMER A " +
            "INNER JOIN mCrop B ON A.CROP_ID = B.Crop_Code " +
            "WHERE FIN_YEAR = @FIN_YEAR and SEASON = @SEASON ORDER BY Crop_Name ASC";
            string[] param = { "@FIN_YEAR", "@SEASON" };
            object[] value = { drpSession.SelectedItem.Text, drpSeassion.SelectedValue };
            ds = dbsAppStock.param_passing_fetch(sql, param, value);
            return ds;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return null;
        }
        finally
        {
            //cmd.Dispose();
            //if (con.State != ConnectionState.Closed)
            //    con.Close();
            //con.Dispose();
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
                    DataRow[] SchemeCode = ds.Tables[0].Select("SCHEME_NAME = '" + Session["SchemeName"].ToString() + "'");
                    ListItem limDefault = new ListItem();
                    limDefault.Selected = true;
                    foreach (DataRow value in SchemeCode)
                    {
                        if (drpSession.SelectedItem.Text == "2018-19")
                        {
                            limDefault.Text = value[2].ToString();
                            limDefault.Value = value[1].ToString();
                        }
                        else
                        {
                            limDefault.Text = value[2].ToString();
                            limDefault.Value = value[0].ToString();
                        }
                        
                    }

                    

                    drpScheme.Items.Add(limDefault);
                    //if (drpSession.SelectedItem.Text == "2018-19")
                    //{
                    //    this.drpScheme.DataValueField = "SCHEME_CODE1";
                    //    this.drpScheme.DataTextField = "SCHEME_NAME";
                    //    this.drpScheme.DataBind();
                    //}
                    //else
                    //{
                    //    this.drpScheme.DataValueField = "SCHEME_CODE";
                    //    this.drpScheme.DataTextField = "SCHEME_NAME";
                    //    this.drpScheme.DataBind();
                    //}
                }
            }

            //DataRow[] SchemeCode = ds.Tables[0].Select("SCHEME_NAME = " + Session["SchemeName"].ToString() + "");
            //string mSchemeCode = "";            
            //foreach (DataRow value in SchemeCode)
            //{
            //    mSchemeCode = value[1].ToString();
            //}
            
            drpScheme.SelectedItem.Text = Session["SchemeName"].ToString();
            //drpScheme.SelectedValue = mSchemeCode;
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

        SqlCommand cmd = new SqlCommand("SELECT [SCHEME_CODE],[SCHEME_CODE1],[SCHEME_NAME] FROM [stock].[dbo].[mSCHEME] ORDER BY SCHEME_NAME ASC", con);
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
        //DataSet ds = new DataSet();

        //string sql = "STOCK_RPT_SaleVarietywise1";
        //string[] param = { "@Crop_Code", "@FIN_YR", "@Seassion", "@Dist_Code", "@SCHEME_CODE", "@USER_TYPE", "@MONTH", "@FRMDT", "@TODT" };
        //object[] value = { drpCropName.SelectedValue.Trim(), drpSession.SelectedValue.Trim(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim(),
        //drpDistrict.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpDistrict.SelectedValue.Trim(),drpScheme.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpScheme.SelectedValue.Trim(),
        //drpUser.SelectedValue.Trim()=="0"?System.Convert.DBNull:drpUser.SelectedValue.Trim(),drpMonth.SelectedValue.Trim()=="0" ?System.Convert.DBNull:drpMonth.SelectedValue.Trim(),
        //txtfromdate.Text.Trim()==""?System.Convert.DBNull:txtfromdate.Text.Trim(),txtTodate.Text.Trim()==""?System.Convert.DBNull:txtTodate.Text.Trim() };

        //string[] param = { "@Crop_Code", "@Dist_Code", "@FIN_YR", "@Seassion", "@SCHEME_CODE", "@USER_TYPE", "@MONTH", "@FRMDT", "@TODT" };
        //object[] value = { drpCropName.SelectedValue.Trim(),drpDistrict.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpDistrict.SelectedValue.Trim(),
        //drpSession.SelectedValue.Trim(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim(),
        //drpScheme.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpScheme.SelectedValue.Trim(),
        //drpUser.SelectedValue.Trim()=="0"?System.Convert.DBNull:drpUser.SelectedValue.Trim(),
        //drpMonth.SelectedValue.Trim()=="0" ?System.Convert.DBNull:drpMonth.SelectedValue.Trim(),
        //txtfromdate.Text.Trim()==""?System.Convert.DBNull:txtfromdate.Text.Trim(),
        //txtTodate.Text.Trim()==""?System.Convert.DBNull:txtTodate.Text.Trim() };
        //ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);

        objUserBEL = new BLL_DropDown();

        string mDistCode = "";

        string mSeassion = "";
        string mScheme = "";
        string mUserType = "";
        string mMonth = "";
        string mFromDate = "";
        string mToDate = "";
        if (drpDistrict.SelectedValue.Trim() == "--- All ---")
        {
            mDistCode = null;
        }
        else
        {
            mDistCode = drpDistrict.SelectedValue.Trim();
        }
        if (drpSeassion.SelectedValue.Trim() == "0")
        {
            mSeassion = null;
        }
        else
        {
            mSeassion = drpSeassion.SelectedValue.Trim();
        }
        if (drpScheme.SelectedValue.Trim() == "--- All ---")
        {
            mScheme = null;
        }
        else
        {
            mScheme = drpScheme.SelectedValue.Trim();
        }

        if (drpUser.SelectedValue.Trim() == "0")
        {
            mUserType = null;
        }
        else
        {
            mUserType = drpUser.SelectedValue.Trim();
        }

        if (drpMonth.SelectedValue.Trim() == "0")
        {
            mMonth = null;
        }
        else
        {
            mMonth = drpMonth.SelectedValue.Trim();
        }

        if (txtfromdate.Text.Trim() == "")
        {
            mFromDate = null;
        }
        else
        {
            mFromDate = txtfromdate.Text.Trim();
        }

        if (txtTodate.Text.Trim() == "")
        {
            mToDate = null;
        }
        else
        {
            mToDate = txtTodate.Text.Trim();
        }

        objUserBEL.CROP_ID = drpCropName.SelectedValue.Trim();
        objUserBEL.DistCode = mDistCode;
        objUserBEL.FIN_YR = drpSession.SelectedValue.Trim();
        objUserBEL.SEASON = mSeassion;
        objUserBEL.scheme_code = mScheme;
        objUserBEL.UserType = mUserType;
        objUserBEL.Month = mMonth;
        objUserBEL.FROM_DATE = mFromDate;
        objUserBEL.TO_DATE = mToDate;

        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.STOCK_RPT_SaleVarietywise1(objUserBEL);

        if (ds.Tables[1].Rows.Count > 0)
        {
            btnExport.Enabled = true;
            GenerateReport(ds);
        }
        else
        {
            btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;' class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }
    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Dist Name  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>General  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>SC  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>ST  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>No. of total farmers  </td>");
        //DataView view = new DataView(ds.Tables[1]);
        //view.Sort = "CROPCATG_ID ASC";
        //DataTable dtVariety = view.ToTable(true, "CROP_VERID", "Variety_Name");
        //foreach (DataRow dr in dtVariety.Rows)
        //{
        //    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + dr["Variety_Name"].ToString() + "</td>");
        //}
        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");

            str.Append("<td align='left' class='tbltd'>");
            //string tmpstr11 = "<a href='javascript:popUp";
            //string tempStr1 = "('RPT_VarietyWiseSaleDistWIse.aspx?Crop_Code=" + drpCropName.SelectedValue.Trim() + " &SCHEME_CODE= " + drpScheme.SelectedValue.Trim() + " &Dist_Code= " + dr["Dist_Code"].ToString() + " &FIN_YEAR= " + drpSession.SelectedValue.ToString() + " &MONTH= " + drpMonth.SelectedValue.ToString() + "&FRMDT= " + txtfromdate.Text.Trim() + " &TODT= " + txtTodate.Text.Trim() + " &USER_TYPE= " + drpUser.SelectedValue.ToString() + " &Seassion= " + drpSeassion.SelectedValue.Trim() + "')";
            //string tmpstr21 = "'>";
            //str.Append(tmpstr11.Replace('\'', '\"') + tempStr1 + tmpstr21.Replace('\'', '\"'));
            str.Append("" + dr["Dist_Name"].ToString() + "</td>");
            int Gen = 0;
            int SC = 0;
            int ST = 0;

            DataRow[] NoofFarmergen = ds.Tables[2].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND INTCATEGOGY=1");
            if (NoofFarmergen.Length > 0)
            {
                foreach (DataRow value in NoofFarmergen)
                {
                    Gen = Convert.ToInt32(value[1].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] NoofFarmersc = ds.Tables[2].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "AND INTCATEGOGY=2");
            if (NoofFarmersc.Length > 0)
            {
                foreach (DataRow value in NoofFarmersc)
                {
                    SC = Convert.ToInt32(value[1].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] NoofFarmerst = ds.Tables[2].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "AND INTCATEGOGY=3");
            if (NoofFarmerst.Length > 0)
            {
                foreach (DataRow value in NoofFarmerst)
                {
                    ST = Convert.ToInt32(value[1].ToString());
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            DataRow[] NoofFarmer = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            if (NoofFarmer.Length > 0)
            {
                foreach (DataRow value in NoofFarmer)
                {
                    //str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "(Gen-" + Gen + ",SC -" + SC + ",ST - " + ST + ") </td>");
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            }

            //DataRow[] uNoofFarmer = ds.Tables[5].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            //if (uNoofFarmer.Length > 0)
            //{
            //    foreach (DataRow value in uNoofFarmer)
            //    {
            //        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
            //    }
            //}
            //else
            //{
            //    str.Append(" <td style='text-align: Right; padding-right:5px;'>-- </td>");
            //}
            double TotSale = 0;
            //foreach (DataRow drv in dtVariety.Rows)
            //{
            //    DataRow[] Data = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "'");
            //    if (Data.Length > 0)
            //    {
            //        foreach (DataRow value in Data)
            //        {
            //            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[8].ToString() + "</td>");
            //            TotSale = TotSale + double.Parse(value[8].ToString());
            //        }
            //    }
            //    else
            //    {
            //        str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
            //    }
            //}
            //str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotSale) + "</td>");
            str.Append("</tr>");
        }

        //str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        //str.Append("<td>Grand Total</td>");
        //str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[2].Compute("Sum(NoofFarmergen)", "").ToString() + "</td>");
        //str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[3].Compute("Sum(NoofFarmersc)", "").ToString() + "</td>");
        //str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[4].Compute("Sum(NoofFarmerst)", "").ToString() + "</td>");
        //str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[1].Compute("Sum(NoofFarmer)", "").ToString() + "</td>");
        //str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[5].Compute("Sum(NoofFarmer)", "").ToString() + "</td>");

        ////foreach (DataRow dr in dtVariety.Rows)
        ////{
        ////    str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "'").ToString())) + "</td>");
        ////}
        ////str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SALE)", "").ToString())) + "</td>");
        //str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=FarmerSaleList.xls";
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
        FillScheme();
    }
    protected void drpSeassion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCrop();
    }    
}
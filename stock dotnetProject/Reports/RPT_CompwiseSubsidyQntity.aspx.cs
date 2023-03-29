using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_CompwiseSubsidyQntity : System.Web.UI.Page
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
            //Session["SchemeName"] = drpScheme.SelectedItem.Text;
            //if (drpScheme.SelectedItem.Text != "---All---")
            //{
                ds = new DataSet();
                ds = DsScheme();
                drpScheme.Items.Clear();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.drpScheme.DataSource = ds;
                        //DataRow[] SchemeCode = ds.Tables[0].Select("SCHEME_NAME = '" + Session["SchemeName"].ToString() + "'");
                        //ListItem limDefault = new ListItem();
                        //limDefault.Selected = true;
                        //foreach (DataRow value in SchemeCode)
                        //{
                        //    if (drpSession.SelectedItem.Text == "2018-19")
                        //    {
                        //        limDefault.Text = value[2].ToString();
                        //        limDefault.Value = value[1].ToString();
                        //    }
                        //    else
                        //    {
                        //        limDefault.Text = value[2].ToString();
                        //        limDefault.Value = value[0].ToString();
                        //    }
                        //}
                        //drpScheme.Items.Add(limDefault);
                        //drpScheme.SelectedItem.Text = Session["SchemeName"].ToString();
                        if (drpSession.SelectedItem.Text == "2018-19")
                        {
                            this.drpScheme.DataValueField = "SCHEME_CODE1";
                            this.drpScheme.DataTextField = "SCHEME_NAME";
                            this.drpScheme.DataBind();
                        }
                        else
                        {
                            this.drpScheme.DataValueField = "SCHEME_CODE";
                            this.drpScheme.DataTextField = "SCHEME_NAME";
                            this.drpScheme.DataBind();
                        }
                    }
                }
                //this.drpScheme.Items.Insert(0, "--- All ---");
            //}
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
        DataSet ds = new DataSet();
        string sql = "STOCK_CompwiseSubsidyQntity";
        string[] param = { "@Crop_Code", "@APP_TYPE", "@SEASON", "@SCHEME_CODE", "@FIN_YEAR", "@Agency" };
        object[] value = { drpCropName.SelectedValue.Trim() == "--- All ---" ? "0" : drpCropName.SelectedValue.Trim(), drpDealer.SelectedValue.ToString() == "--- All ---" ? "0" : drpDealer.SelectedValue.ToString(), drpSeason.SelectedValue.ToString() == "--- All ---" ? "0" : drpSeason.SelectedValue.ToString(), drpScheme.SelectedValue.ToString() == "--- All ---" ? "0" : drpScheme.SelectedValue.ToString(), drpSession.SelectedValue.ToString() == "--- All ---" ? "0" : drpSession.SelectedValue.ToString(), drpAgency.SelectedValue.ToString() == "--- All ---" ? "0" : drpAgency.SelectedValue.ToString() };
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
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td rowspan='2'>Dist Name  </td>");
        DataView view = new DataView(ds.Tables[1]);
        view.Sort = "CROPCATG_ID ASC";
        DataTable dtCrop = view.ToTable(true, "Crop_Code", "Crop_Name");
        foreach (DataRow dr in dtCrop.Rows)
        {
            str.Append(" <td style='text-align: center; padding-right:5px;' colspan='5'>" + dr["Crop_Name"].ToString() + "</td>");
        }
        str.Append(" <td style='text-align: center; padding-right:5px;' colspan='6'>Total</td>");
        str.Append("</tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");


        foreach (DataRow dr in dtCrop.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Normal</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>SCP</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>TASP</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");
        }
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Normal</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>SCP</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>TASP</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Unregistered Farmer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");
        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");

            foreach (DataRow drv in dtCrop.Rows)
            {
                DataRow[] DataNormal = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code = '" + drv["Crop_Code"].ToString() + "' AND INTCATEGOGY=1");
                if (DataNormal.Length > 0)
                {
                    foreach (DataRow value in DataNormal)
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[7].ToString() + "</td>");

                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
                }
                DataRow[] DataSCP = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code = '" + drv["Crop_Code"].ToString() + "' AND INTCATEGOGY=2");
                if (DataSCP.Length > 0)
                {
                    foreach (DataRow value in DataSCP)
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[7].ToString() + "</td>");

                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
                }
                DataRow[] DataTASP = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code = '" + drv["Crop_Code"].ToString() + "' AND INTCATEGOGY=3");
                if (DataTASP.Length > 0)
                {
                    foreach (DataRow value in DataTASP)
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[7].ToString() + "</td>");

                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
                }
                double UnRegFarmer = 0;

                UnRegFarmer = ds.Tables[2].Compute("Sum(SALE)", "Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code ='" + drv["Crop_Code"].ToString() + "'").ToString() == "" ? 0 : double.Parse(ds.Tables[2].Compute("Sum(SALE)", "Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code ='" + drv["Crop_Code"].ToString() + "'").ToString());



                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[2].Compute("Sum(SALE)", "Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code ='" + drv["Crop_Code"].ToString() + "'").ToString() + "</td>");


                double RegdFarmer = 0;
                RegdFarmer = ds.Tables[1].Compute("Sum(SALE)", "Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code ='" + drv["Crop_Code"].ToString() + "'").ToString() == "" ? 0 : double.Parse(ds.Tables[1].Compute("Sum(SALE)", "Dist_Code= " + dr["Dist_Code"].ToString() + " AND Crop_Code ='" + drv["Crop_Code"].ToString() + "'").ToString());


                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", (UnRegFarmer + RegdFarmer)) + "</td>");




            }
            DataRow[] TDataNormal = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "  AND INTCATEGOGY=1");
            if (TDataNormal.Length > 0)
            {
                double TNormal = 0;
                foreach (DataRow value in TDataNormal)
                {
                    TNormal = TNormal + double.Parse(value[7].ToString());

                }
                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + TNormal + "</td>");
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
            }
            DataRow[] TDataSCP = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "  AND INTCATEGOGY=2");
            if (TDataSCP.Length > 0)
            {
                double TSCP = 0;
                foreach (DataRow value in TDataSCP)
                {
                    TSCP = TSCP + double.Parse(value[7].ToString());


                }
                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + TSCP + "</td>");
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
            }
            DataRow[] TDataTASP = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "  AND INTCATEGOGY=3");
            if (TDataTASP.Length > 0)
            {
                double TTASP = 0;
                foreach (DataRow value in TDataTASP)
                {
                    TTASP = TTASP + double.Parse(value[7].ToString());

                }
                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + TTASP + "</td>");
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
            }


            double unTTotal = 0;
            DataRow[] unTDataTotal = ds.Tables[2].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " ");
            if (unTDataTotal.Length > 0)
            {

                foreach (DataRow value in unTDataTotal)
                {
                    unTTotal = unTTotal + double.Parse(value[6].ToString());


                }
                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + unTTotal + "</td>");

            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
            }


            double TTotal = 0;
            DataRow[] TDataTotal = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " ");
            if (TDataTotal.Length > 0)
            {

                foreach (DataRow value in TDataTotal)
                {
                    TTotal = TTotal + double.Parse(value[7].ToString());


                }
                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (unTTotal + TTotal) + "</td>");
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'>--</td>");
            }
            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td>Total</td>");
        foreach (DataRow drv in dtCrop.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "' AND INTCATEGOGY=1 ").ToString() + "</td>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "' AND INTCATEGOGY=2 ").ToString() + "</td>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "' AND INTCATEGOGY=3 ").ToString() + "</td>");

            double unfar = 0;

            unfar = ds.Tables[2].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "'").ToString() == "" ? 0 : double.Parse(ds.Tables[2].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "'").ToString());

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[2].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "'").ToString() + "</td>");

            double RegTot = ds.Tables[1].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "'").ToString() == "" ? 0 : double.Parse(ds.Tables[1].Compute("Sum(SALE)", "Crop_Code = '" + drv["Crop_Code"].ToString() + "'").ToString());

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (unfar + RegTot) + "</td>");

        }

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "INTCATEGOGY=1 ").ToString() + "</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "INTCATEGOGY=2 ").ToString() + "</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "INTCATEGOGY=3 ").ToString() + "</td>");

        double unTotFarm = 0;
        unTotFarm = ds.Tables[2].Compute("Sum(SALE)", "").ToString() == "" ? 0 : double.Parse(ds.Tables[2].Compute("Sum(SALE)", "").ToString());

        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + ds.Tables[2].Compute("Sum(SALE)", "").ToString() + "</td>");

        double Gtot = 0;
        Gtot = ds.Tables[1].Compute("Sum(SALE)", "").ToString() == "" ? 0 : double.Parse(ds.Tables[1].Compute("Sum(SALE)", "").ToString());
        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + (unTotFarm + Gtot) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=CropwiseSubsidy(Qntity).xls";
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
        FillScheme();
    }
}
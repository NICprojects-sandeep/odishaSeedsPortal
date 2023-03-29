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

public partial class Reports_RPT_BeneficiaryDetails : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {

        if (Session["Name"] != null || Session["UserType"] != null)
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
        else
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
                litNote.Text = "<div style='color:red; padding:10px; font-size: 14px;'><b>Note : This report doesn't include Zero Farmer Id transactions !</b></div>";
                btnExport.Enabled = false;
                FillScheme();
                FillDist();
                FillCrop();
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
            this.drpScheme.Items.Insert(0, "--- All ---");

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

        SqlCommand cmd = new SqlCommand("SELECT [SCHEME_CODE],[SCHEME_NAME] FROM [stock].[dbo].[mSCHEME] ORDER BY SCHEME_NAME ASC", con);
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

            DataSet ds = new DataSet();
            string sql = "select Dist_Name, NDist_Code from dbo.Tbl_Dist order by Dist_Name";
            string[] param = { };
            object[] value = { };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpDistrict.DataSource = ds;
                    this.drpDistrict.DataValueField = "NDist_Code";
                    this.drpDistrict.DataTextField = "Dist_Name";
                    this.drpDistrict.DataBind();
                    this.drpDistrict.Items.Insert(0, "--- Select ---");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string sql = "RPTBeneficiaryDtl";
        string[] param = { "@Crop_Code", "@USER_TYPE", "@SEASON", "@SCHEME_CODE", "@FIN_YEAR", "@Dist_Code", "@FRMDT", "@TODT", "@VCHBLOCKID", "@VCHGPID", "@VCHVILLAGEID" };
        object[] value = {drpCropName.SelectedValue.Trim(),drUser.SelectedValue.Trim()=="0"?System.Convert.DBNull:drUser.SelectedValue.Trim(), drpSeason.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeason.SelectedValue.Trim()
 ,drpScheme.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpScheme.SelectedValue.Trim(), drpSession.SelectedValue.Trim(),drpDistrict.SelectedValue.Trim()=="--- All ---"?System.Convert.DBNull:drpDistrict.SelectedValue.Trim(),txtfromdate.Text.Trim()==""?System.Convert.DBNull:txtfromdate.Text.Trim(),txtTodate.Text.Trim()==""?System.Convert.DBNull:txtTodate.Text.Trim(),drpBlock.SelectedValue.Trim()=="---All---"?System.Convert.DBNull:drpBlock.SelectedValue.Trim(),drpGP.SelectedValue.Trim()=="---All---"?System.Convert.DBNull:drpGP.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() == "---All---" ? "0" : drpVillage.SelectedValue.Trim()  };

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
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 12px; ' border='1' cellpadding='5' cellspacing='5' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='padding-right:5px;'>Sl. No.</td>");
        str.Append("<td style='padding-right:5px;'>Farmer ID </td>");
        str.Append("<td style='padding-right:5px;'>Farmer's Detail</td>");
        str.Append("<td style='padding-right:5px;'>Geographical Info</td>");
        str.Append("<td style='padding-right:5px; width:100px;'>Transaction Detail</td>");
        str.Append("<td style='padding-right:5px;'>Account Detail</td>");
        str.Append("<td style='padding-right:5px;'>Subsidy Details (in Rs.)</td>");
        str.Append("<td style='padding-right:5px;'>Dealer Name</br>Mob. No. </td>");

        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (double.Parse(dr["Subsidy"].ToString()) == 0 && double.Parse(dr["TobePaid"].ToString()) != 0)
            {
                str.Append("<tr style='background-color:#ff794d; font-weight:bold;'>");
            }
            else if (double.Parse(dr["Subsidy"].ToString()) != 0 && double.Parse(dr["TobePaid"].ToString()) != 0)
            {
                str.Append("<tr style='background-color:#ffcccc; font-weight:bold;'>");
            }
            else
            {
                str.Append("<tr>");
            }
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["FARMER_ID"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHFARMERNAME"].ToString() + "</br>" + dr["VCHFATHERNAME"].ToString() + "</br><b>Mob. : </b>" + dr["VCHMOBILENO"].ToString() + "</td>");
            str.Append("<td><b>G.P. : </b>" + dr["vch_GPName"].ToString() + "</br><b>Village : </b>" + dr["vch_VillageName"].ToString() + "</td>");
            str.Append("<td><b>Variety : </b>" + dr["Variety_Name"].ToString() + "</br><b>Quantity : </b>" + dr["TOT_QTL"].ToString() + "</br><b>Transaction Id : </b>" + dr["TRANSACTION_ID"].ToString() + "</td>");
            str.Append("<td><b>Acc. No. : </b>" + dr["VCHACCOUNTNO"].ToString() + "</br><b>IFSC : </b>" + dr["NEW_IFSC_CODE"].ToString() + "</td>");

            if (dr["Process"].ToString() == "P")
            {
                str.Append("<td style='white-space: nowrap;'><b>All in cost :</b> " + dr["ALL_IN_COST_AMOUNT"].ToString() + "</br><b>Subsidy to be paid :</b> " + dr["TobePaid"].ToString() + "<br><b>Subsidy paid :</b> " + dr["Subsidy"].ToString() + " <i class='fa fa-hourglass-half' aria-hidden='true'></i></td>");
            }
            else
            {
                str.Append("<td style='white-space: nowrap;'><b>All in cost :</b> " + dr["ALL_IN_COST_AMOUNT"].ToString() + "</br><b>Subsidy to be paid :</b> " + dr["TobePaid"].ToString() + "<br><b>Subsidy paid :</b> " + dr["Subsidy"].ToString() + "</td>");
            }
            str.Append("<td>" + dr["APP_FIRMNAME"].ToString() + "</br><b> Mob : </b>" + dr["APPMOB_NO"].ToString() + "</td>");
            str.Append("</tr>");
            i++;
        }
        string Subsidy = ds.Tables[0].Compute("Sum(Subsidy)", "").ToString();
        string TobePaid = ds.Tables[0].Compute("Sum(TobePaid)", "").ToString();
        string ALL_IN_COST_AMOUNT = ds.Tables[0].Compute("Sum(ALL_IN_COST_AMOUNT)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td colspan='6' style='text-align: Right;' > </td>");

        str.Append("<td style='white-space: nowrap;'><b>Total All in cost :</b> " + string.Format("{0:f2}", Convert.ToDecimal(ALL_IN_COST_AMOUNT)) + "</br><b>Total Subsidy to be paid :</b> " + string.Format("{0:f2}", Convert.ToDecimal(TobePaid)) + "<br><b>Total Subsidy paid :</b> " + string.Format("{0:f2}", Convert.ToDecimal(Subsidy)) + "</td>");
        str.Append("<td style='text-align: Right;'>&nbsp;</td></tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();


    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=BenificiaryDetails.xls";
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
    protected void drpDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        litReport.Text = string.Empty;
        if (drpDistrict.SelectedIndex > 0)
        {
            drpBlock.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select  Nblock_code,PB.vch_BlockName  from block B INNER JOIN [stock].[dbo].[PDS_BLOCKMASTER] PB ON PB.int_BlockID collate SQL_Latin1_General_CP1_CI_AS =B.Nblock_code where left(Nblock_code,2)=@PFMS_Dist_Code order by block_name";
            string[] param = { "@PFMS_Dist_Code" };
            object[] value = { string.Format("{0:00}", int.Parse(drpDistrict.SelectedValue.Trim())) };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpBlock.DataSource = ds.Tables[0];
            drpBlock.DataTextField = "vch_BlockName";
            drpBlock.DataValueField = "Nblock_code";
            drpBlock.DataBind();
            drpGP.Items.Clear();
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpBlock.Items.Clear();
            drpGP.Items.Clear();
            drpVillage.Items.Clear();
            this.drpBlock.Items.Insert(0, "---All---");
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
    protected void drpBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        litReport.Text = string.Empty;
        if (drpBlock.SelectedIndex > 0)
        {
            drpGP.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select int_GPID ,vch_GPName FROM [stock].[dbo].[PDS_GPMASTER]  where int_BlockID=@Block_Code order by vch_GPName";
            string[] param = { "@Block_Code" };
            object[] value = { drpBlock.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpGP.DataSource = ds.Tables[0];
            drpGP.DataTextField = "vch_GPName";
            drpGP.DataValueField = "int_GPID";
            drpGP.DataBind();
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpGP.Items.Clear();
            drpVillage.Items.Clear();
            this.drpGP.Items.Insert(0, "---All---");
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
    protected void drpGp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpGP.SelectedIndex > 0)
        {
            drpVillage.Items.Clear();
            DataSet ds = new DataSet();
            string sql = "select int_VillageID,vch_VillageName from  dbo.PDS_VILLAGEMASTER where int_GPID=@gp_code order by vch_VillageName";
            string[] param = { "@gp_code" };
            object[] value = { drpGP.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpVillage.DataSource = ds.Tables[0];
            drpVillage.DataTextField = "vch_VillageName";
            drpVillage.DataValueField = "int_VillageID";
            drpVillage.DataBind();
            this.drpVillage.Items.Insert(0, "---All---");
        }
        else
        {
            drpVillage.Items.Clear();
            this.drpVillage.Items.Insert(0, "---All---");
        }
    }
}
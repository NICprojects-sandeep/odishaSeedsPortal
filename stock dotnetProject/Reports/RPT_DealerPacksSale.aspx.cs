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
public partial class Reports_RPT_DealerPacksSale : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL; 
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
        if (this.Session["UserType"].ToString() == "RCSA")
        {
            this.Page.MasterPageFile = "../MasterPages/RCSA.master";
            return;
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
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") && !(this.Session["UserType"].ToString() == "RCSA") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                FillFinYr();
                btnExport.Enabled = false;
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


        SqlCommand cmd = new SqlCommand("SELECT DISTINCT B.Crop_Code,B.Crop_Name FROM STOCK_FARMER A INNER JOIN mCrop B ON A.CROP_ID = B.Crop_Code   ORDER BY Crop_Name ASC", con);
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

        string sql = "STOCK_RPT_DealerPacksSale";
        string[] param = { "@Crop_Code", "@UserType", "@FIN_YR", "@Seassion" };
        object[] value = { drpCropName.SelectedValue.Trim(), drpUser.SelectedValue.ToString() ,drpSession.SelectedValue.Trim(),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull
 : drpSeassion.SelectedValue.Trim() };
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
        str.Append("<td style='text-align: Right; padding-right:5px;' rowspan='2'>No. of Farmer  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' rowspan='2'>Unassigned Farmer Id  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' rowspan='2'>No. of Dealer  </td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' rowspan='2'>No. of PACS  </td>");
        DataView view = new DataView(ds.Tables[1]);
        view.Sort = "CROPCATG_ID ASC";
        DataTable dtVariety = view.ToTable(true, "CROP_VERID", "Variety_Name");
        foreach (DataRow dr in dtVariety.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;' colspan='2'>" + dr["Variety_Name"].ToString() + "</td>");

        }
        str.Append(" <td style='text-align: Right; padding-right:5px;' colspan='2'>Total</td>");

        str.Append(" <td style='text-align: Right; padding-right:5px;' rowspan='2'>Total</td>");
        str.Append("</tr>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        foreach (DataRow dr in dtVariety.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;'>Dealer</td>");
            str.Append(" <td style='text-align: Right; padding-right:5px;'>PACS</td>");
        }
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Dealer</td>");
        str.Append(" <td style='text-align: Right; padding-right:5px;'>PACS</td>");
        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["Dist_Name"].ToString() + "</td>");
            DataRow[] NoofFarmer = ds.Tables[2].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            if (NoofFarmer.Length > 0)
            {
                foreach (DataRow value in NoofFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'> </td>");
            }

            DataRow[] uNoofFarmer = ds.Tables[4].Select("Dist_Code= " + dr["Dist_Code"].ToString() + "");
            if (uNoofFarmer.Length > 0)
            {
                foreach (DataRow value in uNoofFarmer)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'> </td>");
            }

            DataRow[] NoofDealer = ds.Tables[3].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " and DealerPacks='Dealer'");
            if (NoofDealer.Length > 0)
            {
                int noOfdealer = 0;
                foreach (DataRow value in NoofDealer)
                {
                    noOfdealer = noOfdealer + int.Parse(value[1].ToString());

                }
                str.Append(" <td style='text-align: Right; padding-right:5px;'>" + noOfdealer.ToString() + "</td>");
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'> </td>");
            }

            DataRow[] NoofPacks = ds.Tables[3].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " and DealerPacks='PACS'");
            if (NoofPacks.Length > 0)
            {
                foreach (DataRow value in NoofPacks)
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[1].ToString() + "</td>");
                }
            }
            else
            {
                str.Append(" <td style='text-align: Right; padding-right:5px;'> </td>");
            }

            double TotSale = 0;
            foreach (DataRow drv in dtVariety.Rows)
            {
                DataRow[] DataDealer = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "' AND DealerPacks='Dealer'");
                if (DataDealer.Length > 0)
                {
                    foreach (DataRow value in DataDealer)
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[9].ToString() + "</td>");
                        TotSale = TotSale + double.Parse(value[9].ToString());
                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
                }
                DataRow[] DataPack = ds.Tables[1].Select("Dist_Code= " + dr["Dist_Code"].ToString() + " AND CROP_VERID = '" + drv["CROP_VERID"].ToString() + "' AND DealerPacks='PACS'");
                if (DataPack.Length > 0)
                {
                    foreach (DataRow value in DataPack)
                    {
                        str.Append(" <td style='text-align: Right; padding-right:5px;'>" + value[9].ToString() + "</td>");
                        TotSale = TotSale + double.Parse(value[9].ToString());
                    }
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'></td>");
                }
            }
            str.Append("<td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "Dist_Code= " + dr["Dist_Code"].ToString() + " AND DealerPacks='Dealer'") + "</td>");
            str.Append("<td style='text-align: Right; padding-right:5px;'>" + ds.Tables[1].Compute("Sum(SALE)", "Dist_Code= " + dr["Dist_Code"].ToString() + " AND DealerPacks='PACS'").ToString() + "</td>");

            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotSale) + "</td>");
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append("<td>Grand Total</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[2].Compute("Sum(NoofFarmer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[4].Compute("Sum(NoofFarmer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[3].Compute("Sum(NOOFD)", "DealerPacks='Dealer'").ToString() + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;' >" + ds.Tables[3].Compute("Sum(NOOFD)", "DealerPacks='PACS'").ToString() + "</td>");
        foreach (DataRow dr in dtVariety.Rows)
        {
            try
            {
                str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "'  AND DealerPacks='Dealer' ").ToString())) + "</td>");
            }
            catch
            {
                str.Append("<td style='text-align: Right; padding-right:5px;'>0.00</td>");
            }

            try
            {
                str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SALE)", "CROP_VERID='" + dr["CROP_VERID"].ToString() + "'  AND DealerPacks='PACS' ").ToString())) + "</td>");
            }
            catch
            {
                str.Append("<td style='text-align: Right; padding-right:5px;'>0.00</td>");
            }
        }
        string Dealer = ds.Tables[1].Compute("Sum(SALE)", "DealerPacks='Dealer'").ToString();
        string PACS = ds.Tables[1].Compute("Sum(SALE)", "DealerPacks='PACS'").ToString();
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(Dealer != "" ? Dealer : "0")) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(PACS != "" ? PACS : "0")) + "</td>");
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(SALE)", "").ToString())) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DealerPacsSale(Farmer).xls";
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
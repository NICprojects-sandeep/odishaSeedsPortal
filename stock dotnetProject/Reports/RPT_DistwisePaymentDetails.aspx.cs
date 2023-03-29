using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class Reports_RPT_DistwisePaymentDetails : System.Web.UI.Page
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
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 600;
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
                //btnPrint.Enabled = false;
                FillFinYr();
            }
        }
    }
    private void FillFinYr()
    {
        try
        {
            drpSessionYr.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSessionYr.DataSource = ds;
                    drpSessionYr.DataTextField = "FIN_YR";
                    drpSessionYr.DataValueField = "FIN_YR";
                    drpSessionYr.DataBind();
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
    private void FillData()
    {
        string sql = "DistWisePaymentDetails";
        string[] param = { "@FIN_YEAR", "@SEASON" };
        object[] value = { drpSessionYr.SelectedValue.Trim(), drpSeason.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeason.SelectedValue.Trim() };
        DataSet ds = new DataSet();
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GenerateReport(ds.Tables[0]);
                //btnPrint.Enabled = true;
            }
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }

    }
    private void GenerateReport(DataTable ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");

        str.Append("<td rowspan='2'>District Name</td>");
        str.Append("<td rowspan='2'>No. of Transaction</td>");
        str.Append("<td rowspan='2'>Total No. of Farmer</td>");
        str.Append("<td rowspan='2'>Total Amount</td>");
        str.Append("<td colspan='3' style='text-align: center;'>Request</td>");
        str.Append("<td colspan='3'style='text-align: center;'>Subsidy Paid</td>");
        str.Append("<td></td>");
        str.Append("</tr>");

        //Transaction
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");

        str.Append("<td>No. of Transaction</td>");
        str.Append("<td >Total No. of Farmer</td>");
        str.Append("<td >Total Amount</td>");

        //Subsidiy

        str.Append("<td>No.of Transaction</td>");
        str.Append("<td >Total No. of Farmer</td>");
        str.Append("<td >Total Amount</td>");
        str.Append("<td >Balance Amount</td>");
        str.Append("</tr>");


        foreach (DataRow item in ds.Rows)
        {
            str.Append("<tr>");

            str.Append("<td>" + item["Dist_Name"].ToString() + "</td>");
            str.Append("<td>" + item["TotTrans"].ToString() + "</td>");
            str.Append("<td>" + item["TotFarmer"].ToString() + "</td>");
            str.Append("<td>" + item["TOTAMNT"].ToString() + "</td>");
            str.Append("<td>" + item["ReqTrans"].ToString() + "</td>");
            str.Append("<td>" + item["ReqFarmer"].ToString() + "</td>");
            str.Append("<td>" + item["REQAMNT"].ToString() + "</td>");
            str.Append("<td>" + item["PaidTrans"].ToString() + "</td>");
            str.Append("<td>" + item["PaidFarmer"].ToString() + "</td>");
            str.Append("<td>" + item["PAIDAMNT"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (double.Parse(item["TOTAMNT"].ToString()) - double.Parse(item["REQAMNT"].ToString()))) + "</td>");
            str.Append("</tr>");
        }



        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:red;'>");
        str.Append("<td style='text-align: Right;'>Total :</td>");


        str.Append("<td>" + ds.Compute("Sum(TotTrans)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Compute("Sum(TotFarmer)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Compute("Sum(TOTAMNT)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Compute("Sum(ReqTrans)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Compute("Sum(ReqFarmer)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Compute("Sum(REQAMNT)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Compute("Sum(PaidTrans)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Compute("Sum(PaidFarmer)", "").ToString() + "</td>");
        str.Append("<td>" + ds.Compute("Sum(PAIDAMNT)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", (double.Parse(ds.Compute("Sum(TOTAMNT)", "").ToString()) - double.Parse(ds.Compute("Sum(REQAMNT)", "").ToString()))) + "</td>");

        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DistwisePaymentDetails.xls";
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
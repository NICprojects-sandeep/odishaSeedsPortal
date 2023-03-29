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

public partial class Reports_StatusOfSeedSubsidyPaymentFiles : System.Web.UI.Page
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
            //littime.Text = "<div style='font-family:Tahoma; font-size:12px;   font-weight:bold;'>" + littime.Text + " " + String.Format("{0:D}", dt) + " Time : " + String.Format("{0:T}", dt) + "</div>";
            //FillCrop();
            FillFinYr();
            //FillScheme();
        }
        btnExport.Enabled = false;
        Pay_Rel.Enabled = false;
        Pay_Pen.Enabled = false;
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
    //protected void FillCrop()
    //{
    //    try
    //    {
    //        ds = new DataSet();
    //        ds = DsCrop();
    //        drpCropName.Items.Clear();
    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                this.drpCropName.DataSource = ds;
    //                this.drpCropName.DataValueField = "Crop_Code";
    //                this.drpCropName.DataTextField = "Crop_Name";
    //                this.drpCropName.DataBind();
    //                this.drpCropName.Items.Insert(0, "--- All ---");
    //            }
    //        }
    //    }
    //    catch (ApplicationException applicationException1)
    //    {
    //        ApplicationException applicationException = applicationException1;
    //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
    //    }
    //    catch (Exception exception1)
    //    {
    //        Exception exception = exception1;
    //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
    //        ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
    //    }
    //}
    //public DataSet DsCrop()
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);

    //    SqlCommand cmd = new SqlCommand("SELECT distinct Crop_Code,Crop_Name FROM STOCK_FARMER a inner join mCrop b on a.CROP_ID=b.Crop_Code WHERE is_active = 1 ORDER BY Crop_Name ASC", con);
    //    cmd.CommandType = CommandType.Text;
    //    try
    //    {
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    protected void drpSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSeason();
        try
        {
            drpScheme.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = drpSession.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSchemeByYear(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpScheme.DataSource = ds;
                    drpScheme.DataTextField = "SCHEME_NAME";
                    drpScheme.DataValueField = "SCHEME_CODE";
                    drpScheme.DataBind();
                    drpScheme.Items.Insert(0, new ListItem("ALL", "ALL"));
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
    }
    protected void drpSeassion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillScheme();
    }
    private void FillSeason()
    {
        try
        {
            drpSeassion.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = drpSession.SelectedValue;
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSeassion(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSeassion.DataSource = ds;
                    drpSeassion.DataTextField = "SEASSION_NAME";
                    drpSeassion.DataValueField = "SHORT_NAME";
                    drpSeassion.DataBind();
                    drpSeassion.Items.Insert(0, new ListItem("ALL", "ALL"));
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
            drpScheme.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = drpSession.SelectedValue;
            objUserBEL.SEASON = drpSeassion.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSchemeYear(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpScheme.DataSource = ds;
                    drpScheme.DataTextField = "SCHEME_NAME";
                    drpScheme.DataValueField = "SCHEME_CODE";
                    drpScheme.DataBind();
                    drpScheme.Items.Insert(0, new ListItem("ALL", "ALL"));
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (drpSeassion.SelectedValue == "ALL" && drpScheme.SelectedValue != "ALL")
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Please select any Season!');", true);
        }
        else
        {
            lblTotal.Visible = false;
            Pay_Rel.Visible = false;
            Pay_Pen.Visible = false;
            btnExport.Enabled = false;
            gvDetails.DataSource = null;
            gvDetails.DataBind();

            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = drpSession.SelectedValue;
            objUserBEL.SEASON = drpSeassion.SelectedValue;
            objUserBEL.scheme_code = drpScheme.SelectedValue;
            objUserBEL.DEPT_TYPE = drpType.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            if (ddlAppType.SelectedValue == "Dealer")
            {
                ds = objUserDLL.FillPaymentYearD(objUserBEL);
            }
            else if (ddlAppType.SelectedValue == "PACS")
            {
                ds = objUserDLL.FillPaymentYearP(objUserBEL);
            }
            else
            {
                ds = objUserDLL.FillPaymentYearA(objUserBEL);
            }
            if (ds != null)
            {
                if (drpType.SelectedValue == "RELNIC")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataView view = new DataView(ds.Tables[0]);
                        view.RowFilter = string.Format("Status= 'Payment Released' or Status = 'Payment Released.'");

                        object sumPay_Rel;
                        sumPay_Rel = ds.Tables[0].Compute("Sum(Amount)", view.RowFilter);
                        Pay_Rel.Text = "Payment Released : " + sumPay_Rel.ToString();

                        view.RowFilter = string.Format("Status= 'Pending at PFMS.' or Status = 'Pending at Scheme_Officer' or Status = 'Pending at OSSC' or Status = 'Approved by OSSC' or Status = 'Reject by Dept/Ossc'");
                        object sumPay_Pen;
                        sumPay_Pen = ds.Tables[0].Compute("Sum(Amount)", view.RowFilter);
                        Pay_Pen.Text = "Payment Pending at PFMS/Dept. : " + sumPay_Pen.ToString();
                        gvDetails.Visible = true;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        gvDetails.DataSource = ds;
                        gvDetails.DataBind();
                        btnExport.Enabled = true;
                        Pay_Rel.Visible = true;
                        Pay_Pen.Visible = true;
                    }
                }
                else if (drpType.SelectedValue == "PENNIC")
                {


                    DataView view = new DataView(ds.Tables[0]);


                    object sumPay_Rel;
                    sumPay_Rel = ds.Tables[0].Compute("Sum(Amount)", null);
                    lblTotal.Text = "Total : " + sumPay_Rel.ToString();

                    gvDetails.DataSource = null;
                    gvDetails.DataBind();
                    gvDetails.Visible = false;
                    GridView1.Visible = true;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    btnExport.Enabled = true;
                    lblTotal.Visible = true;
                }
                else
                {
                    gvDetails.DataSource = null;
                    gvDetails.DataBind();
                    gvDetails.Visible = false;
                    GridView1.Visible = true;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    btnExport.Enabled = true;
                }
            }

            Session["dsTrans"] = ds;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {            
            //objUserBEL = new BLL_DropDown();
            //objUserBEL.FIN_YR = drpSession.SelectedValue;
            //objUserBEL.SEASON = drpSeassion.SelectedValue;
            //objUserBEL.scheme_code = drpScheme.SelectedValue;
            //objUserBEL.DEPT_TYPE = drpType.SelectedValue;
            //objUserDLL = new DLL_DropDown();
            //ds = new DataSet();
            //if (ddlAppType.SelectedValue == "Dealer")
            //{
            //    ds = objUserDLL.FillPaymentYearD(objUserBEL);
            //}
            //else if (ddlAppType.SelectedValue == "PACS")
            //{
            //    ds = objUserDLL.FillPaymentYearP(objUserBEL);
            //}
            //else
            //{
            //    ds = objUserDLL.FillPaymentYearA(objUserBEL);
            //}
            DataSet ds=(DataSet)Session["dsTrans"];
            
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            //DataTable dt = new DataTable("MyTable");
            //dt.Columns.Add(new DataColumn("Sl. No.", typeof(string)));
            //dt.Columns.Add(new DataColumn("Generated File Name", typeof(string)));
            //dt.Columns.Add(new DataColumn("File Date", typeof(string)));
            //dt.Columns.Add(new DataColumn("No. of Transaction", typeof(string)));
            //dt.Columns.Add(new DataColumn("No. of beneficiary", typeof(string)));
            //dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            //dt.Columns.Add(new DataColumn("Status", typeof(string)));
            if (drpType.SelectedValue == "RELNIC")
            {
                DataView view = new DataView(ds.Tables[0]);
                view.RowFilter = string.Format("Status= 'Payment Released' or Status = 'Payment Released.'");

                object sumPay_Rel;
                sumPay_Rel = ds.Tables[0].Compute("Sum(Amount)", view.RowFilter);

                view.RowFilter = string.Format("Status= 'Pending at PFMS.' or Status = 'Pending at Scheme_Officer' or Status = 'Pending at OSSC' or Status = 'Approved by OSSC' or Status = 'Reject by Dept/Ossc'");
                object sumPay_Pen;
                sumPay_Pen = ds.Tables[0].Compute("Sum(Amount)", view.RowFilter);

                DataRow dr = dt.NewRow();
                dr["Sl. No."] = DBNull.Value;
                dr["Generated File Name"] = "Payment Released : " + sumPay_Rel.ToString();
                dr["File Date"] = DBNull.Value;
                dr["No. of Transaction"] = DBNull.Value;
                dr["No. of beneficiary"] = DBNull.Value;
                dr["Amount"] = DBNull.Value;
                dr["Status"] = "Payment Pending at PFMS/Dept. : " + sumPay_Pen.ToString();
                dt.Rows.Add(dr);
            }
            //ds.Tables.Add(dt);

            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=StatusOfSeedSubsidyPaymentFiles.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);


            GridView1.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex) { }
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Pay_Rel.Enabled = false;
            Pay_Pen.Enabled = false;
            int sumrel = 0;
            int sumpen = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row["Status"] == "Payment Released" || row["Status"] == "Payment Released.")
                {
                    sumrel += Convert.ToInt32(row["Amount"]);
                }
                if (row["Status"] == "Pending at PFMS." || row["Status"] == "Pending at Scheme_Officer" || row["Status"] == "Pending at OSSC" || row["Status"] == "Approved by OSSC" || row["Status"] == "Reject by Dept/Ossc")
                {
                    sumpen += Convert.ToInt32(row["Amount"]);
                }

            }

            
            //string a = null;
            //for (int i = 0; i < gvDetails.Rows.Count; i++)
            //{
            //    if (gvDetails.Rows[i].Cells[6].Text == "Payment Released" || gvDetails.Rows[i].Cells[6].Text == "Payment Released.")
            //    {
            //        sumrel += int.Parse(gvDetails.Rows[i].Cells[5].Text);
            //    }
            //    if (gvDetails.Rows[i].Cells[6].Text == "Pending at PFMS." || gvDetails.Rows[i].Cells[6].Text == "Pending at Scheme_Officer" || gvDetails.Rows[i].Cells[6].Text == "Pending at OSSC" || gvDetails.Rows[i].Cells[6].Text == "Approved by OSSC" || gvDetails.Rows[i].Cells[6].Text == "Reject by Dept/Ossc")
            //    {
            //        sumpen += int.Parse(gvDetails.Rows[i].Cells[5].Text);
            //    }
            //    a = gvDetails.Rows[i].Cells[5].Text.ToString();
            //}

            //Pay_Rel.Enabled = true;
            //Pay_Pen.Enabled = true;
            Pay_Rel.Text = sumrel.ToString();
            Pay_Pen.Text = sumpen.ToString();
        }
        catch (Exception ex) { }
    }
}
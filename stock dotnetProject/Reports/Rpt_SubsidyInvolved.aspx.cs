using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Rpt_SubsidyInvolved : System.Web.UI.Page
{
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillFinYear();
            FillSeassion();
        }
    }

    private void FillFinYear()
    {
        try
        {
            ddlFinYr.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFinYr.DataSource = ds.Tables[0];
                    ddlFinYr.DataTextField = "FIN_YR";
                    ddlFinYr.DataValueField = "FIN_YR";
                    ddlFinYr.DataBind();
                }
            }
            // = ds.Tables[0].Rows[0]["FIN_YR"].ToString();
            //ddlFinYr.Items
            //ddlFinYr.Items.Insert(0, "--SELECT--");
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
    private void FillSeassion()
    {
        try
        {
            ddlSeassion.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSeassion(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSeassion.DataSource = ds;
                    ddlSeassion.DataTextField = "SEASSION_NAME";
                    ddlSeassion.DataValueField = "SHORT_NAME";
                    ddlSeassion.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    private void FillGrid()
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.FIN_YR = ddlFinYr.SelectedItem.Text;
        objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
        ds = new DataSet();
        objUserDLL = new DLL_DropDown();

        ds = objUserDLL.FillSubsidyInvolved(objUserBEL);
        GenerateReport(ds);
    }

    private void GenerateReport(DataSet ds)
    {
        DataTable dtData = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        str.Append("<h2 style='color:#088A66'>Subsidy involved for the year <b>"+ddlFinYr.SelectedItem.Text+"</b> and season <b>"+ddlSeassion.SelectedItem.Text+"</b> as on "+DateTime.Now.ToString()+"</h2>");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Left;'>Sl No</td>");
        str.Append("<td style='text-align: Left;'>Crop Category Name</td>");
        str.Append("<td style='text-align: Right;'>Distinct Farmer</td>");
        str.Append("<td style='text-align: Right;'>Quantity Sold(In Qtl)</td>");
        str.Append("<td style='text-align: Right;'>Subsidy Involved(In Rs.)</td>");
        str.Append("</tr>");
        int i = 0;
        foreach (DataRow dr in dtData.Rows)
        {
            i++;
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["Category_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" +  Int32.Parse(dr["DIST_FARMER"].ToString()) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["SUBSIDY_QTL"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["SUBSIDY_AMOUNT"].ToString())) + "</td>");
            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Total</td>");
        str.Append("<td></td>");
        str.Append("<td style='text-align: Right;'>" + Int32.Parse(dtData.Compute("Sum(DIST_FARMER)", "").ToString()) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(SUBSIDY_QTL)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(SUBSIDY_AMOUNT)", "").ToString())) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }

    protected void ddlFinYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSeassion();
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=SeedSubsidyInvolved" + ddlFinYr.SelectedItem.Text +"_"+ddlSeassion.SelectedItem.Text+"_"+DateTime.Now.ToString()+ ".xls";
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
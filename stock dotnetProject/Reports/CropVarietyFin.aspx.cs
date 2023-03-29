using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CropVarietyFin : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillFinYear();
            FillSeason();
            //FillGrid();
        }
    }
    private void FillFinYear()
    {
        try
        {
            ddlFinYear.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFinYear.DataSource = ds;
                    ddlFinYear.DataTextField = "FIN_YR";
                    ddlFinYear.DataValueField = "FIN_YR";
                    ddlFinYear.DataBind();
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
    private void FillSeason()
    {
        try
        {
            ddlSeason.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = ddlFinYear.SelectedValue;
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSeassion(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSeason.DataSource = ds;
                    ddlSeason.DataTextField = "SEASSION_NAME";
                    ddlSeason.DataValueField = "SHORT_NAME";
                    ddlSeason.DataBind();
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
    protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSeason();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.UserType = ddlType.SelectedItem.Text;
        objUserBEL.FIN_YR = ddlFinYear.SelectedItem.Text;
        objUserBEL.SEASON = ddlSeason.SelectedItem.Text;
        //BAL.SEASON = "R";
        ds = new DataSet();
        objUserDLL = new DLL_DropDown();

        //ds = DAL.GetIncentive(BAL);
        //ProcessToServer(ds);

        ds = objUserDLL.CropVarFin(objUserBEL);
        GenerateReport(ds);
    }
    private void GenerateReport(DataSet ds)
    {
        DataTable dtData = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:1000px; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: center;'>Sl No</td>");
        str.Append("<td style='text-align: Left;'>Crop Name</td>");
        str.Append("<td style='text-align: Left;'>Variety Name</td>");
        str.Append("<td style='text-align: Right;'>Qty(in Qtl)</td>");
        str.Append("</tr>");
        int i = 0;
        foreach (DataRow dr in dtData.Rows)
        {
            i++;
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");            
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["QTY"].ToString())) + "</td>");
            str.Append("</tr>");        
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Total</td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(QTY)", "").ToString())) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=CropVariety" + ddlFinYear.SelectedItem.Text + "-"+ddlSeason.SelectedItem.Text+"-"+ddlType.SelectedItem.Text+".xls";
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
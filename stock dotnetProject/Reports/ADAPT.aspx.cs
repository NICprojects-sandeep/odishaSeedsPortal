using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ADAPT : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillFinYr();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        string sql = "ADAPT";
        string[] param = { "@FIN_YR", "@Seassion" };
        object[] value = { drpSession.SelectedValue.Trim(), drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull : drpSeassion.SelectedValue.Trim() };
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
    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Dist Name  </td>");
        str.Append("<td>Block Name  </td>");
        DataView view = new DataView(ds.Tables[1]);
        DataTable dtVariety = view.ToTable(true, "CROP_ID","Crop_Name");
        foreach (DataRow dr in dtVariety.Rows)
        {
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + dr["Crop_Name"].ToString() + "</td>");

        }
        str.Append(" <td style='text-align: Right; padding-right:5px;'>Total</td>");
        str.Append("</tr>");

       
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["vch_DistrictName"].ToString() + "</td>");
            str.Append("<td>" + dr["vch_BlockName"].ToString() + "</td>");

            double TotSale = 0;
            foreach (DataRow drv in dtVariety.Rows)
            {
                double DealerSale = 0;

                DataRow[] Datadealer = ds.Tables[1].Select("int_DistrictID= " + dr["int_DistrictID"].ToString() + " AND int_BlockID = '" + dr["int_BlockID"].ToString() + "' AND CROP_ID = '" + drv["CROP_ID"].ToString()+ "'");
                if (Datadealer.Length > 0)
                {
                    foreach (DataRow value in Datadealer)
                    {
                        DealerSale = DealerSale + double.Parse(value[6].ToString());
                        TotSale = TotSale + double.Parse(value[6].ToString());
                    }
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", DealerSale) + "</td>");
                }
                else
                {
                    str.Append(" <td style='text-align: Right; padding-right:5px;'>&nbsp;</td>");
                }                
            }
            str.Append(" <td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", TotSale) + "</td>");
            str.Append("</tr>");
        }

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold; color:Red;'>");
        str.Append("<td>Grand Total</td>");
        str.Append("<td></td>");
        foreach (DataRow dr in dtVariety.Rows)
        {
            string a = ds.Tables[1].Compute("Sum(TOT_QTL)", "CROP_ID='" + dr["CROP_ID"].ToString() + "'").ToString();            
            //str.Append("<td style='text-align: Right; padding-right:5px;'>" +  string.Format("{0:f2}", double.Parse(a)) + "</td>");
            if (a == "")
            {
                str.Append("<td style='text-align: Right; padding-right:5px;'>--</td>");
            }
            else
            {

                str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(a)) + "</td>");
            }
            

        }
        str.Append("<td style='text-align: Right; padding-right:5px;'>" + string.Format("{0:f2}", double.Parse(ds.Tables[1].Compute("Sum(TOT_QTL)", "").ToString())) + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=Block_CropWise.xls";
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
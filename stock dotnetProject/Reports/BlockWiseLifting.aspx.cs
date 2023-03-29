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

public partial class Reports_BlockWiseLifting :  System.Web.UI.Page
{
    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            btnToExcel.Enabled = false;
            FillDist();
        }
    }
    private void FillDist()
    {
        try
        {

            ds = new DataSet();
            ds = DsDist();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpDist.DataSource = ds;
                    this.drpDist.DataValueField = "LGDistrict";
                    this.drpDist.DataTextField = "Mod_dist_name";
                    this.drpDist.DataBind();
                }
            }
            this.drpDist.Items.Insert(0, "--- SELECT ---");
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
    private DataSet DsDist()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT [LGDistrict],[Mod_dist_name] FROM [dafpSeed].[dbo].[dist] order by Mod_dist_name", con);
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
        string sql = "STOCK_BLOCKStock";
        string[] param = { "@DIST_ID" };
        object[] value = { drpDist.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnToExcel.Enabled = true;
            GenerateReport(ds);
        }
        else
        {
            btnToExcel.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tbltxt'  align='center'>No Records Found  !</div>";
        }
    }
    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        //str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<span>District :" + drpDist.SelectedItem.Text + "</span>");
        //str.Append( );
        //str.Append("</tr></table>");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='15' cellspacing='10' class='tbltxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");

        str.Append("<td>SI.NO.</td>");
        str.Append("<td >Block Name</td>");
        str.Append("<td style='text-align: right;'>Recv Qty</td>");
        str.Append("<td style='text-align: right;'>Sold Qty</td>");
        str.Append("<td style='text-align: right;'>Avl Qty</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");            
            str.Append("<td>" + item["block_name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["RECV_QTY"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["SOLD_QTY"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + item["AVL_QTY"].ToString() + "</td>");
            str.Append("</tr>");
            i++;
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>&nbsp;</td>");        
        str.Append("<td>Total :</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(RECV_QTY)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(SOLD_QTY)", "").ToString() + "</td>");
        str.Append("<td style='text-align: Right;'>" + ds.Tables[0].Compute("Sum(AVL_QTY)", "").ToString() + "</td>");
        str.Append("</tr>");
        str.Append("</table>");

        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=BlockwiseStock.xls";
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
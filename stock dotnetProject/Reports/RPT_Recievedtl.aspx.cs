using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_Recievedtl : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = Session["DistCode"].ToString();
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            DataSet ds = new DataSet();
            ds = objUserDLL.FillGoDownByDistCodeUserType(objUserBEL);
            drpGodown.DataSource = ds.Tables[0];
            drpGodown.DataValueField = "GODOWN_ID";
            drpGodown.DataTextField = "GODOWN_NAME";
            drpGodown.DataBind();
            drpGodown.Items.Insert(0, "---All---");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string sql = "STOCK_RPT_Recievedtl";
        string[] param = { "@Dist_Code", "@Godown_ID", "@User_Type", "@FromDt", "@ToDt", "@Season" };
        object[] value = { Session["DistCode"].ToString(), drpGodown.SelectedValue.Trim() == "---All---" ? "0" : drpGodown.SelectedValue.Trim(), Session["UserType"].ToString(), DateTime.Parse(txtFrmDt.Text.Trim()), DateTime.Parse(txtToDt.Text.Trim()),drpSeassion.SelectedValue.Trim() == "0" ? System.Convert.DBNull
 : drpSeassion.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds);
            btnExport.Enabled = true;

        }
        else
        {
            btnExport.Enabled = false;
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";

        }

    }

    private void GenerateReport(DataSet ds)
    {
        btnExport.Enabled = true;
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Sl. No. </td>");
        str.Append("<td>Receive Date</td>");
        str.Append("<td>Godown Name</td>");
        str.Append("<td>P.R/Chalan No.</td>");
        str.Append("<td>Lot No.</td>");
        str.Append("<td>Agency Name</td>");
        str.Append("<td>Crop Name</td>");
        str.Append("<td>Variety Name</td>");

        str.Append("<td style='text-align: Right;'>Bag Size(In kg.)</td>");
        str.Append("<td style='text-align: Right;'>No. of Bags</td>");
        str.Append("<td style='text-align: Right;'>Recv. Quantity</td>");
        str.Append("</tr>");
        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["Recv_Date"].ToString() + "</td>");
            str.Append("<td>" + dr["Godown_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["PR_Chalan"].ToString() + "</td>");
            str.Append("<td>" + dr["Lot_No"].ToString() + "</td>");
            str.Append("<td>" + dr["AgenciesName"].ToString() + "</td>");
            str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
            str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["Bag_Size_In_kg"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["Recv_No_Of_Bags"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + dr["Recv_Quantity"].ToString() + "</td>");
            str.Append("</tr>");
            i++;

        }
        string TotBag = ds.Tables[0].Compute("Sum(Recv_No_Of_Bags)", "").ToString();
        string TotQty = ds.Tables[0].Compute("Sum(Recv_Quantity)", "").ToString();
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;' colspan='9'>Total : </td>");
        str.Append("<td style='text-align: Right;'>" + TotBag + "</td>");
        str.Append("<td style='text-align: Right;'>" + TotQty + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=ReceiveDtl.xls";
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
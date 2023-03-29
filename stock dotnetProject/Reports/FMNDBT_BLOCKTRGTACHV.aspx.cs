using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class Reports_FMNDBT_BLOCKTRGTACHV : System.Web.UI.Page
{
    BLL_ACC_LYR obj = new BLL_ACC_LYR();
    BLL_Obj bll_obj = new BLL_Obj();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillImplement();
        }
    }
    protected void fillImplement()
    {
        bll_obj = new BLL_Obj();
        bll_obj.Query = "select * from Implement ";
        DataSet dsImp = obj.returnDataset(bll_obj);
        if (dsImp.Tables[0].Rows.Count > 0)
        {
            drpImplement.DataSource = dsImp;
            drpImplement.DataTextField = "Implement";
            drpImplement.DataValueField = "Impl_id";
            drpImplement.DataBind();
            drpImplement.Items.Insert(0, "--select--");
        }
        else
        {
            drpImplement.Items.Insert(0, "--select--");
        }
    }


    private void GenerateReport(DataSet ds)
    {

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='3' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;' ><td rowspan='2' >Sl No</td><td rowspan='2' >DIST</td><td rowspan='2' >BLOCK</td><td colspan='4' style='text-align:center;'>TARGET</td><td colspan='4' style='text-align:center;'>BOOKING</td><td colspan='4' style='text-align:center;'>PERMIT</td><td colspan='4' style='text-align:center;'>BALANCE</td>");

        str.Append("</tr>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: center;'>GENERAL</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");
        str.Append("<td style='text-align: center;'>TOTAL</td>");

        str.Append("<td style='text-align: center;'>GENERAL</td>");
        str.Append("<td style='text-align: center; '>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");
        str.Append("<td style='text-align: center;'>TOTAL</td>");

        str.Append("<td style='text-align: center;'>GENERAL</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");
        str.Append("<td style='text-align: center;'>TOTAL</td>");

        str.Append("<td style='text-align: center;'>GENERAL</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");
        str.Append("<td style='text-align: center;'>TOTAL</td>");
        str.Append("</tr>");
        int sl = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + sl.ToString() + "</td>");
            sl++;
            str.Append("<td>" + dr["dist_name"].ToString() + "</td>");
            str.Append("<td>" + dr["block_name"].ToString() + "</td>");

            str.Append("<td style='text-align: center;'>" + dr["TRGEN"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["TRGSC"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["TRGST"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["TOTALTARGET"].ToString() + "</td>");

            str.Append("<td style='text-align: center;'>" + dr["BOOKGEN"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["BOOKSC"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["BOOKST"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["TOTALBOOK"].ToString() + "</td>");

            str.Append("<td style='text-align: center;'>" + dr["PERGEN"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["PERSC"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["PERST"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["TOTALPERMI"].ToString() + "</td>");

            str.Append("<td style='text-align: center;'>" + dr["BALGEN"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["BALSC"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["BALST"].ToString() + "</td>");
            str.Append("<td style='text-align: center;'>" + dr["TotBal"].ToString() + "</td>");

            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td  colspan='3'>Grand Total</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(TRGEN)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(TRGSC)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(TRGST)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(TOTALTARGET)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(BOOKGEN)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(BOOKSC)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(BOOKST)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(TOTALBOOK)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(PERGEN)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(PERSC)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(PERST)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(TOTALPERMI)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(BALGEN)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(BALSC)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(BALST)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;'>" + ds.Tables[0].Compute("Sum(TotBal)", "").ToString() + "</td>");
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void drpImplement_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddl_scheme_SelectedIndexChanged(sender, e);
        }
        catch (Exception)
        {

            //throw;
        }

    }
    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpImplement.SelectedIndex.ToString() == "0")
            {
                litReport.Text = " ";
            }
            else
            {

                bll_obj = new BLL_Obj();
                DataSet ds = new DataSet();
                bll_obj.Query = "ADAPT_BlOCKBOOKING_REPORT";
                bll_obj.ParamList = new string[] { "@F_YEAR", "@ImpID" };
                bll_obj.ParamobjList = new object[] { drpYr.SelectedValue.Trim(), drpImplement.SelectedValue.Trim() };
                ds = obj.param_passing_fetch_storeprocedure(bll_obj);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    Session["dt"] = ds.Tables[0];
                    GenerateReport(ds);



                }
                else
                {
                    btnExport.Visible = false;
                    Session["dt"] = null;
                    litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
                }
            }


        }
        catch (Exception ex)
        {

        }
    }
    protected void drpYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillImplement();
        litReport.Text = "";
        btnExport.Visible = false;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string csv = string.Empty;
            DataTable dt = (DataTable)Session["dt"];
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }

            //Add new line.
            csv += "\r\n";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + drpImplement.SelectedItem.Text + "" + drpYr.SelectedValue.Trim() + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();


        }
        catch (Exception ex) { }
    }
}
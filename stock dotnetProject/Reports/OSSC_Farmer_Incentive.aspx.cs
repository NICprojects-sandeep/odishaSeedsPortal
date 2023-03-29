using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_OSSC_Farmer_Incentive : System.Web.UI.Page
{
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblAgency.Text = "OSSC";
            lblCrop.Text = "Paddy";
            lblFinYr.Text = "2020-21";
            //lblSeason.Text = "Rabi";
            CreteDataTable();
            FillGrid();
        }
    }
    private void CreteDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[14] 
                { 
                    new DataColumn("SG_Name", typeof(string)),
                    new DataColumn("Father_husband_name", typeof(string)),
                    new DataColumn("LotNo", typeof(string)),
                    new DataColumn("Dist_code", typeof(string)),
                    new DataColumn("Farmerid", typeof(string)),                    
                    new DataColumn("Ref_No", typeof(string)),
                    new DataColumn("Dist_name", typeof(string)),
                    new DataColumn("villg_name",typeof(string)),
                    new DataColumn("Varity_Code",typeof(string)),
                    new DataColumn("Varity_Name",typeof(string)), 
                    new DataColumn("Class_code",typeof(string)),
                    new DataColumn("ProcessedArea",typeof(string)),
                    new DataColumn("Inspected_Area",typeof(string)),
                    new DataColumn("Qtl_Ha",typeof(decimal))
                });
        Session.Add("dt", dt);
    }
    private void FillGrid()
    {
        StringBuilder str = new StringBuilder("");
        BAL = new BLL_DropDown();
        BAL.FIN_YR = "2020-21";
        //BAL.SEASON = "R";
        ds = new DataSet();
        DAL = new DLL_DropDown();

        ////To be commented BEGIN
        //ds = DAL.GetIncentive(BAL);
        //ProcessToServer(ds);
        ////To be commented END

        //To be commented BEGIN
        ds = DAL.FillIncentive(BAL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GenerateReport(ds);
            }
            else
            {
                str.Append("<table style='width:1325px; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
                str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
                str.Append("<td style='text-align: Left;color: blue'>No record found </td>");
                str.Append("</tr>");
                str.Append("</table>");
                litReport.Text = str.ToString();
            }
        }
        else
        {
            str.Append("<table style='width:1325px; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
            str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
            str.Append("<td style='text-align: Left;color: blue'>No record found </td>");
            str.Append("</tr>");
            str.Append("</table>");
            litReport.Text = str.ToString();

        }
        //To be commented END

        //gvIncentive.DataSource = ds;
        //gvIncentive.DataBind();


        //decimal TotSUBSIDISED_AREA = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SUBSIDISED_AREA"));
        //decimal TotFARMER_SUBSIDY = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FARMER_SUBSIDY"));
        //decimal TotOSSC_SUBSIDY = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OSSC_SUBSIDY"));
        //gvIncentive.FooterRow.Cells[0].Text = "Total";
        //gvIncentive.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;

        
        //gvIncentive.FooterRow.Cells[11].Text = TotSUBSIDISED_AREA.ToString("N2");
        //gvIncentive.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        //gvIncentive.FooterRow.Cells[15].Text = TotFARMER_SUBSIDY.ToString("N2");
        //gvIncentive.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Right;
        //gvIncentive.FooterRow.Cells[16].Text = TotOSSC_SUBSIDY.ToString("N2");
        //gvIncentive.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
    }

    private void GenerateReport(DataSet ds)
    {
        DataTable dtData = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:1325px; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
       
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;'>Sl No</td>");
        str.Append("<td style='text-align: Right;'>Farmer ID</td>");
        str.Append("<td style='text-align: Right;'>SG Name</td>");
        str.Append("<td style='text-align: Right;'>Father/Husband Name</td>");
        str.Append("<td style='text-align: Right;'>District Name</td>");
        str.Append("<td style='text-align: Right;'>Village Name</td>");
        str.Append("<td style='text-align: Right;'>Lot Number</td>");
        str.Append("<td style='text-align: Right;'>Reference Number</td>");
        str.Append("<td style='text-align: Right;'>Season</td>");
        str.Append("<td style='text-align: Right;'>Variety Name</td>");
        str.Append("<td style='text-align: Right;'>Inspected Area (in Hectare)</td>");
        str.Append("<td style='text-align: Right;'>Subsidised Area (in Hectare)</td>");
        str.Append("<td style='text-align: Right;'>Tag Issue Quantity (in Quintals)</td>");
        str.Append("<td style='text-align: Right;'>Qtl/Ha</td>");
        str.Append("<td style='text-align: Right;'>Admissibe Quantity (in Quintals)</td>");
        str.Append("<td style='text-align: Right;'>SeedGrower Subsidy (in Rupees</td>");
        str.Append("<td style='text-align: Right;'>OSSC Subsidy (in Rupees)</td>");
        str.Append("<td style='text-align: Right;'>Account Holder Name</td>");
        str.Append("<td style='text-align: Right;'>Account Number</td>");
        str.Append("<td style='text-align: Right;'>Bank Name</td>");
        str.Append("<td style='text-align: Right;'>Branch Name</td>");
        str.Append("<td style='text-align: Right;'>IFSC Code</td>");
        str.Append("</tr>");
        int i = 0;
        foreach (DataRow dr in dtData.Rows)
        {
            i++;
            str.Append("<tr>");
            str.Append("<td>" + i.ToString() + "</td>");
            str.Append("<td>" + dr["FARMER_ID"].ToString() + "</td>");
            str.Append("<td>" + dr["FARMER_NAME"].ToString() + "</td>");
            str.Append("<td>" + dr["FATHER_HUSBAND_NAME"].ToString() + "</td>");
            str.Append("<td>" + dr["DIST_NAME"].ToString() + "</td>");
            str.Append("<td>" + dr["VILLG_NAME"].ToString() + "</td>");
            str.Append("<td>" + dr["LOT_NO"].ToString() + "</td>");
            str.Append("<td>" + dr["REF_NO"].ToString() + "</td>");
            str.Append("<td>" + dr["SEASON"].ToString() + "</td>");
            str.Append("<td>" + dr["VARIETY_NAME"].ToString() + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["INSPECTED_AREA"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["SUBSIDISED_AREA"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["PROCESSED_QTY"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["Qtl_Ha"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["SUBSIDISED_QTY"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["FARMER_SUBSIDY"].ToString())) + "</td>");
            str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dr["OSSC_SUBSIDY"].ToString())) + "</td>");
            str.Append("<td>" + dr["ACC_HOLDERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["ACC_NO"].ToString()+ "</td>");
            str.Append("<td>" + dr["VCHBANKNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHBRANCHNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["IFSC_CODE"].ToString() + "</td>");
            str.Append("</tr>");
        }
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>Total</td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(SUBSIDISED_AREA)", "").ToString())) + "</td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(FARMER_SUBSIDY)", "").ToString())) + "</td>");
        str.Append("<td style='text-align: Right;'>" + string.Format("{0:f2}", double.Parse(dtData.Compute("Sum(OSSC_SUBSIDY)", "").ToString())) + "</td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");
        str.Append("<td></td>");        
        str.Append("</tr>");
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    private void ProcessToServer(DataSet ds)
    {
        DataTable dtIncentive = (DataTable)Session["dt"];
        int Val = 0;
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        int COUNT = dt.Rows.Count;
                        for (int r = 0; r < COUNT; ++r)
                        {
                            DataRow row = dt.Rows[r];

                            dtIncentive.Rows.Add(row["SG_Name"].ToString(), row["Father_husband_name"].ToString(), row["LotNo"].ToString(), row["Dist_code"].ToString(), row["Farmerid"].ToString(), row["Ref_No"].ToString(), row["Dist_name"].ToString(), row["villg_name"].ToString(), row["Varity_Code"].ToString(), row["Varity_Name"].ToString(), row["Class_code"].ToString(), row["ProcessedArea"].ToString(), row["Inspected_Area"].ToString(), row["Qtl_Ha"].ToString());
                            Session.Add("dt", dtIncentive);
                        }
                    }
                }
            }
        }

        DataTable dt1 = (DataTable)Session["dt"];
        //dt1.DefaultView.Sort = "Farmerid,ProcessedArea,Inspected_Area";
        dt1.DefaultView.Sort = "Farmerid,Qtl_Ha DESC";
        dt1 = dt1.DefaultView.ToTable();
        if (dt1 != null)
        {
            if (dt1.Rows.Count > 0)
            {
                dt1.TableName = "A";
                string A = ConvertDatatableToXML(dt1);
                BAL = new BLL_DropDown();
                BAL.FIN_YR = "2020-21";
                //BAL.SEASON = "R";

                BAL.XML_Value = ConvertDatatableToXML(dt1);
                DAL = new DLL_DropDown();
                DAL.InsertIncentive(BAL, out Val);
                if (Val == 1)
                {
                    string pop = "alert('Transaction Completed!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }                
            }
        }
    }
    public string ConvertDatatableToXML(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    int total = 0;
    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = total.ToString();
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=SeedIncentive" + lblFinYr.Text + ".xls";
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
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", "attachment;filename=SeedIncentive2017-18.xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

    //        //To Export all pages
    //        gvIncentive.AllowPaging = false;
    //        this.FillGrid();

    //        gvIncentive.HeaderRow.BackColor = Color.White;
    //        foreach (TableCell cell in gvIncentive.HeaderRow.Cells)
    //        {
    //            cell.BackColor = gvIncentive.HeaderStyle.BackColor;
    //        }
    //        foreach (GridViewRow row in gvIncentive.Rows)
    //        {
    //            row.BackColor = Color.White;
    //            foreach (TableCell cell in row.Cells)
    //            {
    //                if (row.RowIndex % 2 == 0)
    //                {
    //                    cell.BackColor = gvIncentive.AlternatingRowStyle.BackColor;
    //                }
    //                else
    //                {
    //                    cell.BackColor = gvIncentive.RowStyle.BackColor;
    //                }
    //                cell.CssClass = "textmode";
    //            }
    //        }

    //        gvIncentive.RenderControl(hw);

    //        //style to format numbers to string
    //        string style = @"<style> .textmode { } </style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //    }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}
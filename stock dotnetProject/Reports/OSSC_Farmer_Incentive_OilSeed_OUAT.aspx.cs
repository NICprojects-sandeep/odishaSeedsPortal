using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_OSSC_Farmer_Incentive_OilSeed_OUAT : System.Web.UI.Page
{
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    DataSet ds;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (this.Session["UserType"].ToString() == "ADMI")
        {
            this.Page.MasterPageFile = "../MasterPages/Admin.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "NFSM")
        {
            this.Page.MasterPageFile = "../MasterPages/Scheme.master";
            return;
        }//NMAD
        if (this.Session["UserType"].ToString() == "NMAD")
        {
            this.Page.MasterPageFile = "../MasterPages/Scheme.master";
            return;
        }
    }//3827
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dvOSSC.Visible = false;
            dvDEPT.Visible = false;
            lblAgency.Text = "OUAT";
            lblCrop.Text = "OilSeed";
            //lblFinYr.Text = "2019-20";
            //lblSeason.Text = "Rabi";
            CreteDataTable();
            //FillGrid();
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
        BAL = new BLL_DropDown();
        BAL.FIN_YR = lblFinYr.Text;
        BAL.SEASON = lblSeason.Text;
        ds = new DataSet();
        DAL = new DLL_DropDown();

        //ds = DAL.GetIncentiveOilSeed_OUAT(BAL);
        //ProcessToServer(ds);

        ds = DAL.FillIncentiveOilSeed_OUAT(BAL);
        GenerateReport(ds);
    }
    private void GenerateReport(DataSet ds)
    {
        DataTable dtData = ds.Tables[0];
        StringBuilder str = new StringBuilder("");
        if (dtData != null)
        {
            if (dtData.Rows.Count > 0)
            {
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
                str.Append("<td style='text-align: Right;'>OUAT Subsidy (in Rupees)</td>");
                str.Append("<td style='text-align: Right;'>Account Holder Name</td>");
                str.Append("<td style='text-align: Right;'>Account Number</td>");
                str.Append("<td style='text-align: Right;'>Bank Name</td>");
                str.Append("<td style='text-align: Right;'>Branch Name</td>");
                str.Append("<td style='text-align: Right;'>IFSC Code</td>");
                str.Append("<td style='text-align: Right;'>OUAT Status</td>");
                str.Append("<td style='text-align: Right;'>DEPT Status</td>");
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
                    str.Append("<td>" + dr["ACC_NO"].ToString() + "</td>");
                    str.Append("<td>" + dr["VCHBANKNAME"].ToString() + "</td>");
                    str.Append("<td>" + dr["VCHBRANCHNAME"].ToString() + "</td>");
                    str.Append("<td>" + dr["IFSC_CODE"].ToString() + "</td>");
                    str.Append("<td>" + dr["OSSC_STATUS"].ToString() + "</td>");
                    str.Append("<td>" + dr["PAY_STATUS"].ToString() + "</td>");
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
                str.Append("<td></td>");
                str.Append("<td></td>");
                str.Append("</tr>");
                str.Append("</table>");
                litReport.Text = str.ToString();
            }
        }
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
                BAL.FIN_YR = lblFinYr.Text;
                //BAL.SEASON = "R";

                BAL.XML_Value = ConvertDatatableToXML(dt1);
                DAL = new DLL_DropDown();
                DAL.InsertIncentiveOilSeedOUAT(BAL, out Val);
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
            string attachment = "attachment; filename=SeedIncentiveOilSeed" + lblFinYr.Text + ".xls";
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        BAL = new BLL_DropDown();
        BAL.UPDATED_BY = this.Session["UserID"].ToString();
        BAL.FIN_YR = lblFinYr.Text;
        BAL.SEASON = lblSeason.Text;
        DAL = new DLL_DropDown();
        DAL.OsscAprvOilSeedOUAT(BAL);
        dvOSSC.Visible = false;
        FillGrid();
    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtTrn.Text))
        {
            BAL = new BLL_DropDown();
            BAL.TRANSACTIONID = txtTrn.Text;
            BAL.UPDATED_BY = this.Session["UserID"].ToString();
            BAL.FIN_YR = lblFinYr.Text;
            BAL.SEASON = lblSeason.Text;
            DAL = new DLL_DropDown();
            DAL.OsscAprvOilSeedDeptOUAT(BAL);
            dvDEPT.Visible = false;
            FillGrid();
        }
        else
        {
            string popp = "alert('Please Enter Transaction numbers');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        litReport.Text = "";
        dvOSSC.Visible = false;
        dvDEPT.Visible = false;
        lblFinYr.Text = ddlFinYear.SelectedItem.Text;
        lblSeason.Text = ddlSeason.SelectedItem.Text;
        FillGrid();
        if (this.Session["UserID"].ToString() == "osscadmin")
        {
            BAL = new BLL_DropDown();
            BAL.FIN_YR = lblFinYr.Text;
            BAL.SEASON = lblSeason.Text;
            DAL = new DLL_DropDown();

            int Cnt = DAL.FillPendingIncentiveOilSeed(BAL);
            if (Cnt > 0)
            {
                dvOSSC.Visible = true;
            }
        }
        else if (this.Session["UserID"].ToString() == "nmoopadmin")
        {
            BAL = new BLL_DropDown();
            BAL.FIN_YR = lblFinYr.Text;
            BAL.SEASON = lblSeason.Text;
            DAL = new DLL_DropDown();

            int Cnt = DAL.FillPendingIncentiveOilSeedDept(BAL);
            if (Cnt > 0)
            {
                dvDEPT.Visible = true;
            }
        }
    }
}
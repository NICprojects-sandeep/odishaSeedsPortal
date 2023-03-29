using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DealerPacsPayment : System.Web.UI.Page
{
    DataSet ds;
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

        }
    }

    private void GenerateReport1()
    {
        double dFile = 0;
        double dDept_App = 0;
        double dTot_qtl = 0;
        double dDept_App_qtl = 0;
        double dAmount = 0;
        double dDept_App_amt = 0;
        double dNo_of_dealer = 0;
        double dDept_App_Dealer = 0;
        double dNo_of_beneficiary = 0;
        double dDept_App_farmer = 0;

        double pFile = 0;
        double pDept_App = 0;
        double pTot_qtl = 0;
        double pDept_App_qtl = 0;
        double pAmount = 0;
        double pDept_App_amt = 0;
        double pNo_of_dealer = 0;
        double pDept_App_Dealer = 0;
        double pNo_of_beneficiary = 0;
        double pDept_App_farmer = 0;

        //DEALER BEGIN
        StringBuilder str = new StringBuilder("");
        objUserBEL = new BLL_DropDown();
        objUserBEL.FIN_YR = "2021-22";
        objUserBEL.SUPPLY_TYPE = "Dealer";
        objUserBEL.scheme_code = ddlScheme.SelectedValue.ToString();
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        if (ds != null)
        {
            //if (ds.Tables[0].Rows.Count > 0)
            //{
                ds = objUserDLL.FillDealerPACSReport(objUserBEL);
                btnExport.Enabled = true;
                btnPrint.Enabled = true;

                
                str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
                str.Append("<tr><td colspan='12' style='text-align: center; font-size: 16px'><b>DEALERS (2021-22)</b></td></tr>");
                str.Append("<tr>");
                str.Append("<td style='text-align: center; font-size: 14px'><b>Sl. No</b></td>");
                str.Append("<td style='text-align: center; font-size: 14px'><b>Name of the Scheme</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Files</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Quantity in Quintal</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Amount Involved (in Rs.)</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Dealers</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Beneficiary</b></td>");
                str.Append("</tr>");
                str.Append("<tr>");
                str.Append("<td style='text-align: center'></td>");
                str.Append("<td></td>");
                str.Append("<td><b><span style='font-size: 14px'>Created (NIC)</span></b><span style='font-size: 14px'></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td></span><b><span style='font-size: 14px'>Approved (Deptt.)</span></b></td>");
                str.Append("</tr>");
                int slno = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str.Append("<tr>");
                    str.Append("<td style='font-weight:bold;'>" + slno.ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Scheme_Name"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["File"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Tot_qtl"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_qtl"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Amount"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_amt"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["No_of_dealer"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_Dealer"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["No_of_beneficiary"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_farmer"].ToString() + "</td>");
                    str.Append("</tr>");
                    slno += 1;
                }
                dFile = Convert.ToDouble(ds.Tables[0].Compute("Sum(File)", "").ToString());
                dDept_App = 0;
                dTot_qtl = Convert.ToDouble(ds.Tables[0].Compute("Sum(Tot_qtl)", "").ToString());
                dDept_App_qtl = 0;
                dAmount = Convert.ToDouble(ds.Tables[0].Compute("Sum(Amount)", "").ToString());
                dDept_App_amt = 0;
                dNo_of_dealer = Convert.ToDouble(ds.Tables[0].Compute("Sum(No_of_dealer)", "").ToString());
                dDept_App_Dealer = 0;
                dNo_of_beneficiary = Convert.ToDouble(ds.Tables[0].Compute("Sum(No_of_beneficiary)", "").ToString());
                dDept_App_farmer = 0;


                str.Append("<tr>");
                str.Append("<td style='font-weight:bold;'></td>");
                str.Append("<td style='font-weight:bold;'>TOTAL</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + dFile.ToString() + "</td>");
                str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App)", "").ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + dTot_qtl.ToString() + "</td>");
                str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_qtl)", "").ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + dAmount.ToString() + "</td>");
                str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_amt)", "").ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + dNo_of_dealer.ToString() + "</td>");
                str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_Dealer)", "").ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + dNo_of_beneficiary.ToString() + "</td>");
                str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_farmer)", "").ToString() + "</td>");        
                str.Append("</tr>");
                str.Append("</table>");
            //}
            //else
            //{
            //    str.Append("No records found");
            //}
        }
        else
        {
            str.Append("No records found");
        }
        //DEALER END
        str.Append("</br>");
        str.Append("</br>");
        //PACS BEGIN
        objUserBEL = new BLL_DropDown();
        objUserBEL.FIN_YR = "2021-22";
        objUserBEL.SUPPLY_TYPE = "PACS";
        objUserBEL.scheme_code = ddlScheme.SelectedValue.ToString();
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillDealerPACSReport(objUserBEL);


        if (ds != null)
        {
            //if (ds.Tables[0].Rows.Count > 0)
            //{
                str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
                str.Append("<tr><td colspan='12' style='text-align: center; font-size: 16px'><b>PACS (2021-22)</b></td></tr>");
                str.Append("<tr>");
                str.Append("<td style='text-align: center; font-size: 14px'><b>Sl. No</b></td>");
                str.Append("<td style='text-align: center; font-size: 14px'><b>Name of the Scheme</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Files</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Quantity in Quintal</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Amount Involved (in Rs.)</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Dealers</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Beneficiary</b></td>");
                str.Append("</tr>");
                str.Append("<tr>");
                str.Append("<td style='text-align: center'></td>");
                str.Append("<td></td>");
                str.Append("<td><b><span style='font-size: 14px'>Created (NIC)</span></b><span style='font-size: 14px'></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td></span><b><span style='font-size: 14px'>Approved (Deptt.)</span></b></td>");
                str.Append("</tr>");
                int slno1 = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str.Append("<tr>");
                    str.Append("<td style='font-weight:bold;'>" + slno1.ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Scheme_Name"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["File"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Tot_qtl"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_qtl"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Amount"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_amt"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["No_of_dealer"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_Dealer"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["No_of_beneficiary"].ToString() + "</td>");
                    str.Append("<td style='text-align: Right;'>" + dr["Dept_App_farmer"].ToString() + "</td>");
                    str.Append("</tr>");
                    slno1 += 1;
                }

                pFile = Convert.ToDouble(ds.Tables[0].Compute("Sum(File)", "").ToString());
                pDept_App = Convert.ToDouble(ds.Tables[0].Compute("Sum(Dept_App)", "").ToString());
                pTot_qtl = Convert.ToDouble(ds.Tables[0].Compute("Sum(Tot_qtl)", "").ToString());
                pDept_App_qtl = Convert.ToDouble(ds.Tables[0].Compute("Sum(Dept_App_qtl)", "").ToString());
                pAmount = Convert.ToDouble(ds.Tables[0].Compute("Sum(Amount)", "").ToString());
                pDept_App_amt = Convert.ToDouble(ds.Tables[0].Compute("Sum(Dept_App_amt)", "").ToString());
                pNo_of_dealer = Convert.ToDouble(ds.Tables[0].Compute("Sum(No_of_dealer)", "").ToString());
                pDept_App_Dealer = Convert.ToDouble(ds.Tables[0].Compute("Sum(Dept_App_Dealer)", "").ToString());
                pNo_of_beneficiary = Convert.ToDouble(ds.Tables[0].Compute("Sum(No_of_beneficiary)", "").ToString());
                pDept_App_farmer = Convert.ToDouble(ds.Tables[0].Compute("Sum(Dept_App_farmer)", "").ToString());

                str.Append("<tr>");
                str.Append("<td style='font-weight:bold;'></td>");
                str.Append("<td style='font-weight:bold;'>TOTAL</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pFile.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pDept_App.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pTot_qtl.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pDept_App_qtl.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pAmount.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pDept_App_amt.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pNo_of_dealer.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pDept_App_Dealer.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pNo_of_beneficiary.ToString() + "</td>");
                str.Append("<td style='text-align: Right; font-weight:bold;'>" + pDept_App_farmer.ToString() + "</td>");
                str.Append("</tr>");
                str.Append("</table>");
                //PACS END
                str.Append("</br>");
                str.Append("</br>");

                str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
                str.Append("<tr><td colspan='10' style='text-align: center; font-size: 16px'><b>ALL (2021-22)</b></td></tr>");
                str.Append("<tr>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Files</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Quantity in Quintal</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Amount Involved (in Rs.)</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Dealers</b></td>");
                str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Beneficiary</b></td>");
                str.Append("</tr>");
                str.Append("<tr>");
                str.Append("<td><b><span style='font-size: 14px'>Created (NIC)</span></b><span style='font-size: 14px'></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td><b>Approved (Deptt.)</b></td>");
                str.Append("<td><b>Created (NIC)</b></td>");
                str.Append("<td></span><b><span style='font-size: 14px'>Approved (Deptt.)</span></b></td>");
                str.Append("</tr>");
                str.Append("<td style='font-weight:bold;'>" + (dFile + pFile).ToString() + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dDept_App + pDept_App).ToString() + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dTot_qtl + pTot_qtl) + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dDept_App_qtl + pDept_App_qtl) + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dAmount + pAmount) + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dDept_App_amt + pDept_App_amt) + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dNo_of_dealer + pNo_of_dealer) + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dDept_App_Dealer + pDept_App_Dealer) + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dNo_of_beneficiary + pNo_of_beneficiary) + "</td>");
                str.Append("<td style='font-weight:bold;'>" + (dDept_App_farmer + pDept_App_farmer) + "</td>");

                str.Append("</tr>");
                str.Append("</table>");
            //}
            //else
            //{
            //    str.Append("No records found");
            //}
        }
        else
        {
            str.Append("No records found");
        }
        litReport1.Text = str.ToString();
    }
    //private void GenerateReport2(DataSet ds)
    //{
    //    btnExport.Enabled = true;
    //    btnPrint.Enabled = true;

    //    StringBuilder str = new StringBuilder("");
    //    str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
    //    str.Append("<tr><td colspan='12' style='text-align: center; font-size: 16px'><b>PACS (2021-22)</b></td></tr>");
    //    str.Append("<tr>");
    //    str.Append("<td style='text-align: center; font-size: 14px'><b>Sl. No</b></td>");
    //    str.Append("<td style='text-align: center; font-size: 14px'><b>Name of the Scheme</b></td>");
    //    str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Files</b></td>");
    //    str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Quantity in Quintal</b></td>");
    //    str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Total Amount Involved (in Rs.)</b></td>");
    //    str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Dealers</b></td>");
    //    str.Append("<td colspan='2' style='text-align: center; font-size: 14px'><b>Number of Beneficiary</b></td>");
    //    str.Append("</tr>");
    //    str.Append("<tr>");
    //    str.Append("<td style='text-align: center'></td>");
    //    str.Append("<td></td>");
    //    str.Append("<td><b><span style='font-size: 14px'>Created (NIC)</span></b><span style='font-size: 14px'></td>");
    //    str.Append("<td><b>Approved (Deptt.)</b></td>");
    //    str.Append("<td><b>Created (NIC)</b></td>");
    //    str.Append("<td><b>Approved (Deptt.)</b></td>");
    //    str.Append("<td><b>Created (NIC)</b></td>");
    //    str.Append("<td><b>Approved (Deptt.)</b></td>");
    //    str.Append("<td><b>Created (NIC)</b></td>");
    //    str.Append("<td><b>Approved (Deptt.)</b></td>");
    //    str.Append("<td><b>Created (NIC)</b></td>");
    //    str.Append("<td></span><b><span style='font-size: 14px'>Approved (Deptt.)</span></b></td>");
    //    str.Append("</tr>");
    //    int slno = 1;
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        str.Append("<tr>");
    //        str.Append("<td style='font-weight:bold;'>" + slno.ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Scheme_Name"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["File"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Dept_App"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Tot_qtl"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Dept_App_qtl"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Amount"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Dept_App_amt"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["No_of_dealer"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Dept_App_Dealer"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["No_of_beneficiary"].ToString() + "</td>");
    //        str.Append("<td style='text-align: Right;'>" + dr["Dept_App_farmer"].ToString() + "</td>");
    //        str.Append("</tr>");
    //        slno += 1;
    //    }
    //    str.Append("<tr>");
    //    str.Append("<td style='font-weight:bold;'>TOTAL</td>");
    //    str.Append("<td style='font-weight:bold;'></td>");
    //    str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(File)", "").ToString() + "</td>");
    //    str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App)", "").ToString() + "</td>");
    //    str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Tot_qtl)", "").ToString() + "</td>");
    //    str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_qtl)", "").ToString() + "</td>");
    //    str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Amount)", "").ToString() + "</td>");
    //    str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_amt)", "").ToString() + "</td>");
    //    str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(No_of_dealer)", "").ToString() + "</td>");
    //    str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_Dealer)", "").ToString() + "</td>");
    //    str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(No_of_beneficiary)", "").ToString() + "</td>");
    //    str.Append("<td style='font-weight:bold;'></td>");//str.Append("<td style='text-align: Right; font-weight:bold;'>" + ds.Tables[0].Compute("Sum(Dept_App_farmer)", "").ToString() + "</td>");        
    //    str.Append("</tr>");
    //    str.Append("</table>");
    //    litReport2.Text = str.ToString();
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
               
        GenerateReport1();

        //objUserBEL = new BLL_DropDown();
        //objUserBEL.FIN_YR = "2021-22";
        //objUserBEL.SUPPLY_TYPE = "PACS";
        //objUserDLL = new DLL_DropDown();
        //ds = new DataSet();
        //ds = objUserDLL.FillDealerPACSReport(objUserBEL);
        //GenerateReport2(ds);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=DealerPACS.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            litReport1.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex) { }
    }
}
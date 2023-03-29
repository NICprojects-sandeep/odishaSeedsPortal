using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class Reports_Rpt_DealerWiseSaleDtls : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null || this.Session["AuthToken"] == null || base.Request.Cookies["AuthToken"] == null)
        {
            this.utl.SessionReset();
        }
        else
        {
            if (!this.Session["AuthToken"].ToString().Equals(base.Request.Cookies["AuthToken"].Value))
            {
                this.utl.SessionReset();
                return;
            }
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                //FillFinYr();
                //btnExport.Enabled = false;
                DataSet ds = new DataSet();

                string sql = "STOCK_RPT_DealerWiseSaleDtls";
                string[] param = { "@FIN_YR", "@Session" };
                object[] value = { "2019-20", 'K' };
                ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
                GenerateReport(ds);
            }
        }
    }

    private void GenerateReport(DataSet ds)
    {
        //btnExport.Enabled = true;
        DataTable dtData = ds.Tables[0];
        DataTable dtCrop = ds.Tables[1];

        //Distinct Variety Code and Name
        //DataView view = new DataView(dtData);
        //DataTable distinctValues = view.ToTable(true, "Crop_Verid", "Variety_Name");
        DataView view = new DataView(dtData);
        DataTable distinctValues = view.ToTable(true, "dist_name");

        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'><tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td>SL No</td>");
        str.Append("<td>Dist Name</td>");
        str.Append("<td>Firm Name</td>");
        str.Append("<td>Licence No</td>");
        str.Append("<td>Mobile No</td>");
        str.Append("<td>Acc Holder Name</td>");
        str.Append("<td>Account No</td>");
        str.Append("<td>IFSC</td>");
        str.Append("<td>Bank</td>");
        foreach (DataRow dr1 in dtCrop.Rows)
        {
            str.Append("<td>" + dr1["Crop_Name"].ToString() + "</td>");
        }
        str.Append("</tr>");
        string nm = string.Empty;
        string nm1 = string.Empty;
        foreach (DataRow dr1 in distinctValues.Rows)
        {
            
            DataRow[] result = dtData.Select("dist_name= '" + dr1["dist_name"].ToString() + "'");
            foreach (DataRow row in result)
            {
                //slno = slno + 1;
                //if (slno == 1)
                //{
                    str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");


                    if (nm != row["APP_FIRMNAME"].ToString())
                    {

                        nm = row["APP_FIRMNAME"].ToString();
                        str.Append(" <td>" + row["sl"].ToString() + "</td>");
                        if (nm1 != row["dist_name"].ToString())
                        {                            
                            nm1 = row["dist_name"].ToString();
                            str.Append(" <td>" + row["dist_name"].ToString() + "</td>");
                        }
                        else
                        {   
                            str.Append(" <td></td>");
                        }
                        str.Append(" <td>" + row["APP_FIRMNAME"].ToString() + "</td>");
                        str.Append(" <td>" + row["UPDATED_BY"].ToString() + "</td>");
                        str.Append(" <td>" + row["APPMOB_NO"].ToString() + "</td>");
                        str.Append(" <td>" + row["ACC_HOLDERNAME"].ToString() + "</td>");
                        str.Append(" <td>" + row["ACC_NO"].ToString() + "</td>");
                        str.Append(" <td>" + row["IFSC_CODE"].ToString() + "</td>");
                        str.Append(" <td>" + row["vchBankName"].ToString() + "</td>");
                        foreach (DataRow dr11 in dtCrop.Rows)
                        {
                            DataRow[] qtl = dtData.Select("CROP_ID= '" + dr11["CROP_ID"].ToString() + "' and UPDATED_BY = '" + row["UPDATED_BY"].ToString() + "'");
                            if (qtl.Count() > 0)
                            {
                                str.Append(" <td>" + qtl[0]["TOT_QTL"].ToString() + "</td>");
                            }
                            else
                            {
                                str.Append(" <td>0.00</td>");
                            }
                        }

                    }
                    
                    
                   
                    str.Append("</tr>");
                //}
                //else
                //{
                  
                //    if (nm != row["APP_FIRMNAME"].ToString())
                //    {
                //        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");


                //        str.Append(" <td>" + row["sl"].ToString() + "</td>");
                //        str.Append(" <td> </td>");
                //        nm = row["APP_FIRMNAME"].ToString();
                //        str.Append(" <td>" + row["APP_FIRMNAME"].ToString() + "</td>");
                //        str.Append(" <td>" + row["UPDATED_BY"].ToString() + "</td>");
                //        str.Append(" <td>" + row["APPMOB_NO"].ToString() + "</td>");
                //        str.Append(" <td>" + row["ACC_HOLDERNAME"].ToString() + "</td>");
                //        str.Append(" <td>" + row["ACC_NO"].ToString() + "</td>");
                //        str.Append(" <td>" + row["IFSC_CODE"].ToString() + "</td>");
                //        str.Append("<td></td>");
                //        foreach (DataRow dr11 in dtCrop.Rows)
                //        {
                //            DataRow[] qtl = dtData.Select("CROP_ID= '" + dr11["CROP_ID"].ToString() + "' and UPDATED_BY = '" + row["UPDATED_BY"].ToString() + "'");
                //            if (qtl.Count() > 0)
                //            {
                //                str.Append(" <td>" + qtl[0]["TOT_QTL"].ToString() + "</td>");
                //            }
                //            else
                //            {
                //                str.Append(" <td>0.00</td>");
                //            }
                //        }
                //        str.Append("</tr>");
                //    }
                   
                   
                //}
            }
        }
        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //this.FirstLogin = this.Session["FirstLogin"].ToString();

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
}
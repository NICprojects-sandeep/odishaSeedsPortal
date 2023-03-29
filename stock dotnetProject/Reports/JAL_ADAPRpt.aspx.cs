using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_JAL_ADAPRpt : System.Web.UI.Page
{
    BLL_ACC_LYR obj = new BLL_ACC_LYR();
    BLL_Obj bll_obj = new BLL_Obj();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='3' cellspacing='0' class='tabletxt'>");

        str.Append("<tr style='background-color:#DDEECC; font-weight:bold;'><td rowspan='3' width='10%' >DIST NAME</td><td rowspan='3' width='10%' >BLOCK NAME</td><td colspan='9' style='text-align:center;'>Bore Well</td><td colspan='9' style='text-align:center;'>Shallow Tube Well</td>");

        str.Append("</tr>");

        str.Append("<tr style='background-color: #DDEECC; font-weight: bold;'><td colspan='3' style='text-align: center;'>ADMIN TARGET</td><td colspan='3' style='text-align: center;'>ISSUED TARGET</td><td colspan='3' style='text-align: center;'>BALANCE</td><td colspan='3' style='text-align: center;'>ADMIN TARGET</td><td colspan='3' style='text-align: center;'>ISSUED TARGET</td><td colspan='3' style='text-align: center;'>BALANCE</td>");

        str.Append("</tr>");

        str.Append("<tr style='background-color:#DDEECC; font-weight:bold;'>");
        str.Append("<td style='text-align: center;'>GEN</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");

        str.Append("<td style='text-align: center;'>GEN</td>");
        str.Append("<td style='text-align: center; '>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");

        str.Append("<td style='text-align: center;'>GEN</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");

        str.Append("<td style='text-align: center;'>GEN</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");

        str.Append("<td style='text-align: center;'>GEN</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");

        str.Append("<td style='text-align: center;'>GEN</td>");
        str.Append("<td style='text-align: center;'>SC</td>");
        str.Append("<td style='text-align: center;'>ST</td>");

        str.Append("</tr>");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["Mod_dist_name"].ToString() + "</td>");
            str.Append("<td>" + dr["block_name"].ToString() + "</td>");
            DataRow[] Data = ds.Tables[0].Select("block_code= " + dr["block_code"].ToString() + "");
            foreach (DataRow value in Data)
            {
                str.Append("<td style='text-align: center;color:#32CD32;font-weight:bold;'>" + value["TBWLGec"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#32CD32;font-weight:bold;'>" + value["TBWLSc"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#32CD32;font-weight:bold;'>" + value["TBWLSt"].ToString() + "</td>");

                str.Append("<td style='text-align: center;color:#A52A2A;font-weight:bold;'>" + value["BWLGec"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#A52A2A;font-weight:bold;'>" + value["BWLSc"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#A52A2A;font-weight:bold;'>" + value["BWLSt"].ToString() + "</td>");

                str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + value["BBwlGec"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + value["BBwlSc"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + value["BBwlSt"].ToString() + "</td>");


                str.Append("<td style='text-align: center;color:#32CD32;font-weight:bold'>" + value["TSTWGec"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#32CD32;font-weight:bold'>" + value["TSTWSc"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#32CD32;font-weight:bold'>" + value["TSTWSt"].ToString() + "</td>");


                str.Append("<td style='text-align: center;color:#A52A2A;font-weight:bold;'>" + value["STWGec"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#A52A2A;font-weight:bold;'>" + value["STWSc"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#A52A2A;font-weight:bold;'>" + value["STWSt"].ToString() + "</td>");

                str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + value["BStwGec"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + value["BStwSc"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + value["BStwSt"].ToString() + "</td>");
            }

            str.Append("</tr>");
        }
        str.Append("</tr>");

        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td></td>");
        str.Append("<td>Grand Total</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TBWLGec)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TBWLSc)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TBWLSt)", "").ToString() + "</td>");

        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BWLGec)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BWLSc)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BWLSt)", "").ToString() + "</td>");

        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BBwlGec)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BBwlSc)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BBwlSt)", "").ToString() + "</td>");

        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TSTWGec)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TSTWSc)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(TSTWSt)", "").ToString() + "</td>");

        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(STWGec)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(STWSc)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(STWSt)", "").ToString() + "</td>");

        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BStwGec)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BStwSc)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(BStwSt)", "").ToString() + "</td>");


        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    //protected void Ddldist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bll_obj.Query = " exec JAL_ADAP_DistTarget_View @varfyr";
    //        bll_obj.ParamList = new string[] { "@varfyr" };
    //        bll_obj.ParamobjList = new object[] { ddlfyr.SelectedValue };
    //        DataSet ds = obj.JAL_param_passing_fetch(bll_obj);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            GenerateReport(ds);
    //        }
    //        else
    //        {
    //            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Record Found  !</div>";
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void ddlfyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //bll_obj.Query = " exec  @varfyr";
            //bll_obj.ParamList = new string[] { "@varfyr" };
            //bll_obj.ParamobjList = new object[] { ddlfyr.SelectedValue };

            bll_obj = new BLL_Obj();
            DataSet ds = new DataSet();
            bll_obj.Query = "JAL_ADAPT_DistTarget_View";
            bll_obj.ParamList = new string[] { "@varfyr" };
            bll_obj.ParamobjList = new object[] { ddlfyr.SelectedValue };

            ds = obj.param_passing_fetch_storeprocedure(bll_obj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GenerateReport(ds);
            }
            else
            {
                litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Record Found  !</div>";
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnEperttoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=JALRPT.xls"));
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWriterObj = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWriterObj = new HtmlTextWriter(stringWriterObj);
            litReport.RenderControl(htmlWriterObj);
            Response.Write(stringWriterObj.ToString());
            Response.End();
        }
        catch (Exception ex) { }

    }
}
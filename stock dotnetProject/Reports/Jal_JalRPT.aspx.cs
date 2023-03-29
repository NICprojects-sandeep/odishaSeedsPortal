using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Jalanidhi_Jal_JalRPT : System.Web.UI.Page
{
    BLL_ACC_LYR obj = new BLL_ACC_LYR();
    BLL_Obj bll_obj = new BLL_Obj();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divButton.Visible = false;
            divPrint.Visible = false;

        }
    }
    private void GenerateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:80%; border-collapse:collapse;  font-size: 12px;margin-left: 96px;' border='1' cellpadding='3' cellspacing='0' ><tr style='font weight:bold;'><td style='background-color:#DDEECC;'>DISTRICT</td> <td style='background-color:#DDEECC;'>Block</td> <td colspan='1' style='text-align: center;background-color:#DDEECC; width='10px'> NO. Of APPLICATION</td> <td colspan='1' style='text-align: center;background-color:#DDEECC; width='10px'> NO OF PAYMENT FILE GENERATED</td><td colspan='1'style='text-align: center;background-color:#DDEECC; width='10px'>TOTAL SUBCIDY AMOUNT</td>");

        str.Append("</tr>");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            str.Append("<tr>");
            str.Append("<td style='background-color:#eee; text-align: left;'>" + dr["dist_name"].ToString() + "</td>");
            DataRow[] Data = ds.Tables[0].Select("dist_code= " + dr["dist_code"].ToString() + "");
            str.Append("<td style='background-color:#eee; text-align: left;'>" + dr["block_name"].ToString() + "</td>");
            DataRow[] Data1 = ds.Tables[0].Select("block_code= " + dr["block_code"].ToString() + "");
            foreach (DataRow value in Data1)
            {
                // str.Append("<td style='text-align: center;color:#660000;font-weight:bold'>" + value["block_name"].ToString() + "</td>");

                str.Append("<td style='text-align: center;color:#660000;font-weight:bold'>" + value["APPLICATION_FROM_SUBMIT"].ToString() + "</td>");
                str.Append("<td style='text-align: center;color:#660000;font-weight:bold'>" + value["NoOfPymnt"].ToString() + "</td>");

                str.Append("<td style='text-align: center;color:#660000;font-weight:bold'>" + value["subcidy"].ToString() + "</td>");

            }

            str.Append("</tr>");
        }
        str.Append("</tr>");
        str.Append("<tr style='font-weight:bold;'>");
        str.Append("<td style='background-color:#DDEECC; font-weight:bold;text-align: right;'></td>");
        str.Append("<td style='background-color:#DDEECC; font-weight:bold;text-align: right;'>Grand Total :</td>");
        str.Append("<td style='text-align: center;background-color:#eee;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(APPLICATION_FROM_SUBMIT)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;background-color:#eee;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(NoOfPymnt)", "").ToString() + "</td>");
        str.Append("<td style='text-align: center;background-color:#eee;color:#FF6347;font-weight:bold;'>" + ds.Tables[0].Compute("Sum(subcidy)", "").ToString() + "</td>");
    

        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void Ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlfyr.SelectedIndex == 0)
            {
                string pop = "alert('Please select financial year !!!.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                divButton.Visible = false;
                divPrint.Visible = false;
            }
            if (ddlPlpi.SelectedIndex == 0)
            {
                divButton.Visible = false;
                divPrint.Visible = false;
            }
            else
            {
                divButton.Visible = true;
                divPrint.Visible = true;

                bll_obj = new BLL_Obj();
                DataSet ds = new DataSet();
                bll_obj.Query = "JAL_SPREPORTForeAdapInPayment";
                bll_obj.ParamList = new string[] { "@F_YEAR", "@PLIP" };
                bll_obj.ParamobjList = new object[] { ddlfyr.SelectedValue, ddlPlpi.SelectedValue };

                ds = obj.param_passing_fetch_storeprocedure(bll_obj);

                //bll_obj.Query = "exec JAL_SPREPORTForeAdapInPayment @F_YEAR,@PLIP";
                //bll_obj.ParamList = new string[] { "@F_YEAR", "@PLIP" };
                //bll_obj.ParamobjList = new object[] { ddlfyr.SelectedValue, ddlPlpi.SelectedValue, };
                //DataSet ds = obj.JAL_param_passing_fetch(bll_obj);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GenerateReport(ds);
                }
                else
                {
                    litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px;'  class='tabletxt'  align='center'>No Records Found  !</div>";
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            string attachment = "attachment; filename=JALRPT.xls";
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
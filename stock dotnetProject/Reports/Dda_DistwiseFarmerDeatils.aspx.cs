using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Reports_Dda_DistwiseFarmerDeatils : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (Session["DistName"] != null)
            {
                litDistName.Text = Session["DistName"].ToString();
                string sql = "SELECT NDist_Code FROM [stock].[dbo].[Stock_District] WHERE Dist_Code = @PFMS_Dist_Code";
                string[] param = { "@PFMS_Dist_Code" };
                object[] value = { string.Format("{0:00}", int.Parse(Session["DistCode"].ToString())) };
                Session["DistCode1"] = dbsApp.param_passing_fetchString(sql, param, value);
                fillblock();
            }
            else
            {

            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
    private void fillblock()
    {
        drpBlock.Items.Clear();
        DataSet ds = new DataSet();
        
        string sql = "select  vch_BlockName,int_BlockID  from [FARMERDB].[dbo].[PDS_BLOCKMASTER]  where left(int_BlockID,2)=(SELECT int_DistrictID FROM[FARMERDB].[dbo].[PDS_DISTRICTMASTER] WHERE int_DistrictID=@PFMS_Dist_Code) order by vch_BlockName";
        string[] param = { "@PFMS_Dist_Code" };
        object[] value = {string.Format("{0:00}", int.Parse(Session["DistCode1"].ToString())) };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        drpBlock.DataSource = ds.Tables[0];
        drpBlock.DataTextField = "vch_BlockName";
        drpBlock.DataValueField = "int_BlockID";
        drpBlock.DataBind();
        this.drpBlock.Items.Insert(0, "---SELECT---");
        this.drpGp.Items.Insert(0, "---ALL---");

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }
    private void bindgrid()
    {
        DataSet ds = new DataSet();
        string sql = "DDA_FarmerList";
        string[] param = { "@VCHDISTID", "@VCHBLOCKID", "@VCHGPID" };
        object[] value = { Session["DistCode1"].ToString(),drpBlock.SelectedValue.Trim(),drpGp.SelectedValue.Trim() == "---ALL---" ? "0" : drpGp.SelectedValue.Trim()};
       
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grd_dda_FarmerList.Attributes.Add("style", "display:;");
            litReport.Text = string.Empty;

            grd_dda_FarmerList.DataSource = ds.Tables[0];
            grd_dda_FarmerList.DataBind();
            btnview.Visible = true;
        }
        else
        {
            grd_dda_FarmerList.Attributes.Add("style", "display:none;");
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }
    protected void drpBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBlock.SelectedIndex > 0)
        {
            drpGp.Items.Clear();     
            DataSet ds = new DataSet();
            string sql = "select int_GPID ,vch_GPName FROM [FARMERDB].[dbo].[PDS_GPMASTER]  where int_BlockID=@Block_Code order by vch_GPName";
            string[] param = { "@Block_Code" };
            object[] value = { drpBlock.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            drpGp.DataSource = ds.Tables[0];
            drpGp.DataTextField = "vch_GPName";
            drpGp.DataValueField = "int_GPID";
            drpGp.DataBind();
            this.drpGp.Items.Insert(0, "---ALL---");
        }
        else
        {
            drpGp.Items.Clear();
            this.drpGp.Items.Insert(0, "---ALL---");
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        string Ids = string.Empty;
        foreach (GridViewRow gv in grd_dda_FarmerList.Rows)
        {
            if (gv.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (gv.Cells[0].FindControl("CheckBox") as CheckBox);
                Label lblFid = (gv.Cells[1].FindControl("lbl_NICFARMERID") as Label);
                if (chkRow.Checked == true)
                {
                    Ids += "'" + lblFid.Text.ToString() + "',";
                }
            }
        }
        DataSet ds = new DataSet();
        string sql = "select p.NICFARMERID,p.VCHFARMERNAME,p.VCHFATHERNAME,q.vch_VillageName,r.Category_Value,s.vchBankName,u.vchBranchName,u.NEW_IFSC_CODE,VCHACCHOLDERNM,VCHACCOUNTNO from M_FARMER_REGISTRATION p inner join PDS_VILLAGEMASTER q on p.VCHVILLAGEID=q.int_VillageID inner join Tbl_Category r on p.INTCATEGOGY=r.Category_ID inner join M_PDS_FARMERBANK s on p.INTBANKID=s.intId inner join M_PDS_BANKBRANCH u on p.INTBRANCHID=u.intBranchId inner join Tbl_FarmerApprove as t  on  P.VCHFARMERCODE=t.Farmer_Code  WHERE Status in ('ACCP','CCB','UIID','AAO1','AAO','AAOC') AND p.INTDELETEDFLAG=0 AND  p.INTREJECTED=0  AND NICFARMERID IN(" + Ids + "'0'" + ")  order by VCHFARMERNAME,vch_VillageName";
        string[] param = { };
        object[] value = { };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            litReport.Text = string.Empty;
            GenerateHtml(ds.Tables[0]);
            mp1.Show();
        }
        else
        {
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }

    private void GenerateHtml(DataTable dtData)
    {
        StringBuilder str = new StringBuilder("");
        str.Append("<table style='width:100%; border-collapse:collapse; font-size: 14px; ' border='1' cellpadding='5' cellspacing='0' class='tabletxt'>");
        
        str.Append("<tr style='background-color:#CCCCCC; font-weight:bold;'>");
        str.Append("<td style='text-align: Right;'>Farmer ID</td>");
        str.Append("<td style='text-align: Right;'>Farmer Name</td>");
        str.Append("<td style='text-align: Right;'>Father Name</td>");
        str.Append("<td style='text-align: Right;'>Village Name</td>");
        str.Append("<td style='text-align: Right;'>Category</td>");

        str.Append("<td style='text-align: Right;'>Bank Name</td>");
        str.Append("<td style='text-align: Right;'>Branch Name</td>");
        str.Append("<td style='text-align: Right;'>IFSC CODE</td>");
        str.Append("<td style='text-align: Right;'>ACCOUNT HOLDER NAME</td>");
        str.Append("<td style='text-align: Right;'>ACCOUNT NO.</td>");
        str.Append("</tr>");
        foreach (DataRow dr in dtData.Rows)
        {
            str.Append("<tr>");
            str.Append("<td>" + dr["NICFARMERID"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHFARMERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHFATHERNAME"].ToString() + "</td>");
            str.Append("<td>" + dr["vch_VillageName"].ToString() + "</td>");
            str.Append("<td>" + dr["Category_Value"].ToString() + "</td>");
            str.Append("<td>" + dr["vchBankName"].ToString() + "</td>");
            str.Append("<td>" + dr["vchBranchName"].ToString() + "</td>");
            str.Append("<td>" + dr["NEW_IFSC_CODE"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHACCHOLDERNM"].ToString() + "</td>");
            str.Append("<td>" + dr["VCHACCOUNTNO"].ToString() + "</td>");
            str.Append("</tr>");
        }

        str.Append("</table>");
        litReport.Text = str.ToString();
    }
    protected void btnexportexcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment;filename=DistwiseFarmerDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter htwWriter = new HtmlTextWriter(sWriter);
            litReport.RenderControl(htwWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }
        catch (Exception ex) {
        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)

    {

        if (e.Row.RowType == DataControlRowType.DataRow)

        {

            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

        }

    }
}



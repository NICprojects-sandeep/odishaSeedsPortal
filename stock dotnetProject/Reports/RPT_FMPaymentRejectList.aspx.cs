using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_FMPaymentRejectList : System.Web.UI.Page
{
    FMBLLR bll = new FMBLLR();
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
        if (this.Session["UserType"].ToString() == "IRRI")
        {
            this.Page.MasterPageFile = "../MasterPages/ED.master";
            return;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbl_content.Visible = false;
            ddl_reson.Items.Insert(0, "Select");
        }
    }
    private void BindDrop(string sysid)
    {
        try
        {
            ddl_reson.DataSource = bll.GetErrroName(sysid);
            ddl_reson.DataTextField = "Rejection_Narration";
            ddl_reson.DataValueField = "Rejection_Narration";
            ddl_reson.DataBind();
            ddl_reson.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddl_reson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tbl_content.Visible = true;
            string narration = ddl_reson.Text;
            DataTable dt = bll.GetDetails(narration);
            if (dt.Rows.Count > 1)
            {
                lbl_msg.Text = "";
                gv_sub.DataSource = dt;
                gv_sub.DataBind();
            }
            else
            {
                gv_sub.DataSource = null;
                gv_sub.DataBind();
                lbl_msg.Text = "No Record Found.";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddl_sysid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gv_sub.DataSource = null;
            gv_sub.DataBind();
            BindDrop(ddl_sysid.SelectedValue);
        }
        catch (Exception ex)
        {

        }
    }
}
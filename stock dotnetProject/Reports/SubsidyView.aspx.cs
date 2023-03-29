using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_SubsidyView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        SubsidyReportBLL bll = new SubsidyReportBLL();
        DataTable dt = null;
        if (Session["uniqueid"] != null)
        {
            if (Session["lid"].ToString() == "lbl_cnt")
            {
                dt = bll.GetDataViewR(Session["uniqueid"].ToString());
                if (dt != null)
                {
                    gv_sub.DataSource = dt;
                    gv_sub.DataBind();
                    gv_sub.Columns[10].Visible = false;
                    gv_sub.Columns[11].Visible = false;
                }
                else
                {
                    string msg = "<script>alert('Response not found from PFMS');</script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Control), "alertmsg", msg, false);
                }
            }
            if (Session["lid"].ToString() == "lbl_authno")
            {
                dt = bll.GetDataView(Session["uniqueid"].ToString());
                if (dt != null)
                {
                    gv_sub.DataSource = dt;
                    gv_sub.DataBind();
                    gv_sub.Columns[11].Visible = false;
                }
                else
                {
                    string msg = "<script>alert('Response not found from PFMS');</script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Control), "alertmsg", msg, false);
                }
            }
            if (Session["lid"].ToString() == "lbl_authnor")
            {
                dt = bll.GetDataViewRJ(Session["uniqueid"].ToString());
                int total = Convert.ToInt32(Session["lidcount"].ToString());
                int count = dt.Rows.Count;
                int res = total - count;
                if (res == 0)
                {
                    lbl_count.Visible = false;
                    if (dt != null)
                    {
                        gv_sub.DataSource = dt;
                        gv_sub.DataBind();
                    }
                    else
                    {
                        string msg = "<script>alert('Response not found from PFMS');</script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Control), "alertmsg", msg, false);
                    }
                }
                else
                {
                    lbl_count.Visible = true;
                    if (dt != null)
                    {
                        gv_sub.DataSource = dt;
                        gv_sub.DataBind();
                    }
                    else
                    {
                        string msg = "<script>alert('Response not found from PFMS');</script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Control), "alertmsg", msg, false);
                    }
                    lbl_count.Text = "<font color='red'><b>" + res + "</b> BENEFICIARIES ARE PAID IN ANOTHER FILE.</font>";
                }

            }
        }
        else
        {
            gv_sub.DataSource = null;
            gv_sub.DataBind();
        }

    }

    protected void gv_sub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_sub.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
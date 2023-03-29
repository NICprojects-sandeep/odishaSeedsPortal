using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_SubsidyReport : System.Web.UI.Page
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
        gv_sub.DataSource = bll.GetDataMaster();
        gv_sub.DataBind();
    }

    protected void gv_sub_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gv_sub.PageIndex = e.ne
    }

    protected void gv_sub_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void gv_sub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_sub.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void grid_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        Label lnk = (Label)row.FindControl("lbl_nicid");
        LinkButton lnkl = (LinkButton)row.FindControl("lbl_cnt");
        Session["uniqueid"] = lnk.Text;
        Session["lid"] = lnkl.ID;
        //string pop = "window.open('Stockview.aspx','Report','width=1020,height=900,toolbar=1,resizable=1,location=0, status=0, menubar=0, scrollbars=1 ');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        Response.Redirect("SubsidyView.aspx");
    }
    protected void grid_ClickNo(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        Label lnk = (Label)row.FindControl("lbl_nicid");
        LinkButton lnkl = (LinkButton)row.FindControl("lbl_authno");
        Session["uniqueid"] = lnk.Text;
        Session["lid"] = lnkl.ID;
        Response.Redirect("SubsidyView.aspx");
    }
    protected void grid_ClickNoR(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        Label lnk = (Label)row.FindControl("lbl_nicid");
        LinkButton lnkl = (LinkButton)row.FindControl("lbl_authnor");
        Session["uniqueid"] = lnk.Text;
        Session["lidcount"] = lnkl.Text;
        Session["lid"] = lnkl.ID;
        Response.Redirect("SubsidyView.aspx");
    }
}
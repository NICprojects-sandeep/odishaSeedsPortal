using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Reports_FarmersApprovedByDept : System.Web.UI.Page
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
        ApprovedDeptBLL bll = new ApprovedDeptBLL();
        gv_sub.DataSource = bll.GetDataDeptApp();
        gv_sub.DataBind();
    }
    protected void gv_sub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_sub.PageIndex = e.NewPageIndex;
        BindGrid();
    }

}
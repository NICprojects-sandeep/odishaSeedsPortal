using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DealerList : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();
    private UtilityLibrary utl = new UtilityLibrary();
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
            if (this.Session["UserType"].ToString() != "DIST")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                BindDealer();
            }
        }
    }

    private void BindDealer()
    {
        DataTable dt = new DataTable();

        string sql = "select upper(APP_FIRMNAME)  as APP_FIRMNAME,LIC_NO,appmob_no, case when APP_TYPE='Secretary PACS' then 'PACS' ELSE 'Dealer' end as type  FROM [dafpSeed].[dbo].[SEED_LIC_DIST] where DIST_CODE=@DIST_CODE AND APP_STATUS='A' AND APR_UPTO>GETDATE() AND IS_ACTIVE=1 AND LIC_ACTIVE=1 ORDER BY APP_FIRMNAME";
        string[] param = { "@DIST_CODE" };
        object[] value = { Session["DistCode"].ToString() };
        dt = dbsAppStock.param_passing_fetchDataTable(sql, param, value);
        if (dt.Rows.Count > 0)
        {
            BindGrid(dt);
        }
        else
        {

        }
    }

    private void BindGrid(DataTable dt)
    {
        grd_FarmerList.DataSource = dt;
        grd_FarmerList.DataBind();
    }

    protected void lnkVCard_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        HiddenField hfLIC_NO = (HiddenField)row.FindControl("hfLIC_NO");

        Session["LIC_NO"] = hfLIC_NO.Value.Trim();
        Response.Redirect("DealerSale.aspx");
    }
}
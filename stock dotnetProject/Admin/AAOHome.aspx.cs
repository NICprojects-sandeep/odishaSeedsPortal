using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AAOHome : System.Web.UI.Page
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
            if (this.Session["UserType"].ToString() != "AAOO")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                this.hfMCode.Value = Session["UserID"].ToString();
                this.lblName.Text = Session["FullName"].ToString();

                DataSet ds = new DataSet();
                string sql = "select Nblock_code,LEFT(Nblock_code,2) as int_DistrictID from FARMERDB.[dbo].block b inner join [stock].[dbo].JALANIDHI_DAO_AAO j on b.nic_block_code collate SQL_Latin1_General_CP1_CI_AS=j.blk_code where aao_code=@int_BlockID";
                string[] param = { "@int_BlockID" };
                object[] value = { Session["UserID"].ToString().Substring(4, 6).Trim() };
                ds = dbsAppStock.param_passing_fetch(sql, param, value);
                Session["Block"] = ds.Tables[0].Rows[0]["Nblock_code"].ToString();
                Session["Dist"] = ds.Tables[0].Rows[0]["int_DistrictID"].ToString();
                return;
            }
        }
    }
}
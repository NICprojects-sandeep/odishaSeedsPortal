using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class DBT_DBTFarmerRpt : System.Web.UI.Page
{
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
            if (!(this.Session["UserType"].ToString() == "AAOO") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                string BlockCode1 = "select BLK_CODE from [Stock].dbo.JALANIDHI_DAO_AAO where AAO_CODE=@AAOCode AND PP='1'";
                string[] paramBlk1 = { "@AAOCode" };
                object[] valueBlk1 = { Session["UserID"].ToString().Substring(4, 6) };
                DataSet dsBlk1 = dbsAppStock.param_passing_fetch(BlockCode1, paramBlk1, valueBlk1);
                if (dsBlk1.Tables[0].Rows.Count > 0)
                {
                    Session["BlockCode"] = dsBlk1.Tables[0].Rows[0]["BLK_CODE"].ToString();
                }
                string str = "exec PDS_spSelectEntryDataBlockWise @blockCd";
                string[] param = { "@blockCd" };
                object[] value = { Session["UserID"].ToString().Substring(4, 6) };
                DataSet dsData = dbsApp.param_passing_fetch(str, param, value);

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    grdReport.DataSource = dsData;
                    grdReport.DataBind();
                }
                else
                {
                    string pop = "alert('No Record Found !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                }

                this.utl.SetSessionCookie();
                this.hfSession.Value = HttpUtility.HtmlEncode(this.Session["AuthTokenPage"].ToString());
                return;
            }
        }
    }
}
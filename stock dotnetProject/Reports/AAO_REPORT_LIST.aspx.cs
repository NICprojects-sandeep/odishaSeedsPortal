using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_AAO_REPORT_LIST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    private void Bind()
    {
        DataSet ds = new DataSet();
        string sql = "sp_AAO_report_list_block";
        string[] param = { "@block" };
        object[] value = { Session["Block"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            LinkButton4.Text = ds.Tables[0].Rows[0]["totReg"].ToString();
            LinkButton5.Text = ds.Tables[0].Rows[0]["fid"].ToString();
            Literal3.Text = (Convert.ToInt32(ds.Tables[0].Rows[0]["totReg"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[0]["fid"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[0]["deleted"].ToString())).ToString();
            Literal4.Text = ds.Tables[0].Rows[0]["deleted"].ToString();
            LinkButton1.Text = ds.Tables[0].Rows[0]["ccbrej"].ToString();
            LinkButton2.Text = ds.Tables[0].Rows[0]["pfmsrej"].ToString();
            LinkButton3.Text = ds.Tables[0].Rows[0]["nic"].ToString();
            Literal5.Text = (Convert.ToInt32(Literal3.Text) - (Convert.ToInt32(ds.Tables[0].Rows[0]["ccbrej"].ToString()) + Convert.ToInt32(ds.Tables[0].Rows[0]["pfmsrej"].ToString()) + Convert.ToInt32(ds.Tables[0].Rows[0]["nic"].ToString()))).ToString();
        }
        else
        {
            LinkButton4.Text = "0";
            LinkButton5.Text = "0";
            Literal3.Text = "0";
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAO_REPORT.aspx?id=1");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAO_REPORT.aspx?id=2");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("AAO_REPORT.aspx?id=3");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerList_Block.aspx?id=1");
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerList_Block.aspx?id=2");
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_NIC_AAO_REPORT_LIST : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                Session["Blocks"] = Session["UserID"].ToString().Substring(4, 6);
                //Session["Block"] = "2307";
                //Session["Dist"] = "23";
                //Bind();
                fillgp();
            }
        }
    }
    private void Bind()
    {
        try
        {
            DataSet ds = new DataSet();
            string sql = "NIC_AAO_RJCT_FARMER_LISTNEW";
            string[] param = { "@block", "@gp", "@vil" };
            object[] value = { Session["Blocks"].ToString(), drpGp.SelectedValue.Trim(), drpVillage.SelectedValue.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            grd_FarmerList.DataSource = ds.Tables[0];
            grd_FarmerList.DataBind();
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
            Label txtledName = (Label)Grow.FindControl("Label2");
            Session["fid"] = txtledName.Text;
            Response.Redirect("NIC_AAO_REPORT.aspx");
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (drpGp.SelectedIndex == 0)
        {
            string pop = "alert('Please Select Gram Panchayat !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
        else if (drpVillage.SelectedIndex == 0)
        {
            string pop = "alert('Please Select Village !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
        else
        {
            Bind();
        }
    }
    private void fillgp()
    {
        try
        {
        drpGp.Items.Clear();
        DataSet ds = new DataSet();
        string sql = "select int_GpID as GP_Code, vch_GPName as GP_Name from  dbo.PDS_GPMASTER where    int_BlockID in(select int_BlockID from (SELECT  c.BLK_CODE,c.AAO_CODE,c.PP,b.int_BlockID FROM  stock.dbo.JALANIDHI_DAO_AAO AS c INNER JOIN dbo.PDS_BLOCKMASTER AS b ON c.BLK_CODE COLLATE Latin1_General_CI_AI = b.NIC_BlockCode and c.pp='1') as tbltemp where AAO_CODE=@AAO_Code)  order by GP_Name";
        string[] param = { "@AAO_Code" };
        object[] value = { Session["Blocks"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        drpGp.DataSource = ds.Tables[0];
        drpGp.DataTextField = "GP_Name";
        drpGp.DataValueField = "GP_Code";
        drpGp.DataBind();
        this.drpGp.Items.Insert(0, "---Select---");
        drpVillage.Items.Clear();
        this.drpVillage.Items.Insert(0, "---Select---");

         }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }

    }
    protected void drpGp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpGp.SelectedIndex > 0)
            {
                drpVillage.Items.Clear();
                DataSet ds = new DataSet();
                string sql = "select int_VillageID as villg_code, vch_VillageName as villg_name from dbo.PDS_VILLAGEMASTER where int_GPID=@gp_code order by villg_name";
                string[] param = { "@gp_code" };
                object[] value = { drpGp.SelectedValue.Trim() };
                ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
                drpVillage.DataSource = ds.Tables[0];
                drpVillage.DataTextField = "villg_name";
                drpVillage.DataValueField = "villg_code";
                drpVillage.DataBind();
                this.drpVillage.Items.Insert(0, "---Select---");
            }
            else
            {
                drpVillage.Items.Clear();
                this.drpVillage.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Masters_DealerwiseStock : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillFinYr();
            FillCrop();
            btnSubmit.Visible = false;
            litDt.Text = DateTime.Now.ToShortDateString();
        }
    }
    private void FillFinYr()
    {
        try
        {
            drpSession.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSession.DataSource = ds;
                    drpSession.DataTextField = "FIN_YR";
                    drpSession.DataValueField = "FIN_YR";
                    drpSession.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
        //ddlFinYr.Items.Insert(0, "--Select--");
    }
    protected void FillCrop()
    {
        try
        {

            ds = new DataSet();
            ds = DsCrop();
            drpCropName.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpCropName.DataSource = ds;
                    this.drpCropName.DataValueField = "Crop_Code";
                    this.drpCropName.DataTextField = "Crop_Name";
                    this.drpCropName.DataBind();
                }
            }
            this.drpCropName.Items.Insert(0, "--Select--");

        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }

    public DataSet DsCrop()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Crop_Code,Crop_Name FROM mCrop WHERE is_active = 1 ORDER BY Crop_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            return ds;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return null;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grdDealerwiseStock.Rows)
        {
            DataTable dt = new DataTable();
            Label lblLIC_NO = (Label)gRow.FindControl("lblLIC_NO");
            TextBox txt = (TextBox)gRow.FindControl("txtPhySale");
            DataSet ds = new DataSet();
            string sql = "STOCK_INSDealerwiseStock";
            string[] param = { "@LIC_NO", "@CROP_ID", "@SaleQty", "@UpdatedBy", "@FIN_YEAR", "@SEASON" };
            object[] value = { lblLIC_NO.Text.Trim(), drpCropName.SelectedValue.Trim(), txt.Text.Trim() == "" ? "0" : txt.Text.Trim(), Session["UserID"].ToString(), drpSession.SelectedValue.Trim(), drpSeason.SelectedValue.Trim() == "0" ? "0" : drpSeason.SelectedValue.Trim() };
            ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);

        }
        BindGrid();
        string popp1 = "alert('Data Submitted Successfully !!!');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp1", popp1, true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        DataSet ds = new DataSet();
        string sql = "STOCK_UPDTDEALER_STOCK";
        string[] param = { "@BLOCK_ID", "@CROP_ID", "@FIN_YEAR", "@SEASON" };
        object[] value = { Session["UserID"].ToString().Substring(4, 6).Trim(), drpCropName.SelectedValue.Trim(), drpSession.SelectedValue.Trim() == "--Select--" ? "0" : drpSession.SelectedValue.Trim(), drpSeason.SelectedValue.Trim() == "0" ? "0" : drpSeason.SelectedValue.Trim() };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdDealerwiseStock.DataSource = ds.Tables[0];
            grdDealerwiseStock.DataBind();
            btnSubmit.Visible = true;
        }
        else
        {
            grdDealerwiseStock.DataSource = null;
            grdDealerwiseStock.DataBind();
            btnSubmit.Visible = false;
        }
    }
    protected void txtPhySale_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)(((TextBox)sender).Parent.Parent);
        TextBox phtAmnt = ((TextBox)gr.FindControl("txtPhySale"));
        Label lblACTUAL_SALE = ((Label)gr.FindControl("lblACTUAL_SALE"));
        Label balance = ((Label)gr.FindControl("lblBalance"));
        balance.Text = (decimal.Parse(phtAmnt.Text.Trim().ToString()) - decimal.Parse(lblACTUAL_SALE.Text.Trim().ToString())).ToString();

    }
}
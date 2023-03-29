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
public partial class Reports_RPT_DDAWiseZeroTransaction : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();

    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
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
            if (!(this.Session["UserType"].ToString() == "DRCS") && !(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") && !(this.Session["UserType"].ToString() == "DIST") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                btnPrint.Enabled = false;
                FillBlock();
                lblDist.Text = Session["DistName"].ToString();
            }
        }
    }

    private void FillBlock()
    {
        try
        {

            ds = new DataSet();
            ds = DsBlock();
            drpBlock.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.drpBlock.DataSource = ds;
                    this.drpBlock.DataValueField = "AAO_CODE";
                    this.drpBlock.DataTextField = "BLK_NAME";
                    this.drpBlock.DataBind();
                }
            }

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

    private DataSet DsBlock()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT distinct BLK_CODE,BLK_NAME,AAO_CODE FROM  JALANIDHI_DAO_AAO WHERE  DDA_CODE = " + Session["DistCode"].ToString() + " AND PP='1'", con);
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        string[] param = { "@AAO_CODE" };
        object[] value = { drpBlock.SelectedValue.Trim().Trim() };
        ds = dbsApp.param_passing_fetchDataSetSP("RPT_ZeroTrans", param, value);

        if (ds.Tables[0].Rows.Count > 0)
        {
            btnPrint.Enabled = true;
            grd_TransactionList.DataSource = ds.Tables[0];
            grd_TransactionList.DataBind();
        }
        else
        {
            btnPrint.Enabled = false;
            grd_TransactionList.DataSource = null;
            grd_TransactionList.DataBind();
        }
    }
    protected void grdExamWithclass_PreRender(object sender, EventArgs e)
    {
        MergeCell(new int[] { 1 });
    }
    public void MergeCell(int[] colIndices)
    {
        foreach (int colIndex in colIndices)
        {
            for (int rowIndex = grd_TransactionList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = grd_TransactionList.Rows[rowIndex];
                GridViewRow previousRow = grd_TransactionList.Rows[rowIndex + 1];
                if (row.Cells[colIndex].Text == previousRow.Cells[colIndex].Text)
                {
                    row.Cells[colIndex].RowSpan = previousRow.Cells[colIndex].RowSpan < 2 ? 2 : previousRow.Cells[colIndex].RowSpan + 1;
                    previousRow.Cells[colIndex].Visible = false;
                }
            }
        }
    }
}
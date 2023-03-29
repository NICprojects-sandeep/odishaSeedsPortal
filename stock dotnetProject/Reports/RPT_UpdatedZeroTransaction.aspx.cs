using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RPT_UpdatedZeroTransaction : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
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

                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                BindGrid();
            }
        }
    }
    private void BindGrid()
    {
        DataSet ds = new DataSet();

        string[] param = { "@AAO_CODE" };
        object[] value = { Session["UserID"].ToString().Substring(4, 6).Trim() };
        ds = dbsApp.param_passing_fetchDataSetSP("RPT_UpdtZeroTrans", param, value);

        if (ds.Tables[0].Rows.Count > 0)
        {
            grd_TransactionList.DataSource = ds.Tables[0];
            grd_TransactionList.DataBind();
        }
        else
        {
            grd_TransactionList.DataSource = null;
            grd_TransactionList.DataBind();
        }
    }
    protected void grdExamWithclass_PreRender(object sender, EventArgs e)
    {
        MergeCell(new int[] { 1, 2, 3 });
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
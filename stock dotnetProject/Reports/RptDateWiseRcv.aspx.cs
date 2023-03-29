using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_RptDateWiseRcv : System.Web.UI.Page
{    
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                
        }
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }
    public void MergeCell(int[] colIndices)
    {
        foreach (int colIndex in colIndices)
        {
            for (int rowIndex = gv_DTL.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gv_DTL.Rows[rowIndex];
                GridViewRow previousRow = gv_DTL.Rows[rowIndex + 1];
                if (row.Cells[colIndex].Text == previousRow.Cells[colIndex].Text)
                {
                    row.Cells[colIndex].RowSpan = previousRow.Cells[colIndex].RowSpan < 2 ? 2 : previousRow.Cells[colIndex].RowSpan + 1;
                    previousRow.Cells[colIndex].Visible = false;
                }
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gv_DTL.DataSource = null;
        gv_DTL.DataBind();

        if (string.IsNullOrWhiteSpace(txtFromDate.Text))
        {
            string popp = "alert('Please Provide a Date')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            txtFromDate.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtFromDate.Text) && string.IsNullOrWhiteSpace(txtToDate.Text))
        {
            string popp = "alert('Please Provide a Date')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            txtFromDate.Focus();
            return;
        }
        if (txtFromDate.Text == "" && txtToDate.Text != "")
        {
            string popp = "alert('Please Provide a Date')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            txtFromDate.Focus();
            return;
        }
        if (txtFromDate.Text != "")
        {
            lblText.Text = "Stock Receive Details For The Date : " + txtFromDate.Text;
        }
        if (txtToDate.Text != "")
        {
            lblText.Text += " - "+txtToDate.Text;
        }
        
        objUserBEL = new BLL_DropDown();
        objUserBEL.FROM_DATE = txtFromDate.Text;
        objUserBEL.TO_DATE = txtToDate.Text;
        objUserBEL.UserID = Session["UserID"].ToString();
        
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.GetDelRcvDateWise(objUserBEL);

        gv_DTL.DataSource = ds;
        gv_DTL.DataBind();
    }
    protected void gv_DTL_PreRender(object sender, EventArgs e)
    {
        MergeCell(new int[] { 1 });
        MergeCell(new int[] { 2 });
        //MergeCell(new int[] { 4 });
        //MergeCell(new int[] { 5 });
        //MergeCell(new int[] { 6 });
        //MergeCell(new int[] { 7 });
    }    
}
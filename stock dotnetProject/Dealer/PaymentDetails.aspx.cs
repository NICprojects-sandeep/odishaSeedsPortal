using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Dealer_PaymentDetails : System.Web.UI.Page
{
    DataSet ds;
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
            FillGrid();
        }
    }
    private void FillGrid()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserBELDIST.FDate = txtFromDate.Text.Trim();
        objUserBELDIST.TDate = txtToDate.Text.Trim();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.RptPayDtl(objUserBELDIST);
        //string[] param = { "@UPDATED_BY", "@FROMDATE", "@TODATE" };
        //object[] value = { Session["LIC_NO"].ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim() };
        //ds = dbsApp.param_passing_procStock("STOCK_DealerPayment", param, value);
        gvdealersale.DataSource = ds;
        gvdealersale.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}
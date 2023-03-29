using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ZeroTransReport : System.Web.UI.Page
{
    int qtyTotal = 0;
    int Qtyupd = 0;

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
        string sql = "zero_transaction_report";
        string[] param = { };
        object[] value = { };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        grd_FarmerList.DataSource = ds.Tables[0];
        grd_FarmerList.DataBind();
    }
    protected void grd_FarmerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int tmpupd = 0;
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "total").ToString());
            string check = DataBinder.Eval(e.Row.DataItem, "updated").ToString();
            if (check != "")
            {
                tmpupd = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "updated").ToString());
            }
            qtyTotal += tmpTotal;
            Qtyupd += tmpupd;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotal");
            lblTotalqty.Text = qtyTotal.ToString();
            Label lblupd = (Label)e.Row.FindControl("lblupd");
            lblupd.Text = Qtyupd.ToString();
            Label lblbal = (Label)e.Row.FindControl("lblbalance");
            lblbal.Text = (Convert.ToInt32(lblTotalqty.Text) - Convert.ToInt32(Qtyupd.ToString())).ToString();
        }
    }
}
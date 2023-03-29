using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Reports_Transaction_Accp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string sql = "view_TransactionReport";
        string[] param = {"@frmdt", "@todt"};
        object[] value = { Convert.ToDateTime(txtfrm.Text), Convert.ToDateTime(txtTo.Text) };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grd_FarmerList.Attributes.Add("style", "display:;");
            litReport.Text = string.Empty;
            //GenerateFarmerList(ds.Tables[0]);
            grd_FarmerList.DataSource = ds.Tables[0];
            grd_FarmerList.DataBind();
        }
        else
        {
            grd_FarmerList.Attributes.Add("style", "display:none;");
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }
}
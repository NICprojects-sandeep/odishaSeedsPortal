using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Rpt_TransDtls :  System.Web.UI.Page
{
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {            
            FillData(Session["TransId"].ToString());
        }
    }
    private void FillData(string Number)
    {
        string sql = "SP_SALEDTLSBYTRANSID";
        string[] param = { "@TRANSACTION_NO "};
        object[] value = { Number };
        ds = dbsAppStock.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GenerateReport(ds);            
        }
    }
    private void GenerateReport(DataSet ds)
    {
        if(ds!=null)
        {
            gvTrans.DataSource = ds;
            gvTrans.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Reports_CCBADVACE_PAY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            btnConfirm.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string sql = "advance_ccb_pay";
        string[] param = { "@dates" };
        object[] value = { Convert.ToDateTime(txtfrm.Text) };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
          
            grd_FarmerList.Attributes.Add("style", "display:;");
            litReport.Text = string.Empty;
            //GenerateFarmerList(ds.Tables[0]);
            grd_FarmerList.DataSource = ds.Tables[0];
            grd_FarmerList.DataBind();
            decimal subtot = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                subtot += Convert.ToDecimal(ds.Tables[0].Rows[i]["amount"]);
            }
            Label2.Text = subtot.ToString();
            CheckConfirm(txtfrm.Text);
        }
        else
        {
            btnConfirm.Visible = false;
            Label2.Text = string.Empty;
            grd_FarmerList.Attributes.Add("style", "display:none;");
            litReport.Text = "<div style='background-color:Gray; color:White; font-size: 18px; width:90%;'  class='tabletxt'  align='center'>No Records Found  !</div>";
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string sql = "advance_ccb_payConfirm";
        string[] param = { "@dates" };
        object[] value = { Convert.ToDateTime(txtfrm.Text) };
        dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        CheckConfirm(txtfrm.Text);
        string ppp = "alert('Advance payment is confirmed for selected date !');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
    }
  
    private void CheckConfirm(string p)
    {
        try
        {
            btnConfirm.Visible = true;
            DataSet ds = new DataSet();
            string sql = "Chk_ccb_payConfirm";
            string[] param = { "@dates" };
            object[] value = { Convert.ToDateTime(txtfrm.Text) };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnConfirm.Text = "Confirmed";
                btnConfirm.Enabled = false;
            }
            else
            {
                btnConfirm.Text = "Confirm";
                btnConfirm.Enabled = true;
            }
        }
        catch
        { 
        
        }
    }
}
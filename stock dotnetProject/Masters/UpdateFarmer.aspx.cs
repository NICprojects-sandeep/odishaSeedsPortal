using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Jalanidhi_VAW_UpdateFarmer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillfarmer();
        }

    }

    private void fillfarmer()
    {
        DataSet ds = new DataSet();
        string sql = string.Empty;

        sql = "GET_VAWFarmerList_Kalia";
        string[] param = { "@VCHGPID" };
        object[] value = { Session["UserID"].ToString().Substring(4, 6) };
        ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
        if (ds != null)
        {
            grdAll.DataSource = ds.Tables[0];
            grdAll.DataBind();
        }
        else
        {

            grdAll.DataSource = null;
            grdAll.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
            HiddenField lnlbtn = (HiddenField)row.FindControl("hfFid");

            RadioButton hfSMY = (RadioButton)row.FindControl("hfSMY");
            RadioButton hfSMN = (RadioButton)row.FindControl("hfSMN");

            RadioButton hfITY = (RadioButton)row.FindControl("hfITY");
            RadioButton hfITN = (RadioButton)row.FindControl("hfITN");

            RadioButton hfGSY = (RadioButton)row.FindControl("hfGSY");
            RadioButton hfGSN = (RadioButton)row.FindControl("hfGSN");

            RadioButton hfHODY = (RadioButton)row.FindControl("hfHODY");
            RadioButton hfHODN = (RadioButton)row.FindControl("hfHODN");

            TextBox Adhaar = (TextBox)row.FindControl("txtAdhaar");


            string Type = string.Empty;
            string HOF = string.Empty;
            string GS = string.Empty;
            string IT = string.Empty;
            if (hfSMY.Checked.Equals(true))
            {
                Type = "S";
            }
            else
            {
                Type = "O";
            }
            if (hfHODY.Checked.Equals(true))
            {
                HOF = "Y";
            }
            else
            {
                HOF = "N";
            }
            if (hfGSY.Checked.Equals(true))
            {
                GS = "Y";
            }
            else
            {
                GS = "N";
            }
            if (hfITY.Checked.Equals(true))
            {
                IT = "Y";
            }
            else
            {
                IT = "N";
            }

            DataSet ds = new DataSet();
            string sql = string.Empty;
            string ip_address = Request.UserHostAddress;
            sql = "FARMER_VAW_UPDT";
            string[] param = { "@NICFARMERID", "@F_TYPE", "@IT_PAY", "@GOV_SER", "@HOF", "@VAW_ID", "@IP", "@VCHAADHARNO" };
            object[] value = { lnlbtn.Value.Trim(), Type, IT, GS, HOF, Session["UserID"].ToString(), ip_address, Adhaar.Text.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            fillfarmer();
            string pop = "alert('Data Saved Successfully !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            return;


        }
        catch (Exception ex)
        { }
    }

    protected void grdAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the TextBox control.
            HiddenField hfAdhaar = (e.Row.FindControl("hfAdhar") as HiddenField);
            TextBox txtAdhaar = (e.Row.FindControl("txtAdhaar") as TextBox);
            Label lblAvl = (e.Row.FindControl("lblAvl") as Label);
            if (hfAdhaar.Value.Trim() != "")
            {
                lblAvl.Visible = true;
                txtAdhaar.Visible = false;
            }
            else
            {
                lblAvl.Visible = false;
                txtAdhaar.Visible = true;

            }

        }
    }
}
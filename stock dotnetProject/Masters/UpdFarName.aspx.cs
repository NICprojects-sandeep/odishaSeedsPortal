using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class Masters_UpdFarName : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
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
            if (!(this.Session["UserType"].ToString() == "DIST") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                BindGrid();
                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                return;
            }
        }
    }

    private void BindGrid()
    {
        DataSet ds = new DataSet();
        string[] param = { "@distid" };
        object[] value = { Session["DistCode"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSetSP("SP_Getfarmer", param, value);
        if (ds != null)
        {
            fillgrid(ds);

        }
    }

    public void fillgrid(DataSet ds)
    {
        grd_updfarnm.DataSource = ds.Tables[0];
        grd_updfarnm.DataBind();
    }

    protected void grd_updfarnm_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd_updfarnm.EditIndex = e.NewEditIndex;
        //BindGrid();

    }
    protected void grd_updfarnm_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string farmercode = grd_updfarnm.DataKeys[e.RowIndex].Value.ToString();

        Label lblDistCode = (Label)grd_updfarnm.Rows[e.RowIndex].FindControl("lblDistCode");
        TextBox txtfarmername = (TextBox)grd_updfarnm.Rows[e.RowIndex].FindControl("txtfarmername");

        TextBox txtfathername = (TextBox)grd_updfarnm.Rows[e.RowIndex].FindControl("txtfarmerfathrnm");
        TextBox txtAccHoldname = (TextBox)grd_updfarnm.Rows[e.RowIndex].FindControl("txtAccHoldname");
        TextBox txtmobno = (TextBox)grd_updfarnm.Rows[e.RowIndex].FindControl("txtmobno");

        //if(txtmobno.Text=="")
        //{
        //txtmobno.Enabled = true;
        DataSet dss = new DataSet();
        string[] parm = { "@farmername", "@farmerfname", "@VCHACCHOLDERNM", "@mobileno", "@farmercode", "@distcode" };
        object[] valu = { txtfarmername.Text, txtfathername.Text, txtAccHoldname.Text.Trim(), txtmobno.Text, farmercode, lblDistCode.Text };
        dss = dbsFarmer.param_passing_proc("SP_Updfarmernamemobile", parm, valu);
        string pop = "alert('Data Updated Successfully !!!')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        grd_updfarnm.EditIndex = -1;
        BindGrid();



        //}
        //else
        //{
        //    //txtmobno.Enabled = false;
        //    DataSet ds = new DataSet();
        //    string[] param = { "@farmername", "@farmerfname", "@farmercode", "@distcode" };
        //    object[] value = { txtfarmername.Text, txtfathername.Text, farmercode, lblDistCode.Text };
        //    ds = dbsFarmer.param_passing_fetchDataSetSP("SP_Updfarmernamemobile", param, value);
        //    grd_updfarnm.EditIndex = -1;
        //    BindGrid();
        //}



        //DataSet dss = new DataSet();
        //string[] par = { "@farmercode" };

    }
    protected void grd_updfarnm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_updfarnm.EditIndex = -1;
        BindGrid();
    }
}
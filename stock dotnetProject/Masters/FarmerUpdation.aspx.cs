using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FarmerUpdation : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    SqlConnection con;
    SqlCommand objCmd;
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
            if (!Page.IsPostBack)
            {
                Session["Blocks"] = Session["UserID"].ToString().Substring(4, 6);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(txtfarid.Text!="")
        {
            Button1.Visible = false;
            Button2.Visible = false;
            RadioButtonList1.SelectedIndex = -1;
            Panel1.Visible = false;
            check();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Can not blank.');", true);
        }
       
    }
    private void check()
    {
        try
        {
            DataSet ds = new DataSet();
            string sql = "select * from [FARMERDB].[dbo].[View_ValidFarmerForUpdation] where NICFARMERID=@fid and AAO_CODE=@user";
            string[] param = { "@fid", "@user" };
            object[] value = { txtfarid.Text.Trim(), Session["Blocks"].ToString() };
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            if(ds.Tables[0].Rows.Count>0)
            {
                RadioButtonList1.Visible = true;
            }
            else
            {
                RadioButtonList1.Visible = false;
                Panel1.Visible = false;
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('FarmerId not Present.');", true);
            }
           
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    private void Bind()
    {
        string BlockCode1 = "select * from [FARMERDB].[dbo].[View_ValidFarmerForUpdation] where NICFARMERID = @fc and AAO_CODE=@user";
        string[] paramBlk1 = { "@fc","@user" };
        object[] valueBlk1 = { txtfarid.Text.Trim(), Session["Blocks"].ToString() };
        DataSet dsBlk1 = dbsAppStock.param_passing_fetch(BlockCode1, paramBlk1, valueBlk1);
        if (dsBlk1.Tables[0].Rows.Count > 0)
        {
            lblDist.Text = dsBlk1.Tables[0].Rows[0]["vch_DistrictName"].ToString();
            lblBlock.Text = dsBlk1.Tables[0].Rows[0]["vch_BlockName"].ToString();
            lblBlock0.Text = dsBlk1.Tables[0].Rows[0]["vch_GPName"].ToString();
            lblBlock1.Text = dsBlk1.Tables[0].Rows[0]["vch_VillageName"].ToString();
            txtPinCode.Text = dsBlk1.Tables[0].Rows[0]["VCHPINCODE"].ToString();
            txtHofNm.Text = dsBlk1.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
            if (dsBlk1.Tables[0].Rows[0]["INTGENDER"].ToString() == "1")
                txtPinCode0.Text = "Male";
            else
                txtPinCode0.Text = "Female";
            if (dsBlk1.Tables[0].Rows[0]["INTCATEGOGY"].ToString() == "1")
                txtPinCode1.Text = "General";
            else if (dsBlk1.Tables[0].Rows[0]["INTCATEGOGY"].ToString() == "2")
                txtPinCode1.Text = "SC";
            else
                txtPinCode1.Text = "ST";
            txtDtBirth.Text = dsBlk1.Tables[0].Rows[0]["DTMDOB"].ToString();
            txtAge.Text = dsBlk1.Tables[0].Rows[0]["INTAGE"].ToString();
            if (dsBlk1.Tables[0].Rows[0]["INTRELATION"].ToString() == "1")
                txtDtBirth0.Text = "Father";
            else
                txtDtBirth0.Text = "Husband";

            txtHusbandNm.Text = dsBlk1.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
            txtVoterId.Text = dsBlk1.Tables[0].Rows[0]["VCHVOTERIDCARDNO"].ToString();
            txtAAdhar.Text = dsBlk1.Tables[0].Rows[0]["VCHAADHARNO"].ToString();
            txtMobileNo.Text = dsBlk1.Tables[0].Rows[0]["VCHMOBILENO"].ToString();
            txtDtBirth1.Text = dsBlk1.Tables[0].Rows[0]["INTFAMILYMEMBERS"].ToString();
            txtAccNo0.Text = dsBlk1.Tables[0].Rows[0]["VCHACCHOLDERNM"].ToString();
            txtAccNo.Text = dsBlk1.Tables[0].Rows[0]["VCHACCOUNTNO"].ToString();            
            txtBanknm.Text = dsBlk1.Tables[0].Rows[0]["vchBankName"].ToString();
            txtbrnm.Text= dsBlk1.Tables[0].Rows[0]["vchBranchName"].ToString();
            txtIfscCd.Text = dsBlk1.Tables[0].Rows[0]["VCHIFSCCODE"].ToString();
            HiddenField1.Value= dsBlk1.Tables[0].Rows[0]["VCHFARMERCODE"].ToString();
            HiddenField2.Value = dsBlk1.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
            HiddenField3.Value = dsBlk1.Tables[0].Rows[0]["NICFARMERID"].ToString();
            if(dsBlk1.Tables[0].Rows[0]["status"].ToString()=="0")
            {
                txtHusbandNm.ReadOnly = false;
                txtHusbandNm.CssClass = "etxtbox";
               
                btnupdate.Visible = true;
                btnupdate0.Visible = true;
            }
            else
            {
                txtHusbandNm.ReadOnly = true;
                txtHusbandNm.CssClass = "";
                
                btnupdate.Visible = false;
                btnupdate0.Visible = false;
            }

        }
    }

   

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        // UPDATEFATHERNAME
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
        con.Open();
        objCmd = new SqlCommand("UPDATEFATHERNAME", con);
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add("@VCHFARMERCODE", SqlDbType.VarChar).Value = HiddenField1.Value.ToString();
        objCmd.Parameters.Add("@VCHFATHERNAME", SqlDbType.VarChar).Value = txtHusbandNm.Text;
        objCmd.Parameters.Add("@OVCHFATHERNAME", SqlDbType.VarChar).Value = HiddenField2.Value.ToString();
        objCmd.Parameters.Add("@NICFARMERID", SqlDbType.VarChar).Value = HiddenField3.Value.ToString();
        objCmd.Parameters.Add("@IP", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
        objCmd.Parameters.Add("@USER", SqlDbType.VarChar).Value = Session["UserID"].ToString();
        objCmd.Parameters.Add("@val", SqlDbType.Int);
        objCmd.Parameters["@Val"].Direction = ParameterDirection.Output;
        objCmd.ExecuteNonQuery();
        string output = objCmd.Parameters["@Val"].Value.ToString();
        if (output == "0")
        {
            string pop = "alert('Successfully  Updated!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            txtHusbandNm.ReadOnly = true;
            txtHusbandNm.CssClass = "";
            RadioButtonList1.Visible = false;
            btnupdate.Visible = false;
            btnupdate0.Visible = false;
            Panel1.Visible = false;
            txtfarid.Text = "";
        }
        else if (output == "1")
        {
            string pop = "alert('Try again.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
    }

    protected void btnupdate0_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FarmerUpdation.aspx");
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(RadioButtonList1.SelectedItem.Value=="0")
        {
            txtHusbandNm.ReadOnly = true;
            txtHusbandNm.CssClass = "";
            btnupdate.Visible = false;
            btnupdate0.Visible = false;
            Bind();
            trBankDetails.Visible = true;
            familydetaiis.Visible = false;
            Panel1.Visible = true;
        }
        if (RadioButtonList1.SelectedItem.Value == "1")
        {
            txtHusbandNm.ReadOnly = true;
            txtHusbandNm.CssClass = "";
            btnupdate.Visible = false;
            btnupdate0.Visible = false;
            trBankDetails.Visible = false;
            familydetaiis.Visible = true;
            Bind();
            try
            {
               
                string sql = "select VCHFARMERCODE,VCHNAME,INTAGE,(SELECT COUNT(*) FROM [FARMERDB].[dbo].[Fmn_Family_Update] where VCHFARMERCODE=@VCHFARMERCODE) as counts from [FARMERDB].[dbo].[T_FARMER_FAMILYDETAILS] where VCHFARMERCODE=@VCHFARMERCODE and VCHNAME not in(select VCHACCHOLDERNM from [FARMERDB].[dbo].[M_FARMER_REGISTRATION] where VCHFARMERCODE=@VCHFARMERCODE) order by VCHNAME";
                string[] param = { "@VCHFARMERCODE" };
                object[] value = { HiddenField1.Value.ToString() };
                DataSet ds = dbsAppStock.param_passing_fetch(sql, param, value);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    if(ds.Tables[0].Rows[0]["counts"].ToString()=="0")
                    {
                        Button1.Visible = true;
                        Button2.Visible = true;
                    }
                    else
                    {
                        Button1.Visible = false;
                        Button2.Visible = false;
                    }
                    txtHusbandNm.ReadOnly = true;
                    txtHusbandNm.CssClass = "";

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            Panel1.Visible = true;
           
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkTest = (CheckBox)sender;
            GridViewRow grdRow = (GridViewRow)chkTest.NamingContainer;
            TextBox txtname = (TextBox)grdRow.FindControl("txt1");
            TextBox txtlage = (TextBox)grdRow.FindControl("txt2");
            if (chkTest.Checked)
            {
                txtname.ReadOnly = false;
                txtlage.ReadOnly = false;
                txtname.ForeColor = System.Drawing.Color.Black;
                txtlage.ForeColor = System.Drawing.Color.Black;
                txtname.BackColor = System.Drawing.Color.Transparent;
                txtlage.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                txtname.ReadOnly = true;
                txtlage.ReadOnly = true;
                txtname.ForeColor = System.Drawing.Color.Blue;
                txtlage.ForeColor = System.Drawing.Color.Blue;
                txtname.BackColor = System.Drawing.ColorTranslator.FromHtml("#cccccc");
                txtlage.BackColor = System.Drawing.ColorTranslator.FromHtml("#cccccc");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                if (chk.Checked.Equals(true))
                {
                    HiddenField hName = (HiddenField)GridView1.Rows[i].FindControl("HiddenField1");
                    HiddenField hAge = (HiddenField)GridView1.Rows[i].FindControl("HiddenField2");
                    TextBox txtName = (TextBox)GridView1.Rows[i].FindControl("txt1");
                    TextBox txtAge = (TextBox)GridView1.Rows[i].FindControl("txt2");

                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
                    con.Open();
                    objCmd = new SqlCommand("F_FamilyUpd", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@VCHFARMERCODE", SqlDbType.VarChar).Value = HiddenField1.Value.ToString();
                    objCmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["UserID"].ToString();
                    objCmd.Parameters.Add("@OVCHNAME", SqlDbType.VarChar).Value = hName.Value.ToString();
                    objCmd.Parameters.Add("@OINTAGE", SqlDbType.VarChar).Value = hAge.Value.ToString();
                    objCmd.Parameters.Add("@VCHNAME", SqlDbType.VarChar).Value = txtName.Text.Trim();
                    objCmd.Parameters.Add("@INTAGE", SqlDbType.VarChar).Value = txtAge.Text.Trim(); 
                    objCmd.Parameters.Add("@Ip_Address", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                    objCmd.Parameters.Add("@val", SqlDbType.Int);
                    objCmd.Parameters["@Val"].Direction = ParameterDirection.Output;
                    objCmd.ExecuteNonQuery();
                    string output = objCmd.Parameters["@Val"].Value.ToString();
                    if (output == "0")
                    {
                        string pop = "alert('Successfully  Updated!!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        RadioButtonList1.Visible = false;
                        btnupdate.Visible = false;
                        btnupdate0.Visible = false;
                        Panel1.Visible = false;
                        txtfarid.Text = "";
                    }
                    else if (output == "1")
                    {
                        string pop = "alert('Try again.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
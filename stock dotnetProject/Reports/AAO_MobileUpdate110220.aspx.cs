using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_AAO_MobileUpdate : System.Web.UI.Page
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
            if (!(this.Session["UserType"].ToString() == "AAOO") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            lblError.Text = string.Empty;
            if (!Page.IsPostBack)
            {
                Session["Blocks"] = Session["UserID"].ToString().Substring(4, 6);

            }
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Reset();
        DataSet ds = new DataSet();
        string sql = "select a.VCHFARMERCODE from [FARMERDB].[dbo].[View_ValidFarmerForUpdation] a where a.NICFARMERID=@NICFARMERID and a.AAO_CODE=@AAO_Code";
        string[] param = { "@NICFARMERID", "@AAO_Code" };
        object[] value = { txtfarid.Text, Session["Blocks"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
           
            bind();
        }
        else
        {
            DivError.Visible = true;
            lblError.Text = "No Data Found!!!";

        }
    }

    protected void Reset()
    {
        divDetails.Visible = false;
        Label1.Text = string.Empty;
        Label2.Text = string.Empty;
        lblDist.Text = string.Empty;
        lblBlock.Text = string.Empty;
        lblBlock0.Text = string.Empty;
        lblBlock1.Text = string.Empty;
        iblmob.Text = string.Empty;
    }
    private void bind()
    {
        try
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
            // con.Open();
            string sql = "select a.VCHFARMERNAME,a.VCHFATHERNAME,b.vch_DistrictName,c.vch_BlockName,d.vch_GPName,e.vch_VillageName,a.VCHMOBILENO from  M_FARMER_REGISTRATION a  inner join PDS_DISTRICTMASTER b on a.VCHDISTID = b.int_DistrictID  inner join PDS_BLOCKMASTER c on a.VCHBLOCKID = c.int_BlockID  inner join PDS_GPMASTER d on a.VCHGPID = d.int_GPID  inner join PDS_VILLAGEMASTER e on a.VCHVILLAGEID = e.int_VillageID where a.NICFARMERID=@NICFARMERID ";
            string[] param = { "@NICFARMERID" };
            object[] value = { txtfarid.Text.Trim() };
            DataSet ds = new DataSet();
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                divDetails.Visible = true;
                Label1.Text = ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                Label2.Text = ds.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                lblDist.Text = ds.Tables[0].Rows[0]["vch_DistrictName"].ToString();
                lblBlock.Text = ds.Tables[0].Rows[0]["vch_BlockName"].ToString();
                lblBlock0.Text = ds.Tables[0].Rows[0]["vch_GPName"].ToString();
                lblBlock1.Text = ds.Tables[0].Rows[0]["vch_VillageName"].ToString();
                iblmob.Text = ds.Tables[0].Rows[0]["VCHMOBILENO"].ToString();

            }
            else
            {
                lblError.Text = "No Data Found!!!";

            }
        }
        catch (Exception ex)
        {
            //pnlEdit.Visible = false;
            //lblResult.Text = "Contact Server Admin";
        }
    }
    private bool CheckUnique(string mob)
    {
        try
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
            // con.Open();
            string sql = "select VCHMOBILENO from M_FARMER_REGISTRATION  where VCHMOBILENO=@VCHMOBILENO  and INTDELETEDFLAG=0 and INTREJECTED=0";
            string[] param = { "@VCHMOBILENO" };
            object[] value = { mob };
            DataSet ds = new DataSet();
            ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        catch (Exception ex)
        {
            return false;
            //pnlEdit.Visible = false;
            //lblResult.Text = "Contact Server Admin";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            DateTime date = DateTime.Now;
            string Ip = Request.UserHostAddress;
            string createdBy = Session["Blocks"].ToString();
            bool ucheck=CheckUnique(txtMobile.Text.Trim());
            if(!ucheck)
            {
                bool msg = InsertData(txtfarid.Text.Trim(), Label1.Text.ToString(), iblmob.Text.ToString(), date, createdBy, Ip);
                if (msg == true)
                {
                    bool msg1 = UpdateFarmerMobNo(txtfarid.Text.Trim(), txtMobile.Text.Trim());
                    if (msg1 == true)
                    {
                        DivError.Visible = true;
                        lblError.Text = "Updated Successfully done...";
                        lblError.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        DivError.Visible = true;
                        lblError.Text = "Oops something error!!! Update Failled...";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    DivError.Visible = true;
                    lblError.Text = " You have Already Updated Mobile Number";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            else{
                     DivError.Visible = true;
                    lblError.Text = " This Mobile Number already Exist";
                    lblError.ForeColor = System.Drawing.Color.Red;
            }
        }

        catch (Exception ex)
        {

        }
    }

    protected bool InsertData(string Fid, string Name, string OldMobNo, DateTime date, string CreatedBy, string Ip)
    {
        bool result = false;
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
            con.Open();
            var objCmd = new SqlCommand("INSERT INTO mobileupdate([FarmerId],[Name],[OldMobNo],[Date],[CreatedBy],[Ip])VALUES(@Fid,@Name,@MobNumber,@date,@CreatedBy,@Ip)", con);
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.AddWithValue("@Fid", Fid);
            objCmd.Parameters.AddWithValue("@Name", Name);
            objCmd.Parameters.AddWithValue("@MobNumber", OldMobNo);
            objCmd.Parameters.AddWithValue("@date", date);
            objCmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            objCmd.Parameters.AddWithValue("@Ip", Ip);
            if (objCmd.ExecuteNonQuery() == 1)
            {
                result = true;
            }
            else
            {
                result = false;
            }

        }
        catch (Exception ex)
        {


        }
        return result;
    }
    protected bool UpdateFarmerMobNo(string fid, string NewMobNo)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
        con.Open();
        var objCmd1 = new SqlCommand("update M_FARMER_REGISTRATION set VCHMOBILENO=@MNo where NICFARMERID=@Fid", con);
        objCmd1.CommandType = CommandType.Text;
        objCmd1.Parameters.AddWithValue("@Fid", fid);
        objCmd1.Parameters.AddWithValue("@MNo", NewMobNo);
        int status = objCmd1.ExecuteNonQuery();
        if (status == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
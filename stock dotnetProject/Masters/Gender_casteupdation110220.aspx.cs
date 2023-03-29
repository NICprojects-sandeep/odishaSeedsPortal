using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gender_casteupdation : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                Session["Blocks"] = Session["UserID"].ToString().Substring(4, 6);
  
            }
        }
       
    }
    private void Bind(string farmercode)
    {
        DataSet ds = new DataSet();
        string sql=string.Empty;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            sql = "select *,'' AS GEN_ST,'0' AS CATE_ST  from [FARMERDB].[dbo].[View_ValidFarmerForUpdation] a where a.AAO_CODE=@AAO_Code AND a.VCHFARMERCODE=@VCHFARMERCODE and a.VCHFARMERCODE not in (select VCHFARMERCODE from AAO_CATEGORY_GENDER_UPDATION_NEW where VCHFARMERCODE=@VCHFARMERCODE and UPD_TYPE='C')";
           //sql = "select *,'' AS GEN_ST,'0' AS CATE_ST  from [FARMERDB].[dbo].[View_ValidFarmerForUpdation] a where a.AAO_CODE=@AAO_Code AND a.VCHFARMERCODE=@VCHFARMERCODE";
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            sql = "select *,'0' AS GEN_ST,'' AS CATE_ST  from [FARMERDB].[dbo].[View_ValidFarmerForUpdation] a where a.AAO_CODE=@AAO_Code AND a.VCHFARMERCODE=@VCHFARMERCODE and a.VCHFARMERCODE not in (select VCHFARMERCODE from AAO_CATEGORY_GENDER_UPDATION_NEW where VCHFARMERCODE=@VCHFARMERCODE and UPD_TYPE='G')";
        }
       
        string[] param = { "@AAO_Code", "@VCHFARMERCODE" };
        object[] value = { Session["Blocks"].ToString(), farmercode };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        grd_FarmerList.DataSource = ds.Tables[0];
        grd_FarmerList.DataBind();
    }
    private void VeryfiFarmerId()
    {
        grd_FarmerList.DataSource = null;
        grd_FarmerList.DataBind();
        DataSet ds = new DataSet();
        string sql = "select a.VCHFARMERCODE from [FARMERDB].[dbo].[View_ValidFarmerForUpdation] a where a.NICFARMERID=@NICFARMERID and a.AAO_CODE=@AAO_Code";
        string[] param = { "@NICFARMERID", "@AAO_Code" };
        object[] value = { txtfarid.Text,Session["Blocks"].ToString() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        if(ds.Tables[0].Rows.Count>0)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                if (CheckJalanidhi() == 0 && Checkfarmmechanisation() == 0)
                {

                    Bind(ds.Tables[0].Rows[0]["VCHFARMERCODE"].ToString());
                }
                else
                {

                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('This Farmer id has already in jalanidhi/FARM MECHANISATION transaction! So you can not change.');", true);
                }
            }
            else
            {
                Bind(ds.Tables[0].Rows[0]["VCHFARMERCODE"].ToString());
            }
           
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Farmerid Not Found!');", true);
        }
    }
    private int CheckJalanidhi()
    {
        DataSet ds = new DataSet();
        string sql = "select * from DBT_JALANIDHI_INSMASTER where APPLICATION_STATUS=0 and DEACTIVATE_ST=0 and PARMNT_ST=0 and FARMER_ID=@NICFARMERID";
        string[] param = { "@NICFARMERID" };
        object[] value = { txtfarid.Text};
        ds = dbsJalanidhi.param_passing_fetchDataSet(sql, param, value);
        return ds.Tables[0].Rows.Count;
    }
    private int Checkfarmmechanisation()
    {
        DataSet ds = new DataSet();
        string sql = "select * from FMNDBT_BOOKING_MASTER where FARMER_ID=@NICFARMERID and BOOKING_STATUS='V'";
        string[] param = { "@NICFARMERID" };
        object[] value = { txtfarid.Text };
        ds = dbsJalanidhi.param_passing_fetchDataSetFm(sql, param, value);
        return ds.Tables[0].Rows.Count;
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            VeryfiFarmerId();
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            VeryfiFarmerId();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(RadioButtonList1.SelectedIndex==0)
        {
            VeryfiFarmerId();
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            VeryfiFarmerId();
        }
        
    }
}
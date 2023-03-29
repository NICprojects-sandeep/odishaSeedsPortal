using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_FarmerTypeUpdate : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();


    SqlConnection objConn = new SqlConnection();
    SqlCommand objCmd = new SqlCommand();


    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["FarmerDBConnectionString"].ConnectionString;
    private UtilityLibrary utl = new UtilityLibrary();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                if ((this.Session["UserType"].ToString() == "AAOO") || (this.Session["UserType"].ToString() == "AHOO"))
                {

                }
                else
                {
                    base.Response.Redirect("../Unauthorised.aspx", true);
                    return;
                }
                if (!this.Page.IsPostBack)
                {
                    string BlockCode1 = "select BLK_CODE from [Stock].dbo.JALANIDHI_DAO_AAO where AAO_CODE=@AAOCode AND PP='1'";
                    string[] paramBlk1 = { "@AAOCode" };
                    object[] valueBlk1 = { Session["UserID"].ToString().Substring(4, 6) };
                    DataSet dsBlk1 = dbsAppStock.param_passing_fetch(BlockCode1, paramBlk1, valueBlk1);
                    if (dsBlk1.Tables[0].Rows.Count > 0)
                    {
                        Session["BlockCode"] = dsBlk1.Tables[0].Rows[0]["BLK_CODE"].ToString();
                        fillfARMERiD();
                    }
                    else
                    {
                        Session["BlockCode"] = null;

                    }
                    divfarmDetails.Visible = false;
                    divUpdate.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { }
    }


    protected void fillfARMERiD()
    {
        string Fid = "sp_GetFarmerId @BlockCode";
        string[] paramFid = { "@BlockCode" };
        object[] valueFid = { Session["BlockCode"].ToString() };
        DataSet dsFid = dbsFarmer.param_passing_fetchDataSet(Fid, paramFid, valueFid);
        if (dsFid.Tables[0].Rows.Count > 0)
        {
            ddlFarmerId.DataSource = dsFid;
            ddlFarmerId.DataTextField = "NICFARMERID";
            ddlFarmerId.DataValueField = "NICFARMERID";
            ddlFarmerId.DataBind();
            ddlFarmerId.Items.Insert(0, "--select--");
        }
        else
        {
            ddlFarmerId.DataSource = null;
            ddlFarmerId.DataBind();
            ddlFarmerId.Items.Insert(0, "--select--");
        }
    }
    //protected void btnsearch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string str1 = "select b.NIC_BlockCode,int_BlockID from M_FARMER_REGISTRATION as a inner join PDS_BLOCKMASTER as b on a.VCHBLOCKID=b.int_BlockID where NICFARMERID=@FarmerId";
    //        string[] paramDist1 = { "@FarmerId" };
    //        object[] valueDist1 = { txtfarmerid.Text };
    //        DataSet dsFarmDetails1 = dbsFarmer.param_passing_fetchDataSet(str1, paramDist1, valueDist1);
    //        if (dsFarmDetails1.Tables[0].Rows.Count > 0)
    //        {
    //            if (dsFarmDetails1.Tables[0].Rows[0]["NIC_BlockCode"].ToString() == Session["BlockCode"].ToString())
    //            {
    //                string str = "EXEC sp_SelectFarmerDetails @FarmerId";
    //                string[] paramDist = { "@FarmerId" };
    //                object[] valueDist = { txtfarmerid.Text };
    //                DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str, paramDist, valueDist);
    //                if (dsFarmDetails.Tables[0].Rows.Count > 0)
    //                {
    //                    divfarmDetails.Visible = true;
    //                    ViewState["VCHBLOCKID"] = dsFarmDetails.Tables[0].Rows[0]["VCHBLOCKID"].ToString();
    //                    ViewState["VCHFARMERCODE"] = dsFarmDetails.Tables[0].Rows[0]["VCHFARMERCODE"].ToString();
    //                    tdFarmerName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
    //                    tdFatherName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
    //                    tdDistrict.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_DistrictName"].ToString();
    //                    tdBlock.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_BlockName"].ToString();
    //                    tdGp.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_GPName"].ToString();
    //                    tdVillage.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_VillageName"].ToString();
    //                    tdGender.InnerText = dsFarmDetails.Tables[0].Rows[0]["Gender"].ToString();
    //                    tdCategory.InnerText = dsFarmDetails.Tables[0].Rows[0]["Category_Value"].ToString();

    //                }


    //            }

    //        }        

    //    }
    //    catch (Exception ex)
    //    { }
    //}
    protected void ddlFarmerId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string str = "EXEC sp_SelectFarmerDetails @FarmerId";
            string[] paramDist = { "@FarmerId" };
            object[] valueDist = { ddlFarmerId.SelectedValue };
            DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str, paramDist, valueDist);
            if (dsFarmDetails.Tables[0].Rows.Count > 0)
            {
                divfarmDetails.Visible = true;
                divUpdate.Visible = true;
                ViewState["VCHBLOCKID"] = dsFarmDetails.Tables[0].Rows[0]["VCHBLOCKID"].ToString();
                ViewState["VCHFARMERCODE"] = dsFarmDetails.Tables[0].Rows[0]["VCHFARMERCODE"].ToString();
                tdFarmerName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                tdFatherName.InnerText = dsFarmDetails.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                tdDistrict.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_DistrictName"].ToString();
                tdBlock.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_BlockName"].ToString();
                tdGp.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_GPName"].ToString();
                tdVillage.InnerText = dsFarmDetails.Tables[0].Rows[0]["vch_VillageName"].ToString();
                tdGender.InnerText = dsFarmDetails.Tables[0].Rows[0]["Gender"].ToString();
                tdCategory.InnerText = dsFarmDetails.Tables[0].Rows[0]["Category_Value"].ToString();

            }
        }
        catch (Exception ex)
        { }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(_connstrStock);
            con.Open();
            //SqlCommand cmd = new SqlCommand("sp_FarmerNameUpdate", con);
            //cmd.CommandType = CommandType.StoredProcedure;

            objCmd = new SqlCommand("sp_FarmerTypeUpdate", con);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@FarmerId", SqlDbType.VarChar).Value = ddlFarmerId.SelectedValue;
            objCmd.Parameters.Add("@FarmerType", SqlDbType.Char).Value = ddlFarmerType.SelectedValue;
            objCmd.ExecuteNonQuery();
            con.Close();

            fillfARMERiD();
            ddlFarmerType.SelectedIndex = 0;
            divfarmDetails.Visible = false;
            divUpdate.Visible = false;
            string pop = "alert('Farmer Type Update Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
        catch (Exception ex)
        { }
    }
}
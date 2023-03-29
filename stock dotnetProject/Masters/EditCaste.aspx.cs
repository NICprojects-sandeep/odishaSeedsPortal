using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditCaste : System.Web.UI.Page
{
    SqlCommand objCmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckDuplicate();
        if (!IsPostBack)
        {
            CheckDuplicate();
            bind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
        con.Open();
        objCmd = new SqlCommand("INSERT INTO AAO_CATEGORY_GENDER_UPDATION_NEW([VCHFARMERCODE],[UPD_TYPE],[OLD_VALUE],[NEW_VALUE],[UPD_BY],[UPD_DATE],[UPD_IP])VALUES(@VCH,'C',@OCATE,@NCATE,@CUSER,@CDATE,@CIP)", con);
        objCmd.CommandType = CommandType.Text;
        objCmd.Parameters.Add("@VCH", SqlDbType.VarChar).Value = HiddenField1.Value.ToString();
        objCmd.Parameters.Add("@OCATE", SqlDbType.VarChar).Value = HiddenField2.Value.ToString();
        objCmd.Parameters.Add("@NCATE", SqlDbType.VarChar).Value = DropDownList1.SelectedValue.ToString();
        objCmd.Parameters.Add("@CDATE", SqlDbType.DateTime).Value = DateTime.Now;
        objCmd.Parameters.Add("@CIP", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
        objCmd.Parameters.Add("@CUSER", SqlDbType.VarChar).Value = Session["UserID"].ToString();
        objCmd.ExecuteNonQuery();
        lblResult.Text = "Caste Changed Successfully!!";
        pnlEdit.Visible = false;
    }
    private void bind()
    {
        try
        {
            string EID = Request.QueryString["id"];
            string BlockCode1 = "select a.* from  [FARMERDB].[dbo].[View_ValidFarmerForUpdation] a where  a.AAO_CODE=@AAO_Code and a.VCHFARMERCODE=@vch";
            string[] paramBlk1 = { "@AAO_Code", "@vch" };
            object[] valueBlk1 = { Session["Blocks"].ToString(), EID };
            DataSet dsBlk1 = dbsAppStock.param_passing_fetch(BlockCode1, paramBlk1, valueBlk1);
            if (dsBlk1.Tables[0].Rows.Count > 0)
            {
                lblDist.Text = dsBlk1.Tables[0].Rows[0]["vch_DistrictName"].ToString();
                lblBlock.Text = dsBlk1.Tables[0].Rows[0]["vch_BlockName"].ToString();
                lblBlock0.Text = dsBlk1.Tables[0].Rows[0]["vch_GPName"].ToString();
                lblBlock1.Text = dsBlk1.Tables[0].Rows[0]["vch_VillageName"].ToString();
                lblBlock2.Text = dsBlk1.Tables[0].Rows[0]["NICFARMERID"].ToString();
                lblBlock3.Text = dsBlk1.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                lblBlock4.Text = dsBlk1.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                DropDownList1.SelectedValue = dsBlk1.Tables[0].Rows[0]["INTCATEGOGY"].ToString();
                HiddenField1.Value = EID.ToString();
                HiddenField2.Value = dsBlk1.Tables[0].Rows[0]["INTCATEGOGY"].ToString();
            }
            else
            {
                pnlEdit.Visible = false;
                lblResult.Text = "No data Present";

            }
        }
        catch (Exception ex)
        {
            pnlEdit.Visible = false;
            lblResult.Text = "Oops Something Error!.";
        }

    }
     private void CheckDuplicate()
    {
        try
        {
            string EID = Request.QueryString["id"];
            string BlockCode1 = "select VCHFARMERCODE from AAO_CATEGORY_GENDER_UPDATION_NEW where VCHFARMERCODE=@VCHFARMERCODE and UPD_TYPE='C'";
            string[] paramBlk1 = { "@VCHFARMERCODE"};
            object[] valueBlk1 = {EID};
            DataSet dsBlk1 = dbsFarmer.param_passing_fetchDataSet(BlockCode1, paramBlk1, valueBlk1);
            if (dsBlk1.Tables[0].Rows.Count > 0)
            {
               Response.Redirect("Gender_casteupdation.aspx");
            }
            
        }
        catch (Exception ex)
        {
            pnlEdit.Visible = false;
            lblResult.Text = ex.Message.ToString();
        }

    }
}
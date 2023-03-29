using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Farmer_FarmerInquiry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsvalidVoterId())
        {
            Session["VCHVOTERIDCARDNO"] = txtVoterId.Text.Trim();
            Response.Redirect("FarmerIdentification.aspx");
        }
        else
        {
            Session.Remove("VCHVOTERIDCARDNO");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowMassage", "alert('Invalid Voter Id Number !');", true);
        }

    }

    private bool IsvalidVoterId()
    {
        DataSet ds = new DataSet();

        string sql = "select COUNT(*) from dbo.M_FARMER_REGISTRATION  FR INNER JOIN dbo.Tbl_FarmerApprove FA ON FA.Farmer_Code=FR.VCHFARMERCODE WHERE  INTDELETEDFLAG=0 AND  INTREJECTED=0 AND Status in ('ACCP','CCB','UIID','AAO1','AAO','AAOC')  AND  VCHVOTERIDCARDNO=@VCHVOTERIDCARDNO";
        string[] param = { "@VCHVOTERIDCARDNO" };
        object[] value = { txtVoterId.Text.Trim() };
        ds = dbsFarmer.param_passing_fetchDataSet(sql, param, value);
        if (ds.Tables[0].Rows[0][0].ToString() != "0")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
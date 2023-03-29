using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FarmerAccountInformation : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();
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
            if (this.Session["UserType"].ToString() != "SEAD")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                return;
            }
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static VMFarmerInformation GetFarmerDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL farmerAccountBAL = new FarmerAccountBAL();
        VMFarmerInformation vMFarmerInformation = farmerAccountBAL.GetFarAccountInfo(voterNo, accNo);
        return vMFarmerInformation;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static FarmerApproveStatus GetFarmerApproveStatusDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL farmerAccountBAL = new FarmerAccountBAL();
        FarmerApproveStatus farmerApproveStatus = farmerAccountBAL.GetApproveStatus(voterNo, accNo);
        return farmerApproveStatus;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static int GetFarmerAAOAAO1StatusDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL farmerAccountBAL = new FarmerAccountBAL();
        int count = farmerAccountBAL.GetAAOAAO1Status(voterNo, accNo);
        return count;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static FarmerRejectStatus GetFarmerRejectStatusDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL farmerAccountBAL = new FarmerAccountBAL();
        FarmerRejectStatus farmerRejectStatus = farmerAccountBAL.GetRejectStatus(voterNo, accNo);
        return farmerRejectStatus;
    }

    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{

    //    FarmerAccountBAL farmerAccountBAL = new FarmerAccountBAL();
    //    Session["fid"] = farmerAccountBAL.GetFarmerCode(txtVoterID.Text);

    //    Response.Redirect("NIC_AAO_REPORT.aspx");
    //}
}
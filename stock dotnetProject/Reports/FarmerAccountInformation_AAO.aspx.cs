using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FarmerAccountInformation_AAO : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();
    private UtilityLibrary utl = new UtilityLibrary();
    //string aao_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //aao_Code = HttpContext.Current.Session["aaoCode"].ToString();

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
            if (this.Session["UserType"].ToString() != "AAOO")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                this.utl.SetSessionCookie();
                Session["AAO_Code"] = Session["UserID"].ToString().Substring(4, 6);
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                return;
            }
        }

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static int GetCount(string voterNo, string accNo)
    {
        FarmerAccountBAL_AAO farmerAccountBAL_AAO = new FarmerAccountBAL_AAO();

        return farmerAccountBAL_AAO.GetCount(HttpContext.Current.Session["AAO_Code"].ToString(), voterNo, accNo);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static VMFarmerInformation GetFarmerDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL_AAO farmerAccountBAL = new FarmerAccountBAL_AAO();
        VMFarmerInformation vMFarmerInformation = farmerAccountBAL.GetFarAccountInfo(voterNo, accNo);
        return vMFarmerInformation;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static FarmerApproveStatus GetFarmerApproveStatusDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL_AAO farmerAccountBAL = new FarmerAccountBAL_AAO();
        FarmerApproveStatus farmerApproveStatus = farmerAccountBAL.GetApproveStatus(voterNo, accNo);
        return farmerApproveStatus;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static int GetFarmerAAOAAO1StatusDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL_AAO farmerAccountBAL = new FarmerAccountBAL_AAO();
        int count = farmerAccountBAL.GetAAOAAO1Status(voterNo, accNo);
        return count;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static FarmerRejectStatus GetFarmerRejectStatusDetails(string voterNo, string accNo)
    {
        FarmerAccountBAL_AAO farmerAccountBAL = new FarmerAccountBAL_AAO();
        FarmerRejectStatus farmerRejectStatus = farmerAccountBAL.GetRejectStatus(voterNo, accNo);
        return farmerRejectStatus;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        FarmerAccountBAL_AAO farmerAccountBAL_AAO = new FarmerAccountBAL_AAO();
        //Session["fid"] = farmerAccountBAL_AAO.GetFarmerCode(txtVoterID.Text);
        Session["fid"] = farmerAccountBAL_AAO.GetFarmerCode(txtVoterID.Text.Trim() == "" ? txtAccountNo.Text.Trim() : txtVoterID.Text.Trim());
        Response.Redirect("NIC_AAO_REPORT.aspx");
    }
}
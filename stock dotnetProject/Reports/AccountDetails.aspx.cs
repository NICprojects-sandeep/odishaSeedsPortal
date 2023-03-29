using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_AccountDetails : System.Web.UI.Page
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
    public static List<RegDetails_M_Reg> GetRegisteredFarmer(string acNo, string mobNo)
    {
        PFMSBAL1 pFMSBAL = new PFMSBAL1();
        List<RegDetails_M_Reg> list = pFMSBAL.GetRegDetails(acNo, mobNo);
        return list;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<ReqSentDetails> GetReqSentDetails(string acNo, string mobNo)
    {
        PFMSBAL1 pFMSBAL = new PFMSBAL1();
       List<ReqSentDetails> list = pFMSBAL.GetReqSentDetailsData(acNo, mobNo);
       return list;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<F_ApproveDetails> GetApproveFarmerDetails(string acNo, string mobNo)
    {
        PFMSBAL1 pFMSBAL = new PFMSBAL1();
       List<F_ApproveDetails> list = pFMSBAL.GetFarmerDtls(acNo, mobNo);
       return list;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static AccountDetailsList GetAllNotApprovedData(string acNo, string mobNo)
    {
        PFMSBAL1 pFMSBAL = new PFMSBAL1();
        AccountDetailsList accountDetailsList = pFMSBAL.GetFarmerDtlsNotApproved(acNo, mobNo);

        return accountDetailsList;
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

public partial class Reports_BeneficiaryAccptResponseListDuplicate_AAO : System.Web.UI.Page
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
            if (this.Session["UserType"].ToString() != "AAOO")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                this.utl.SetSessionCookie();
                //Session["AAO_Code"] = Session["UserID"].ToString().Substring(4, 6);
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                return;
            }
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<VMBeneficiaryResponseListDuplicate> GetBeneficiaryResponseListDuplicate()
    {
        PFMSBAL1 pFMSBAL = new PFMSBAL1();
        List<VMBeneficiaryResponseListDuplicate> list = pFMSBAL.GetBenificiaryAccpResponse();
        return list.OrderBy(x => x.accountNo).ToList();
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using Newtonsoft.Json;

public partial class Login : System.Web.UI.Page
{
    private IDBManager db = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.AppendHeader("Pragma", "no-cache");
        base.Response.AppendHeader("Pragma", "no-store");
        base.Response.AppendHeader("cache-control", "no-cache , no-store, private, must-revalidate");
        base.Response.AppendHeader("Expires", "0");
        this.txtUserId.Focus();

        if (!this.Page.IsPostBack)
        {
            FillNews();
            this.Session.Add("Rand_No", 0);
            this.GenerateRandom();
            System.Web.UI.WebControls.Image image = this.captcha;
            long ticks = DateTime.Now.Ticks;
            image.ImageUrl = string.Concat("CaptchaHandler.ashx?r=", ticks.ToString());
            this.SetCaptchaText();
            return;
        }
    }
    private void FillNews()
    {
        //StringBuilder str = new StringBuilder("");
        string strScrolling = "<marquee behavior='scroll' direction='left'  scrolldelay='50' style='font-weight: bold;color: black;'><font face='color: red; background-color: yellow; font-size: 25px; font-weight: bold; text-align: center;'>";
        string News="";
        DataSet ds = new DataSet();
        int i = 1;
        ds = dbsApp.param_passing_text("SELECT NEWS_ID,NEWS FROM mLATESTNEWS WHERE IS_ACTIVE = 1 ORDER BY NEWS_ID DESC");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //    str.Append("<tr style='height: 25px;'>");            
            //    str.Append("<td style='color: red; background-color: yellow; font-size: 16px; font-weight: bold; text-align: center;'>" + dr["NEWS"].ToString() + "</td>");            
            //    str.Append("</tr>");
            News = News + "" + dr["NEWS"].ToString() + "&nbsp;&nbsp;&nbsp;";
            i++;
        }
        strScrolling = strScrolling + News + "&nbsp;&nbsp;&nbsp;</font><br><br></marquee>";
        litNews.Text = strScrolling.ToString();         
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        if (this.Page.IsValid)
        {
            try
            {
                try
                {
                    this.db.Open();
                    string str = this.hfPasswd.Value.ToString().Trim();
                    if (str.Length == 0)
                    {
                        throw new ApplicationException("Login Problem. Please contact website Administrator!");
                    }
                    if (Session["Captcha"].ToString() != txtCaptcha.Text.ToUpper().Trim())
                    {
                        GenerateRandom();                           // CHANGE SALT VALUE ON LOGIN ATTEMPT FAILURE
                        SetCaptchaText();
                        txtUserId.Text = "";
                        txtPwd.Text = "";
                        hfPasswd.Value = "";
                        txtCaptcha.Text = "";
                        throw new ApplicationException("Verification Code Does not Match ! Please Try Again");
                    }
                    int Is_Dealer = 0;
                    int Is_OSSC = 0;
                    int Is_SalePoint = 0;
                    objUserBELDIST = new BLL_Dealer();
                    objUserBELDIST.APPEMAIL_ID = txtUserId.Text.Trim();
                    objUserDLLDIST = new DLL_Dealer();
                    Is_Dealer = objUserDLLDIST.CheckLogIn(objUserBELDIST);
                    Is_OSSC = objUserDLLDIST.CheckLogInOSSC(objUserBELDIST);

                    //string[] param = { "@APPEMAIL_ID" };
                    //string[] value = { txtUserId.Text.Trim() };
                    //string LoginCount = "SELECT COUNT(DISTINCT A.SEED_LIC_DIST_ID)Cnt FROM [dafpseed].[dbo].[SEED_LIC_DIST] A " +
                    //"INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
                    //"INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID " +
                    //"WHERE B.APPEMAIL_ID = @APPEMAIL_ID AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC'";

                    //Is_Dealer = dbsApp.ReturnSingleValue(LoginCount, param, value);   

                    //string LoginCount = "SELECT COUNT(*) FROM [dbo].[LoginAccess] WHERE UserID=@UserID AND [Login_Status]='Login'";
                    //string[] Param = { "@UserID" };
                    //object[] Value = { txtUserId.Text.Trim() };
                    //DataSet dsCount = dbsAppStock.param_passing_fetch(LoginCount, Param, Value);
                    //DataTable dtCount = dsCount.Tables[0];
                    //if (int.Parse(dtCount.Rows[0][0].ToString()) >= 1)
                    //{
                    //    //msg maximum user exceed...
                    //    string pop = "alert('Maximum User Exceed !!!');";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);

                    //    this.GenerateRandom();
                    //    this.SetCaptchaText();
                    //    this.txtUserId.Text = "";
                    //    this.txtPwd.Text = "";
                    //    this.hfPasswd.Value = "";
                    //    this.txtCaptcha.Text = "";
                    //}
                    //else
                    //{
                    if (Is_Dealer > 0)
                    {
                        string CPass = txtPwd.Text.Trim();
                        string _Licno = "";
                        string _Licno1 = "";
                        DateTime _AprUpto = DateTime.Now;
                        //if (txtUserId.Text == "snayak.gadi@gmail.com")
                        //{
                        //}
                        //else
                        //{
                            string json = (new WebClient()).DownloadString("https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=" + txtUserId.Text + "&password=" + txtPwd.Text + "&password1=" + hfPas.Value);

                            DataTable dt = new DataTable();
                            dt = JsonConvert.DeserializeObject<DataTable>(json);

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    _Licno = HttpUtility.HtmlEncode(row["licenceNo"].ToString());
                                    _AprUpto = Convert.ToDateTime(HttpUtility.HtmlEncode(row["validTill"].ToString()));


                                    objUserBELDIST = new BLL_Dealer();
                                    objUserBELDIST.LICENCENO = _Licno;
                                    objUserDLLDIST = new DLL_Dealer();
                                    int _Cnt = objUserDLLDIST.ChkValidLic(objUserBELDIST);
                                    BAL = new BLL_DropDown();
                                    //BAL.LICENCENO1 = "";
                                    if (_Cnt == 1)
                                    {
                                        _Licno1 = _Licno;
                                        //BAL = new BLL_DropDown();
                                        BAL.LICENCENO1 = _Licno;
                                        BAL.APR_UPTO = _AprUpto;
                                    }
                                }
                                //if (Is_OSSC > 0)
                                //{
                                //    Is_SalePoint = 1;
                                //}
                            }
                        //}

                        //if (txtUserId.Text == "snayak.gadi@gmail.com")
                        //{

                        //    _Licno = "ODBHA1/2020-21/0026";


                            objUserBELDIST = new BLL_Dealer();
                            objUserBELDIST.LICENCENO = _Licno1;
                            objUserDLLDIST = new DLL_Dealer();
                            DataSet dsLogInDtl = new DataSet();
                            dsLogInDtl = objUserDLLDIST.CheckLic(objUserBELDIST);

                            //objUserBELDIST = new BLL_Dealer();
                            //objUserBELDIST.LICENCENO = _Licno;
                            //objUserDLLDIST = new DLL_Dealer();
                            //DataSet dsLogInDtl = new DataSet();
                            //dsLogInDtl = objUserDLLDIST.CheckLic0(objUserBELDIST);

                            if (dsLogInDtl != null)
                            {
                                if (dsLogInDtl.Tables[0].Rows.Count > 0)
                                {
                                    Session["SEED_LIC_DIST_ID"] = HttpUtility.HtmlEncode(dsLogInDtl.Tables[0].Rows[0]["SEED_LIC_DIST_ID"].ToString());
                                    Session["LIC_NO"] = HttpUtility.HtmlEncode(dsLogInDtl.Tables[0].Rows[0]["LIC_NO"].ToString());
                                    Session["REF_NO"] = HttpUtility.HtmlEncode(dsLogInDtl.Tables[0].Rows[0]["REF_NO"].ToString());
                                    Session["APP_FIRMNAME"] = HttpUtility.HtmlEncode(dsLogInDtl.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
                                    Session["APP_TYPE"] = HttpUtility.HtmlEncode(dsLogInDtl.Tables[0].Rows[0]["APP_TYPE"].ToString());
                                }
                            }

                            string BLOCK_CODE = "";
                            objUserBELDIST = new BLL_Dealer();
                            objUserBELDIST.SEED_LIC_DIST_ID = Session["SEED_LIC_DIST_ID"].ToString();
                            objUserDLLDIST = new DLL_Dealer();

                            BLOCK_CODE = objUserDLLDIST.GetBlockCode(objUserBELDIST);
                            if (!string.IsNullOrEmpty(BLOCK_CODE))
                            {
                                Session["BLOCK_ID"] = BLOCK_CODE;
                            }
                            string str1 = "";
                            this.Session["Rand_No"].ToString().Trim();
                            BAL = new BLL_DropDown();
                            BAL.UserID = this.txtUserId.Text;
                            DAL = new DLL_DropDown();
                            ds = new DataSet();
                            ds = DAL.ValidateUserName(BAL);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    str1 = ds.Tables[0].Rows[0]["Password"].ToString();
                                }
                            }
                            if ((str == ConvertToSHA512(this.Session["Rand_No"].ToString() + str1)))
                            {
                                Is_SalePoint = 1;
                            }

                            if (Is_SalePoint > 0)
                            {

                                this.db.CreateInParameters(1);
                                this.db.AddInParameters(0, "@UserID", this.txtUserId.Text.Trim());
                                dataSet = this.db.ExecuteDataSet(CommandType.StoredProcedure, "Stock_GetPassword");
                                if (dataSet.Tables[0].Rows.Count <= 0)
                                {
                                    ActivityLog activityLog = new ActivityLog()
                                    {
                                        UserID = this.txtUserId.Text,
                                        UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                        //UserIP = Request.UserHostAddress.ToString(),
                                        ActivityType = "Audit",
                                        Activity = "Login Attempt with Wrong UserID",
                                        PageURL = HttpContext.Current.Request.Url.ToString(),
                                        Remark = "Invalid UserID. Anonymous Login Attempt!",
                                        OS = Request.Browser.Platform.ToString(),
                                        BWSER = Request.Browser.Type.ToString()
                                    };
                                    ActivityLog.InsertActivityLog_Stock(activityLog);
                                    this.GenerateRandom();
                                    this.SetCaptchaText();
                                    this.txtUserId.Text = "";
                                    this.txtPwd.Text = "";
                                    this.hfPasswd.Value = "";
                                    this.txtCaptcha.Text = "";
                                    this.lbl_errmsg.Text = "Invalid UserID or Password!";
                                    this.lbl_errmsg.ForeColor = Color.Red;
                                }
                                else
                                {
                                    this.Session["Rand_No"].ToString().Trim();
                                    BAL = new BLL_DropDown();
                                    BAL.UserID = this.txtUserId.Text;
                                    DAL = new DLL_DropDown();
                                    ds = new DataSet();
                                    ds = DAL.ValidateUserName(BAL);
                                    if (ds != null)
                                    {
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            str1 = ds.Tables[0].Rows[0]["Password"].ToString();
                                        }
                                    }

                                    //string str1 = ds.Tables[0].Rows[0]["Password"].ToString();
                                    if (!(str == ConvertToSHA512(this.Session["Rand_No"].ToString() + str1)))
                                    {

                                        ActivityLog activityLog2 = new ActivityLog()
                                        {
                                            UserID = this.txtUserId.Text,
                                            UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                            //UserIP = Request.UserHostAddress.ToString(),
                                            ActivityType = "Audit",
                                            Activity = "Login Failure",
                                            PageURL = HttpContext.Current.Request.Url.ToString(),
                                            Remark = "Passowrd Does not Match",
                                            OS = Request.Browser.Platform.ToString(),
                                            BWSER = Request.Browser.Type.ToString()
                                        };
                                        ActivityLog.InsertActivityLog_Stock(activityLog2);
                                        this.GenerateRandom();
                                        this.SetCaptchaText();
                                        this.txtUserId.Text = "";
                                        this.txtPwd.Text = "";
                                        this.hfPasswd.Value = "";
                                        this.txtCaptcha.Text = "";
                                        this.lbl_errmsg.Text = "Invalid UserID or Password!";
                                        this.lbl_errmsg.ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        Is_SalePoint = 1;
                                        FormsAuthenticationTicket formsAuthenticationTicket = null;
                                        DateTime now = DateTime.Now;
                                        DateTime dateTime = DateTime.Now;
                                        formsAuthenticationTicket = new FormsAuthenticationTicket(1, txtUserId.Text, now, dateTime.AddMinutes(10), false, "");
                                        string str3 = FormsAuthentication.Encrypt(formsAuthenticationTicket);
                                        HttpContext.Current.Response.Cookies.Add(new HttpCookie("AuthToken", str3));
                                        HttpContext.Current.Session["AuthToken"] = str3;

                                        this.Session["UserID"] = dataSet.Tables[0].Rows[0]["UserID"].ToString();
                                        this.Session["UserType"] = dataSet.Tables[0].Rows[0]["User_Type"].ToString();
                                        this.Session["Name"] = dataSet.Tables[0].Rows[0]["Name"].ToString();
                                        this.Session["UID"] = dataSet.Tables[0].Rows[0]["UID"].ToString();
                                        this.Session["DistCode"] = dataSet.Tables[0].Rows[0]["Dist_Code"].ToString();
                                        this.Session["LGDistrict"] = dataSet.Tables[0].Rows[0]["LGDistrict"].ToString();
                                        this.Session["DistName"] = dataSet.Tables[0].Rows[0]["Dist_Name"].ToString();
                                        this.Session["FullName"] = dataSet.Tables[0].Rows[0]["FullName"].ToString();
                                        ActivityLog activityLog6 = new ActivityLog()
                                        {
                                            UserID = this.txtUserId.Text,
                                            UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                            OS = Request.Browser.Platform.ToString(),
                                            BWSER = Request.Browser.Type.ToString()
                                        };
                                        int AccessID = ActivityLog.InsertLoginAccess_Fert(activityLog6);
                                        this.Session["AccessID"] = AccessID;

                                        ActivityLog.InsertTempLog_Stock(this.Session.SessionID, "Session Timeout", Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
                                        ActivityLog.UpdateLog_Stock(this.Session["UserID"].ToString(), this.Session.SessionID);
                                        ActivityLog.UpdateLoginSession(this.Session["UserID"].ToString(), HttpContext.Current.Session.SessionID);
                                        ActivityLog activityLog3 = new ActivityLog()
                                        {
                                            UserID = this.Session["UserID"].ToString(),
                                            UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                            ActivityType = "Audit",
                                            Activity = "Login Success",
                                            PageURL = HttpContext.Current.Request.Url.ToString(),
                                            Remark = "Login Success",
                                            OS = Request.Browser.Platform.ToString(),
                                            BWSER = Request.Browser.Type.ToString()

                                        };
                                        ActivityLog.InsertActivityLog_Stock(activityLog3);
                                        this.GenerateRandom();
                                        if (Convert.ToInt32(dataSet.Tables[0].Rows[0]["time_diff"].ToString()) != 0)
                                        {
                                            this.Session["FirstLogin"] = "N";
                                            if (this.Session["UserType"].ToString() == "OAIC")
                                            {
                                                base.Response.Redirect("Masters/Home.aspx", false);
                                                return;
                                            }
                                            if (this.Session["UserType"].ToString() == "OSSC")
                                            {
                                                base.Response.Redirect("Masters/Home.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "ADMI")
                                            {
                                                if (this.Session["UserID"].ToString() == "fmn_director")
                                                {
                                                    base.Response.Redirect("Admin/DirectorHome.aspx", false);
                                                    return;
                                                }
                                                else
                                                {
                                                    base.Response.Redirect("Admin/AdminHome.aspx", false);
                                                    return;
                                                }
                                            }
                                            else if (this.Session["UserType"].ToString() == "SEAD")
                                            {
                                                base.Response.Redirect("Admin/SeedAdminHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "DIST")
                                            {
                                                int len = txtUserId.Text.Length;
                                                string us = txtUserId.Text.Substring(4, len - 4);
                                                if (us == "kandhamal")
                                                {
                                                    us = "PHULBANI";
                                                }

                                                if (us == "subarnapur")
                                                {
                                                    us = "SONEPUR";
                                                }

                                                string str11 = "select DDA_CODE,DDA_NAME from FMN_DDA_DAO_LIST where DDA_NAME=@Dname";
                                                string[] paramDist = { "@Dname" };
                                                object[] valueDist = { us };
                                                DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str11, paramDist, valueDist);

                                                if (dsFarmDetails.Tables[0].Rows.Count == 0)
                                                {
                                                }
                                                else
                                                {
                                                    Session["FMDDACD"] = dsFarmDetails.Tables[0].Rows[0]["DDA_CODE"].ToString();
                                                    Session["DDANM"] = dsFarmDetails.Tables[0].Rows[0]["DDA_NAME"].ToString();
                                                    base.Response.Redirect("Admin/DistHome.aspx", false);
                                                    return;
                                                }

                                                //base.Response.Redirect("Admin/DistHome.aspx", false);
                                                //return;

                                            }
                                            else if (this.Session["UserType"].ToString() == "DRCS")
                                            {
                                                base.Response.Redirect("Admin/DRCSHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "NFSM")
                                            {
                                                base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "RKVY")
                                            {
                                                base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "NMAD")
                                            {
                                                base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "SPAD")
                                            {
                                                base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "FOUR")
                                            {
                                                base.Response.Redirect("Reports/Transaction_Accp.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "AAOO")
                                            {
                                                base.Response.Redirect("Admin/AAOHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "AHOO")
                                            {
                                                base.Response.Redirect("Admin/AHOHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "RCSA")
                                            {
                                                base.Response.Redirect("Admin/RCSAHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "IRRI")
                                            {
                                                base.Response.Redirect("Admin/EDHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "FINA")
                                            {
                                                base.Response.Redirect("Admin/FinanceAdminHome.aspx", false);
                                                return;
                                            }
                                            else if (this.Session["UserType"].ToString() == "ADAP")
                                            {
                                                base.Response.Redirect("Admin/AdaptHome.aspx", false);
                                                return;
                                            }
                                            //else if (this.Session["UserType"].ToString() == "FMN_DDA")
                                            //{


                                            //    int len = txtUserId.Text.Length;
                                            //    string us = txtUserId.Text.Substring(4, len - 4);

                                            //    string str11 = "select DDA_CODE,DDA_NAME from FMN_DDA_DAO_LIST where DAO_NAME=@Dname";
                                            //    string[] paramDist = { "@Dname" };
                                            //    object[] valueDist = { us };
                                            //    DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str11, paramDist, valueDist);

                                            //    if (dsFarmDetails.Tables[0].Rows.Count == 0)
                                            //    {
                                            //    }
                                            //    else
                                            //    {
                                            //        Session["FMDDACD"] = dsFarmDetails.Tables[0].Rows[0]["DDA_CODE"].ToString();
                                            //        Session["DDANM"] = dsFarmDetails.Tables[0].Rows[0]["DDA_NAME"].ToString();
                                            //        base.Response.Redirect("Admin/DDAHome.aspx", false);
                                            //        return;
                                            //    }


                                            //}
                                            else
                                            {
                                                base.Response.Redirect("Login.aspx", false);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            this.Session["FirstLogin"] = "Y";
                                            if (this.Session["UserType"].ToString() == "OAIC")
                                            {
                                                base.Response.Redirect("Manufacture/ChangePasswd.aspx", false);
                                                return;
                                            }
                                            if (this.Session["UserType"].ToString() == "OSSC")
                                            {
                                                base.Response.Redirect("Manufacture/ChangePasswd.aspx", false);
                                                return;
                                            }
                                            //else if (this.Session["UserType"].ToString() == "ADMIN")
                                            //{
                                            //    //base.Response.Redirect("Manufacture/ChangePasswd.aspx", false);
                                            //    //return;
                                            //}
                                        }
                                    }

                                }
                            }
                            else
                            {
                                int Cntt1 = 0;
                                objUserBELDIST = new BLL_Dealer();
                                objUserBELDIST.APPEMAIL_ID = txtUserId.Text.Trim();
                                objUserDLLDIST = new DLL_Dealer();

                                Cntt1 = objUserDLLDIST.CntLogIn(objUserBELDIST);
                                if (Cntt1 > 1)
                                {
                                    Response.Redirect("Dealer/Decide.aspx", false);
                                }
                                else
                                {
                                    base.Response.Redirect("Dealer/Home.aspx", false);
                                    return;
                                }
                            }
                            //int Cntt1 = dbsApp.ReturnSingleValue("SELECT COUNT(*)Cnt FROM [dafpseed].[dbo].[SEED_LIC_DIST] WHERE SEED_LIC_DIST_ID IN (SELECT DISTINCT SEED_LIC_DIST_ID FROM [dafpseed].[dbo].[SEED_LIC_APP_DIST] WHERE APPEMAIL_ID = @APPEMAIL_ID) AND APP_STATUS = 'A' AND CONVERT(DATE,DATEADD(MONTH,1,APR_UPTO),103) >= CONVERT(DATE,GETDATE(),103) AND LIC_ACTIVE = 1 AND IS_ACTIVE = 1", param, value);
                        //}
                    }
                    else
                    {
                        string str1 = "";
                        this.db.CreateInParameters(1);
                        this.db.AddInParameters(0, "@UserID", this.txtUserId.Text.Trim());
                        dataSet = this.db.ExecuteDataSet(CommandType.StoredProcedure, "Stock_GetPassword");
                        if (dataSet.Tables[0].Rows.Count <= 0)
                        {
                            ActivityLog activityLog = new ActivityLog()
                            {
                                UserID = this.txtUserId.Text,
                                UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                //UserIP = Request.UserHostAddress.ToString(),
                                ActivityType = "Audit",
                                Activity = "Login Attempt with Wrong UserID",
                                PageURL = HttpContext.Current.Request.Url.ToString(),
                                Remark = "Invalid UserID. Anonymous Login Attempt!",
                                OS = Request.Browser.Platform.ToString(),
                                BWSER = Request.Browser.Type.ToString()
                            };
                            ActivityLog.InsertActivityLog_Stock(activityLog);
                            this.GenerateRandom();
                            this.SetCaptchaText();
                            this.txtUserId.Text = "";
                            this.txtPwd.Text = "";
                            this.hfPasswd.Value = "";
                            this.txtCaptcha.Text = "";
                            this.lbl_errmsg.Text = "Invalid UserID or Password!";
                            this.lbl_errmsg.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.Session["Rand_No"].ToString().Trim();
                            BAL = new BLL_DropDown();
                            BAL.UserID = this.txtUserId.Text;
                            DAL = new DLL_DropDown();
                            ds = new DataSet();
                            ds = DAL.ValidateUserName(BAL);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    str1 = ds.Tables[0].Rows[0]["Password"].ToString();
                                }
                            }

                            //string str1 = ds.Tables[0].Rows[0]["Password"].ToString();
                            if (!(str == ConvertToSHA512(this.Session["Rand_No"].ToString() + str1)))
                            {
                                ActivityLog activityLog2 = new ActivityLog()
                                {
                                    UserID = this.txtUserId.Text,
                                    UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                    //UserIP = Request.UserHostAddress.ToString(),
                                    ActivityType = "Audit",
                                    Activity = "Login Failure",
                                    PageURL = HttpContext.Current.Request.Url.ToString(),
                                    Remark = "Passowrd Does not Match",
                                    OS = Request.Browser.Platform.ToString(),
                                    BWSER = Request.Browser.Type.ToString()
                                };
                                ActivityLog.InsertActivityLog_Stock(activityLog2);
                                this.GenerateRandom();
                                this.SetCaptchaText();
                                this.txtUserId.Text = "";
                                this.txtPwd.Text = "";
                                this.hfPasswd.Value = "";
                                this.txtCaptcha.Text = "";
                                this.lbl_errmsg.Text = "Invalid UserID or Password!";
                                this.lbl_errmsg.ForeColor = Color.Red;
                            }
                            else
                            {
                                FormsAuthenticationTicket formsAuthenticationTicket = null;
                                DateTime now = DateTime.Now;
                                DateTime dateTime = DateTime.Now;
                                formsAuthenticationTicket = new FormsAuthenticationTicket(1, txtUserId.Text, now, dateTime.AddMinutes(10), false, "");
                                string str3 = FormsAuthentication.Encrypt(formsAuthenticationTicket);
                                HttpContext.Current.Response.Cookies.Add(new HttpCookie("AuthToken", str3));
                                HttpContext.Current.Session["AuthToken"] = str3;

                                this.Session["UserID"] = dataSet.Tables[0].Rows[0]["UserID"].ToString();
                                this.Session["UserType"] = dataSet.Tables[0].Rows[0]["User_Type"].ToString();
                                this.Session["Name"] = dataSet.Tables[0].Rows[0]["Name"].ToString();
                                this.Session["UID"] = dataSet.Tables[0].Rows[0]["UID"].ToString();
                                this.Session["DistCode"] = dataSet.Tables[0].Rows[0]["Dist_Code"].ToString();
                                this.Session["LGDistrict"] = dataSet.Tables[0].Rows[0]["LGDistrict"].ToString();
                                this.Session["DistName"] = dataSet.Tables[0].Rows[0]["Dist_Name"].ToString();
                                this.Session["FullName"] = dataSet.Tables[0].Rows[0]["FullName"].ToString();
                                ActivityLog activityLog6 = new ActivityLog()
                                {
                                    UserID = this.txtUserId.Text,
                                    UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                    OS = Request.Browser.Platform.ToString(),
                                    BWSER = Request.Browser.Type.ToString()
                                };
                                int AccessID = ActivityLog.InsertLoginAccess_Fert(activityLog6);
                                this.Session["AccessID"] = AccessID;

                                ActivityLog.InsertTempLog_Stock(this.Session.SessionID, "Session Timeout", Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
                                ActivityLog.UpdateLog_Stock(this.Session["UserID"].ToString(), this.Session.SessionID);
                                ActivityLog.UpdateLoginSession(this.Session["UserID"].ToString(), HttpContext.Current.Session.SessionID);
                                ActivityLog activityLog3 = new ActivityLog()
                                {
                                    UserID = this.Session["UserID"].ToString(),
                                    UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                    ActivityType = "Audit",
                                    Activity = "Login Success",
                                    PageURL = HttpContext.Current.Request.Url.ToString(),
                                    Remark = "Login Success",
                                    OS = Request.Browser.Platform.ToString(),
                                    BWSER = Request.Browser.Type.ToString()

                                };
                                ActivityLog.InsertActivityLog_Stock(activityLog3);
                                this.GenerateRandom();
                                if (Convert.ToInt32(dataSet.Tables[0].Rows[0]["time_diff"].ToString()) != 0)
                                {
                                    this.Session["FirstLogin"] = "N";
                                    if (this.Session["UserType"].ToString() == "OAIC")
                                    {
                                        base.Response.Redirect("Masters/Home.aspx", false);
                                        return;
                                    }
                                    if (this.Session["UserType"].ToString() == "OSSC")
                                    {
                                        base.Response.Redirect("Masters/Home.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "ADMI")
                                    {
                                        if (this.Session["UserID"].ToString() == "fmn_director")
                                        {
                                            base.Response.Redirect("Admin/DirectorHome.aspx", false);
                                            return;
                                        }
                                        else
                                        {
                                            base.Response.Redirect("Admin/AdminHome.aspx", false);
                                            return;
                                        }
                                        //base.Response.Redirect("Admin/AdminHome.aspx", false);
                                        //return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "SEAD")
                                    {
                                        base.Response.Redirect("Admin/SeedAdminHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "DIST")
                                    {
                                        int len = txtUserId.Text.Length;
                                        string us = txtUserId.Text.Substring(4, len - 4);
                                        if (us == "kandhamal")
                                        {
                                            us = "PHULBANI";
                                        }

                                        if (us == "subarnapur")
                                        {
                                            us = "SONEPUR";
                                        }

                                        string str11 = "select DDA_CODE,DDA_NAME from FMN_DDA_DAO_LIST where DDA_NAME=@Dname";
                                        string[] paramDist = { "@Dname" };
                                        object[] valueDist = { us };
                                        DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str11, paramDist, valueDist);

                                        if (dsFarmDetails.Tables[0].Rows.Count == 0)
                                        {
                                        }
                                        else
                                        {
                                            Session["FMDDACD"] = dsFarmDetails.Tables[0].Rows[0]["DDA_CODE"].ToString();
                                            Session["DDANM"] = dsFarmDetails.Tables[0].Rows[0]["DDA_NAME"].ToString();
                                            base.Response.Redirect("Admin/DistHome.aspx", false);
                                            return;
                                        }

                                        //base.Response.Redirect("Admin/DistHome.aspx", false);
                                        //return;

                                    }
                                    else if (this.Session["UserType"].ToString() == "DRCS")
                                    {
                                        base.Response.Redirect("Admin/DRCSHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "NFSM")
                                    {
                                        base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "RKVY")
                                    {
                                        base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "NMAD")
                                    {
                                        base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "SPAD")
                                    {
                                        base.Response.Redirect("Admin/SchemeHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "FOUR")
                                    {
                                        base.Response.Redirect("Reports/Transaction_Accp.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "AAOO")
                                    {
                                        base.Response.Redirect("Admin/AAOHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "AHOO")
                                    {
                                        base.Response.Redirect("Admin/AHOHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "RCSA")
                                    {
                                        base.Response.Redirect("Admin/RCSAHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "IRRI")
                                    {
                                        base.Response.Redirect("Admin/EDHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "FINA")
                                    {
                                        base.Response.Redirect("Admin/FinanceAdminHome.aspx", false);
                                        return;
                                    }
                                    else if (this.Session["UserType"].ToString() == "ADAP")
                                    {
                                        base.Response.Redirect("Admin/AdaptHome.aspx", false);
                                        return;
                                    }
                                    //else if (this.Session["UserType"].ToString() == "FMN_DDA")
                                    //{


                                    //    int len = txtUserId.Text.Length;
                                    //    string us = txtUserId.Text.Substring(4, len - 4);

                                    //    string str11 = "select DDA_CODE,DDA_NAME from FMN_DDA_DAO_LIST where DAO_NAME=@Dname";
                                    //    string[] paramDist = { "@Dname" };
                                    //    object[] valueDist = { us };
                                    //    DataSet dsFarmDetails = dbsFarmer.param_passing_fetchDataSet(str11, paramDist, valueDist);

                                    //    if (dsFarmDetails.Tables[0].Rows.Count == 0)
                                    //    {
                                    //    }
                                    //    else
                                    //    {
                                    //        Session["FMDDACD"] = dsFarmDetails.Tables[0].Rows[0]["DDA_CODE"].ToString();
                                    //        Session["DDANM"] = dsFarmDetails.Tables[0].Rows[0]["DDA_NAME"].ToString();
                                    //        base.Response.Redirect("Admin/DDAHome.aspx", false);
                                    //        return;
                                    //    }


                                    //}
                                    else
                                    {
                                        base.Response.Redirect("Login.aspx", false);
                                        return;
                                    }
                                }
                                else
                                {
                                    this.Session["FirstLogin"] = "Y";
                                    if (this.Session["UserType"].ToString() == "OAIC")
                                    {
                                        base.Response.Redirect("Manufacture/ChangePasswd.aspx", false);
                                        return;
                                    }
                                    if (this.Session["UserType"].ToString() == "OSSC")
                                    {
                                        base.Response.Redirect("Manufacture/ChangePasswd.aspx", false);
                                        return;
                                    }
                                    //else if (this.Session["UserType"].ToString() == "ADMIN")
                                    //{
                                    //    //base.Response.Redirect("Manufacture/ChangePasswd.aspx", false);
                                    //    //return;
                                    //}
                                }
                            }
                        }
                        //}
                    }
                }
                catch (ApplicationException applicationException1)
                {
                    ApplicationException applicationException = applicationException1;
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
                    ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
                }
            }
            finally
            {
                this.db.Close();
                dataSet.Clear();
                dataSet.Dispose();
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtUserId.Text = "";
        this.txtPwd.Text = "";
        this.hfPasswd.Value = "";
        this.txtCaptcha.Text = "";
        this.GenerateRandom();
        this.SetCaptchaText();
    }
    private void GenerateRandom()
    {
        char chr;
        char chr1;
        Random random = new Random();
        string empty = string.Empty;
        for (int i = 0; i < 16; i++)
        {
            do
            {
                chr1 = Convert.ToChar(random.Next(48, 128));
                chr = chr1;
            }
            while (!Regex.IsMatch(chr1.ToString(), "[A-Za-z0-9]"));
            empty = string.Concat(empty, chr);
        }
        this.Session["Rand_No"] = empty.ToString();
        this.hfRandNo.Value = empty.ToString();
    }
    private void SetCaptchaText()
    {
        char chr;
        char chr1;
        Random random = new Random();
        string empty = string.Empty;
        for (int i = 0; i < 6; i++)
        {
            do
            {
                chr1 = Convert.ToChar(random.Next(48, 128));
                chr = chr1;
            }
            while (!Regex.IsMatch(chr1.ToString(), "[ACDEFGHJKLMNPQRTUVWXYZ234679]"));
            empty = string.Concat(empty, chr);
        }
        this.Session["Captcha"] = empty.ToString();
    }
    private string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
        {
            return builder.ToString().ToLower();
        }
        return builder.ToString();

    }
    private string ConvertToSHA512(string pass)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(pass);
        SHA512CryptoServiceProvider sha512hasstring = new SHA512CryptoServiceProvider();
        byte[] hash = sha512hasstring.ComputeHash(bytes);
        return ByteArrayToHexString(hash);
    }
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
        {
            hex.AppendFormat("{0:x2}", b);
        }

        return hex.ToString();
    }     
}
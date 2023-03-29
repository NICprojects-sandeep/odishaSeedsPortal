using DataAccessLayer;
using System;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;

public partial class Manufacture_ChangePasswd : System.Web.UI.Page
{
    private IDBManager db = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    DataSet ds;
    private string FirstLogin = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null || this.Session["AuthToken"] == null || base.Request.Cookies["AuthToken"] == null)
        {
            this.utl.SessionReset();
        }
        else if (this.Session["AuthToken"].ToString().Equals(base.Request.Cookies["AuthToken"].Value))
        {
            this.FirstLogin = this.Session["FirstLogin"].ToString();
            if (!this.Page.IsPostBack)
            {
                this.lblUserName.Text = this.Session["UserID"].ToString();
                this.utl.SetSessionCookie();
                this.SesTicket.Value = this.Session["AuthTokenPage"].ToString();
                this.Session.Add("Rand_No", 0);
                this.GenerateRandom();
                if (this.FirstLogin == "Y")
                {
                    ((HyperLink)base.Master.FindControl("Mhome")).Enabled = false;
                }
                ActivityLog activityLog = new ActivityLog()
                {
                    UserID = this.Session["UserID"].ToString(),
                    UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                    ActivityType = "Audit",
                    Activity = (this.FirstLogin == "N" ? "Change Password Attempt" : "Change Password Attempt (First Login)"),
                    PageURL = HttpContext.Current.Request.Url.ToString(),
                    Remark = "",
                    OS = Request.Browser.Platform.ToString(),
                    BWSER = Request.Browser.Type.ToString()
                };
                ActivityLog.InsertActivityLog_Stock(activityLog);
            }
        }
        else
        {
            this.utl.SessionReset();
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (this.Session["UserType"].ToString() == "OSSC")
        {
            this.Page.MasterPageFile = "../MasterPages/STOCK.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "OAIC")
        {
            this.Page.MasterPageFile = "../MasterPages/STOCK.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "DIST")
        {
            this.Page.MasterPageFile = "../MasterPages/Dist.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "DRCS")
        {
            this.Page.MasterPageFile = "../MasterPages/DRCS.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "NFSM")
        {
            this.Page.MasterPageFile = "../MasterPages/Scheme.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "RKVY")
        {
            this.Page.MasterPageFile = "../MasterPages/Scheme.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "NMAD")
        {
            this.Page.MasterPageFile = "../MasterPages/Scheme.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "SPAD")
        {
            this.Page.MasterPageFile = "../MasterPages/Scheme.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "AAOO")
        {
            this.Page.MasterPageFile = "../MasterPages/AAO_.master";
            return;
        }
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        string empty = string.Empty;
        string lower = string.Empty;
        empty = this.hfPasswdE.Value.ToString().Trim();
        if (this.Page.IsValid)
        {
            if (this.SesTicket.Value == this.Session["AuthTokenPage"].ToString() || this.Session["AuthTokenPage"] != null)
            {
                try
                {
                    try
                    {
                        this.db.Open();
                        if (this.hfPasswd.Value.Length <= 0)
                        {
                            throw new ApplicationException("Please Check Password Before Submit!");
                        }
                        if (this.Session["UserID"].ToString() == "")
                        {
                            throw new ApplicationException("Session Timed Out. Please Login Again!");
                        }
                        this.db.CreateInParameters(1);
                        this.db.AddInParameters(0, "@UserID", this.Session["UserID"].ToString());

                        string OldPass = "";
                        BAL = new BLL_DropDown();
                        BAL.UserID = this.Session["UserID"].ToString();
                        DAL = new DLL_DropDown();
                        ds = new DataSet();
                        ds = DAL.ValidateUserName(BAL);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                OldPass = ds.Tables[0].Rows[0]["Password"].ToString();
                            }
                        }

                        //dataSet = this.db.ExecuteDataSet(CommandType.StoredProcedure, "Stock_GetPassword");
                        //if (dataSet.Tables[0].Rows.Count > 0)
                        //{
                        //    this.Session["Rand_No"].ToString().Trim();
                        //    string str = dataSet.Tables[0].Rows[0]["Passwd"].ToString();
                        //    string str1 = string.Concat(this.Session["Rand_No"].ToString().Trim(), str);
                        //    lower = FormsAuthentication.HashPasswordForStoringInConfigFile(str1, "MD5").ToLower();
                        //}
                        //if (empty != lower)
                        if (empty != ConvertToSHA512(this.Session["Rand_No"].ToString() + OldPass))
                        {
                            ActivityLog activityLog = new ActivityLog()
                            {
                                UserID = this.Session["UserID"].ToString(),
                                UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                ActivityType = "Audit",
                                Activity = (this.FirstLogin == "N" ? "Change Password Failure" : "Change Password Failure (First Login)"),
                                PageURL = HttpContext.Current.Request.Url.ToString(),
                                Remark = "Existing password did not match!",
                                OS = Request.Browser.Platform.ToString(),
                                BWSER = Request.Browser.Type.ToString()
                            };
                            ActivityLog.InsertActivityLog_Stock(activityLog);
                            throw new ApplicationException("Existing password did not match!");
                        }

                        BAL = new BLL_DropDown();
                        BAL.UserID = Session["UserID"].ToString();
                        BAL.UserPass = this.hfPasswd.Value;
                        DAL = new DLL_DropDown();
                        DAL.UpdPass(BAL);
                        string str2 = "0";
                        //this.db.CreateInParameters(3);
                        //this.db.AddInParameters(0, "@UserID", this.Session["UserID"].ToString());
                        //this.db.AddInParameters(1, "@Passwd", this.hfPasswd.Value);
                        //this.db.AddInParameters(2, "@PasswdE", this.hfPasswdE.Value);
                        //this.db.CreateOutParameters(1);
                        //this.db.AddOutParameters(0, "@msg", 1, 50);
                        //this.db.ExecuteNonQuery(CommandType.StoredProcedure, "Stock_SP_ChangePasswd");
                        //string str2 = this.db.outParameters[0].Value.ToString();
                        if (str2.Trim() == "0")
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Password changed successfully!');", true);
                            ActivityLog activityLog1 = new ActivityLog()
                            {
                                UserID = this.Session["UserID"].ToString(),
                                UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                ActivityType = "Audit",
                                Activity = (this.FirstLogin == "N" ? "Change Password Success" : "Change Password Success (First Login)"),
                                PageURL = HttpContext.Current.Request.Url.ToString(),
                                Remark = "",
                                OS = Request.Browser.Platform.ToString(),
                                BWSER = Request.Browser.Type.ToString()
                            };
                            ActivityLog.InsertActivityLog_Stock(activityLog1);
                            this.hfPasswd.Value = "";
                            this.hfPasswdE.Value = "";
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('Password Changed!');"), true);
                            base.Response.Redirect(string.Concat("../ChangePwdConfirm.aspx?v=", this.Session["AuthTokenPage"].ToString()), false);
                            //base.Response.End();
                            return;
                        }
                        else if (str2.Trim() == "1")
                        {
                            ActivityLog activityLog2 = new ActivityLog()
                            {
                                UserID = this.Session["UserID"].ToString(),
                                UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                ActivityType = "Audit",
                                Activity = (this.FirstLogin == "N" ? "Change Password Failure" : "Change Password Failure (First Login)"),
                                PageURL = HttpContext.Current.Request.Url.ToString(),
                                Remark = "New password can not be same as previous 3 passwords!",
                                OS = Request.Browser.Platform.ToString(),
                                BWSER = Request.Browser.Type.ToString()
                            };
                            ActivityLog.InsertActivityLog_Stock(activityLog2);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('New Password Can not be Same as Previous 3 Passwords. Please Try with Different Password!');", true);
                        }
                        else if (str2.Trim() == "2")
                        {
                            ActivityLog activityLog3 = new ActivityLog()
                            {
                                UserID = this.Session["UserID"].ToString(),
                                UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                ActivityType = "Audit",
                                Activity = (this.FirstLogin == "N" ? "Change Password Failure" : "Change Password Failure (First Login)"),
                                PageURL = HttpContext.Current.Request.Url.ToString(),
                                Remark = "Database could not complete change password request!",
                                OS = Request.Browser.Platform.ToString(),
                                BWSER = Request.Browser.Type.ToString()
                            };
                            ActivityLog.InsertActivityLog_Stock(activityLog3);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Problem Encountered! Please Try Again!');", true);
                        }
                        else if (str2.Trim() == "3")
                        {
                            ActivityLog activityLog4 = new ActivityLog()
                            {
                                UserID = this.Session["UserID"].ToString(),
                                UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                ActivityType = "Audit",
                                Activity = (this.FirstLogin == "N" ? "Change Password Failure" : "Change Password Failure (First Login)"),
                                PageURL = HttpContext.Current.Request.Url.ToString(),
                                Remark = "Existing password did not match!",
                                OS = Request.Browser.Platform.ToString(),
                                BWSER = Request.Browser.Type.ToString()
                            };
                            ActivityLog.InsertActivityLog_Stock(activityLog4);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Existing Password Does not Match!');", true);
                        }
                        this.hfPasswd.Value = "";
                        this.hfPasswdE.Value = "";
                    }
                    catch (ApplicationException applicationException1)
                    {
                        ApplicationException applicationException = applicationException1;
                        this.lblMesg.Visible = true;
                        this.lblMesg.Text = applicationException.Message;
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
                    }
                    catch (Exception exception)
                    {
                        ExceptionHandler.WriteEx(exception);
                    }
                }
                finally
                {
                    this.db.Close();
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Problem Encountered While Executing the Request. Please Login Again!');", true);
            }
        }
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
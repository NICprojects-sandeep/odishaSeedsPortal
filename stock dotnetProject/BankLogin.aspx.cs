using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Security.Cryptography;

public partial class BankLogin : System.Web.UI.Page
{
    private IDBManager db = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["SqlConAuth"].ConnectionString);
    private UtilityLibrary utl = new UtilityLibrary();
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.AppendHeader("Pragma", "no-cache");
        base.Response.AppendHeader("Pragma", "no-store");
        base.Response.AppendHeader("cache-control", "no-cache , no-store, private, must-revalidate");
        base.Response.AppendHeader("Expires", "0");
        this.fname.Focus();

        if (!this.Page.IsPostBack)
        {
            this.Session.Add("Rand_No", 0);
            this.GenerateRandom();
           
            long ticks = DateTime.Now.Ticks;
          
            this.SetCaptchaText();
            return;
        }
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
                    this.db.CreateInParameters(1);
                    this.db.AddInParameters(0, "@Username", this.fname.Text.Trim());
                    dataSet = this.db.ExecuteDataSet(CommandType.StoredProcedure, "NicAutht_UserLogin");
                    this.db.Close();
                    if (dataSet.Tables[0].Rows.Count <= 0)
                    {
                        ActivityLog activityLog = new ActivityLog()
                        {
                            UserID = this.fname.Text,
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
                        this.fname.Text = "";
                        this.email.Text = "";
                        this.hfPasswd.Value = "";
                        this.lbl_errmsg.Text = "Invalid UserID or Password!";
                        this.lbl_errmsg.ForeColor = Color.Red;
                    }
                    else
                    {

                        // this.Session["Rand_No"].ToString().Trim();
                        //this.fname.Text = this.Session["Rand_No"].ToString();
                        string str1 = dataSet.Tables[0].Rows[0]["Password"].ToString();
                       // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", str, "');"), true);
                        if (!(str == ConvertToSHA512(this.Session["Rand_No"].ToString() + str1)))
                        {
                            ActivityLog activityLog2 = new ActivityLog()
                            {
                                UserID = this.fname.Text,
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
                           // this.fname.Text = "";
                            this.email.Text = "";
                            this.hfPasswd.Value = "";
                            this.lbl_errmsg.Text = "Invalid UserID or Password!";
                            this.lbl_errmsg.ForeColor = Color.Red;
                        }
                        else
                        {
                            FormsAuthenticationTicket formsAuthenticationTicket = null;
                            DateTime now = DateTime.Now;
                            DateTime dateTime = DateTime.Now;
                            formsAuthenticationTicket = new FormsAuthenticationTicket(1, "FERT", now, dateTime.AddMinutes(10), false, "");
                            string str3 = FormsAuthentication.Encrypt(formsAuthenticationTicket);
                            HttpContext.Current.Response.Cookies.Add(new HttpCookie("AuthToken", str3));
                            HttpContext.Current.Session["AuthToken"] = str3;
                            this.Session["UserID"] = this.fname.Text.Trim();
                            this.Session["UserType"] = dataSet.Tables[0].Rows[0]["User_Type"].ToString();
                            if (this.Session["UserType"].ToString() == "NICA")
                            {

                                base.Response.Redirect("Admin/Dashboard.aspx", false);
                                return;
                            }
                            else if (this.Session["UserType"].ToString() == "SCHO")
                            {
                                bal obj = new bal();
                                this.Session["SchemeRole"] = obj.getSchemeOfficerRole(this.Session["UserID"].ToString());
                                base.Response.Redirect("SchemeOfficer/Dashboard.aspx", false);
                                return;
                            }
                            else
                            {
                                base.Response.Redirect("BankLogin.aspx", false);
                                return;
                            }

                        }
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
        this.fname.Text = "";
        this.email.Text = "";
        this.hfPasswd.Value = "";
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
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
        {
            hex.AppendFormat("{0:x2}", b);
        }

        return hex.ToString();
    }

    private string ConvertToSHA512(string pass)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(pass);
        SHA512CryptoServiceProvider sha512hasstring = new SHA512CryptoServiceProvider();
        byte[] hash = sha512hasstring.ComputeHash(bytes);
        return ByteArrayToHexString(hash);
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
}
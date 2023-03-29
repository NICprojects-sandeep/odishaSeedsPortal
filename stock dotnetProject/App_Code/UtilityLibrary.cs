using DataAccessLayer;
using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;

public class UtilityLibrary
{
    private IDBManager db = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);

    public UtilityLibrary()
    {
    }

    public void ChangeSession()
    {
        try
        {
            FormsAuthenticationTicket formsAuthenticationTicket = null;
            DateTime now = DateTime.Now;
            DateTime dateTime = DateTime.Now;
            formsAuthenticationTicket = new FormsAuthenticationTicket(1, "FERT", now, dateTime.AddMinutes(10), false, "");
            string str = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("AuthToken", str));
            HttpContext.Current.Session["AuthToken"] = str;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteException(exception.Message);
        }
    }

    public int ddlSelIndex(DropDownList ddl, string id)
    {
        int num = 0;
        int num1 = 0;
        while (num1 < ddl.Items.Count)
        {
            if (!ddl.Items[num1].Value.Trim().ToLower().Equals(id.Trim().ToLower()))
            {
                num1++;
            }
            else
            {
                num = num1;
                break;
            }
        }
        return num;
    }

    public int ddlSelIndexText(DropDownList ddl, string id)
    {
        int num = 0;
        int num1 = 0;
        while (num1 < ddl.Items.Count)
        {
            if (!ddl.Items[num1].Text.Trim().ToLower().Equals(id.Trim().ToLower()))
            {
                num1++;
            }
            else
            {
                num = num1;
                break;
            }
        }
        return num;
    }

    public int RadioSelIndex(RadioButtonList rbtn, string id)
    {
        int num = 0;
        int num1 = 0;
        while (num1 < rbtn.Items.Count)
        {
            if (!rbtn.Items[num1].Value.Trim().ToLower().Equals(id.Trim().ToLower()))
            {
                num1++;
            }
            else
            {
                num = num1;
                break;
            }
        }
        return num;
    }

    public int RadioSelIndexText(RadioButtonList rbtn, string id)
    {
        int num = 0;
        int num1 = 0;
        while (num1 < rbtn.Items.Count)
        {
            if (!rbtn.Items[num1].Text.Trim().ToLower().Equals(id.Trim().ToLower()))
            {
                num1++;
            }
            else
            {
                num = num1;
                break;
            }
        }
        return num;
    }

    public void SessionReset()
    {
        try
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.RemoveAll();
            if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMinutes(10);
            }
            if (HttpContext.Current.Request.Cookies["AuthToken"] != null)
            {
                HttpContext.Current.Response.Cookies["AuthToken"].Value = string.Empty;
                HttpContext.Current.Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMinutes(10);
            }
            HttpContext.Current.Response.Redirect("~/Login.aspx", true);
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteException(exception.Message);
        }
    }

    public void SetSessionCookie()
    {
        try
        {
            FormsAuthenticationTicket formsAuthenticationTicket = null;
            DateTime now = DateTime.Now;
            DateTime dateTime = DateTime.Now;
            formsAuthenticationTicket = new FormsAuthenticationTicket(1, "FERT_NEW", now, dateTime.AddMinutes(10), false, "");
            string str = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            HttpContext.Current.Session["AuthTokenPage"] = str;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteException(exception.Message);
        }
    }

    public void SetSessionCookieChildPage()
    {
        try
        {
            FormsAuthenticationTicket formsAuthenticationTicket = null;
            DateTime now = DateTime.Now;
            DateTime dateTime = DateTime.Now;
            formsAuthenticationTicket = new FormsAuthenticationTicket(1, "FERT_NEW_CHILD", now, dateTime.AddMinutes(10), false, "");
            string str = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            HttpContext.Current.Session["AuthTokenPageChild"] = str;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteException(exception.Message);
        }
    }
}
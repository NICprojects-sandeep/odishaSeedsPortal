using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePwdConfirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.ClearHeaders();
        base.Response.AppendHeader("Pragma", "no-cache");
        base.Response.AppendHeader("Pragma", "no-store");
        base.Response.AppendHeader("cache-control", "no-cache , no-store, private, must-revalidate");
        base.Response.AppendHeader("Expires", "-1");
        string str = "";
        str = base.Request.QueryString["v"].ToString();
        if (str != null && this.Session["AuthTokenPage"] != null && str == this.Session["AuthTokenPage"].ToString())
        {
            try
            {
                this.Session.Clear();
                this.Session.Abandon();
                this.Session.RemoveAll();
                if (base.Request.Cookies["ASP.NET_SessionId"] != null)
                {
                    base.Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                    base.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                    base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                }
                if (base.Request.Cookies["AuthToken"] != null)
                {
                    base.Response.Cookies["AuthToken"].Value = string.Empty;
                    base.Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                    base.Response.Cookies.Add(new HttpCookie("AuthToken", ""));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
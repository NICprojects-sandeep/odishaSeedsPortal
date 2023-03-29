using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BankLogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            try
            {
                this.Session.Clear();
                this.Session.Abandon();

            this.Session.RemoveAll();
                base.Response.Redirect("/stock/BankLogin.aspx", false);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
                ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
            }
    }
}
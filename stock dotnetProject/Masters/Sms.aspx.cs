using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class FMNEW_Sms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (txtMob.Text.Trim() == "")
        {
            string pop = "alert('Enter Mobile No. !!!.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
        if (txtMSG.Text.Trim() == "")
        {
            string pop = "alert('Type Message !!!.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        }
        dbsApp.SendSmsPOST("91" + txtMob.Text,txtMSG.Text.Replace("'"," "));

        txtMob.Text = string.Empty;
        txtMSG.Text = string.Empty;
        string pop1 = "alert('SMS Send Successfully !!!.');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
public partial class Admin_NicSystemManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void add_nic_system(object sender, EventArgs e)
    {
        bal obj = new bal();
        string systemID = systemidText.Value;
        string ceilingAmount = ceilingAmountText.Value;
        string requestPath = requestPathText.Value;
        string requestType = requestUsernameText.Value == ""? "local" :"ftp" ;
        string bankRequestPath = bankRequestPathText.Value;
        string transactionUrlPath = transactionUrlPathText.Value;
        string usernameReq = requestType == "ftp" ? requestUsernameText.Value : null;
        string passwordReq = requestType == "ftp" ? requestPasswordText.Value : null;
        string port = requestType == "ftp" ? reqport.Value : null;

        systemMaster myobj = new systemMaster();
        myobj.System_ID = systemID;
        myobj.Ceiling_Amount = Convert.ToDecimal(ceilingAmount);
        myobj.TID_Url = transactionUrlPath;
        myobj.RequestFolder_Path = requestPath;
        myobj.PathType = requestType;
        myobj.UserName = usernameReq;
        myobj.Password = passwordReq;
        myobj.Port = port;
        myobj.BankXml_Path = bankRequestPath;
        try
        {
            obj.addNicSystem(myobj);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ALERT", "alert('A New system has been added!'); location.reload();", true);
        }
        catch (Exception) {

        }
    }
}
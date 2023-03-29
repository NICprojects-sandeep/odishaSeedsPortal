using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddSystemAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        populateData();
        populateList();
    }

    public void populateList()
    {
        bal obj = new bal();
        var itemList = obj.getSystemwiseBankList();


        foreach (var item in itemList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.System_ID.ToString()+ item.Bank_Code.ToString()+ item.Account_Number.ToString(), "$(document).ready(function () { $('#myList').append('<tr><td>" + item.System_ID + "</td><td>" + item.Bank_Code + "</td><td>" + item.Account_Number + "</td></tr>') });", true);
        }
    }

    public void populateData()
    {
        bal obj = new bal();
        var systemList = obj.systemDetails();
        var bankList = obj.BankList();
        var distList = obj.DistrictList();

        foreach (var item in systemList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.System_ID, "$(document).ready(function () { $('#system-select').append('<option value=" + item.System_ID + ">" + item.System_ID + "</option>') });", true);
        }

        foreach (var item in bankList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.Bank_Code, "$(document).ready(function () { $('#bank-select').append('<option value=" + item.Bank_Code + ">" + item.Bank_Name + "</option>') });", true);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static void AddNewSystemAccount(string sysID, string bankID, string accountNo, string oBalance, string bankUrl, string type, string username, string password)
    {
        bal obj = new bal();
        SystemWiseAccounts myobj = new SystemWiseAccounts();
        Page page = HttpContext.Current.CurrentHandler as Page;
        myobj.System_ID = sysID;
        myobj.Bank_Code = bankID;
        myobj.Account_Number = accountNo;
        myobj.Opening_Balance = oBalance;
        myobj.BankUrl = bankUrl;
        myobj.Type = type;
        myobj.Username = username;
        myobj.Password = password;
        

        try
        {
            obj.AddSystemWiseAccounts(myobj);
        }
        catch (Exception ex)
        {
            
            page.Response.Write("<script>alert('The Operation was unsuccessful');</script>");
        }
    }
}
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddTopup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        populateData();
        populateList();
    }

    public void populateData()
    {
        bal obj = new bal();
        var systemList = obj.systemDetails();
        var bankList = obj.BankList();
        var distList = obj.DistrictList();

        foreach (var item in bankList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.Bank_Code, "$(document).ready(function () { $('#bank-select').append('<option value=" + item.Bank_Code + ">" + item.Bank_Name + "</option>') });", true);
        }
    }

    public void populateList()
    {
        bal obj = new bal();
        var itemList = obj.getTopupList();


        foreach (var item in itemList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.sl_no.ToString(), "$(document).ready(function () { $('#myList').append('<tr><td>" + item.Bank_Name + "</td><td>" + item.Account_Number + "</td><td>" + item.Topup_amount + "</td></tr>') });", true);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<systemWiseBank> populateAccountNo(string bankID)
    {
        bal obj = new bal();
        var AccountList = obj.getBankWiseAccounts(bankID);
        return AccountList;
        //foreach (var item in AccountList)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), item.System_ID, "$(document).ready(function () { $('#system-select').append('<option value=" + item.System_ID + ">" + item.System_ID + "</option>') });", true);
        //}
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static void topupAdd(string bankID, string accountNo, string amount)
    {
        bal obj = new bal();
        Page page = HttpContext.Current.CurrentHandler as Page;

        Topup myobj = new Topup();
        myobj.Bank_Name= obj.getBankName(bankID);
        myobj.Account_Number = accountNo;
        myobj.Topup_amount = Convert.ToDecimal(amount);

        try
        {
            obj.AddNewTopup(myobj);
        }
        catch (Exception ex)
        {
            
            page.Response.Write("<script>alert('The Operation was unsuccessful');</script>");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DistBankDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        populateData();
        populateList();
    }

    //Populating Selectors Data in the Page
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

        foreach (var item in distList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.Dist_Code, "$(document).ready(function () { $('#dist-select').append('<option value=" + item.Dist_Code + ">" + item.Dist_Name + "</option>') });", true);
        }
    }

    public void populateList()
    {
        bal obj = new bal();
        var itemList = obj.getDistBankList();
       

        foreach (var item in itemList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.System_ID + item.Bank_Code + item.Dist_Code, "$(document).ready(function () { $('#myList').append('<tr><td>" + item.System_ID + "</td><td>" + item.Bank_Code + "</td><td>" + item.Dist_Code + "</td></tr>') });", true);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static void AddDistrictWiseBank(string sysID, string bankID, string distCode)
    {
        bal obj = new bal();
        DistrictWiseBank myobj = new DistrictWiseBank();
        myobj.System_ID = sysID;
        myobj.Bank_Code = bankID;
        myobj.District_code = distCode;
        try
        {
            obj.AddDistWiseBank(myobj);
        }
        catch (Exception ex)
        {

        }

    }
}
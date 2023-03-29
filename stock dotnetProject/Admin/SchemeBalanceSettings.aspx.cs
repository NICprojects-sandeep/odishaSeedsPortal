using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SchemeBalanceSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        populateData();
    }

    public void populateData()
    {
        bal obj = new bal();
        var systemList = obj.systemDetails();

        foreach (var item in systemList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.System_ID, "$(document).ready(function () { $('#system-select').append('<option value=" + item.System_ID + ">" + item.System_ID + "</option>') });", true);
        }
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static void updateSlabAmount(string schemeCode, string slab)
    {
        bal obj = new bal();
        obj.updateSlab(schemeCode, Convert.ToDecimal(slab));

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<SchemeSettings> SystemWiseScheme(string sysID)
    {
        bal obj = new bal();
        Decimal systemTopups = 0;
        var systemAccounts = obj.systemBankDetails(sysID);
        List<SchemeSettings> mySchemeList = new List<SchemeSettings>();
        
        foreach (var systems in systemAccounts)
        {
            systemTopups = systemTopups + Convert.ToDecimal((obj.systemTopUps(systems.Account_Number).Sum(x => x.Topup_amount)));
        }
            var openingBalance = systemAccounts.Sum(s=> s.Opening_Balance).ToString();
            var totalAmount = (Convert.ToDecimal(openingBalance) + systemTopups).ToString();
            var balanceAmount = Convert.ToDecimal(totalAmount) - Convert.ToDecimal(obj.paymentAcceptedRequest(sysID).Sum(x => x.Control_Sum).ToString());
            var schemeList = obj.getSystemWiseScheme(sysID);

            foreach (var schemeItem in schemeList)
            {
                SchemeSettings schemeObj = new SchemeSettings();
                var schemeCode = schemeItem.Scheme_Code;
                var schemeName = schemeItem.Scheme_Name;
                var slabAmount = schemeItem.Slab_Amount;
                var approvedReqScheme = obj.getSchemeWiseAccepted(schemeCode).Sum(x => x.Control_Sum).ToString();
                var minValue = (Convert.ToDecimal(slabAmount) - Convert.ToDecimal(approvedReqScheme)).ToString();
                var maxValue = (Convert.ToDecimal(slabAmount) + Convert.ToDecimal(balanceAmount)).ToString();
                schemeObj.OpeningBalance = openingBalance;
                schemeObj.SchemeName = schemeItem.Scheme_Name;
                schemeObj.TotalAmount = totalAmount;
                schemeObj.BalanceAmount = balanceAmount;
                schemeObj.SchemeCode = schemeCode;
                schemeObj.SlabAmount = slabAmount;
                schemeObj.ApprovedAmount = approvedReqScheme;
                schemeObj.MaxValue = maxValue;
                schemeObj.MinValue = minValue;
                mySchemeList.Add(schemeObj);
            }
        return mySchemeList;
    }
}
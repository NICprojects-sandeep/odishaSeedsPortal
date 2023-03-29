using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        populateSystem();
        //serviceStatus();
    }
    public void populateSystem()
    {
        bal obj = new bal();
        var systemList = obj.systemDetails();
        foreach (var systems in systemList)
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa", "$(document).ready(function () { $('#selectorid').append('<option>" + systems.System_ID + "</option>') });", true);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<ServiceRunStatus> serviceStatus()
    {
        bal obj = new bal();
        var ServiceConfigsM = obj.getServerStatus();
        //string demo = ServiceConfigsM.Select(x => x.serviceTime).ToString() ;
        List<ServiceRunStatus> myobj = new List<ServiceRunStatus>();
        foreach (var item in ServiceConfigsM)
        {
            string ServiceRunningTime = item.serviceTime;

            string scheduleroneRunningTime = item.schedOneStat;
            string schedulertwoRunningTime = item.schedTwoStat;
            string schedulerthreeRunningTime = item.schedThreeStat;

            string serviceStat = (Convert.ToInt64(DateTime.Now.Subtract(Convert.ToDateTime(ServiceRunningTime)).TotalMilliseconds) < 6000) ? "True" : "False";
            string scOneStat = (Convert.ToInt64(DateTime.Now.Subtract(Convert.ToDateTime(scheduleroneRunningTime)).TotalMilliseconds) < 6000) ? "True" : "False";
            string scTwoStat = (Convert.ToInt64(DateTime.Now.Subtract(Convert.ToDateTime(schedulertwoRunningTime)).TotalMilliseconds) < 6000) ? "True" : "False";
            string scThreeStat = (Convert.ToInt64(DateTime.Now.Subtract(Convert.ToDateTime(schedulerthreeRunningTime)).TotalMilliseconds) < 6000) ? "True" : "False";



            ServiceRunStatus myList = new ServiceRunStatus();
            myList.ServiceStatus = serviceStat;
            myList.ServiceTime = ServiceRunningTime;
            myList.schedulerOneStatus = scOneStat;
            myList.schedulerOneTime = scheduleroneRunningTime;
            myList.schedulerTwoStatus = scTwoStat;
            myList.schedulerTwoTime = schedulertwoRunningTime;
            myList.schedulerThreeStatus = scThreeStat;
            myList.schedulerThreeTime = schedulerthreeRunningTime;
            myobj.Add(myList);

        }
        return myobj;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<SystemWiseGraphList> SchemeWiseGraph(string systemID)
    {
        bal obj = new bal();

        var SystemDetails = obj.systemDetails(systemID);
        List<SystemWiseGraphList> myObject = new List<SystemWiseGraphList>();
        foreach (var items in SystemDetails)
        {
            SystemWiseGraphList schemeList = new SystemWiseGraphList();
            string SID = items.System_ID;
            string openingBalance = items.Ceiling_Amount.ToString();

            string requestedPayment = obj.paymentRequest(systemID).Sum(s => s.Control_Sum).ToString();
            string acceptedRequests = obj.paymentAcceptedRequest(systemID).Sum(s => s.Control_Sum).ToString();
            string bankPaid = obj.responseAuthorization(systemID).Sum(s => s.TotalCost).ToString();

            string requestedPaymentCount = obj.paymentRequest(systemID).Count().ToString();
            string acceptedRequestsCount = obj.paymentAcceptedRequest(systemID).Count().ToString();
            string bankPaidCount = obj.responseAuthorization(systemID).Count().ToString();

            double totalBalance = Convert.ToDouble(openingBalance);
            double balanceAmount = totalBalance - Convert.ToDouble(requestedPayment);
            double tobePaid = Convert.ToDouble(requestedPayment) - Convert.ToDouble(bankPaid);

            schemeList.System_ID = systemID;
            schemeList.Opening_Balance = openingBalance;
            schemeList.Requested_Payment = requestedPayment;
            schemeList.Accepted_Payment = acceptedRequests;
            schemeList.Paid_By_Bank = bankPaid;
            schemeList.Requested_Payment_Count = requestedPaymentCount;
            schemeList.Accepted_Payment_Count = acceptedRequestsCount;
            schemeList.Paid_By_Bank_Count = bankPaidCount;
            schemeList.Total_Balance = totalBalance.ToString();
            schemeList.Balance_Amount = balanceAmount.ToString();
            schemeList.Amount_To_Be_Paid = tobePaid.ToString();


            myObject.Add(schemeList);
        }
        return myObject;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Model;
using System.Net.Sockets;
using System.Net;
/// <summary>
/// Summary description for bal
/// </summary>

//namespace BAS.Models
//{
public class bal
{
    //Retrives all the Inactive Request list
    BASEntities orm = new BASEntities();
    public List<Request_PaymentList_Header> getInactiveList()
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "0").OrderByDescending(s => s.Unique_Message_Id).OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    public List<Request_PaymentList_Header> getSchemeWiseInactiveList(string schemeRole)
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "0" && s.Scheme_Code == schemeRole).OrderByDescending(s => s.Unique_Message_Id).OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    public string getTransactionUrl(string sysID)
    {
        return orm.systemMasters.Where(s => s.System_ID == sysID).Select(g => g.TID_Url).FirstOrDefault();
    }

    public string getDistNameForBeneficiary(string distID)
    {
        return orm.PDS_DISTRICTMASTER.Where(s => s.int_DistrictID == distID).Select(g => g.vch_DistrictName).FirstOrDefault();
    }

    public string getBlockNameForBeneficiary(string blockID)
    {
        return orm.PDS_BLOCKMASTER.Where(s => s.int_BlockID== blockID).Select(g => g.vch_BlockName).FirstOrDefault();
    }

    //Retrives all the payment lists
    public List<Request_PaymentList_Header> getAllPaymentList()
    {
        return orm.Request_PaymentList_Header.OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    //Retrives schemeWise payment lists
    public List<Request_PaymentList_Header> getSchemeWisePaymentList(string schemeRole)
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "1" && s.Scheme_Code == schemeRole).OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    //Retrives schemeWise Approved request
    public List<Request_PaymentList_Header> getSchemeWiseApprovedList(string schemeRole)
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "3" && s.Scheme_Code == schemeRole).OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    //Retrives schemeWise Rejected request
    public List<Request_PaymentList_Header> getSchemeWiseRejectedList(string schemeRole)
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "4" && s.Scheme_Code == schemeRole).OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    //Retrives all the payment lists
    public List<Request_PaymentList_Header> getPaymentList()
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "1").OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    public string getBankName(string bankID)
    {
        return orm.bankMasters.Where(s => s.Bank_Code == bankID).Select(g => g.Bank_Name).FirstOrDefault();
    }



    //Retrives List of NIC Rejected request
    public List<Request_PaymentList_Header> getNicRejectedList()
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "2").OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    //Retrives List of Approved request
    public List<Request_PaymentList_Header> getApprovedList()
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "3").OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    //Retrives List of Rejected request
    public List<Request_PaymentList_Header> getRejectedList()
    {

        return orm.Request_PaymentList_Header.Where(s => s.File_Status == "4").OrderByDescending(s => s.Unique_Message_Id).ToList();
    }

    //Get Unique Message ID based payment requests
    public List<Request_PaymentList_Header> getPaymentList(string UmID)
    {
       
        return orm.Request_PaymentList_Header.Where(s => s.Unique_Message_Id == UmID).ToList();
    }

    //Retrives List of Payment requests made in between two dates
    public List<Request_PaymentList_Header> getPaymentList(DateTime stDate, DateTime EnDate)
    {
      
        return orm.Request_PaymentList_Header.Where(s => s.Message_Date_Time >= stDate && s.Message_Date_Time <= EnDate).ToList();
    }

    //Retrives List of Approved Payment requests made in between two dates
    public List<Request_PaymentList_Header> getPaymentListApproved(DateTime stDate, DateTime EnDate)
    {
     
        return orm.Request_PaymentList_Header.Where(s => s.Message_Date_Time >= stDate && s.Message_Date_Time <= EnDate && s.File_Status == "3").ToList();
    }

    public List<List> getPaymentListBank(DateTime stDate, DateTime EnDate)
    {

      
        return orm.Request_PaymentList_Header.Join(orm.Response_Payment_AuthorizationList_Header, umid => umid.Unique_Message_Id, umi => umi.Unique_Message_Id, (umid, umi) => new { umid, umi }).Where(s => s.umid.Message_Date_Time >= stDate && s.umid.Message_Date_Time <= EnDate && s.umid.File_Status == "3" && s.umid.Unique_Message_Id == s.umi.Unique_Message_Id).Select(m => new List { UmID = m.umid.Unique_Message_Id, TotalCost = m.umid.Control_Sum }).ToList();
    }

    public List<Request_PaymentList_Header> approvePayment(string UmID)
    { 
        var mDBContext = new BASEntities();
        string ip_address = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
        mDBContext.Configuration.ProxyCreationEnabled = false;
        //string hostName = Dns.GetHostName(); 
        var table = mDBContext.Request_PaymentList_Header.FirstOrDefault(s => s.Unique_Message_Id == UmID);
        table.File_Status = "3";
        table.AcceptedDate = DateTime.Now;
        table.ipAddress = ip_address;


        mDBContext.SaveChanges();

        return mDBContext.Request_PaymentList_Header.Where(s => s.Unique_Message_Id == UmID).ToList();
    }

    private IPAddress LocalIPAddress()
    {
        if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
        {
            return null;
        }

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        return host
            .AddressList
            .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
    }
    public decimal? getSlabAmount(string scode)
    {
        return orm.schemeMasters.Where(s => s.Scheme_Code == scode).Select(g => g.Slab_Amount).FirstOrDefault();
    }

    public List<Response_Payment_Authorization_Message> getBankPaid(string scode)
    {
        return orm.Response_Payment_Authorization_Message.Where(s => s.Scheme_Code == scode).ToList();
    }

    public List<Request_PaymentList_Header> getRequestPage(string UmID)
    {
       
        return orm.Request_PaymentList_Header.Where(s => s.Unique_Message_Id == UmID).ToList();
    }

    public List<FarmerDetails> getRequestDetailsPage(string UmID)
    {
        return orm.Request_PaymentList_Details.
             Join(orm.Accp_Farmer_Dtls, umid => umid.Scheme_Specific_Id, umi => umi.VCHFARMERCODE, (umid, umi) => new { umid, umi }).
             Where(s => s.umid.Unique_Message_Id == UmID).Select(m => new FarmerDetails { UMID = m.umid.Unique_Message_Id,FarmerID = m.umi.NICFARMERID, BenName = m.umi.VCHFARMERNAME, FatherName = m.umi.VCHFATHERNAME,DistID = m.umi.VCHDISTID, BlockID = m.umi.VCHBLOCKID, Account= m.umid.Bank_PostOffice_Account_number, Amount = m.umid.Total_Payment_Amount, Gender=m.umi.GENDER, Category=m.umi.CATEGOGY, Capacity=m.umid.FMN_CAPACITY,  Impliment= m.umid.FMN_IMPLIMENT,Subimpliment=m.umid.FMN_SUB_IMPLIMENT, TransactionID = m.umid.Unique_Credit_Transaction_Id }).ToList();
        //, TransactionID = m.umid.Unique_Credit_Transaction_Id
    }

    public List<Request_PaymentList_Header> getAllRequestPage(string scode)
    {
      
        return orm.Request_PaymentList_Header.Where(s => s.Scheme_Code == scode).ToList();
    }

    public List<Request_Bank_Wise_PaymentList_Header> getAllBankRequestPage(string UmID)
    {
       
        return orm.Request_Bank_Wise_PaymentList_Header.Where(s => s.Unique_Message_Id == UmID).ToList();
    }


    public void rejectPayment(string UmID)
    {
        
        var table = orm.Request_PaymentList_Header.FirstOrDefault(s => s.Unique_Message_Id == UmID);
        table.File_Status = "4";        
        orm.SaveChanges();
    }
    public void rejectPayment(string UmID, string response)
    {
       
        var table = orm.Request_Payment_Rejection.FirstOrDefault();

        var t = new Request_Payment_Rejection
        {
            Unique_Message_Id = UmID,
            Reson_of_rejection = response,
        };
        try
        {
            orm.Request_Payment_Rejection.Add(t);
            orm.SaveChanges();
        }
        catch (Exception ex) {
        }
    }

    public List<Request_Payment_Rejection> getRejectionReson(string UmID)
    {

        var table = new DataTable();
        return orm.Request_Payment_Rejection.Where(s => s.Unique_Message_Id == UmID).ToList();
    }

    public List<ServiceConfig> getErrorLogPath()
    {
       
        return orm.ServiceConfigs.Where(s => s.Action == "ErrorLog").ToList();
    }

    public List<schemeMaster> getSchemeMasterList(string scode)
    {
   
        return orm.schemeMasters.Where(s => s.Scheme_Code == scode).ToList();
    }

    public List<Topup> getSchemeTopupList(string scode)
    {
      
        return orm.Topups.Where(s => s.Account_Number == scode).ToList();
    }

    public List<Request_PaymentList_Header> getSchemeWiseRequestList(string scode)
    {
   
        return orm.Request_PaymentList_Header.Where(s => s.Scheme_Code == scode).ToList();
    }

    public List<Response_Payment_Authorization_List> getSchemeWiseAccList(string scode)
    {

        return orm.Response_Payment_Authorization_List.Where(s => s.Destination == scode).ToList();
    }

    public List<schemeMaster> getSchemeWiseAccList()
    {

        return orm.schemeMasters.ToList();
    }

    public List<allError> getErrorList(string umid)
    {
   
        return (from e in orm.allErrors
                where e.Unique_MsgID == umid
                select e).ToList();
    }

    public Boolean addUser(User myobj)
    {
        try
        {
            orm.Users.Add(myobj);
            orm.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public void addNicSystem(systemMaster obj)
    {
        orm.systemMasters.Add(obj);
        orm.SaveChanges();
    }

    public List<ServiceRunTimes> getServerStatus()
    {
        //var mDBContext = new BASEntities();
        var serviceStat = orm.ServiceConfigs.Where(s => s.Action == "Service is Running").Select(x => x.Date).Single().ToString();
        var schedOneStat = orm.ServiceConfigs.Where(s => s.Action == "SchdulingOne is Running").Select(x => x.Date).Single().ToString();
        var schedTwoStat = orm.ServiceConfigs.Where(s => s.Action == "SchdulingTwo is Running").Select(x => x.Date).Single().ToString();
        var schedThreeStat = orm.ServiceConfigs.Where(s => s.Action == "ThirdSchduling is Running").Select(x => x.Date).Single().ToString();

        List<ServiceRunTimes> myobj = new List<ServiceRunTimes>();
        ServiceRunTimes mylist = new ServiceRunTimes();
        mylist.serviceTime = serviceStat;
        mylist.schedOneStat = schedOneStat;
        mylist.schedTwoStat = schedTwoStat;
        mylist.schedThreeStat = schedThreeStat;
        myobj.Add(mylist);
        return myobj;
    }


    public List<systemMaster> systemDetails(string systemid)
    {
        return orm.systemMasters.Where(s => s.System_ID == systemid).ToList();
    }




    public List<schemeMaster> schemeName(string scheme_code)
    {
        return orm.schemeMasters.Where(s => s.Scheme_Code == scheme_code).ToList();
        // return orm.schemeMasters.
    }

    public List<Topup> schemeTopup(string scheme_code)
    {
        return orm.Topups.Where(s => s.Account_Number == scheme_code).ToList();
    }

    public List<Request_PaymentList_Header> paymentRequest(string sysid)
    {
        return orm.Request_PaymentList_Header.Where(s => s.Data_Source == sysid).ToList();
    }

    public string getSchemeOfficerRole(string soID)
    {
        return orm.schemeOfficerRoles.Where(s => s.UserID == soID).Select(g => g.Scheme_Code).FirstOrDefault();
    }

    public List<Request_PaymentList_Header> paymentAcceptedRequest(string sysid)
    {
        return orm.Request_PaymentList_Header.Where(s => s.Data_Source == sysid && s.File_Status == "3").ToList();
    }

    public List<List> responseAuthorization(string sysid)
    {
        return orm.Request_PaymentList_Header.Join(orm.Response_Payment_Authorization_List, umid => umid.Unique_Message_Id, umi => umi.Unique_Message_Id, (umid, umi) => new { umid, umi }).Where(s => s.umid.File_Status == "3" && s.umid.Unique_Message_Id == s.umi.Unique_Message_Id && s.umid.Data_Source == sysid).Select(m => new List { UmID = m.umid.Unique_Message_Id, TotalCost = m.umid.Control_Sum }).ToList();
    }

    //Retrive the list of Systems
    public List<systemMaster> systemDetails()
    {
        return orm.systemMasters.ToList();
    }

    //Retrive the list of Banks from the database
    public List<bankMaster> BankList()
    {
        return orm.bankMasters.ToList();
    }

    //Retrive the list of District from the database
    public List<districtMaster> DistrictList()
    {
        return orm.districtMasters.ToList();
    }

    //Retrive the list of District from the database
    public List<systemMaster> AccountsInBanksList()
    {
        return orm.systemMasters.ToList();
    }

    //Add Districtwise Bank
    public void AddDistWiseBank(DistrictWiseBank dwobj)
    {
        var obj = new districtWiseBank();
        obj.System_ID = dwobj.System_ID;
        obj.Bank_Code = dwobj.Bank_Code;
        obj.Dist_Code = dwobj.District_code;
        orm.districtWiseBanks.Add(obj);
        orm.SaveChanges();

    }

    //Add Districtwise Bank
    public void AddNewTopup(Topup dwobj)
    {
        orm.Topups.Add(dwobj);
        orm.SaveChanges();

    }

    //Add Account Number to Systems
    public void AddSystemWiseAccounts(SystemWiseAccounts dwobj)
    {
        var obj = new systemWiseBank();
        obj.System_ID = dwobj.System_ID;
        obj.Bank_Code = dwobj.Bank_Code;
        obj.Account_Number = dwobj.Account_Number;
        obj.Opening_Balance = Convert.ToDecimal(dwobj.Opening_Balance);
        obj.BankUrlPath = dwobj.BankUrl;
        obj.type = dwobj.Type;
        obj.UserName = dwobj.Username;
        obj.Password = dwobj.Password;
        orm.systemWiseBanks.Add(obj);
        orm.SaveChanges();
    }

    //Get accounts based on Banks
    public List<systemWiseBank> getBankWiseAccounts(string bankID)
    {
        return orm.systemWiseBanks.Where(s => s.Bank_Code == bankID).ToList();
    }

    //Get Schemes based on System IDs
    public List<schemeMaster> getSystemWiseScheme(string sysID)
    {
        return orm.schemeMasters.Where(s => s.System_ID == sysID).ToList();
    }

    public List<systemWiseBank> systemBankDetails(string sysID)
    {
        return orm.systemWiseBanks.Where(s => s.System_ID == sysID).ToList();
    }

    public List<Topup> systemTopUps(string accountNo)
    {

        return orm.Topups.Where(s => s.Account_Number == accountNo).ToList();
    }

    public List<Request_PaymentList_Header> getSchemeWiseAccepted(string sCode)
    {

        return orm.Request_PaymentList_Header.Where(s => s.Scheme_Code == sCode && s.File_Status == "3").ToList();
    }

    public List<schemeMaster> getSchemesinSystem(string sysCode)
    {
        orm.Configuration.ProxyCreationEnabled = false;
        return orm.schemeMasters.Where(s => s.System_ID == sysCode).ToList();
    }

    public void updateSlab(string scode, decimal slab)
    {

        orm.Configuration.ProxyCreationEnabled = false;
        orm.schemeMasters.FirstOrDefault(s => s.Scheme_Code == scode).Slab_Amount = slab;
        orm.SaveChanges();

    }

    public List<Topup> getTopupList()
    {

        orm.Configuration.ProxyCreationEnabled = false;
        return orm.Topups.OrderByDescending(x => x.sl_no).ToList();

    }

    public List<systemWiseBank> getSystemwiseBankList()
    {

        orm.Configuration.ProxyCreationEnabled = false;
        return orm.systemWiseBanks.ToList();

    }

    public List<districtWiseBank> getDistBankList()
    {

        orm.Configuration.ProxyCreationEnabled = false;
        return orm.districtWiseBanks.ToList();

    }
}
//}
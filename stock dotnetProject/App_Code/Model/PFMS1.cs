using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

/// <summary>
/// Summary description for PFMS1
/// </summary>
public class PFMSBAL1
{
    FARMERDBEntities pFMSEntities = new FARMERDBEntities();
    stocknewEntities1 StockEntities = new stocknewEntities1();

    public List<Dist> GetDistrict()
    {
        var lst = (from d in pFMSEntities.Tbl_Dist
                   select new Dist { DistName = d.Dist_Name, ShortName = d.SHORT_NAME }).ToList();
        return lst;
    }
    public List<string> GetFID(string shortName)
    {
        List<string> list = (from f in StockEntities.STOCK_FARMER
                             where f.FARMER_ID.StartsWith(shortName)
                             select f.FARMER_ID).Distinct().ToList();
        return list;
    }
    public List<VMStatus> GetAllStatus(string fid)
    {
        List<VMStatus> list = new List<VMStatus>();
        List<REPORT_STATUSDETAILS_Result> lst = pFMSEntities.REPORT_STATUSDETAILS(fid).ToList();

        string accNo = (from fr in pFMSEntities.M_FARMER_REGISTRATION
                        where fr.NICFARMERID == fid
                        select fr.VCHACCOUNTNO).FirstOrDefault();

        foreach (var v in lst)
        {
            VMStatus vm = new VMStatus();
            string[] s;
            if (v.RESPONSEBULK != null)
            {
                s = v.RESPONSEBULK.Split('/');
                vm.responseRecvStatus = s[0];
                vm.bulkStatus = s[1];
            }
            else
            {
                vm.responseRecvStatus = "NO";
                vm.bulkStatus = "NO";
            }

            vm.farmerID = v.FARMER_ID;
            vm.transactionCode = v.nTRANS;
            vm.accountNo = accNo;
            vm.reqMsgID = v.reqMsgID;
            vm.respMsgID = v.respMsgID;
            vm.transDate = v.DATES.ToString();
            vm.reqSent = v.request;

            //vm.responseRecvStatus = s[0];
            //vm.bulkStatus = s[1];
            vm.BankPaid = v.BankPaid;
            list.Add(vm);
        }
        return list;
    }
    public List<VMStatus> PendingAtNic(string fid)
    {
        List<VMStatus> list = new List<VMStatus>();
        List<REPORT_STATUSDETAILS_Result> lst = pFMSEntities.REPORT_STATUSDETAILS(fid).ToList();

        string accNo = (from fr in pFMSEntities.M_FARMER_REGISTRATION
                        where fr.NICFARMERID == fid
                        select fr.VCHACCOUNTNO).FirstOrDefault();

        foreach (var v in lst)
        {
            string[] s = v.RESPONSEBULK.Split('/');
            VMStatus vm = new VMStatus();

            vm.farmerID = v.FARMER_ID;
            vm.transactionCode = v.nTRANS;
            vm.accountNo = accNo;
            vm.reqMsgID = v.reqMsgID;
            vm.respMsgID = v.respMsgID;
            vm.transDate = v.DATES.ToString();
            vm.reqSent = v.request;
            vm.responseRecvStatus = s[0];
            vm.bulkStatus = s[1];
            vm.BankPaid = v.BankPaid;
            if (vm.reqSent == null)
            {
                list.Add(vm);
            }
        }
        return list;
    }
    public List<VMStatus> PendingAtDepartment(string fid)
    {
        List<VMStatus> list = new List<VMStatus>();
        List<REPORT_STATUSDETAILS_Result> lst = pFMSEntities.REPORT_STATUSDETAILS(fid).ToList();

        string accNo = (from fr in pFMSEntities.M_FARMER_REGISTRATION
                        where fr.NICFARMERID == fid
                        select fr.VCHACCOUNTNO).FirstOrDefault();

        foreach (var v in lst)
        {
            string[] s = v.RESPONSEBULK.Split('/');
            VMStatus vm = new VMStatus();

            vm.farmerID = v.FARMER_ID;
            vm.transactionCode = v.nTRANS;
            vm.accountNo = accNo;
            vm.reqMsgID = v.reqMsgID;
            vm.respMsgID = v.respMsgID;
            vm.transDate = v.DATES.ToString();
            vm.reqSent = v.request;
            vm.responseRecvStatus = s[0];
            vm.bulkStatus = s[1];
            vm.BankPaid = v.BankPaid;
            if (vm.reqSent != null && vm.responseRecvStatus != null)
            {
                list.Add(vm);
            }
        }
        return list;
    }
    public List<VMStatus> SentFromNicPendingAtPFMS(string fid)
    {
        List<VMStatus> list = new List<VMStatus>();
        List<REPORT_STATUSDETAILS_Result> lst = pFMSEntities.REPORT_STATUSDETAILS(fid).ToList();

        string accNo = (from fr in pFMSEntities.M_FARMER_REGISTRATION
                        where fr.NICFARMERID == fid
                        select fr.VCHACCOUNTNO).FirstOrDefault();

        foreach (var v in lst)
        {
            string[] s = v.RESPONSEBULK.Split('/');
            VMStatus vm = new VMStatus();

            vm.farmerID = v.FARMER_ID;
            vm.transactionCode = v.nTRANS;
            vm.accountNo = accNo;
            vm.reqMsgID = v.reqMsgID;
            vm.respMsgID = v.respMsgID;
            vm.transDate = v.DATES.ToString();
            vm.reqSent = v.request;
            vm.responseRecvStatus = s[0];
            vm.bulkStatus = s[1];
            vm.BankPaid = v.BankPaid;
            if (vm.reqSent != null && vm.responseRecvStatus == null)
            {
                list.Add(vm);
            }

        }
        return list;
    }

    //Account Details By Account No
    public List<RegDetails_M_Reg> GetRegDetails(string accNo, string mobNo)
    {
        List<RegDetails_M_Reg> regDetails_M_Reg = new List<RegDetails_M_Reg>();

        if (accNo != "" && mobNo != "")
        {
            regDetails_M_Reg = (from r in pFMSEntities.M_FARMER_REGISTRATION
                                where (r.VCHACCOUNTNO==accNo || r.VCHACCOUNTNO==("0"+accNo) || r.VCHACCOUNTNO==("00"+accNo) || r.VCHACCOUNTNO==("000"+accNo)) && r.VCHMOBILENO == mobNo
                                select new RegDetails_M_Reg
                                {
                                    // nicFarmerID = r.NICFARMERID,
                                    farmerCode = r.VCHFARMERCODE,
                                    farmerName = r.VCHFARMERNAME,
                                    fatherName = r.VCHFATHERNAME,
                                    mobNo = r.VCHMOBILENO,
                                    accountNo = r.VCHACCOUNTNO,
                                    bankName = (from b in pFMSEntities.Tbl_Bank
                                                where b.Bank_Code == r.INTBANKID.ToString()
                                                select b.Bank_Name).FirstOrDefault(),
                                    branchName = (from b in pFMSEntities.Tbl_Branch
                                                  where b.Branch_Code == r.INTBRANCHID.ToString()
                                                  select b.Branch_Name).FirstOrDefault(),
                                    IFSCCode = r.VCHIFSCCODE,
                                    pfmsStatus = r.CHAPFMSSTATUS
                                }).ToList();


        }
        else if (accNo != "" && mobNo == "")
        {
            regDetails_M_Reg = (from r in pFMSEntities.M_FARMER_REGISTRATION
                                where (r.VCHACCOUNTNO == accNo || r.VCHACCOUNTNO == ("0" + accNo) || r.VCHACCOUNTNO == ("00" + accNo) || r.VCHACCOUNTNO == ("000" + accNo))
                                select new RegDetails_M_Reg
                                {// nicFarmerID = r.NICFARMERID,
                                    farmerCode = r.VCHFARMERCODE,
                                    farmerName = r.VCHFARMERNAME,
                                    fatherName = r.VCHFATHERNAME,
                                    mobNo = r.VCHMOBILENO,
                                    accountNo = r.VCHACCOUNTNO,
                                    bankName = (from b in pFMSEntities.Tbl_Bank
                                                where b.Bank_Code == r.INTBANKID.ToString()
                                                select b.Bank_Name).FirstOrDefault(),
                                    branchName = (from b in pFMSEntities.Tbl_Branch
                                                  where b.Branch_Code == r.INTBRANCHID.ToString()
                                                  select b.Branch_Name).FirstOrDefault(),
                                    IFSCCode = r.VCHIFSCCODE,
                                    pfmsStatus = r.CHAPFMSSTATUS
                                }).ToList();


        }
        else if (accNo == "" && mobNo != "")
        {
            regDetails_M_Reg = (from r in pFMSEntities.M_FARMER_REGISTRATION
                                where r.VCHMOBILENO == mobNo
                                select new RegDetails_M_Reg
                                {
                                    // nicFarmerID = r.NICFARMERID,
                                    farmerCode = r.VCHFARMERCODE,
                                    farmerName = r.VCHFARMERNAME,
                                    fatherName = r.VCHFATHERNAME,
                                    mobNo = r.VCHMOBILENO,
                                    accountNo = r.VCHACCOUNTNO,
                                    bankName = (from b in pFMSEntities.Tbl_Bank
                                                where b.Bank_Code == r.INTBANKID.ToString()
                                                select b.Bank_Name).FirstOrDefault(),
                                    branchName = (from b in pFMSEntities.Tbl_Branch
                                                  where b.Branch_Code == r.INTBRANCHID.ToString()
                                                  select b.Branch_Name).FirstOrDefault(),
                                    IFSCCode = r.VCHIFSCCODE,
                                    pfmsStatus = r.CHAPFMSSTATUS
                                }).ToList();


        }
        return regDetails_M_Reg;
    }
    public List<ReqSentDetails> GetReqSentDetailsData(string accNo, string mobNo)
    {
        List<ReqSentDetails> reqSentDetails = new List<ReqSentDetails>();

        if (accNo != "" && mobNo != "")
        {
            reqSentDetails = (from r in pFMSEntities.Request_tbl_Beneficiary_Message
                              where (r.Bank_Account_number == accNo || r.Bank_Account_number == ("0" + accNo) || r.Bank_Account_number == ("00" + accNo) || r.Bank_Account_number == ("000" + accNo)) && r.Mobile_number == mobNo
                             // where r.Bank_Account_number.EndsWith(accNo) && r.Mobile_number == mobNo
                              select new ReqSentDetails
                              {

                                  uniqmsgID = r.Unique_Message_Id,
                                  Name = r.Beneficiary_Name,
                                  fatherName = r.Father_Mother_name,
                                  schemeSpecificID = r.Scheme_Specific_ID,
                                  IFSCCode = r.IFSC_Code,
                                  accountNo = r.Bank_Account_number,
                                  mobNo = r.Mobile_number
                              }).ToList();
        }
        else if (accNo != "" && mobNo == "")
        {
            reqSentDetails = (from r in pFMSEntities.Request_tbl_Beneficiary_Message
                              where (r.Bank_Account_number == accNo || r.Bank_Account_number == ("0" + accNo) || r.Bank_Account_number == ("00" + accNo) || r.Bank_Account_number == ("000" + accNo))
                              select new ReqSentDetails
                              {

                                  uniqmsgID = r.Unique_Message_Id,
                                  Name = r.Beneficiary_Name,
                                  fatherName = r.Father_Mother_name,
                                  schemeSpecificID = r.Scheme_Specific_ID,
                                  IFSCCode = r.IFSC_Code,
                                  accountNo = r.Bank_Account_number,
                                  mobNo = r.Mobile_number
                              }).ToList();
        }
        else if (accNo == "" && mobNo != "")
        {
            reqSentDetails = (from r in pFMSEntities.Request_tbl_Beneficiary_Message
                              where r.Mobile_number == mobNo
                              select new ReqSentDetails
                              {

                                  uniqmsgID = r.Unique_Message_Id,
                                  Name = r.Beneficiary_Name,
                                  fatherName = r.Father_Mother_name,
                                  schemeSpecificID = r.Scheme_Specific_ID,
                                  IFSCCode = r.IFSC_Code,
                                  accountNo = r.Bank_Account_number,
                                  mobNo = r.Mobile_number
                              }).ToList();
        }
        return reqSentDetails;
    }
    public List<F_ApproveDetails> GetFarmerDtls(string accountNo, string mobNo)
    {
        //F_ApproveDetails f_ApproveDetails = new F_ApproveDetails();
        List<F_ApproveDetails> farmer = new List<F_ApproveDetails>();
        if (accountNo != "" && mobNo != "")
        {
            farmer = (from fr in pFMSEntities.M_FARMER_REGISTRATION
                      join fa in pFMSEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals fa.Farmer_Code
                      where (fr.VCHACCOUNTNO == accountNo || fr.VCHACCOUNTNO == ("0" + accountNo) || fr.VCHACCOUNTNO == ("00" + accountNo) || fr.VCHACCOUNTNO == ("000" + accountNo)) && fr.VCHMOBILENO == mobNo && (fa.Status == "ACCP" || fa.Status == "CCB")
                      select new F_ApproveDetails
                      {

                          fID = fr.NICFARMERID,
                          FCode = fr.VCHFARMERCODE,
                          //Name = fr.VCHFARMERNAME,
                          //fatherName = fr.VCHFATHERNAME
                          status = fa.Status

                      }).ToList();
        }

        else if (accountNo != "" && mobNo == "")
        {
            farmer = (from fr in pFMSEntities.M_FARMER_REGISTRATION
                      join fa in pFMSEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals fa.Farmer_Code
                      where (fr.VCHACCOUNTNO == accountNo || fr.VCHACCOUNTNO == ("0" + accountNo) || fr.VCHACCOUNTNO == ("00" + accountNo) || fr.VCHACCOUNTNO == ("000" + accountNo)) && (fa.Status == "ACCP" || fa.Status == "CCB")
                      select new F_ApproveDetails
                      {

                          fID = fr.NICFARMERID,
                          FCode = fr.VCHFARMERCODE,
                          //Name = fr.VCHFARMERNAME,
                          //fatherName = fr.VCHFATHERNAME
                          status = fa.Status

                      }).ToList();
        }

        else if (accountNo == "" && mobNo != "")
        {
            farmer = (from fr in pFMSEntities.M_FARMER_REGISTRATION
                      join fa in pFMSEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals fa.Farmer_Code
                      where fr.VCHMOBILENO == mobNo && (fa.Status == "ACCP" || fa.Status == "CCB")
                      select new F_ApproveDetails
                      {

                          fID = fr.NICFARMERID,
                          FCode = fr.VCHFARMERCODE,
                          //Name = fr.VCHFARMERNAME,
                          //fatherName = fr.VCHFATHERNAME
                          status = fa.Status

                      }).ToList();
        }
        return farmer;
    }
    public AccountDetailsList GetFarmerDtlsNotApproved(string accountNo, string mobNo)
    {

        AccountDetailsList accountDetailsList = new AccountDetailsList();

        if (accountNo != "" && mobNo != "")
        {
            accountDetailsList.RBAM = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                       join rbam in pFMSEntities.Response_tbl_Beneficiary_Ack_Message on f.VCHFARMERCODE equals rbam.Scheme_Specific_ID
                                       where (f.VCHACCOUNTNO == accountNo || f.VCHACCOUNTNO == ("0" + accountNo) || f.VCHACCOUNTNO == ("00" + accountNo) || f.VCHACCOUNTNO == ("000" + accountNo)) && f.VCHMOBILENO == mobNo
                                       select new F_NotInApprove
                                       {
                                           fID = f.NICFARMERID,
                                           fCode = f.VCHFARMERCODE,
                                           // fName = f.VCHFARMERNAME,
                                           status = rbam.Beneficiary_Status,
                                           reason = rbam.Rejection_Reason_Narration
                                       }).ToList();

            accountDetailsList.RBAMD = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                        join rbamd in pFMSEntities.Response_tbl_Beneficiary_Ack_Message_D on f.VCHFARMERCODE equals rbamd.Scheme_Specific_ID
                                        where (f.VCHACCOUNTNO == accountNo || f.VCHACCOUNTNO == ("0" + accountNo) || f.VCHACCOUNTNO == ("00" + accountNo) || f.VCHACCOUNTNO == ("000" + accountNo)) && f.VCHMOBILENO == mobNo
                                        select new F_NotInApprove
                                        {
                                            fID = f.NICFARMERID,
                                            fCode = f.VCHFARMERCODE,
                                            //fName = f.VCHFARMERNAME,
                                            status = rbamd.Beneficiary_Status,
                                            reason = rbamd.Rejection_Reason_Narration
                                        }).ToList();

            accountDetailsList.RBAMD2 = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                         join rbamd2 in pFMSEntities.Response_tbl_Beneficiary_Ack_Message_D2 on f.VCHFARMERCODE equals rbamd2.Scheme_Specific_ID
                                         where (f.VCHACCOUNTNO == accountNo || f.VCHACCOUNTNO == ("0" + accountNo) || f.VCHACCOUNTNO == ("00" + accountNo) || f.VCHACCOUNTNO == ("000" + accountNo)) && f.VCHMOBILENO == mobNo
                                         select new F_NotInApprove
                                         {
                                             fID = f.NICFARMERID,
                                             fCode = f.VCHFARMERCODE,
                                             // fName = f.VCHFARMERNAME,
                                             status = rbamd2.Beneficiary_Status,
                                             reason = rbamd2.Rejection_Reason_Narration
                                         }).ToList();
        }

        else if (accountNo != "" && mobNo == "")
        {
            accountDetailsList.RBAM = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                       join rbam in pFMSEntities.Response_tbl_Beneficiary_Ack_Message on f.VCHFARMERCODE equals rbam.Scheme_Specific_ID
                                       where (f.VCHACCOUNTNO == accountNo || f.VCHACCOUNTNO == ("0" + accountNo) || f.VCHACCOUNTNO == ("00" + accountNo) || f.VCHACCOUNTNO == ("000" + accountNo))
                                       select new F_NotInApprove
                                       {
                                           fID = f.NICFARMERID,
                                           fCode = f.VCHFARMERCODE,
                                           // fName = f.VCHFARMERNAME,
                                           status = rbam.Beneficiary_Status,
                                           reason = rbam.Rejection_Reason_Narration
                                       }).ToList();

            accountDetailsList.RBAMD = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                        join rbamd in pFMSEntities.Response_tbl_Beneficiary_Ack_Message_D on f.VCHFARMERCODE equals rbamd.Scheme_Specific_ID
                                        where (f.VCHACCOUNTNO == accountNo || f.VCHACCOUNTNO == ("0" + accountNo) || f.VCHACCOUNTNO == ("00" + accountNo) || f.VCHACCOUNTNO == ("000" + accountNo))
                                        select new F_NotInApprove
                                        {
                                            fID = f.NICFARMERID,
                                            fCode = f.VCHFARMERCODE,
                                            //fName = f.VCHFARMERNAME,
                                            status = rbamd.Beneficiary_Status,
                                            reason = rbamd.Rejection_Reason_Narration
                                        }).ToList();

            accountDetailsList.RBAMD2 = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                         join rbamd2 in pFMSEntities.Response_tbl_Beneficiary_Ack_Message_D2 on f.VCHFARMERCODE equals rbamd2.Scheme_Specific_ID
                                         where (f.VCHACCOUNTNO == accountNo || f.VCHACCOUNTNO == ("0" + accountNo) || f.VCHACCOUNTNO == ("00" + accountNo) || f.VCHACCOUNTNO == ("000" + accountNo))
                                         select new F_NotInApprove
                                         {
                                             fID = f.NICFARMERID,
                                             fCode = f.VCHFARMERCODE,
                                             //fName = f.VCHFARMERNAME,
                                             status = rbamd2.Beneficiary_Status,
                                             reason = rbamd2.Rejection_Reason_Narration
                                         }).ToList();
        }
        else if (accountNo == "" && mobNo != "")
        {
            accountDetailsList.RBAM = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                       join rbam in pFMSEntities.Response_tbl_Beneficiary_Ack_Message on f.VCHFARMERCODE equals rbam.Scheme_Specific_ID
                                       where f.VCHMOBILENO == mobNo
                                       select new F_NotInApprove
                                       {
                                           fID = f.NICFARMERID,
                                           fCode = f.VCHFARMERCODE,
                                           //fName = f.VCHFARMERNAME,
                                           status = rbam.Beneficiary_Status,
                                           reason = rbam.Rejection_Reason_Narration
                                       }).ToList();

            accountDetailsList.RBAMD = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                        join rbamd in pFMSEntities.Response_tbl_Beneficiary_Ack_Message_D on f.VCHFARMERCODE equals rbamd.Scheme_Specific_ID
                                        where f.VCHMOBILENO == mobNo
                                        select new F_NotInApprove
                                        {
                                            fID = f.NICFARMERID,
                                            fCode = f.VCHFARMERCODE,
                                            //fName = f.VCHFARMERNAME,
                                            status = rbamd.Beneficiary_Status,
                                            reason = rbamd.Rejection_Reason_Narration
                                        }).ToList();

            accountDetailsList.RBAMD2 = (from f in pFMSEntities.M_FARMER_REGISTRATION
                                         join rbamd2 in pFMSEntities.Response_tbl_Beneficiary_Ack_Message_D2 on f.VCHFARMERCODE equals rbamd2.Scheme_Specific_ID
                                         where f.VCHMOBILENO == mobNo
                                         select new F_NotInApprove
                                         {
                                             fID = f.NICFARMERID,
                                             fCode = f.VCHFARMERCODE,
                                             //fName = f.VCHFARMERNAME,
                                             status = rbamd2.Beneficiary_Status,
                                             reason = rbamd2.Rejection_Reason_Narration
                                         }).ToList();
        }

        return accountDetailsList;
    }

  
    //Farmer Details
    public List<VMBeneficiaryResponseListDuplicate> GetBenificiaryAccpResponse()
    {
        List<VMBeneficiaryResponseListDuplicate> list = new List<VMBeneficiaryResponseListDuplicate>();
        var listResponse = pFMSEntities.View_BenificiaryAccpResponse.ToList();

        foreach (var x in listResponse)
        {
            int cnt = (from ap in pFMSEntities.Tbl_FarmerApprove
                       where ap.Farmer_Code == x.VCHFARMERCODE && (ap.Status=="ACCP" || ap.Status=="CCB" || ap.Status=="AAO" || ap.Status=="AAO1" || ap.Status=="AAOC")
                       select ap).Count();

           
                VMBeneficiaryResponseListDuplicate vm = new VMBeneficiaryResponseListDuplicate();
                vm.accountNo = x.VCHACCOUNTNO;
                vm.farmerName = x.VCHFARMERNAME;
                vm.fatherName = x.VCHFATHERNAME;
                vm.accountHolderName = x.VCHACCHOLDERNM;
                vm.farmerCode = x.VCHFARMERCODE;
                if (cnt > 0)
                {
                    vm.farmerID = x.NICFARMERID;
                }
                else
                {
                    vm.farmerID = "";
                }
                vm.IFSCCode = x.VCHIFSCCODE;

                list.Add(vm);
            
        }

        return list;
    }
}
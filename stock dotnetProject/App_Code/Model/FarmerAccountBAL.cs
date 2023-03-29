using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;


/// <summary>
/// Summary description for FarmerAccountBAL
/// </summary>
public class FarmerAccountBAL
{
    FARMERDBEntities fARMERDBEntities = new FARMERDBEntities();
    stocknewEntities1 StockEntities = new stocknewEntities1();

    public VMFarmerInformation GetFarAccountInfo(string voterNo, string accNo)
    {
        VMFarmerInformation vMFarmerInformation = new VMFarmerInformation();

        //if (GetCount(aaoCd, voterNo) > 0)
        //{
        if (voterNo != "")
        {
            vMFarmerInformation = (from f in fARMERDBEntities.M_FARMER_REGISTRATION
                                   where f.VCHVOTERIDCARDNO == voterNo
                                   select new VMFarmerInformation
                                   {
                                       farmerName = f.VCHFARMERNAME,
                                       fatherName = f.VCHFATHERNAME,
                                       distName = (from d in fARMERDBEntities.Tbl_Dist
                                                   where d.NDist_Code == f.VCHDISTID
                                                   select d.Dist_Name).FirstOrDefault(),
                                       blockName = (from b in fARMERDBEntities.blocks
                                                    where b.Nblock_code == f.VCHBLOCKID
                                                    select b.block_name).FirstOrDefault(),
                                       GP = (from g in fARMERDBEntities.Tbl_GP
                                             where g.GP_Code == f.VCHGPID
                                             select g.GP_Name).FirstOrDefault(),
                                       villName = (from v in fARMERDBEntities.Villages
                                                   where v.villg_code == f.VCHVILLAGEID
                                                   select v.villg_name).FirstOrDefault(),
                                       mobNo = f.VCHMOBILENO,
                                       voterID = f.VCHVOTERIDCARDNO,

                                       accNo = f.VCHACCOUNTNO,
                                       accHolderName = f.VCHACCHOLDERNM,
                                       bankName = (from b in fARMERDBEntities.Tbl_Bank
                                                   where b.Bank_Code == f.INTBANKID.ToString()
                                                   select b.Bank_Name).FirstOrDefault(),
                                       branchName = (from b in fARMERDBEntities.Tbl_Branch
                                                     where b.Branch_Code == f.INTBRANCHID.ToString()
                                                     select b.Branch_Name).FirstOrDefault(),
                                       IFSC = f.VCHIFSCCODE
                                   }).FirstOrDefault();
        }
        else if (accNo != "")
        {
            vMFarmerInformation = (from f in fARMERDBEntities.M_FARMER_REGISTRATION
                                   where f.VCHACCOUNTNO == accNo
                                   select new VMFarmerInformation
                                   {
                                       farmerName = f.VCHFARMERNAME,
                                       fatherName = f.VCHFATHERNAME,
                                       distName = (from d in fARMERDBEntities.Tbl_Dist
                                                   where d.NDist_Code == f.VCHDISTID
                                                   select d.Dist_Name).FirstOrDefault(),
                                       blockName = (from b in fARMERDBEntities.blocks
                                                    where b.Nblock_code == f.VCHBLOCKID
                                                    select b.block_name).FirstOrDefault(),
                                       GP = (from g in fARMERDBEntities.Tbl_GP
                                             where g.GP_Code == f.VCHGPID
                                             select g.GP_Name).FirstOrDefault(),
                                       villName = (from v in fARMERDBEntities.Villages
                                                   where v.villg_code == f.VCHVILLAGEID
                                                   select v.villg_name).FirstOrDefault(),
                                       mobNo = f.VCHMOBILENO,
                                       voterID = f.VCHVOTERIDCARDNO,

                                       accNo = f.VCHACCOUNTNO,
                                       accHolderName = f.VCHACCHOLDERNM,
                                       bankName = (from b in fARMERDBEntities.Tbl_Bank
                                                   where b.Bank_Code == f.INTBANKID.ToString()
                                                   select b.Bank_Name).FirstOrDefault(),
                                       branchName = (from b in fARMERDBEntities.Tbl_Branch
                                                     where b.Branch_Code == f.INTBRANCHID.ToString()
                                                     select b.Branch_Name).FirstOrDefault(),
                                       IFSC = f.VCHIFSCCODE
                                   }).FirstOrDefault();
        }
        // }
        return vMFarmerInformation;
    }


    //public FarmerStatus GetStatusOfFarmer(string voterID)
    //{
    //    FarmerStatus farmerStatus = new FarmerStatus();

    //    farmerStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
    //                   join ap in fARMERDBEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals ap.Farmer_Code
    //                    where fr.VCHVOTERIDCARDNO == voterID
    //                   select new FarmerStatus { 
    //                       // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
    //                   status=ap.Status,
    //                   farID=fr.NICFARMERID
    //                   }).FirstOrDefault();

    //    if ( farmerStatus == null)
    //    {
    //        farmerStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
    //                        join am in fARMERDBEntities.Response_tbl_Beneficiary_Ack_Message on fr.VCHFARMERCODE equals am.Scheme_Specific_ID
    //                        where fr.VCHVOTERIDCARDNO == voterID
    //                        select new FarmerStatus
    //                        {
    //                            status = am.Beneficiary_Status,
    //                            reason = am.Rejection_Reason_Narration
    //                        }).FirstOrDefault();


    //    }

    //    else if (farmerStatus.status != "ACCP" || farmerStatus.status != "CCB" || farmerStatus.status != "UIID" || farmerStatus.status != "RJCT")
    //    {
    //        farmerStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
    //                        join ap in fARMERDBEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals ap.Farmer_Code
    //                        where (ap.Status == "AAO" || ap.Status == "AAO1") && fr.VCHVOTERIDCARDNO == voterID
    //                        select new FarmerStatus
    //                        {
    //                            status = ap.Status,
    //                            Message = "Waiting For Approval From AAO/AAO1"
    //                        }).FirstOrDefault();
    //    }
    //    return farmerStatus;
    //}

    public FarmerApproveStatus GetApproveStatus(string voterID, string accNo)
    {
        FarmerApproveStatus farmerApproveStatus = new FarmerApproveStatus();
        //if (GetCount(aaoCd, voterID) > 0)
        //{
        if (voterID != "")
        {
            farmerApproveStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                   join ap in fARMERDBEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals ap.Farmer_Code
                                   where fr.VCHVOTERIDCARDNO == voterID
                                   select new FarmerApproveStatus
                                   {
                                       // && (ap.Status=="ACCP" || ap.Status=="CCB")
                                       status = ap.Status,
                                       farID = fr.NICFARMERID
                                   }).FirstOrDefault();

            //}
        }
        else if (accNo != "")
        {
            farmerApproveStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                   join ap in fARMERDBEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals ap.Farmer_Code
                                   where fr.VCHACCOUNTNO == accNo && (ap.Status == "ACCP" || ap.Status == "CCB")
                                   select new FarmerApproveStatus
                                   {
                                       //  && (ap.Status=="ACCP" || ap.Status=="CCB")
                                       status = ap.Status,
                                       farID = fr.NICFARMERID
                                   }).FirstOrDefault();
        }
        return farmerApproveStatus;
    }

    public int GetAAOAAO1Status(string voterID, string accNo)
    {
        int count = 0;
        //if (GetCount(aaoCd, voterID) > 0)
        //{
        if (voterID != "")
        {
            count = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                     join ap in fARMERDBEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals ap.Farmer_Code
                     where (ap.Status == "AAO" || ap.Status == "AAO1") && fr.VCHVOTERIDCARDNO == voterID
                     select ap).Count();
        }
        // }
        else if (accNo != "")
        {
            count = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                     join ap in fARMERDBEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals ap.Farmer_Code
                     where (ap.Status == "AAO" || ap.Status == "AAO1") && fr.VCHACCOUNTNO == accNo
                     select ap).Count();
        }
        return count;
    }

    public FarmerRejectStatus GetRejectStatus(string voterID, string accNo)
    {
        FarmerRejectStatus farmerRejectStatus = new FarmerRejectStatus();
        int check = 0;
        int check1 = 1;
        //if (GetCount(aaoCd, voterID) > 0)
        //{
        if (voterID != "")
        {
            farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                  join bm in fARMERDBEntities.Response_tbl_Beneficiary_Ack_Message on fr.VCHFARMERCODE equals bm.Scheme_Specific_ID
                                  where fr.VCHVOTERIDCARDNO == voterID && (bm.Beneficiary_Status == "ACCP" || bm.Beneficiary_Status == "RJCT")
                                  select new FarmerRejectStatus
                                  {
                                      // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                      Name = bm.Beneficiary_Name,
                                      status = bm.Beneficiary_Status,
                                      reason = bm.Rejection_Reason_Narration,
                                      AccNo = bm.Bank_Account_Number
                                  }).FirstOrDefault();
            if (farmerRejectStatus == null)
            {
                check = 1;
                farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                      join bm in fARMERDBEntities.Response_tbl_Beneficiary_Ack_Message_D on fr.VCHFARMERCODE equals bm.Scheme_Specific_ID
                                      where fr.VCHVOTERIDCARDNO == voterID && (bm.Beneficiary_Status == "ACCP" || bm.Beneficiary_Status == "RJCT")
                                      select new FarmerRejectStatus
                                      {
                                          // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") &&    
                                          Name = bm.Beneficiary_Name,
                                          status = bm.Beneficiary_Status,
                                          reason = bm.Rejection_Reason_Narration,
                                          AccNo = bm.Bank_Account_Number
                                      }).FirstOrDefault();
            }
            else if (farmerRejectStatus == null && check == 1)
            {
                check1 = 2;
                farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                      join bm in fARMERDBEntities.Response_tbl_Beneficiary_Ack_Message_D2 on fr.VCHFARMERCODE equals bm.Scheme_Specific_ID
                                      where fr.VCHVOTERIDCARDNO == voterID && (bm.Beneficiary_Status == "ACCP" || bm.Beneficiary_Status == "RJCT")
                                      select new FarmerRejectStatus
                                      {
                                          // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                          Name = bm.Beneficiary_Name,
                                          status = bm.Beneficiary_Status,
                                          reason = bm.Rejection_Reason_Narration,
                                          AccNo = bm.Bank_Account_Number
                                      }).FirstOrDefault();
            }
            else if (farmerRejectStatus == null && check == 1 && check1 == 2)
            {
                farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                      join ccb1 in fARMERDBEntities.Response_CCB1 on fr.VCHFARMERCODE equals ccb1.Scheme_Specific_ID
                                      where fr.VCHVOTERIDCARDNO == voterID
                                      select new FarmerRejectStatus
                                      {
                                          // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                          Name = ccb1.NAMEINCBS,
                                          status = "Accepted",
                                          reason = "Accepted by bank but department rejected.",
                                          AccNo = ""
                                      }).FirstOrDefault();
            }
        }
        else if (accNo != "")
        {
            farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                  join bm in fARMERDBEntities.Response_tbl_Beneficiary_Ack_Message on fr.VCHFARMERCODE equals bm.Scheme_Specific_ID
                                  where fr.VCHACCOUNTNO == accNo && (bm.Beneficiary_Status == "ACCP" || bm.Beneficiary_Status == "RJCT")
                                  select new FarmerRejectStatus
                                  {
                                      // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                      Name = bm.Beneficiary_Name,
                                      status = bm.Beneficiary_Status,
                                      reason = bm.Rejection_Reason_Narration,
                                      AccNo = bm.Bank_Account_Number
                                  }).FirstOrDefault();
            if (farmerRejectStatus == null)
            {
                check = 1;
                farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                      join bm in fARMERDBEntities.Response_tbl_Beneficiary_Ack_Message_D on fr.VCHFARMERCODE equals bm.Scheme_Specific_ID
                                      where fr.VCHACCOUNTNO == accNo && (bm.Beneficiary_Status == "ACCP" || bm.Beneficiary_Status == "RJCT")
                                      select new FarmerRejectStatus
                                      {
                                          // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") &&    
                                          Name = bm.Beneficiary_Name,
                                          status = bm.Beneficiary_Status,
                                          reason = bm.Rejection_Reason_Narration,
                                          AccNo = bm.Bank_Account_Number
                                      }).FirstOrDefault();
            }
            else if (farmerRejectStatus == null && check == 1)
            {
                check1 = 2;
                farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                      join bm in fARMERDBEntities.Response_tbl_Beneficiary_Ack_Message_D2 on fr.VCHFARMERCODE equals bm.Scheme_Specific_ID
                                      where fr.VCHACCOUNTNO == accNo && (bm.Beneficiary_Status == "ACCP" || bm.Beneficiary_Status == "RJCT")
                                      select new FarmerRejectStatus
                                      {
                                          // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                          Name = bm.Beneficiary_Name,
                                          status = bm.Beneficiary_Status,
                                          reason = bm.Rejection_Reason_Narration,
                                          AccNo = bm.Bank_Account_Number
                                      }).FirstOrDefault();
            }
            else if (farmerRejectStatus == null && check == 1 && check1 == 2)
            {
                farmerRejectStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                      join ccb1 in fARMERDBEntities.Response_CCB1 on fr.VCHFARMERCODE equals ccb1.Scheme_Specific_ID
                                      where fr.VCHACCOUNTNO == accNo
                                      select new FarmerRejectStatus
                                      {
                                          // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                          Name = ccb1.NAMEINCBS,
                                          status = "Accepted",
                                          reason = "Accepted by bank but department rejected.",
                                          AccNo = ""
                                      }).FirstOrDefault();
            }
        }
        //}
        return farmerRejectStatus;
    }

    //public int GetFarmerCode(string voterID)
    //{
    //    return (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
    //            where fr.VCHVOTERIDCARDNO == voterID
    //            select fr.INTFARMERID).FirstOrDefault();
    //}

}
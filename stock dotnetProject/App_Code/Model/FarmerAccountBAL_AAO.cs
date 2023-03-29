using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

/// <summary>
/// Summary description for FarmerAccountBAL_AAO
/// </summary>
public class FarmerAccountBAL_AAO
{
    FARMERDBEntities fARMERDBEntities = new FARMERDBEntities();
    stocknewEntities1 StockEntities = new stocknewEntities1();

    int count1 = 0;
    int count2 = 0;

    public int GetCount(string aaoCode, string voterNo, string accNo)
    {
        string bID = "";
        var blk = (from ja in StockEntities.JALANIDHI_DAO_AAO
                   where ja.AAO_CODE == aaoCode
                   select new{blkCode = ja.BLK_CODE}).ToArray();

        var blockCode = (from b in blk
                         join pd in fARMERDBEntities.PDS_BLOCKMASTER on b.blkCode equals pd.NIC_BlockCode
                         select new {blkID= pd.int_BlockID }).ToList();

        if (voterNo != "")
        {
            bID = (from b in fARMERDBEntities.M_FARMER_REGISTRATION
                          where b.VCHVOTERIDCARDNO == voterNo
                          select b.VCHBLOCKID).FirstOrDefault();

            count2 = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                      join fu in fARMERDBEntities.NIC_farmer_updated_by_aao on fr.VCHFARMERCODE equals fu.VCHFARMERCODE
                      where fr.VCHVOTERIDCARDNO==voterNo
                      select fu).Count();

        }
        else if (accNo != "")
        {
            bID = (from b in fARMERDBEntities.M_FARMER_REGISTRATION
                   where b.VCHACCOUNTNO == accNo
                   select b.VCHBLOCKID).FirstOrDefault();

            count2 = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                      join fu in fARMERDBEntities.NIC_farmer_updated_by_aao on fr.VCHFARMERCODE equals fu.VCHFARMERCODE
                      where fr.VCHACCOUNTNO == accNo
                      select fu).Count();
        }
        if (bID != "")
        {
            count1 = (from b in blockCode
                      where b.blkID == bID
                      select b).Count();
        }
        
        if (count1 != 0 && count2 == 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public VMFarmerInformation GetFarAccountInfo(string voterNo,string accNo)
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
                                       blockName = (from b in fARMERDBEntities.PDS_BLOCKMASTER
                                                    where b.int_BlockID == f.VCHBLOCKID
                                                    select b.vch_BlockName).FirstOrDefault(),
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
                                       blockName = (from b in fARMERDBEntities.PDS_BLOCKMASTER
                                                    where b.int_BlockID == f.VCHBLOCKID
                                                    select b.vch_BlockName).FirstOrDefault(),                                                    
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

    public FarmerApproveStatus GetApproveStatus(string voterID,string accNo)
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
                                       // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                       status = ap.Status,
                                       farID = fr.NICFARMERID
                                   }).FirstOrDefault();
            //}
        }
        else if (accNo != "")
        {
            farmerApproveStatus = (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                                   join ap in fARMERDBEntities.Tbl_FarmerApprove on fr.VCHFARMERCODE equals ap.Farmer_Code
                                   where fr.VCHACCOUNTNO == accNo
                                   select new FarmerApproveStatus
                                   {
                                       // (ap.Status == "ACCP" || ap.Status == "CCB" || ap.Status == "UIID") && 
                                       status = ap.Status,
                                       farID = fr.NICFARMERID
                                   }).FirstOrDefault();
        }
        return farmerApproveStatus;
    }

    public int GetAAOAAO1Status(string voterID,string accNo)
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

    public FarmerRejectStatus GetRejectStatus(string voterID,string accNo)
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
                                      reason = bm.Rejection_Reason_Narration
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
                                          reason = bm.Rejection_Reason_Narration
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
                                          reason = bm.Rejection_Reason_Narration
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
                                          reason = "Accepted by bank but department rejected."
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
                                      reason = bm.Rejection_Reason_Narration
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
                                          reason = bm.Rejection_Reason_Narration
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
                                          reason = bm.Rejection_Reason_Narration
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
                                          reason = "Accepted by bank but department rejected."
                                      }).FirstOrDefault();
            }
        }
        //}
        return farmerRejectStatus;
    }

    public int GetFarmerCode(string voterID)
    {
        return (from fr in fARMERDBEntities.M_FARMER_REGISTRATION
                 where fr.VCHVOTERIDCARDNO == voterID || fr.VCHACCOUNTNO == voterID 
                select fr.INTFARMERID).FirstOrDefault();
    }

}
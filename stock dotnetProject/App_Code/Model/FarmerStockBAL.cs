using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

/// <summary>
/// Summary description for FarmerStockBAL
/// </summary>
public class FarmerStockBAL
{
    stocknewEntities1 StockEntities = new stocknewEntities1();
    FARMERDBEntities fARMERDBEntities = new FARMERDBEntities();

    public string GetDistCode(string aao)
    {
        return (from j in StockEntities.JALANIDHI_DAO_AAO
                where j.AAO_CODE == aao
                select j.DDA_NAME.Substring(0, 3)).FirstOrDefault();
    }

    //public int GetCount(string aaoCode, string fID)
    //{
    //    int count1 = 0;
    //    string bID = "";
    //    var blk = (from ja in StockEntities.JALANIDHI_DAO_AAO
    //               where ja.AAO_CODE == aaoCode
    //               select new { blkCode = ja.BLK_CODE }).ToArray();

    //    var blockCode = (from b in blk
    //                     join pd in fARMERDBEntities.PDS_BLOCKMASTER on b.blkCode equals pd.NIC_BlockCode
    //                     select new { blkID = pd.int_BlockID }).ToList();

    //    if (fID != "")
    //    {
    //        bID = (from b in fARMERDBEntities.M_FARMER_REGISTRATION
    //               where b.NICFARMERID == fID
    //               select b.VCHBLOCKID).FirstOrDefault();

    //    }
       
    //    if (bID != "")
    //    {
    //        count1 = (from b in blockCode
    //                  where b.blkID == bID
    //                  select b).Count();
    //    }

    //    return count1;
    //}

    public List<VMPaymentStatus> GetStockDetails(string farID)
    {
        List<VMPaymentStatus> list = new List<VMPaymentStatus>();

        var lstStock = (from s in StockEntities.STOCK_FARMER
                        where s.FARMER_ID == farID && s.SUBSIDY_AMOUNT != 0
                        select s).ToList();
        if (lstStock.Count != 0)
        {
            foreach (var x in lstStock)
            {
                List<VMPaymentStatusProp> innerList = new List<VMPaymentStatusProp>();
                VMPaymentStatus vmlst = new VMPaymentStatus();
                var lstReq = (from rqpt in fARMERDBEntities.Request_tbl_Payment_List
                              where ((rqpt.Unique_Credit_Transaction_Id == (x.TRANSACTION_ID + "2054")) || (rqpt.Unique_Credit_Transaction_Id == (x.TRANSACTION_ID + "9140")) || (rqpt.Unique_Credit_Transaction_Id == (x.TRANSACTION_ID + "9143")) || (rqpt.Unique_Credit_Transaction_Id == (x.TRANSACTION_ID + "9145")))
                              select rqpt).ToList();

                if (lstReq.Count != 0)
                {
                    foreach (var v in lstReq)
                    {
                        VMPaymentStatusProp vm = new VMPaymentStatusProp();
                        vm.uniqtransID = v.Unique_Credit_Transaction_Id;
                        vm.transDate = x.UPDATED_ON.ToString();
                        vm.farmerName = (from f in fARMERDBEntities.M_FARMER_REGISTRATION
                                         where f.NICFARMERID == farID
                                         select f.VCHFARMERNAME).FirstOrDefault();
                        vm.bagSize = x.BAG_SIZE_KG ?? 0;
                        vm.noOfBags = x.NO_OF_BAGS ?? 0;
                        vm.totQTL = x.TOT_QTL ?? 0;
                        vm.amount = (from rqpt in fARMERDBEntities.Request_tbl_Payment_List
                                     where rqpt.Unique_Credit_Transaction_Id.StartsWith(v.Unique_Credit_Transaction_Id)
                                     select rqpt.Total_Payment_Amount).FirstOrDefault() ?? 0;

                        int countSentToBank = (from rspm in fARMERDBEntities.Response_tbl_Paymemt_Ack_Message
                                               where rspm.Original_End_to_End_Id.StartsWith(v.Unique_Credit_Transaction_Id) && rspm.Record_Level_Status == "ACPT"
                                               select rspm).Count();
                        if (countSentToBank != 0)
                        {
                            vm.status = "Sent to bank";
                            //(from rspm in fARMERDBEntities.Response_tbl_Paymemt_Ack_Message
                            //         where rspm.Original_End_to_End_Id.StartsWith(v.TRANSACTION_ID)
                            //         select rspm.Record_Level_Status).FirstOrDefault();
                        }
                        else
                        {
                            vm.status = "Pending";
                        }

                        int countPaid = (from rspam in fARMERDBEntities.Response_tbl_Payment_Authorization_Message
                                         where rspam.Unique_Credit_Transaction_Id.StartsWith(v.Unique_Credit_Transaction_Id) && rspam.Record_Status == "ACCP"
                                         select rspam).Count();
                        if (countPaid != 0)
                        {
                            vm.paidStatus = "Paid";
                        }
                        else
                        {
                            vm.paidStatus =(from rspam in fARMERDBEntities.Response_tbl_Payment_Authorization_Message
                                             where rspam.Unique_Credit_Transaction_Id.StartsWith(v.Unique_Credit_Transaction_Id) && rspam.Record_Status == "RJCT"
                                             select rspam.Rejection_Narration).FirstOrDefault();
                            if (vm.paidStatus == null)
                            {
                                vm.paidStatus = "";
                            }
                        }

                        //(from rspam in fARMERDBEntities.Response_tbl_Payment_Authorization_Message
                        //             where rspam.Unique_Credit_Transaction_Id.StartsWith(v.TRANSACTION_ID)
                        //             select rspam.Record_Status).FirstOrDefault();

                        innerList.Add(vm);

                    }
                }

                else if (lstReq.Count == 0)
                {
                    var lstReqCCB = (from c in fARMERDBEntities.Request_Payment_tblTransaction
                                     where ((c.TransactionID == (x.TRANSACTION_ID + "2054")) || (c.TransactionID == (x.TRANSACTION_ID + "9140")) || (c.TransactionID == (x.TRANSACTION_ID + "9143")) || (c.TransactionID == (x.TRANSACTION_ID + "9145")))
                                     select c).ToList();
                    if (lstReqCCB.Count != 0)
                    {
                        foreach (var v in lstReqCCB)
                        {
                            VMPaymentStatusProp vm = new VMPaymentStatusProp();
                            vm.uniqtransID = v.TransactionID;
                            vm.transDate = x.UPDATED_ON.ToString();
                            vm.farmerName = (from f in fARMERDBEntities.M_FARMER_REGISTRATION
                                             where f.NICFARMERID == farID
                                             select f.VCHFARMERNAME).FirstOrDefault();
                            vm.bagSize = x.BAG_SIZE_KG ?? 0;
                            vm.noOfBags = x.NO_OF_BAGS ?? 0;
                            vm.totQTL = x.TOT_QTL ?? 0;
                            vm.amount = v.Amount??0;
                                //(from rqpt in fARMERDBEntities.Request_Payment_tblTransaction
                                //         where rqpt.TransactionID.StartsWith(v.TransactionID)
                                //         select rqpt.Amount).FirstOrDefault() ?? 0;

                            int countSentToBank =  (from rspm in fARMERDBEntities.Request_Payment_tblTransaction
                                                   where rspm.TransactionID.StartsWith(v.TransactionID)
                                                   select rspm).Count();
                           
                                
                            if (countSentToBank != 0)
                            {
                                vm.status = "Sent to CCB";
                                //(from rspm in fARMERDBEntities.Response_tbl_Paymemt_Ack_Message
                                //         where rspm.Original_End_to_End_Id.StartsWith(v.TRANSACTION_ID)
                                //         select rspm.Record_Level_Status).FirstOrDefault();
                            }
                            else
                            {
                                vm.status = "Pending";
                            }

                            int countPaid = (from rspam in fARMERDBEntities.Response_Payment_tblTransaction
                                             where rspam.TransactionID.StartsWith(v.TransactionID) && rspam.PStatus=="A"
                                             select rspam).Count();
                            if (countPaid != 0)
                            {
                                vm.paidStatus = "Paid";
                            }
                            else
                            {
                                vm.paidStatus = "";
                            }

                            //(from rspam in fARMERDBEntities.Response_tbl_Payment_Authorization_Message
                            //             where rspam.Unique_Credit_Transaction_Id.StartsWith(v.TRANSACTION_ID)
                            //             select rspam.Record_Status).FirstOrDefault();

                            innerList.Add(vm);

                        }
                    }
                    else
                    {
                        VMPaymentStatusProp vm = new VMPaymentStatusProp();
                        vm.uniqtransID = x.TRANSACTION_ID;
                        vm.transDate = x.UPDATED_ON.ToString();
                        vm.farmerName = (from f in fARMERDBEntities.M_FARMER_REGISTRATION
                                         where f.NICFARMERID == farID
                                         select f.VCHFARMERNAME).FirstOrDefault();
                        vm.bagSize = x.BAG_SIZE_KG ?? 0;
                        vm.noOfBags = x.NO_OF_BAGS ?? 0;
                        vm.totQTL = x.TOT_QTL ?? 0;
                        vm.amount = x.SUBSIDY_AMOUNT ?? 0;
                        vm.status = "Not sent to PFMS OR CCB";
                        vm.paidStatus = "";

                        innerList.Add(vm);

                    }
                   
                }
                vmlst.lstPaymentStatus = innerList.ToList();
                vmlst.transNo = x.TRANSACTION_ID;
                list.Add(vmlst);
            }

        }
        return list;
    }

}
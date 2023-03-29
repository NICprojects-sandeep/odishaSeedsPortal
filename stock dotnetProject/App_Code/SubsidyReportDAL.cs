using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubsidyReportDAL
/// </summary>
public class SubsidyReportDAL
{
    public SubsidyReportDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetDataMaster()
    {
        DataTable dt = new DataTable();
        string query = "select * from [FARMERDB].[dbo].[FinancialSubsidy]";
        dt = DbConfigue.GetTableDetails(query);
        return dt;
    }

    public DataTable GetDataViewR(string uniqueid)
    {
        DataTable dt = new DataTable();
        string query = "";
        if (uniqueid.Substring(0, 4) == "0025")
        {
            query = "select b.NICFARMERID FID,a.Beneficiary_Name Name,a.Bank_Post_Office_Name bank_name,c.vchBranchName branch_name,a.BankPost_Office_Account_number accno," +
                    "left(a.Unique_Credit_Transaction_Id,len(a.Unique_Credit_Transaction_Id) - 4) tranid,e.UPDATED_ON Date, a.Total_Payment_Amount amt, "+
                    "f.Scheme_Name scheme,null sts,null res from [FARMERDB].[dbo].[Request_tbl_Payment_List] a inner join [FARMERDB].[dbo].[M_FARMER_REGISTRATION] b on " +
                    "a.Scheme_Specific_Id=b.VCHFARMERCODE inner join [FARMERDB].[dbo].[M_PDS_BANKBRANCH] c on b.INTBRANCHID=c.intBranchId inner join stock.dbo.STOCK_FARMER e on " +
                    "left(a.Unique_Credit_Transaction_Id,len(a.Unique_Credit_Transaction_Id)-4)=e.TRANSACTION_ID collate SQL_Latin1_General_CP1_CI_AS inner join "+
                    "[FARMERDB].[dbo].[Scheme_Master] f on f.Scheme_Code=right(a.Unique_Credit_Transaction_Id,4) where a.Unique_Message_Id='" + uniqueid + "'";
        }
        else
        {
            query = "select b.FarmerID FID,b.FarmerName Name,a.BankName bank_name,a.BranchName branch_name,b.AccountNumber accno,left(a.TransactionID,len(a.TransactionID)-4) tranid,e.UPDATED_ON Date,b.Amount amt," +
                   "f.Scheme_Name scheme,null sts,null res from [FARMERDB].[dbo].[Response_Payment_tblTransaction] a inner join [FARMERDB].[dbo].[Request_Payment_tblTransaction] b on " +
                   "a.TransactionID=b.TransactionID inner join stock.dbo.STOCK_FARMER e on left(a.TransactionID,len(a.TransactionID)-4)=e.TRANSACTION_ID collate " +
                   "SQL_Latin1_General_CP1_CI_AS inner join [FARMERDB].[dbo].[Scheme_Master] f on f.Scheme_Code=right(a.TransactionID,4) where a.CPAID='" + uniqueid + "'";
        }
        dt = DbConfigue.GetTableDetails(query);
        return dt;
    }

    public DataTable GetDataView(string uniqueid)
    {
        DataTable dt = new DataTable();
        string query = "";
        if (uniqueid.Substring(0, 4) == "0025")
        {
            query = "select c.NICFARMERID FID,a.Beneficiary_Name Name,a.Bank_Post_Office_Name bank_name,d.vchBranchName branch_name,a.Bank_Account_number accno,left(a.Unique_Credit_Transaction_Id," +
                           "len(a.Unique_Credit_Transaction_Id)-4) tranid,e.UPDATED_ON Date,a.Total_Payment_Amount amt,f.Scheme_Name scheme,a.Record_Status sts,null res from " +
                           "[FARMERDB].[dbo].[Response_tbl_Payment_Authorization_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Credit_Transaction_Id=" +
                           "b.Unique_Credit_Transaction_Id inner join [FARMERDB].[dbo].[M_FARMER_REGISTRATION] c on b.Scheme_Specific_Id=c.VCHFARMERCODE inner join " +
                           "[FARMERDB].[dbo].[M_PDS_BANKBRANCH] d on c.INTBRANCHID=d.intBranchId inner join stock.dbo.STOCK_FARMER e on left(a.Unique_Credit_Transaction_Id," +
                           "len(a.Unique_Credit_Transaction_Id)-4)=e.TRANSACTION_ID collate SQL_Latin1_General_CP1_CI_AS inner join [FARMERDB].[dbo].[Scheme_Master] f on " +
                           "f.Scheme_Code=right(a.Unique_Credit_Transaction_Id,4) where b.Unique_Message_Id='" + uniqueid + "' and a.Record_Status='ACCP'";
        }
        else
        {
            query = "select b.FarmerID FID,b.FarmerName Name,a.BankName bank_name,a.BranchName branch_name,b.AccountNumber accno,left(a.TransactionID,len(a.TransactionID)-4) tranid,e.UPDATED_ON Date,b.Amount amt," +
                   "f.Scheme_Name scheme,a.PStatus sts,null res from [FARMERDB].[dbo].[Response_Payment_tblTransaction] a inner join [FARMERDB].[dbo].[Request_Payment_tblTransaction] b on " +
                   "a.TransactionID=b.TransactionID inner join stock.dbo.STOCK_FARMER e on left(a.TransactionID,len(a.TransactionID)-4)=e.TRANSACTION_ID collate " +
                   "SQL_Latin1_General_CP1_CI_AS inner join [FARMERDB].[dbo].[Scheme_Master] f on f.Scheme_Code=right(a.TransactionID,4) where a.CPAID='" + uniqueid + "' and a.PStatus='A'";
        }
        dt = DbConfigue.GetTableDetails(query);
        return dt;
    }

    public DataTable GetDataViewRJ(string uniqueid)
    {
        DataTable dt = new DataTable();
        string query = "";
        if (uniqueid.Substring(0, 4) == "0025")
        {
            query = "select c.NICFARMERID FID,a.Beneficiary_Name Name,a.Bank_Post_Office_Name bank_name,d.vchBranchName branch_name,a.Bank_Account_number accno,left(a.Unique_Credit_Transaction_Id," +
                           "len(a.Unique_Credit_Transaction_Id)-4) tranid,e.UPDATED_ON Date,a.Total_Payment_Amount amt,f.Scheme_Name scheme,a.Record_Status sts,a.Rejection_Narration res from " +
                           "[FARMERDB].[dbo].[Response_tbl_Payment_Authorization_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Credit_Transaction_Id=" +
                           "b.Unique_Credit_Transaction_Id inner join [FARMERDB].[dbo].[M_FARMER_REGISTRATION] c on b.Scheme_Specific_Id=c.VCHFARMERCODE inner join " +
                           "[FARMERDB].[dbo].[M_PDS_BANKBRANCH] d on c.INTBRANCHID=d.intBranchId inner join stock.dbo.STOCK_FARMER e on left(a.Unique_Credit_Transaction_Id," +
                           "len(a.Unique_Credit_Transaction_Id)-4)=e.TRANSACTION_ID collate SQL_Latin1_General_CP1_CI_AS inner join [FARMERDB].[dbo].[Scheme_Master] f on " +
                           "f.Scheme_Code=right(a.Unique_Credit_Transaction_Id,4) where b.Unique_Message_Id='" + uniqueid + "' and a.Record_Status='RJCT'";
        }
        else
        {
            query = "select b.FarmerID FID,b.FarmerName Name,a.BankName bank_name,a.BranchName branch_name,b.AccountNumber accno,left(a.TransactionID,len(a.TransactionID)-4) tranid,e.UPDATED_ON Date,b.Amount amt," +
                   "f.Scheme_Name scheme,a.PStatus sts,null res from [FARMERDB].[dbo].[Response_Payment_tblTransaction] a inner join [FARMERDB].[dbo].[Request_Payment_tblTransaction] b on " +
                   "a.TransactionID=b.TransactionID inner join stock.dbo.STOCK_FARMER e on left(a.TransactionID,len(a.TransactionID)-4)=e.TRANSACTION_ID collate " +
                   "SQL_Latin1_General_CP1_CI_AS inner join [FARMERDB].[dbo].[Scheme_Master] f on f.Scheme_Code=right(a.TransactionID,4) where a.CPAID='" + uniqueid + "' and a.PStatus='R'";
        }
        dt = DbConfigue.GetTableDetails(query);
        return dt;
    }
}
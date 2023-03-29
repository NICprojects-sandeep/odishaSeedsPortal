using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubsidyReportDAL
/// </summary>
public class FMDAL
{
    public FMDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetDataMaster(string farmerid)
    {
        DataTable dt = new DataTable();
        string query = "select  VCHFARMERNAME, VCHFATHERNAME,e.[Gender_Value],f.Category_Value,VCHAADHARNO,VCHVOTERIDCARDNO,  VCHACCHOLDERNM, VCHACCOUNTNO, c.vchBankName, d.vchBranchName, d.NEW_IFSC_CODE,lv.Dist_Code LGD_District_Code,lv.Village_Code LGD_Village_Code from [FARMERDB].[dbo].[M_FARMER_REGISTRATION] a inner join [FARMERDB].[dbo].[Tbl_FarmerApprove] b on a.VCHFARMERCODE=b.Farmer_Code inner join [FARMERDB].[dbo].[M_PDS_FARMERBANK] c on a.INTBANKID=c.intId inner join [FARMERDB].[dbo].[M_PDS_BANKBRANCH] d on a.INTBRANCHID=d.intBranchId inner join [FARMERDB].[dbo].[Tbl_Gender] e on a.INTGENDER=e.Gender_ID inner join [FARMERDB].[dbo].[Tbl_Category] f on a.[INTCATEGOGY]=f.Category_ID   inner join [FARMERDB].[dbo].[PDS_VILLAGEMASTER] v on v.int_VillageID=a.VCHVILLAGEID inner join [FARMERDB].[dbo].[LGVillage] lv on lv.Village_Code=v.lgvillage_code  where b.Status in('ACCP','CCB') and NICFARMERID='" + farmerid + "'";
        dt = DbConfigue.GetTableDetails(query);
        return dt;
    }
    internal DataTable GetDataScheemwise(string SchemeCode)
    {
        string[] paramAcc = { "@SCHEME_CODE_SP" };
        object[] valueAcc = { SchemeCode };
        string Procedure = "RPT_FinanceData";
        DataSet dsAcc = dbsApp.param_passing_fetchDataSetSP(Procedure, paramAcc, valueAcc);
        return dsAcc.Tables[0];
    }
    internal DataTable GetDataDBTbeneficiary2016_17()
    {
        string[] paramAcc = { };
        object[] valueAcc = { };
        string Procedure = "DBT_beneficiary2016-17";
        DataSet dsAcc = dbsFarmer.param_passing_fetchDataSetSP(Procedure, paramAcc, valueAcc);
        return dsAcc.Tables[0];
    }

    internal DataTable GetDBT_beneficiary2017_18()
    {
        string[] paramAcc = { };
        object[] valueAcc = { };
        string Procedure = "DBT_beneficiary2017-18";
        DataSet dsAcc = dbsFarmer.param_passing_fetchDataSetSP(Procedure, paramAcc, valueAcc);
        return dsAcc.Tables[0];
    }
    internal DataTable WEB_FarmerConsolidated2017_18()
    {
        string[] paramAcc = { };
        object[] valueAcc = { };
        string Procedure = "WEB_FarmerConsolidated";
        DataSet dsAcc = dbsAppStock.param_passing_fetchDataSetSP(Procedure, paramAcc, valueAcc);
        return dsAcc.Tables[0];
    }
    internal DataTable WEB_FarmerConsolidated2018_19()
    {
        string[] paramAcc = { };
        object[] valueAcc = { };
        string Procedure = "WEB_FarmerConsolidated_18_19";
        DataSet dsAcc = dbsAppStock.param_passing_fetchDataSetSP(Procedure, paramAcc, valueAcc);
        return dsAcc.Tables[0];
    }
}
public class Datareturn
{
    public string VCHDISTID { get; set; }
    public string VCHBLOCKID { get; set; }
    
}
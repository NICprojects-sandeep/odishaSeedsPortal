using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubsidyReportBLL
/// </summary>
public class FMBLL
{
    FMDAL dal = new FMDAL();
    public FMBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetDataMaster(string farmerid)
    {
        return dal.GetDataMaster(farmerid);
    }
    internal DataTable GetDataScheemwise(string SchemeCode)
    {
        return dal.GetDataScheemwise(SchemeCode);
    }
    internal DataTable GetDBT_beneficiary2016_17()
    {
        return dal.GetDataDBTbeneficiary2016_17();
    }

    internal DataTable GetDBT_beneficiary2017_18()
    {
        return dal.GetDBT_beneficiary2017_18();
    }
    internal DataTable WEB_FarmerConsolidated2017_18()
    {
        return dal.WEB_FarmerConsolidated2017_18();
    }
    internal DataTable WEB_FarmerConsolidated2018_19()
    {
        return dal.WEB_FarmerConsolidated2018_19();
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubsidyReportBLL
/// </summary>
public class SubsidyReportBLL
{
    SubsidyReportDAL dal = new SubsidyReportDAL();
    public SubsidyReportBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetDataMaster()
    {
        return dal.GetDataMaster();
    }
    public DataTable GetDataViewR(string uniqueid)
    {
        return dal.GetDataViewR(uniqueid);
    }
    public DataTable GetDataView(string uniqueid)
    {
        return dal.GetDataView(uniqueid);
    }
    public DataTable GetDataViewRJ(string uniqueid)
    {
        return dal.GetDataViewRJ(uniqueid);
    }
}
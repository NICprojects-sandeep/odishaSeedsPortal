using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ApprovedDeptBLL
/// </summary>
public class ApprovedDeptBLL
{
    ApprovedDeptDAL dal = new ApprovedDeptDAL();
    public ApprovedDeptBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetDataDeptApp()
    {
        return dal.GetDataDeptApp();
    }
}
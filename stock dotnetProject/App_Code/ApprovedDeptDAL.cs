using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ApprovedDeptDAL
/// </summary>
public class ApprovedDeptDAL
{
    public ApprovedDeptDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetDataDeptApp()
    {
        DataTable dt = new DataTable();
        string query = "select * from [FARMERDB].[dbo].[Department_Approve_Table]";
        dt = DbConfigue.GetTableDetails(query);
        return dt;
    }
}
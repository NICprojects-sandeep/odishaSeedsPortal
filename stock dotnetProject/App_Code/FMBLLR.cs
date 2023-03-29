using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FMBLL
/// </summary>
public class FMBLLR
{
    FMDALR dal = new FMDALR();
    public FMBLLR()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetErrroName(string sysid)
    {
        return dal.GetErrroName(sysid);
    }
    public DataTable GetDetails(string narration)
    {
        return dal.GetDetails(narration);
    }
}
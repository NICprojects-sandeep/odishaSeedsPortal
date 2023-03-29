﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable GetDataMaster(string farmerid)
    {
        FMBLL bll = new FMBLL();
        DataTable dt = bll.GetDataMaster(farmerid);
        //Datareturn[] dts = new Datareturn[dt.Rows.Count];
        //for (int i=0;i<dt.Rows.Count;i++)
        //{
        //    var dtss = new Datareturn();
        //    dtss.VCHDISTID = dt.Rows[i]["VCHDISTID"].ToString();
        //    dts[i] = dtss;
        //}

        return dt;
    }

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for DBT_beneficiary
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class DBT_beneficiary : System.Web.Services.WebService {

    public DBT_beneficiary () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable GetDBT_beneficiary2016_17()
    {
        FMBLL bll = new FMBLL();
        DataTable dt = bll.GetDBT_beneficiary2016_17();
       

        return dt;
    }

    [WebMethod]
    public DataTable GetDBT_beneficiary2017_18()
    {
        FMBLL bll = new FMBLL();
        DataTable dt = bll.GetDBT_beneficiary2017_18();
              return dt;
    }
    
}

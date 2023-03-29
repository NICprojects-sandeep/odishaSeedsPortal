using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WSCropDistFarmer
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSCropDistFarmer : System.Web.Services.WebService {

    public WSCropDistFarmer () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable GetData()
    {
        DLL_DropDown bll = new DLL_DropDown();
        DataTable dt = bll.GetDataMaster();
        dt.TableName = "STOCK_DISTCROPFARMER";
        return dt;
    }
    
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for FarmerData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FarmerData : System.Web.Services.WebService
{

    public FarmerData() 
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataSet RFarmerDate()
    {
        string sql = "select top (100000) C.lgvillage_code,[VCHFARMERNAME],[VCHFATHERNAME],CASE [INTRELATION] WHEN 1 THEN 'Father' WHEN 2 THEN 'Husband' END [INTRELATION],CASE [INTGENDER] WHEN 1 THEN'Male' else 'Female' END [INTGENDER],CASE [INTCATEGOGY] WHEN 1 THEN 'General' WHEN 2 THEN 'SC' ELSE 'ST' END [INTCATEGOGY],[DTMDOB],[INTAGE],[VCHVOTERIDCARDNO],[VCHAADHARNO],[VCHMOBILENO],[INTFAMILYMEMBERS],[VCHACCHOLDERNM],[VCHACCOUNTNO],[INTBANKID],[INTBRANCHID],[VCHIFSCCODE],[VCHPINCODE],[INTDELETEDFLAG],A.[DTMCREATEDON],[DTMUPDATEDON],[INTREJECTED],[NICFARMERID],[CHAPFMSSTATUS],[ADHARSTATUS],[ADHARUPDT] " +
        "FROM [FARMERDB].[dbo].[M_FARMER_REGISTRATION] a " +
        "inner join [FARMERDB].[dbo].[Tbl_FarmerApprove] b on a.VCHFARMERCODE=b.Farmer_Code " +
        "INNER JOIN [FARMERDB].[dbo].[PDS_VILLAGEMASTER] C on a.VCHVILLAGEID=C.int_VillageID " +
        "WHERE b.Status in('ACCP','CCB') and VCHPACSCODE!='D' and sms_st is null AND lgvillage_code IS NOT NULL order by a.VCHFARMERCODE ";
        SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["SqlConStock"].ToString());
        da.SelectCommand.CommandTimeout = 0;
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Update();
        }
        return ds;
    }
    public void Update()
    {
        string constr = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("update top (100000) b set  b.sms_st='1' from [FARMERDB].[dbo].[M_FARMER_REGISTRATION] a inner join [FARMERDB].[dbo].[Tbl_FarmerApprove] b on a.VCHFARMERCODE=b.Farmer_Code where Status in('ACCP','CCB') and VCHPACSCODE!='D' and sms_st is null"))
            {
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandTimeout = 0;
                con.Close();
            }
        }
    }

}

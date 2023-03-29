using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


/// <summary>
/// Summary description for darpan5
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class darpan5 : System.Web.Services.WebService {

    SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["OSSOPCAConnectionString"].ToString());

    public darpan5 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string RetDMDashStateGraph()
    {
        StringBuilder strb = new StringBuilder();

        try
        {
            strb.Append("<RetDMDashStateGraph>");

            CN.Open();
            SqlCommand CM = new SqlCommand("select	* from DMDashboardGraph", CN);
            SqlDataAdapter ad = new SqlDataAdapter(CM);
            DataTable Dt = new DataTable();
            ad.Fill(Dt);


            for (var j = 0; j < Dt.Rows.Count; j++) // Check if the DataTable returns any data from database
            {
                if (Dt.Rows[j]["type"].ToString() == "7")
                {
                    strb.Append("<SDASH>");
                    strb.Append("<State_Code>" + Dt.Rows[j]["State_Code"].ToString() + "</State_Code>");
                    strb.Append("<District_Code>" + Dt.Rows[j]["District_Code"].ToString() + "</District_Code>");
                    strb.Append("<teh_code>" + Dt.Rows[j]["teh_code"].ToString() + "</teh_code>");
                    strb.Append("<blk_code>" + Dt.Rows[j]["blk_code"].ToString() + "</blk_code>");
                    strb.Append("<sector_code>" + Dt.Rows[j]["sector_code"].ToString() + "</sector_code>");

                    strb.Append("<dept_code>" + Dt.Rows[j]["dept_code"].ToString() + "</dept_code>");
                    strb.Append("<Project_code>" + Dt.Rows[j]["Project_code"].ToString() + "</Project_code>");
                    strb.Append("<Srno>" + "1" + "</Srno>");

                    strb.Append("<cnt1>" + Dt.Rows[j]["cnt1"].ToString() + "</cnt1>");
                    strb.Append("<cnt2>" + Dt.Rows[j]["cnt2"].ToString() + "</cnt2>");
                    strb.Append("<cnt3>" + Dt.Rows[j]["cnt3"].ToString() + "</cnt3>");
                    strb.Append("<cnt4>" + Dt.Rows[j]["cnt4"].ToString() + "</cnt4>");
                    strb.Append("<cnt5>" + Dt.Rows[j]["cnt5"].ToString() + "</cnt5>");

                    strb.Append("<dataportmode>" + Dt.Rows[j]["dataportmode"].ToString() + "</dataportmode>");
                    strb.Append("<modedesc>" + Dt.Rows[j]["modedesc"].ToString() + "</modedesc>");

                    strb.Append("<data_lvl_code>" + Dt.Rows[j]["data_lvl_code"].ToString() + "</data_lvl_code>");
                    strb.Append("<yr>" + Dt.Rows[j]["yr"].ToString() + "</yr>");
                    strb.Append("<mnth>" + Dt.Rows[j]["mnth"].ToString() + "</mnth>");
                    strb.Append("</SDASH>");

                }
            }
            strb.Append("</RetDMDashStateGraph>");
            CN.Close();
            return strb.ToString();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    [WebMethod]
    public string RetDMDashCaption()
    {
        StringBuilder strb = new StringBuilder();


        try
        {
            strb.Append("<RetDMDashCaption>");

            CN.Open();
            SqlCommand CM = new SqlCommand("select	* from DMDashboardCaption", CN);
            SqlDataAdapter ad = new SqlDataAdapter(CM);
            DataTable Dt = new DataTable();
            ad.Fill(Dt);


            for (var j = 0; j < Dt.Rows.Count; j++) // Check if the DataTable returns any data from database
            {
                if (Dt.Rows[j]["type"].ToString() == "7")
                {
                    strb.Append("<DASH>");
                    strb.Append("<state_code>" + Dt.Rows[j]["State_Code"].ToString() + "</state_code>");
                    strb.Append("<District_code>" + Dt.Rows[j]["District_Code"].ToString() + "</District_code>");
                    strb.Append("<teh_code>" + Dt.Rows[j]["teh_code"].ToString() + "</teh_code>");
                    strb.Append("<blk_code>" + Dt.Rows[j]["blk_code"].ToString() + "</blk_code>");
                    strb.Append("<sector_code>" + Dt.Rows[j]["sector_code"].ToString() + "</sector_code>");

                    strb.Append("<dept_code>" + Dt.Rows[j]["dept_code"].ToString() + "</dept_code>");
                    strb.Append("<Project_code>" + Dt.Rows[j]["Project_code"].ToString() + "</Project_code>");


                    strb.Append("<cnt1>" + Dt.Rows[j]["cnt1"].ToString() + "</cnt1>");
                    strb.Append("<cnt2>" + Dt.Rows[j]["cnt2"].ToString() + "</cnt2>");
                    strb.Append("<cnt3>" + Dt.Rows[j]["cnt3"].ToString() + "</cnt3>");
                    strb.Append("<cnt4>" + Dt.Rows[j]["cnt4"].ToString() + "</cnt4>");
                    strb.Append("<cnt5>" + Dt.Rows[j]["cnt5"].ToString() + "</cnt5>");

                    strb.Append("<dataportmode>" + Dt.Rows[j]["dataportmode"].ToString() + "</dataportmode>");
                    strb.Append("<modedesc>" + Dt.Rows[j]["modedesc"].ToString() + "</modedesc>");

                    strb.Append("<data_lvl_code>" + Dt.Rows[j]["data_lvl_code"].ToString() + "</data_lvl_code>");
                    strb.Append("<yr>" + Dt.Rows[j]["yr"].ToString() + "</yr>");
                    strb.Append("<mnth>" + Dt.Rows[j]["mnth"].ToString() + "</mnth>");
                    strb.Append("</DASH>");
                }
            }
            strb.Append("</RetDMDashCaption>");
            CN.Close();
            return strb.ToString();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
}

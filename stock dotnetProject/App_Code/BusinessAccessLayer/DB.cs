using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Policy;

using System.Configuration;
/// <summary>
/// Summary description for DB
/// </summary>
public class DB
{
    public SqlTransaction tran = null;

    public SqlConnection Conn;
    public SqlConnection cn;
    public SqlCommand cm;
    public SqlDataAdapter da;
    public DataSet ds = new DataSet();
    private string strQuery;
    public string sq;
    public DB()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static SqlConnection conn()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        return con;
    }

    public static SqlConnection DatabaseConnection()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        return con;
    }
    public SqlConnection Connect()
    {
        Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConFmn"].ConnectionString);
        Conn.Open();
        return Conn;
    }
    public static DataSet record_select(BLL_Obj bll)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        SqlCommand com = new SqlCommand(bll.Query, con);
        SqlDataAdapter ada = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        return ds;
    }
    
    public static int InsertRecord(BLL_Obj bll_obj)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        int k = com.ExecuteNonQuery();
        return k;
        con.Close();
    }

    public static int DeleteRecord(BLL_Obj bll_obj)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
       int k= com.ExecuteNonQuery();
        
        return k;
        con.Close();
    }

    public static int TruncRecord(BLL_Obj bll_obj)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
       int k= com.ExecuteNonQuery();
       return k;
        con.Close();

    }

    public static int UpdateRecord(BLL_Obj bll_obj)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        int k = com.ExecuteNonQuery();
        return k;

        con.Close();
    }
    public static int insertdata(BLL_Obj bll)
    {
        int i = 0;
        string[] arrParm =bll.Param.Split(',');
        string[] arrParmObj = bll.Paramobj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll.Query, con);
        com.CommandType = CommandType.Text;
        for (i = 0; i < arrParm.Length; i++)
        {
            com.Parameters.AddWithValue(arrParm[i], arrParmObj[i]);

        }
      int msg=  com.ExecuteNonQuery();
        con.Close();
        return msg;

    }
    public static string insertProcedurepumpdata(BLL_Obj bll_obj)
    {
        
        int i = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
      
        for (i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
      i=  com.ExecuteNonQuery();
    
        return i.ToString();
        con.Close();
    }
    public static string insertProcedurepumpdataRMSG(BLL_Obj bll_obj)
    {
        

        int i = 0;

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        SqlParameter msg = com.Parameters.Add("@msg", SqlDbType.VarChar, 200);
        msg.Size = 200;
        msg.Direction = ParameterDirection.Output;
        for (i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
        i = com.ExecuteNonQuery();

        string s = com.Parameters["@msg"].Value.ToString().Trim();
        return s;
        con.Close();
    }
    public static int JAL_insertProcdataRecord(BLL_Obj bll)
    {
        int i = 0;
        //string[] arrParm = bll.Param.Split(',');
        //string[] arrParmObj = bll.Paramobj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConJAL"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        //for (i = 0; i < arrParm.Length; i++)
        //{
        //    com.Parameters.AddWithValue(arrParm[i], arrParmObj[i]);

        //}
        for (i = 0; i < bll.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll.ParamList[i], bll.ParamobjList[i]);

        }
        int msg = com.ExecuteNonQuery();

        return msg;
        con.Close();
    }
    public static DataSet JAL_param_passing_fetchdata(BLL_Obj bll_obj)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConJAL"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static DataSet JAL_param_passing_fetch_storeprocedureData(BLL_Obj bll_obj)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConJAL"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static DataSet JAL_record_select(BLL_Obj bll)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConJAL"].ToString());
        SqlCommand com = new SqlCommand(bll.Query, con);
        SqlDataAdapter ada = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        return ds;
    }
    public static string JAL_insertProceduredata(BLL_Obj bll_obj)
    {
        int i = 0;
        //string[] arrParm = parm.Split(',');
        //string[] arrParmObj = parmObj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConJAL"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        SqlParameter msg = com.Parameters.Add("@msg", SqlDbType.VarChar, 200);
        msg.Size = 200;
        msg.Direction = ParameterDirection.Output;
        for (i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
        com.ExecuteNonQuery();

        string s = com.Parameters["@msg"].Value.ToString().Trim();
        return s;
        con.Close();
    }




    public static int insertProcdataRecord(BLL_Obj bll)
    {
        int i = 0;
        string[] arrParm = bll.Param.Split(',');
        string[] arrParmObj = bll.Paramobj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        for (i = 0; i < arrParm.Length; i++)
        {
            com.Parameters.AddWithValue(arrParm[i], arrParmObj[i]);

        }
        int msg = com.ExecuteNonQuery();
        
        return msg;
        con.Close();
    }
    public  int insertRollbackProcData(BLL_Obj bll_obj)
    {
        int i = 0;
        string[] arrParm = bll_obj.Param.Split(',');
        string[] arrParmObj = bll_obj.Paramobj.Split('~');

        SqlCommand com = new SqlCommand(bll_obj.Query, Conn);
        com.CommandType = CommandType.StoredProcedure;
        for (i = 0; i < arrParm.Length; i++)
        {
            com.Parameters.AddWithValue(arrParm[i], arrParmObj[i]);
        }
        com.Transaction = tran;
        int k = com.ExecuteNonQuery();
        return k;
        Conn.Close();
    }

    public static string insertProccData(BLL_Obj bll_obj)
    {
        int i = 0;
        string[] arrParm = bll_obj.Param.Split(',');
        string[] arrParmObj = bll_obj.Paramobj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        SqlParameter msg = com.Parameters.Add("@msg", SqlDbType.VarChar, 200);
        msg.Size = 200;
        msg.Direction = ParameterDirection.Output;
        for (i = 0; i < arrParm.Length; i++)
        {
            com.Parameters.AddWithValue(arrParm[i], arrParmObj[i]);

        }
        com.ExecuteNonQuery();
        string s = com.Parameters["@msg"].Value.ToString().Trim();
        return s;
        con.Close();
    }
    public static string insertProceduredata(BLL_Obj bll_obj)
    {
        int i = 0;
        //string[] arrParm = parm.Split(',');
        //string[] arrParmObj = parmObj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        SqlParameter msg = com.Parameters.Add("@msg", SqlDbType.VarChar, 200);
        msg.Size = 200;
        msg.Direction = ParameterDirection.Output;
        for (i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
        com.ExecuteNonQuery();

        string s = com.Parameters["@msg"].Value.ToString().Trim();
        return s;
        con.Close();
    }
    public static string insertProcedure1data(BLL_Obj bll_obj)
    {
        int i = 0;
        //string[] arrParm = parm.Split(',');
        //string[] arrParmObj = parmObj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        SqlParameter msg = com.Parameters.Add("@msg", SqlDbType.VarChar, 200);
        msg.Size = 200;
        msg.Direction = ParameterDirection.Output;
        for (i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
        com.ExecuteNonQuery();
        int return_value = com.ExecuteNonQuery();
        string s = com.Parameters["@msg"].Value.ToString().Trim();
        return s + ":" + return_value;
        con.Close();
    }
    public static DataSet param_passing_fetchdata(BLL_Obj bll_obj)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static DataSet param_passing_fetchFarmerdata(BLL_Obj bll_obj)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFDB"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static DataSet param_passing_fetch_storeprocedureData(BLL_Obj bll_obj)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static DataSet param_passing_fetch_globalData(BLL_Obj bll_obj)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds, bll_obj.Alies);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static int param_passing_updData(BLL_Obj bll_obj)
    {
        int str = 0;
        try
        {
            //DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            // SqlDataAdapter da = new SqlDataAdapter(cm);
           int k= cm.ExecuteNonQuery();
          
            con.Close();
            // da.Fill(ds);
          return k;
        }
        catch (Exception ex)
        {
            return str;
        }
    }

    public static DataTable param_passing_fetchDataTableData(BLL_Obj bll_obj)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < bll_obj.ParamList.Length; i++)
            {
                cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static Byte[] Ret_imageData(BLL_Obj bll_obj)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = bll_obj.Query;
        cmd.Connection = con;
        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = bll_obj.id;
        SqlDataReader dr = cmd.ExecuteReader();
        dr.Read();
        Byte[] ar = (Byte[])(dr[0]);
        dr.Close();
        cmd.Dispose();
        return ar;
    }
    public static string SendSMSData(BLL_Obj bll_obj)
    {
        try
        {
            string senderid = "AGRIOR";
            string uid = "agrior.auth";
            string pwd = "V*ei$32a";

            //string msg = "Farmer Name :Babuli Padhihari,Implement : Power tiller";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://smsgw.nic.in/sendsms_nic/sendmsg.php?uname=" + uid + "&pass=" + pwd + "&send=" + senderid + "&dest=" + Mob + "&msg=" + msg + "&prty=3&vp=30");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + pwd + "&message='" + bll_obj.Msg + "'&mnumber=" + bll_obj.Mob + "&signature=" + senderid + "");

            request.Method = WebRequestMethods.Http.Get;
            /* Assign the response object of ‘WebRequest’ to the ‘WebResponse’ variable, response.*/
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Assign the response version to the String variable.
            String ver = response.ProtocolVersion.ToString();
            /* Create and assign the response stream to the StreamReader
            variable.*/
            StreamReader reader = new
            StreamReader(response.GetResponseStream());
            string tmp = reader.ReadToEnd();

            
            return tmp;
            response.Close();
            //Response.Write("SMS sent successfully");

        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static string FMN_PermitNoRecord(BLL_Obj bll_obj)
    {
        string id = bll_obj.Dname.Substring(0, 3) + "-" + bll_obj.Imp.Substring(0, 3) + "-";
        if (bll_obj.slno < 10)
        {
            id = id + "000" + bll_obj.slno.ToString();
        }
        else if (bll_obj.slno < 100)
        {
            id = id + "00" + bll_obj.slno.ToString();
        }
        else if (bll_obj.slno < 1000)
        {
            id = id + "0" + bll_obj.slno.ToString();
        }
        else
            id = bll_obj.slno.ToString();
        return id;
    }
    public static string SendSmsPOSTData(BLL_Obj bll_obj)
    {
        try
        {
            string uname = "agrior.auth";
            string pwd = "V*ei$32a";
            string senderID = "AGRIOR";
            //string msg = "Testing";
            string mno = "919861426043,919437464228";
            HttpWebRequest request;
            Stream dataStream;

            //DataSet mob = dbsApp.returnDataset("select top 10000 Mobile_No from fmn_master");
            //string TotMob = string.Empty;
            //for (int i = 0; i < 10000; i++)
            //{
            //    TotMob += "91" + mob.Tables[0].Rows[i][0].ToString() + ",";
            //}
            //string finMob = "919437464228," + TotMob + "919861426043";

            request = (HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?");

            request.ProtocolVersion = HttpVersion.Version10;
            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
            request.Method = "POST";
            // Console.WriteLine("Before calling");
            //SingleSMS(uname, pwd, sender, mno, msg);
            // SingleSMS(uname, pwd, sender, mno, msg);
            // Method for sending single SMS.
            string query = "username=" + HttpUtility.UrlEncode(uname) +
                "&pin=" + HttpUtility.UrlEncode(pwd) +
                "&message=" + HttpUtility.UrlEncode(bll_obj.Msg) +
                "&mnumber=" + HttpUtility.UrlEncode(bll_obj.finMob) +
                "&signature=" + HttpUtility.UrlEncode(senderID);
            byte[] byteArray = Encoding.ASCII.GetBytes(query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            string Status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static string BroadcastSMSData(BLL_Obj bll_obj)
    {

        string userId = "egovodisha";

        string authCode = "7823915";
        //string SMSMode = "T";
        string RevertText = "-- Invalid mobile number --";

        try
        {

            if (bll_obj.Mob.Length >= 10)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://mkisan.gov.in/ksewa/ksewa.aspx");
                request.Timeout = 900000;
                request.ReadWriteTimeout = 900000;
                request.ProtocolVersion = HttpVersion.Version10; request.UserAgent = ".NET Framework Example Client";

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows98; DigExt)";

                request.Method = "POST";

                String query = "" + "txtMsg=" + System.Uri.EscapeDataString(bll_obj.Msg) +

                "&mobileNo=" + System.Uri.EscapeDataString(bll_obj.Mob) +

                "&SMSMode =" + System.Uri.EscapeDataString(bll_obj.SMSMode) +

                "&userId=" + System.Uri.EscapeDataString(userId) +

                "&authCode=" + System.Uri.EscapeDataString(authCode);
                byte[] byteArray = Encoding.ASCII.GetBytes(query);

                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();

                dataStream.Write(byteArray, 0, byteArray.Length);

                dataStream.Close();

                WebResponse response = request.GetResponse();
                String Status = ((HttpWebResponse)response).StatusDescription;

                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                reader.Close();

                dataStream.Close();

                response.Close();
                RevertText = responseFromServer;
            }

            else
            {

                RevertText = "-- Invalid mobile number --";

            }
        }
        catch (Exception ex)
        {
            // Response.Write(ex.ToString());
            RevertText = "-- Remote site server is not responding. --";
        }
        return RevertText;

    }
    public static DataTable DataReaderDataRecord(BLL_Obj bll_obj)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand(bll_obj.Query, con);
            cm.CommandType = CommandType.Text;
            SqlDataReader reader = cm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            con.Close();
            return dt;
        }
        catch
        {
            return null;
        }
    }
    public static string FMN_PermitSLNoNewData(BLL_Obj bll_obj)
    {
        //string id = Dname.Substring(0, 3) + "-" + Imp.Substring(0, 3) + "-";
        string Fnyr = "/FM/" + bll_obj.Fyr.Substring(0, 5) + bll_obj.Fyr.Substring(7, 2);
        string id =bll_obj.Prno;
        if (bll_obj.slno < 10)
        {
            id = id + "000" + bll_obj.slno.ToString() + Fnyr;
        }
        else if (bll_obj.slno < 100)
        {
            id = id + "00" + bll_obj.slno.ToString() + Fnyr;
        }
        else if (bll_obj.slno < 1000)
        {
            id = id + "0" + bll_obj.slno.ToString() + Fnyr;
        }
        else
            id = bll_obj.slno.ToString() + Fnyr;
        return id;
    }
    public static string SLnoRecord(BLL_Obj bll_obj)
    {
        string ref_no = string.Empty;
        string srno = "";
        Int64 serial_no;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(bll_obj.Query, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        srno = ds.Tables[0].Rows[0][0].ToString().Trim();

        if (srno.Trim() == "")
        {

            srno = "0001";

            if (Convert.ToInt32(bll_obj.mnt) < 10)
            {
                ref_no = bll_obj.yrr + bll_obj.mnt + srno;
            }
            else
            {
                ref_no = bll_obj.yrr + bll_obj.mnt + srno;
            }
            con.Close();
            return ref_no;
        }
        else
        {

            serial_no = Convert.ToInt64(srno);
            srno = "";
            serial_no = serial_no + 1;
            if (serial_no < 10)
                srno = "000" + serial_no.ToString();
            else if (serial_no < 100)
                srno = "00" + serial_no.ToString();
            else if (serial_no < 1000)
                srno = "0" + serial_no.ToString();
            else
                srno = serial_no.ToString();
            ref_no = srno;
            con.Close();
            return ref_no;
        }
    }

    public static string SLno1Record(BLL_Obj bll_obj)
    {
        string ref_no = string.Empty;
        string srno = "";
        Int64 serial_no;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlCon"].ToString());
        con.Open();
        SqlCommand cm = new SqlCommand(bll_obj.Query, con);
        cm.CommandType = CommandType.Text;
        for (int i = 0; i < bll_obj.ParamList.Length; i++)
        {
            cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
        }
        SqlDataAdapter da = new SqlDataAdapter(cm);
        DataSet ds = new DataSet();
        da.Fill(ds);
        srno = ds.Tables[0].Rows[0][0].ToString().Trim();

        if (srno.Trim() == "")
        {

            srno = "0001";

            if (Convert.ToInt32(bll_obj.mnt) < 10)
            {
                ref_no = bll_obj.yrr + bll_obj.mnt + srno;
            }
            else
            {
                ref_no = bll_obj.yrr + bll_obj.mnt + srno;
            }
            con.Close();
            return ref_no;
        }
        else
        {

            serial_no = Convert.ToInt64(srno);
            srno = "";
            serial_no = serial_no + 1;
            if (serial_no < 10)
                srno = "000" + serial_no.ToString();
            else if (serial_no < 100)
                srno = "00" + serial_no.ToString();
            else if (serial_no < 1000)
                srno = "0" + serial_no.ToString();
            else
                srno = serial_no.ToString();
            ref_no = srno;
            con.Close();
            return ref_no;
        }
    }

    public static string executequeryData(BLL_Obj bll_obj)
    {
        int i = 0;
        //string[] arrParm = parm.Split(',');
        //string[] arrParmObj = parmObj.Split('~');
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Agrisnet"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.Text;
        SqlParameter msg = com.Parameters.Add("@msg", SqlDbType.VarChar, 200);
        msg.Size = 200;
        msg.Direction = ParameterDirection.Output;
        for (i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
        com.ExecuteNonQuery();

        string s = com.ExecuteNonQuery().ToString();
        return s;
        con.Close();
    }

    public static string FMN_InvoiceSLNoNewData(BLL_Obj bll_obj)
    {
        //string id = Dname.Substring(0, 3) + "-" + Imp.Substring(0, 3) + "-";
        string Fnyr = "/" + bll_obj.Fyr;
        string id = bll_obj.Prno;

        id = id + "/" + bll_obj.slno.ToString() + Fnyr;
  
        return id;
    }


    public static DataSet record_fetchFarmerdata(BLL_Obj bll)
    {
        SqlConnection Farmercon = new SqlConnection(ConfigurationManager.AppSettings["SqlConFDB"].ToString());
        try
        {
            SqlCommand com = new SqlCommand(bll.Query, Farmercon);
            SqlDataAdapter ada = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            Farmercon.Close();
            return ds;

        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (Farmercon.State != ConnectionState.Closed)
                Farmercon.Close();
        }
    }


public static string insertProceduredata_OutputMsg(BLL_Obj bll_obj)
    {
        int i = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        SqlParameter msg = com.Parameters.Add(bll_obj.dbmsg, SqlDbType.VarChar, 200);
        msg.Size = 200;
        msg.Direction = ParameterDirection.Output;
        for (i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
        com.ExecuteNonQuery();

        string s = com.Parameters[0].Value.ToString().Trim();
        return s;
        con.Close();
    }


public static int param_passing_updFarmerDBData(BLL_Obj bll_obj)
{
    int str = 0;
    SqlConnection Farmercon = new SqlConnection(ConfigurationManager.AppSettings["SqlConFDB"].ToString());
    try
    {
        //DataSet ds = new DataSet();

        Farmercon.Open();
        SqlCommand cm = new SqlCommand(bll_obj.Query, Farmercon);
        cm.CommandType = CommandType.Text;
        for (int i = 0; i < bll_obj.ParamList.Length; i++)
        {
            cm.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);
        }
        // SqlDataAdapter da = new SqlDataAdapter(cm);
        int k = cm.ExecuteNonQuery();

        Farmercon.Close();
        // da.Fill(ds);
        return k;
    }
    catch (Exception ex)
    {
        return str;
    }
    finally
    {
        if (Farmercon.State != ConnectionState.Closed)
            Farmercon.Close();
    }
}
public static int TruncRecordWithparam(BLL_Obj bll_obj)
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFmn"].ToString());
    try
    {
        con.Open();
        SqlCommand com = new SqlCommand(bll_obj.Query, con);
        com.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < bll_obj.ParamList.Length; i++)
        {
            com.Parameters.AddWithValue(bll_obj.ParamList[i], bll_obj.ParamobjList[i]);

        }
        int k = com.ExecuteNonQuery();
        con.Close();
        return k;

    }
    catch (Exception ex)
    {
        return 0;
    }
    finally
    {
        if (con.State != ConnectionState.Closed)
            con.Close();
    }

}











}
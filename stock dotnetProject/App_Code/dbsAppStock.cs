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

/// <summary>
/// Summary description for dbsAppStock
/// </summary>
public class dbsAppStock
{

    public SqlTransaction tran = null;

    public SqlConnection Conn;
    public SqlConnection cn;
    public SqlCommand cm;
    public SqlDataAdapter da;
    public DataSet ds = new DataSet();
    private string strQuery;
    public string sq;
    public dbsAppStock()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static SqlConnection conn()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        return con;
    }
    public SqlConnection Connect()
    {
        Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString);
        Conn.Open();
        return Conn;
    }
    public static DataSet returnDataset(string qry)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            SqlCommand com = new SqlCommand(qry, con);
            SqlDataAdapter ada = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static void Insert(string InsQr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            con.Open();
            SqlCommand com = new SqlCommand(InsQr, con);
            com.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static void Delete(string delQr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            con.Open();
            SqlCommand com = new SqlCommand(delQr, con);
            com.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static void Trunc(string delQr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            con.Open();
            SqlCommand com = new SqlCommand(delQr, con);
            com.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static void Update(string UpdQr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            con.Open();
            SqlCommand com = new SqlCommand(UpdQr, con);
            com.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    /// <summary>
    /// Fertiliser Licencing System
    /// </summary>
    /// <param name="qry"></param>
    /// <param name="mnt"></param>
    /// <param name="yrr"></param>
    /// <param name="dcode"></param>
    /// <returns>FLS REF_NO</returns>
    /// 
    public static string FLS_SLno(string qry, string mnt, string yrr, string dcode)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            int pryr;
            string fyr = string.Empty;
            string y = "04";
            if (Convert.ToInt32(mnt) < 04)
            {
                pryr = Convert.ToInt32(yrr) - 1;
                fyr = "24" + dcode + "/" + pryr + "-" + yrr + "/";

            }
            else
            {
                pryr = Convert.ToInt32(yrr) + 1;
                fyr = "24" + dcode + "/" + yrr + "-" + pryr + "/";
            }

            if (srno.Trim() == "")
            {

                srno = "0001";

                if (Convert.ToInt32(mnt) < 10)
                {
                    ref_no = fyr + srno + "/N";
                }
                else
                {
                    ref_no = fyr + srno + "/N";
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
                    srno = fyr + "000" + serial_no.ToString() + "/N";
                else if (serial_no < 100)
                    srno = fyr + "00" + serial_no.ToString() + "/N";
                else if (serial_no < 1000)
                    srno = fyr + "0" + serial_no.ToString() + "/N";
                else
                    srno = fyr + serial_no.ToString() + "/N";
                ref_no = srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FLS_ExistStateSLno(string qry, string mnt, string yrr, string dcode)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            int pryr;
            string fyr = string.Empty;
            string y = "04";
            if (Convert.ToInt32(mnt) < 04)
            {
                pryr = Convert.ToInt32(yrr) - 1;
                fyr = "24" + dcode + "/" + pryr + "-" + yrr + "/";

            }
            else
            {
                pryr = Convert.ToInt32(yrr) + 1;
                fyr = "24" + dcode + "/" + yrr + "-" + pryr + "/";
            }

            if (srno.Trim() == "")
            {

                srno = "0001";

                if (Convert.ToInt32(mnt) < 10)
                {
                    ref_no = fyr + srno + "/E";
                }
                else
                {
                    ref_no = fyr + srno + "/E";
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
                    srno = fyr + "000" + serial_no.ToString() + "/E";
                else if (serial_no < 100)
                    srno = fyr + "00" + serial_no.ToString() + "/E";
                else if (serial_no < 1000)
                    srno = fyr + "0" + serial_no.ToString() + "/E";
                else
                    srno = fyr + serial_no.ToString() + "/E";
                ref_no = srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FLS_Dist_SLno_Exist(string qry, string mnt, string yrr, string dcode)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int pryr;
            string fyr = string.Empty;
            string y = "04";
            if (Convert.ToInt32(mnt) < 04)
            {
                pryr = Convert.ToInt32(yrr) - 1;
                fyr = "24" + dcode.Trim() + "/" + pryr + "-" + yrr + "/";

            }
            else
            {
                pryr = Convert.ToInt32(yrr) + 1;
                fyr = "24" + dcode.Trim() + "/" + yrr + "-" + pryr + "/";
            }


            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                srno = "0001";

                if (Convert.ToInt32(mnt) < 10)
                {
                    ref_no = fyr + srno + "/E";
                }
                else
                {
                    ref_no = fyr + srno + "/E";
                }
                con.Close();
                return ref_no;
            }
            else
            {

                srno = ds.Tables[0].Rows[0][0].ToString().Trim();
                serial_no = Convert.ToInt64(srno);
                srno = "";
                serial_no = serial_no + 1;
                if (serial_no < 10)
                    srno = fyr + "000" + serial_no.ToString() + "/E";
                else if (serial_no < 100)
                    srno = fyr + "00" + serial_no.ToString() + "/E";
                else if (serial_no < 1000)
                    srno = fyr + "0" + serial_no.ToString() + "/E";
                else
                    srno = fyr + serial_no.ToString() + "/E";
                ref_no = srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FLS_Dist_SLno(string qry, string mnt, string yrr, string dcode)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int pryr;
            string fyr = string.Empty;
            string y = "04";
            if (Convert.ToInt32(mnt) < 04)
            {
                pryr = Convert.ToInt32(yrr) - 1;
                fyr = "24" + dcode.Trim() + "/" + pryr + "-" + yrr + "/";

            }
            else
            {
                pryr = Convert.ToInt32(yrr) + 1;
                fyr = "24" + dcode.Trim() + "/" + yrr + "-" + pryr + "/";
            }


            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                srno = "0001";

                if (Convert.ToInt32(mnt) < 10)
                {
                    ref_no = fyr + srno + "/N";
                }
                else
                {
                    ref_no = fyr + srno + "/N";
                }
                con.Close();
                return ref_no;
            }
            else
            {

                srno = ds.Tables[0].Rows[0][0].ToString().Trim();
                serial_no = Convert.ToInt64(srno);
                srno = "";
                serial_no = serial_no + 1;
                if (serial_no < 10)
                    srno = fyr + "000" + serial_no.ToString() + "/N";
                else if (serial_no < 100)
                    srno = fyr + "00" + serial_no.ToString() + "/N";
                else if (serial_no < 1000)
                    srno = fyr + "0" + serial_no.ToString() + "/N";
                else
                    srno = fyr + serial_no.ToString() + "/N";
                ref_no = srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    //PESTICIDE LICENSE STARTS
    public static string PLS_Dist_Ref_no(string qry, string[] parameters, object[] values, string yrr, string dao_cd)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string do_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();


            SqlCommand cm = new SqlCommand(qry, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);


            // SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";
                do_no = "P/" + "24" + dao_cd.Trim() + "/" + yrr + "/" + srno + "/N";
                con.Close();
                return do_no;
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
                //do_no = "Pesti/" + dao_cd.Trim() + "/" + yrr + "/" + srno + "/N";
                do_no = "P/" + "24" + dao_cd.Trim() + "/" + yrr + "/" + srno + "/N";
                con.Close();
                return do_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string PLS_Dist_Ref_no_E(string qry, string yrr, string dao_cd, string lic_type)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string do_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";
                do_no = "P/" + "24" + dao_cd.Trim() + "/" + yrr + "/" + srno + "/E";
                con.Close();
                return do_no;
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
                //do_no = "Pesti/" + dao_cd.Trim() + "/" + yrr + "/" + srno + "/E";
                do_no = "P/" + "24" + dao_cd.Trim() + "/" + yrr + "/" + srno + "/E";
                con.Close();
                return do_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string PLS_License_N(string qry, string c_year, string str2, string code)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string do_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";
                do_no = "PESTI" + '/' + code + '/' + str2 + '/' + c_year + '/' + srno + '/' + 'N';

                con.Close();
                return do_no;
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
                //do_no = "Pesti" + '/' + code + '/' + str2 + '/' + c_year + '/' + srno + '/' + 'N';
                do_no = "PESTI" + '/' + code + '/' + str2 + '/' + c_year + '/' + srno + '/' + 'N';
                con.Close();
                return do_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string PLS_License_E(string qry, string c_year, string str2, string code, string Type)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string do_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";
                do_no = "PESTI" + '/' + code + '/' + str2 + '/' + c_year + '/' + srno + '/' + 'E';

                con.Close();
                return do_no;
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
                //do_no = "PESTI" + '/' + code + '/' + str2 + '/' + c_year + '/' + srno + '/' + 'E';
                do_no = "PESTI" + '/' + code + '/' + str2 + '/' + c_year + '/' + srno + '/' + 'E';
                con.Close();
                return do_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    //PESTICIDE LICENSE ENDS
    public static string FIM_DO_no(string qry, string yrr, string dist_cd)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string do_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "00001";
                do_no = yrr + "/" + dist_cd + "/" + srno;
                con.Close();
                return do_no;
            }
            else
            {
                serial_no = Convert.ToInt64(srno);
                srno = "";
                serial_no = serial_no + 1;
                if (serial_no < 10)
                    srno = "0000" + serial_no.ToString();
                else if (serial_no < 100)
                    srno = "000" + serial_no.ToString();
                else if (serial_no < 1000)
                    srno = "00" + serial_no.ToString();
                else if (serial_no < 10000)
                    srno = "0" + serial_no.ToString();
                else
                    srno = serial_no.ToString();
                do_no = yrr + "/" + dist_cd + "/" + srno;
                con.Close();
                return do_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FIM_SLno(string qry, string yrr, string brand, string rk_cd)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "00001";
                ref_no = yrr + "/" + brand + "/" + rk_cd + "/" + srno;
                con.Close();
                return ref_no;
            }
            else
            {
                serial_no = Convert.ToInt64(srno);
                srno = "";
                serial_no = serial_no + 1;
                if (serial_no < 10)
                    srno = "0000" + serial_no.ToString();
                else if (serial_no < 100)
                    srno = "000" + serial_no.ToString();
                else if (serial_no < 1000)
                    srno = "00" + serial_no.ToString();
                else if (serial_no < 10000)
                    srno = "0" + serial_no.ToString();
                else
                    srno = serial_no.ToString();
                ref_no = yrr + "/" + brand + "/" + rk_cd + "/" + srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FIM_rake_SLno(string qry, string yrr, string rk_cd)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";
                ref_no = yrr + "/" + rk_cd + "/" + srno;
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
                ref_no = yrr + "/" + rk_cd + "/" + srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FQC_SLno(string qry, string code, string yrr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";

                if (Convert.ToInt32(code) < 10)
                {
                    ref_no = "24" + code + "/" + "S/" + yrr + "/" + srno;
                }
                else
                {
                    ref_no = "24" + code + "/" + "S/" + yrr + "/" + srno;
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
                ref_no = "24" + code + "/" + "S/" + yrr + "/" + srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FQC_ref_SLno(string qry, string code, string yrr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";

                if (Convert.ToInt32(code) < 10)
                {
                    ref_no = "24" + code + "/" + "R/" + yrr + "/" + srno;
                }
                else
                {
                    ref_no = "24" + code + "/" + "R/" + yrr + "/" + srno;
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
                ref_no = "24" + code + "/" + "R/" + yrr + "/" + srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FQC_LabSLno(string qry, string code, string mon)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "01";

                //if (Convert.ToInt32(code) < 10)
                //{
                //    ref_no = code + "/" + yrr + "/" + srno;
                //}
                //else
                //{
                ref_no = code + "/" + mon + srno;
                //}
                con.Close();
                return ref_no;
            }
            else
            {
                serial_no = Convert.ToInt64(srno);
                srno = "";
                serial_no = serial_no + 1;
                if (serial_no < 10)
                    srno = "0" + serial_no.ToString();
                else
                    srno = serial_no.ToString();
                ref_no = code + "/" + mon + srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string SIM_SLno(string qry, string yrr, string seas, string crop_nm)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";
                ref_no = yrr + "/" + seas + "/" + crop_nm + "/" + srno;
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
                ref_no = yrr + "/" + seas + "/" + crop_nm + "/" + srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    /// <summary>
    /// Generates a random numbers with the given length
    /// </summary>
    /// <param name="min">minimum length of the number</param>
    /// <param name="max">maximum length of the number</param>
    /// <returns>Random number</returns>
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public static string FIM_Whsl_DO_no(string qry, string yrr, string dist_cd, string fo_id)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string do_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "00001";
                do_no = "W" + "/" + yrr + "/" + fo_id + "/" + dist_cd + "/" + srno;
                con.Close();
                return do_no;
            }
            else
            {
                serial_no = Convert.ToInt64(srno);
                srno = "";
                serial_no = serial_no + 1;
                if (serial_no < 10)
                    srno = "0000" + serial_no.ToString();
                else if (serial_no < 100)
                    srno = "000" + serial_no.ToString();
                else if (serial_no < 1000)
                    srno = "00" + serial_no.ToString();
                else if (serial_no < 10000)
                    srno = "0" + serial_no.ToString();
                else
                    srno = serial_no.ToString();
                do_no = "W" + "/" + yrr + "/" + fo_id + "/" + dist_cd + "/" + srno;
                con.Close();
                return do_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string SLno(string qry, string mnt, string yrr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";

                if (Convert.ToInt32(mnt) < 10)
                {
                    ref_no = yrr + mnt + srno;
                }
                else
                {
                    ref_no = yrr + mnt + srno;
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
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string SLno1(string qry, string[] parameters, object[] values, string mnt, string yrr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlCommand cm = new SqlCommand(qry, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "0001";

                if (Convert.ToInt32(mnt) < 10)
                {
                    ref_no = yrr + mnt + srno;
                }
                else
                {
                    ref_no = yrr + mnt + srno;
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
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string FMN_PermitNo(string Dname, string Imp, int slno)
    {
        string id = Dname.Substring(0, 3) + "-" + Imp.Substring(0, 3) + "-";
        if (slno < 10)
        {
            id = id + "000" + slno.ToString();
        }
        else if (slno < 100)
        {
            id = id + "00" + slno.ToString();
        }
        else if (slno < 1000)
        {
            id = id + "0" + slno.ToString();
        }
        else
            id = slno.ToString();
        return id;
    }
    // Public Shared Sub InsertProcedure(ByVal procedureName As String, ByVal parm As String, ByVal parmObj As String)
    //    Dim i As Integer
    //    Dim arrParm() As String = parm.Split(",")
    //    Dim arrparmObj() As String = parmObj.Split(",")
    //    Dim com As New SqlCommand(procedureName, DbsAppl.Connection())
    //    com.CommandType = CommandType.StoredProcedure
    //    For i = 0 To arrParm.Length - 1
    //        com.Parameters.AddWithValue(arrParm(i), arrparmObj(i))
    //    Next
    //    com.ExecuteNonQuery()
    //End Sub
    public static void insertProc(string prcName, string parm, string parmObj)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            int i = 0;
            string[] arrParm = parm.Split(',');
            string[] arrParmObj = parmObj.Split('~');

            con.Open();
            SqlCommand com = new SqlCommand(prcName, con);
            com.CommandType = CommandType.StoredProcedure;
            for (i = 0; i < arrParm.Length; i++)
            {
                com.Parameters.AddWithValue(arrParm[i], arrParmObj[i]);

            }
            com.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public void insertRollbackProc(string prcName, string parm, string parmObj)
    {
        int i = 0;
        string[] arrParm = parm.Split(',');
        string[] arrParmObj = parmObj.Split('~');

        SqlCommand com = new SqlCommand(prcName, Conn);
        com.CommandType = CommandType.StoredProcedure;
        for (i = 0; i < arrParm.Length; i++)
        {
            com.Parameters.AddWithValue(arrParm[i], arrParmObj[i]);
        }
        com.Transaction = tran;
        com.ExecuteNonQuery();

    }
    public static string insertProcc(string prcName, string parm, string parmObj)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            int i = 0;
            string[] arrParm = parm.Split(',');
            string[] arrParmObj = parmObj.Split('~');

            con.Open();
            SqlCommand com = new SqlCommand(prcName, con);
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
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static string ComputeHash(string plainText,
                                     string hashAlgorithm,
                                     byte[] saltBytes)
    {
        // If salt is not specified, generate it on the fly.
        if (saltBytes == null)
        {
            // Define min and max salt sizes.
            int minSaltSize = 4;
            int maxSaltSize = 8;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            // Allocate a byte array, which will hold the salt.
            saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltBytes);
        }

        // Convert plain text into a byte array.
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        // Allocate array, which will hold plain text and salt.
        byte[] plainTextWithSaltBytes =
                new byte[plainTextBytes.Length + saltBytes.Length];

        // Copy plain text bytes into resulting array.
        for (int i = 0; i < plainTextBytes.Length; i++)
            plainTextWithSaltBytes[i] = plainTextBytes[i];

        // Append salt bytes to the resulting array.
        for (int i = 0; i < saltBytes.Length; i++)
            plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

        // Because we support multiple hashing algorithms, we must define
        // hash object as a common (abstract) base class. We will specify the
        // actual hashing algorithm class later during object creation.
        HashAlgorithm hash;

        // Make sure hashing algorithm name is specified.
        if (hashAlgorithm == null)
            hashAlgorithm = "";

        // Initialize appropriate hashing algorithm class.
        switch (hashAlgorithm.ToUpper())
        {
            case "SHA1":
                hash = new SHA1Managed();
                break;

            case "SHA256":
                hash = new SHA256Managed();
                break;

            case "SHA384":
                hash = new SHA384Managed();
                break;

            case "SHA512":
                hash = new SHA512Managed();
                break;

            default:
                hash = new MD5CryptoServiceProvider();
                break;
        }

        // Compute hash value of our plain text with appended salt.
        byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

        // Create array which will hold hash and original salt bytes.
        byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                            saltBytes.Length];

        // Copy hash bytes into resulting array.
        for (int i = 0; i < hashBytes.Length; i++)
            hashWithSaltBytes[i] = hashBytes[i];

        // Append salt bytes to the result.
        for (int i = 0; i < saltBytes.Length; i++)
            hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

        // Convert result into a base64-encoded string.
        string hashValue = Convert.ToBase64String(hashWithSaltBytes);

        // Return the result.
        return hashValue;
    }
    /// <summary>
    /// Compares a hash of the specified plain text value to a given hash
    /// value. Plain text is hashed with the same salt value as the original
    /// hash.
    /// </summary>
    /// <param name="plainText">
    /// Plain text to be verified against the specified hash. The function
    /// does not check whether this parameter is null.
    /// </param>
    /// <param name="hashAlgorithm">
    /// Name of the hash algorithm. Allowed values are: "MD5", "SHA1", 
    /// "SHA256", "SHA384", and "SHA512" (if any other value is specified,
    /// MD5 hashing algorithm will be used). This value is case-insensitive.
    /// </param>
    /// <param name="hashValue">
    /// Base64-encoded hash value produced by ComputeHash function. This value
    /// includes the original salt appended to it.
    /// </param>
    /// <returns>
    /// If computed hash mathes the specified hash the function the return
    /// value is true; otherwise, the function returns false.
    /// </returns>
    public static bool VerifyHash(string plainText,
                                  string hashAlgorithm,
                                  string hashValue)
    {
        // Convert base64-encoded hash value into a byte array.
        byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

        // We must know size of hash (without salt).
        int hashSizeInBits, hashSizeInBytes;

        // Make sure that hashing algorithm name is specified.
        if (hashAlgorithm == null)
            hashAlgorithm = "";

        // Size of hash is based on the specified algorithm.
        switch (hashAlgorithm.ToUpper())
        {
            case "SHA1":
                hashSizeInBits = 160;
                break;

            case "SHA256":
                hashSizeInBits = 256;
                break;

            case "SHA384":
                hashSizeInBits = 384;
                break;

            case "SHA512":
                hashSizeInBits = 512;
                break;

            default: // Must be MD5
                hashSizeInBits = 128;
                break;
        }

        // Convert size of hash from bits to bytes.
        hashSizeInBytes = hashSizeInBits / 8;

        // Make sure that the specified hash value is long enough.
        if (hashWithSaltBytes.Length < hashSizeInBytes)
            return false;

        // Allocate array to hold original salt bytes retrieved from hash.
        byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                    hashSizeInBytes];

        // Copy salt from the end of the hash to the new array.
        for (int i = 0; i < saltBytes.Length; i++)
            saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

        // Compute a new hash string.
        string expectedHashString =
                    ComputeHash(plainText, hashAlgorithm, saltBytes);

        // If the computed hash matches the specified hash,
        // the plain text value must be correct.
        return (hashValue == expectedHashString);

    }
    public static string SQC_Refno(string qry, string yrr, string type_cd)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            string ref_no = string.Empty;
            string srno = "";
            Int64 serial_no;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            srno = ds.Tables[0].Rows[0][0].ToString().Trim();

            if (srno.Trim() == "")
            {

                srno = "00001";
                ref_no = "SQC" + "/" + "2401" + "/" + yrr + "/" + type_cd + srno;
                con.Close();
                return ref_no;
            }
            else
            {
                serial_no = Convert.ToInt64(srno);
                srno = "";
                serial_no = serial_no + 1;
                if (serial_no < 10)
                    srno = "0000" + serial_no.ToString();
                else if (serial_no < 100)
                    srno = "000" + serial_no.ToString();
                else if (serial_no < 1000)
                    srno = "00" + serial_no.ToString();
                else if (serial_no < 10000)
                    srno = "0" + serial_no.ToString();
                else
                    srno = serial_no.ToString();
                ref_no = "SQC" + "/" + "2401" + "/" + yrr + "/" + type_cd + srno;
                con.Close();
                return ref_no;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    /// <summary>
    /// For SMS Generation
    /// </summary>
    /// <param name="qry"></param>
    /// <param name="yrr"></param>
    /// <param name="type_cd"></param>
    /// <returns></returns>
    /// 
    public static void SendSMS(string Mob, string msg)
    {
        try
        {
            string senderid = "AGRIOR";
            string uid = "agrior.auth";
            string pwd = "V*ei$32a";

            //string msg = "Farmer Name :Babuli Padhihari,Implement : Power tiller";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://smsgw.nic.in/sendsms_nic/sendmsg.php?uname=" + uid + "&pass=" + pwd + "&send=" + senderid + "&dest=" + Mob + "&msg=" + msg + "&prty=3&vp=30");
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
            response.Close();

            //Response.Write("SMS sent successfully");

        }
        catch (Exception ex)
        {

        }
    }
    public static string CheckDay()
    {
        try
        {
            string todayday, stend;
            DateTime STime, now;
            todayday = System.DateTime.Today.DayOfWeek.ToString();
            stend = "17:59:59";
            STime = Convert.ToDateTime(stend);
            now = Convert.ToDateTime(System.DateTime.Now.ToString("hh:mm tt"));
            if (todayday.ToString() == "Thursday" || (todayday.ToString() == "Friday" && now <= STime))
            {
                return "T";
            }
            else
            {
                return "F";
            }
        }
        catch (Exception ex) { return null; }
    }
    public static string CalcFYear()
    {
        try
        {

            int mnth = System.DateTime.Now.Month;
            if (mnth > 03)
            {
                string yr = System.DateTime.Now.Year.ToString();
                string nextyr = System.DateTime.Now.AddYears(1).Year.ToString();
                string nextyear = nextyr.Substring(2, 2);
                string cfyr = yr + "-" + nextyear;
                return cfyr;
            }
            else
            {
                string nextyr = System.DateTime.Now.AddYears(-1).Year.ToString();
                string yr = System.DateTime.Now.Year.ToString();
                string year = yr.Substring(2, 2);
                string cfyr = nextyr + "-" + year;
                return cfyr;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static DataSet param_passing_fetch(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            DataSet ds = new DataSet();

            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static DataSet param_passing_proc(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            DataSet ds = new DataSet();
            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static void param_passing_upd(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            //DataSet ds = new DataSet();

            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            // SqlDataAdapter da = new SqlDataAdapter(cm);
            cm.ExecuteNonQuery();
            con.Close();
            // da.Fill(ds);
            //return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static DataTable param_passing_fetchDataTable(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            DataTable dt = new DataTable();

            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    public static Byte[] Ret_image(string id, string q)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = q;
            cmd.Connection = con;
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Byte[] ar = (Byte[])(dr[0]);
            dr.Close();
            cmd.Dispose();
            return ar;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }
    #region " Validations for File Types"
    private enum ImageFileExtension
    {
        none = 0,
        jpg = 1,
        jpeg = 2,
        bmp = 3,
        gif = 4,
        png = 5
    }

    private enum VideoFileExtension
    {
        none = 0,
        wmv = 1,
        mpg = 2,
        mpeg = 3,
        mp4 = 4,
        avi = 5,
        flv = 6
    }

    private enum PDFFileExtension
    {
        none = 0,
        PDF = 1
    }

    private enum TextFileExtension
    {
        none = 0,
        txt = 1,
        doc = 2,
        docx = 3
    }

    public enum FileType
    {
        None = 0,
        Image = 1,
        Video = 2,
        PDF = 3,
        Text = 4,
        DOC = 5,
        DOCX = 6
    }

    public static bool isValidFile(byte[] bytFile, FileType flType, String FileContentType)
    {
        bool isvalid = false;

        if (flType == FileType.None)
        {
            return isvalid;
        }
        else if (flType == FileType.Image)
        {
            isvalid = isValidImageFile(bytFile, FileContentType);
        }
        else if (flType == FileType.Video)
        {
            isvalid = isValidVideoFile(bytFile, FileContentType);
        }
        else if (flType == FileType.PDF)
        {
            isvalid = isValidPDFFile(bytFile, FileContentType);
        }
        else if (flType == FileType.Text)
        {
            isvalid = isValidTextFile(bytFile, FileContentType);
        }

        return isvalid;

    }

    private static bool isValidImageFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytejpg = { 255, 216, 255, 224 };
        byte[] chkBytebmp = { 66, 77 };
        byte[] chkBytegif = { 71, 73, 70, 56 };
        byte[] chkBytepng = { 137, 80, 78, 71 };
        bool isvalid = false;

        ImageFileExtension imgfileExtn = ImageFileExtension.none;
        if (FileContentType.Contains("jpg") | FileContentType.Contains("jpeg"))
        {
            imgfileExtn = ImageFileExtension.jpg;
        }
        else if (FileContentType.Contains("bmp"))
        {
            imgfileExtn = ImageFileExtension.bmp;
        }
        else if (FileContentType.Contains("gif"))
        {
            imgfileExtn = ImageFileExtension.gif;
        }
        else if (FileContentType.Contains("png"))
        {
            imgfileExtn = ImageFileExtension.png;
        }

        if (imgfileExtn == ImageFileExtension.jpg || imgfileExtn == ImageFileExtension.jpeg)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytejpg[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.bmp)
        {
            if (bytFile.Length >= 2)
            {
                int j = 0;
                for (Int32 i = 0; i <= 1; i++)
                {
                    if (bytFile[i] == chkBytebmp[i])
                    {
                        j = j + 1;
                        if (j == 1)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.gif)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytegif[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.png)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytepng[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }

    private static bool isValidVideoFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytewmv = { 48, 38, 178, 117 };
        byte[] chkByteavi = { 82, 73, 70, 70 };
        byte[] chkByteflv = { 70, 76, 86, 1 };
        byte[] chkBytempg = { 0, 0, 1, 186 };
        byte[] chkBytemp4 = { 0, 0, 0, 20 };
        bool isvalid = false;

        VideoFileExtension vdofileExtn = VideoFileExtension.none;
        if (FileContentType.Contains("wmv"))
        {
            vdofileExtn = VideoFileExtension.wmv;
        }
        else if (FileContentType.Contains("mpg") || FileContentType.Contains("mpeg"))
        {
            vdofileExtn = VideoFileExtension.mpg;
        }
        else if (FileContentType.Contains("mp4"))
        {
            vdofileExtn = VideoFileExtension.mp4;
        }
        else if (FileContentType.Contains("avi"))
        {
            vdofileExtn = VideoFileExtension.avi;
        }
        else if (FileContentType.Contains("flv"))
        {
            vdofileExtn = VideoFileExtension.flv;
        }

        if (vdofileExtn == VideoFileExtension.wmv)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytewmv[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if ((vdofileExtn == VideoFileExtension.mpg || vdofileExtn == VideoFileExtension.mpeg))
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytempg[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.mp4)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytemp4[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.avi)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteavi[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.flv)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteflv[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;

        //if (isinvalid)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}
    }

    private static bool isValidPDFFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytepdf = { 37, 80, 68, 70 };
        bool isvalid = false;

        PDFFileExtension pdffileExtn = PDFFileExtension.none;
        if (FileContentType.Contains("pdf"))
        {
            pdffileExtn = PDFFileExtension.PDF;
        }

        if (pdffileExtn == PDFFileExtension.PDF)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytepdf[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }
    //private static bool ValidDropChk(DropDownList chk,string val)
    //{
    //    if (chk.Items.FindByValue(val))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private static bool isValidTextFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytetxt = { 99, 114, 101, 97 };
        byte[] chkBytedocx = { 208, 207, 17, 224 };
        byte[] chkBytedoc = { 80, 75, 3, 4 };
        byte[] chkBytetxt_ = { 255, 254, 66, 0 };

        bool isvalid = false;

        TextFileExtension txtfileExtn = TextFileExtension.none;
        if (FileContentType.Contains("txt") || FileContentType.Contains("text"))
        {
            txtfileExtn = TextFileExtension.txt;
        }
        else if (FileContentType.Contains("doc"))
        {
            txtfileExtn = TextFileExtension.doc;
        }
        else if (FileContentType.Contains("docx") || FileContentType.Contains("octet"))
        {
            txtfileExtn = TextFileExtension.docx;
        }

        if (txtfileExtn == TextFileExtension.txt)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytetxt[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (txtfileExtn == TextFileExtension.docx)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytedocx[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (txtfileExtn == TextFileExtension.doc)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytedoc[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }
    #endregion
    //USING FOR DEALER FOR SEED LICENCE SYSTEM
    public static DataSet param_passing_fetchSeed(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConSeed"].ToString());
        try
        {
            DataSet ds = new DataSet();

            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
    }

    public static DataSet param_passing_fetchDataSetSP(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConStock"].ToString());
        try
        {
            DataSet ds = new DataSet();

            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandTimeout = 0;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
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
}

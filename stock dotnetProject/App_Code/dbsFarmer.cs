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
/// Summary description for dbsFarmer
/// </summary>
public class dbsFarmer
{
    public SqlTransaction tran = null;

    public SqlConnection Conn;
    public SqlConnection cn;
    public SqlCommand cm;
    public SqlDataAdapter da;
    public DataSet ds = new DataSet();
    private string strQuery;
    public string sq;
    public dbsFarmer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static SqlConnection conn()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
        return con;
    }

    public static DataSet param_passing_fetchDataSetSP(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
        try
        {
            DataSet dt = new DataSet();

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

    public static DataSet param_passing_fetchDataSet(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
        try
        {
            DataSet dt = new DataSet();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.Text;
            for (int i = 0; i < parameters.Length; i++)
            {
                cm.Parameters.AddWithValue(parameters[i], values[i]);
            }
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cm);
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
    public static DataSet param_passing_fetchDataSet_new(string q)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
        try
        {
            DataSet dt = new DataSet();

            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.Text;

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

    public static DataSet param_passing_proc(string q, string[] parameters, object[] values)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["SqlConFarmer"].ToString());
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

}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DbConfigueAGRIS
/// </summary>
public class DbConfigueAGRIS
{
    public DbConfigueAGRIS()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static string _connDAFP;
    public static string ConnectionStringDAFP
    {
        get
        {
            _connDAFP = ConfigurationManager.ConnectionStrings["constrDAFP"].ConnectionString;
            return _connDAFP;
        }
    }
    public static DataTable GetTableDetails(string query)
    {
        SqlConnection con = new SqlConnection(DbConfigueAGRIS.ConnectionStringDAFP);
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        da.Fill(ds);
        return ds.Tables[0];
    }
    public static object GetScalar(string query)
    {
        object var;
        SqlConnection con = new SqlConnection(DbConfigueAGRIS.ConnectionStringDAFP);
        try
        {
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var = cmd.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return var;
    }
}
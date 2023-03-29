using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for DbConfigue
/// </summary>
public class DbConfigue
{
    SqlConnection con;
    SqlCommand com;
    SqlDataReader dr;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;

	public DbConfigue()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static string _conn;
    public static string ConnectionString
    {
        get
        {
            _conn = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
            return _conn;
        }
    }
    public DataSet BindDataSet(string qry, Hashtable ht)
    {
        DataSet ds = new DataSet();
        con = new SqlConnection(_conn);
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry p in ht)
                {
                    cmd.Parameters.AddWithValue((string)p.Key, p.Value);
                }
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return ds;
        }
        catch (Exception)
        {
            throw;
        }
        return ds;
    }
    public static object GetScalar(string query, SqlParameter[] param)
    {
        object var;
        SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(param);
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
    public static object GetScalar_SMP(string query, SqlParameter[] param)
    {
        object var;
        SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
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
    public static object GetScalar(string query)
    {
        object var;
        SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
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
    public static DataTable ExecuteNonQuerySP_DA(string query, SqlParameter[] param)
    {
        SqlConnection con=new SqlConnection(DbConfigue.ConnectionString);
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return dt;
    }

    public static int ExecuteNonQuerySP(string query, SqlParameter[] param)
    {
        int rowaffected = 0;
        SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.Parameters.AddRange(param);
            con.Open();
            rowaffected = cmd.ExecuteNonQuery();
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
        return rowaffected;
    }

    public static int ExecuteNonQuery(string query, SqlParameter[] param)
    {
        int rowaffected = 0;
        SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.Parameters.AddRange(param);
            con.Open();
            rowaffected = cmd.ExecuteNonQuery();
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
        return rowaffected;
    }
    //public static int ExecuteNonQuery(string query)
    //{
    //    int rowaffected = 0;
    //    SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        con.Open();
    //        rowaffected = cmd.ExecuteNonQuery();
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //    }
    //    return rowaffected;
    //}
    public static DataTable GetTableDetails_SP(string query, SqlParameter[] param)
    {
        try
        {
            SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(param);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static DataTable GetTableDetails(string query, SqlParameter[] param)
    {
        try
        {
            SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(param);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static DataTable GetTableDetails(string query)
    {
        SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        da.Fill(ds);
        return ds.Tables[0];
    }
    public static DataView GetViewTable(string query)
    {
        SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        DataView dv = ds.Tables[0].DefaultView;
        return dv;
    }
    //public static DataView GetViewTable(string query)
    //{
    //    SqlConnection con = new SqlConnection(DbConfigue.ConnectionString);
    //    DataSet ds = new DataSet();
    //    SqlDataAdapter da = new SqlDataAdapter(query, con);
    //    da.Fill(ds);
    //    DataView dv = ds.Tables[0].DefaultView;
    //    return dv;
    //}

}
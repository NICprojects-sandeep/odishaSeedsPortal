using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dbsJalanidhi
/// </summary>
public class dbsJalanidhi
{
    public SqlTransaction tran = null;

    public SqlConnection Conn;
    public SqlConnection cn;
    public SqlCommand cm;
    public SqlDataAdapter da;
    public DataSet ds = new DataSet();
    private string strQuery;
    public string sq;
    public dbsJalanidhi()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet param_passing_fetchDataSet(string q, string[] parameters, object[] values)
    {
        try
        {
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConJAL"].ToString());
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
            return null;
        }
    }
    public static DataSet param_passing_fetchDataSetFm(string q, string[] parameters, object[] values)
    {
        try
        {
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sqlfmdbt"].ToString());
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
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for cls_ossopca
/// </summary>
public class cls_ossopca
{
    SqlConnection cn;
    SqlCommand cm;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    View v1 = new View();
    SqlParameter pm;
    SqlConnection pcn
    {
        set
        {
            cn = value;
        }
        get
        {
            return cn;
        }
    }
    
	public cls_ossopca()
	{
		//
		// TODO: Add constructor logic here
		//
        string str = ConfigurationManager.ConnectionStrings["OSSOPCAConnectionString"].ToString();
        cn = new SqlConnection(str);

	}

    
    public int sql_stmt(string[] param, object[] val, string proc)
    {
        int e=0;
        //try
        //{
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            cm = new SqlCommand(proc, cn);
            cm.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < val.Length; i++)
            {

                pm = new SqlParameter(param[i], val[i]);
                cm.Parameters.Add(pm);
            }
          
            pm = new SqlParameter("@error",SqlDbType.VarChar,5000);
            pm.Direction = ParameterDirection.Output;
            cm.Parameters.Add(pm);
             e = cm.ExecuteNonQuery();


             string ss = (cm.Parameters["@error"].Value).ToString();
            cn.Close();

        //}
        //catch (Exception) { }
        return e;
    }
    public DataSet fetch(string[] param, object[] val, string proc)
    {
        if (cn.State == ConnectionState.Closed)
        {
            cn.Open();
        }
        cm = new SqlCommand(proc, cn);
        cm.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < val.Length; i++)
        {
            pm = new SqlParameter(param[i], val[i]);
            cm.Parameters.Add(pm);
        }
        da = new SqlDataAdapter(cm);
        cn.Close();
        ds.Clear();
        ds.Reset();
        da.Fill(ds);
        return ds;
    }
    public DataSet fetchdttm(string[] param, DateTime[] val, string proc)
    {
        if (cn.State == ConnectionState.Closed)
        {
            cn.Open();
        }
        cm = new SqlCommand(proc, cn);
        cm.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < val.Length; i++)
        {
            pm = new SqlParameter(param[i], val[i]);
            cm.Parameters.Add(pm);
        }
        da = new SqlDataAdapter(cm);
        cn.Close();
        ds.Clear();
        ds.Reset();
        da.Fill(ds);
        return ds;
    }
    public DataSet grdv_show(string[] param, object[] val, string proc, GridView g)
    {
      DataSet ds_fetch=  fetch(param, val, proc);
      g.DataSource = ds_fetch.Tables[0];
        g.DataBind();
        return ds_fetch;
    }
    public DataSet return_dataset(string q)
    {
        try
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            da = new SqlDataAdapter(q, cn);
            cn.Close();
            ds.Clear();
            ds.Reset();
            da.Fill(ds);
          
        }
            
        catch
        { }
        return ds;
    }
    public GridView grd_show(string q, GridView g)
    {

        return_dataset(q);
        g.DataSource = ds.Tables[0];
        g.DataBind();
        return g;
    }
    public DetailsView dtv_show(string[] param, object[] val, string proc, DetailsView d)
    {
        fetch(param, val, proc);
        d.DataSource = ds.Tables[0];
        d.DataBind();
        return d;
    }  
    public int dml_stmt(string q)
    {
        if (cn.State == ConnectionState.Closed)
        {
            cn.Open();
        }
        cm = new SqlCommand(q, cn);
        int r_val = cm.ExecuteNonQuery();
        cn.Close();
        return r_val;
    }
    public DataSet parameter_passing(string query, string[] param, object[] values)
    {
        if (cn.State == ConnectionState.Closed)
        {
            cn.Open();
        }
        cm = new SqlCommand(query, cn);
        cm.CommandType = CommandType.Text;
        for (int i = 0; i < param.Length; i++)
        {
            cm.Parameters.AddWithValue(param[i], values[i]);

        }
        da = new SqlDataAdapter(cm);
        cn.Close();
        DataSet ds = new DataSet();

        da.Fill(ds);
        return ds;
    }
    public int dml_param_passing(string q, string[] param, object[] values)
    {
        if (cn.State == ConnectionState.Closed)
        {
            cn.Open();
        }
        cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        for (int i = 0; i < values.Length; i++)
        {
            cm.Parameters.AddWithValue(param[i], values[i]);
        }
       
        int x = cm.ExecuteNonQuery();
        return x;
    }
    public DataSet grid_return(string q, string[] param, object[] values, GridView g)
    {
        parameter_passing(q, param, values);
        g.DataSource = ds.Tables[0];
        g.DataBind();
        return ds;
    }

    public bool IsEmail(string Email)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Email))
            return (true);
        else
            return (false);
    }
    public bool ValidFunction(string[] Value, int Ln, int Ln2)
    {
        bool x = true;
        Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9\\/\\ .-]+$");
        for (int i = 0; i < Value.Length; i++)
        {
            if ((Value[i].Length > Ln2) && (Value[i].Length < Ln) && (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true))
            {
                x = true;
               
            }
            else
            {
                x = false;
                return x;
            }
        }
        return x;
    }
    //vikram
    public bool ValidFunctionspecial(string[] Value, int Ln, int Ln2)
    {
        bool x = true;
        Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9\\/\\ )\\(\\,\\.-]+$");
        for (int j = 0; j < Value.Length; j++)
        {
            if ((Value[j].Length > Ln2) && (Value[j].Length < Ln) && (x = objAlphaPattern.IsMatch(Value[j].ToString()) == true))
            {
                x = true;
            }
            else
            {
                x = false;
                return x;
            }
        }
        return x;
    }
    public static string NumberToWords(Int64 number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }
    //suraj
    public bool validfunctioncharnumberspacedot(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[0-9a-zA-Z\s\.]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;

            }
        }
        return x;
    }
    public bool validfunctionnumberdot(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[0-9\.]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;

            }
        }
        return x;
    }
    public bool ValidFunctionDropDown(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[0-9]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;

            }
        }
        return x;


    }
    public bool ValidFunctionDropDownCrop(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;

            }
        }
        return x;



    }
    public bool ValidFunctionDropDownCategory(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[a-zA-Z]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;


            }
        }
        return x;


    }
    public bool ValidFunctionOnlyNumber(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[0-9]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;

            }
        }
        return x;


    }
    public bool ValidFunctionOnlyCharacter(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[a-zA-Z \.]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;

            }
        }
        return x;


    }
    public bool validfunctioncharnumber(string[] Value, int MaxLn, int MinLn2)
    {
        bool x = true;
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length > MaxLn)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Length < MinLn2)
            {
                x = false;
                return x;
            }
            else
            {
                x = true;
            }
        }

        for (int i = 0; i < Value.Length; i++)
        {


            Regex objAlphaPattern = new Regex(@"^[0-9a-zA-Z]+$");
            if (x = objAlphaPattern.IsMatch(Value[i].ToString()) == true)
            {
                x = true;

            }
            else
            {
                x = false;
                return x;

            }
        }
        return x;
    }


    public DataTable parameter_dtpassing(string query, string[] param, object[] values)
    {
        if (cn.State == ConnectionState.Closed)
        {
            cn.Open();
        }
        cm = new SqlCommand(query, cn);
        cm.CommandType = CommandType.Text;
        for (int i = 0; i < param.Length; i++)
        {
            cm.Parameters.AddWithValue(param[i], values[i]);

        }
        da = new SqlDataAdapter(cm);
        cn.Close();
        DataTable dt = new DataTable();

        da.Fill(dt);
        return dt;
    }

    public static DataSet param_passing_text(string q)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OSSOPCAConnectionString"].ToString());
        try
        {
            con.Open();
            SqlCommand cm = new SqlCommand(q, con);
            cm.CommandType = CommandType.Text;
            cm.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Close();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            //ExceptionHandler.WriteEx(ex);
            if (con.State != ConnectionState.Closed)
                con.Close();
            return null;
        }
    }
}


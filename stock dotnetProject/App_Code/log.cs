using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web.Configuration;
public class log
{
    public void auditlog(string ip, string uid, string url, string devicetype, string model, string manufacture, string os, string bwser, string screensize, string action,
    string attack, string param, string mode)
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
        SqlCommand cmd = new SqlCommand("insert into AUDIT_TRAIL values(@IP,@UID,@URL,@DEVICETYPE, @MODEL, @MANUFACTURE, @OS, @BWSER, @SCREENSIZE, convert(datetime,@DT,103),@AC, @ATTACK, @PARAM, @MODE)", con);

        cmd.Parameters.AddWithValue("@IP", ip);
        cmd.Parameters.AddWithValue("@UID", uid.ToString());
        cmd.Parameters.AddWithValue("@URL", url);

        if (devicetype == "False")
        {
            devicetype = "PC";
        }
        else
        {
            devicetype = "MOB";
        }
        cmd.Parameters.AddWithValue("@DEVICETYPE", devicetype);


        cmd.Parameters.AddWithValue("@MODEL", model);
        cmd.Parameters.AddWithValue("@MANUFACTURE", manufacture);
        cmd.Parameters.AddWithValue("@OS", os);
        cmd.Parameters.AddWithValue("@BWSER", bwser);
        cmd.Parameters.AddWithValue("@SCREENSIZE", screensize);
        cmd.Parameters.AddWithValue("@DT", System.DateTime.Now.ToString());
        cmd.Parameters.AddWithValue("@AC", action);
        cmd.Parameters.AddWithValue("@ATTACK", attack);
        cmd.Parameters.AddWithValue("@PARAM", param);
        cmd.Parameters.AddWithValue("@MODE", mode);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


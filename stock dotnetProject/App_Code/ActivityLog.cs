using DataAccessLayer;
using System;
using System.Configuration;
using System.Data;

public class ActivityLog
{
    private string _UserID;

    private string _OS;

    private string _BWSER;

    private string _UserIP;

    private string _ActivityType;


    private string _Activity;

    private string _PageURL;

    private string _Remark;

    public string Activity
    {
        get
        {
            return this._Activity;
        }
        set
        {
            this._Activity = value;
        }
    }

    public string ActivityType
    {
        get
        {
            return this._ActivityType;
        }
        set
        {
            this._ActivityType = value;
        }
    }

    public string PageURL
    {
        get
        {
            return this._PageURL;
        }
        set
        {
            this._PageURL = value;
        }
    }

    public string Remark
    {
        get
        {
            return this._Remark;
        }
        set
        {
            this._Remark = value;
        }
    }

    public string UserID
    {
        get
        {
            return this._UserID;
        }
        set
        {
            this._UserID = value;
        }
    }

    public string OS
    {
        get
        {
            return this._OS;
        }
        set
        {
            this._OS = value;
        }
    }

    public string BWSER
    {
        get
        {
            return this._BWSER;
        }
        set
        {
            this._BWSER = value;
        }
    }

    public string UserIP
    {
        get
        {
            return this._UserIP;
        }
        set
        {
            this._UserIP = value;
        }
    }

    public ActivityLog()
    {
    }

    //Code For STOCK--DBT
    public static void DeleteTempLog_Stock(string SessionID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat("DELETE FROM STOCK_TempLoginStatus WHERE SessionID='", SessionID, "'"));
        dBManager.Close();
    }
    public static int InsertActivityLog_Stock(ActivityLog activity)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        dBManager.CreateInParameters(8);
        dBManager.AddInParameters(0, "@UserID", activity.UserID);
        dBManager.AddInParameters(1, "@UserIP", activity.UserIP);
        dBManager.AddInParameters(2, "@ActivityType", activity.ActivityType);
        dBManager.AddInParameters(3, "@Activity", activity.Activity);
        dBManager.AddInParameters(4, "@PageURL", activity.PageURL);
        dBManager.AddInParameters(5, "@Remark", activity.Remark);
        dBManager.AddInParameters(6, "@OS", activity.OS);
        dBManager.AddInParameters(7, "@BWSER", activity.BWSER);
        dBManager.CreateOutParameters(1);
        dBManager.AddOutParameters(0, "@retVal", 2, 5);
        dBManager.ExecuteNonQuery(CommandType.StoredProcedure, "Stock_SP_InsertActivityLog");
        int num = Convert.ToInt32(dBManager.outParameters[0].Value);
        dBManager.Close();
        return num;
    }
    public static void InsertTempLog_Stock(string SessionID, string value, string IP)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "INSERT INTO STOCK_TempLoginStatus (SessionID,Status,UserIP) VALUES ('", SessionID, "', '", value, "', '", IP, "')" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }
    public static void RemoveLog_Stock(string UserID, string SessionID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "UPDATE STOCK_Users SET Login_Status='Logout',SessionID=NULL WHERE SessionID='", SessionID, "' AND UserID='", UserID, "'" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }

    public static void UpdateLog_Stock(string UserID, string SessionID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "UPDATE STOCK_Users SET Login_Status='Login',SessionID='", SessionID, "' WHERE UserID='", UserID, "'" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }
    public static void UpdateTempLog_Stock(string SessionID, string value)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] strArrays = new string[] { "UPDATE STOCK_TempLoginStatus SET Status='", value, "' WHERE SessionID='", SessionID, "'" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(strArrays));
        dBManager.Close();
    }

    public static int InsertLoginAccess_Fert(ActivityLog activity)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        dBManager.CreateInParameters(4);
        dBManager.AddInParameters(0, "@UserID", activity.UserID);
        dBManager.AddInParameters(1, "@UserIP", activity.UserIP);
        dBManager.AddInParameters(2, "@OS", activity.OS);
        dBManager.AddInParameters(3, "@BWSER", activity.BWSER);
        dBManager.CreateOutParameters(1);
        dBManager.AddOutParameters(0, "@retVal", 2, 5);
        dBManager.ExecuteNonQuery(CommandType.StoredProcedure, "Stock_SP_InsertLoginAccess");
        int num = Convert.ToInt32(dBManager.outParameters[0].Value);
        dBManager.Close();
        return num;
    }
    public static void RemoveLoginAccess_Fert(string UserID, string AccessID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "UPDATE LoginAccess SET Login_Status='Logout' WHERE  UserID='", UserID, "' AND AccessID='", AccessID, "'" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }
    public static void RemoveLoginAccess_Fert1(string UserID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "UPDATE LoginAccess SET Login_Status='Logout' WHERE  UserID='", UserID, "' " };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }
    public static void InsertLoginSession(string SessionID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "INSERT INTO [dbo].[LoginSession] (SessionID) VALUES ('", SessionID, "')" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }
    public static void UpdateLoginSession(string UserID, string SessionID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "UPDATE [dbo].[LoginSession] SET [UserId]='", UserID, "' WHERE [SessionID]='", SessionID, "'" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }
    public static void DeleteLoginSession(string SessionID)
    {
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "DELETE [dbo].[LoginSession] WHERE [SessionID]='", SessionID, "'" };
        dBManager.ExecuteNonQuery(CommandType.Text, string.Concat(sessionID));
        dBManager.Close();
    }
    public static string GetLoginUser(string SessionID)
    {
        string STR = string.Empty;
        IDBManager dBManager = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
        dBManager.Open();
        string[] sessionID = new string[] { "SELECT [UserId] FROM [dbo].[LoginSession] WHERE [SessionID]='", SessionID, "'" };
        DataSet dS1 = dBManager.ExecuteDataSet(CommandType.Text, string.Concat(sessionID));
        DataTable DT = dS1.Tables[0];
        if (DT.Rows.Count > 0)
        {
            STR = DT.Rows[0]["UserId"].ToString();
        }
        dBManager.Close();
        return STR;
    }
}
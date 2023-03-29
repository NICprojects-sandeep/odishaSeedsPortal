<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        Dictionary<string, HttpSessionState> sessionData =
                   new Dictionary<string, HttpSessionState>();
        Application["s"] = sessionData;

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Dictionary<string, HttpSessionState> sessionData =
          (Dictionary<string, HttpSessionState>)Application["s"];

        if (sessionData.Keys.Contains(HttpContext.Current.Session.SessionID))
        {
            sessionData.Remove(HttpContext.Current.Session.SessionID);
            sessionData.Add(HttpContext.Current.Session.SessionID,
                            HttpContext.Current.Session);
            ActivityLog.InsertLoginSession(HttpContext.Current.Session.SessionID);
        }
        else
        {
            sessionData.Add(HttpContext.Current.Session.SessionID,
                            HttpContext.Current.Session);
            ActivityLog.InsertLoginSession(HttpContext.Current.Session.SessionID);
        }
        Application["s"] = sessionData;
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        string UserID = ActivityLog.GetLoginUser(this.Session.SessionID);
        ActivityLog.RemoveLog_Stock(UserID, this.Session.SessionID);
        ActivityLog.RemoveLoginAccess_Fert1(UserID);
        ActivityLog.UpdateTempLog_Stock(this.Session.SessionID, "Logout Option Clicked by User");
        ActivityLog.DeleteLoginSession(this.Session.SessionID);

        Dictionary<string, HttpSessionState> sessionData =
           (Dictionary<string, HttpSessionState>)Application["s"];
        sessionData.Remove(this.Session.SessionID);
        Application["s"] = sessionData;
    }
       
</script>

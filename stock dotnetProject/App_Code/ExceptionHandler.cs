using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Globalization;


public class ExceptionHandler
{
	public ExceptionHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void WriteException(string errMessage)
    {
        string logFile = String.Empty;
        try
        {
            logFile = ConfigurationManager.AppSettings["ErrorLog"].ToString();
            string path = logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                string remoteIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                sw.WriteLine("\r\nLog Entry : ");
                sw.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error in " + System.Web.HttpContext.Current.Request.Url.ToString() + "\r\nError Message : " + errMessage;
                sw.WriteLine(err);
                //
                sw.WriteLine(remoteIP);
                //
                sw.WriteLine("_____________________");
                sw.Flush();
                sw.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(errMessage);
        }
    }

    public static void WriteEx(Exception exception)
    {
        string logFile = String.Empty;
        StreamWriter logWriter;
        try
        {
            logFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ErrorLog"].ToString()) + "ExTraceLog.txt";
            if (File.Exists(logFile))
                logWriter = File.AppendText(logFile);
            else
                logWriter = File.CreateText(logFile);
            logWriter.WriteLine("=>" + DateTime.Now + " " + " An Error occured : " + exception.StackTrace + " Message : " + exception.Message + "\n\n");
            logWriter.Close();
        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(exception.Message);
        }
    }

    public static string CreateErrorMessage(Exception ApplException)
    {
        StringBuilder message = new StringBuilder();
        try
        {
            message.Append("Exception :: " + ApplException.ToString());
            if (ApplException.InnerException != null)
            {
                message.Append("Inner Exception :: " + ApplException.InnerException.ToString());
            }
            return message.ToString();
        }
        catch
        {
            message.Append("Exception :: Unknown Exception");
            return message.ToString();
        }
    }

    public static void WriteLog(string message)
    {
        FileStream fileStream = null;
        StreamWriter streamWriter = null;
        string logFile = String.Empty;
        try
        {
            logFile = ConfigurationManager.AppSettings["ErrorLog"].ToString();
            string path = logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
            if (path.Equals(""))            // RETURN IF PATH DOES NOT EXIST
                return;
            // CREATE LOG FILE DIRECTORY IF IT DOES NOT EXIST
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo = new FileInfo(path);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) 
                logDirInfo.Create();
            //
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                string remoteIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                sw.WriteLine("\r\nLog Entry : ");
                sw.WriteLine("Remote IP : " + remoteIP);
                sw.WriteLine("{0}", "Date/Time Error Occured : " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                sw.WriteLine(message);
                sw.WriteLine("_____________________");
                sw.Flush();
                sw.Close();
            }
        }
        finally
        {
            if (streamWriter != null)
                streamWriter.Close();
            if (fileStream != null)
                fileStream.Close();
        }
    }
}

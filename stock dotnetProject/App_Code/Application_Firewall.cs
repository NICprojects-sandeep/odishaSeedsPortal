using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace WAF
{

    	//'This class intercept each response and monitor the parameters for malicious output if found then rectify or drop the packet redirecting to WAF error page.
	//'This class scans for email id in **@**.** format and rectify it with suitable solution with **[AT]**[DOT]**
	public class monitor_response : System.IO.Stream
	{
		private System.IO.Stream Base;
        private Stream _sink;
		public monitor_response(System.IO.Stream ResponseStream)
		{
			if (ResponseStream == null)
				throw new ArgumentNullException("ResponseStream");
			this.Base = ResponseStream;
		}

		public override bool CanRead {

            get 
            { return CanRead; }
           
		}

		public override bool CanSeek {

            get {return CanSeek; }
		}

		public override bool CanWrite {

            get { return CanWrite; }
		}


		public override void Flush()
		{
		}

		public override long Length {

            get { return Length; }
		}

		public override long Position {

            get { return Position; }

			set { }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.Base.Read(buffer, offset, count);
		}

        public override long Seek(long offset, System.IO.SeekOrigin direction)
		{
            return _sink.Seek(offset, direction);
		}


		public override void SetLength(long value)
		{
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			// Get HTML code 
			string HTML = System.Text.Encoding.UTF8.GetString(buffer, offset, count);
			string MatchEmailPattern = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			Regex rx = new Regex(MatchEmailPattern);
			// Find matches.
			MatchCollection matches = rx.Matches(HTML);
                    
			
			int noOfMatches = matches.Count;
            if (HttpContext.Current.Response.ContentType == "text/html")
            {
                foreach (Match m in matches)
                {
                    HTML = HTML.Replace(m.Value, m.Value.ToString().Replace("@", "[AT]").Replace(".", "[DOT]"));
                }
            }

			MatchCollection matches_reverse = default(MatchCollection);
			
			string input_pattern = "<input.*?.*?/>";
			Regex rx1 = new Regex(input_pattern);
			matches_reverse = rx1.Matches(HTML);


			foreach (Match m1 in matches_reverse) {
				
                if (HttpContext.Current.Response.ContentType == "text/html")
                { 
                    HTML = HTML.Replace(m1.Value, m1.Value.ToString().Replace("[AT]", "@").Replace("[DOT]", "."));
                    if (!m1.Value.Contains("type=\"hidden\"")) {
					    HTML = HTML.Replace(m1.Value, m1.Value.Replace("/>", "autocomplete=off />"));
				    }
                }
			}

			string script_add_head_tag = "<script type='text/javascript'> history.go(1); </script> <script type='text/javascript'> if (!navigator.onLine) { window.location = 'Default_error.aspx';} </script> </head>";

			HTML = HTML.Replace("</head>", script_add_head_tag);

			buffer = System.Text.Encoding.UTF8.GetBytes(HTML);

			this.Base.Write(buffer, 0, buffer.Length);
		}
	}
	//This class intercept each requests and monitor the parameters for malicious signature if found then gives a WAF error.
    public class monitor_request : IHttpModule, IRequiresSessionState
    {
        log log = new log();
        string csrf = "";
        string csrf_key = "";
        //Defines the set of characters that will be checked.
        //You can add to this list, or remove items from this list, as appropriate for your site
        public static string[] blackList = {
            "admin'--",
            "' or 1=1--",
            "' union select 1, 'Eyeless', 'ez2do', 1--",
            "administrator'--",
            "superuser'--",
            "test'--",
            "' or 0=0 --",
            "' or 0=0 --'",
            "' or 0=0 # ",
            "or 0=0 --",
            "' or 0=0 #",
            "or 0=0 #",
            "' or 'x'='x ",
            "or 1=1--",
            "' or a=a--'",
            "' or a=a #",
            "' or a=a--",
            "' or 'a'='a",
            "') or ('a'='a",
            "hi' or 1=1 --",
            "hi' or 'a'='a",
            "hi') or ('a'='a",
            "' or 1=1--",
            "'or 1=1;--",
            "' or 'a'='a",
            "') or ('a'='a",
            "union",
            "select *",
            "union all"
        };
        HttpApplication application;
        public void Dispose()
        {
            //no-op 
        }

        //Tells ASP.NET that there is code to run during BeginRequest
        public void Init(HttpApplication app)
        {
            app.BeginRequest += app_BeginRequest;
            app.PostAcquireRequestState += app_AcquireRequestState;
            //AddHandler app.PostAuthenticateRequest, AddressOf app_AcquireRequestState
            application = app;

        }

        //For each incoming request, check the query-string, form and cookie values for suspicious values.
        private void app_BeginRequest(object sender, EventArgs e)
        {
            HttpRequest Request = (sender as HttpApplication).Context.Request;


            if (dos_detect.isDoS() == true) {
                log.auditlog(Request.UserHostAddress.ToString(), "ANO", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "DoS",
                "ATTACK", "NA", "QUERYSTRING");
                HttpContext.Current.Response.Redirect("~/Custom_errors/DoSAttack.aspx.aspx");
            }

            //& Request.Url.AbsolutePath.ToString().Contains(".aspx")
            if (Request.Cookies["Auth_Token"] == null & Request.Url.AbsolutePath.ToString().Contains(".aspx")) {
                log.auditlog(Request.UserHostAddress.ToString(), "ANO", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "VISIT",
                "NORMAL", "NA", "NA");
            }
            if ((Request.Url.AbsolutePath.ToString().Contains(".aspx"))) {
                application.Response.Filter = new monitor_response(application.Response.Filter);
            }

            foreach (string key in Request.QueryString) {
                CheckInput(Request.QueryString[key], Request.UserHostAddress.ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "QUERYSTRING");
            }
            foreach (string key in Request.Form)
            {
                if (key !=null && key.Contains("CSRF"))
                {

                    csrf_key = key;
                }

                CheckInput(Request.Form[key], Request.UserHostAddress.ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "FORMPOST");
            }
            foreach (string key in Request.Cookies) {
                CheckInput(Request.Cookies[key].Value, Request.UserHostAddress.ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "COOKIE");
            }

            //if (File.Exists(application.Server.MapPath(Request.Url.AbsolutePath.Replace("%20"," "))))
            //{
            //}
            //else
            //{
            //    HttpContext.Current.Response.Redirect("ErrorPages/DefaultErrorPage.aspx");
            //    //~/Custom_errors/CSRFerror.aspx
            //}
        }

        //The utility method that performs the blacklist comparisons
        //You can change the error handling, and error redirect location to whatever makes sense for your site.
        private void CheckInput(string parameter, string IP, string url, string devicetype, string model, string manufacture, string os, string browser, string screensize, string mode)
        {
            for (int i = 0; i <= blackList.Length - 1; i++) {
                if ((parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0)) {
                    // 
                    //Handle the discovery of suspicious Sql characters here 
                    // 
                    //generic error page on your site 

                    log.auditlog(IP, "ANO", url, devicetype, model, manufacture, os, browser, screensize, "SQL-INJ",
                    "ATTACK", parameter, mode);

                    HttpContext.Current.Response.Redirect("~/Custom_errors/waf.aspx");
                }
            }
        }

        private void app_AcquireRequestState(object sender, EventArgs e)
        {
            HttpRequest Request = (sender as HttpApplication).Context.Request;
            //Broken Authentication & Session Management Server side session checking for authenticated pages
         if (Request.Url.AbsolutePath.ToString().Contains(".aspx"))
          {
                
            if ((Request.Cookies["Auth_Token"] != null) && (Request.Cookies["NICSec_AuthToken"] != null))
            {
                if ((application.Session["NICSec_AuthToken"] != null))
                {
                    if (((application.Session["NICSec_AuthToken"].ToString().Equals(Request.Cookies["NICSec_AuthToken"].Value) == false)))
                    {
                        // log.auditlog("ANO", Request.UserHostAddress.ToString, Request.Browser.Type.ToString, Request.Url.AbsolutePath.ToString, "CSRFAttack")

                        log.auditlog(Request.UserHostAddress.ToString(), "ANO", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "CSRF",
                        "ATTACK", "NA", "COOKIE");
                        HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl.ToString());
                    }
                    else
                    {
                        if (FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name == application.Session["uid"].ToString())
                        {
                        }
                        else
                        {
                            log.auditlog(Request.UserHostAddress.ToString(), "ANO", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "CSRF", "ATTACK", "NA", "COOKIE");
                            HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl.ToString());
                        }
                    }
                }
                else
                {
                    log.auditlog(Request.UserHostAddress.ToString(), "ANO", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), "CSRF",
                    "ATTACK", "NA", "COOKIE");
                    HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl.ToString());
                }
                ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                if (application.Session[application.Request.Url.AbsolutePath] == null)
                {
                    Random rn = new Random();
                    application.Session[Request.Url.AbsolutePath.ToString()] = rn.Next();
                    // Request.Form[csrf_key] = application.Session[Request.Url.AbsolutePath.ToString()].ToString();


                    HttpApplication ap = (HttpApplication)sender;
                    HttpContext context = application.Context;
                    if (context.Handler != null)
                    {


                        Page page = context.Handler as Page;

                        page.ClientScript.RegisterHiddenField("CSRF", application.Session[Request.Url.AbsolutePath.ToString()].ToString());
                    }

                }

                else
                {
                    if (Request.Form[csrf_key] != null)
                    {



                        if (Request.Form[csrf_key].ToString() != "")
                        {

                            if (application.Session[application.Request.Url.AbsolutePath.ToString()].ToString() == Request.Form[csrf_key].ToString())
                            {

                                HttpApplication ap = (HttpApplication)sender;
                                HttpContext context = application.Context;
                                if (context.Handler != null)
                                {


                                    Page page = context.Handler as Page;

                                    page.ClientScript.RegisterHiddenField("CSRF", application.Session[Request.Url.AbsolutePath.ToString()].ToString());
                                }
                            }
                            else
                            {
                                if (Request.Url.AbsolutePath.ToString().Replace("%20", " ").Contains(FormsAuthentication.LoginUrl.ToString()) == false)
                                {
                                    HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl.ToString());

                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (Request.Url.AbsolutePath.ToString().Replace("%20", " ").Contains(FormsAuthentication.LoginUrl.ToString()) == false)
                            {
                                HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl.ToString());

                            }
                            else
                            {
                                return;
                            }

                        }

                    }
                    else
                    {
                        //if (Request.Url.AbsolutePath.ToString().Replace("%20", " ").Contains(FormsAuthentication.LoginUrl.ToString()) == false)
                        //{
                        //    HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl.ToString());

                        //}
                        //else
                        //{
                        //    return;
                        //}
                    }
                }

            }
            }
        }
    }


	public sealed class dos_detect
	{
		private const double DURATION = 1;
		private const int ALLOWED_HITS = 2;
		public static bool isDoS()
		{
			HttpContext context = HttpContext.Current;
			string key = context.Request.UserHostAddress.ToString();
			int number_hits = 0;
			if (context.Cache[key] == null) {
				number_hits = number_hits + 1;
			} else {
				number_hits = int.Parse(context.Cache[key].ToString());
			}
			if (number_hits > ALLOWED_HITS) {
				return true;
			} else {
				number_hits = number_hits + 1;
			}

			if (number_hits == 1) {
				context.Cache.Add(key, number_hits, null, DateTime.Now.AddMinutes(DURATION), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
			}
			return false;
		}
	}

}

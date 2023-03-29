using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Web;
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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Policy;

using System.Configuration;

/// <summary>
/// Summary description for BLL_Dealer
/// </summary>
public class BLL_Dealer
{
    static HttpWebRequest request;
    public BLL_Dealer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string F_Year { get; set; }
    public string STATUS { get; set; }
    public string LICENCENO { get; set; }
    public string CROP_ID { get; set; }
    public string SESSION { get; set; }
    public string CROPCATG_ID { get; set; }
    public string CROP_VERID { get; set; }
    public string CROP_CLASS { get; set; }
    public string USER_TYPE { get; set; }
    public string FARMER_ID { get; set; }
    public string DIST_CODE { get; set; }
    public string DAO_CD { get; set; }
    public string XMLVALUE { get; set; }
    public string UPDATED_BY { get; set; }
    public string USERIP { get; set; }
    public string TRANSACTION_ID { get; set; }
    public string RECEIVE_UNITCD { get; set; }
    public string FDate { get; set; }
    public string TDate { get; set; }
    public string APPEMAIL_ID { get; set; }
    public string SEED_LIC_DIST_ID { get; set; }
    public string REF_NO { get; set; }
    public string FIN_YR { get; set; }
    public string SEASON { get; set; }
    public string BANK_CODE { get; set; }
    public string BRANCH_CODE { get; set; }
    public string AADHAAR_NO { get; set; }
    public string ACC_HOLDERNAME { get; set; }
    public string ACC_NO { get; set; }
    public string IFSC_CODE { get; set; }
    public string CASH_MEO_NO { get; set; }
    public String COUPON_CODE { get; set; }

    private string mob;
    private string msg;
    public string Mob
    {

        get
        {
            return mob;

        }
        set
        {
            mob = value;
        }
    }
    public string Msg
    {

        get
        {
            return msg;

        }
        set
        {
            msg = value;
        }
    }
    private string stype;
    public string SType
    {

        get
        {
            return stype;

        }
        set
        {
            stype = value;
        }
    }
    private string tempid;
    public string TempId
    {

        get
        {
            return tempid;

        }
        set
        {
            tempid = value;
        }
    }
    private string smsmode;
    public string SMSMode
    {
        get
        {
            return smsmode;
        }

        set
        {
            smsmode = value;
        }

    }

    public string Send_OTP_SMS(BLL_Dealer objUserBELDIST)
    {


        string RevertText = "-- Invalid mobile number --";

        try
        {

            if (objUserBELDIST.Mob.Length >= 10)
            {
                string template_id = string.Empty;

                template_id = objUserBELDIST.tempid;


                string senderid = "AGRIOR";
                string uNameSms = "agriodisha.sms";
                string uNameOtp = "agriodisha.otp";
                //string pwdS = "Al@#gh883";
                //string pwdO = "Au@#gh883";
                string pwdS = "Al%40%23gh883";
                string pwdO = "Ku%40%23gh883";
                string entity_id = "1101606950000043751";

                //Response.Write("SMS sent successfully");
                string createdURL = "";
                if (objUserBELDIST.SType == "O")
                {
                    createdURL = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uNameOtp + "&pin=" + pwdO + "&message='" + objUserBELDIST.Msg + "'&mnumber=" + objUserBELDIST.Mob + "&signature=" + senderid + "&dlt_entity_id=" + entity_id + "&dlt_template_id=" + template_id + "";
                }
                else
                {

                    createdURL = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uNameSms + "&pin=" + pwdS + "&message='" + objUserBELDIST.Msg + "'&mnumber=" + objUserBELDIST.Mob + "&signature=" + senderid + "&dlt_entity_id=" + entity_id + "&dlt_template_id=" + template_id + "";
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);

                request = (HttpWebRequest)WebRequest.Create(createdURL);

                //   ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
                request.Method = "POST";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string ver = response.ProtocolVersion.ToString();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string ss = reader.ReadToEnd();
                response.Close();
                RevertText = ss;



            }

            else
            {

                RevertText = "-- Invalid mobile number --";

            }
        }
        catch (Exception ex)
        {
            // Response.Write(ex.ToString());
            RevertText = "-- Remote site server is not responding. --";
        }
        return RevertText;


    }
    public static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        if (((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors))
        {
            return true;
        }
        else if (((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch))
        {
            Zone z = default(Zone);
            z = Zone.CreateFromUrl(((HttpWebRequest)sender).RequestUri.ToString());
            if ((z.SecurityZone == System.Security.SecurityZone.Intranet | z.SecurityZone == System.Security.SecurityZone.MyComputer))
            {
                return true;
            }
            return true;
        }
        else
        {
            return true;
        }
        //return true;
    }
    public string OTP { get; set; }
}
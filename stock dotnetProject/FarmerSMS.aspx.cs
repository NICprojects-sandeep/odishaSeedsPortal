using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FarmerSMS : System.Web.UI.Page
{
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();

    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSMS_Click(object sender, EventArgs e)
    {
        
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.GetMobileSMS();
        if (ds != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                CallMyProcess(ds.Tables[0].Rows[i]["VCHMOBILENO"].ToString(), ds.Tables[0].Rows[i]["NICFARMERID"].ToString(), ds.Tables[0].Rows[i]["NAME"].ToString());
               
            }
        }
    }
    public static async void CallMyProcess(string mob, string farmerid, string farmeranme)
    {        
        string j = await BroadcastSMS(mob, "POST", farmerid,farmeranme);
        string[] X = j.Split('/');
        BLL_DropDown objUserBEL = new BLL_DropDown();
        objUserBEL.MOBSTATUS = X[0] + X[1].ToString();
        objUserBEL.MOBNO = X[1].ToString();
        DLL_DropDown objUserDLL = new DLL_DropDown();
        objUserDLL.UpdateMobSMS(objUserBEL);
        //Console.WriteLine("Time" + j + " Consuming Process end");
    }

    public static Task<string> BroadcastSMS(string mobileNo, string SMSMode, string farmerid, string farmeranme)
    {
        return Task.Run(() =>
        {
        string userId = "egovodisha";

        string authCode = "7823915";

        string RevertText = "-- Invalid mobile number --";

        try
        {
            if (mobileNo.Length >= 10)
            {
               
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://mkisan.gov.in/ksewa/ksewa.aspx");
                request.Timeout = 900000;
                request.ReadWriteTimeout = 900000;
                request.ProtocolVersion = HttpVersion.Version10; request.UserAgent = ".NET Framework Example Client";

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows98; DigExt)";

                request.Method = "POST";
                //string xxx = mobileNo;
                string txtMsg = "Dear " + farmeranme + " Your New Farmer ID : " + farmerid + "/" + mobileNo;
                string[] X = txtMsg.Split('/');
                String query = "" + "txtMsg=" + System.Uri.EscapeDataString(txtMsg) +

                "&mobileNo=" + System.Uri.EscapeDataString("9861790232") +

                "&SMSMode =" + System.Uri.EscapeDataString(SMSMode) +

                "&userId=" + System.Uri.EscapeDataString(userId) +

                "&authCode=" + System.Uri.EscapeDataString(authCode);
                byte[] byteArray = Encoding.ASCII.GetBytes(query);

                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();

                dataStream.Write(byteArray, 0, byteArray.Length);

                dataStream.Close();

                WebResponse response = request.GetResponse();
                String Status = ((HttpWebResponse)response).StatusDescription;

                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                reader.Close();

                dataStream.Close();

                response.Close();
                RevertText = responseFromServer;
            }

            else
            {

                RevertText = "-- Invalid mobile number --";

            }
        }
        catch (Exception ex)
        {
            // Response.Write(ex.ToString());
            //RevertText = "-- Remote site server is not responding. --";
        }
        return RevertText + "/" + mobileNo;
        });

    }

}
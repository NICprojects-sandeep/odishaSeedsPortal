using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Text;
using System.Security.Cryptography;

public partial class Masters_TestPage : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
    CheckCredentials objUserBEL1;
    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDLL = new DLL_DropDown();
        int Cnt = 1;        
        if (Cnt == 1)
        {
            if (this.Session["UserID"] == null || this.Session["AuthToken"] == null || base.Request.Cookies["AuthToken"] == null)
            {
                this.utl.SessionReset();
            }
            else
            {
                if (!this.Session["AuthToken"].ToString().Equals(base.Request.Cookies["AuthToken"].Value))
                {
                    this.utl.SessionReset();
                    return;
                }
                if (!(this.Session["UserType"].ToString() == "OAIC") && !(this.Session["UserType"].ToString() == "OSSC") || !(this.Session["FirstLogin"].ToString() == "N"))
                {
                    base.Response.Redirect("../Unauthorised.aspx", true);
                    return;
                }
                if (!IsPostBack)
                {
                    

                    if (Session["distcode"] == null)
                    {
                        Response.Redirect("../DBTHome.aspx");
                    }
                    else
                    {
                        objUserBEL1 = new CheckCredentials();
                        objUserBEL1.spo_code = Session["UID"].ToString();
                        objUserBEL1.spo_name = Session["FullName"].ToString();//row["LOT_NO"].ToString();
                        objUserBEL1.token = ConvertToSHA512(Session["UID"].ToString() + Session["FullName"].ToString() + "Sikun@1234");
                        SendReceiveDetails(objUserBEL1);
                        
                    }
                }
            }
        }
        else
        {            
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Online seed transactions for Kharif 2018 has been closed.'); window.location='" + Request.ApplicationPath + "/Masters/Home.aspx';", true);
        }
    }
    //CheckCredentials objUserBEL1;
    //protected void btnEgency_Click(object sender, EventArgs e)
    //{
    //    objUserBEL1 = new CheckCredentials();
    //    objUserBEL1.spo_code = Session["UID"].ToString();
    //    objUserBEL1.spo_name = Session["FullName"].ToString();//row["LOT_NO"].ToString();
    //    objUserBEL1.token = ConvertToSHA512(Session["UID"].ToString() + Session["FullName"].ToString() + "Sikun@1234");
    //    SendReceiveDetails(objUserBEL1);
    //}
    
    public void SendReceiveDetails(CheckCredentials objUserBEL1)
    {
        try
        {
            string apiUrl = "http://164.100.140.107/auth/sisSpoLogin";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            var json = JsonConvert.SerializeObject(objUserBEL1);// + ConvertToSHA512(objUserBEL1 + "Sikun@1234"));
            //var json = JsonConvert.SerializeObject(objUserBEL1);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                Response.Redirect("http://164.100.140.107/#/login?token=" + responseString);
            }
        }
        catch (Exception ex)
        {
            
        }        
    }

    private string ConvertToSHA512(string pass)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(pass);
        SHA512CryptoServiceProvider sha512hasstring = new SHA512CryptoServiceProvider();
        byte[] hash = sha512hasstring.ComputeHash(bytes);
        return ByteArrayToHexString(hash);
    }
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
        {
            hex.AppendFormat("{0:x2}", b);
        }

        return hex.ToString();
    }
}
public class CheckCredentials
{
    public string spo_code { get; set; }
    public string spo_name { get; set; }
    public string token { get; set; }
}
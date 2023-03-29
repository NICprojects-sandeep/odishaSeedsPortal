using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_PaymentDetailsReport : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();
    private UtilityLibrary utl = new UtilityLibrary();
    protected void Page_Load(object sender, EventArgs e)
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
            if (this.Session["UserType"].ToString() != "SEAD")
            {
                this.utl.SessionReset();
                return;
            }
            if (!this.Page.IsPostBack)
            {
                this.utl.SetSessionCookie();
                this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                return;
            }
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<Dist> DistName()
    {
        PFMSBAL1 pFMSBAL = new PFMSBAL1 ();
        List<Dist> list = pFMSBAL.GetDistrict();

        list.RemoveAll(x => x.DistName == "Select");
        return list;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<VMStatus> GetFarmerIDByDist(string shName, string remark)
    {
        List<string> farID = new List<string>();
        List<VMStatus> listStatus = new List<VMStatus>();
        PFMSBAL1 pFMSBAL = new PFMSBAL1();
        List<VMStatus> lst = new List<VMStatus>();

        if (remark == "ALL")
        {
            farID = pFMSBAL.GetFID(shName);
            //farCount = farID.Count;


            foreach (string f in farID)
            {
                lst = pFMSBAL.GetAllStatus(f);

                foreach (var v in lst)
                {
                    v.transDate = v.transDate.Substring(0, 10);
                    listStatus.Add(v);
                }
            }
        }

        else if (remark == "PAN")
        {
            farID = pFMSBAL.GetFID(shName);

            foreach (string f in farID)
            {

                lst = pFMSBAL.PendingAtNic(f);

                foreach (var v in lst)
                {
                    v.transDate = v.transDate.Substring(0, 10);
                    listStatus.Add(v);
                }
            }
        }
        else if (remark == "PAD")
        {
            farID = pFMSBAL.GetFID(shName);

            foreach (string f in farID)
            {

                lst = pFMSBAL.PendingAtDepartment(f);

                foreach (var v in lst)
                {
                    v.transDate = v.transDate.Substring(0, 10);
                    listStatus.Add(v);
                }
            }
        }
        else if (remark == "SFNPAP")
        {
            farID = pFMSBAL.GetFID(shName);

            foreach (string f in farID)
            {

                lst = pFMSBAL.SentFromNicPendingAtPFMS(f);

                foreach (var v in lst)
                {
                    v.transDate = v.transDate.Substring(0, 10);
                    listStatus.Add(v);
                }
            }
        }

        var listOrder = listStatus.OrderBy(x => x.transDate);
        return listOrder.ToList();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<VMStatus> GetFarmerID(string fID, string remark)
    {
        List<string> farID = new List<string>();
        List<VMStatus> listStatus = new List<VMStatus>();
        PFMSBAL1 pFMSBAL = new PFMSBAL1();
        List<VMStatus> lst = new List<VMStatus>();

        if (remark == "ALL")
        {
            lst = pFMSBAL.GetAllStatus(fID);

                foreach (var v in lst)
                {
                    v.transDate = v.transDate.Substring(0, 10);
                    listStatus.Add(v);
                }
        }

        else if (remark == "PAN")
        {
            lst = pFMSBAL.PendingAtNic(fID);

            foreach (var v in lst)
            {
                v.transDate = v.transDate.Substring(0, 10);
                listStatus.Add(v);
            }
        }
        else if (remark == "PAD")
        {
            lst = pFMSBAL.PendingAtDepartment(fID);

            foreach (var v in lst)
            {
                v.transDate = v.transDate.Substring(0, 10);
                listStatus.Add(v);
            }
        }
        else if (remark == "SFNPAP")
        {
            lst = pFMSBAL.SentFromNicPendingAtPFMS(fID);

            foreach (var v in lst)
            {
                v.transDate = v.transDate.Substring(0, 10);
                listStatus.Add(v);
            }
        }

        
        var listOrder = listStatus.OrderBy(x => x.transDate);
        return listOrder.ToList();
    }
}
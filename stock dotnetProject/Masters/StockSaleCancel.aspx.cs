using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

public partial class Masters_StockSaleCancel : System.Web.UI.Page
{
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlPopUp.Visible = false;
            tblDtls.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillSearchDtls();
    }
    private void FillSearchDtls()
    {
        tblDtls.Visible = false;
        string SUPPLY_TYPE = "";
        string SUPPLY_NAME = "";
        objUserBEL = new BLL_DropDown();
        objUserBEL.CashMemo_No = txtCashMemoNo.Text;
        objUserBEL.UPDATED_BY = Session["Name"].ToString();
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillCashMemoDtls(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                tblDtls.Visible = true;
                tdGODOWN_NAME.InnerText = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["GODOWN_NAME"].ToString());
                tdSALE_DATE.InnerText = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_DATE"].ToString());
                tdAPP_FIRMNAME.InnerText = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
                tdUPDATED_BY.InnerText = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["UPDATED_BY"].ToString());
                SUPPLY_NAME = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SUPPLY_NAME"].ToString());
                SUPPLY_TYPE = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["USER_TYPE"].ToString());
                gvCashMemoDtls.DataSource = ds;
                gvCashMemoDtls.DataBind();
            }
            else
            {
                string popp = "alert('Please Enter a Valid Cash Memo No!!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);                
                gvCashMemoDtls.DataSource = null;
                gvCashMemoDtls.DataBind();
                txtCashMemoNo.Text = "";
                txtCashMemoNo.Focus();
            }
        }
        else
        {
            string popp = "alert('Please Enter a Valid Cash Memo No!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            gvCashMemoDtls.DataSource = null;
            gvCashMemoDtls.DataBind();
            txtCashMemoNo.Text = "";
            txtCashMemoNo.Focus();
        }
        lblReceiver.Text = lblReceiverType.Text = SUPPLY_NAME;
        lblSeller.Text = lblSellerType.Text = SUPPLY_TYPE;
    }
    protected void lbtnSALETRANSID_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;
        btnCancel.Enabled = true;
        pnlPopUp.Visible = true;

        lblError.Text = "";
        LinkButton btnEdit = (LinkButton)sender;
        GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
        Label lblSALETRANSID = (Label)Grow.FindControl("lblSALETRANSID");

        lblSALETRANSID1.Text = lblSALETRANSID.Text;
        td_SALE_DATE.InnerHtml = HttpUtility.HtmlEncode(tdSALE_DATE.InnerHtml);
        td_APP_FIRMNAME.InnerHtml = HttpUtility.HtmlEncode(tdAPP_FIRMNAME.InnerHtml);
        td_GODOWN_NAME.InnerHtml = HttpUtility.HtmlEncode(tdGODOWN_NAME.InnerHtml);
        td_LOTNO.InnerHtml = HttpUtility.HtmlEncode(tdUPDATED_BY.InnerHtml);

        objUserBEL = new BLL_DropDown();
        objUserBEL.SALETRANSID = lblSALETRANSID.Text;
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillSaleTransDtls(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                tdBagSize1.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BAG_SIZE1"].ToString());
                tdNoofBags1.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_NO_OF_BAG1"].ToString());
                tdBagSize2.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BAG_SIZE_IN_KG2"].ToString());
                tdRcvNoofBags2.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["RECV_NO_OF_BAGS2"].ToString());
                tdAvlNoofBags2.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["AVL_NO_OF_BAGS"].ToString());
                tdBagSize3.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BAG_SIZE_IN_KG3"].ToString());
                tdRcvNoofBags3.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["RECV_NO_OF_BAGS3"].ToString());
                tdAvlNoofBags3.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["AVL_NO_OF_BAGS3"].ToString());
                tdBagSize4.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BAG_SIZE4"].ToString());
                tdRcvNoofBags4.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["NO_OF_BAGS4"].ToString());
                td_LOTNO.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["LOT_NO"].ToString());
                Session["cGODOWN_ID"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["GODOWN_ID"].ToString());
                Session["cSALE_TO"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_TO"].ToString());
                Session["cDD_NUMBER"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["DD_NUMBER"].ToString());
                Session["cLOT_NUMBER"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["LOT_NO"].ToString());
                Session["cBAG_SIZE_KG"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BAG_SIZE1"].ToString());
                Session["cSALE_NO_OF_BAG"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_NO_OF_BAG1"].ToString());
            }
        }
        if (Convert.ToInt32(tdNoofBags1.InnerHtml) > Convert.ToInt32(tdAvlNoofBags3.InnerHtml))
        {
            lblError.Text = "Due to insufficient " + lblReceiver.Text + " Stock the transaction can't be deleted";
            lblError.Visible = true;
            btnCancel.Enabled = false;
        }
        //this.ModalPopUp.Show();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.SALETRANSID = lblSALETRANSID1.Text;
        objUserBEL.UPDATED_BY = Session["UserID"].ToString();
        objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
        objUserDLL = new DLL_DropDown();
        int Val = 0;

        objUserDLL.CancelCashMemo(objUserBEL, out Val);
        if (Val == 1)//SUCCESS
        {
            cSendDetails objUserBEL1;
            objUserBEL1 = new cSendDetails();
            objUserBEL1.GODOWN_ID = Session["cGODOWN_ID"].ToString();
            objUserBEL1.SALE_TO = Session["cSALE_TO"].ToString();
            objUserBEL1.DD_NUMBER = Session["cDD_NUMBER"].ToString();
            objUserBEL1.LOT_NUMBER = Session["cLOT_NUMBER"].ToString();
            objUserBEL1.BAG_SIZE_KG = Session["cBAG_SIZE_KG"].ToString();
            objUserBEL1.SALE_NO_OF_BAG = Session["cSALE_NO_OF_BAG"].ToString();
            objUserBEL1.APIKEY = "key01002";
            SendSaleCancelDetails(objUserBEL1);

            string popp = "alert('Transaction Completed Successfull !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            FillSearchDtls();
        }
        else if (Val == 2)//NO OF BAG ISSUE
        {
            string popp = "alert('Oops Transaction Can Not be Deleted !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
        }
        else if (Val == 3)//UNSUCCESS
        {
            string popp = "alert('Oops Transaction Un-Successfull !!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
        }
        pnlPopUp.Visible = false;
    }

    public void SendSaleCancelDetails(cSendDetails objUserBEL1)
    {
        try
        {
            string apiUrl = "http://164.100.140.107/addSellStockbySIS/rejectSisSoldLot";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            var json = JsonConvert.SerializeObject(objUserBEL1);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(apiUrl, stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                //string pop1 = "alert('" + responseString + "');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
            }
        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
        }
        //return fdetails;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        pnlPopUp.Visible = true;
    }
}

public class cSendDetails
{
    public string GODOWN_ID { get; set; }
    public string SALE_TO { get; set; }
    public string DD_NUMBER { get; set; }
    public string LOT_NUMBER { get; set; }
    public string BAG_SIZE_KG { get; set; }
    public string SALE_NO_OF_BAG { get; set; }
    public string APIKEY { get; set; }//key01001
}
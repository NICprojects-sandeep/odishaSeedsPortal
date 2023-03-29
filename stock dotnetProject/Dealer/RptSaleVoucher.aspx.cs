using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Text;

public partial class Masters_RptSaleVoucher : System.Web.UI.Page
{
    DataSet ds;
    DataSet ds1;
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string TransactionNo = Request.QueryString["Tid"];
                string FarmerId = Request.QueryString["FId"];
                string LicenceNo = Request.QueryString["LicNo"];

                if (TransactionNo != null && FarmerId != null && LicenceNo != null)
                {
                    hfTransactionNo.Value = TransactionNo;
                    hfFarmerId.Value = FarmerId;
                    hfLicNo.Value = LicenceNo;

                    lbl_TransactionId.Text = hfTransactionNo.Value;
                    lbl_LicNo.Text = LicenceNo;
                    lbl_LicNo0.Text = LicenceNo;
                    GetFirmName();
                    lbl_recvdt.Text = "";
                    FillFarmerDtls();
                    FillTransactions();
                }
            }
        }
        catch (Exception ee)
        {
        }
    }
    private void GetFirmName()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = hfLicNo.Value;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFirmName(objUserBELDIST);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_LicName.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
                lbl_LicName0.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
            }
        }
    }
    private void FillTransactions()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = hfFarmerId.Value;
        objUserBELDIST.TRANSACTION_ID = hfTransactionNo.Value;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFarmerDtl(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_recvdt.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_DATE"].ToString());
                grd_stockdtls.DataSource = ds;
                grd_stockdtls.DataBind();
            }
        }

        ds = new DataSet();
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.TRANSACTION_ID = hfTransactionNo.Value;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFarmerInv(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                td_TotAmt.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["TOT_AMT"].ToString());
                td_SubAmt.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SUB_AMT"].ToString());
            }
        }
    }

    private void FillFarmerDtls()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = hfFarmerId.Value;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFarmerInvHdr(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                td_Name.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString());
                td_FName.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHFATHERNAME"].ToString());
                td_MobNo.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHMOBILENO"].ToString());
                td_Village.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["villg_name"].ToString());
                td_GP.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["GP_Name"].ToString());
                td_Block.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["BLOCK_NAME"].ToString());
                td_Dist.InnerHtml = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["Dist_Name"].ToString());
                lblAccNo.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["STARVCHACCOUNTNO"].ToString());
                lblBankName.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["VCHBANKNAME"].ToString());
            }
        }

        lblReceiver.Text = td_Name.InnerHtml;
    }
    protected void grd_stockdtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Receive_Unitname = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.RECEIVE_UNITCD = DataBinder.Eval(e.Row.DataItem, "RECEIVE_UNITCD").ToString();
            objUserBELDIST.SESSION = "K";
            objUserBELDIST.F_Year = "2022-23";
            objUserDLLDIST = new DLL_Dealer();
            string PRICE_RECEIVE_UNITCD = objUserDLLDIST.GetPRICE_RECEIVE_UNITCD(objUserBELDIST);

            if (PRICE_RECEIVE_UNITCD == "0000")
            {
                Receive_Unitname = DataBinder.Eval(e.Row.DataItem, "Receive_Unitname").ToString();//hfUserType.Value;
            }
            else
            {
                objUserBELDIST = new BLL_Dealer();
                objUserBELDIST.RECEIVE_UNITCD = PRICE_RECEIVE_UNITCD;
                objUserDLLDIST = new DLL_Dealer();
                Receive_Unitname = objUserDLLDIST.GetReceive_UnitnameByCode(objUserBELDIST);
            }
            Label lbl_Source1 = (Label)e.Row.FindControl("lbl_Source1");
            lbl_Source1.Text = Receive_Unitname;
        }
    }


    protected void btnSale_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dealer_Sale.aspx");
    }
}
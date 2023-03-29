using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

public partial class Masters_Stock_SaleEntry : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    DataTable dt;
    DataTable dt1;

    Regex RegDate = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[\/]\d{4}$");
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("../Maintainance.aspx");
        objUserDLL = new DLL_DropDown();

        int Cnt = objUserDLL.CheckDate();
        if (Cnt >= 0)// && ((Session["DistCode"].ToString() == "02") || (Session["DistCode"].ToString() == "09") || (Session["DistCode"].ToString() == "07")))
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
                    Random r = new Random();
                    int x = r.Next(0, 100);
                    lblHiddenText.Text = x.ToString();
                    Session["HiddenTextS"] = x.ToString();

                    if (Session["distcode"] == null)
                    {
                        Response.Redirect("../DBTHome.aspx");
                    }
                    else
                    {
                        this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                        lbl_dist.Text = Session["distcode"].ToString();
                        if (Session["UserType"].ToString() == "OAIC")
                        {
                            rbtn_stocksupplied.SelectedValue = "1";
                        }
                        else if (Session["UserType"].ToString() == "OSSC")
                        {
                            rbtn_stocksupplied.SelectedValue = "6";
                        }

                        tr_Scheme.Visible = false;
                        tr_CreditBillNo.Visible = false;
                        tr_Date.Visible = false;
                        tr_Select.Visible = false;
                        tr_SelectGoDown.Visible = true;
                        tr_Deliverydate.Visible = true;
                        tr_SaleTo.Visible = false;
                        tr_DDNumber.Visible = true;
                        tr_CashMemoNo.Visible = false;
                        tr_Amount.Visible = true;
                        ddlDist.Visible = false;
                        FillDistricts();
                        FillSupplyType();
                        pnlMainSection.Enabled = true;
                        pnlDelivery.Enabled = false;
                        pnlDelivery.Visible = false;
                        FILL_GODOWN();
                        //FOR OAIC PACS
                        CREATENEWOAICTABLE();//
                        AddOAICDists();
                        //
                        FILLSTOCKDELIVEREDTO();
                        btn_Submit.Enabled = false;
                        btn_ExistVariety.Enabled = false;
                        FillFinYr();
                        FillSeassion();
                        CREATENEWDATATABLE();
                    }
                }
            }
        }
        else
        {
            divview.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Online seed transactions for Rabi 2020-21 has been closed.'); window.location='" + Request.ApplicationPath + "/Masters/Home.aspx';", true);
        }
    }
    private void AddOAICDists()
    {
        DataTable dtOAIC = (DataTable)Session["dtOAIC"];
        dtOAIC.Rows.Add("02", "BALASORE");
        dtOAIC.Rows.Add("05", "BOLANGIR");
        dtOAIC.Rows.Add("29", "SONEPUR");
        dtOAIC.Rows.Add("09", "DHENKANAL");
        dtOAIC.Rows.Add("15", "KALAHANDI");
        dtOAIC.Rows.Add("17", "KEONJHAR");
        dtOAIC.Rows.Add("21", "MAYURBHANJ");
        dtOAIC.Rows.Add("06", "BOUDH");
        dtOAIC.Rows.Add("26", "PURI");
        dtOAIC.Rows.Add("28", "SAMBALPUR");
        dtOAIC.Rows.Add("03", "BARGARH");
    }
    private void FillSeassion()
    {
        try
        {
            ddlSeassion.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.STATUS = "T";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSeassion(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSeassion.DataSource = ds;
                    ddlSeassion.DataTextField = "SEASSION_NAME";
                    ddlSeassion.DataValueField = "SHORT_NAME";
                    ddlSeassion.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
        //ddlSeassion.Items.Insert(0, "--Select--");
    }
    private void FillFinYr()
    {
        try
        {
            ddlFinYr.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "T";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFinYr.DataSource = ds;
                    ddlFinYr.DataTextField = "FIN_YR";
                    ddlFinYr.DataValueField = "FIN_YR";
                    ddlFinYr.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
        //ddlFinYr.Items.Insert(0, "--Select--");
    }
    private void FillSupplyType()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSupplyType(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rbtn_stocksupplied.DataSource = ds;
                    rbtn_stocksupplied.DataTextField = "SUPPLY_NAME";
                    rbtn_stocksupplied.DataValueField = "SUPPLY_ID";
                    rbtn_stocksupplied.DataBind();
                }
                else
                {
                    rbtn_stocksupplied.DataSource = null;
                    rbtn_stocksupplied.DataBind();
                }
            }
            else
            {
                rbtn_stocksupplied.DataSource = null;
                rbtn_stocksupplied.DataBind();
            }

            if (Session["UserType"].ToString() == "OAIC")
            {
                rbtn_stocksupplied.SelectedValue = "1";
            }
            else if (Session["UserType"].ToString() == "OSSC")
            {
                rbtn_stocksupplied.SelectedValue = "6";
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void FillPACSReabate()
    {
        rbtnlPacsRebate.Items.Clear();
        DataTable dtPacsRebate = new DataTable();
        dtPacsRebate.Columns.AddRange(new DataColumn[2]
                {
                    new DataColumn("SlNo", typeof(string)),
                    new DataColumn("Narration", typeof(string))
                });
        dtPacsRebate.Rows.Add("1", "Own Arrangement");
        dtPacsRebate.Rows.Add("2", "OSSC Arrangement");

        if (dtPacsRebate != null)
        {
            if (dtPacsRebate.Rows.Count > 0)
            {
                rbtnlPacsRebate.DataSource = dtPacsRebate;
                rbtnlPacsRebate.DataTextField = "Narration";
                rbtnlPacsRebate.DataValueField = "SlNo";
                rbtnlPacsRebate.DataBind();

                if (rbtn_stocksupplied.SelectedValue == "9")
                {
                    rbtnlPacsRebate.SelectedValue = "1";
                }
            }
        }
    }
    private void CREATENEWDATATABLE()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[15]
                {
                    new DataColumn("GODOWN_ID", typeof(string)),
                    new DataColumn("GODOWN_NAME", typeof(string)),
                    new DataColumn("RECEIVE_UNITCD", typeof(string)),
                    new DataColumn("MOU_REFNO", typeof(string)),
                    new DataColumn("CATEGORY_ID", typeof(string)),
                    new DataColumn("CATEGORY_NAME", typeof(string)),
                    new DataColumn("CROP_ID", typeof(string)),
                    new DataColumn("CROP_NAME", typeof(string)),
                    new DataColumn("CROP_CLASS", typeof(string)),
                    new DataColumn("VARIETY_ID", typeof(string)),
                    new DataColumn("VARIETY_NAME", typeof(string)),
                    new DataColumn("LOT_NO",typeof(string)),
                    new DataColumn("BAG_SIZE",typeof(string)),
                    new DataColumn("NO_OF_BAGS",typeof(string)),
                    new DataColumn("QUANTITY",typeof(string))
                });
        Session.Add("dt", dt);
    }
    private void CREATENEWOAICTABLE()
    {
        DataTable dtOAIC = new DataTable();
        dtOAIC.Columns.AddRange(new DataColumn[2]
                {
                    new DataColumn("DIST_CODE", typeof(string)),
                    new DataColumn("DIST_NAME", typeof(string))
                });
        Session.Add("dtOAIC", dtOAIC);
    }
    public void FILL_GODOWN()
    {
        try
        {
            ddl_godown.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = Session["DistCode"].ToString();
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillGoDownByDistCodeUserType(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_godown.DataSource = ds;
                    ddl_godown.DataTextField = "GODOWN_NAME";
                    ddl_godown.DataValueField = "GODOWN_ID";
                    ddl_godown.DataBind();
                    ddl_godown.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddl_godown.DataSource = null;
                    ddl_godown.DataBind();
                }
            }
            else
            {
                ddl_godown.DataSource = null;
                ddl_godown.DataBind();
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void rbtn_stocksupplied_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDist.Visible = false;
            tr_Scheme.Visible = false;
            txtDemonName.Visible = false;
            if (rbtn_stocksupplied.SelectedValue == "1" || rbtn_stocksupplied.SelectedValue == "6" || rbtn_stocksupplied.SelectedValue == "9")//Dealer/Pacs
            {
                tr_CreditBillNo.Visible = false;
                tr_Date.Visible = false;
                tr_Select.Visible = false;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = false;
                tr_StockDeliveredTo.Visible = true;
                tr_DDNumber.Visible = true;
                //tr_CashMemoNo.Visible = true;
                tr_Amount.Visible = true;
            }
            else if (rbtn_stocksupplied.SelectedValue == "2" || rbtn_stocksupplied.SelectedValue == "7")//Departmental
            {
                rbtn_depttype.SelectedValue = "01";
                tr_CreditBillNo.Visible = true;
                tr_Date.Visible = true;
                tr_Select.Visible = true;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = true;
                tr_StockDeliveredTo.Visible = false;
                tr_DDNumber.Visible = false;
                tr_Amount.Visible = false;

                ddl_SellTo.Visible = true;
                txt_saleto.Visible = false;
            }
            else if (rbtn_stocksupplied.SelectedValue == "3" || rbtn_stocksupplied.SelectedValue == "8")//Godown
            {
                tr_CreditBillNo.Visible = false;
                tr_Date.Visible = false;
                tr_Select.Visible = false;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = false;
                tr_StockDeliveredTo.Visible = true;
                tr_DDNumber.Visible = false;
                //tr_CashMemoNo.Visible = true;
                tr_Amount.Visible = false;
                ddlDist.Visible = true;
            }
            else if (rbtn_stocksupplied.SelectedValue == "10")//OAIC
            {
                tr_CreditBillNo.Visible = false;
                tr_Date.Visible = false;
                tr_Select.Visible = false;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = false;
                tr_StockDeliveredTo.Visible = false;
                tr_DDNumber.Visible = true;
                //tr_CashMemoNo.Visible = true;
                tr_Amount.Visible = true;
            }
            else if (rbtn_stocksupplied.SelectedValue == "12")//OSSC-FARMER
            {
                tr_CreditBillNo.Visible = false;
                tr_Date.Visible = false;
                tr_Select.Visible = false;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = true;
                ddl_SellTo.Visible = false;
                txt_saleto.Visible = true;
                tr_StockDeliveredTo.Visible = false;
                tr_DDNumber.Visible = false;
                tr_Amount.Visible = true;
            }
            else if (rbtn_stocksupplied.SelectedValue == "13")//DEMONSTRATION
            {
                tr_CreditBillNo.Visible = false;
                tr_Date.Visible = false;
                tr_Select.Visible = false;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = false;
                ddl_SellTo.Visible = false;
                txt_saleto.Visible = true;
                tr_StockDeliveredTo.Visible = true;
                tr_DDNumber.Visible = false;
                tr_Amount.Visible = false;
                tr_Scheme.Visible = true;
                txtDemonName.Visible = true;
            }
            txt_saledt.Text = "";
            txt_ddnum.Text = "";
            txt_CashMemoNo.Text = "";
            txt_amount.Text = "";
            FILLSTOCKDELIVEREDTO();
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void FILLSTOCKDELIVEREDTO()
    {
        try
        {
            rbtnlPacsRebate.DataSource = null;
            rbtnlPacsRebate.DataBind();
            rbtnlPacsRebate.Visible = false;

            DataTable dtDealer = new DataTable();
            dtDealer.Columns.AddRange(new DataColumn[2]
                {
                    new DataColumn("LIC_NO", typeof(string)),
                    new DataColumn("DEALER", typeof(string))
                });

            ddl_StockDeliveredTo.Items.Clear();
            if (rbtn_stocksupplied.SelectedValue == "1" || rbtn_stocksupplied.SelectedValue == "6")
            {
                DataTable dt = new DataTable();

                objUserBEL = new BLL_DropDown();
                objUserBEL.DistCode = Session["DistCode"].ToString();
                objUserBEL.UserType = Session["UserType"].ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();

                if (rbtn_stocksupplied.SelectedValue == "1")
                {
                    bool ISOAIC = false;
                    DataTable dtOAIC = (DataTable)Session["dtOAIC"];
                    if (dtOAIC != null)
                    {
                        if (dtOAIC.Rows.Count > 0)
                        {
                            int COUNT = dtOAIC.Rows.Count;
                            for (int r = 0; r < COUNT; ++r)
                            {
                                DataRow row = dtOAIC.Rows[r];
                                if (row["DIST_CODE"].ToString() == Session["distcode"].ToString())
                                {
                                    ISOAIC = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (ISOAIC)
                    {
                        objUserBEL = new BLL_DropDown();
                        objUserBEL.DistCode = Session["DistCode"].ToString();
                        objUserBEL.UserType = Session["UserType"].ToString();
                        objUserDLL = new DLL_DropDown();
                        ds = new DataSet();
                        ds = objUserDLL.GetDealerLicenceByDistCodeUserTypePacs(objUserBEL);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dt = ds.Tables[0];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string Dealer = dr["Dealer"].ToString();
                                    string LIC_NO = dr["LIC_NO"].ToString();
                                    dtDealer.Rows.Add(LIC_NO, Dealer);
                                }
                            }
                        }
                    }
                }
                else
                {
                    objUserBEL = new BLL_DropDown();
                    objUserBEL.DistCode = Session["DistCode"].ToString();
                    objUserBEL.UserType = Session["UserType"].ToString();
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.GetDealerLicenceByDistCodeUserType(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            dt = ds.Tables[0];
                            foreach (DataRow dr in dt.Rows)
                            {
                                string Dealer = dr["Dealer"].ToString();
                                string LIC_NO = dr["LIC_NO"].ToString();
                                dtDealer.Rows.Add(LIC_NO, Dealer);
                            }
                        }
                    }
                }

                if (dtDealer != null)
                {
                    if (dtDealer.Rows.Count > 0)
                    {
                        ddl_StockDeliveredTo.DataSource = dtDealer;
                        ddl_StockDeliveredTo.DataTextField = "Dealer";
                        ddl_StockDeliveredTo.DataValueField = "LIC_NO";
                        ddl_StockDeliveredTo.DataBind();
                        ddl_StockDeliveredTo.Items.Insert(0, "--Select--");
                    }
                }
            }
            else if (rbtn_stocksupplied.SelectedValue == "2" || rbtn_stocksupplied.SelectedValue == "7")
            {
                ddl_SellTo.Items.Clear();

                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillDistricts();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_SellTo.DataSource = ds;
                        ddl_SellTo.DataTextField = "Dist_Name";
                        ddl_SellTo.DataValueField = "Dist_Code";
                        ddl_SellTo.DataBind();
                        ddl_SellTo.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddl_SellTo.DataSource = null;
                        ddl_SellTo.DataBind();
                    }
                }
                else
                {
                    ddl_SellTo.DataSource = null;
                    ddl_SellTo.DataBind();
                }
            }
            else if (rbtn_stocksupplied.SelectedValue == "3" || rbtn_stocksupplied.SelectedValue == "8")
            {
                ddl_StockDeliveredTo.DataSource = null;
                ddl_StockDeliveredTo.DataBind();
                ddl_StockDeliveredTo.Items.Clear();
                ddl_StockDeliveredTo.Items.Insert(0, "--Select--");

                FillDistricts();
            }
            else if (rbtn_stocksupplied.SelectedValue == "9")
            {

                objUserBEL = new BLL_DropDown();
                objUserBEL.DistCode = Session["DistCode"].ToString();
                objUserBEL.UserType = Session["UserType"].ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.GetDealerLicenceByDistCodeUserTypePacs(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            string Dealer = dr["Dealer"].ToString();
                            string LIC_NO = dr["LIC_NO"].ToString();
                            dtDealer.Rows.Add(LIC_NO, Dealer);
                        }
                    }
                }

                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = Session["UserType"].ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.GetDealerLicenceByUserTypePacs(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            string Dealer = dr["Dealer"].ToString();
                            string LIC_NO = dr["LIC_NO"].ToString();
                            dtDealer.Rows.Add(LIC_NO, Dealer);
                        }
                    }
                }

                if (dtDealer != null)
                {
                    if (dtDealer.Rows.Count > 0)
                    {
                        ddl_StockDeliveredTo.DataSource = dtDealer;
                        ddl_StockDeliveredTo.DataTextField = "Dealer";
                        ddl_StockDeliveredTo.DataValueField = "LIC_NO";
                        ddl_StockDeliveredTo.DataBind();
                        ddl_StockDeliveredTo.Items.Insert(0, "--Select--");
                    }
                }
                rbtnlPacsRebate.Visible = true;
                FillPACSReabate();
            }
            else if (rbtn_stocksupplied.SelectedValue == "13")
            {
                ddl_StockDeliveredTo.DataSource = null;
                ddl_StockDeliveredTo.DataBind();
                ddl_StockDeliveredTo.Items.Clear();

                FillDemonNames();
                FillDemonSchemes();
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void FillDemonNames()
    {
        txtDemonName.Visible = true;
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.GetDemonNames();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_StockDeliveredTo.DataSource = ds;
                ddl_StockDeliveredTo.DataTextField = "DEMON_NAME";
                ddl_StockDeliveredTo.DataValueField = "DEMON_ID";
                ddl_StockDeliveredTo.DataBind();
                ddl_StockDeliveredTo.Items.Insert(0, "--Select--");
            }
        }
    }
    private void FillDemonSchemes()
    {
        ddl_Scheme.DataSource = null;
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.GetDemonSchemes();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_Scheme.DataSource = ds;
                ddl_Scheme.DataTextField = "SCHEME_NAME";
                ddl_Scheme.DataValueField = "SCHEME_ID";
                ddl_Scheme.DataBind();
                ddl_Scheme.Items.Insert(0, "--Select--");
            }
        }
    }
    protected void FILL_CROPCATAGORY()
    {
        try
        {
            ddl_cropCatagory.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.Godown_ID = ddl_godown.SelectedValue;
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserBEL.SESSION = ddlSeassion.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropCategorySale(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_cropCatagory.DataSource = ds;
                    ddl_cropCatagory.DataTextField = "Category_Name";
                    ddl_cropCatagory.DataValueField = "Category_Code";
                    ddl_cropCatagory.DataBind();
                    ddl_cropCatagory.Items.Insert(0, "--Select--");
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void FILLCROPNAME()
    {
        try
        {
            ddl_crop.Items.Clear();
            ddl_crop.DataSource = null;
            ddl_crop.DataBind();
            objUserBEL = new BLL_DropDown();
            objUserBEL.Godown_ID = ddl_godown.SelectedValue;
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserBEL.CROPCATG_ID = ddl_cropCatagory.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SESSION = ddlSeassion.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropByCategoryIdSale(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_crop.DataSource = ds;
                    ddl_crop.DataTextField = "Crop_Name";
                    ddl_crop.DataValueField = "Crop_Code";
                    ddl_crop.DataBind();
                    ddl_crop.Items.Insert(0, "--Select--");
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void FILLCROPVARIETY()
    {
        try
        {
            ddl_cropvariety.Items.Clear();
            ddl_cropvariety.DataSource = null;
            ddl_cropvariety.DataBind();
            objUserBEL = new BLL_DropDown();
            objUserBEL.Godown_ID = ddl_godown.SelectedValue;
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserBEL.CROPCATG_ID = ddl_cropCatagory.SelectedValue;
            objUserBEL.CROP_ID = ddl_crop.SelectedValue;
            objUserBEL.SESSION = ddlSeassion.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropVarietyByCropIdSale(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_cropvariety.DataSource = ds;
                    ddl_cropvariety.DataTextField = "Variety_Name";
                    ddl_cropvariety.DataValueField = "Variety_Code";
                    ddl_cropvariety.DataBind();
                    ddl_cropvariety.Items.Insert(0, "--Select--");
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void rbtn_depttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtn_depttype.SelectedValue == "01")
            {
                ddl_SellTo.Visible = true;
                txt_saleto.Visible = false;
            }
            else if (rbtn_depttype.SelectedValue == "02")
            {
                ddl_SellTo.Visible = false;
                txt_saleto.Visible = true;
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FILL_CROPCATAGORY();
            FILLCROPNAME();
            FILLCROPVARIETY();
            grd_lotdtls.DataSource = null;
            grd_lotdtls.DataBind();
            tblDelivery.Visible = true;
            pnlContents.Visible = true;
            DataTable dt = (DataTable)Session["dt"];

        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void txt_saledt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DateTime.Now < Convert.ToDateTime(txt_saledt.Text))
            {
                string pop = "alert('Delivery date Cannot be greater than todays date !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                txt_saledt.Text = "";
            }
            else
            {
                if (Convert.ToDateTime(txt_saledt.Text) >= Convert.ToDateTime("01/11/2015"))
                {
                    if (RegDate.IsMatch(txt_saledt.Text))
                    {
                    }
                    else
                    {
                        string popp = "alert('Please Enter a valid Date!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                        return;
                    }
                }
                else
                {
                    string pop = "alert('Delivery date Cannot be less than opening date !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                    txt_saledt.Text = "";
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddl_cropCatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FILLCROPNAME();
            FILLCROPVARIETY();
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddl_crop_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FILLCROPVARIETY();
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddl_cropvariety_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FILLGRID();
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void FILLGRID()
    {
        try
        {
            grd_lotdtls.DataSource = null;
            grd_lotdtls.DataBind();

            objUserBEL = new BLL_DropDown();
            objUserBEL.Godown_ID = ddl_godown.SelectedValue;
            objUserBEL.CROPCATG_ID = ddl_cropCatagory.SelectedValue;
            objUserBEL.CROP_ID = ddl_crop.SelectedValue;
            objUserBEL.Crop_Verid = ddl_cropvariety.SelectedValue;
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();

            DataTable dt = new DataTable();
            dt.Clear();
            dt = objUserDLL.FillGrid1(objUserBEL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    grd_lotdtls.Visible = true;
                    btn_Submit.Enabled = true;

                    grd_lotdtls.DataSource = dt;
                    grd_lotdtls.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void btn_ExistVariety_Click(object sender, EventArgs e)
    {
        try
        {
            FILL_GODOWN();
            FILLSTOCKDELIVEREDTO();
            FILL_CROPCATAGORY();
            FILLCROPNAME();
            FILLCROPVARIETY();
            FILLGRID();
            pnlMainSection.Enabled = true;
            pnlDelivery.Enabled = false;
            pnlDelivery.Visible = false;

            btn_Submit.Enabled = false;
            btn_ExistVariety.Enabled = false;
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGoDown();
    }
    private void FillGoDown()
    {
        try
        {
            ddl_StockDeliveredTo.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.CURGODOWNID = ddl_godown.SelectedValue;
            objUserBEL.DistCode = ddlDist.SelectedValue;
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.GetGoDownByUserTypeDistCode(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_StockDeliveredTo.DataSource = ds;
                    ddl_StockDeliveredTo.DataTextField = "GODOWN_NAME";
                    ddl_StockDeliveredTo.DataValueField = "GODOWN_ID";
                    ddl_StockDeliveredTo.DataBind();
                    ddl_StockDeliveredTo.Items.Insert(0, "--Select--");

                    ddl_StockDeliveredTo.Focus();

                    tblDelivery.Visible = false;
                    pnlContents.Visible = false;
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void FillDistricts()
    {
        try
        {
            ddlDist.Items.Clear();
            ddlDist.DataSource = null;
            ddlDist.DataBind();

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillDistricts();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDist.DataSource = ds;
                    ddlDist.DataTextField = "DIST_NAME";
                    ddlDist.DataValueField = "DIST_CODE";
                    ddlDist.DataBind();
                    ddlDist.Items.Insert(0, "--Select--");
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddl_StockDeliveredTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txt_ddnum.Text = "";
            txt_CashMemoNo.Text = "";
            txt_amount.Text = "";

            if (rbtn_stocksupplied.SelectedValue == "1" || rbtn_stocksupplied.SelectedValue == "6" || rbtn_stocksupplied.SelectedValue == "9")
            {
                if (ddl_StockDeliveredTo.SelectedIndex == 0)
                {
                    tblDelivery.Visible = false;
                    pnlContents.Visible = false;
                }
                else
                {
                    tblDelivery.Visible = true;
                    pnlContents.Visible = true;
                }
            }
            else if (rbtn_stocksupplied.SelectedValue == "3" || rbtn_stocksupplied.SelectedValue == "8")
            {
                ddl_StockDeliveredTo.DataSource = null;
                ddl_StockDeliveredTo.DataBind();
                tblDelivery.Visible = true;
                pnlContents.Visible = true;
            }
            else if (rbtn_stocksupplied.SelectedValue == "13")
            {
                txtDemonName.Text = "";
                txtDemonName.ReadOnly = false;
                if (ddl_StockDeliveredTo.SelectedValue == "1" || ddl_StockDeliveredTo.SelectedValue == "2")
                {
                    txtDemonName.ReadOnly = true;
                    txtDemonName.Text = this.Session["DistName"].ToString();
                }

                txtDemonName.Visible = true;
                tblDelivery.Visible = true;
                pnlContents.Visible = true;
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void rbtn_class_SelectedIndexChanged(object sender, EventArgs e)
    {
        FILLGRID();
    }
    protected void grd_lotdtls_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //if (Session["HiddenTextS"] != null)
        //{
        //    try
        //    {
        //        if (Session["HiddenTextS"].ToString() == lblHiddenText.Text.ToString())
        //        {
        //            Label lbl_lotno = (Label)grd_lotdtls.Rows[e.NewEditIndex].FindControl("lbl_lotno");
        //            Label lbl_bagno = (Label)grd_lotdtls.Rows[e.NewEditIndex].FindControl("lbl_bagno");
        //            ViewState["bag_no"] = lbl_bagno.Text;
        //            Label lbl_bagsize = (Label)grd_lotdtls.Rows[e.NewEditIndex].FindControl("lbl_bagsize");
        //            Label lbl_quantity = (Label)grd_lotdtls.Rows[e.NewEditIndex].FindControl("lbl_quantity");
        //            ViewState["quantity"] = lbl_quantity.Text;
        //            TextBox txt_bagsold = (TextBox)grd_lotdtls.Rows[e.NewEditIndex].FindControl("txt_bagsold");
        //            TextBox txt_quantitysold = (TextBox)grd_lotdtls.Rows[e.NewEditIndex].FindControl("txt_quantitysold");
        //            Button btn_add = (Button)grd_lotdtls.Rows[e.NewEditIndex].FindControl("btn_add");
        //            RadioButton rbtn_lot = (RadioButton)grd_lotdtls.Rows[e.NewEditIndex].FindControl("rbtn_lot");
        //            Label lbl_sourcetype = (Label)grd_lotdtls.Rows[e.NewEditIndex].FindControl("lbl_source");
        //            Label lbl_sourceid = (Label)grd_lotdtls.Rows[e.NewEditIndex].FindControl("lbl_sourceid");
        //            //Label lbl_class = (Label)grd_lotdtls.Rows[e.NewEditIndex].FindControl("lbl_class");
        //            if (ddl_cropCatagory.SelectedIndex == 0)
        //            {
        //                string pop = "alert('Please select the Crop Catagory!!!')";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        //                ScriptManager.GetCurrent(Page).SetFocus(ddl_cropCatagory);
        //                return;
        //            }
        //            else if (ddl_crop.SelectedIndex == 0)
        //            {
        //                string pop = "alert('Please select the Crop Name!!!')";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        //                ScriptManager.GetCurrent(Page).SetFocus(ddl_crop);
        //                return;
        //            }
        //            else if (ddl_cropvariety.SelectedIndex == 0)
        //            {
        //                string pop = "alert('Please select the Crop Variety!!!')";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        //                ScriptManager.GetCurrent(Page).SetFocus(ddl_cropvariety);
        //                return;
        //            }
        //            else if (txt_bagsold.Text == "")
        //            {
        //                string pop = "alert('Please enter the no of bags sold!!!')";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        //                ScriptManager.GetCurrent(Page).SetFocus(txt_bagsold);
        //                return;
        //            }
        //            else
        //            {
        //                Regex reg = new Regex("^[0-9]*$");
        //                Regex regdec = new Regex(@"^-?\d*\.{0,1}\d+$");

        //                if (reg.IsMatch(txt_bagsold.Text) == false)
        //                {
        //                    string pop1 = "alert('OOOOPS!! Something Went Wrong');";
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
        //                    txt_bagsold.Text = "";
        //                    txt_quantitysold.Text = "";
        //                    return;
        //                }
        //                else if (regdec.IsMatch(txt_quantitysold.Text) == false)
        //                {
        //                    string pop1 = "alert('OOOOPS!! Something Went Wrong');";
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
        //                    txt_bagsold.Text = "";
        //                    txt_quantitysold.Text = "";
        //                    return;
        //                }
        //                else
        //                {
        //                    //return;
        //                }

        //                string rtval = "F";
        //                if (btn_add.Text == "Add")
        //                {
        //                }
        //                if (btn_add.Text == "Modify")
        //                {
        //                }
        //            }
        //        }
        //    }
        //    catch (ApplicationException applicationException1)
        //    {
        //        ApplicationException applicationException = applicationException1;
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
        //        ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        //    }
        //    catch (Exception exception1)
        //    {
        //        Exception exception = exception1;
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        //    }
        //}
    }
    protected void rbtn_lot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow i in grd_lotdtls.Rows)
            {
                RadioButton chk = (RadioButton)i.FindControl("rbtn_lot");
                TextBox txt_bagsold = (TextBox)i.FindControl("txt_bagsold");
                TextBox txt_quantitysold = (TextBox)i.FindControl("txt_quantitysold");
                Button btn_add = (Button)i.FindControl("btn_add");

                if (chk.Checked == true)
                {
                    txt_bagsold.Enabled = true;
                    btn_add.Enabled = true;

                    txt_bagsold.BackColor = System.Drawing.Color.LemonChiffon;
                    txt_quantitysold.BackColor = System.Drawing.Color.LemonChiffon;
                    btn_add.BackColor = System.Drawing.Color.SeaGreen;
                    txt_bagsold.Focus();
                }
                else
                {
                    txt_bagsold.Enabled = false;
                    btn_add.Enabled = false;

                    txt_bagsold.BackColor = System.Drawing.Color.White;
                    txt_quantitysold.BackColor = System.Drawing.Color.White;
                    btn_add.BackColor = System.Drawing.Color.Gray;
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddl_SellTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_SellTo.SelectedIndex == 0)
        {
            tblDelivery.Visible = false;
            pnlContents.Visible = false;
        }
        else
        {
            tblDelivery.Visible = true;
            pnlContents.Visible = true;
        }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.hfSession.Value != this.Session["AuthTokenPage"].ToString())
            {
                ExceptionHandler.WriteException("Session Value in Cookie And Hidden Field Does not Match");
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Error Executing the Request. Please Contact Site Administrator for Details!');", true);
                return;
            }
            else
            {
                Button btn = (Button)sender;
                RadioButton rbtn_lot = (RadioButton)btn.Parent.FindControl("rbtn_lot");
                Label lbl_sourceid = (Label)btn.Parent.FindControl("lbl_sourceid");
                Label lblRefNo = (Label)btn.Parent.FindControl("lblRefNo");
                Label lbl_source = (Label)btn.Parent.FindControl("lbl_source");
                Label lbl_lotno = (Label)btn.Parent.FindControl("lbl_lotno");
                Label lbl_bagsize = (Label)btn.Parent.FindControl("lbl_bagsize");
                Label lbl_bagno = (Label)btn.Parent.FindControl("lbl_bagno");
                Label lbl_quantity = (Label)btn.Parent.FindControl("lbl_quantity");
                TextBox txt_bagsold = (TextBox)btn.Parent.FindControl("txt_bagsold");
                TextBox txt_quantitysold = (TextBox)btn.Parent.FindControl("txt_quantitysold");
                Button btn_add = (Button)btn.Parent.FindControl("btn_add");

                if (ddl_cropCatagory.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Crop Catagory!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_cropCatagory);
                    return;
                }
                else if (ddl_crop.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Crop Name!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_crop);
                    return;
                }
                else if (txt_bagsold.Text == "")
                {
                    string pop = "alert('Please enter no of bags receive!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_bagsold);
                    return;
                }
                else
                {
                    decimal total = Convert.ToInt32(lbl_bagsize.Text) * Convert.ToInt32(txt_bagsold.Text);
                    decimal Quantity = Convert.ToDecimal(total) / (100);
                    txt_quantitysold.Text = Quantity.ToString();
                    Session["value"] = txt_quantitysold.Text;

                    string UserType = this.Session["UserType"].ToString();

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.LOT_NO = lbl_lotno.Text.Trim();
                    objUserBEL.Crop_Verid = ddl_cropvariety.SelectedValue;
                    objUserBEL.Godown_ID = ddl_godown.SelectedValue;
                    objUserDLL = new DLL_DropDown();

                    double RECV_NO_OF_BAGS = objUserDLL.GetBagsFromStockByLotNoVerId(objUserBEL);
                    if (RECV_NO_OF_BAGS > 0)
                    {
                        int BAGS_AVAILABLE = Convert.ToInt32(RECV_NO_OF_BAGS);
                        int BAGS_SOLD = Convert.ToInt32(txt_bagsold.Text);

                        if (BAGS_AVAILABLE < BAGS_SOLD)
                        {
                            string popp = "alert('You can not Sale more than the Available Stock!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_bagsold.Text = "";
                            txt_quantitysold.Text = "";
                            return;
                        }
                    }
                    DataTable dt = (DataTable)Session["dt"];

                    if (btn_add.Text == "Add")
                    {
                        objUserBEL = new BLL_DropDown();
                        objUserBEL.LOT_NO = lbl_lotno.Text.Trim();
                        objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
                        objUserBEL.SEASON = ddlSeassion.SelectedValue;
                        objUserDLL = new DLL_DropDown();
                        string Class = objUserDLL.GetClass(objUserBEL);
                        if (lbl_sourceid.Text == "0123")
                        {
                            dt.Rows.Add(ddl_godown.SelectedValue, ddl_godown.SelectedItem.Text, lbl_sourceid.Text, lblRefNo.Text, ddl_cropCatagory.SelectedValue, ddl_cropCatagory.SelectedItem.Text, ddl_crop.SelectedValue, ddl_crop.SelectedItem.Text, Class, ddl_cropvariety.SelectedValue, ddl_cropvariety.SelectedItem.Text, lbl_lotno.Text, lbl_bagsize.Text, txt_bagsold.Text, txt_quantitysold.Text);
                        }
                        else
                        {
                            dt.Rows.Add(ddl_godown.SelectedValue, ddl_godown.SelectedItem.Text, lbl_sourceid.Text, "", ddl_cropCatagory.SelectedValue, ddl_cropCatagory.SelectedItem.Text, ddl_crop.SelectedValue, ddl_crop.SelectedItem.Text, Class, ddl_cropvariety.SelectedValue, ddl_cropvariety.SelectedItem.Text, lbl_lotno.Text, lbl_bagsize.Text, txt_bagsold.Text, txt_quantitysold.Text);
                        }

                        btn_add.Text = "Update";
                        string pop = "alert('Data Saved Sucessfully!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            int COUNT = dt.Rows.Count;
                            for (int r = 0; r < COUNT; ++r)
                            {
                                DataRow row = dt.Rows[r];
                                if (row["LOT_NO"].ToString() == lbl_lotno.Text && row["VARIETY_ID"].ToString() == ddl_cropvariety.SelectedValue && row["GODOWN_ID"].ToString() == ddl_godown.SelectedValue)// && row["Receive_Unitcd"].ToString() == lbl_SorceID.Text && row["LOT_NO"].ToString() == lbl_LotNo.Text && row["BAG_SIZE_KG"].ToString() == lbl_BagSize.Text && row["PRICE_QTL"].ToString() == lbl_PricePerQtl.Text)
                                {
                                    row["NO_OF_BAGS"] = txt_bagsold.Text;
                                    row["QUANTITY"] = txt_quantitysold.Text;
                                }
                            }
                        }
                        string pop = "alert('Data Updated Sucessfully!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        btn_Submit.Enabled = true;
                        btn_ExistVariety.Enabled = true;

                    }
                    else
                    {
                        btn_Submit.Enabled = false;
                        btn_ExistVariety.Enabled = false;
                    }

                    txt_bagsold.Enabled = false;
                    btn_add.Enabled = false;
                    btn_add.BackColor = System.Drawing.Color.Gray;
                    txt_bagsold.BackColor = System.Drawing.Color.White;
                    txt_quantitysold.BackColor = System.Drawing.Color.White;
                    rbtn_lot.Checked = false;
                    Filllottrans();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    private void Filllottrans()
    {
        DataTable dt = (DataTable)Session["dt"];
        grd_lottrans.DataSource = dt;
        grd_lottrans.DataBind();
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Label lbl_Cat = (Label)btn.Parent.FindControl("lbl_Cat");
        Label lbl_Crop = (Label)btn.Parent.FindControl("lbl_Crop");
        Label lbl_Variety = (Label)btn.Parent.FindControl("lbl_Variety");
        Label lbl_Class = (Label)btn.Parent.FindControl("lbl_Class");
        Label lbl_Source = (Label)btn.Parent.FindControl("lbl_Source");
        Label lbl_BagSize = (Label)btn.Parent.FindControl("lbl_BagSize");
        Label lbl_BagReceive = (Label)btn.Parent.FindControl("lbl_BagReceive");
        Label lbl_LotNo = (Label)btn.Parent.FindControl("lbl_LotNo");

        //string[] param = { "@RANDOM_NO", "@CROPCATG_ID", "@CROP_ID", "@CROP_VERID", "@CROP_CLASS", "@RECEIVE_UNITCD", "@BAG_SIZE_KG" };
        //object[] value = { hfRandom.Value, lbl_Cat.Text, lbl_Crop.Text, lbl_Variety.Text, lbl_Class.Text, lbl_Source.Text, lbl_BagSize.Text };
        //dbsApp.param_passing_updstock("DELETE FROM TMP_FARMERSALE WHERE RANDOM_NO = @RANDOM_NO AND CROPCATG_ID = @CROPCATG_ID AND CROP_ID = @CROP_ID AND CROP_VERID = @CROP_VERID AND CROP_CLASS = @CROP_CLASS AND Receive_Unitcd = @Receive_Unitcd AND BAG_SIZE_KG = @BAG_SIZE_KG ", param, value);        
        DataTable dt = (DataTable)Session["dt"];
        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            DataRow dr = dt.Rows[i];
            if (dr["LOT_NO"].ToString() == lbl_LotNo.Text)
            {
                dr.Delete();
                Session.Add("dt", dt);
                break;
            }
        }
        Filllottrans();
        //FillGrid2();

        string pop = "alert('Item Deleted Sucessfully!!!')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
    }
    double TotQty = 0;
    double TotAmt = 0;
    protected void grd_lottrans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    TotQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "QUANTITY"));
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    Label lblTotQty = (Label)e.Row.FindControl("lblTotQty");
        //    lblTotQty.Text = TotQty.ToString();
        //}

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    TotAmt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AMOUNT"));
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    Label lblTotAmt = (Label)e.Row.FindControl("lblTotAmt");
        //    lblTotAmt.Text = TotAmt.ToString();
        //}
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            td_LicenceNo.Visible = false;

            //lbl_stockedat.Text = HttpUtility.HtmlEncode(ddl_godown.SelectedItem.Text);
            lbl_recvdt.Text = HttpUtility.HtmlEncode(txt_saledt.Text);

            if (Session["UserType"].ToString() == "OAIC")
            {
                lblCompanyName.Text = "ODISHA AGRO INDUSTRIES CORPORATION LTD";
                Image1.ImageUrl = "~/images/LogoofOAIC.png";

            }
            else
            {
                lblCompanyName.Text = "ODISHA STATE SEEDS CORPORATION LTD";
                Image1.ImageUrl = "~/images/OSSCLogo.png";

            }
            string SUPPLY_TYPE = rbtn_stocksupplied.SelectedValue;
            string DEPT_TYPE = rbtn_depttype.SelectedValue;
            string SaleTo = ddl_StockDeliveredTo.SelectedValue;
            if (rbtn_stocksupplied.SelectedValue == "2")
            {
                if (rbtn_depttype.SelectedValue == "01")
                {
                    SaleTo = ddl_SellTo.SelectedValue;
                }
                else if (rbtn_depttype.SelectedValue == "02")
                {
                    SaleTo = txt_saleto.Text;
                }
            }

            if (SUPPLY_TYPE == "1" || SUPPLY_TYPE == "6" || SUPPLY_TYPE == "9")
            {
                string[] param1 = { "@LIC_NO" };
                object[] val1 = { SaleTo };
                td_LicenceNo.Visible = true;

                string Length = SaleTo.Length.ToString();
                objUserBEL.LIC_NO = SaleTo;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();

                if (Length == "19")
                {
                    ds = objUserDLL.GetDistFirmNameLicenceNo(objUserBEL);
                }
                else
                {
                    ds = objUserDLL.GetStateFirmNameLicenceNo(objUserBEL);
                }

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbl_saletonm.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
                        lblLicenceNo.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["LIC_NO"].ToString());
                    }
                }
            }
            else if (SUPPLY_TYPE == "2" || SUPPLY_TYPE == "7")
            {
                if (DEPT_TYPE == "01")
                {
                    lbl_saletonm.Text = "DDA, " + HttpUtility.HtmlEncode(ddl_SellTo.SelectedItem.Text);
                }
                else
                {
                    lbl_saletonm.Text = HttpUtility.HtmlEncode(txt_saleto.Text);
                }
            }
            else if (SUPPLY_TYPE == "3" || SUPPLY_TYPE == "8")
            {
                lbl_saletonm.Text = HttpUtility.HtmlEncode(ddl_StockDeliveredTo.SelectedItem.Text);
            }
            else if (SUPPLY_TYPE == "12")
            {
                lbl_saletonm.Text = HttpUtility.HtmlEncode(txt_saleto.Text);
            }
            else if (SUPPLY_TYPE == "13")
            {
                lbl_saletonm.Text = ddl_StockDeliveredTo.SelectedItem.Text + "(" + HttpUtility.HtmlEncode(txtDemonName.Text) + ")</br>Scheme : " + ddl_Scheme.SelectedItem.Text;
            }
            ds = new DataSet();
            DataTable dt = (DataTable)Session["dt"];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = Session["distcode"].ToString();
            objUserBEL.UserType = Session["UserType"].ToString();
            ds = objUserDLL.FillGoDownByDistCodeUserType(objUserBEL);
            DataTable dtFillGoDown = ds.Tables[0];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropVariety();
            DataTable dtFillCropVariety = ds.Tables[0];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCrop();
            DataTable dtFillCrop = ds.Tables[0];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropCategory();
            DataTable dtFillCropCategory = ds.Tables[0];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillSource();
            DataTable dtFillSource = ds.Tables[0];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillMOUs();
            DataTable dtFillMOUs = ds.Tables[0];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            objUserBEL = new BLL_DropDown();
            objUserBEL.SEASON = ddlSeassion.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            ds = objUserDLL.FillPriceList(objUserBEL);
            DataTable dtFillPriceList = ds.Tables[0];

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            objUserBEL = new BLL_DropDown();
            objUserBEL.SEASON = ddlSeassion.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            ds = objUserDLL.FillPriceReceiveUnit(objUserBEL);
            DataTable dtFillPriceReceiveUnit = ds.Tables[0];

            List<GoDown> GoDownList = new List<GoDown>();
            List<Crop> croplist = new List<Crop>();
            List<CropCategory> cropCatList = new List<CropCategory>();
            List<CropVariety> cropVList = new List<CropVariety>();
            List<Source> sourceList = new List<Source>();
            List<MouData> MOUList = new List<MouData>();
            List<PriceList> priceList = new List<PriceList>();
            List<temp> lsttemp = new List<temp>();
            List<GridShow> lstGridShow = new List<GridShow>();
            List<PriceSourceMapping> lstPriceSource = new List<PriceSourceMapping>();

            foreach (DataRow row in dt.Rows)
            {
                temp obj = new temp();
                obj.GODOWN_ID = row["GODOWN_ID"].ToString();
                obj.RECEIVE_UNITCD = row["RECEIVE_UNITCD"].ToString();
                obj.MOU_REFNO = row["MOU_REFNO"].ToString();
                obj.CATEGORY_ID = row["CATEGORY_ID"].ToString();
                obj.CROP_ID = row["CROP_ID"].ToString();
                obj.CROP_CLASS = row["CROP_CLASS"].ToString();
                obj.VARIETY_ID = row["VARIETY_ID"].ToString();
                obj.LOT_NO = row["LOT_NO"].ToString();
                obj.BAG_SIZE = Convert.ToInt32(row["BAG_SIZE"].ToString());
                obj.NO_OF_BAGS = Convert.ToInt32(row["NO_OF_BAGS"].ToString());
                obj.QUANTITY = Convert.ToDouble(row["QUANTITY"].ToString());

                lsttemp.Add(obj);
            }

            foreach (DataRow row in dtFillGoDown.Rows)
            {
                GoDown obj = new GoDown();
                obj.GODOWN_ID = row["GODOWN_ID"].ToString();
                obj.GODOWN_NAME = row["GODOWN_NAME"].ToString();

                GoDownList.Add(obj);
            }

            foreach (DataRow row in dtFillCropVariety.Rows)
            {
                CropVariety obj = new CropVariety();
                obj.Variety_Code = row["Variety_Code"].ToString();
                obj.Crop_Code = row["Crop_Code"].ToString();
                obj.Variety_Name = row["Variety_Name"].ToString();

                cropVList.Add(obj);
            }

            foreach (DataRow row in dtFillCrop.Rows)
            {
                Crop obj = new Crop();
                obj.Crop_Code = row["Crop_Code"].ToString();
                obj.Category_Code = row["Category_Code"].ToString();
                obj.Crop_Name = row["Crop_Name"].ToString();

                croplist.Add(obj);
            }

            foreach (DataRow row in dtFillCropCategory.Rows)
            {
                CropCategory obj = new CropCategory();
                obj.Category_Code = row["Category_Code"].ToString();
                obj.Category_Name = row["Category_Name"].ToString();

                cropCatList.Add(obj);
            }

            foreach (DataRow row in dtFillSource.Rows)
            {
                Source obj = new Source();
                obj.AgenciesID = row["AgenciesID"].ToString();
                obj.Receive_Unitcd = row["Receive_Unitcd"].ToString();
                obj.Receive_Unitname = row["Receive_Unitname"].ToString();
                obj.IS_OAIC = Convert.ToBoolean(row["IS_OAIC"]);
                obj.IS_OSSC = Convert.ToBoolean(row["IS_OSSC"]);
                obj.Is_Active = Convert.ToBoolean(row["Is_Active"]);

                sourceList.Add(obj);
            }

            foreach (DataRow row in dtFillMOUs.Rows)
            {
                MouData obj = new MouData();
                obj.REF_NO = row["REF_NO"].ToString();
                obj.NAME = row["NAME"].ToString();
                obj.DIST_CODE = row["DIST_CODE"].ToString();
                obj.SESSION = row["SESSION"].ToString();
                obj.USER_TYPE = row["USER_TYPE"].ToString();

                MOUList.Add(obj);
            }

            foreach (DataRow row in dtFillPriceReceiveUnit.Rows)
            {
                PriceSourceMapping obj = new PriceSourceMapping();
                obj.RECEIVE_UNITCD = row["RECEIVE_UNITCD"].ToString();
                obj.PRICE_RECEIVE_UNITCD = row["PRICE_RECEIVE_UNITCD"].ToString();
                obj.SEASSION = row["SEASSION"].ToString();
                obj.FIN_YR = row["FIN_YR"].ToString();

                lstPriceSource.Add(obj);
            }

            foreach (DataRow row in dtFillPriceList.Rows)
            {
                PriceList obj = new PriceList();
                obj.Crop_class = row["Crop_class"].ToString();
                obj.RECEIVE_UNITCD = row["RECEIVE_UNITCD"].ToString();
                obj.Crop_Vcode = row["Crop_Vcode"].ToString();
                obj.seasons = row["seasons"].ToString();
                obj.All_in_cost_Price = Convert.ToDecimal(row["All_in_cost_Price"]);
                obj.GOI_Subsidy = Convert.ToDecimal(row["GOI_Subsidy"].ToString());
                obj.STATEPLAN_Subsidy = Convert.ToDecimal(row["STATEPLAN_Subsidy"].ToString());
                obj.TOT_SUBSIDY = Convert.ToDecimal(row["TOT_SUBSIDY"].ToString());
                obj.FARMER_PRICE = Convert.ToDecimal(row["FARMER_PRICE"].ToString());
                obj.Date_of_entry = Convert.ToDateTime(row["Date_of_entry"]);
                obj.F_Year = row["F_Year"].ToString();

                priceList.Add(obj);
            }

            var GODOWN_ID = lsttemp.Select(g => g.GODOWN_ID).Distinct();
            string GoDownNames = "";
            foreach (var k in GODOWN_ID)
            {
                string GoDownName = GoDownList.Where(a => a.GODOWN_ID == k).FirstOrDefault().GODOWN_NAME;//from a in GoDownList where a.GODOWN_ID == k select a.GODOWN_NAME.ToString().FirstOrDefault();
                GoDownNames += GoDownName + ",";
            }
            lbl_stockedat.Text = GoDownNames.Substring(0, GoDownNames.Length - 1);
            for (int i = 0; i < lsttemp.Count; i++)
            {
                GridShow grdshow = new GridShow();
                grdshow.GoDown_Name = GoDownList.Where(g => g.GODOWN_ID == lsttemp[i].GODOWN_ID).FirstOrDefault().GODOWN_NAME;
                grdshow.Crop_Name = croplist.Where(p => p.Crop_Code == lsttemp[i].CROP_ID).FirstOrDefault().Crop_Name;
                grdshow.Variety_Name = cropVList.Where(v => v.Variety_Code == lsttemp[i].VARIETY_ID).FirstOrDefault().Variety_Name;
                grdshow.CROP_CLASS = lsttemp[i].CROP_CLASS;
                grdshow.Receive_Unitname = sourceList.Where(s => s.Receive_Unitcd == lsttemp[i].RECEIVE_UNITCD).FirstOrDefault().Receive_Unitname;

                grdshow.LOT_NO = lsttemp[i].LOT_NO;
                grdshow.NO_OF_BAGS = lsttemp[i].NO_OF_BAGS;
                grdshow.BAG_SIZE = lsttemp[i].BAG_SIZE;
                grdshow.Quantity_sale = lsttemp[i].QUANTITY;
                string RECEIVE_UNITCD = lstPriceSource.Where(p => p.RECEIVE_UNITCD == lsttemp[i].RECEIVE_UNITCD && p.SEASSION == ddlSeassion.SelectedValue && p.FIN_YR == ddlFinYr.SelectedValue).FirstOrDefault().PRICE_RECEIVE_UNITCD;
                grdshow.All_in_cost_Price = priceList.Where(p => p.RECEIVE_UNITCD == RECEIVE_UNITCD && p.Crop_class == lsttemp[i].CROP_CLASS && p.Crop_Vcode == lsttemp[i].VARIETY_ID && p.seasons == ddlSeassion.SelectedValue && p.F_Year == ddlFinYr.SelectedValue).FirstOrDefault() != null ? priceList.Where(p => p.RECEIVE_UNITCD == RECEIVE_UNITCD && p.Crop_class == lsttemp[i].CROP_CLASS && p.Crop_Vcode == lsttemp[i].VARIETY_ID && p.seasons == ddlSeassion.SelectedValue && p.F_Year == ddlFinYr.SelectedValue).FirstOrDefault().All_in_cost_Price : 0;
                grdshow.total_amount = (Convert.ToDecimal(grdshow.Quantity_sale) * grdshow.All_in_cost_Price);

                lstGridShow.Add(grdshow);
            }

            grd_stockdtls.DataSource = lstGridShow;
            grd_stockdtls.DataBind();
            //grid footer
            grd_stockdtls.FooterRow.Cells[6].Text = "  TOTAL  ";
            grd_stockdtls.FooterRow.Cells[6].Font.Bold = true;

            grd_stockdtls.FooterRow.Cells[7].Font.Bold = true;
            for (int i = 4; i <= grd_stockdtls.Columns.Count - 1; i++)
            {
                int tot = 0;
                foreach (GridViewRow gdv in grd_stockdtls.Rows)
                {
                    Label lbl_NoofBags = (Label)gdv.FindControl("lbl_NoofBags");
                    tot = tot + Convert.ToInt32(lbl_NoofBags.Text);
                }
                grd_stockdtls.FooterRow.Cells[7].Text = tot.ToString();
            }

            grd_stockdtls.FooterRow.Cells[9].Font.Bold = true;
            for (int i = 6; i <= grd_stockdtls.Columns.Count - 1; i++)
            {
                Decimal total1 = 0;
                foreach (GridViewRow gdv in grd_stockdtls.Rows)
                {
                    Label lbl_Qtlsold = (Label)gdv.FindControl("lbl_Qtlsold");
                    total1 = total1 + Convert.ToDecimal(lbl_Qtlsold.Text);
                }
                grd_stockdtls.FooterRow.Cells[9].Text = total1.ToString();
            }

            grd_stockdtls.FooterRow.Cells[11].Font.Bold = true;
            for (int i = 8; i <= grd_stockdtls.Columns.Count - 1; i++)
            {
                Decimal total2 = 0;
                foreach (GridViewRow gdv in grd_stockdtls.Rows)
                {
                    Label lbl_amount = (Label)gdv.FindControl("lbl_amount");
                    total2 = total2 + Convert.ToDecimal(lbl_amount.Text);
                }
                grd_stockdtls.FooterRow.Cells[11].Text = total2.ToString();
            }
            ModalPopUp.Show();
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtn_stocksupplied.SelectedValue == "1" || rbtn_stocksupplied.SelectedValue == "6" || rbtn_stocksupplied.SelectedValue == "9")
            {
                //if (ddl_godown.SelectedIndex == 0)
                //{
                //    string popp = "alert('Select GoDown!!!')";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                //    ScriptManager.GetCurrent(Page).SetFocus(ddl_godown);
                //    return;
                //}
                if (string.IsNullOrWhiteSpace(txt_saledt.Text))
                {
                    string popp = "alert('Enter Delivery Date!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                    return;
                }
                else if (ddl_StockDeliveredTo.SelectedIndex == 0)
                {
                    string popp = "alert('Select Stock Delivered To!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_StockDeliveredTo);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txt_ddnum.Text))
                {
                    string popp = "alert('Enter DD Number!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_ddnum);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txt_amount.Text))
                {
                    string popp = "alert('Enter Amount!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_amount);
                    return;
                }
            }
            else if (rbtn_stocksupplied.SelectedValue == "2" || rbtn_stocksupplied.SelectedValue == "7")
            {
                if (string.IsNullOrWhiteSpace(txt_Deptcreditno.Text))
                {
                    string popp = "alert('Enter Credit Bill No!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_Deptcreditno);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txt_Deptdate.Text))
                {
                    string popp = "alert('Enter Date!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_Deptdate);
                    return;
                }
                //else if (ddl_godown.SelectedIndex == 0)
                //{
                //    string popp = "alert('Select GoDown!!!')";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                //    ScriptManager.GetCurrent(Page).SetFocus(ddl_godown);
                //    return;
                //}
                else if (string.IsNullOrWhiteSpace(txt_saledt.Text))
                {
                    string popp = "alert('Enter Sale Date!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                    return;
                }

                if (rbtn_depttype.SelectedValue == "01")
                {
                    if (ddl_SellTo.SelectedIndex == 0)
                    {
                        string popp = "alert('Select Sale To!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_SellTo);
                        return;
                    }
                }
                else if (rbtn_depttype.SelectedValue == "02")
                {
                    if (string.IsNullOrWhiteSpace(txt_saleto.Text))
                    {
                        string popp = "alert('Enter Sale To!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        ScriptManager.GetCurrent(Page).SetFocus(txt_saleto);
                        return;
                    }
                }
                if (RegDate.IsMatch(txt_Deptdate.Text))
                {
                }
                else
                {
                    string popp = "alert('Please Enter a valid Date!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_Deptdate);
                    return;
                }
            }
            else if (rbtn_stocksupplied.SelectedValue == "3" || rbtn_stocksupplied.SelectedValue == "8")
            {
                //if (ddl_godown.SelectedIndex == 0)
                //{
                //    string popp = "alert('Select GoDown!!!')";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                //    ScriptManager.GetCurrent(Page).SetFocus(ddl_godown);
                //    return;
                //}
                if (string.IsNullOrWhiteSpace(txt_saledt.Text))
                {
                    string popp = "alert('Enter Sale Date!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                    return;
                }
                else if (ddl_StockDeliveredTo.SelectedIndex == 0)
                {
                    string popp = "alert('Select Stock Delivered To!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_StockDeliveredTo);
                    return;
                }
            }
            else if (rbtn_stocksupplied.SelectedValue == "12")
            {
                //if (ddl_godown.SelectedIndex == 0)
                //{
                //    string popp = "alert('Select GoDown Name!!!')";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                //    ScriptManager.GetCurrent(Page).SetFocus(ddl_godown);
                //    return;
                //}
                if (string.IsNullOrWhiteSpace(txt_saledt.Text))
                {
                    string popp = "alert('Enter Sale Date!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txt_saleto.Text))
                {
                    string popp = "alert('Enter Farmer Name!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_saleto);
                    return;
                }
            }

            if (RegDate.IsMatch(txt_saledt.Text))
            {
            }
            else
            {
                string popp = "alert('Please Enter a valid Delivery Date!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                ScriptManager.GetCurrent(Page).SetFocus(txt_saledt);
                return;
            }


            pnlMainSection.Enabled = false;
            pnlDelivery.Visible = true;
            pnlDelivery.Enabled = true;
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void txt_bagsold_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt_bagsold = (TextBox)row.FindControl("txt_bagsold");
            Label lbl_bagsize = (Label)row.FindControl("lbl_bagsize");
            TextBox txt_quantitysold = (TextBox)row.FindControl("txt_quantitysold");
            Label lbl_bagno = (Label)row.FindControl("lbl_bagno");

            if (Convert.ToDouble(txt_bagsold.Text) > Convert.ToDouble(lbl_bagno.Text))
            {
                string popp = "alert('Balance Exceeds')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                txt_quantitysold.Text = string.Empty;
                txt_bagsold.Text = string.Empty;
                txt_bagsold.Focus();
            }
            else
            {
                if (txt_bagsold.Text != string.Empty)
                {
                    if (Convert.ToDouble(txt_bagsold.Text) > 0)
                    {

                        txt_quantitysold.Text = (Convert.ToDouble(txt_bagsold.Text) * Convert.ToDouble(lbl_bagsize.Text) / 100).ToString();

                    }
                    else
                    {
                        string popp = "alert('Please Enter No of Bags')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_quantitysold.Text = string.Empty;
                        txt_bagsold.Text = string.Empty;
                        txt_bagsold.Focus();
                    }
                }
                else
                {
                    string popp = "alert('Please Enter No of Bags')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                    txt_quantitysold.Text = string.Empty;
                    txt_bagsold.Text = string.Empty;
                    txt_bagsold.Focus();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void txt_ddnum_TextChanged(object sender, EventArgs e)
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.DD_NUMBER = txt_ddnum.Text;
            objUserDLL = new DLL_DropDown();

            int Cnt = objUserDLL.CheckSaleByDDNo(objUserBEL);
            if (Cnt > 0)
            {
                int Cnt1 = 0;
                objUserBEL = new BLL_DropDown();
                objUserBEL.SUPPLY_TYPE = rbtn_stocksupplied.SelectedValue;
                objUserBEL.DD_NUMBER = txt_ddnum.Text;
                objUserDLL = new DLL_DropDown();

                if (rbtn_stocksupplied.SelectedValue == "1" || rbtn_stocksupplied.SelectedValue == "6" || rbtn_stocksupplied.SelectedValue == "9")
                {
                    objUserBEL.SALE_TO = ddl_StockDeliveredTo.SelectedValue;
                    Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToDDNo(objUserBEL);

                    if (Cnt1 < 0)
                    {
                        txt_CashMemoNo.Focus();
                    }
                    else
                    {
                        string popp = "alert('Provided DD Number alreary exists!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_ddnum.Text = "";
                        txt_ddnum.Focus();
                    }
                }
                else if (rbtn_stocksupplied.SelectedValue == "2" || rbtn_stocksupplied.SelectedValue == "7")
                {
                    if (rbtn_depttype.SelectedValue == "01")
                    {
                        objUserBEL.SALE_TO = ddl_SellTo.SelectedValue;
                        Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToDDNo(objUserBEL);
                        if (Cnt1 < 0)
                        {
                            txt_CashMemoNo.Focus();
                        }
                        else
                        {
                            string popp = "alert('Provide DD Number alreary exists!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_ddnum.Text = "";
                            txt_ddnum.Focus();
                        }
                    }
                    else if (rbtn_depttype.SelectedValue == "02")
                    {
                        objUserBEL.SALE_TO = txt_saleto.Text;
                        Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToDDNo(objUserBEL);
                        if (Cnt1 < 0)
                        {
                            txt_CashMemoNo.Focus();
                        }
                        else
                        {
                            string popp = "alert('Provide DD Number alreary exists!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_ddnum.Text = "";
                            txt_ddnum.Focus();
                        }
                    }
                }
                else if (rbtn_stocksupplied.SelectedValue == "3" || rbtn_stocksupplied.SelectedValue == "8")
                {
                    objUserBEL.SALE_TO = ddl_StockDeliveredTo.Text;
                    Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToDDNo(objUserBEL);
                    if (Cnt1 < 0)
                    {
                        txt_CashMemoNo.Focus();
                    }
                    else
                    {
                        string popp = "alert('Provide DD Number alreary exists!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_ddnum.Text = "";
                        txt_ddnum.Focus();
                    }
                }
            }
            else
            {
                txt_CashMemoNo.Focus();
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void txt_CashMemoNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.CashMemo_No = txt_CashMemoNo.Text;
            objUserDLL = new DLL_DropDown();

            int Cnt = objUserDLL.CheckSaleByCashMemoNo(objUserBEL);
            if (Cnt > 0)
            {
                int Cnt1 = 0;
                objUserBEL = new BLL_DropDown();
                objUserBEL.SUPPLY_TYPE = rbtn_stocksupplied.SelectedValue;
                objUserBEL.CashMemo_No = txt_CashMemoNo.Text;
                objUserDLL = new DLL_DropDown();
                if (rbtn_stocksupplied.SelectedValue == "1" || rbtn_stocksupplied.SelectedValue == "6" || rbtn_stocksupplied.SelectedValue == "9")
                {
                    objUserBEL.SALE_TO = ddl_StockDeliveredTo.SelectedValue;
                    Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToCMNo(objUserBEL);
                    if (Cnt1 < 0)
                    {
                        txt_amount.Focus();
                    }
                    else
                    {
                        string popp = "alert('Provide CashMemo Number alreary exists!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_CashMemoNo.Text = "";
                        txt_CashMemoNo.Focus();
                    }
                }
                else if (rbtn_stocksupplied.SelectedValue == "2" || rbtn_stocksupplied.SelectedValue == "7")
                {
                    if (rbtn_depttype.SelectedValue == "01")
                    {
                        objUserBEL.SALE_TO = ddl_SellTo.SelectedValue;
                        Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToCMNo(objUserBEL);
                        if (Cnt1 < 0)
                        {
                            txt_amount.Focus();
                        }
                        else
                        {
                            string popp = "alert('Provide CashMemo Number alreary exists!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_CashMemoNo.Text = "";
                            txt_CashMemoNo.Focus();
                        }
                    }
                    else if (rbtn_depttype.SelectedValue == "02")
                    {
                        objUserBEL.SALE_TO = txt_saleto.Text;
                        Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToCMNo(objUserBEL);
                        if (Cnt1 < 0)
                        {
                            txt_amount.Focus();
                        }
                        else
                        {
                            string popp = "alert('Provide CashMemo Number alreary exists!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_CashMemoNo.Text = "";
                            txt_CashMemoNo.Focus();
                        }
                    }
                }
                else if (rbtn_stocksupplied.SelectedValue == "3" || rbtn_stocksupplied.SelectedValue == "8")
                {
                    objUserBEL.SALE_TO = ddl_StockDeliveredTo.Text;
                    Cnt1 = objUserDLL.CheckSaleBySupplyTypeSaleToCMNo(objUserBEL);
                    if (Cnt1 < 0)
                    {
                        txt_amount.Focus();
                    }
                    else
                    {
                        string popp = "alert('Provide CashMemo Number alreary exists!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_CashMemoNo.Text = "";
                        txt_CashMemoNo.Focus();
                    }
                }
            }
            else
            {
                txt_amount.Focus();
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void txt_saleto_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txt_saleto.Text))
        {
            tblDelivery.Visible = false;
            pnlContents.Visible = false;
        }
        else
        {
            tblDelivery.Visible = true;
            pnlContents.Visible = true;
        }
    }
    protected void grd_lotdtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    try
        //    {
        //        Label lbl_sourceid = (e.Row.FindControl("lbl_sourceid") as Label);
        //        Label lbl_lotno = (e.Row.FindControl("lbl_lotno") as Label);
        //        Label lbl_bagsize = (e.Row.FindControl("lbl_bagsize") as Label);
        //        Button btn_add = (e.Row.FindControl("btn_add") as Button);
        //        TextBox txt_bagsold = (e.Row.FindControl("txt_bagsold") as TextBox);
        //        TextBox txt_quantitysold = (e.Row.FindControl("txt_quantitysold") as TextBox);

        //        DataTable dt = (DataTable)Session["dt"];
        //        if (dt.Rows.Count > 0)
        //        {
        //            int COUNT = dt.Rows.Count;
        //            for (int r = 0; r < COUNT; ++r)
        //            {
        //                DataRow row = dt.Rows[r];
        //                if (row["RECEIVE_UNITCD"].ToString() == lbl_sourceid.Text && row["LOT_NO"].ToString() == lbl_lotno.Text && row["BAG_SIZE"].ToString() == lbl_bagsize.Text && row["VARIETY_ID"].ToString() == ddl_cropvariety.SelectedValue)
        //                {
        //                    txt_bagsold.Text = dt.Rows[r]["NO_OF_BAGS"].ToString();
        //                    txt_quantitysold.Text = dt.Rows[r]["QUANTITY"].ToString();
        //                    btn_add.Text = "Modify";
        //                }
        //            }
        //        }
        //    }
        //    catch (ApplicationException applicationException1)
        //    {
        //        ApplicationException applicationException = applicationException1;
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
        //        ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        //    }
        //    catch (Exception exception1)
        //    {
        //        Exception exception = exception1;
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        //    }
        //}
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            string DEPT_TYPE = rbtn_depttype.SelectedValue;
            string CREDIT_BILL_NO = txt_Deptcreditno.Text;
            DataTable dt = (DataTable)Session["dt"];
            dt.TableName = "A";
            string SaleTo = ddl_StockDeliveredTo.SelectedValue;
            if (rbtn_stocksupplied.SelectedValue == "2" || rbtn_stocksupplied.SelectedValue == "7")
            {
                if (rbtn_depttype.SelectedValue == "01")
                {
                    SaleTo = ddl_SellTo.SelectedValue;
                }
                else if (rbtn_depttype.SelectedValue == "02")
                {
                    SaleTo = txt_saleto.Text;
                }
            }
            else if (rbtn_stocksupplied.SelectedValue == "12")
            {
                SaleTo = txt_saleto.Text;
            }
            else if (rbtn_stocksupplied.SelectedValue == "13")
            {
                SaleTo = HttpUtility.HtmlEncode(txtDemonName.Text);
                DEPT_TYPE = ddl_StockDeliveredTo.SelectedItem.Text;
                CREDIT_BILL_NO = ddl_Scheme.SelectedItem.Text;
            }
            if (string.IsNullOrWhiteSpace(txt_amount.Text))
            {
                txt_amount.Text = "0";
            }
            string A = ConvertDatatableToXML(dt);
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = Session["distcode"].ToString();
            objUserBEL.UserType = Session["UserType"].ToString();
            string DealerType = "";
            if (rbtn_stocksupplied.SelectedValue == "9" || rbtn_stocksupplied.SelectedValue == "1")
            {
                objUserBEL.ISPACS = true;
                DealerType = "PACS";
            }
            else
            {
                objUserBEL.ISPACS = false;
                DealerType = "Dealer";
            }
            objUserBEL.SUPPLY_TYPE = rbtn_stocksupplied.SelectedValue;
            objUserBEL.CREDIT_BILL_NO = CREDIT_BILL_NO;
            objUserBEL.mDATE = txt_Deptdate.Text;
            objUserBEL.DEPT_TYPE = DEPT_TYPE;
            objUserBEL.Godown_ID = ddl_godown.SelectedValue;
            objUserBEL.Sale_Date = txt_saledt.Text;
            objUserBEL.SaleTo = SaleTo;
            objUserBEL.DD_NUMBER = ddlCollectNo.SelectedValue + "/" + txt_ddnum.Text;
            objUserBEL.AMOUNT = Convert.ToDecimal(txt_amount.Text);
            objUserBEL.CONFIRM_STATUS = Convert.ToChar("Y");
            objUserBEL.SESSION = ddlSeassion.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserBEL.PACSRebate = rbtnlPacsRebate.SelectedValue;
            objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            objUserBEL.XML_Value = ConvertDatatableToXML(dt);

            string Sale_Date = objUserBEL.Sale_Date;
            string FIN_YR = objUserBEL.FIN_YR;
            string SESSION = objUserBEL.SESSION;
            string DD_NUMBER = objUserBEL.DD_NUMBER;
            string ch_Date = objUserBEL.Sale_Date;
            string UserID = objUserBEL.UPDATED_BY;
            string UserIP = objUserBEL.UserIP;

            objUserDLL = new DLL_DropDown();
            int Val = 0;
            string CASH_MEMO_NO = "";
            objUserDLL.InsertSale(objUserBEL, out Val, out CASH_MEMO_NO);

            if (Val == 1)//SUCCESS
            {
                cSendDetails objUserBEL1;
                foreach (DataRow row in dt.Rows)
                {
                    objUserBEL = new BLL_DropDown();
                    objUserBEL.Crop_Verid = row["VARIETY_ID"].ToString();
                    objUserDLL = new DLL_DropDown();
                    string varietyName1 = objUserDLL.GetVarietyNameByVarietyCode(objUserBEL);

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.CROP_ID = row["CROP_ID"].ToString();
                    objUserDLL = new DLL_DropDown();
                    string cropName1 = objUserDLL.GetCropNameByCropCode(objUserBEL);

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.LIC_NO = SaleTo;
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    string partyName = "";
                    ds = objUserDLL.GetDistFirmNameLicenceNo(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            partyName = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
                        }
                    }

                    objUserBEL1 = new cSendDetails();
                    objUserBEL1.dist_Code = Session["LGDistrict"].ToString();
                    objUserBEL1.lot_Number = row["LOT_NO"].ToString();
                    objUserBEL1.source_ID = ddl_godown.SelectedValue;
                    objUserBEL1.sourceType = "GoDown";
                    objUserBEL1.receiver_ID = SaleTo;
                    objUserBEL1.receiverType = DealerType;
                    objUserBEL1.partyName = partyName;
                    objUserBEL1.qty_Per_Bag_Kg = row["BAG_SIZE"].ToString();
                    objUserBEL1.no_of_Bag = row["NO_OF_BAGS"].ToString(); ;
                    objUserBEL1.date_sale = Sale_Date;
                    objUserBEL1.year = FIN_YR;
                    objUserBEL1.season = SESSION;
                    objUserBEL1.ch_Number = DD_NUMBER;
                    objUserBEL1.ch_Date = Sale_Date;
                    objUserBEL1.crop = row["CROP_ID"].ToString();
                    objUserBEL1.cropName = row["CROP_NAME"].ToString();
                    objUserBEL1.variety = row["VARIETY_ID"].ToString();
                    objUserBEL1.varietyName = row["VARIETY_NAME"].ToString();
                    objUserBEL1.Class = row["CROP_CLASS"].ToString();
                    objUserBEL1.UserID = UserID;
                    objUserBEL1.UserIP = UserIP;
                    objUserBEL1.CropCatg_ID = row["BAG_SIZE"].ToString();
                    objUserBEL1.CASH_MEMO_NO = CASH_MEMO_NO;
                    objUserBEL1.APIKEY = "key01001";
                    SendSaleDetails(objUserBEL1);
                }

                Session["GoDown"] = ddl_godown.SelectedValue;
                Session["GoDownName"] = ddl_godown.SelectedItem.Text;
                Session["Sale_Date"] = txt_saledt.Text;
                Session["CashMemo"] = CASH_MEMO_NO;
                FillSupplyType();
                tr_CreditBillNo.Visible = false;
                tr_Date.Visible = false;
                tr_Select.Visible = false;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = false;
                tr_DDNumber.Visible = true;
                //tr_CashMemoNo.Visible = true;
                tr_Amount.Visible = true;

                pnlMainSection.Enabled = true;
                pnlDelivery.Enabled = false;
                pnlDelivery.Visible = false;
                FILL_GODOWN();
                FILLSTOCKDELIVEREDTO();
                FILL_CROPCATAGORY();
                FILLCROPNAME();
                FILLCROPVARIETY();

                btn_Submit.Enabled = false;
                btn_ExistVariety.Enabled = false;
                txt_saledt.Text = "";
                txt_ddnum.Text = "";
                txt_CashMemoNo.Text = "";
                txt_amount.Text = "";

                tr_CreditBillNo.Visible = false;
                tr_Date.Visible = false;
                tr_Select.Visible = false;
                tr_SelectGoDown.Visible = true;
                tr_Deliverydate.Visible = true;
                tr_SaleTo.Visible = false;
                tr_StockDeliveredTo.Visible = true;
                tr_DDNumber.Visible = true;
                //tr_CashMemoNo.Visible = true;
                tr_Amount.Visible = true;


                CREATENEWDATATABLE();
                grd_lottrans.DataSource = null;
                grd_lottrans.DataBind();

                grd_lotdtls.DataSource = null;
                grd_lotdtls.DataBind();

                grd_stockdtls.DataSource = null;
                grd_stockdtls.DataBind();

                if (rbtn_stocksupplied.SelectedValue == "13")
                {
                    string popicon = "window.open('RptDemonVoucher.aspx','Report','width=1020,height=900,toolbar=1,resizable=1,location=0, status=0, menubar=0, scrollbars=1');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popicon", popicon, true);
                }
                else
                {
                    string popicon = "window.open('RptSaleVoucher.aspx','Report','width=1020,height=900,toolbar=1,resizable=1,location=0, status=0, menubar=0, scrollbars=1');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popicon", popicon, true);
                }
                ActivityLog activityLog = new ActivityLog()
                {
                    UserID = this.Session["UserID"].ToString(),
                    UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                    ActivityType = "Action",
                    Activity = "SALE Entry",
                    PageURL = HttpContext.Current.Request.Url.ToString(),
                    Remark = "SALE Entry Successfully !",
                    OS = Request.Browser.Platform.ToString(),
                    BWSER = Request.Browser.Type.ToString()
                };
                ActivityLog.InsertActivityLog_Stock(activityLog);
            }
            else if (Val == 0)//UN-SUCCESS
            {
                string popp = "alert('Oops Transaction Un-Successfull !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            }

            ModalPopUp.Hide();
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ModalPopUp.Hide();
    }
    public string ConvertDatatableToXML(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    protected void ddlFinYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSeassion();
        FILL_CROPCATAGORY();
        FILLCROPNAME();
        FILLCROPVARIETY();
    }
    protected void ddlSeassion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FILL_CROPCATAGORY();
        FILLCROPNAME();
        FILLCROPVARIETY();
    }
    public void SendSaleDetails(cSendDetails objUserBEL1)
    {
        try
        {
            string apiUrl = "http://164.100.140.107/addSellStockbySIS";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            var json = JsonConvert.SerializeObject(objUserBEL1);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(apiUrl, stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                string pop1 = "alert('" + responseString + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
            }
        }
        catch (Exception ex)
        {
            string pop1 = "alert('Oops!!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
        }
        //return fdetails;
    }
}

public class GoDown
{
    public string GODOWN_ID { get; set; }
    public string GODOWN_NAME { get; set; }
}
public class Crop
{
    public string Crop_Code { get; set; }
    public string Category_Code { get; set; }
    public string Crop_Name { get; set; }
    public bool IS_ACTIVE { get; set; }
}
public class CropVariety
{
    public string Variety_Code { get; set; }
    public string Crop_Code { get; set; }
    public string Variety_Name { get; set; }
    public bool IS_ACTIVE { get; set; }
}
public class CropCategory
{
    public string Category_Code { get; set; }
    public string Category_Name { get; set; }
    public bool IS_ACTIVE { get; set; }
}
public class Source
{
    public string AgenciesID { get; set; }
    public string Receive_Unitcd { get; set; }
    public string Receive_Unitname { get; set; }
    public bool IS_OAIC { get; set; }
    public bool IS_OSSC { get; set; }
    public bool Is_Active { get; set; }
}
public class MouData
{
    public string REF_NO { get; set; }
    public string NAME { get; set; }
    public string DIST_CODE { get; set; }
    public string SESSION { get; set; }
    public string USER_TYPE { get; set; }
    public bool IS_ACTIVE { get; set; }
}
public class PriceList
{
    public string Crop_class { get; set; }
    public string RECEIVE_UNITCD { get; set; }
    public string Crop_Vcode { get; set; }
    public string seasons { get; set; }
    public decimal All_in_cost_Price { get; set; }
    public decimal GOI_Subsidy { get; set; }
    public decimal STATEPLAN_Subsidy { get; set; }
    public decimal TOT_SUBSIDY { get; set; }
    public decimal FARMER_PRICE { get; set; }
    public DateTime Date_of_entry { get; set; }
    public string F_Year { get; set; }
}
public class temp
{
    public string GODOWN_ID { get; set; }
    public string GODOWN_NAME { get; set; }
    public string RECEIVE_UNITCD { get; set; }
    public string MOU_REFNO { get; set; }
    public string CATEGORY_ID { get; set; }
    public string CROP_ID { get; set; }
    public string CROP_CLASS { get; set; }
    public string VARIETY_ID { get; set; }
    public string LOT_NO { get; set; }
    public int BAG_SIZE { get; set; }
    public int NO_OF_BAGS { get; set; }
    public double QUANTITY { get; set; }
}
public class GridShow
{
    public string GoDown_Name { get; set; }
    public string Crop_Name { get; set; }
    public string Variety_Name { get; set; }
    public string CROP_CLASS { get; set; }
    public string Receive_Unitname { get; set; }
    public string LOT_NO { get; set; }
    public int NO_OF_BAGS { get; set; }
    public int BAG_SIZE { get; set; }
    public double Quantity_sale { get; set; }
    public decimal All_in_cost_Price { get; set; }
    public decimal total_amount { get; set; }
}
public class PriceSourceMapping
{
    public string RECEIVE_UNITCD { get; set; }
    public string PRICE_RECEIVE_UNITCD { get; set; }
    public string SEASSION { get; set; }
    public string FIN_YR { get; set; }
}

public class cSendDetails
{
    public string dist_Code { get; set; }
    public string lot_Number { get; set; }
    public string source_ID { get; set; }
    public string partyName { get; set; }
    public string sourceType { get; set; }
    public string receiver_ID { get; set; }
    public string receiverType { get; set; }
    public string qty_Per_Bag_Kg { get; set; }
    public string no_of_Bag { get; set; }
    public string date_sale { get; set; }
    public string year { get; set; }
    public string season { get; set; }
    public string ch_Number { get; set; }
    public string ch_Date { get; set; }
    public string crop { get; set; }
    public string cropName { get; set; }
    public string variety { get; set; }
    public string varietyName { get; set; }
    public string Class { get; set; }
    public string UserID { get; set; }
    public string UserIP { get; set; }
    public string CropCatg_ID { get; set; }
    public string CASH_MEMO_NO { get; set; }
    public string APIKEY { get; set; }//key01001
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PriceList : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillFinYr();
            FillSeassion();
            FillCrop();
            FillSource();
            FillCropVariety();
        }
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
    }
    private void FillCrop()
    {
        try
        {
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.Crop_Bind(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCrop.DataSource = ds;
                    ddlCrop.DataTextField = "Crop_Name";
                    ddlCrop.DataValueField = "Crop_Code";
                    ddlCrop.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
    }
    private void FillSource()
    {
        try
        {
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.Source_Bind(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSource.DataSource = ds;
                    ddlSource.DataTextField = "Receive_Unitname";
                    ddlSource.DataValueField = "Receive_Unitcd";
                    ddlSource.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
    }

    private void FillCropVariety()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserBEL.ABOVE10YEAR = ddlVarietyAge.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.Variety_Bind(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCrop.DataSource = ds;
                    gvCrop.DataBind();
                }
                else
                {
                    string ppp = "alert('Variety Not Available ! ! !');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
                    gvCrop.DataSource = null;
                    gvCrop.DataBind();
                }
            }
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
    }


    protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCropVariety();
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
    private void CreteDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[2] 
                { 
                    new DataColumn("Variety_Code", typeof(string)),
                    new DataColumn("Variety_Name", typeof(string))
                });
        Session.Add("dt", dt);
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        CreteDataTable();
        DataTable dt = (DataTable)Session["dt"];
        foreach (GridViewRow row in gvCrop.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbStatus = (row.Cells[1].FindControl("cbStatus") as CheckBox);
                Label lblVCode = (row.Cells[1].FindControl("lblVCode") as Label);
                Label lblVName = (row.Cells[1].FindControl("lblVName") as Label);
                if (cbStatus.Checked)
                {

                    dt.Rows.Add(lblVCode.Text, lblVName.Text);
                    Session.Add("dt", dt);

                }
            }
        }
        string os = null;
        String userAgent = Request.UserAgent;

        if (userAgent.IndexOf("Windows NT 6.3") > 0)
        {
            os = "Windows 8.1";
        }
        else if (userAgent.IndexOf("Windows NT 6.2") > 0)
        {
            os = "Windows 8";
        }
        else if (userAgent.IndexOf("Windows NT 6.1") > 0)
        {
            os = "Windows 7";
        }
        else if (userAgent.IndexOf("Windows NT 6.0") > 0)
        {
            os = "Windows Vista";
        }
        else if (userAgent.IndexOf("Windows NT 5.2") > 0)
        {
            os = "Windows Server 2003";// Windows XP x64 Edition
        }
        else if (userAgent.IndexOf("Windows NT 5.1") > 0)
        {
            os = "Windows XP";
        }
        else if (userAgent.IndexOf("Windows NT 5.01") > 0)
        {
            os = "Windows 2000(SP1)";
        }
        else if (userAgent.IndexOf("Windows NT 5.0") > 0)
        {
            os = "Windows 2000";
        }
        else if (userAgent.IndexOf("Windows NT 4.0") > 0)
        {
            os = "Windows NT 4.0";
        }
        else if (userAgent.IndexOf("Win 9x 4.90") > 0)
        {
            os = "Windows Me";
        }
        else if (userAgent.IndexOf("Windows 98") > 0)
        {
            os = "Windows 98";
        }
        else if (userAgent.IndexOf("Windows 95") > 0)
        {
            os = "Windows 95";
        }
        else if (userAgent.IndexOf("Windows CE") > 0)
        {
            os = "Windows CE";
        }
        else if (userAgent.IndexOf("Intel Mac OS X") > 0)
        {
            os = "Mac OS X";
        }
        else if (userAgent.IndexOf("Windows NT 10.0") > 0)
        {
            os = "Windows 10";
        }
        else
        {
            os = "Others";
        }
        dt.TableName = "A";
        string A = ConvertDatatableToXML(dt);
        objUserBEL = new BLL_DropDown();
        objUserBEL.XML_Value = ConvertDatatableToXML(dt);
        objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
        objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
        objUserBEL.CROP_ID = ddlCrop.SelectedValue;
        objUserBEL.ABOVE10YEAR = ddlVarietyAge.SelectedValue;
        objUserBEL.RECEIVE_UNITCD = ddlSource.SelectedValue;
        objUserBEL.Transportation = Convert.ToDecimal(txtTransportation.Text);
        objUserBEL.Dealercommsiosn = Convert.ToDecimal(txtDealerCommission.Text);
        objUserBEL.Pacscommsiosn = Convert.ToDecimal(txtPacsCommission.Text);
        objUserBEL.allincostprice = Convert.ToDecimal(txtallincost.Text);
        objUserBEL.goisubsidy = Convert.ToDecimal(txtgoiprice.Text);
        objUserBEL.spsubsidy = Convert.ToDecimal(txtspprice.Text);
        objUserBEL.loadingcharges = Convert.ToDecimal(txtloadcharges.Text);

        objUserBEL.UPDATED_BY = Session["Name"].ToString();
        objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
        objUserBEL.UID = Session["Name"].ToString();
        objUserBEL.URL = Request.Url.AbsolutePath.ToString();
        objUserBEL.DEVICETYPE = Request.Browser.IsMobileDevice.ToString();
        objUserBEL.MODEL = Request.Browser.MobileDeviceModel.ToString();
        objUserBEL.MANUFACTURE = Request.Browser.MobileDeviceManufacturer.ToString();
        objUserBEL.OS = os;
        objUserBEL.BWSER = Request.Browser.Type.ToString();
        objUserBEL.SCREENSIZE = Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString();
        objUserDLL = new DLL_DropDown();

        int outputvalue = 0;

        objUserDLL.InsertPriceListFordept(objUserBEL, out outputvalue);

        if (outputvalue == 1)
        {
            string ppp = "alert('Crop Variety has been added ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
            ResetAll();

        }
        else
        {
            string ppp = "alert('some error occurred. please contact application administrator ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
            ResetAll();
        }
    }
    private void ResetAll()
    {
        txtTransportation.Text = "";
        txtDealerCommission.Text = "";
        txtPacsCommission.Text = "";
        txtallincost.Text = "";
        txtgoiprice.Text = "";
        txtgoiprice.Text = "";
        txtspprice.Text = "";
        txtloadcharges.Text = "";
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        ResetAll();
    }
    protected void ddlVarietyAge_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCropVariety();
    }
}
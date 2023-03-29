using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Crop : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    Regex RegDec = new Regex(@"^\-{0,1}\d+(.\d+){0,1}$");

    protected void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string cach = "no-cache,must-revalidate,no-store";
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", cach);
        if (!IsPostBack)
        {
            FillFinYr();
            FillSeassion();
            FillCropCatgory();
            FillCrop();
            FillCrop1();
        }
    }
    private void FillCrop()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.CROPCATG_ID = ddlCropCategory.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
            objUserBEL.STATUS = null;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCrop(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCrop.DataSource = ds;
                    gvCrop.DataBind();
                }
                else
                {
                    string ppp = "alert('Crop Record Not Available ! ! !');";
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
    private void FillCrop1()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.CROPCATG_ID = ddlCropCategory.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
            objUserBEL.STATUS = "1";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCrop(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCrop1.DataSource = ds;
                    gvCrop1.DataBind();
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
    private void FillCropCatgory()
    {
        try
        {
            ddlCropCategory.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
            objUserBEL.STATUS = "1";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropCategory(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCropCategory.DataSource = ds;
                    ddlCropCategory.DataTextField = "CATEGORY_NAME";
                    ddlCropCategory.DataValueField = "CATEGORY_CODE";
                    ddlCropCategory.DataBind();
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
    private void CreteDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] 
                { 
                    new DataColumn("CATEGORY_CODE", typeof(string)),
                    new DataColumn("CROP_CODE", typeof(string)),
                    new DataColumn("CROP_NAME", typeof(string)),
                    new DataColumn("Txt_Max_Qntl", typeof(string)),
                });
        Session.Add("dt", dt);
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CreteDataTable();
        DataTable dt = (DataTable)Session["dt"];
        foreach (GridViewRow row in gvCrop.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbStatus = (row.Cells[1].FindControl("cbStatus") as CheckBox);
                Label lblCropCode = (row.Cells[1].FindControl("lblCropCode") as Label);
                Label lblCropName = (row.Cells[1].FindControl("lblCropName") as Label);
                TextBox txtmaxsusidy = (row.Cells[1].FindControl("txtmaxsusidy") as TextBox);
                if (cbStatus.Checked)
                {
                    if (RegDec.IsMatch(txtmaxsusidy.Text))
                    {
                    }
                    else
                    {
                        string ppp = "alert('Please set the MAX Susbsidy for the crop');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
                        cbStatus.Checked = false;
                        ScriptManager.GetCurrent(Page).SetFocus(txtmaxsusidy);
                        txtmaxsusidy.Text = "";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtmaxsusidy.Text))
                    {
                        string ppp = "alert('Please set the MAX Susbsidy for the crop');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
                        cbStatus.Checked = false;
                        ScriptManager.GetCurrent(Page).SetFocus(txtmaxsusidy);
                        txtmaxsusidy.Text = "";
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(ddlCropCategory.SelectedValue, lblCropCode.Text, lblCropName.Text, txtmaxsusidy.Text);
                        Session.Add("dt", dt);
                    }
                }
            }


        }
        lblSeassionName.Text = ddlSeassion.SelectedItem.Text;
        lblYearName.Text = ddlFinYr.Text;
        lblCategoryName.Text = ddlCropCategory.SelectedValue.ToString();
        DataTable dt1 = (DataTable)Session["dt"];
        gvSelectedCrop.DataSource = dt1;
        gvSelectedCrop.DataBind();

        if (dt1.Rows.Count > 0)
        {
            popupEdit.Show();
        }
        else
        {
            string ppp = "alert('Please Select The Crop ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);


        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
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
        DataTable dt = (DataTable)Session["dt"];
        dt.TableName = "A";
        string A = ConvertDatatableToXML(dt);
        objUserBEL = new BLL_DropDown();
        objUserBEL.XML_Value = ConvertDatatableToXML(dt);
        objUserBEL.FIN_YR = lblYearName.Text;
        objUserBEL.SEASON = lblSeassionName.Text;
        objUserBEL.CROPCATG_ID = lblCategoryName.Text;
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
        objUserDLL.InsertCrop(objUserBEL, out outputvalue);

        if (outputvalue == 1)
        {
            string ppp = "alert('Crop Category has been added ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
            FillCrop();
            FillCrop1();
        }
        else
        {
            string ppp = "alert('some error occurred. please contact application administrator ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
        }
    }
    protected void ddlCropCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCrop();
        FillCrop1();
    }
}
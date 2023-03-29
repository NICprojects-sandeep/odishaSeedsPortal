using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Variety : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
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
            FillCropVariety();
            FillCropVariety1();
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
                    ddlCropCategory.Items.Insert(0,"--SELECT--");
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
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
            objUserBEL.STATUS = "1";
            objUserBEL.CROPCATG_ID = ddlCropCategory.SelectedValue.ToString();
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCrop(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCrop.DataSource = ds;
                    ddlCrop.DataTextField = "Crop_Name";
                    ddlCrop.DataValueField = "Crop_Code";
                    ddlCrop.DataBind();
                    ddlCrop.Items.Insert(0,"--SELECT--");
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
            // objUserBEL.CROPCATG_ID = ddlCropCategory.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserBEL.STATUS = null;
            objUserBEL.ABOVE10YEAR = "0";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropVariety(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvVariety.DataSource = ds;
                    gvVariety.DataBind();
                }
                else
                {
                    string ppp = "alert('Variety Not Available ! ! !');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
                    gvVariety.DataSource = null;
                    gvVariety.DataBind();
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
    private void FillCropVariety1()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            // objUserBEL.CROPCATG_ID = ddlCropCategory.SelectedValue;
            objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedItem.Text;
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserBEL.STATUS = "1";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropVariety(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvVariety1.DataSource = ds;
                    gvVariety1.DataBind();
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

    protected void ddlCropCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCrop();
    }
    private void CreteDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] 
                { 
                    new DataColumn("CROP_CODE", typeof(string)),
                    new DataColumn("Variety_Code", typeof(string)),
                    new DataColumn("Variety_Name", typeof(string)),
                    new DataColumn("ABOVE_10YEAR", typeof(string))
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
        foreach (GridViewRow row in gvVariety.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbStatus = (row.Cells[1].FindControl("cbStatus") as CheckBox);
                Label lblVCode = (row.Cells[1].FindControl("lblVCode") as Label);
                Label lblVName = (row.Cells[1].FindControl("lblVName") as Label);
                CheckBox duartionStatus = (row.Cells[1].FindControl("duartionStatus") as CheckBox);
                if (cbStatus.Checked)
                {
                    if (duartionStatus.Checked)
                    {
                        dt.Rows.Add(ddlCrop.SelectedValue, lblVCode.Text, lblVName.Text, 1);
                        Session.Add("dt", dt);
                    }
                    else
                    {
                        dt.Rows.Add(ddlCrop.SelectedValue, lblVCode.Text, lblVName.Text);
                        Session.Add("dt", dt);
                    }
                }
            }
        }
        lblSeassionName.Text = ddlSeassion.SelectedItem.Text;
        lblYearName.Text = ddlFinYr.Text;
        lblCropName.Text = ddlCrop.SelectedItem.ToString();
        DataTable dt1 = (DataTable)Session["dt"];
        gvSelectedVariety.DataSource = dt1;
        gvSelectedVariety.DataBind();
        if (dt1.Rows.Count > 0)
        {
            popupEdit.Show();
        }
        else
        {
            string ppp = "alert('Please Select The Variety ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
        }

    }
    protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCropVariety();
        FillCropVariety1();
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
        objUserBEL.CROP_ID = ddlCrop.SelectedValue;
        objUserBEL.UPDATED_BY = Session["Name"].ToString();
        objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
        objUserBEL.UID = Session["Name"].ToString();
        // objUserBEL.ABOVE10YEAR = null;
        objUserBEL.URL = Request.Url.AbsolutePath.ToString();
        objUserBEL.DEVICETYPE = Request.Browser.IsMobileDevice.ToString();
        objUserBEL.MODEL = Request.Browser.MobileDeviceModel.ToString();
        objUserBEL.MANUFACTURE = Request.Browser.MobileDeviceManufacturer.ToString();
        objUserBEL.OS = os;
        objUserBEL.BWSER = Request.Browser.Type.ToString();
        objUserBEL.SCREENSIZE = Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString();
        objUserDLL = new DLL_DropDown();
        int outputvalue = 0;
        objUserDLL.InsertVariety(objUserBEL, out outputvalue);

        if (outputvalue == 1)
        {
            string ppp = "alert('Crop Variety has been added ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
            FillCropVariety();
            FillCropVariety1();
        }
        else
        {
            string ppp = "alert('some error occurred. please contact application administrator ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CropCategory : System.Web.UI.Page
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
            FillCropCatgory1();
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
            objUserBEL.STATUS = null;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropCategory(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCategory.DataSource = ds;
                    gvCategory.DataBind();
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
    private void FillCropCatgory1()
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
                    gvCategory1.DataSource = ds;
                    gvCategory1.DataBind();
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
        dt.Columns.AddRange(new DataColumn[2] 
                { 
                    new DataColumn("CATEGORY_CODE", typeof(string)),
                    new DataColumn("CATEGORY_NAME", typeof(string))
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
        foreach (GridViewRow row in gvCategory.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbStatus = (row.Cells[1].FindControl("cbStatus") as CheckBox);
                Label lblCategoryCode = (row.Cells[1].FindControl("lblCategoryCode") as Label);
                Label lblCategoryName = (row.Cells[1].FindControl("lblCategoryName") as Label);
                if (cbStatus.Checked)
                {
                    //string CategoryCode = row.Cells[1].Text;
                    //string storname = row.Cells[2].Text;
                    dt.Rows.Add(lblCategoryCode.Text, lblCategoryName.Text);

                    Session.Add("dt", dt);
                }
            }
        }
        lblSeassionName.Text = ddlSeassion.SelectedItem.Text;
        lblYearName.Text = ddlFinYr.Text;
        DataTable dt1 = (DataTable)Session["dt"];
        gvSelectedGropCategory.DataSource = dt1;
        gvSelectedGropCategory.DataBind();
        if (dt1.Rows.Count > 0)
        {
            popupEdit.Show();
        }
        else
        {
            string ppp = "alert('Please Select The Category ! ! !');";
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
        objUserDLL.InsertCropCategory(objUserBEL, out outputvalue);

        if (outputvalue == 1)
        {
            string ppp = "alert('Crop Category has been added ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
            FillCropCatgory();
            FillCropCatgory1();
        }
        else
        {
            string ppp = "alert('some error occurred. please contact application administrator ! ! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
        }
    }
}
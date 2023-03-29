using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_OsscLotEntry : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
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
            if (!(this.Session["UserType"].ToString() == "OAIC") && !(this.Session["UserType"].ToString() == "OSSC") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }
            if (!this.Page.IsPostBack)
            {
                FillCrop();
                FillVariety();
                FillLots();
                return;
            }
        }
    }
    private void FillVariety()
    {
        try
        {
            ddlVariety.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropVarietyByCropId(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVariety.DataSource = ds;
                    ddlVariety.DataTextField = "Variety_Name";
                    ddlVariety.DataValueField = "Variety_Code";
                    ddlVariety.DataBind();
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
    private void FillCrop()
    {
        try
        {
            ddlCrop.Items.Clear();
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCrop();
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
        catch (Exception exception1)
        {
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }
    protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillVariety();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlCrop.SelectedIndex == -1)
        {
            string pop = "alert('Please Select Crop !!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlCrop);
            return;
        }
        else if (ddlVariety.SelectedIndex == -1)
        {
            string pop = "alert('Please Select Variety !!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlVariety);
            return;
        }
        else if (txtLotNo.Text == "")
        {
            string pop = "alert('Please enter the Lot Number!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            ScriptManager.GetCurrent(Page).SetFocus(txtLotNo);
            return;
        }
        else if (txtQty.Text == "")
        {
            txtQty.Text = "0";
        }
        try
        {
            decimal Qty = 0;
            if (string.IsNullOrWhiteSpace(txtQty.Text))
            {
                Qty = 0;
            }
            else
            {
                Qty = Convert.ToDecimal(txtQty.Text);
            }
            objUserBEL = new BLL_DropDown();
            objUserBEL.slno = 0;
            objUserBEL.CROP_ID = ddlCrop.SelectedValue;
            objUserBEL.Crop_Verid = ddlVariety.SelectedValue;
            objUserBEL.LOT_NO = txtLotNo.Text;
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserBEL.AMOUNT = Qty;
            objUserBEL.SaleTo = txtSource.Text;
            objUserBEL.ApprovaTypeID = 1;
            objUserBEL.REF_NO = "";
            objUserBEL.TypeId = 1;

            objUserDLL = new DLL_DropDown();
            int Val = objUserDLL.InsertClass1(objUserBEL);
            if (Val == 1)//SUCCESS
            {
                string pop = "alert('Lot Number Added Successfully !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                FillLots();
            }
            else if (Val == 2)
            {
                string pop = "alert('Lot Number Already Exists !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            }
            else if (Val == 0)
            {
                string pop = "alert('Some error occured !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
            }

            txtLotNo.Text = "";
            txtQty.Text = "";
            txtSource.Text = "";
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
    private void FillLots()
    {
        objUserBEL = new BLL_DropDown();
        //objUserBEL.CROP_ID = ddlCrop.SelectedValue;
        //objUserBEL.Crop_Verid = ddlVariety.SelectedValue;
        objUserBEL.UPDATED_BY = Session["UserID"].ToString();
        objUserBEL.TypeId = 1;

        int Cnt = 0;
        StringBuilder str = new StringBuilder("");
        objUserDLL = new DLL_DropDown();
        ds = objUserDLL.ClassSel1(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                str.Append("<table class='table table-bordered' width='100%'");
                str.Append("<thead>");
                str.Append("<tr style='text-align:left;'>");
                str.Append("<th>SL NO</th>");
                str.Append("<th>LOT NUMBER</th>");
                str.Append("<th>QUANTITY</th>");
                str.Append("<th>SOURCE</th>");
                str.Append("<th>STATUS</th>");
                str.Append("</tr>");
                str.Append("</thead>");
                str.Append("<tbody>");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Cnt += 1;
                        str.Append("<tr style='text-align:left;'>");
                        str.Append("<td>" + Cnt.ToString() + "</td>");
                        str.Append("<td>" + dr["LOT_NO"].ToString() + "</td>");
                        str.Append("<td>" + dr["QTY"].ToString() + "</td>");
                        str.Append("<td>" + dr["SOURCE"].ToString() + "</td>");
                        str.Append("<td>" + dr["IS_OSSOPCA"].ToString() + "</td>");
                        str.Append("</tr>");
                    }
                }
                str.Append("</tbody>");
                str.Append("</table>");
            }
        }
        litReport.Text = str.ToString();

    }
}
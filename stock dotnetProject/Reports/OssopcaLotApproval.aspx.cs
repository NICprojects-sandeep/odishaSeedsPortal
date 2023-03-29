using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_OssopcaLotApproval : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null)
        {
            this.utl.SessionReset();
            base.Response.Redirect("../Login.aspx", true);
            return;
        }
        else
        {
            if (!this.Page.IsPostBack)
            {
                FillFinYr();
                FillLots();
            }
        }        
    }
    private void FillLots()
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.CROP_ID = ddlYear.SelectedValue;
        objUserBEL.Crop_Verid = ddlSeassion.SelectedValue;
        objUserBEL.UPDATED_BY = Session["UserID"].ToString();
        objUserBEL.TypeId = 2;

        int Cnt = 0;
        StringBuilder str = new StringBuilder("");
        objUserDLL = new DLL_DropDown();
        ds = objUserDLL.ClassSel1(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvLots.DataSource = ds;
                gvLots.DataBind();

                //str.Append("<table class='table table-bordered'");
                //str.Append("<thead>");
                //str.Append("<tr>");
                //str.Append("<th>SL NO</th>");
                //str.Append("<th>LOT NUMBER</th>");
                //str.Append("<th>QUANTITY</th>");
                //str.Append("<th>SOURCE</th>");
                //str.Append("</tr>");
                //str.Append("</thead>");
                //str.Append("<tbody>");
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in ds.Tables[0].Rows)
                //    {
                //        Cnt += 1;
                //        str.Append("<tr>");
                //        str.Append("<td>" + Cnt.ToString() + "</td>");
                //        str.Append("<td>" + dr["LOT_NO"].ToString() + "</td>");
                //        str.Append("<td>" + dr["QTY"].ToString() + "</td>");
                //        str.Append("<td>" + dr["SOURCE"].ToString() + "</td>");
                //        //gvClass.DataSource = ds;
                //        //gvClass.DataBind();
                //        str.Append("</tr>");
                //    }
                //}
                //str.Append("</tbody>");
                //str.Append("</table>");
            }
        }
        //litReport.Text = str.ToString();
    }
    private void FillFinYr()
    {
        try
        {
            ddlYear.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.STATUS = "A";
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillFinYr(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlYear.DataSource = ds;
                    ddlYear.DataTextField = "FIN_YR";
                    ddlYear.DataValueField = "FIN_YR";
                    ddlYear.DataBind();
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        InsertLot();
        FillLots();
    }
    private void InsertLot()
    {
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
            objUserBEL.FIN_YR = ddlYear.SelectedValue;
            objUserBEL.SEASON = ddlSeassion.SelectedValue;
            objUserBEL.LOT_NO = txtLotNo.Text;
            objUserBEL.UPDATED_BY = Session["UserID"].ToString();
            objUserBEL.AMOUNT = Qty;
            objUserBEL.SaleTo = txtSource.Text;
            objUserBEL.REF_NO = txtReason.Text;
            objUserBEL.TypeId = 4;

            objUserDLL = new DLL_DropDown();
            int Val = objUserDLL.InsertClass(objUserBEL);
            if (Val == 1)//SUCCESS
            {
                string pop = "alert('Lot Number Added Successfully !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
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
            txtReason.Text = "";
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
    protected void ddl_Action_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            this.Page.MaintainScrollPositionOnPostBack = true;
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            var hfSlno = (HiddenField)row.FindControl("hfSlno");
            DropDownList ddl_Action = (DropDownList)row.FindControl("ddl_Action");

            string A = ddl_Action.SelectedValue;
            string B = ddl_Action.SelectedItem.Text;
            string c = hfSlno.Value;
            if (ddl_Action.SelectedValue == "2" || ddl_Action.SelectedValue == "3")
            {
                int ApprovaTypeID = 0;
                if (ddl_Action.SelectedValue == "2")
                {
                    ApprovaTypeID = 1;
                }
                else if (ddl_Action.SelectedValue == "3")
                {
                    ApprovaTypeID = 2;
                }
                objUserBEL = new BLL_DropDown();
                objUserBEL.slno = Convert.ToInt32(hfSlno.Value);
                objUserBEL.CROP_ID = "";
                objUserBEL.Crop_Verid = "";
                objUserBEL.LOT_NO = "";
                objUserBEL.UPDATED_BY = "";
                objUserBEL.AMOUNT = 0;
                objUserBEL.SaleTo = "";
                objUserBEL.ApprovaTypeID = ApprovaTypeID;
                objUserBEL.REF_NO = "";
                objUserBEL.TypeId = 3;

                objUserDLL = new DLL_DropDown();
                int Val = objUserDLL.InsertClass1(objUserBEL);
                if (Val == 1)//SUCCESS
                {
                    string pop = "alert('Lot Number Added Successfully !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    FillLots();
                }
                else if (Val == 0)
                {
                    string pop = "alert('Some error occured !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
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
    protected void gvLots_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLots.PageIndex = e.NewPageIndex;
            FillLots();
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
    protected void gvLots_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var rbtn_lot = (CheckBox)e.Row.FindControl("rbtn_lot");
                ////var ddl_Source = (DropDownList)e.Row.FindControl("ddl_Source");
                //
                //var lbl_CropVerNm = (Label)e.Row.FindControl("lbl_CropVerNm");
                //var ddl_BagSize = (DropDownList)e.Row.FindControl("ddl_BagSize");
                //var txt_LotNo = (TextBox)e.Row.FindControl("txt_LotNo");
                //var txt_NoOfBag = (TextBox)e.Row.FindControl("txt_NoOfBag");
                //var txt_Quintal = (TextBox)e.Row.FindControl("txt_Quintal");
                //var btn_Add = (Button)e.Row.FindControl("btn_Add");
                var ddl_Action = (DropDownList)e.Row.FindControl("ddl_Action");
                var hfSlno = (HiddenField)e.Row.FindControl("hfSlno");
                //var txt_testingDate = (TextBox)e.Row.FindControl("txt_testingDate");
                //var lbl_ExpiryDate = (Label)e.Row.FindControl("lbl_ExpiryDate");

                ddl_Action.Items.Clear();
                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = "STOCK";
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillAction(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_Action.DataSource = ds;
                        ddl_Action.DataTextField = "ACTION_NAME";
                        ddl_Action.DataValueField = "ACTION_ID";//hfSlno.ToString();
                        ddl_Action.DataBind();
                    }
                }
                ddl_Action.Items.Insert(0, "--Select--");
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
}
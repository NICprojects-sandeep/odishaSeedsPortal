using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

public partial class Masters_Stock_OBEntry : System.Web.UI.Page
{
    private IDBManager db = new DBManager(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["DataConnString"].ConnectionString);
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();

    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
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
                this.hfDistCode.Value = HttpUtility.HtmlEncode(Session["DistCode"].ToString());
                this.lblDistName.Text = this.lblDistName1.Text = HttpUtility.HtmlEncode(Session["DistName"].ToString());
                this.hfOBDate.Value = HttpUtility.HtmlEncode(Session["OBDate"].ToString());
                this.lblOBDate.Text = HttpUtility.HtmlEncode(hfOBDate.Value);
                this.FillGodown();
                this.utl.SetSessionCookie();
                this.hfSession.Value = HttpUtility.HtmlEncode(this.Session["AuthTokenPage"].ToString());
                FillFinYr();
                FillSeassion();
                return;
            }
        }
    }

    private void FillSeassion()
    {
        try
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
                        ddlSeassion.DataValueField = "SEASSION_NAME";
                        ddlSeassion.DataBind();
                    }
                }
                ddlSeassion.Items.Insert(0, "--Select--");
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
        finally
        {
            this.db.Close();
        }
    }

    private void FillFinYr()
    {
        try
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
                ddlFinYr.Items.Insert(0, "--Select--");
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
        finally
        {
            this.db.Close();
        }
    }
    protected void FillGodown()
    {
        try
        {
            try
            {
                //this.db.Open();
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                string str1 = this.Session["UserType"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!");
                }

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
                        Chk_Godown.DataSource = ds;
                        Chk_Godown.DataTextField = "Godown_Name";
                        Chk_Godown.DataValueField = "Godown_ID";
                        Chk_Godown.DataBind();
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
        finally
        {
            this.db.Close();
        }
    }
    protected void Chk_Godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                objUserBEL = new BLL_DropDown();
                objUserBEL.Godown_ID = Chk_Godown.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.GetGoDownByGoDownId(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.tblMainSection.Visible = false;
                        this.tblSearch.Visible = true;
                        this.lblDateOB.Text = hfOBDate.Value;
                        this.lblGodownName.Text = ds.Tables[0].Rows[0]["Godown_name"].ToString();
                        this.FillCropCatg();
                        this.ddl_CropName.Items.Clear();
                        this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
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
        finally
        {
            this.db.Close();
        }
    }
    protected void FillCropCatg()
    {
        try
        {
            try
            {
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropCategory();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.ddl_CropCatagory.DataSource = ds;
                        this.ddl_CropCatagory.DataValueField = "CATEGORY_CODE";
                        this.ddl_CropCatagory.DataTextField = "CATEGORY_NAME";
                        this.ddl_CropCatagory.DataBind();
                        this.ddl_CropCatagory.Items.Insert(0, "--- SELECT ---");
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
        finally
        {
            this.db.Close();
        }
    }
    protected void ddl_CropCatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                Grd_OBDtls.DataSource = null;
                Grd_OBDtls.DataBind();
                if (ddl_CropCatagory.SelectedIndex > 0)
                {
                    this.FillCrop();
                }
                else
                {
                    this.ddl_CropName.Items.Clear();
                    this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
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
        finally
        {
            this.db.Close();
        }
    }
    protected void FillCrop()
    {
        try
        {
            try
            {
                this.db.Open();
                Regex regex = new Regex("^[0-9A-Za-z]*$");
                string CropCatgID = this.ddl_CropCatagory.SelectedValue;
                if (!regex.IsMatch(CropCatgID))
                {
                    throw new ApplicationException("Invalid Characters!");
                }

                objUserBEL = new BLL_DropDown();
                objUserBEL.CROPCATG_ID = ddl_CropCatagory.SelectedValue;
                objUserDLL = new DLL_DropDown();

                ds = new DataSet();
                ds = objUserDLL.FillCropByCategoryId(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.ddl_CropName.DataSource = ds;
                        this.ddl_CropName.DataValueField = "Crop_Code";
                        this.ddl_CropName.DataTextField = "Crop_Name";
                        this.ddl_CropName.DataBind();
                        this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
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
        finally
        {
            this.db.Close();
        }
    }
    protected void FillCropVariety()
    {
        try
        {
            try
            {
                this.db.Open();
                Regex regex = new Regex("^[0-9A-Za-z]*$");
                string CropID = this.ddl_CropName.SelectedValue;
                if (!regex.IsMatch(CropID))
                {
                    throw new ApplicationException("Invalid Characters!");
                }
                objUserBEL = new BLL_DropDown();
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByCropId(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_OBDtls.DataSource = ds;
                        Grd_OBDtls.DataBind();
                        this.trSubmit.Visible = true;
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
        finally
        {
            this.db.Close();
        }
    }
    protected void ddl_CropName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                Grd_OBDtls.DataSource = null;
                Grd_OBDtls.DataBind();
                if (ddl_CropName.SelectedIndex > 0)
                {
                    this.FillCropVariety();
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
        finally
        {
            this.db.Close();
        }
    }
    protected void rbtn_lot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow i in Grd_OBDtls.Rows)
            {
                RadioButton chk = (RadioButton)i.FindControl("rbtn_lot");
                DropDownList ddl_Source = (DropDownList)i.FindControl("ddl_Source");
                TextBox txt_LotNo = (TextBox)i.FindControl("txt_LotNo");
                DropDownList ddl_BagSize = (DropDownList)i.FindControl("ddl_BagSize");
                TextBox txt_NoOfBag = (TextBox)i.FindControl("txt_NoOfBag");
                TextBox txt_Quintal = (TextBox)i.FindControl("txt_Quintal");
                Button btn_Add = (Button)i.FindControl("btn_Add");

                if (chk.Checked == true)
                {
                    ddl_Source.Enabled = true;
                    txt_LotNo.Enabled = true;
                    txt_NoOfBag.Enabled = true;
                    ddl_BagSize.Enabled = true;
                    btn_Add.Enabled = true;

                    ddl_Source.BackColor = System.Drawing.Color.LemonChiffon;
                    txt_LotNo.BackColor = System.Drawing.Color.LemonChiffon;
                    txt_NoOfBag.BackColor = System.Drawing.Color.LemonChiffon;
                    ddl_BagSize.BackColor = System.Drawing.Color.LemonChiffon;
                    txt_Quintal.BackColor = System.Drawing.Color.LemonChiffon;
                    btn_Add.BackColor = System.Drawing.Color.SeaGreen;
                }
                else
                {
                    ddl_Source.Enabled = false;
                    txt_LotNo.Enabled = false;
                    txt_NoOfBag.Enabled = false;
                    ddl_BagSize.Enabled = false;
                    btn_Add.Enabled = false;

                    ddl_Source.BackColor = System.Drawing.Color.White;
                    txt_LotNo.BackColor = System.Drawing.Color.White;
                    txt_NoOfBag.BackColor = System.Drawing.Color.White;
                    ddl_BagSize.BackColor = System.Drawing.Color.White;
                    txt_Quintal.BackColor = System.Drawing.Color.White;
                    btn_Add.BackColor = System.Drawing.Color.Gray;

                    ddl_Source.SelectedIndex = 0;
                    txt_LotNo.Text = string.Empty;
                    ddl_BagSize.SelectedIndex = 0;
                    txt_NoOfBag.Text = string.Empty;
                    txt_Quintal.Text = string.Empty;
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
    protected void txt_LotNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                string UserType = this.Session["UserType"].ToString();
                GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
                TextBox txt_LotNo = (TextBox)row.FindControl("txt_LotNo");

                BLL_DropDown objUserBEL = new BLL_DropDown();
                objUserBEL.LOT_NO = txt_LotNo.Text.Trim();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                DLL_DropDown objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillLotNoUserType(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string popp = "alert('Lot No already exists!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_LotNo.Text = string.Empty;
                        return;
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
        finally
        {
            this.db.Close();
        }
    }
    protected void ddl_BagSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
                TextBox txt_NoOfBag = (TextBox)row.FindControl("txt_NoOfBag");
                TextBox txt_Quintal = (TextBox)row.FindControl("txt_Quintal");
                DropDownList ddl_BagSize = (DropDownList)row.FindControl("ddl_BagSize");

                if (txt_NoOfBag.Text != string.Empty)
                {
                    if (Convert.ToDouble(txt_NoOfBag.Text) > 0)
                    {
                        if (ddl_BagSize.SelectedIndex > 0)
                        {
                            txt_Quintal.Text = (Convert.ToDouble(txt_NoOfBag.Text) * Convert.ToDouble(ddl_BagSize.SelectedValue) / 100).ToString();
                        }
                        else
                        {
                            string popp = "alert('Please Select Bag Size!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_Quintal.Text = string.Empty;
                            ddl_BagSize.Focus();
                        }
                    }
                }
                else
                {
                    txt_NoOfBag.Text = string.Empty;
                    txt_NoOfBag.Focus();
                    txt_Quintal.Text = string.Empty;
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
        finally
        {
            this.db.Close();
        }
    }
    protected void txt_NoOfBag_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt_NoOfBag = (TextBox)row.FindControl("txt_NoOfBag");
            TextBox txt_Quintal = (TextBox)row.FindControl("txt_Quintal");
            DropDownList ddl_BagSize = (DropDownList)row.FindControl("ddl_BagSize");

            if (txt_NoOfBag.Text != string.Empty)
            {
                if (Convert.ToDouble(txt_NoOfBag.Text) > 0)
                {
                    if (ddl_BagSize.SelectedIndex > 0)
                    {
                        txt_Quintal.Text = (Convert.ToDouble(txt_NoOfBag.Text) * Convert.ToDouble(ddl_BagSize.SelectedValue) / 100).ToString();
                    }
                    else
                    {
                        string popp = "alert('Please Select Bag Size!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_Quintal.Text = string.Empty;
                        ddl_BagSize.Focus();
                    }
                }
            }
            else
            {
                string popp = "alert('Please Enter Number of Bags!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                txt_NoOfBag.Text = string.Empty;
                txt_Quintal.Text = string.Empty;
                txt_NoOfBag.Focus();
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
    private void SetSelectedRecord()
    {
        try
        {
            for (int i = 0; i < Grd_OBDtls.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)Grd_OBDtls.Rows[i].Cells[0]
                                                .FindControl("rbtn_lot");
                if (rb != null)
                {
                    HiddenField hfCropVerid = (HiddenField)Grd_OBDtls.Rows[i]
                                        .Cells[0].FindControl("hfCropVerid");
                    if (hfCropVerid != null && ViewState["SelectedContact"] != null)
                    {
                        if (hfCropVerid.Value.Equals(ViewState["SelectedContact"].ToString()))
                        {
                            rb.Checked = true;
                            break;
                        }
                    }
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
    private void GetSelectedRecord()
    {
        try
        {
            for (int i = 0; i < Grd_OBDtls.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)Grd_OBDtls.Rows[i]
                                .Cells[0].FindControl("rbtn_lot");
                if (rb != null)
                {
                    if (rb.Checked)
                    {
                        HiddenField hf = (HiddenField)Grd_OBDtls.Rows[i]
                                        .Cells[0].FindControl("hfCropVerid");
                        if (hf != null)
                        {
                            ViewState["SelectedContact"] = hf.Value;
                        }

                        break;
                    }
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
    protected void Grd_OBDtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grd_OBDtls.PageIndex = e.NewPageIndex;
            this.FillCropVariety();
            this.SetSelectedRecord();
        }
        catch (ApplicationException applicationException1)
        {
            ApplicationException applicationException = applicationException1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", string.Concat("alert('", applicationException.Message, "');"), true);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(applicationException1));
        }
    }
    protected void Grd_OBDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rbtn_lot = (CheckBox)e.Row.FindControl("rbtn_lot");
                var ddl_Source = (DropDownList)e.Row.FindControl("ddl_Source");

                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillSourceByUserType(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_Source.DataSource = ds;
                        ddl_Source.DataValueField = "Receive_Unitcd";
                        ddl_Source.DataTextField = "Receive_Unitname";
                        ddl_Source.DataBind();
                        ddl_Source.Items.Insert(0, "--- SELECT ---");
                    }
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
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (this.hfSession.Value != this.Session["AuthTokenPage"].ToString())
                {
                    ExceptionHandler.WriteException("Session Value in Cookie And Hidden Field Does not Match");
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Error Executing the Request. Please Contact Site Administrator for Details!');", true);
                    return;
                }
                Button btn = (Button)sender;
                CheckBox rbtn_lot = (CheckBox)btn.Parent.FindControl("rbtn_lot");
                HiddenField hfCropVerid = (HiddenField)btn.Parent.FindControl("hfCropVerid");
                DropDownList ddl_Source = (DropDownList)btn.Parent.FindControl("ddl_Source");
                TextBox txt_LotNo = (TextBox)btn.Parent.FindControl("txt_LotNo");
                DropDownList ddl_BagSize = (DropDownList)btn.Parent.FindControl("ddl_BagSize");
                TextBox txt_NoOfBag = (TextBox)btn.Parent.FindControl("txt_NoOfBag");
                TextBox txt_Quintal = (TextBox)btn.Parent.FindControl("txt_Quintal");


                if (ddlFinYr.SelectedIndex == 0)
                {
                    string pop = "alert('Please select Financial Year !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddlFinYr);
                    return;
                }
                else if (ddlSeassion.SelectedIndex == 0)
                {
                    string pop = "alert('Please select Seassion !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSeassion);
                    return;
                }
                else if (ddl_CropCatagory.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Crop Catagory !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_CropCatagory);
                    return;
                }
                else if (ddl_CropName.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Crop Name !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_CropName);
                    return;
                }
                else if (rbtn_class.SelectedIndex < 0)
                {
                    string pop = "alert('Please select the Class !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(rbtn_class);
                    return;
                }
                else if (ddl_Source.SelectedIndex == 0)
                {
                    string pop = "alert('Please select Source !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_Source);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txt_LotNo.Text))
                {
                    string pop = "alert('Please enter the Lot Number !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_LotNo);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txt_NoOfBag.Text))
                {
                    string pop = "alert('Please enter no of bags receive !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_NoOfBag);
                    return;
                }
                else if (ddl_BagSize.SelectedIndex == 0)
                {
                    string pop = "alert('Please Bag Size (IN Kg.) !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_BagSize);
                    return;
                }
                else
                {
                    //try
                    //{
                    try
                    {
                        decimal total = Convert.ToInt32(ddl_BagSize.SelectedValue) * Convert.ToInt32(txt_NoOfBag.Text);
                        decimal Quantity = Convert.ToDecimal(total) / (100);

                        if (Convert.ToDecimal(txt_Quintal.Text) == Quantity)
                        {
                            objUserBEL = new BLL_DropDown();
                            objUserBEL.LOT_NO = txt_LotNo.Text.Trim();
                            objUserBEL.UserType = this.Session["UserType"].ToString();
                            objUserDLL = new DLL_DropDown();
                            string str = "";
                            int Cnt = objUserDLL.GetLotNoFromOB(objUserBEL);
                            if (Cnt == 0)
                            {
                                objUserBEL.DistCode = hfDistCode.Value;
                                objUserBEL.Godown_ID = Chk_Godown.SelectedValue;
                                objUserBEL.CROPCATG_ID = ddl_CropCatagory.SelectedValue;
                                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                                objUserBEL.Crop_Verid = hfCropVerid.Value;
                                objUserBEL.CropClass = rbtn_class.SelectedValue;
                                objUserBEL.Receive_Unitcd = ddl_Source.SelectedValue;
                                objUserBEL.LOT_NO = txt_LotNo.Text;
                                objUserBEL.Bag_Size_In_kg = ddl_BagSize.SelectedValue;
                                objUserBEL.Recv_No_Of_Bags = txt_NoOfBag.Text;
                                objUserBEL.OB_date = Session["OBDate"].ToString();
                                objUserBEL.OB_Quantity = txt_Quintal.Text;
                                objUserBEL.SESSION = ddlSeassion.SelectedValue;
                                objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
                                objUserBEL.UserType = Session["UserType"].ToString();
                                objUserBEL.UserID = Session["UserID"].ToString();
                                objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

                                objUserDLL = new DLL_DropDown();
                                str = objUserDLL.InsertOB(objUserBEL);
                                if (str == "OB Detail Added Successfully")
                                {
                                    ActivityLog activityLog = new ActivityLog()
                                    {
                                        UserID = this.Session["UserID"].ToString(),
                                        UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                        ActivityType = "Action",
                                        Activity = "OB Entry",
                                        PageURL = HttpContext.Current.Request.Url.ToString(),
                                        Remark = "OB Entry Successfully !",
                                        OS = Request.Browser.Platform.ToString(),
                                        BWSER = Request.Browser.Type.ToString()
                                    };
                                    ActivityLog.InsertActivityLog_Stock(activityLog);

                                    btn.Text = "Add More";

                                    txt_LotNo.Text = string.Empty;
                                    txt_NoOfBag.Text = string.Empty;
                                    txt_Quintal.Text = string.Empty;
                                    ddl_BagSize.SelectedIndex = 0;
                                    txt_LotNo.BackColor = System.Drawing.Color.LemonChiffon;
                                    txt_NoOfBag.BackColor = System.Drawing.Color.LemonChiffon;
                                    txt_Quintal.BackColor = System.Drawing.Color.LemonChiffon;
                                    btn.BackColor = System.Drawing.Color.SeaGreen;

                                    string popp = "alert('Data Saved Sucessfully !!!');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);

                                    string popp1 = "alert('You Can Add Same Variety From Other Source By Clicking Add More !!!');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp1", popp1, true);

                                    objUserBEL = new BLL_DropDown();
                                    objUserBEL.UserType = this.Session["UserType"].ToString();
                                    objUserDLL = new DLL_DropDown();
                                    ds = new DataSet();
                                    ds = objUserDLL.FillSourceByUserType(objUserBEL);
                                    if (ds != null)
                                    {
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            ddl_Source.DataSource = ds;
                                            ddl_Source.DataValueField = "Receive_Unitcd";
                                            ddl_Source.DataTextField = "Receive_Unitname";
                                            ddl_Source.DataBind();
                                            ddl_Source.Items.Insert(0, "--- SELECT ---");
                                        }
                                    }
                                }
                                else
                                {
                                    string pop = "alert('Something went Wrong!!!');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                                    return;
                                }
                            }
                            else
                            {
                                string popp = "alert('Lot Number Can Not be Duplicated!!!');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                                return;
                            }
                        }
                        else
                        {
                            string popp = "alert('Please Check the Quintal value!!!');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        }

                    }
                    catch (Exception exception)
                    {
                        ExceptionHandler.WriteEx(exception);
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
        finally
        {
            this.db.Close();
        }
    }
    protected void btn_ExistCrop_Click(object sender, EventArgs e)
    {
        try
        {
            this.ddl_CropCatagory.SelectedIndex = 0;
            this.ddl_CropName.Items.Clear();
            this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
            rbtn_class.SelectedValue = "Certified";
            Grd_OBDtls.DataSource = null;
            Grd_OBDtls.DataBind();
            trSubmit.Visible = false;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void btn_ExistGodown_Click(object sender, EventArgs e)
    {
        try
        {
            this.ddl_CropCatagory.SelectedIndex = 0;
            this.ddl_CropName.Items.Clear();
            this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
            rbtn_class.SelectedValue = "Certified";
            Grd_OBDtls.DataSource = null;
            Grd_OBDtls.DataBind();
            Chk_Godown.SelectedIndex = -1;
            trSubmit.Visible = false;
            tblMainSection.Visible = true;
            tblSearch.Visible = false;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }
    }
    protected void ddlFinYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSeassion();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
            objUserBEL.CROPVARIETY_NAME = txtVariety.Text;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropVarietyByCropIdVarietyNameLike(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_OBDtls.DataSource = ds;
                    Grd_OBDtls.DataBind();
                    this.trSubmit.Visible = true;
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
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Masters_Stock_ReceiveEntry : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    string _connstrOssopca = ConfigurationManager.ConnectionStrings["OSSOPCAConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("../Maintainance.aspx");

        objUserDLL = new DLL_DropDown();

        int Cnt = 0;
        
        //objUserDLL.CheckDate(out Cnt);
        if (Cnt >= 0)
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
                    objUserBEL = new BLL_DropDown();
                    objUserBEL.DistCode = Session["DistCode"].ToString();
                    objUserBEL.UserType = Session["UserType"].ToString();
                    objUserDLL = new DLL_DropDown();
                    Session["SlNo"] = 1;
                    grd_lottrans.Visible = false;
                    this.hfDistCode.Value = Session["DistCode"].ToString();
                    this.lblDistName.Text = this.lblDistName1.Text = Session["DistName"].ToString();
                    this.FillGodown();
                    this.trReceivedType.Visible = false;
                    this.trWayBill.Visible = false;
                    this.ddl_ChallanNo.Visible = false;
                    lblGrowerVareity.Visible = false;
                    this.CreateNewDataTable();
                    this.utl.SetSessionCookie();
                    this.hfSession.Value = this.Session["AuthTokenPage"].ToString();
                    FillFinYr();
                    FillSeassion(ddlFinYr.SelectedValue.ToString());
                    trScheme.Visible = false;
                    return;
                }
            }
        }
        else
        {
            divview.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Online seed transactions for Rabi 2020-21 has been closed.'); window.location='" + Request.ApplicationPath + "/Masters/Home.aspx';", true);
        }
    }
    private void FillSeassion(string p)
    {
        try
        {
            ddlSeassion.Items.Clear();
            objUserBEL = new BLL_DropDown();
            objUserBEL.FIN_YR = p;
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
            //ddlSeassion.Items.FindByText(ds.Tables[0].Rows[0]["SEASSION_NAME"].ToString()).Selected = true;
            //ddlSeassion.Items.Insert(0, "--Select--");
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
                    ddlFinYr.DataSource = ds.Tables[0];
                    ddlFinYr.DataTextField = "FIN_YR";
                    ddlFinYr.DataValueField = "FIN_YR";
                    ddlFinYr.DataBind();
                }
            }
            // = ds.Tables[0].Rows[0]["FIN_YR"].ToString();
            //ddlFinYr.Items
            //ddlFinYr.Items.Insert(0, "--SELECT--");
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
    protected void FillGodown()
    {
        try
        {
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
    protected void Chk_Godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objUserBEL = new BLL_DropDown();
            objUserBEL.Godown_ID = Chk_Godown.SelectedValue;
            Session["Godown_Id"] = objUserBEL.Godown_ID.ToString();
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.GetGoDownByGoDownId(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.tblMainSection.Visible = false;
                    this.tblSearch.Visible = true;
                    this.lblGodownName.Text = ds.Tables[0].Rows[0]["Godown_name"].ToString();
                    FillAgency();

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
    protected void FillAgency()
    {
        try
        {
            Regex regex = new Regex("^\\d{2}$");
            string str = this.Session["DistCode"].ToString();
            string str1 = this.Session["UserType"].ToString();
            if (!regex.IsMatch(str))
            {
                throw new ApplicationException("Invalid Characters!");
            }

            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillAgencyByOAIC();


            if (str1 == "OAIC")
            {
                ds = objUserDLL.FillAgencyByOAIC();
            }
            else if (str1 == "OSSC")
            {
                ds = objUserDLL.FillAgencyByOSSC();
            }
            else
            {
                throw new ApplicationException("Invalid Characters !");
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rbtn_RecvedFrom.DataSource = ds;
                    rbtn_RecvedFrom.DataTextField = "AgenciesName";
                    rbtn_RecvedFrom.DataValueField = "AgenciesID";
                    rbtn_RecvedFrom.DataBind();
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
    protected void rbtn_RecvedFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddl_ChallanNo.Visible = false;
            txtChallanNo.Visible = true;
            txtChallanNo.Attributes.Remove("readonly");
            trWayBill.Visible = false;
            trReceivedType.Visible = true;
            trChallan.Visible = true;
            DataSet unit = new DataSet();
            ddl_RecvFrom.Items.Clear();

            if (rbtn_RecvedFrom.SelectedValue == "01") //Seed Grower
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = true;
                lblGrowerVareity.Text = string.Empty;
                lblRecvText.Text = "Seed Grower Name";
                lblChallan.Text = "P.R No.";
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!");
                }
                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.DistCode = this.Session["DistCode"].ToString();
                objUserBEL.FIN_YR = "2017-18";
                //objUserBEL.SEASON = ddlSeassion.SelectedValue;
                //objUserBEL.CropClass = rbtn_class.SelectedValue;
                objUserDLL = new DLL_DropDown();

                ds = new DataSet();
                ds = objUserDLL.FillMOUByUserTypeDistCode1(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_RecvFrom.DataSource = ds;
                        ddl_RecvFrom.DataValueField = "REF_NO";
                        ddl_RecvFrom.DataTextField = "NAME";
                        ddl_RecvFrom.DataBind();
                    }
                }
                ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "02") //OutsideAgencies
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                lblRecvText.Text = "Outside Agencies";
                lblChallan.Text = "Challan No.";
                trWayBill.Visible = true;
                lblWayBill.Text = "Way Bill Number";
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!");
                }
                string val1 = "0101";
                string val2 = "0102";
                //if (Session["UserType"].ToString() == "OAIC" )
                //{
                //    val1 = "0147";
                //    val2 = "0148";
                //}
                //else if (Session["UserType"].ToString() == "OSSC")
                //{
                //    val1 = "0219";
                //    val2 = "0220";
                //}
                objUserBEL = new BLL_DropDown();
                objUserBEL.AgenciesID = rbtn_RecvedFrom.SelectedValue;
                objUserBEL.Value1 = val1;
                objUserBEL.Value2 = val2;
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillSourceByAgencyIdUserTypeValues(objUserBEL);

                ddl_RecvFrom.DataSource = ds;
                ddl_RecvFrom.DataTextField = "Receive_Unitname";
                ddl_RecvFrom.DataValueField = "Receive_Unitcd";
                ddl_RecvFrom.DataBind();
                ddl_RecvFrom.Items.Insert(0, "---SELECT---");
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "03") //Godown
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                lblRecvText.Text = "Transfered From Godown";
                lblChallan.Text = "Challan No.";
                ddl_ChallanNo.Visible = true;
                txtChallanNo.Visible = false;
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!");
                }

                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.SaleTo = Chk_Godown.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillGoDownByUserTypeSaleTo(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_RecvFrom.DataSource = ds;
                        ddl_RecvFrom.DataTextField = "GODOWN_NAME";
                        ddl_RecvFrom.DataValueField = "GODOWN_ID";
                        ddl_RecvFrom.DataBind();
                        ddl_RecvFrom.Items.Insert(0, "---SELECT---");
                    }

                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "04") //OSSC OR OAIC
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                trChallan.Visible = true;
                trReceivedType.Visible = false;
                lblChallan.Text = "Challan No.";
                if (Session["UserType"].ToString() == "OAIC")
                {
                    ddl_RecvFrom.Items.Clear();
                    ddl_RecvFrom.Items.Insert(0, "0102");
                }
                else if (Session["UserType"].ToString() == "OSSC")
                {
                    ddl_RecvFrom.Items.Clear();
                    ddl_RecvFrom.Items.Insert(0, "0101");
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "05") //Departmental Farm 
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                lblRecvText.Text = "Agency Name";
                lblChallan.Text = "Challan No.";
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!");
                }
                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.DistCode = this.Session["DistCode"].ToString();
                objUserBEL.FIN_YR = "2017-18";
                //objUserBEL.CropClass = rbtn_class.SelectedValue;
                objUserDLL = new DLL_DropDown();

                ds = new DataSet();
                ds = objUserDLL.FillGovtFarmByDistCode(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_RecvFrom.DataSource = ds;
                        ddl_RecvFrom.DataValueField = "Name_of_agency";
                        ddl_RecvFrom.DataTextField = "AgName";
                        ddl_RecvFrom.DataBind();
                    }
                }
                ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");

                //trWayBill.Visible = true;
                //lblWayBill.Text = "Name Of Farm";
                //Regex regex = new Regex("^\\d{2}$");
                //string str = this.Session["DistCode"].ToString();
                //objUserBEL.DistCode = this.Session["DistCode"].ToString();
                //objUserBEL.FIN_YR = this.ddlFinYr.SelectedValue;
                //if (!regex.IsMatch(str))
                //{
                //    throw new ApplicationException("Invalid Characters!");
                //}
                //objUserBEL = new BLL_DropDown();
                //objUserBEL.AgenciesID = rbtn_RecvedFrom.SelectedValue;
                //objUserBEL.UserType = Session["UserType"].ToString();
                //objUserDLL = new DLL_DropDown();
                //ds = new DataSet();
                //ds = objUserDLL.FillSourceByAgencyIdUserType(objUserBEL);
                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        ddl_RecvFrom.DataSource = ds;
                //        ddl_RecvFrom.DataTextField = "Receive_Unitname";
                //        ddl_RecvFrom.DataValueField = "Receive_Unitcd";
                //        ddl_RecvFrom.DataBind();
                //        ddl_RecvFrom.Items.Insert(0, "---SELECT---");
                //    }
                //}     
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "06") //OUAT Farm
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                lblRecvText.Text = "Agency Name";
                lblChallan.Text = "Challan No.";
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!");
                }
                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.DistCode = this.Session["DistCode"].ToString();
                objUserBEL.FIN_YR = "2017-18";
                //objUserBEL.CropClass = rbtn_class.SelectedValue;
                objUserDLL = new DLL_DropDown();

                ds = new DataSet();
                ds = objUserDLL.FillByDistCodeOUAT(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_RecvFrom.DataSource = ds;
                        ddl_RecvFrom.DataValueField = "Name_of_agency";
                        ddl_RecvFrom.DataTextField = "AgName";
                        ddl_RecvFrom.DataBind();
                    }
                }
                ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");

                //trScheme.Visible = false;
                //lblRecvText.Text = "Farm Name";
                ////trWayBill.Visible = true;
                ////lblWayBill.Text = "Name Of Farm";
                //Regex regex = new Regex("^\\d{2}$");
                //string str = this.Session["DistCode"].ToString();
                //if (!regex.IsMatch(str))
                //{
                //    throw new ApplicationException("Invalid Characters!");
                //}
                //objUserBEL = new BLL_DropDown();
                //objUserBEL.AgenciesID = rbtn_RecvedFrom.SelectedValue;
                //objUserBEL.UserType = Session["UserType"].ToString();
                //objUserDLL = new DLL_DropDown();
                //ds = new DataSet();
                //ds = objUserDLL.FillSourceByAgencyIdUserType(objUserBEL);
                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        ddl_RecvFrom.DataSource = ds;
                //        ddl_RecvFrom.DataTextField = "Receive_Unitname";
                //        ddl_RecvFrom.DataValueField = "Receive_Unitcd";
                //        ddl_RecvFrom.DataBind();
                //        ddl_RecvFrom.Items.Insert(0, "---SELECT---");
                //    }
                //}
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "07") //Others
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                lblRecvText.Text = "Select Farm Name";
                lblChallan.Text = "Challan No.";
                trWayBill.Visible = true;
                lblWayBill.Text = "Name Of Farm";
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!8");
                }

                objUserBEL = new BLL_DropDown();
                objUserBEL.AgenciesID = rbtn_RecvedFrom.SelectedValue;
                objUserBEL.UserType = Session["UserType"].ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillSourceByAgencyIdUserType(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_RecvFrom.DataSource = ds;
                        ddl_RecvFrom.DataTextField = "Receive_Unitname";
                        ddl_RecvFrom.DataValueField = "Receive_Unitcd";
                        ddl_RecvFrom.DataBind();
                        ddl_RecvFrom.Items.Insert(0, "---SELECT---");
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "08") //Own
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                trChallan.Visible = false;
                trReceivedType.Visible = false;
                lblChallan.Text = "Challan No.";
                if (Session["UserType"].ToString() == "OAIC")
                {
                    ddl_RecvFrom.Items.Clear();
                    ddl_RecvFrom.Items.Insert(0, "0101");
                }
                else if (Session["UserType"].ToString() == "OSSC")
                {
                    ddl_RecvFrom.Items.Clear();
                    ddl_RecvFrom.Items.Insert(0, "0102");
                }
                #endregion
            }
            if (rbtn_RecvedFrom.SelectedValue == "09") //MOU Agency
            {
                #region
                trScheme.Visible = false;
                lblGrowerVareity.Visible = false;
                lblRecvText.Text = "MOU Agency Name";
                lblChallan.Text = "Challan No.";
                Regex regex = new Regex("^\\d{2}$");
                string str = this.Session["DistCode"].ToString();
                if (!regex.IsMatch(str))
                {
                    throw new ApplicationException("Invalid Characters!");
                }
                objUserBEL = new BLL_DropDown();
                //objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.DistCode = this.Session["DistCode"].ToString();
                objUserBEL.FIN_YR = "2017-18";
                //objUserBEL.SEASON = ddlSeassion.SelectedValue;
                //objUserBEL.CropClass = rbtn_class.SelectedValue;
                objUserDLL = new DLL_DropDown();

                ds = new DataSet();
                ds = objUserDLL.FillByUserTypeMOUAgency(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_RecvFrom.DataSource = ds;
                        ddl_RecvFrom.DataValueField = "Name_of_agency";
                        ddl_RecvFrom.DataTextField = "AgName";
                        ddl_RecvFrom.DataBind();
                    }
                }
                ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");
                #endregion
            }
            if (rbtn_RecvedFrom.SelectedValue == "10") //Scheme
            {
                #region
                trScheme.Visible = true;
                lblGrowerVareity.Visible = false;
                lblRecvText.Text = "Agency Name";
                //ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");
                #endregion
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
    //static DataTable ConvertListToDataTable(List<sgWithLot[]> list)
    //{
    //    // New table.
    //    DataTable table = new DataTable();
    //    // Get max columns.
    //    int columns = 0;
    //    foreach (var array in list)
    //    {
    //        if (array.Length > columns)
    //        {
    //            columns = array.Length;
    //        }
    //    }
    //    // Add columns.
    //    for (int i = 0; i < columns; i++)
    //    {
    //        table.Columns.Add();
    //    }
    //    // Add rows.
    //    foreach (var array in list)
    //    {
    //        table.Rows.Add(array);
    //    }
    //    return table;
    //}
    protected void txt_Date_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DateTime.Now < Convert.ToDateTime(txt_Date.Text))
            {
                string pop = "alert('Received date Cannot be greater than todays date !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txt_Date);
                txt_Date.Text = "";
                return;
            }
            else if (Convert.ToDateTime(txt_Date.Text) <= Convert.ToDateTime(Session["OBDate"].ToString()))
            {
                string pop = "alert('Received date Cannot be less than or equal to OB Date !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txt_Date);
                txt_Date.Text = "";
                return;
            }
            else if (Convert.ToDateTime(txt_Date.Text) <= Convert.ToDateTime("2015-11-01"))
            {
                string pop = "alert('Received date Cannot be less than 01-Nov-2015. !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txt_Date);
                txt_Date.Text = "";
                return;
            }
            else if (rbtn_RecvedFrom.SelectedValue == "03")//Godown
            {
                if (rbtn_RecvedFrom.SelectedIndex == -1)
                {
                    string pop = "alert('Please select the Receive Source !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(rbtn_RecvedFrom);
                    txt_Date.Text = "";
                    return;
                }
                else if (ddl_RecvFrom.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Delivered Godown !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                    txt_Date.Text = "";
                    return;
                }
                else if (txt_Date.Text == "")
                {
                    string pop = "alert('Please enter the Date !!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_Date);
                    txt_Date.Text = "";
                    return;
                }
                else
                {
                    GetChallanNo();
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
    private void GetChallanNo()
    {
        try
        {
            ddl_ChallanNo.Items.Clear();

            if (rbtn_RecvedFrom.SelectedValue == "01" || rbtn_RecvedFrom.SelectedValue == "03") //Seed Grower
            {
                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.Sale_Date = txt_Date.Text;
                objUserBEL.Godown_ID = ddl_RecvFrom.SelectedValue;
                objUserBEL.SALE_TO = Chk_Godown.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.GetRcvCashMemoNo(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_ChallanNo.DataSource = ds;
                        ddl_ChallanNo.DataTextField = "CASH_MEMO_NO";
                        ddl_ChallanNo.DataValueField = "CASH_MEMO_NO";
                        ddl_ChallanNo.DataBind();
                        ddl_ChallanNo.Items.Insert(0, "---SELECT---");
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
    protected void txtChallanNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Regex regex = new Regex("^\\d{2}$");
            string str = this.Session["DistCode"].ToString();
            string str1 = this.Session["UserType"].ToString();
            if (!regex.IsMatch(str))
            {
                throw new ApplicationException("Invalid Characters!9");
            }

            objUserBEL = new BLL_DropDown();
            objUserBEL.Challan_No = txtChallanNo.Text;
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.GetRcvChallan_No(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbl_challan.Visible = true;
                    lbl_challan.ForeColor = System.Drawing.Color.Red;
                    lbl_challan.Text = "Challan Number Already Exist";
                    txtChallanNo.Text = string.Empty;
                    txtChallanNo.Focus();
                    return;
                }
                else
                {
                    lbl_challan.Visible = false;
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
    protected void btn_Proceed_Click(object sender, EventArgs e)
    {
        try
        {
            #region
            this.trSubmit.Visible = false;
            //tblCropInf.Visible = true;
            btn_Submit.Text = "Submit";
            if (ddlFinYr.SelectedIndex == -1)
            {
                string pop = "alert('Please Select The Financial Year !!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(ddlFinYr);
                return;
            }
            else if (ddlSeassion.SelectedIndex == -1)
            {
                string pop = "alert('Please Select Seassion !!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(ddlSeassion);
                return;
            }
            else if (rbtn_RecvedFrom.SelectedIndex == -1)
            {
                string pop = "alert('Please select the Received From!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(rbtn_RecvedFrom);
                return;
            }
            else if (txt_Date.Text == "")
            {
                string pop = "alert('Please enter the Date!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txt_Date);
                return;
            }

            else
            {
                if (rbtn_RecvedFrom.SelectedValue == "01")
                {
                    if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the MOU Name !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    ChallanValid();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "02")
                {

                    if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the Outside Agencies !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    else if (txtWayBillNo.Text == "")
                    {
                        string pop = "alert('Please enter the Waybill Number!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(txtWayBillNo);
                        return;
                    }
                    ChallanValid();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "03") //GODOWN
                {
                    if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the godown!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    else if (txt_Date.Text == "")
                    {
                        string pop = "alert('Please enter the Date!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(txt_Date);
                        return;
                    }
                    ChallanValid();
                    grd_lottrans.Visible = true;
                    Grd_RecvDtls.Visible = false;
                    objUserBEL = new BLL_DropDown();
                    objUserBEL.UserType = Session["UserType"].ToString();
                    objUserBEL.Sale_Date = txt_Date.Text;
                    objUserBEL.Godown_ID = ddl_RecvFrom.SelectedValue;
                    objUserBEL.CashMemo_No = ddl_ChallanNo.SelectedValue;
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.FillSourceByUserTypeCashMemoNo(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grd_lottrans.DataSource = ds;
                            grd_lottrans.DataBind();
                            tblCropInf.Visible = false;
                            this.trSubmit.Visible = true;
                            btn_Submit.Text = "Confirm";
                        }
                    }
                }
                else if (rbtn_RecvedFrom.SelectedValue == "04")
                {
                    DisableControls();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "05")
                {
                    if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the Agency Name !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    ChallanValid();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "06")
                {
                    if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please Select The Agency Name !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    //else if (txtWayBillNo.Text == "")
                    //{
                    //    string pop = "alert('Please enter the Name Of Farm !!!')";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    //    ScriptManager.GetCurrent(Page).SetFocus(txtWayBillNo);
                    //    return;
                    //}
                    ChallanValid();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "07")
                {
                    if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the Farm Name !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    else if (txtWayBillNo.Text == "")
                    {
                        string pop = "alert('Please enter the Farm name!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(txtWayBillNo);
                        return;
                    }
                    ChallanValid();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "08")
                {
                    if (txt_Date.Text == "")
                    {
                        string pop = "alert('Please enter the Date!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(txt_Date);
                        return;
                    }
                    DisableControls();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "09")
                {

                    if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the MOU Agency Name !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    ChallanValid();
                }
                else if (rbtn_RecvedFrom.SelectedValue == "10")
                {

                    if (ddl_Scheme.SelectedIndex == 0)
                    {
                        string pop = "alert('Please Select The scheme Name !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_Scheme);
                        return;
                    }
                    else if (ddl_RecvFrom.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the Agency Name !!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_RecvFrom);
                        return;
                    }
                    ChallanValid();
                }
            }
            #endregion
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
    public void ChallanValid() //Code For Check Challan No...
    {
        try
        {
            if (rbtn_RecvedFrom.SelectedValue == "03")
            {
                if (ddl_ChallanNo.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Challan No. !!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_ChallanNo);
                    return;
                }
                else
                {
                    //insert
                    //insert_RecvData();
                    DisableControls();
                }
            }
            if (rbtn_RecvedFrom.SelectedValue == "02" || rbtn_RecvedFrom.SelectedValue == "05" || rbtn_RecvedFrom.SelectedValue == "06" || rbtn_RecvedFrom.SelectedValue == "07" || rbtn_RecvedFrom.SelectedValue == "09" || rbtn_RecvedFrom.SelectedValue == "10")
            {
                if (txtChallanNo.Text == "")
                {
                    string pop = "alert('Please enter the Challan No. !!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txtChallanNo);
                    return;
                }
                else
                {
                    //insert
                    //insert_RecvData();
                    DisableControls();
                }
            }
            if (rbtn_RecvedFrom.SelectedValue == "01")
            {
                if (txtChallanNo.Text == "")
                {
                    string pop = "alert('Please Enter The P.R No. !!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txtChallanNo);
                    return;
                }
                else
                {
                    //insert
                    //insert_RecvData();
                    DisableControls();
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
    public void DisableControls()
    {
        ddlFinYr.Enabled = false;
        ddlSeassion.Enabled = false;
        rbtn_RecvedFrom.Enabled = false;
        ddl_RecvFrom.Enabled = false;
        txtWayBillNo.Enabled = false;
        txt_Date.Enabled = false;
        txtChallanNo.Enabled = false;
        ddl_ChallanNo.Enabled = false;
        tblCropInf.Visible = true;
        FillCropCatg();
        this.ddl_CropName.Items.Clear();
        this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
        tblCropInf.Visible = true;
    }
    public void EnableControls()
    {
        rbtn_RecvedFrom.Enabled = true;
        ddl_RecvFrom.Enabled = true;
        txtWayBillNo.Enabled = true;
        txt_Date.Enabled = true;
        txtChallanNo.Enabled = true;
        ddl_ChallanNo.Enabled = true;
        tblCropInf.Visible = false;
        FillCropCatg();
    }
    protected void FillCropCatg()
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
                    this.ddl_CropCatagory.DataValueField = "Category_Code";
                    this.ddl_CropCatagory.DataTextField = "Category_Name";
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
    protected void ddl_CropCatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Grd_RecvDtls.DataSource = null;
            Grd_RecvDtls.DataBind();
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
    protected void FillCrop()
    {
        try
        {
            Regex regex = new Regex("^\\d{2}$");
            string CropCatgID = this.ddl_CropCatagory.SelectedValue;
            if (!regex.IsMatch(CropCatgID))
            {
                throw new ApplicationException("Invalid Characters!10");
            }

            objUserBEL = new BLL_DropDown();
            objUserBEL.CROPCATG_ID = ddl_CropCatagory.SelectedValue;
            objUserDLL = new DLL_DropDown();
            ds = new DataSet();
            ds = objUserDLL.FillCropByCategoryId(objUserBEL);
            ddl_CropName.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grd_RecvDtls.DataSource = null;
                    Grd_RecvDtls.DataBind();

                    this.ddl_CropName.DataSource = ds;
                    this.ddl_CropName.DataValueField = "Crop_Code";
                    this.ddl_CropName.DataTextField = "Crop_Name";
                    this.ddl_CropName.DataBind();
                }

            }
            this.ddl_CropName.Items.Insert(0, "--- SELECT ---");

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
    protected void FillCropVariety()
    {
        try
        {
            string CropClass = string.Empty;

            if (rbtn_class.SelectedValue == "Foundation")
            {
                CropClass = "C01";
            }
            else if (rbtn_class.SelectedValue == "TL")
            {
                CropClass = "C02";
            }
            if (rbtn_class.SelectedValue == "Certified")
            {
                CropClass = "C03";
            }

            Grd_RecvDtls.DataSource = null;
            Grd_RecvDtls.DataBind();
            Regex regex = new Regex("^[0-9A-Za-z]*$");
            string CropID = this.ddl_CropName.SelectedValue;
            if (!regex.IsMatch(CropID))
            {
                throw new ApplicationException("Invalid Characters!11");
            }

            if (rbtn_RecvedFrom.SelectedValue == "01") // MOU  // Seed Grower 
            {
                #region
                objUserBEL = new BLL_DropDown();
                objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.DistCode = this.Session["DistCode"].ToString();
                //objUserBEL.FIN_YR = "2018-19";
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                //objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                // objUserBEL.CropClass = rbtn_class.SelectedValue;
                objUserBEL.CROP_ID = CropID.ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByCropIdReceive1(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "02") // Outside Agencies
            {
                #region
                objUserBEL = new BLL_DropDown();
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByOutsideAgencies(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.Columns[5].HeaderText = "Testing Date";
                        Grd_RecvDtls.Columns[6].HeaderText = "Expiry Date";
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;

                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "04") // Self // OSSC // OAIC
            {
                #region
                objUserBEL = new BLL_DropDown();
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyBySelf(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "05") //Department
            {
                #region
                objUserBEL = new BLL_DropDown();
                objUserBEL.DistCode = Session["DistCode"].ToString();
                //objUserBEL.FIN_YR = "2017-18";
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByGovtFarm(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "06") //OUAT
            {
                #region
                objUserBEL = new BLL_DropDown();
                objUserBEL.DistCode = Session["DistCode"].ToString();
                //objUserBEL.FIN_YR = "2017-18";
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByOUAT(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "09") //MOU Agency
            {
                #region
                objUserBEL = new BLL_DropDown();
                //objUserBEL.UserType = this.Session["UserType"].ToString();
                objUserBEL.DistCode = this.Session["DistCode"].ToString();
                //objUserBEL.FIN_YR = "2017-18";
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                // objUserBEL.CropClass = rbtn_class.SelectedValue;
                objUserBEL.CROP_ID = CropID.ToString();

                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByMOUAgency(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;
                    }
                    //else
                    //{
                    //    Grd_RecvDtls.DataSource = null;
                    //    Grd_RecvDtls.DataBind();
                    //    this.trSubmit.Visible = false;
                    //}
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "10") //Scheme
            {
                #region
                objUserBEL = new BLL_DropDown();
                objUserBEL.DistCode = Session["DistCode"].ToString();
                objUserBEL.AGENCYSECTER = ddl_Scheme.SelectedValue;
                //objUserBEL.FIN_YR = "2017-18";
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByCropIdScheme(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;
                    }
                }
                #endregion
            }
            else
            {
                objUserBEL = new BLL_DropDown();
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillCropVarietyByCropId(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grd_RecvDtls.DataSource = ds;
                        Grd_RecvDtls.DataBind();
                        this.trSubmit.Visible = true;
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
    protected void ddl_CropName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grd_RecvDtls.DataSource = null;
        Grd_RecvDtls.DataBind();
        if (ddl_CropName.SelectedIndex > 0)
        {
            this.FillCropVariety();
        }
    }
    protected void rbtn_lot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow i in Grd_RecvDtls.Rows)
            {
                RadioButton chk = (RadioButton)i.FindControl("rbtn_lot");
                TextBox txt_LotNo = (TextBox)i.FindControl("txt_LotNo");
                DropDownList ddl_BagSize = (DropDownList)i.FindControl("ddl_BagSize");
                TextBox txt_NoOfBag = (TextBox)i.FindControl("txt_NoOfBag");
                TextBox txt_Quintal = (TextBox)i.FindControl("txt_Quintal");

                Button btn_Add = (Button)i.FindControl("btn_Add");
                DropDownList ddl_LotNo = (DropDownList)i.FindControl("ddl_LotNo");

                TextBox txt_testingDate = (TextBox)i.FindControl("txt_testingDate");

                if (chk.Checked == true)
                {
                    txt_LotNo.Enabled = true;
                    txt_NoOfBag.Enabled = true;
                    ddl_BagSize.Enabled = true;
                    btn_Add.Enabled = true;
                    ddl_LotNo.Enabled = true;
                    txt_testingDate.Enabled = true;


                    txt_LotNo.BackColor = System.Drawing.Color.LemonChiffon;
                    txt_NoOfBag.BackColor = System.Drawing.Color.LemonChiffon;
                    ddl_BagSize.BackColor = System.Drawing.Color.LemonChiffon;
                    txt_Quintal.BackColor = System.Drawing.Color.LemonChiffon;
                    ddl_LotNo.BackColor = System.Drawing.Color.LemonChiffon;
                    txt_testingDate.BackColor = System.Drawing.Color.LemonChiffon;
                    btn_Add.BackColor = System.Drawing.Color.SeaGreen;


                }
                else
                {
                    txt_LotNo.Enabled = false;
                    txt_NoOfBag.Enabled = false;
                    ddl_BagSize.Enabled = false;
                    btn_Add.Enabled = false;
                    ddl_LotNo.Enabled = false;
                    txt_testingDate.Enabled = false;


                    txt_LotNo.BackColor = System.Drawing.Color.White;
                    txt_NoOfBag.BackColor = System.Drawing.Color.White;
                    ddl_BagSize.BackColor = System.Drawing.Color.White;
                    txt_Quintal.BackColor = System.Drawing.Color.White;
                    ddl_LotNo.BackColor = System.Drawing.Color.White;
                    txt_testingDate.BackColor = System.Drawing.Color.White;
                    btn_Add.BackColor = System.Drawing.Color.Gray;


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
            this.Page.MaintainScrollPositionOnPostBack = true;
            string UserType = this.Session["UserType"].ToString();
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt_LotNo = (TextBox)row.FindControl("txt_LotNo");

            objUserBEL = new BLL_DropDown();
            objUserBEL.LOT_NO = txt_LotNo.Text.Trim();
            objUserBEL.UserType = this.Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            int Cnt = objUserDLL.GetLotNoFromOB(objUserBEL);
            if (Cnt != 0)
            {
                string popp = "alert('Lot No already exists!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                txt_LotNo.Text = string.Empty;
                return;
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
    protected void ddl_BagSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.Page.MaintainScrollPositionOnPostBack = true;
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
    protected void txt_NoOfBag_TextChanged(object sender, EventArgs e)
    {
        try
        {
            this.Page.MaintainScrollPositionOnPostBack = true;
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt_NoOfBag = (TextBox)row.FindControl("txt_NoOfBag");
            TextBox txt_Quintal = (TextBox)row.FindControl("txt_Quintal");
            DropDownList ddl_BagSize = (DropDownList)row.FindControl("ddl_BagSize");

            Label lbl_IssueBag = (Label)row.FindControl("lbl_IssueBag");

            Label lbl_AvlBag = (Label)row.FindControl("lbl_AvlBag");

            if (lbl_AvlBag.Text == string.Empty)
            {
                lbl_AvlBag.Text = "0";
            }
            int recvbag = Convert.ToInt32(txt_NoOfBag.Text);
            int issuebag = Convert.ToInt32(lbl_AvlBag.Text);
            if (txt_NoOfBag.Text != string.Empty)
            {
                if (Convert.ToDouble(txt_NoOfBag.Text) > 0)
                {
                    if (ddl_BagSize.SelectedIndex > 0)
                    {
                        if (lbl_AvlBag.Text == "0")
                        {
                            txt_Quintal.Text = (Convert.ToDouble(txt_NoOfBag.Text) * Convert.ToDouble(ddl_BagSize.SelectedValue) / 100).ToString();
                        }
                        else if (recvbag > issuebag)
                        {
                            string popp = "alert('You Can Not Receive More Than Issue Bags!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_NoOfBag.Text = "";
                            txt_Quintal.Text = "";
                            return;
                        }
                        else
                        {
                            txt_Quintal.Text = (Convert.ToDouble(txt_NoOfBag.Text) * Convert.ToDouble(ddl_BagSize.SelectedValue) / 100).ToString();
                        }
                    }
                    else
                    {
                        string popp = "alert('Please Select Bag Size!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                        txt_Quintal.Text = string.Empty;
                        ddl_BagSize.Focus();
                        txt_NoOfBag.Text = "";
                        txt_Quintal.Text = "";
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
            for (int i = 0; i < Grd_RecvDtls.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)Grd_RecvDtls.Rows[i].Cells[0]
                                                .FindControl("rbtn_lot");
                if (rb != null)
                {
                    HiddenField hfCropVerid = (HiddenField)Grd_RecvDtls.Rows[i]
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
            for (int i = 0; i < Grd_RecvDtls.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)Grd_RecvDtls.Rows[i]
                                .Cells[0].FindControl("rbtn_lot");
                if (rb != null)
                {
                    if (rb.Checked)
                    {
                        HiddenField hf = (HiddenField)Grd_RecvDtls.Rows[i]
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
    protected void Grd_RecvDtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grd_RecvDtls.PageIndex = e.NewPageIndex;
            this.FillCropVariety();
            this.SetSelectedRecord();
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
    protected void Grd_RecvDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rbtn_lot = (CheckBox)e.Row.FindControl("rbtn_lot");
                //var ddl_Source = (DropDownList)e.Row.FindControl("ddl_Source");
                var hfCropVerid = (HiddenField)e.Row.FindControl("hfCropVerid");
                var lbl_CropVerNm = (Label)e.Row.FindControl("lbl_CropVerNm");
                var ddl_BagSize = (DropDownList)e.Row.FindControl("ddl_BagSize");
                var txt_LotNo = (TextBox)e.Row.FindControl("txt_LotNo");
                var txt_NoOfBag = (TextBox)e.Row.FindControl("txt_NoOfBag");
                var txt_Quintal = (TextBox)e.Row.FindControl("txt_Quintal");
                var btn_Add = (Button)e.Row.FindControl("btn_Add");
                var ddl_LotNo = (DropDownList)e.Row.FindControl("ddl_LotNo");

                var txt_testingDate = (TextBox)e.Row.FindControl("txt_testingDate");
                var lbl_ExpiryDate = (Label)e.Row.FindControl("lbl_ExpiryDate");

                ddl_LotNo.Items.Clear();
                ddl_BagSize.Items.Clear();
                objUserBEL = new BLL_DropDown();
                objUserBEL.STATUS = "1";
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillAllActiveBags(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_BagSize.DataSource = ds;
                        ddl_BagSize.DataTextField = "BAG_NAME";
                        ddl_BagSize.DataValueField = "BAG_SIZE";
                        ddl_BagSize.DataBind();
                    }
                }
                ddl_BagSize.Items.Insert(0, "--Select--");

                if (rbtn_RecvedFrom.SelectedValue == "01")
                {
                    #region
                    string CropClass = string.Empty;

                    if (rbtn_class.SelectedValue == "Foundation")
                    {
                        CropClass = "C01";
                    }
                    else if (rbtn_class.SelectedValue == "TL")
                    {
                        CropClass = "C02";
                    }
                    if (rbtn_class.SelectedValue == "Certified")
                    {
                        CropClass = "C03";
                    }

                    ddl_LotNo.Visible = true;
                    txt_LotNo.Visible = false;

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                    objUserBEL.Crop_Verid = hfCropVerid.Value;
                    objUserBEL.CropClass = CropClass.ToString();
                    objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.FillLotByVarietyCode1(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl_LotNo.DataSource = ds;
                            ddl_LotNo.DataTextField = "LOTNO";
                            ddl_LotNo.DataValueField = "LOTNO";
                            ddl_LotNo.DataBind();
                        }
                    }
                    ddl_LotNo.Items.Insert(0, "--- SELECT ---");
                    #endregion
                }
                else if (rbtn_RecvedFrom.SelectedValue == "05") //Dept
                {
                    #region
                    string CropClass = string.Empty;

                    if (rbtn_class.SelectedValue == "Foundation")
                    {
                        CropClass = "C01";
                    }
                    else if (rbtn_class.SelectedValue == "TL")
                    {
                        CropClass = "C02";
                    }
                    if (rbtn_class.SelectedValue == "Certified")
                    {
                        CropClass = "C03";
                    }

                    ddl_LotNo.Visible = true;
                    txt_LotNo.Visible = false;

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                    objUserBEL.Crop_Verid = hfCropVerid.Value;
                    objUserBEL.CropClass = CropClass.ToString();
                    objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.FillLotByGovtFarm(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl_LotNo.Items.Clear();
                            ddl_LotNo.DataSource = ds;
                            ddl_LotNo.DataTextField = "LotNo";
                            ddl_LotNo.DataValueField = "LotNo";
                            ddl_LotNo.DataBind();
                        }
                    }
                    ddl_LotNo.Items.Insert(0, "--- SELECT ---");
                    #endregion
                }
                else if (rbtn_RecvedFrom.SelectedValue == "06") //OUAT
                {
                    #region
                    string CropClass = string.Empty;

                    if (rbtn_class.SelectedValue == "Foundation")
                    {
                        CropClass = "C01";
                    }
                    else if (rbtn_class.SelectedValue == "TL")
                    {
                        CropClass = "C02";
                    }
                    if (rbtn_class.SelectedValue == "Certified")
                    {
                        CropClass = "C03";
                    }

                    ddl_LotNo.Visible = true;
                    txt_LotNo.Visible = false;

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                    objUserBEL.Crop_Verid = hfCropVerid.Value;
                    objUserBEL.CropClass = CropClass.ToString();
                    objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.FillLotByOUAT(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl_LotNo.DataSource = ds;
                            ddl_LotNo.DataTextField = "LotNo";
                            ddl_LotNo.DataValueField = "LotNo";
                            ddl_LotNo.DataBind();
                        }
                    }
                    ddl_LotNo.Items.Insert(0, "--- SELECT ---");
                    #endregion
                }
                else if (rbtn_RecvedFrom.SelectedValue == "09") //MOU Agency
                {
                    #region
                    string CropClass = string.Empty;

                    if (rbtn_class.SelectedValue == "Foundation")
                    {
                        CropClass = "C01";
                    }
                    else if (rbtn_class.SelectedValue == "TL")
                    {
                        CropClass = "C02";
                    }
                    if (rbtn_class.SelectedValue == "Certified")
                    {
                        CropClass = "C03";
                    }

                    ddl_LotNo.Visible = true;
                    txt_LotNo.Visible = false;

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                    objUserBEL.Crop_Verid = hfCropVerid.Value;
                    objUserBEL.CropClass = CropClass.ToString();
                    objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.FillLotByMOUAgency(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl_LotNo.DataSource = ds;
                            ddl_LotNo.DataTextField = "LotNo";
                            ddl_LotNo.DataValueField = "LotNo";
                            ddl_LotNo.DataBind();
                        }
                    }
                    ddl_LotNo.Items.Insert(0, "--- SELECT ---");
                    #endregion
                }
                else if (rbtn_RecvedFrom.SelectedValue == "10") // SCHEME
                {
                    #region
                    string CropClass = string.Empty;

                    if (rbtn_class.SelectedValue == "Foundation")
                    {
                        CropClass = "C01";
                    }
                    else if (rbtn_class.SelectedValue == "TL")
                    {
                        CropClass = "C02";
                    }
                    if (rbtn_class.SelectedValue == "Certified")
                    {
                        CropClass = "C03";
                    }

                    ddl_LotNo.Visible = true;
                    txt_LotNo.Visible = false;

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                    objUserBEL.Crop_Verid = hfCropVerid.Value;
                    objUserBEL.CropClass = CropClass.ToString();
                    objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.FillLotByVarietyCodeScheme(objUserBEL);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddl_LotNo.DataSource = ds;
                            ddl_LotNo.DataTextField = "LotNo";
                            ddl_LotNo.DataValueField = "LotNo";
                            ddl_LotNo.DataBind();
                        }
                    }
                    ddl_LotNo.Items.Insert(0, "--- SELECT ---");
                    #endregion
                }
                else
                {
                    ddl_LotNo.Visible = false;
                    txt_LotNo.Visible = true;
                    txt_testingDate.Visible = true;
                    lbl_ExpiryDate.Visible = true;
                    //Grd_RecvDtls.Columns[5].HeaderText = "Testing Date";
                    //Grd_RecvDtls.Columns[6].HeaderText = "Expiry Date";

                }



                DataTable dt = (DataTable)Session["dt"];
                foreach (DataRow row in dt.Rows)
                {
                    string mVarietyCode = row["VARIETY_ID"].ToString();
                    string mClass = row["CROP_CLASS"].ToString();

                    if (hfCropVerid.Value == mVarietyCode && rbtn_class.SelectedValue == mClass)
                    {
                        if (rbtn_RecvedFrom.SelectedValue == "01")
                        {
                            ddl_LotNo.SelectedValue = row["LOT_NO"].ToString();
                        }
                        else
                        {
                            txt_LotNo.Text = row["LOT_NO"].ToString();
                        }
                        ddl_BagSize.SelectedValue = row["BAG_SIZE"].ToString();
                        txt_NoOfBag.Text = row["NO_OF_BAGS"].ToString();
                        txt_Quintal.Text = row["QUANTITY"].ToString();
                        btn_Add.Text = "Update";
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
    private void CreateNewDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[13] 
                {
                    new DataColumn("SlNo", typeof(int)),
                    new DataColumn("CATEGORY_ID", typeof(string)),
                    new DataColumn("CROP_ID", typeof(string)),
                    new DataColumn("CROP_CLASS", typeof(string)),
                    new DataColumn("VARIETY_ID", typeof(string)),                    
                    new DataColumn("VARIETY_NAME", typeof(string)),
                    new DataColumn("LOT_NO",typeof(string)),
                    new DataColumn("REF_NO",typeof(string)),
                    new DataColumn("BAG_SIZE",typeof(string)), 
                    new DataColumn("NO_OF_BAGS",typeof(string)),
                    new DataColumn("QUANTITY",typeof(string)),
                    new DataColumn("Testing_Date",typeof(string)),
                    new DataColumn("Expiry_Date",typeof(string))
                });
        Session.Add("dt", dt);
    }
    protected void btn_Add_Click(object sender, EventArgs e)
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
                HiddenField hfCropVerid = (HiddenField)btn.Parent.FindControl("hfCropVerid");
                Label lbl_CropVerNm = (Label)btn.Parent.FindControl("lbl_CropVerNm");

                TextBox txt_LotNo = (TextBox)btn.Parent.FindControl("txt_LotNo");
                DropDownList ddl_BagSize = (DropDownList)btn.Parent.FindControl("ddl_BagSize");
                TextBox txt_NoOfBag = (TextBox)btn.Parent.FindControl("txt_NoOfBag");
                TextBox txt_Quintal = (TextBox)btn.Parent.FindControl("txt_Quintal");
                Button btn_Add = (Button)btn.Parent.FindControl("btn_Add");
                DropDownList ddl_LotNo = (DropDownList)btn.Parent.FindControl("ddl_LotNo");

                HiddenField hfTestingdate = (HiddenField)btn.Parent.FindControl("hfTestingdate");
                HiddenField hfExpiredate = (HiddenField)btn.Parent.FindControl("hfExpiredate");

                TextBox txt_testingDate = (TextBox)btn.Parent.FindControl("txt_testingDate");
                Label lbl_ExpiryDate = (Label)btn.Parent.FindControl("lbl_ExpiryDate");

                HiddenField hfTestDate = (HiddenField)btn.Parent.FindControl("hfTestDate");
                HiddenField hfExpDate = (HiddenField)btn.Parent.FindControl("hfExpDate");

                string RefNo = string.Empty;

                if (rbtn_RecvedFrom.SelectedValue == "01" || rbtn_RecvedFrom.SelectedValue == "05" || rbtn_RecvedFrom.SelectedValue == "06" || rbtn_RecvedFrom.SelectedValue == "09" || rbtn_RecvedFrom.SelectedValue == "10")
                {
                    if (ddl_LotNo.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select Lot No. !!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_LotNo);
                        return;
                    }
                    else if (ddl_CropCatagory.SelectedIndex == 0)
                    {
                        string pop = "alert('Please select the Crop Catagory!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(ddl_CropCatagory);
                        return;
                    }
                }
                else
                {
                    if (txt_LotNo.Text == "")
                    {
                        string pop = "alert('Please enter the Lot Number!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        ScriptManager.GetCurrent(Page).SetFocus(txt_LotNo);
                        return;
                    }
                }

                if (ddlFinYr.SelectedIndex == -1)
                {
                    string pop = "alert('Please select Financial Year!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddlFinYr);
                    return;
                }
                else if (ddlSeassion.SelectedIndex == -1)
                {
                    string pop = "alert('Please select Seassion!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSeassion);
                    return;
                }
                else if (ddl_CropCatagory.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Crop Catagory!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_CropCatagory);
                    return;
                }
                else if (ddl_CropName.SelectedIndex == 0)
                {
                    string pop = "alert('Please select the Crop Name!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_CropName);
                    return;
                }
                else if (rbtn_class.SelectedIndex < 0)
                {
                    string pop = "alert('Please select the Class!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(rbtn_class);
                    return;
                }
                else if (ddl_BagSize.SelectedIndex == 0)
                {
                    string pop = "alert('Please Bag Size (IN Kg.)!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddl_BagSize);
                    return;
                }
                else if (txt_NoOfBag.Text == "")
                {
                    string pop = "alert('Please enter no of bags receive!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    ScriptManager.GetCurrent(Page).SetFocus(txt_NoOfBag);
                    return;
                }
                else
                {
                    decimal total = Convert.ToInt32(ddl_BagSize.SelectedValue) * Convert.ToInt32(txt_NoOfBag.Text);
                    decimal Quantity = Convert.ToDecimal(total) / (100);
                    txt_Quintal.Text = Quantity.ToString();
                    Session["value"] = txt_Quintal.Text;

                    if (Session["value"].ToString() == txt_Quintal.Text)
                    {
                        if (rbtn_RecvedFrom.SelectedValue == "01" || rbtn_RecvedFrom.SelectedValue == "05" || rbtn_RecvedFrom.SelectedValue == "06" || rbtn_RecvedFrom.SelectedValue == "09" || rbtn_RecvedFrom.SelectedValue == "10")
                        {
                            objUserBEL = new BLL_DropDown();
                            objUserBEL.LOT_NO = ddl_LotNo.SelectedValue;
                            objUserBEL.UserType = this.Session["UserType"].ToString();
                            objUserBEL.Challan_No = txtChallanNo.Text.Trim();
                            objUserDLL = new DLL_DropDown();
                            objUserBEL.TESTING_DATE = txt_testingDate.Text;
                            objUserBEL.EXPIRY_DATE = lbl_ExpiryDate.Text;
                            int Cnt = objUserDLL.GetLotNoFromRCV(objUserBEL);
                            if (Cnt != 0)
                            {
                                string popp = "alert('Lot No already exists!!!')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                                txt_LotNo.Text = string.Empty;
                                return;
                            }
                        }
                        else
                        {
                            objUserBEL = new BLL_DropDown();
                            objUserBEL.LOT_NO = txt_LotNo.Text.Trim();
                            objUserBEL.UserType = this.Session["UserType"].ToString();
                            objUserBEL.Challan_No = txtChallanNo.Text.Trim();
                            objUserBEL.TESTING_DATE = txt_testingDate.Text.ToString();
                            objUserBEL.EXPIRY_DATE = Convert.ToDateTime(objUserBEL.TESTING_DATE).AddMonths(9).ToString();
                            objUserDLL = new DLL_DropDown();
                            int Cnt = objUserDLL.GetLotNoFromRCV(objUserBEL);
                            if (Cnt != 0)
                            {
                                string popp = "alert('Lot No already exists!!!')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                                txt_LotNo.Text = string.Empty;
                                return;
                            }
                        }

                        objUserBEL = new BLL_DropDown();
                        objUserBEL.Crop_Verid = hfCropVerid.Value;
                        objUserBEL.CropClass = rbtn_class.SelectedValue;
                        objUserBEL.Godown_ID = Chk_Godown.SelectedValue;
                        objUserBEL.UserType = this.Session["UserType"].ToString();
                        objUserDLL = new DLL_DropDown();

                        objUserBEL = new BLL_DropDown();
                        objUserBEL.Crop_Verid = hfCropVerid.Value;
                        objUserBEL.CropClass = rbtn_class.SelectedValue;
                        objUserBEL.LOT_NO = txt_LotNo.Text;
                        objUserBEL.UserType = this.Session["UserType"].ToString();

                        //objUserBEL.TESTING_DATE = hfTestingdate.Value;
                        //objUserBEL.EXPIRY_DATE = hfExpiredate.Value;

                        objUserDLL = new DLL_DropDown();
                        int Cnt1 = objUserDLL.CheckFromRCV1(objUserBEL);
                        if (Cnt1 == 0)
                        {
                            DataTable dt = (DataTable)Session["dt"];
                            var foundLotNo = dt.Select("Lot_No = '" + txt_LotNo.Text + "'");

                            if (btn_Add.Text == "Add")
                            {
                                if (foundLotNo.Length > 0)
                                {
                                    string popp = "alert('Provided Lot Number Already Exists!!!')";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                                    txt_LotNo.Text = "";
                                    txt_LotNo.Focus();
                                    return;
                                }
                                else
                                {
                                    string LotNo = "";
                                    string TDate = "";
                                    string EDate = "";
                                    if (rbtn_RecvedFrom.SelectedValue == "01")
                                    {
                                        LotNo = ddl_LotNo.SelectedValue;
                                        RefNo = ddl_RecvFrom.SelectedValue;
                                        TDate = hfTestingdate.Value;
                                        EDate = hfExpiredate.Value;
                                    }
                                    else if (rbtn_RecvedFrom.SelectedValue == "02")
                                    {
                                        LotNo = txt_LotNo.Text;
                                        //TDate = txt_testingDate.Text.ToString();
                                        TDate = hfTestDate.Value;
                                        EDate = hfExpDate.Value;//Convert.ToDateTime(TDate).AddMonths(9).ToString("dd/MM/yyyy");
                                    }
                                    else if (rbtn_RecvedFrom.SelectedValue == "05" || rbtn_RecvedFrom.SelectedValue == "06" || rbtn_RecvedFrom.SelectedValue == "09" || rbtn_RecvedFrom.SelectedValue == "10")
                                    {
                                        LotNo = ddl_LotNo.SelectedValue;
                                        RefNo = GetRefNoFromLotNo(ddl_LotNo.SelectedValue);
                                        TDate = hfTestingdate.Value;
                                        EDate = hfExpiredate.Value;

                                    }
                                    else
                                    {
                                        LotNo = txt_LotNo.Text;
                                        //abc= txt_testingDate.Text.ToString();
                                        //xyz= Convert.ToDateTime(abc).AddMonths(9).ToString();
                                        //hfTestingdate.Value = txt_testingDate.Text.ToString();
                                        //hfExpiredate.Value= Convert.ToDateTime(objUserBEL.TESTING_DATE).AddMonths(9).ToString();
                                    }

                                    DataRow[] dRCheck = dt.Select("VARIETY_ID='" + hfCropVerid.Value + "' AND LOT_NO='" + LotNo + "'");
                                    {
                                        if (dRCheck.Length > 0)
                                        {
                                            ddl_LotNo.SelectedIndex = 0;
                                            ddl_BagSize.SelectedIndex = 0;
                                            txt_NoOfBag.Text = string.Empty;
                                            txt_Quintal.Text = string.Empty;
                                            string popp = "alert('Record already exists !!!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                                        }
                                        else
                                        {
                                            dt.Rows.Add(int.Parse(Session["SlNo"].ToString()), ddl_CropCatagory.SelectedValue, ddl_CropName.SelectedValue, rbtn_class.SelectedValue, hfCropVerid.Value, lbl_CropVerNm.Text, LotNo, RefNo, ddl_BagSize.SelectedValue, txt_NoOfBag.Text, txt_Quintal.Text, (Convert.ToDateTime(TDate)).ToString("dd/MM/yyyy"), (Convert.ToDateTime(EDate)).ToString("dd/MM/yyyy"));
                                            string pop = "alert('Data Added Sucessfully!!!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);

                                            // ddl_LotNo.SelectedIndex = 0;
                                            ddl_BagSize.SelectedIndex = 0;

                                            txt_NoOfBag.Text = string.Empty;
                                            txt_Quintal.Text = string.Empty;
                                            trTempDetails.Visible = true;
                                            grdTempDetails.DataSource = dt;
                                            grdTempDetails.DataBind();
                                            Session["SlNo"] = int.Parse(Session["SlNo"].ToString()) + 1;
                                        }
                                    }
                                }
                            }
                            Session.Add("dt", dt);
                        }
                        else
                        {
                            string popp = "alert('Provided Lot Number Already Exists!!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
                            txt_LotNo.Text = "";
                            txt_LotNo.Focus();
                        }
                    }
                    else
                    {
                        string popp = "alert('Please Check the Quintal value!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
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
    protected void btn_Submit_Click(object sender, EventArgs e)
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
                if (btn_Submit.Text == "Confirm")
                {
                    objUserBEL = new BLL_DropDown();
                    objUserBEL.DistCode = Session["DistCode"].ToString();
                    objUserBEL.Godown_ID = Chk_Godown.SelectedValue;
                    objUserBEL.AgenciesID = rbtn_RecvedFrom.SelectedValue;
                    objUserBEL.mGodown_ID = ddl_RecvFrom.SelectedValue;
                    objUserBEL.CashMemo_No = ddl_ChallanNo.Text;
                    objUserBEL.Sale_Date = txt_Date.Text.Trim();
                    objUserBEL.UserType = Session["UserType"].ToString();
                    objUserBEL.UserID = Session["UserID"].ToString();
                    objUserBEL.SESSION = ddlSeassion.SelectedValue;
                    objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
                    objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

                    objUserDLL = new DLL_DropDown();
                    int Val = objUserDLL.InsertRCVConfirm(objUserBEL);
                    if (Val == 1)//SUCCESS
                    {
                        ActivityLog activityLog = new ActivityLog()
                        {
                            UserID = this.Session["UserID"].ToString(),
                            UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                            ActivityType = "Action",
                            Activity = "Receive Entry",
                            PageURL = HttpContext.Current.Request.Url.ToString(),
                            Remark = "Receive Entry Successfully !",
                            OS = Request.Browser.Platform.ToString(),
                            BWSER = Request.Browser.Type.ToString()
                        };
                        ActivityLog.InsertActivityLog_Stock(activityLog);

                        string pop = "alert('Receive Entry Successfully Completed !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);

                        grd_lottrans.Visible = false;
                        this.hfDistCode.Value = Session["DistCode"].ToString();
                        this.lblDistName.Text = this.lblDistName1.Text = Session["DistName"].ToString();
                        this.FillGodown();
                        this.trReceivedType.Visible = false;
                        this.trWayBill.Visible = false;
                        this.ddl_ChallanNo.Visible = false;
                        this.CreateNewDataTable();
                        this.utl.SetSessionCookie();
                        this.hfSession.Value = this.Session["AuthTokenPage"].ToString();

                        Grd_RecvDtls.DataSource = null;
                        Grd_RecvDtls.DataBind();

                        grd_lottrans.DataSource = null;
                        grd_lottrans.DataBind();

                        trSubmit.Visible = false;

                        FillCropCatg();
                        this.ddl_CropName.Items.Clear();
                        this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
                        tblCropInf.Visible = true;

                        btn_ExistGodown.Visible = true;

                        txtChallanNo.Enabled = true;
                        txt_Date.Enabled = true;
                        txt_Date.Text = string.Empty;
                        txtChallanNo.Visible = true;
                        txtChallanNo.Text = string.Empty;

                        tblCropInf.Visible = false;
                    }
                    else if (Val == 0)//UN-SUCCESS
                    {
                        string pop = "alert('Something went Wrong!!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                    }
                }
                else if (btn_Submit.Text == "Submit")
                {
                    DataTable dt = (DataTable)Session["dt"];
                    if (dt.Rows.Count > 0)
                    {
                        dt.TableName = "A";
                        objUserBEL = new BLL_DropDown();
                        objUserBEL.DistCode = hfDistCode.Value;
                        objUserBEL.Godown_ID = Session["Godown_Id"].ToString();
                        // objUserBEL.Godown_ID = Chk_Godown.SelectedValue;
                        objUserBEL.AgenciesID = rbtn_RecvedFrom.SelectedValue;
                        if (rbtn_RecvedFrom.SelectedValue == "01")
                        {
                            objUserBEL.Receive_Unitcd = "0123";
                            objUserBEL.MOU_REFNO = ddl_RecvFrom.SelectedValue;
                        }
                        else if (rbtn_RecvedFrom.SelectedValue == "05")
                        {
                            objUserBEL.Receive_Unitcd = "0121";
                            objUserBEL.MOU_REFNO = dt.Rows[0]["REF_NO"].ToString();
                        }
                        else if (rbtn_RecvedFrom.SelectedValue == "06")
                        {
                            objUserBEL.Receive_Unitcd = "0122";
                            objUserBEL.MOU_REFNO = dt.Rows[0]["REF_NO"].ToString();
                        }
                        else if (rbtn_RecvedFrom.SelectedValue == "09")
                        {
                            objUserBEL.Receive_Unitcd = "0124";
                            objUserBEL.MOU_REFNO = dt.Rows[0]["REF_NO"].ToString();
                        }
                        else if (rbtn_RecvedFrom.SelectedValue == "10")
                        {
                            if (ddl_Scheme.SelectedValue == "BGREI")
                            {
                                objUserBEL.Receive_Unitcd = "0125";
                            }
                            else if (ddl_Scheme.SelectedValue == "ICRISAT")
                            {
                                objUserBEL.Receive_Unitcd = "0126";
                            }
                            else if (ddl_Scheme.SelectedValue == "NFSM")
                            {
                                objUserBEL.Receive_Unitcd = "0127";
                            }
                            else if (ddl_Scheme.SelectedValue == "NMOOP-DAandFP")
                            {
                                objUserBEL.Receive_Unitcd = "0128";
                            }
                            else if (ddl_Scheme.SelectedValue == "NSC")
                            {
                                objUserBEL.Receive_Unitcd = "0129";
                            }
                            else if (ddl_Scheme.SelectedValue == "RKVY")
                            {
                                objUserBEL.Receive_Unitcd = "0130";
                            }
                            objUserBEL.MOU_REFNO = dt.Rows[0]["REF_NO"].ToString();
                        }
                        else
                        {
                            objUserBEL.Receive_Unitcd = ddl_RecvFrom.SelectedValue;
                            //objUserBEL.MOU_REFNO = "";
                        }
                        objUserBEL.Outagency_Waybillnum_Farm = txtWayBillNo.Text.Trim();
                        objUserBEL.Challan_No = txtChallanNo.Text.Trim();
                        objUserBEL.mReceive_Date = txt_Date.Text.Trim();
                        objUserBEL.SESSION = ddlSeassion.SelectedValue;
                        objUserBEL.FIN_YR = ddlFinYr.SelectedValue;
                        objUserBEL.UserType = Session["UserType"].ToString();
                        objUserBEL.UserID = Session["UserID"].ToString();
                        objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                        objUserBEL.XML_Value = ConvertDatatableToXML(dt);
                        string dist_Code = objUserBEL.DistCode;                        
                        string source_ID = objUserBEL.Receive_Unitcd;
                        string receiver_ID = objUserBEL.Godown_ID;
                        string date_Intake = objUserBEL.mReceive_Date;
                        string year = objUserBEL.FIN_YR;
                        string season = objUserBEL.SESSION;
                        string pr_Number = objUserBEL.Challan_No;                        
                        string UserID = objUserBEL.UserID;
                        string UserIP = objUserBEL.UserIP;
                        objUserDLL = new DLL_DropDown();
                        int str = objUserDLL.InsertRCVSubmit(objUserBEL);

                        if (str == 1)
                        {
                            ActivityLog activityLog = new ActivityLog()
                            {
                                UserID = this.Session["UserID"].ToString(),
                                UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                                ActivityType = "Action",
                                Activity = "Receive Entry",
                                PageURL = HttpContext.Current.Request.Url.ToString(),
                                Remark = "Receive Entry Successfully !",
                                OS = Request.Browser.Platform.ToString(),
                                BWSER = Request.Browser.Type.ToString()
                            };
                            ActivityLog.InsertActivityLog_Stock(activityLog);

                            string pop = "alert('Receive Entry Successfully Completed !!!');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);


                            grd_lottrans.Visible = false;
                            this.hfDistCode.Value = Session["DistCode"].ToString();
                            this.lblDistName.Text = this.lblDistName1.Text = Session["DistName"].ToString();
                            this.FillGodown();
                            this.trReceivedType.Visible = false;
                            this.trWayBill.Visible = false;
                            this.ddl_ChallanNo.Visible = false;
                            this.CreateNewDataTable();
                            this.utl.SetSessionCookie();
                            this.hfSession.Value = this.Session["AuthTokenPage"].ToString();

                            Grd_RecvDtls.DataSource = null;
                            Grd_RecvDtls.DataBind();

                            grd_lottrans.DataSource = null;
                            grd_lottrans.DataBind();

                            trTempDetails.Visible = false;
                            grdTempDetails.DataSource = null;
                            grdTempDetails.DataBind();

                            trSubmit.Visible = false;
                            FillCropCatg();
                            this.ddl_CropName.Items.Clear();
                            this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
                            tblCropInf.Visible = true;
                            btn_ExistGodown.Visible = true;

                            txtChallanNo.Enabled = true;
                            txtChallanNo.Text = string.Empty;
                            tblCropInf.Visible = false;

                            Session["SlNo"] = 1;
                            cReceiveDetails objUserBEL1;
                            foreach (DataRow row in dt.Rows)
                            {                                
                                objUserBEL1 = new cReceiveDetails();
                                objUserBEL1.dist_Code = Session["LGDistrict"].ToString();
                                objUserBEL1.lot_Number = row["LOT_NO"].ToString();
                                objUserBEL1.source_ID = source_ID;
                                objUserBEL1.receiver_ID = receiver_ID;
                                objUserBEL1.qty_Per_Bag_Kg = row["BAG_SIZE"].ToString();
                                objUserBEL1.no_of_Bag= row["NO_OF_BAGS"].ToString();
                                objUserBEL1.date_Intake = date_Intake;
                                objUserBEL1.year = year;
                                objUserBEL1.season = season;
                                objUserBEL1.crop = row["CROP_ID"].ToString();
                                objUserBEL1.variety = row["VARIETY_ID"].ToString();
                                objUserBEL1.Class = row["CROP_CLASS"].ToString();
                                objUserBEL1.pr_Number = pr_Number;
                                objUserBEL1.SourceType = rbtn_RecvedFrom.SelectedItem.Text;
                                objUserBEL1.agencyName = ddl_RecvFrom.SelectedItem.Text;//rbtn_RecvedFrom.SelectedItem.Text;
                                objUserBEL1.receiverType = "godown";
                                objUserBEL1.UserID = UserID;
                                objUserBEL1.UserIP = UserIP;
                                objUserBEL1.CropCatg_ID = row["BAG_SIZE"].ToString();
                                SendReceiveDetails(objUserBEL1);
                            }                            
                        }
                        else
                        {
                            string pop = "alert('Something went Wrong !!!');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            return;
                        }
                    }
                    else
                    {
                        string pop = "alert('Add Some Variety !!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                        return;
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
    protected void btn_ExistGodown_Click(object sender, EventArgs e)
    {
        rbtn_RecvedFrom.SelectedIndex = -1;
        ClearAll();
        this.tblMainSection.Visible = true;
        tblSearch.Visible = false;
        tblCropInf.Visible = false;
        EnableControls();
        txtWayBillNo.Text = "";
        txt_Date.Text = "";
        txtChallanNo.Text = "";
        Grd_RecvDtls.DataSource = null;
        Grd_RecvDtls.DataBind();
        grd_lottrans.DataSource = null;
        grd_lottrans.DataBind();
        trSubmit.Visible = false;
        Session["Godown_Id"] = null;
    }
    public void ClearAll()
    {
        //control enabled
        rbtn_RecvedFrom.Enabled = true;
        ddl_RecvFrom.Enabled = true;
        txtWayBillNo.Enabled = true;
        txt_Date.Enabled = true;
        txtChallanNo.Enabled = true;
        txt_Date.Text = "";
        txtChallanNo.Text = "";
        rbtn_RecvedFrom.SelectedIndex = -1;
        rbtn_class.SelectedIndex = 0;
        ddl_RecvFrom.Items.Clear();
        txtWayBillNo.Text = "";
        FillGodown();
        trReceivedType.Visible = false;
        trWayBill.Visible = false;
        ddl_ChallanNo.Visible = false;
    }
    protected void ddl_RecvFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GetChallanNo();

            if (rbtn_RecvedFrom.SelectedValue == "01") //Seed Grower
            {
                lblGrowerVareity.Visible = true;
                objUserBEL = new BLL_DropDown();
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillGrowerVarietyName(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string variety = string.Empty;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            variety = variety + dr["Varity_Name"].ToString() + ", ";
                        }
                        lblGrowerVareity.Text = "Vareity Name : " + variety;
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
            //ExceptionHandler.WriteLog(ExceptionHandler.CreateErrorMessage(exception));
        }

    }
    protected void rbtn_class_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Grd_RecvDtls.DataSource = null;
            Grd_RecvDtls.DataBind();
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
    protected void btnOk_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Stock_OBEntry.aspx");
    }
    protected void ddl_CropCatagory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            Grd_RecvDtls.DataSource = null;
            Grd_RecvDtls.DataBind();
            FillCrop();
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
    protected void ddl_CropName_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            Grd_RecvDtls.DataSource = null;
            Grd_RecvDtls.DataBind();
            FillCropVariety();
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
    protected void ddl_Scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblRecvText.Text = "Agency Name";
            trWayBill.Visible = false;
            lblChallan.Text = "Challan No.";
            Regex regex = new Regex("^\\d{2}$");
            string str = this.Session["DistCode"].ToString();
            if (!regex.IsMatch(str))
            {
                throw new ApplicationException("Invalid Characters!");
            }
            objUserBEL = new BLL_DropDown();
            objUserBEL.AGENCYSECTER = ddl_Scheme.SelectedValue;
            objUserBEL.DistCode = this.Session["DistCode"].ToString();
            objUserBEL.FIN_YR = "2017-18";
            objUserDLL = new DLL_DropDown();

            ddl_RecvFrom.Items.Clear();
            ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");

            ds = new DataSet();
            ds = objUserDLL.FillByUserTypeScheme(objUserBEL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_RecvFrom.DataSource = ds;
                    ddl_RecvFrom.DataValueField = "Name_of_agency";
                    ddl_RecvFrom.DataTextField = "AgName";
                    ddl_RecvFrom.DataBind();
                    ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");
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
    protected string GetRefNoFromLotNo(string lot) //Get LOTNO.
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);

        SqlCommand cmd = new SqlCommand("SELECT Ref_NO FROM VW_agencyTag WHERE Status='3' and LotNo=@LotNo", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LotNo", lot);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "N/A";
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return null;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void CancelControls()
    {

        ddlFinYr.Enabled = false;
        ddlSeassion.Enabled = false;
        FillAgency();
        rbtn_RecvedFrom.Enabled = true;
        ddl_RecvFrom.Items.Clear();
        ddl_RecvFrom.Items.Insert(0, "--- SELECT ---");
        this.trReceivedType.Visible = false;
        this.trWayBill.Visible = false;
        txtWayBillNo.Enabled = true;
        txtWayBillNo.Text = string.Empty;
        this.ddl_ChallanNo.Visible = false;
        ddl_RecvFrom.Enabled = true;
        lblGrowerVareity.Visible = false;
        txt_Date.Enabled = true;
        txt_Date.Text = string.Empty;
        txtChallanNo.Enabled = true;
        txtChallanNo.Text = string.Empty;
        tblCropInf.Visible = true;
        FillCropCatg();
        this.ddl_CropName.Items.Clear();
        this.ddl_CropName.Items.Insert(0, "--- SELECT ---");
        tblCropInf.Visible = false;
        trTempDetails.Visible = false;
        trSubmit.Visible = false;
        grdTempDetails.DataSource = null;
        grdTempDetails.DataBind();
        trScheme.Visible = false;
        ddl_Scheme.SelectedIndex = 0;
        lbl_challan.Text = string.Empty;

        Grd_RecvDtls.DataSource = null;
        Grd_RecvDtls.DataBind();
        Response.Redirect("Stock_ReceiveEntry.aspx");
        // FillCropVariety();

    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        CancelControls();
    }
    protected void btn_Cancel1_Click(object sender, EventArgs e)
    {
        CancelControls();
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnDel = (Button)sender;
            Label lblSl = (Label)btnDel.Parent.FindControl("lblSl");
            DataTable dt = (DataTable)Session["dt"];
            DataRow[] row = dt.Select("SlNo=" + lblSl.Text.Trim());
            foreach (DataRow dr in row)
            {
                dt.Rows.Remove(dr);
            }
            dt.AcceptChanges();
            grdTempDetails.DataSource = dt;
            grdTempDetails.DataBind();
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
    //  BIBHU  // 

    // Receive Grid
    protected void ddl_LotNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CropClass = string.Empty;

            if (rbtn_class.SelectedValue == "Foundation")
            {
                CropClass = "C01";
            }
            else if (rbtn_class.SelectedValue == "TL")
            {
                CropClass = "C02";
            }
            if (rbtn_class.SelectedValue == "Certified")
            {
                CropClass = "C03";
            }
            objUserBEL = new BLL_DropDown();
            objUserBEL.CropClass = CropClass.ToString();

            this.Page.MaintainScrollPositionOnPostBack = true;
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            TextBox txt_NoOfBag = (TextBox)row.FindControl("txt_NoOfBag");
            TextBox txt_Quintal = (TextBox)row.FindControl("txt_Quintal");
            var hfCropVerid = (HiddenField)row.FindControl("hfCropVerid");
            DropDownList ddl_lotNo = (DropDownList)row.FindControl("ddl_LotNo");
            DropDownList ddl_BagSize = (DropDownList)row.FindControl("ddl_BagSize");
            Label lbl_IssueBag = (Label)row.FindControl("lbl_IssueBag");

            Label lbl_AvlBag = (Label)row.FindControl("lbl_AvlBag");

            var hfavlbag = (HiddenField)row.FindControl("hfavlbag");

            var hfTestingdate = (HiddenField)row.FindControl("hfTestingdate");

            var hfExpiredate = (HiddenField)row.FindControl("hfExpiredate");

            txt_NoOfBag.Enabled = true;
            txt_NoOfBag.Text = string.Empty;
            txt_Quintal.Text = string.Empty;
            hfavlbag.Value = string.Empty;
            lbl_IssueBag.Text = string.Empty;


            // ddl_BagSize.SelectedIndex = 0;


            if (rbtn_RecvedFrom.SelectedValue == "01") // SEED GROWER
            {
                #region
                objUserBEL = new BLL_DropDown();

                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();

                objUserBEL.LOT_NO = ddl_lotNo.SelectedValue;
                objUserBEL.Crop_Verid = hfCropVerid.Value;

                objUserBEL.Godown_ID = Chk_Godown.SelectedValue;

                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillRecvBagsByLot(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        hfTestingdate.Value = ds.Tables[0].Rows[0]["Testing_date"].ToString();
                        hfExpiredate.Value = ds.Tables[0].Rows[0]["Expire_Date"].ToString();

                        DateTime testdate = Convert.ToDateTime(hfTestingdate.Value.ToString());
                        DateTime expdate = Convert.ToDateTime(hfExpiredate.Value.ToString());
                        DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        if (todaydate > expdate)
                        {
                            string pop = "alert('Lot.No Expiry !!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            txt_NoOfBag.Enabled = false;
                            ddl_lotNo.SelectedIndex = 0;
                            ddl_BagSize.SelectedIndex = 0;
                            return;
                        }
                        else
                        {
                            lbl_IssueBag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                            objUserDLL = new DLL_DropDown();
                            objUserBEL.Godown_ID = Session["Godown_Id"].ToString();
                            DataSet dts = new DataSet();
                            dts = objUserDLL.CountBagsFromStockStockDetails(objUserBEL);
                            if (dts.Tables[0].Rows[0]["count"].ToString() == "0")
                            {
                                lbl_AvlBag.Text = lbl_IssueBag.Text;
                            }
                            else
                            {
                                int aa = 0;
                                int bb = 0;
                                objUserDLL = new DLL_DropDown();
                                DataSet dss = new DataSet();
                                dss = objUserDLL.fillBagsFromStockStockDetails(objUserBEL);
                                if (dss != null)
                                {
                                    if (dss.Tables[0].Rows.Count > 0)
                                    {
                                        hfavlbag.Value = dss.Tables[0].Rows[0]["Recv_No_Of_Bags"].ToString();
                                        aa = Convert.ToInt32(lbl_IssueBag.Text);
                                        bb = Convert.ToInt32(hfavlbag.Value);
                                        lbl_AvlBag.Text = (aa - bb).ToString();
                                        int inputNumber;
                                        inputNumber = Convert.ToInt32(lbl_AvlBag.Text);
                                        if (inputNumber <= 0)
                                        {
                                            string pop = "alert('No Stock Available !!!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                                            txt_NoOfBag.Enabled = false;
                                            lbl_AvlBag.Text = "0";
                                            return;
                                        }
                                        else
                                        {
                                            txt_NoOfBag.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "05") // Departmental farm
            {
                #region

                objUserBEL = new BLL_DropDown();
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                objUserBEL.LOT_NO = ddl_lotNo.SelectedValue;
                objUserBEL.Crop_Verid = hfCropVerid.Value;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillRecvBagsByLotByDepartmental(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfTestingdate.Value = ds.Tables[0].Rows[0]["Testing_date"].ToString();
                        hfExpiredate.Value = ds.Tables[0].Rows[0]["Expire_Date"].ToString();

                        DateTime testdate = Convert.ToDateTime(hfTestingdate.Value.ToString());
                        DateTime expdate = Convert.ToDateTime(hfExpiredate.Value.ToString());
                        DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        if (todaydate > expdate)
                        {
                            string pop = "alert('Lot.No Expiry !!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            txt_NoOfBag.Enabled = false;
                            ddl_lotNo.SelectedIndex = 0;
                            ddl_BagSize.SelectedIndex = 0;
                            return;
                        }
                        else
                        {
                            lbl_IssueBag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                            objUserDLL = new DLL_DropDown();
                            objUserBEL.Godown_ID = Session["Godown_Id"].ToString();
                            DataSet dts = new DataSet();
                            dts = objUserDLL.CountBagsFromStockStockDetails(objUserBEL);

                            if (dts.Tables[0].Rows[0]["count"].ToString() == "0")
                            {
                                lbl_AvlBag.Text = lbl_IssueBag.Text;
                            }
                            else
                            {
                                int aa = 0;
                                int bb = 0;
                                objUserDLL = new DLL_DropDown();
                                DataSet dss = new DataSet();
                                dss = objUserDLL.fillBagsFromStockStockDetails(objUserBEL);
                                if (dss != null)
                                {
                                    if (dss.Tables[0].Rows.Count > 0)
                                    {
                                        hfavlbag.Value = dss.Tables[0].Rows[0]["Recv_No_Of_Bags"].ToString();

                                        aa = Convert.ToInt32(lbl_IssueBag.Text);
                                        bb = Convert.ToInt32(hfavlbag.Value);
                                        lbl_AvlBag.Text = (aa - bb).ToString();

                                        int inputNumber;
                                        inputNumber = Convert.ToInt32(lbl_AvlBag.Text);

                                        if (inputNumber <= 0)
                                        {
                                            string pop = "alert('No Stock Available !!!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                                            txt_NoOfBag.Enabled = false;
                                            lbl_AvlBag.Text = "0";
                                            return;
                                        }
                                        else
                                        {
                                            txt_NoOfBag.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "06") // OUAT farm
            {
                #region

                objUserBEL = new BLL_DropDown();
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                objUserBEL.LOT_NO = ddl_lotNo.SelectedValue;
                objUserBEL.Crop_Verid = hfCropVerid.Value;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillRecvBagsByLotByOuat(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfTestingdate.Value = ds.Tables[0].Rows[0]["Testing_date"].ToString();
                        hfExpiredate.Value = ds.Tables[0].Rows[0]["Expire_Date"].ToString();

                        DateTime testdate = Convert.ToDateTime(hfTestingdate.Value.ToString());
                        DateTime expdate = Convert.ToDateTime(hfExpiredate.Value.ToString());
                        DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        if (todaydate > expdate)
                        {
                            string pop = "alert('Lot.No Expiry !!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            txt_NoOfBag.Enabled = false;
                            ddl_lotNo.SelectedIndex = 0;
                            ddl_BagSize.SelectedIndex = 0;
                            return;
                        }
                        else
                        {
                            lbl_IssueBag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                            objUserDLL = new DLL_DropDown();
                            objUserBEL.Godown_ID = Session["Godown_Id"].ToString();
                            DataSet dts = new DataSet();
                            dts = objUserDLL.CountBagsFromStockStockDetails(objUserBEL);

                            if (dts.Tables[0].Rows[0]["count"].ToString() == "0")
                            {
                                lbl_AvlBag.Text = lbl_IssueBag.Text;
                            }
                            else
                            {
                                int aa = 0;
                                int bb = 0;
                                objUserDLL = new DLL_DropDown();
                                DataSet dss = new DataSet();
                                dss = objUserDLL.fillBagsFromStockStockDetails(objUserBEL);
                                if (dss != null)
                                {
                                    if (dss.Tables[0].Rows.Count > 0)
                                    {
                                        hfavlbag.Value = dss.Tables[0].Rows[0]["Recv_No_Of_Bags"].ToString();

                                        aa = Convert.ToInt32(lbl_IssueBag.Text);
                                        bb = Convert.ToInt32(hfavlbag.Value);

                                        lbl_AvlBag.Text = (aa - bb).ToString();
                                        int inputNumber;
                                        inputNumber = Convert.ToInt32(lbl_AvlBag.Text);
                                        if (inputNumber <= 0)
                                        {
                                            string pop = "alert('No Stock Available !!!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                                            txt_NoOfBag.Enabled = false;
                                            lbl_AvlBag.Text = "0";
                                            return;
                                        }
                                        else
                                        {
                                            txt_NoOfBag.Enabled = true;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "09") // MOU Agency
            {

                #region

                objUserBEL = new BLL_DropDown();
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                objUserBEL.LOT_NO = ddl_lotNo.SelectedValue;
                objUserBEL.Crop_Verid = hfCropVerid.Value;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillRecvBagsByLotByMOUAgency(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfTestingdate.Value = ds.Tables[0].Rows[0]["Testing_date"].ToString();
                        hfExpiredate.Value = ds.Tables[0].Rows[0]["Expire_Date"].ToString();

                        DateTime testdate = Convert.ToDateTime(hfTestingdate.Value.ToString());
                        DateTime expdate = Convert.ToDateTime(hfExpiredate.Value.ToString());
                        DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        if (todaydate > expdate)
                        {
                            string pop = "alert('Lot.No Expiry !!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            txt_NoOfBag.Enabled = false;
                            ddl_lotNo.SelectedIndex = 0;
                            ddl_BagSize.SelectedIndex = 0;
                            return;
                        }
                        else
                        {
                            lbl_IssueBag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                            objUserDLL = new DLL_DropDown();
                            objUserBEL.Godown_ID = Session["Godown_Id"].ToString();
                            DataSet dts = new DataSet();
                            dts = objUserDLL.CountBagsFromStockStockDetails(objUserBEL);

                            if (dts.Tables[0].Rows[0]["count"].ToString() == "0")
                            {
                                lbl_AvlBag.Text = lbl_IssueBag.Text;
                            }
                            else
                            {
                                int aa = 0;
                                int bb = 0;
                                objUserDLL = new DLL_DropDown();
                                DataSet dss = new DataSet();
                                dss = objUserDLL.fillBagsFromStockStockDetails(objUserBEL);
                                if (dss != null)
                                {
                                    if (dss.Tables[0].Rows.Count > 0)
                                    {
                                        hfavlbag.Value = dss.Tables[0].Rows[0]["Recv_No_Of_Bags"].ToString();

                                        aa = Convert.ToInt32(lbl_IssueBag.Text);
                                        bb = Convert.ToInt32(hfavlbag.Value);

                                        lbl_AvlBag.Text = (aa - bb).ToString();
                                        int inputNumber;
                                        inputNumber = Convert.ToInt32(lbl_AvlBag.Text);
                                        if (inputNumber <= 0)
                                        {
                                            string pop = "alert('No Stock Available !!!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                                            txt_NoOfBag.Enabled = false;
                                            lbl_AvlBag.Text = "0";
                                            return;
                                        }
                                        else
                                        {
                                            txt_NoOfBag.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            else if (rbtn_RecvedFrom.SelectedValue == "10") // scheme
            {
                #region

                objUserBEL = new BLL_DropDown();
                objUserBEL.REF_NO = ddl_RecvFrom.SelectedValue;
                objUserBEL.CROP_ID = ddl_CropName.SelectedValue;
                objUserBEL.CropClass = CropClass.ToString();
                objUserBEL.LOT_NO = ddl_lotNo.SelectedValue;
                objUserBEL.Crop_Verid = hfCropVerid.Value;
                objUserDLL = new DLL_DropDown();
                ds = new DataSet();
                ds = objUserDLL.FillRecvBagsByLotByScheme(objUserBEL);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hfTestingdate.Value = ds.Tables[0].Rows[0]["Testing_date"].ToString();
                        hfExpiredate.Value = ds.Tables[0].Rows[0]["Expire_Date"].ToString();

                        DateTime testdate = Convert.ToDateTime(hfTestingdate.Value.ToString());
                        DateTime expdate = Convert.ToDateTime(hfExpiredate.Value.ToString());
                        DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        if (todaydate > expdate)
                        {
                            string pop = "alert('Lot.No Expiry !!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                            txt_NoOfBag.Enabled = false;
                            ddl_lotNo.SelectedIndex = 0;
                            ddl_BagSize.SelectedIndex = 0;

                            return;
                        }
                        else
                        {
                            lbl_IssueBag.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                            objUserDLL = new DLL_DropDown();
                            objUserBEL.Godown_ID = Session["Godown_Id"].ToString();
                            DataSet dts = new DataSet();
                            dts = objUserDLL.CountBagsFromStockStockDetails(objUserBEL);

                            if (dts.Tables[0].Rows[0]["count"].ToString() == "0")
                            {
                                lbl_AvlBag.Text = lbl_IssueBag.Text;
                            }
                            else
                            {
                                int aa = 0;
                                int bb = 0;
                                objUserDLL = new DLL_DropDown();
                                DataSet dss = new DataSet();
                                dss = objUserDLL.fillBagsFromStockStockDetails(objUserBEL);
                                if (dss != null)
                                {
                                    if (dss.Tables[0].Rows.Count > 0)
                                    {
                                        hfavlbag.Value = dss.Tables[0].Rows[0]["Recv_No_Of_Bags"].ToString();

                                        aa = Convert.ToInt32(lbl_IssueBag.Text);
                                        bb = Convert.ToInt32(hfavlbag.Value);

                                        lbl_AvlBag.Text = (aa - bb).ToString();
                                        int inputNumber;
                                        inputNumber = Convert.ToInt32(lbl_AvlBag.Text);
                                        if (inputNumber <= 0)
                                        {
                                            string pop = "alert('No Stock Available !!!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                                            txt_NoOfBag.Enabled = false;
                                            lbl_AvlBag.Text = "0";
                                            return;
                                        }
                                        else
                                        {
                                            txt_NoOfBag.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
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
    // Kharif 2017
    protected void txt_testingDate_TextChanged(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = true;
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txt_testingDate = (TextBox)row.FindControl("txt_testingDate");
        try
        {
            Label lbl_ExpiryDate = (Label)row.FindControl("lbl_ExpiryDate");
            HiddenField hfTestDate = (HiddenField)row.FindControl("hfTestDate");
            HiddenField hfExpDate = (HiddenField)row.FindControl("hfExpDate");
            Label lbl_AvlBag = (Label)row.FindControl("lbl_AvlBag");

            if (txt_testingDate.Text == "")
            {
                string pop = "alert('Please enter the Date !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txt_testingDate);
                txt_testingDate.Text = "";
                return;
            }
            else
            {
                //this.Page.MaintainScrollPositionOnPostBack = true;
                //GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
                //TextBox txt_testingDate = (TextBox)row.FindControl("txt_testingDate");
                //Label lbl_ExpiryDate = (Label)row.FindControl("lbl_ExpiryDate");
                //Label lbl_AvlBag = (Label)row.FindControl("lbl_AvlBag");

                string a = txt_testingDate.Text;
                lbl_ExpiryDate.Text = Convert.ToDateTime(a).AddMonths(9).ToString("dd/MM/yyyy");
                hfTestDate.Value = Convert.ToDateTime(txt_testingDate.Text).ToString("yyyy-MM-dd");
                hfExpDate.Value = Convert.ToDateTime(a).AddMonths(9).ToString("yyyy-MM-dd");
                lbl_AvlBag.Visible = false;
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
            txt_testingDate.Text = string.Empty;
            Exception exception = exception1;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "asyncPostBack", "alert('Your request could not be completed due to exception. Please intimate technical team for rectification!');", true);
        }
    }

    public void SendReceiveDetails(cReceiveDetails objUserBEL1)
    {
        //FarmerDetails fdetails = null;
        try
        {
            //string apiUrl = "http://10.172.31.57:3000/addStockbySIS";
            string apiUrl = "http://164.100.140.107/addStockbySIS";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            var json = JsonConvert.SerializeObject(objUserBEL1);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsync(apiUrl, stringContent).Result;

            //var json = JsonConvert.SerializeObject(fdList);
            //var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            ////var client = new HttpClient();
            //var response = await client.PostAsync(uri, stringContent);



            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                string pop1 = "alert('" + responseString + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);
                // Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                //string s = JsonConvert.SerializeObject(responseString);
                //DataSet ds1 =(DataSet) JsonConvert.DeserializeObject(s);
                // fdetails = JsonConvert.DeserializeObject<FarmerDetails>(responseString);
            }
            // DataSet ds = (new JavaScriptSerializer()).Deserialize<DataSet>(response.Content.ReadAsStringAsync().Result);

        }
        catch (Exception ex)
        {
            //string pop1 = "alert('Oops!!!');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "pop1", pop1, true);

        }
        //return fdetails;
    }

    public class cReceiveDetails
    {
        public string dist_Code { get; set; }
        public string lot_Number { get; set; }
        public string receiver_ID { get; set; }
        public string qty_Per_Bag_Kg { get; set; }
        public string no_of_Bag { get; set; }
        public string date_Intake { get; set; }
        public string year { get; set; }
        public string season { get; set; }
        public string crop { get; set; }
        public string variety { get; set; }
        public string Class { get; set; }
        public string pr_Number { get; set; }
        public string SourceType { get; set; }
        public string receiverType { get; set; }
        public string agencyName { get; set; }
        public string UserID { get; set; }
        public string UserIP { get; set; }
        public string CropCatg_ID { get; set; }
        public string source_ID { get; set; }

    }
}
    


    
        

        
    

  

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ReSetPassword : System.Web.UI.Page
{
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearAll();
            FillUserType();
        }
    }
    private void FillUserType()
    {
        ddlUserType.Items.Clear();
        ds = new DataSet();
        DAL = new DLL_DropDown();
        ds = DAL.FillUserType();
        ddlUserType.DataSource = ds.Tables[0];
        ddlUserType.DataTextField = "USER_TYPE";
        ddlUserType.DataValueField = "USER_TYPE";
        ddlUserType.DataBind();
        this.ddlUserType.Items.Insert(0, "--Select--");  
    }
    private void ClearAll()
    {
        lblUserID.Text = "";
        lblUserID1.Text = "";
        lblUserType1.Text = "";
        lblPassword.Text = "";
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        ddlUser.Items.Clear();
        ds = new DataSet();
        DAL = new DLL_DropDown();
        BAL = new BLL_DropDown();
        BAL.UserType = ddlUserType.SelectedValue.ToString();
        ds = DAL.FillUserUserByType(BAL);
        ddlUser.DataSource = ds.Tables[0];
        ddlUser.DataTextField = "UserID";
        ddlUser.DataValueField = "UserID";
        ddlUser.DataBind();
        this.ddlUser.Items.Insert(0, "--Select--");
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        ds = new DataSet();
        DAL = new DLL_DropDown();
        BAL = new BLL_DropDown();
        BAL.UserType = ddlUserType.SelectedValue.ToString();
        BAL.UserID = ddlUser.SelectedValue.ToString();
        ds = DAL.FillUserUserByIDType(BAL);
        DataTable dt = ds.Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            lblUserID1.Text = dr["UserID"].ToString();
            lblUserType1.Text = dr["USER_TYPE"].ToString();
            lblUserID.Text = dr["UserID"].ToString() + "(" + dr["FullName"].ToString() + ")";
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        BAL = new BLL_DropDown();
        BAL.UserID = lblUserID1.Text;
        BAL.UserType = lblUserType1.Text;
        DAL = new DLL_DropDown();
        int Val = DAL.ReSetPassword(BAL);
        if (Val == 1)
        {
            string popp = "alert('Password Has Been Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            lblPassword.Text = "Test@1234";
        }
    }
}
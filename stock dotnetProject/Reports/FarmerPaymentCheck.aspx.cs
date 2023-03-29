using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FarmerPaymentCheck : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["Name"] != null || Session["UserType"] != null)
        {
            if (this.Session["UserType"].ToString() == "ADMI")
            {
                this.Page.MasterPageFile = "../MasterPages/Admin.master";
                return;
            }
            if (this.Session["UserType"].ToString() == "AAOO")
            {
                this.Page.MasterPageFile = "../MasterPages/AAO_.master";
                return;
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.FARMER_ID = txtFarmerId.Text;
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetFarmerpmtDtls(objUserBELDIST);
        gvDetails.DataSource = ds;
        gvDetails.DataBind();
    }
}
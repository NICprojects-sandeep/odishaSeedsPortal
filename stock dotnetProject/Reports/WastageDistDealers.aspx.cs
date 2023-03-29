using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_WastageDistDealers : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string FIN_YR = Request.QueryString["FIN_YR"];
            string SEASSION_NAME = Request.QueryString["SEASSION_NAME"];
            FillData(FIN_YR, SEASSION_NAME);
        }
    }
    private void FillData(string FIN_YR,string SEASSION_NAME)
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.FIN_YR = FIN_YR;
        objUserBEL.SEASON = SEASSION_NAME;
        objUserDLL = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLL.FillSeedWastageDealers(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
        }

    }
}
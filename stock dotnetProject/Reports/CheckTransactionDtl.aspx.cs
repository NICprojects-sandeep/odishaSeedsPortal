using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CheckTransactionDtl : System.Web.UI.Page
{
    DataSet ds;
    BLL_DropDown objUserBELDIST;
    DLL_DropDown objUserDLLDIST;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        objUserBELDIST = new BLL_DropDown();
        objUserBELDIST.TRANSACTIONID = txtTransactionID.Text;
        objUserDLLDIST = new DLL_DropDown();
        ds = new DataSet();
        ds = objUserDLLDIST.CheckTransactionDtls(objUserBELDIST);
        int cnt= Convert.ToInt32(ds.Tables[0].Rows[0]["CNT"]);
        
        if(cnt==1)
        {
            gvTransactionDtls.DataSource = ds.Tables[1];
            gvTransactionDtls.DataBind();
        }
        else
        {
            gvTransactionDtls.DataSource = null;
            gvTransactionDtls.DataBind();
        }
        
    }
}
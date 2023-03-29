using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_UpdatedList : System.Web.UI.Page
{
    string _connstrSeed = ConfigurationManager.ConnectionStrings["SqlConSeed"].ConnectionString;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ds = FillDealers();
            gvList.DataSource = ds;
            gvList.DataBind();
        }
    }
    public DataSet FillDealers()
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("SELECT A.LIC_NO, A.ACC_HOLDERNAME, A.ACC_NO,A.BANK_ID,B.vchBankName,B.vchBranchName, A.BRANCH_ID, A.IFSC_CODE, A.UPDATED_ON, A.UPDATED_BY FROM BANKDETAILS_LOG A " +
"INNER JOIN SEED_LIC_DIST C ON A.LIC_NO = C.LIC_NO "+
"INNER JOIN [FARMERDB].[DBO].[M_PDS_BANKBRANCH] B ON CAST(A.BRANCH_ID AS BIGINT)  = CAST(B.intBranchId AS BIGINT) "+
"WHERE C.DIST_CODE = @DIST_CODE AND A.UPDATED_ON>'2020-08-24' ORDER BY LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", Session["DistCode"].ToString());
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            return ds;
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
}
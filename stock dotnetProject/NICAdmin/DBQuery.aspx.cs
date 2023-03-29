using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NICAdmin_DBQuery : System.Web.UI.Page
{
    string _connstrSeed = ConfigurationManager.ConnectionStrings["SqlConSeed"].ConnectionString;
    string _connstrStock = ConfigurationManager.ConnectionStrings["stockConnectionString"].ConnectionString;
    string _connstrFarmer = ConfigurationManager.ConnectionStrings["FarmerDBConnectionString"].ConnectionString;
    DataSet ds;
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDBs();
        }
    }

    private void FillDBs()
    {
        ddlDB.Items.Add(new ListItem("dafpSEED", "1"));
        ddlDB.Items.Add(new ListItem("STOCK", "2"));
        ddlDB.Items.Add(new ListItem("FARMERDB", "3"));
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        
        if (ddlDB.SelectedValue == "1")
        {
            con = new SqlConnection(_connstrSeed);
        }
        else if (ddlDB.SelectedValue == "2")
        {
            con = new SqlConnection(_connstrStock);
        }
        else if (ddlDB.SelectedValue == "3")
        {
            con = new SqlConnection(_connstrFarmer);
        }

        string Qry = txtQry.Text;
        SqlCommand cmd = new SqlCommand(Qry, con);
        cmd.CommandType = CommandType.Text;
        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            gvResult.DataSource = ds;
            gvResult.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_Aao_Adhar_Updation : System.Web.UI.Page
{
    SqlCommand cm = new SqlCommand();
    SqlConnection cn = new SqlConnection();

    DataSet ds = new DataSet();
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(_connstrStock);
            con.Open();

            SqlCommand cmd = new SqlCommand("GET_AAO_FARMER_DETAILS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@farmerid", SqlDbType.VarChar).Value = txtfarmerid.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["ADHARSTATUS"].ToString() == "V")
                {
                    txtfarmerid.Text = "";
                    Response.Write("<script>alert('Aaddhar No.is already exist no need for updation.')</script>");
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["ADHARSTATUS"].ToString() == "U")
                    {
                        txtfarmerid.Text = "";
                        Response.Write("<script>alert('Aaddhar No.is already updated .')</script>");
                    }
                    else
                    {
                        txtfarmername.Text = ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                        txtfathername.Text = ds.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                        //txtdbo.Text = ds.Tables[0].Rows[0]["DTMDOB"].ToString()==""?"":Convert.ToDateTime(ds.Tables[0].Rows[0]["DTMDOB"]).ToShortDateString();
                        txtadharno.Text = ds.Tables[0].Rows[0]["VCHAADHARNO"].ToString();
                        ViewState["aadharno"] = ds.Tables[0].Rows[0]["VCHAADHARNO"];
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('No data found.')</script>");
            }
        }

        catch (Exception ex)
        {
            throw ex;

        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(_connstrStock);
            con.Open();

            if (txtadharno.Text.Length == 12)
            {
                SqlCommand cmd = new SqlCommand("NIC_AAO_FARMER_Adhar_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@adharno", SqlDbType.VarChar).Value = txtadharno.Text;
                cmd.Parameters.Add("@farmerid", SqlDbType.VarChar).Value = txtfarmerid.Text;

                cmd.Parameters.Add("@adharno1", SqlDbType.VarChar).Value = ViewState["aadharno"].ToString();
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = Request.UserHostAddress.ToString();
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Write("<script>alert('Duplicate Aadhar Number')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Data updated successfully')</script>");
                }
                con.Close();
                //Response.Write("<script>alert('Data updated successfully')</script>");
                btnupdate.Enabled = false;
                txtfarmerid.Text = "";
                txtfarmername.Text = "";
                txtfathername.Text = "";
                txtadharno.Text = "";
                //txtdbo.Text = "";
                ViewState["aadharno"] = "";
                ViewState["dtmdob"] = "";
                ViewState["farmername"] = "";
                ViewState["fathername"] = "";

            }
            else
            {
                Response.Write("<script>alert('AADHAAR No. should be 12 digit')</script>");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClear1_Click(object sender, EventArgs e)
    {
        txtfarmerid.Text = "";
        txtfarmername.Text = "";
        txtfathername.Text = "";
        txtadharno.Text = "";
        //txtdbo.Text = "";
    }
}
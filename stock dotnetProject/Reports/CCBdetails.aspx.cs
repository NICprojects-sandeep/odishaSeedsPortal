using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

public partial class CCBdetails : System.Web.UI.Page
{
    private UtilityLibrary utl = new UtilityLibrary();
    SqlConnection CNfar = new SqlConnection(ConfigurationManager.ConnectionStrings["FarmerDBConnectionString"].ToString());
    SqlConnection stck = new SqlConnection(ConfigurationManager.ConnectionStrings["stockConnectionString"].ToString());
    cls_ossopca obj = new cls_ossopca();
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
            if (!(this.Session["UserType"].ToString() == "ADMI") && !(this.Session["UserType"].ToString() == "SEAD") || !(this.Session["FirstLogin"].ToString() == "N"))
            {
                base.Response.Redirect("../Unauthorised.aspx", true);
                return;
            }

            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Cache-Control", "Private,no-cache,must-revalidate,no-store");
            try
            {

                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Cache-Control", "Private,no-cache,must-revalidate,no-store");
            }

            catch (Exception ee)
            {
                Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message, false);
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //this.FirstLogin = this.Session["FirstLogin"].ToString();

        if (this.Session["UserType"].ToString() == "ADMI")
        {
            this.Page.MasterPageFile = "../MasterPages/Admin.master";
            return;
        }
        if (this.Session["UserType"].ToString() == "SEAD")
        {
            this.Page.MasterPageFile = "../MasterPages/SeedAdmin.master";
            return;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        cls_ossopca obj = new cls_ossopca();
        string[] Control = { };
        if (obj.ValidFunction(Control, 25, 0))
        {
            // Tab1.ActiveTabIndex = 0;
            Session["CPAID"] = txtRef.Text;
            binddetails(Session["CPAID"].ToString());
            
        }
        else
        {
           
        }
    }
    protected void binddetails(string batchid)
    {
        try
        {
            string[] Cont = { batchid };
            if (batchid !=null)
            {
                stck.Open();
                SqlCommand CM = new SqlCommand("select distinct  a.Batch_ID, b.Unique_Credit_Transaction_Id,b.Total_Payment_Amount,c.TRANSACTION_ID,c.UPDATED_ON,d.VCHFARMERNAME as Beneficiary_Name ,c.FARMER_ID,c.Crop_Name,c.Variety_Name,c.TOT_QTL,c.DEALER_CODE,c.UPDATED_BY from [FARMERDB].[dbo].[Request_tbl_Payment_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Message_Id=b.Unique_Message_Id inner join  [FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id,CHARINDEX('O',Unique_Credit_Transaction_Id)-1) COLLATE Latin1_General_CI_AS = c.TRANSACTION_ID inner join [FARMERDB].[dbo].M_FARMER_REGISTRATION d on c.FARMER_ID=d.NICFARMERID collate Latin1_General_CI_AS  where Batch_ID=@cpaid and c.UPDATED_BY=@dlrid and((a.Scheme_Code=c.SCHEME_CODE_GOI COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_GOI !=0) or(a.Scheme_Code=c.SCHEME_CODE_SP COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_SP !=0))", stck);
                CM.CommandType = CommandType.Text;
                CM.Parameters.AddWithValue("@cpaid", batchid);
                CM.Parameters.AddWithValue("@dlrid", drpDealer.SelectedValue);
                SqlDataAdapter ad = new SqlDataAdapter(CM);
                DataTable Dt = new DataTable();
                ad.Fill(Dt);
                CNfar.Close();
                Grd_optsumm.DataSource = Dt;
                Grd_optsumm.DataBind();
                decimal total = Dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Payment_Amount"));
                decimal totalqtl = Dt.AsEnumerable().Sum(row => row.Field<decimal>("TOT_QTL"));
                Grd_optsumm.FooterRow.Cells[6].Text = "<b>Total<b>";               
                Grd_optsumm.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                Grd_optsumm.FooterRow.Cells[7].Text = "<b> " + totalqtl.ToString("N1") + "<b>";
                Grd_optsumm.FooterRow.Cells[8].Text = "<b> " + total.ToString("N2") + "<b>";
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Invalid Request')", true);
            }
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }

    protected void ddl_TextChanged(object sender, EventArgs e)
    {        
        try
        {
            //txtRef.Text = "";
            if (txtRef.Text !=null)
            {

                stck.Open();
                SqlCommand CM = new SqlCommand("select distinct  c.DEALER_CODE + '(' + c.UPDATED_BY + ')' as DL_CODE , a.Unique_Message_Id,c.UPDATED_BY from [FARMERDB].[dbo].[Request_tbl_Payment_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Message_Id=b.Unique_Message_Id  inner join  [FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id,CHARINDEX('O', Unique_Credit_Transaction_Id)-1) COLLATE Latin1_General_CI_AS = c.TRANSACTION_ID  WHERE a.Batch_ID=@cpaid", stck);
                //, b.Unique_Credit_Transaction_Id,c.*
                CM.CommandType = CommandType.Text;
                CM.Parameters.AddWithValue("@cpaid", txtRef.Text);

                SqlDataAdapter ad = new SqlDataAdapter(CM);
                DataTable Dt = new DataTable();
                ad.Fill(Dt);

                stck.Close();

                drpDealer.DataSource = Dt;
                drpDealer.DataTextField = "DL_CODE";
                drpDealer.DataValueField = "UPDATED_BY";
                drpDealer.DataBind();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Invalid Request')", true);
            }
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }

    
}
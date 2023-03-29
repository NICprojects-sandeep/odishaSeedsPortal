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

public partial class Reports_PFMSdetails1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public static DataTable GetData(string query)
    {
        string stock = ConfigurationManager.ConnectionStrings["stockConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(stock))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.CommandTimeout = 0;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    decimal totqtl = 0;
    decimal totamt = 0;
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtt = new DataTable();

            string dealerId = gvpfms.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvtrans = e.Row.FindControl("gvtrans") as GridView;
            Label dlrcode = e.Row.FindControl("lbldlrcode") as Label;
            //dtt = GetData(string.Format("select distinct  a.Batch_ID, b.Unique_Credit_Transaction_Id,b.Total_Payment_Amount,c.TRANSACTION_ID,c.UPDATED_ON,d.VCHFARMERNAME as Beneficiary_Name ,c.FARMER_ID,c.Crop_Name,c.Variety_Name,c.TOT_QTL,c.DEALER_CODE,c.UPDATED_BY from [FARMERDB].[dbo].[Request_tbl_Payment_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Message_Id=b.Unique_Message_Id inner join  [FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id,CHARINDEX('O',Unique_Credit_Transaction_Id)-1) COLLATE Latin1_General_CI_AS = c.TRANSACTION_ID inner join [FARMERDB].[dbo].M_FARMER_REGISTRATION d on c.FARMER_ID=d.NICFARMERID collate Latin1_General_CI_AS  where Batch_ID='" + txtSearch.Text + "' and((a.Scheme_Code=c.SCHEME_CODE_GOI COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_GOI !=0) or(a.Scheme_Code=c.SCHEME_CODE_SP COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_SP !=0)) and dealer_code='{0}'", dlrcode.Text));
            //gvtrans.DataSource = dtt;
            dtt = GetData(string.Format("select distinct  a.Batch_ID, b.Unique_Credit_Transaction_Id,b.Total_Payment_Amount,c.TRANSACTION_ID,c.UPDATED_ON,d.VCHFARMERNAME as "+                "Beneficiary_Name ,c.FARMER_ID,c.Crop_Name,c.Variety_Name,c.TOT_QTL,c.DEALER_CODE,c.UPDATED_BY from (SELECT Batch_ID,Unique_Message_Id,Scheme_Code FROM[FARMERDB].[dbo].[Request_tbl_Payment_Message] "+ 
            "UNION ALL SELECT Batch_ID, Unique_Message_Id, Scheme_Code FROM[FARMERDB].[dbo].[Request_tbl_Payment_Message_Rabi] "+ 
            ") a inner join(SELECT Unique_Credit_Transaction_Id,Total_Payment_Amount,Unique_Message_Id FROM [FARMERDB].[dbo].[Request_tbl_Payment_List] "+ 
			"UNION ALL SELECT Unique_Credit_Transaction_Id,Total_Payment_Amount,Unique_Message_Id FROM [FARMERDB].[dbo].[Request_tbl_Payment_List_Rabi] "+ 
			") b on a.Unique_Message_Id = b.Unique_Message_Id inner join[FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id, CHARINDEX('O', Unique_Credit_Transaction_Id) - 1) COLLATE "+ "Latin1_General_CI_AS = c.TRANSACTION_ID inner join[FARMERDB].[dbo].M_FARMER_REGISTRATION d on c.FARMER_ID = d.NICFARMERID collate Latin1_General_CI_AS  where Batch_ID = '" + txtSearch.Text + "' and((a.Scheme_Code = c.SCHEME_CODE_GOI COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_GOI != 0) or(a.Scheme_Code = c.SCHEME_CODE_SP COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_SP != 0)) and dealer_code = '{0}'", dlrcode.Text));
            gvtrans.DataSource = dtt;
            gvtrans.DataSource = dtt;
            gvtrans.DataBind();
            decimal total = dtt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Payment_Amount"));
            decimal totalqtl = dtt.AsEnumerable().Sum(row => row.Field<decimal>("TOT_QTL"));
            gvtrans.FooterRow.Cells[6].Text = "<b>Total<b>";
            gvtrans.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvtrans.FooterRow.Cells[7].Text = "<b> " + totalqtl.ToString("N2") + "<b>";
            gvtrans.FooterRow.Cells[8].Text = "<b> " + total.ToString("N2") + "<b>";
            dtt.Clear();
        }

    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["stockConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                //string sql = "select distinct c.DEALER_CODE,UPPER(b.Beneficiary_Name) as DealerName from [FARMERDB].[dbo].[Request_tbl_Payment_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Message_Id=b.Unique_Message_Id inner join  [FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id,CHARINDEX('O',Unique_Credit_Transaction_Id)-1) COLLATE Latin1_General_CI_AS = c.TRANSACTION_ID inner join [FARMERDB].[dbo].M_FARMER_REGISTRATION d on c.FARMER_ID=d.NICFARMERID collate Latin1_General_CI_AS  where Batch_ID=@cpaid and((a.Scheme_Code=c.SCHEME_CODE_GOI COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_GOI !=0) or(a.Scheme_Code=c.SCHEME_CODE_SP COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_SP !=0))";                
                string sql = "select distinct c.DEALER_CODE,UPPER(b.Beneficiary_Name) as DealerName from (SELECT Unique_Message_Id,Scheme_Code,Batch_ID FROM [FARMERDB].[dbo].[Request_tbl_Payment_Message] " +
                "UNION ALL SELECT Unique_Message_Id, Scheme_Code, Batch_ID FROM[FARMERDB].[dbo].[Request_tbl_Payment_Message_Rabi] " +
                ") a inner join(SELECT Beneficiary_Name, Unique_Message_Id, Unique_Credit_Transaction_Id FROM[FARMERDB].[dbo].[Request_tbl_Payment_List] " +
                "UNION ALL SELECT Beneficiary_Name, Unique_Message_Id, Unique_Credit_Transaction_Id FROM[FARMERDB].[dbo].[Request_tbl_Payment_List_Rabi] " +
                ") b on a.Unique_Message_Id = b.Unique_Message_Id inner join[FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id, CHARINDEX('O', Unique_Credit_Transaction_Id) - 1) COLLATE Latin1_General_CI_AS = c.TRANSACTION_ID inner join[FARMERDB].[dbo].M_FARMER_REGISTRATION d on c.FARMER_ID = d.NICFARMERID collate Latin1_General_CI_AS  where Batch_ID = @cpaid and((a.Scheme_Code = c.SCHEME_CODE_GOI COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_GOI != 0) or(a.Scheme_Code = c.SCHEME_CODE_SP COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_SP != 0))";
                cmd.Parameters.AddWithValue("@cpaid", txtSearch.Text.Trim());
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvpfms.DataSource = dt;
                    gvpfms.DataBind();
                }
            }
        }
    }
    protected void gvtrans_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            
            LinkButton lnkView = (LinkButton)e.CommandSource;
            //Label lblTrnsId = (Label)row;
            Session["TransId"] = lnkView.Text;

            string popicon = "window.open('Rpt_TransDtls.aspx','Report','width=1020,height=900,toolbar=1,resizable=1,location=0, status=0, menubar=0, scrollbars=1');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popicon", popicon, true);
        }
    }
}
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
using System.IO;
using System.Drawing;

public partial class PFMSdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnExport.Visible=false ;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //Allows for printing
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
            dtt = GetData(string.Format("select distinct  a.Batch_ID, b.Unique_Credit_Transaction_Id,b.Total_Payment_Amount,c.TRANSACTION_ID,c.UPDATED_ON,d.VCHFARMERNAME as Beneficiary_Name ,c.FARMER_ID,c.Crop_Name,c.Variety_Name,c.TOT_QTL,c.DEALER_CODE,c.UPDATED_BY from [FARMERDB].[dbo].[Request_tbl_Payment_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Message_Id=b.Unique_Message_Id inner join  [FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id,CHARINDEX('O',Unique_Credit_Transaction_Id)-1) COLLATE Latin1_General_CI_AS = c.TRANSACTION_ID inner join [FARMERDB].[dbo].M_FARMER_REGISTRATION d on c.FARMER_ID=d.NICFARMERID collate Latin1_General_CI_AS  where Batch_ID='" + txtSearch.Text + "' and((a.Scheme_Code=c.SCHEME_CODE_GOI COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_GOI !=0) or(a.Scheme_Code=c.SCHEME_CODE_SP COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_SP !=0)) and dealer_code='{0}'", dlrcode.Text));
            gvtrans.DataSource = dtt;   
            gvtrans.DataBind();
            decimal total = dtt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Payment_Amount"));
            decimal totalqtl = dtt.AsEnumerable().Sum(row => row.Field<decimal>("TOT_QTL"));
            gvtrans.FooterRow.Cells[6].Text = "<b>Total<b>";
            gvtrans.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvtrans.FooterRow.Cells[7].Text = "<b> " + totalqtl.ToString("N1") + "<b>";
            gvtrans.FooterRow.Cells[8].Text = "<b> " + total.ToString("N2") + "<b>";
            dtt.Clear();
            
        }
        
    }

    protected void btnsrch_Click(object sender, EventArgs e)
    {
        paintGrid();
    }

    protected void paintGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["stockConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string sql = "select distinct c.DEALER_CODE,UPPER(b.Beneficiary_Name) as DealerName from [FARMERDB].[dbo].[Request_tbl_Payment_Message] a inner join [FARMERDB].[dbo].[Request_tbl_Payment_List] b on a.Unique_Message_Id=b.Unique_Message_Id inner join  [FARMERDB].[dbo].[VW_STOCKTRANSACTIONS] c on  left(Unique_Credit_Transaction_Id,CHARINDEX('O',Unique_Credit_Transaction_Id)-1) COLLATE Latin1_General_CI_AS = c.TRANSACTION_ID inner join [FARMERDB].[dbo].M_FARMER_REGISTRATION d on c.FARMER_ID=d.NICFARMERID collate Latin1_General_CI_AS  where Batch_ID=@cpaid and((a.Scheme_Code=c.SCHEME_CODE_GOI COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_GOI !=0) or(a.Scheme_Code=c.SCHEME_CODE_SP COLLATE Latin1_General_CI_AI and c.TOT_SUB_AMOUNT_SP !=0))";
                cmd.Parameters.AddWithValue("@cpaid", txtSearch.Text.Trim());
                cmd.CommandText = sql;
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);                    
                    gvpfms.DataSource = dt;
                    gvpfms.DataBind();
                    btnExport.Visible = true;
                }
            }
        }
    }

    //protected void ExportToExcel(object sender, EventArgs e)
    protected void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvpfms.AllowPaging = false;
            paintGrid();
            gvpfms.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvpfms.HeaderRow.Cells)
            {
                cell.BackColor = gvpfms.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvpfms.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvpfms.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvpfms.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            gvpfms.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }


    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }
   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Masters_Rpt_Dealer_Stock : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
        if (!IsPostBack)
        {
            FillDealers();
            pnlPrintArea.Visible = false;
            btnPrint.Visible = false;
        }
    }
    private void FillDealers()
    {
        if (Session["distcode"] != null && Session["UserType"] != null)
        {
            string[] param = { "@DIST_CD", "@COMP_NAME" };
            object[] val = { Session["distcode"].ToString(), Session["UserType"].ToString() };
            ds = dbsAppStock.param_passing_fetchSeed("SELECT A.LIC_NO + '/DA & FP(O) - ' + A.APP_FIRMNAME AS 'Dealer',REF_NO,LIC_NO FROM SEED_LIC_DIST A " +
            "LEFT OUTER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
            "WHERE A.DIST_CODE = @DIST_CD AND A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND GETDATE()< = A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = @COMP_NAME " +
            "GROUP BY APP_FIRMNAME,REF_NO,LIC_NO ORDER BY A.LIC_NO ", param, val);

            ddlDealer.DataSource = ds;
            ddlDealer.DataTextField = "Dealer";
            ddlDealer.DataValueField = "LIC_NO";
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, "--Select--");
        }
        else
        {
            Response.Redirect("../DBTHome.aspx");
        }
    }
    protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lotdtls = "SELECT B.Category_Name,C.CROP_NAME,D.Variety_Name,A.CLASS,E.RECEIVE_UNITNAME,A.LOT_NO,A.BAG_SIZE_IN_KG,A.AVL_QUANTITY,ISNULL(CAST((A.AVL_QUANTITY*100 / A.BAG_SIZE_IN_KG) AS INT),0) AS STOCK_NOOFBAGS,A.PRICE_QTL,ISNULL(CAST((A.AVL_QUANTITY * A.PRICE_QTL) AS DECIMAL(18,2)),0) AS TOTAL_AMOUNT " +
        "FROM STOCK_DEALERSTOCK A " +
        "LEFT OUTER JOIN mCropCategory B ON A.CROPCATG_ID = B.Category_Code " +
        "LEFT OUTER JOIN mCrop C ON A.CROP_ID = C.Crop_Code " +
        "LEFT OUTER JOIN mCropVariety D ON A.CROP_VERID = D.Variety_Code " +
        "LEFT OUTER JOIN STOCK_RECEIVE_UNIT_MASTER E ON A.RECEIVE_UNITCD = E.RECEIVE_UNITCD " +
        "WHERE A.LICENCE_NO = @LIC_NO ORDER BY C.CROP_NAME,D.Variety_Name,A.CLASS,E.RECEIVE_UNITNAME,A.LOT_NO";

        string[] param = { "@LIC_NO" };
        object[] val = { ddlDealer.SelectedValue };

        string SqlSel = "SELECT LIC_NO + '/DA & FP(O)' AS LIC_NO,APP_FIRMNAME FROM SEED_LIC_DIST WHERE LIC_NO = @LIC_NO";
        ds1 = dbsAppStock.param_passing_fetchSeed(SqlSel, param, val);
        if (ds1 != null)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblLicNo.Text = ds1.Tables[0].Rows[0]["LIC_NO"].ToString();
                lblName.Text = ds1.Tables[0].Rows[0]["APP_FIRMNAME"].ToString();
                ds = dbsAppStock.param_passing_fetch(lotdtls, param, val);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        pnlPrintArea.Visible = true;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        pnlPrintArea.Visible = false;
                        btnPrint.Visible = false;
                    }
                }
                grd_stockdtls.DataSource = ds;
                grd_stockdtls.DataBind();

                decimal total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AVL_QUANTITY"));
                decimal total1 = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TOTAL_AMOUNT"));
                grd_stockdtls.FooterRow.Cells[1].Text = "Total";
                grd_stockdtls.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                grd_stockdtls.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                grd_stockdtls.FooterRow.Cells[8].Text = total.ToString();
                grd_stockdtls.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Center;
                grd_stockdtls.FooterRow.Cells[10].Text = total1.ToString();
            }
        }
    }
}
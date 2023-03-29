using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Dealer_RptCurrentStock : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    protected void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string cach = "no-cache,must-revalidate,no-store";
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", cach);

            if (Session["SRC_AuthToken"] != null)
            {
                if (Session["SRC_AuthToken"].ToString() != Request.Cookies["SRC_AuthToken"].Value.ToString())
                {
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                    Response.Redirect("~/Seed/District/LogIn.aspx");
                }
            }
        }
        catch
        {
            Response.Redirect("~/LogIn.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["LIC_NO"] != null)
            {

                lblReportAsOn.Text = DateTime.Now.ToString("dd/MM/yyyy");
                FillCategory();
                btnPrint.Visible = false;
            }
        }
    }
    private void FillCategory()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.RptCurStock(objUserBELDIST);
        //string[] Param = { "@LIC_NO" };
        //string[] Value = { Session["LIC_NO"].ToString() };
        //ds = dbsApp.param_passing_fetchStock("SELECT D.Category_Code,D.Category_Name FROM Stock_ReceiveDealer A " +
        //"LEFT OUTER JOIN mCropVariety B ON A.CROP_VARIETY_CODE = B.Variety_Code " +
        //"LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
        //"LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
        //"INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
        //"INNER JOIN mSEASSION F ON A.SEASSION_NAME = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
        //"WHERE LIC_NO = @LIC_NO GROUP BY D.Category_Code,D.Category_Name", Param, Value);
        gvCropCat.DataSource = ds;
        gvCropCat.DataBind();
    }
    protected void gvCropCat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCropCatg_ID = (Label)e.Row.FindControl("lblCropCatg_ID");
            GridView gvCrop = (GridView)e.Row.FindControl("gvCrop");
            Session["CropCatg_ID"] = lblCropCatg_ID.Text;

            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
            objUserBELDIST.CROPCATG_ID = Session["CropCatg_ID"].ToString();
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.RptCurStock1(objUserBELDIST);

            //string[] Param = { "@LIC_NO", "@CropCatg_ID" };
            //string[] Value = { Session["LIC_NO"].ToString(), Session["CropCatg_ID"].ToString() };
            //ds = dbsApp.param_passing_fetchStock("SELECT C.Crop_Code,C.Crop_Name FROM Stock_ReceiveDealer A " +
            //"LEFT OUTER JOIN mCropVariety B ON A.CROP_VARIETY_CODE = B.Variety_Code " +
            //"LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
            //"LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
            //"INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
            //"INNER JOIN mSEASSION F ON A.SEASSION_NAME = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
            //"WHERE LIC_NO = @LIC_NO AND D.Category_Code = @CropCatg_ID GROUP BY C.Crop_Code,C.Crop_Name", Param, Value);
            gvCrop.DataSource = ds;
            gvCrop.DataBind();
        }
    }
    protected void gvCrop_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCrop_ID = (Label)e.Row.FindControl("lblCrop_ID");
            GridView gvCropClass = (GridView)e.Row.FindControl("gvCropClass");

            Session["Crop_ID"] = lblCrop_ID.Text;

            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
            objUserBELDIST.CROPCATG_ID = Session["CropCatg_ID"].ToString();
            objUserBELDIST.CROP_ID = Session["Crop_ID"].ToString();
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.RptCurStock2(objUserBELDIST);

            //string[] Param = { "@LIC_NO", "@CropCatg_ID", "@Crop_ID" };
            //string[] Value = { Session["LIC_NO"].ToString(), Session["CropCatg_ID"].ToString(), Session["Crop_ID"].ToString() };
            //ds = dbsApp.param_passing_fetchStock("SELECT A.CROP_CLASS FROM Stock_ReceiveDealer A " +
            //"LEFT OUTER JOIN mCropVariety B ON A.CROP_VARIETY_CODE = B.Variety_Code " +
            //"LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
            //"LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
            //"INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
            //"INNER JOIN mSEASSION F ON A.SEASSION_NAME = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
            //"WHERE LIC_NO = @LIC_NO AND D.Category_Code = @CropCatg_ID AND C.Crop_Code = @Crop_ID GROUP BY A.CROP_CLASS", Param, Value);
            gvCropClass.DataSource = ds;
            gvCropClass.DataBind();
        }
    }
    protected void gvCropClass_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCROP_CLASS = (Label)e.Row.FindControl("lblCROP_CLASS");
            GridView gvDtls = (GridView)e.Row.FindControl("gvDtls");

            objUserBELDIST = new BLL_Dealer();
            objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
            objUserBELDIST.CROPCATG_ID = Session["CropCatg_ID"].ToString();
            objUserBELDIST.CROP_ID = Session["Crop_ID"].ToString();
            objUserBELDIST.CROP_CLASS = lblCROP_CLASS.Text;
            objUserDLLDIST = new DLL_Dealer();
            ds = new DataSet();
            ds = objUserDLLDIST.RptCurStock3(objUserBELDIST);

            //string[] Param = { "@LIC_NO", "@CropCatg_ID", "@Crop_ID", "@CROP_CLASS" };
            //string[] Value = { Session["LIC_NO"].ToString(), Session["CropCatg_ID"].ToString(), Session["Crop_ID"].ToString(), lblCROP_CLASS.Text };
            //ds = dbsApp.param_passing_fetchStock("SELECT B.Variety_Code,B.Variety_Name,SUM(STOCK_QUANTITY)STOCK_QUANTITY,SUM(AVL_QUANTITY)AVL_QUANTITY FROM STOCK_DEALERSTOCK A " +
            //"LEFT OUTER JOIN mCropVariety B ON A.CROP_VERID = B.Variety_Code " +
            //"LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
            //"LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
            //"INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
            //"INNER JOIN mSEASSION F ON A.SEASSION = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
            //"WHERE A.LICENCE_NO = @LIC_NO AND D.Category_Code = @CropCatg_ID AND C.Crop_Code = @Crop_ID AND A.CLASS = @CROP_CLASS " +
            //"GROUP BY B.Variety_Code,B.Variety_Name", Param, Value);
            gvDtls.DataSource = ds;
            gvDtls.DataBind();
        }
    }
}
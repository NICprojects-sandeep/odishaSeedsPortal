using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DLL_DropDown
/// </summary>
public class DLL_DropDown
{
    string _connstrOssopca = ConfigurationManager.ConnectionStrings["OSSOPCAConnectionString"].ConnectionString;
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;
    string _connstrSeed = ConfigurationManager.ConnectionStrings["SqlConSeed"].ConnectionString;
    string _connstrAuth = ConfigurationManager.ConnectionStrings["SqlConAuth"].ConnectionString;
    DataSet ds = new DataSet();
    public DLL_DropDown()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetCurFinYr()
    {
        try
        {

            int mnth = System.DateTime.Now.Month;
            if (mnth > 03)
            {
                string yr = System.DateTime.Now.Year.ToString();
                string nextyr = System.DateTime.Now.AddYears(1).Year.ToString();
                string nextyear = nextyr.Substring(2, 2);
                string cfyr = yr + "-" + nextyear;
                return cfyr;
            }
            else
            {
                string nextyr = System.DateTime.Now.AddYears(-1).Year.ToString();
                string yr = System.DateTime.Now.Year.ToString();
                string year = yr.Substring(2, 2);
                string cfyr = nextyr + "-" + year;
                return cfyr;
            }
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return null;
        }
    }
    public DataSet FillGoDownByDistCodeUserType(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT GODOWN_ID,GODOWN_NAME FROM STOCK_GODOWN_MASTER WHERE DIST_CODE = @DIST_CODE AND USER_TYPE = @USER_TYPE AND ISACTIVE = 'Y'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetGoDownByGoDownId(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT GODOWN_ID,GODOWN_NAME FROM STOCK_GODOWN_MASTER WHERE GODOWN_ID = @GODOWN_ID AND ISACTIVE='Y'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
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
    public DataSet GetGoDownByUserType(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT GODOWN_ID,GODOWN_NAME FROM STOCK_GODOWN_MASTER WHERE USER_TYPE = @USER_TYPE AND ISACTIVE='Y'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetGoDownByUserTypeDistCode(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT GODOWN_ID,GODOWN_NAME FROM STOCK_GODOWN_MASTER WHERE USER_TYPE = @USER_TYPE AND DIST_CODE = @DIST_CODE AND ISACTIVE='Y' AND GODOWN_ID !=@GODOWN_ID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.CURGODOWNID);
            cmd.Parameters.AddWithValue("@Dist_Code", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public string GetCropNameByCropCode(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        string CROP_NAME = "";
        SqlCommand cmd = new SqlCommand("SELECT CROP_NAME FROM mCROP WHERE CROP_CODE = @CROP_CODE AND IS_ACTIVE = 1", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CROP_CODE", objBELUserDetails.CROP_ID);
            con.Open();
            CROP_NAME = cmd.ExecuteScalar().ToString();
            con.Close();

            return CROP_NAME;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return "";
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public string GetVarietyNameByVarietyCode(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        string VARIETY_NAME = "";
        SqlCommand cmd = new SqlCommand("SELECT Variety_Name FROM mCropVariety WHERE Variety_Code = @Variety_Code AND IS_ACTIVE = 1", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Variety_Code", objBELUserDetails.Crop_Verid);
            con.Open();
            VARIETY_NAME = cmd.ExecuteScalar().ToString();
            con.Close();

            return VARIETY_NAME;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return "";
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillCropVariety()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Variety_Code,Crop_Code,Variety_Name FROM mCropVariety WHERE IS_ACTIVE = 1 ORDER BY Variety_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillCrop()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Crop_Code,Category_Code,Crop_Name FROM mCrop WHERE IS_ACTIVE = 1 ORDER BY Crop_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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

    public DataSet FillDistrict()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Dist_Code, Dist_Name FROM Stock_District  ORDER BY Dist_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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

    public DataSet FillScheme()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT SCHEME_CODE, SCHEME_NAME FROM mSCHEME  ORDER BY SCHEME_NAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillCropCategory()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Category_Code,Category_Name FROM mCropCategory WHERE IS_ACTIVE = 1 ORDER BY Category_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillCropCategorySale(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.CATEGORY_CODE,B.CATEGORY_NAME FROM STOCK_STOCKDETAILS A " +
        "LEFT OUTER JOIN mCROPCATEGORY B ON A.CROPCATG_ID = B.CATEGORY_CODE " +
        "WHERE A.AVL_QUANTITY > 0 AND USER_TYPE = @USER_TYPE AND A.expiry_date>getdate() AND A.GODOWN_ID = @GODOWN_ID AND A.AVL_NO_OF_BAGS > 0 AND A.VALIDITY = 1 " +
        "GROUP BY B.CATEGORY_CODE,B.CATEGORY_NAME " +
        "ORDER BY B.CATEGORY_NAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
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
    public DataSet FillSource()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT AGENCIESID,RECEIVE_UNITCD,RECEIVE_UNITNAME,IS_OAIC,IS_OSSC,IS_ACTIVE FROM STOCK_RECEIVE_UNIT_MASTER WHERE IS_ACTIVE = 1 ORDER BY Receive_Unitname ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillMOUs()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT REF_NO,NAME,DIST_CODE,SESSION,USER_TYPE FROM mMouData WHERE IS_ACTIVE = 1 ORDER BY NAME ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillPriceList(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT CROP_CLASS,RECEIVE_UNITCD,CROP_VCODE,SEASONS,ALL_IN_COST_PRICE,GOI_SUBSIDY,STATEPLAN_SUBSIDY,TOT_SUBSIDY,FARMER_PRICE,DATE_OF_ENTRY,F_YEAR FROM STOCK_PRICELIST where seasons = @seasons and F_Year = @F_Year ORDER BY F_YEAR ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@seasons", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@F_Year", objBELUserDetails.FIN_YR);
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
    public DataSet FillPriceReceiveUnit(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT RECEIVE_UNITCD,PRICE_RECEIVE_UNITCD,SEASSION,FIN_YR FROM PRICE_SOURCEMAPPING WHERE SEASSION = @SEASSION AND FIN_YR = @FIN_YR ORDER BY RECEIVE_UNITCD", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@SEASSION", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
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
    public DataSet FillCropByCategoryId(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Crop_Code,Crop_Name FROM mCrop WHERE Category_Code = @CROPCATG_ID AND is_active = 1 ORDER BY Crop_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CROPCATG_ID", objBELUserDetails.CROPCATG_ID);
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
    public DataSet FillCropByCategoryIdSale(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.Crop_Code,B.Crop_Name FROM Stock_StockDetails A " +
        "LEFT OUTER JOIN mCROP B ON A.CROP_ID = B.Crop_Code " +
        "WHERE A.AVL_QUANTITY > 0 AND USER_TYPE = @USERTYPE AND A.CROPCATG_ID = @CROPCATG_ID AND A.GODOWN_ID = @GODOWN_ID AND A.AVL_NO_OF_BAGS > 0 AND A.VALIDITY = 1 " +
        "GROUP BY B.Crop_Code,B.Crop_Name " +
        "ORDER BY B.Crop_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@USERTYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@CROPCATG_ID", objBELUserDetails.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
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
    public DataSet FillCropVarietyByCropIdReceive(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("sp_FILLVARIETYBYCLASS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@CROP_ID", objBELUserDetails.CROP_ID);
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@CLASS_NAME", objBELUserDetails.CropClass);
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
    public DataSet FillLotNoUserType(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT LOT_NO,USER_TYPE FROM (SELECT LOT_NO,USER_TYPE FROM STOCK_OPENINGBALANCE UNION ALL SELECT LOT_NO,USER_TYPE FROM DBO.STOCK_RECEIVEDETAILS ) AS A WHERE  REPLACE(REPLACE(REPLACE(REPLACE(LOT_NO,'/',''),' ',''),'-',''),'\','')=REPLACE(REPLACE(REPLACE(@LOT_NO,'/',''),'-',''),' ','') AND USER_TYPE = @USERTYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet FillSourceByUserType(BLL_DropDown objBELUserDetails)
    {
        string SqlSel = "";
        SqlConnection con = new SqlConnection(_connstrStock);
        if (objBELUserDetails.UserType == "OSSC")
        {
            SqlSel = "SELECT RECEIVE_UNITCD,RECEIVE_UNITNAME FROM STOCK_RECEIVE_UNIT_MASTER WHERE AGENCIESID = '02' AND IS_ACTIVE = 1 AND IS_OSSC = 1 ORDER BY RECEIVE_UNITNAME";
        }
        else if (objBELUserDetails.UserType == "OAIC")
        {
            SqlSel = "SELECT RECEIVE_UNITCD,RECEIVE_UNITNAME FROM STOCK_RECEIVE_UNIT_MASTER WHERE AGENCIESID = '02' AND IS_ACTIVE = 1 AND IS_OAIC = 1 ORDER BY RECEIVE_UNITNAME";
        }
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillSourceByUserTypeCashMemoNo(BLL_DropDown objBELUserDetails)
    {
        string SqlSel = "SELECT A.CROPCATG_ID,B.Category_Name,A.CROP_ID,C.Crop_Name,A.CROP_VERID,D.Variety_Name,A.CLASS,A.LOT_NUMBER,A.BAG_SIZE_KG,SALE_NO_OF_BAG,CONVERT(DECIMAL(10,2),(CONVERT(DECIMAL(10,2),A.BAG_SIZE_KG) * SALE_NO_OF_BAG)/100)Quantity_sale FROM Stock_SaleDetails A " +
        "LEFT OUTER JOIN mCropCategory B ON A.CROPCATG_ID = B.Category_Code " +
        "LEFT OUTER JOIN mCrop C ON A.CROP_ID = C.Crop_Code AND C.Category_Code = A.CROPCATG_ID " +
        "LEFT OUTER JOIN mCropVariety D ON A.CROP_VERID = D.Variety_Code AND A.CROP_ID = D.Crop_Code " +
        "WHERE A.USER_TYPE = @USER_TYPE AND A.IS_ACTIVE = 'Y' AND A.STATUS = 'T' AND A.CONFIRM_STATUS = '0' AND CONVERT(VARCHAR(10),A.SALE_DATE,103) = @SALE_DATE AND A.GODOWN_ID = @GODOWN_ID AND A.CASH_MEMO_NO = @CASH_MEMO_NO ORDER BY B.Category_Name,A.CROP_ID,C.Crop_Name,A.CROP_VERID,D.Variety_Name,A.CLASS,A.LOT_NUMBER";
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@SALE_DATE", objBELUserDetails.Sale_Date);
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
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
    public DataSet FillSourceByAgencyIdUserType(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        string SqlSel = "";
        if (objBELUserDetails.UserType == "OSSC")
        {
            SqlSel = "SELECT RECEIVE_UNITCD,RECEIVE_UNITNAME FROM STOCK_RECEIVE_UNIT_MASTER WHERE AGENCIESID = @AGENCIESID AND IS_ACTIVE = 1 AND IS_OSSC = 1 ORDER BY RECEIVE_UNITNAME";
        }
        else if (objBELUserDetails.UserType == "OAIC")
        {
            SqlSel = "SELECT RECEIVE_UNITCD,RECEIVE_UNITNAME FROM STOCK_RECEIVE_UNIT_MASTER WHERE AGENCIESID = @AGENCIESID AND IS_ACTIVE = 1 AND IS_OAIC = 1 ORDER BY RECEIVE_UNITNAME";
        }
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@AGENCIESID", objBELUserDetails.AgenciesID);
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
    public DataSet FillSourceByAgencyIdUserTypeValues(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        string SqlSel = "";
        if (objBELUserDetails.UserType == "OSSC")
        {
            SqlSel = "SELECT RECEIVE_UNITCD,RECEIVE_UNITNAME FROM STOCK_RECEIVE_UNIT_MASTER WHERE AGENCIESID = @AGENCIESID AND RECEIVE_UNITCD NOT IN (@VALUE1,@VALUE2) AND IS_ACTIVE = 1 AND IS_OSSC = 1 ORDER BY RECEIVE_UNITNAME";
        }
        else if (objBELUserDetails.UserType == "OAIC")
        {
            SqlSel = "SELECT RECEIVE_UNITCD,RECEIVE_UNITNAME FROM STOCK_RECEIVE_UNIT_MASTER WHERE AGENCIESID = @AGENCIESID AND RECEIVE_UNITCD NOT IN (@VALUE1,@VALUE2) AND IS_ACTIVE = 1 AND IS_OAIC = 1 ORDER BY RECEIVE_UNITNAME";
        }
        SqlCommand cmd = new SqlCommand(SqlSel, con);

        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@AGENCIESID", objBELUserDetails.AgenciesID);
            cmd.Parameters.AddWithValue("@VALUE1", objBELUserDetails.Value1);
            cmd.Parameters.AddWithValue("@VALUE2", objBELUserDetails.Value2);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public int GetLotNoFromOB(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM STOCK_OPENINGBALANCE WHERE REPLACE(REPLACE(REPLACE(REPLACE(LOT_NO,'/',''),' ',''),'-',''),'\','') = REPLACE(REPLACE(REPLACE(@LOT_NO,'/',''),'-',''),' ','') AND USER_TYPE = @USER_TYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int GetLotNoFromRCV(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM STOCK_RECEIVEDETAILS WHERE REPLACE(REPLACE(REPLACE(REPLACE(LOT_NO,'/',''),' ',''),'-',''),'\','') = REPLACE(REPLACE(REPLACE(@LOT_NO,'/',''),'-',''),' ','') AND Challan_No=@Challan_No AND USER_TYPE = @USER_TYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@Challan_No", objBELUserDetails.Challan_No);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckFromOB(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM STOCK_OPENINGBALANCE WHERE CROP_VERID = @CROP_VERID AND CLASS = @CLASS AND GODOWN_ID = @GODOWN_ID AND USER_TYPE = @USER_TYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CROP_VERID", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckGoDownStock(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Stock_StockDetails WHERE GODOWN_ID = @GODOWN_ID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckOBByGoDown(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM STOCK_OPENINGBALANCE WHERE GODOWN_ID = @GODOWN_ID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckSaleByDDNo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) CNT FROM STOCK_SALEDETAILS WHERE DD_NUMBER = @DD_NUMBER", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DD_NUMBER", objBELUserDetails.DD_NUMBER);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckSaleBySupplyTypeSaleToDDNo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) CNT FROM Stock_SaleDetails WHERE SUPPLY_TYPE = @SUPPLY_TYPE AND SALE_TO = @SALE_TO AND DD_NUMBER = @DD_NUMBER", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@SUPPLY_TYPE", objBELUserDetails.SUPPLY_TYPE);
            cmd.Parameters.AddWithValue("@SALE_TO", objBELUserDetails.SALE_TO);
            cmd.Parameters.AddWithValue("@DD_NUMBER", objBELUserDetails.DD_NUMBER);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckSaleByCashMemoNo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) CNT FROM Stock_SaleDetails WHERE CASH_MEMO_NO = @CASH_MEMO_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckSaleBySupplyTypeSaleToCMNo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) CNT FROM Stock_SaleDetails WHERE SUPPLY_TYPE = @SUPPLY_TYPE AND SALE_TO = @SALE_TO AND CASH_MEMO_NO = @CASH_MEMO_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@SUPPLY_TYPE", objBELUserDetails.SUPPLY_TYPE);
            cmd.Parameters.AddWithValue("@SALE_TO", objBELUserDetails.SALE_TO);
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataTable CheckOBByGoDownVariety(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT * FROM STOCK_OPENINGBALANCE WHERE GODOWN_ID = @GODOWN_ID AND CROP_VERID = @CROP_VERID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@CROP_VERID", objBELUserDetails.Crop_Verid);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            return ds.Tables[0];
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
    public int CheckFromRCV1(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM STOCK_RECEIVEDETAILS WHERE CROP_VERID = @CROP_VERID AND CLASS = @CLASS AND LOT_NO = @LOT_NO AND USER_TYPE = @USER_TYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CROP_VERID", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public string InsertOB(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_Sp_InsOpeningBalance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@Dist_Code", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@Godown_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@CropCatg_ID", objBELUserDetails.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@Crop_ID", objBELUserDetails.CROP_ID);
            cmd.Parameters.AddWithValue("@Crop_Verid", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@Class", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@Receive_Unitcd", objBELUserDetails.Receive_Unitcd);
            cmd.Parameters.AddWithValue("@Lot_No", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@Bag_Size_In_kg", objBELUserDetails.Bag_Size_In_kg);
            cmd.Parameters.AddWithValue("@Recv_No_Of_Bags", objBELUserDetails.Recv_No_Of_Bags);
            cmd.Parameters.AddWithValue("@OB_date", objBELUserDetails.OB_date);
            cmd.Parameters.AddWithValue("@OB_Quantity", objBELUserDetails.OB_Quantity);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@User_Type", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@UserID", objBELUserDetails.UserID);
            cmd.Parameters.AddWithValue("@UserIP", objBELUserDetails.UserIP);
            cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            cmd.Parameters["@msg"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            string strMessage = (string)cmd.Parameters["@msg"].Value;
            con.Close();
            return strMessage;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return "";
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int InsertRCVSubmit(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_Sp_InsReceiveDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@Dist_Code", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@Godown_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@AgenciesID", objBELUserDetails.AgenciesID);
            cmd.Parameters.AddWithValue("@Receive_Unitcd", objBELUserDetails.Receive_Unitcd);
            //cmd.Parameters.AddWithValue("@MOU_REFNO", objBELUserDetails.MOU_REFNO);
            cmd.Parameters.AddWithValue("@Outagency_Waybillnum_Farmnm", objBELUserDetails.Outagency_Waybillnum_Farm);
            cmd.Parameters.AddWithValue("@Challan_No", objBELUserDetails.Challan_No);
            cmd.Parameters.AddWithValue("@Recv_Date", objBELUserDetails.mReceive_Date);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@User_Type", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@UserID", objBELUserDetails.UserID);
            cmd.Parameters.AddWithValue("@UserIP", objBELUserDetails.UserIP);
            cmd.Parameters.AddWithValue("@VALUES", objBELUserDetails.XML_Value);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            int strMessage = (int)cmd.Parameters["@Val"].Value;
            con.Close();
            return strMessage;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void InsertSale(BLL_DropDown objBELUserDetails, out int Val, out string CASH_MEMO_NO)
    {
        Val = 0;
        CASH_MEMO_NO = "";
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_Sp_InsSaleDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@IS_PACS", objBELUserDetails.ISPACS);
            cmd.Parameters.AddWithValue("@SUPPLY_TYPE", objBELUserDetails.SUPPLY_TYPE);
            cmd.Parameters.AddWithValue("@CREDIT_BILL_NO", objBELUserDetails.CREDIT_BILL_NO);
            cmd.Parameters.AddWithValue("@mDATE", objBELUserDetails.mDATE);
            cmd.Parameters.AddWithValue("@DEPT_TYPE", objBELUserDetails.DEPT_TYPE);
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@SALE_DATE", objBELUserDetails.Sale_Date);
            cmd.Parameters.AddWithValue("@SALE_TO", objBELUserDetails.SaleTo);
            cmd.Parameters.AddWithValue("@DD_NUMBER", objBELUserDetails.DD_NUMBER);
            cmd.Parameters.AddWithValue("@AMOUNT", objBELUserDetails.AMOUNT);
            cmd.Parameters.AddWithValue("@CONFIRM_STATUS", objBELUserDetails.CONFIRM_STATUS);
            cmd.Parameters.AddWithValue("@SEASSION", objBELUserDetails.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@PACSRebate", objBELUserDetails.PACSRebate);
            cmd.Parameters.AddWithValue("@USERIP", objBELUserDetails.UserIP);
            cmd.Parameters.AddWithValue("@VALUES", objBELUserDetails.XML_Value);
            cmd.Parameters.Add("@CASH_MEMO_NO", SqlDbType.VarChar, 100);
            cmd.Parameters["@CASH_MEMO_NO"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            CASH_MEMO_NO = cmd.Parameters["@CASH_MEMO_NO"].Value.ToString();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int InsertRCVConfirm(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_Sp_InsReceiveDetails_Godown", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@AGENCIESID", objBELUserDetails.AgenciesID);
            cmd.Parameters.AddWithValue("@FROM_GODOWN_ID", objBELUserDetails.mGodown_ID);
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
            cmd.Parameters.AddWithValue("@SALE_DATE", objBELUserDetails.Sale_Date);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@USERID", objBELUserDetails.UserID);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@USERIP", objBELUserDetails.UserIP);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            int strMessage = (int)cmd.Parameters["@Val"].Value;
            con.Close();
            return strMessage;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet GetRcvCashMemoNo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT CASH_MEMO_NO FROM STOCK_SALEDETAILS  " +
        "WHERE USER_TYPE = @USER_TYPE AND IS_ACTIVE = 'Y' AND STATUS = 'T' AND CONFIRM_STATUS = '0' AND CONVERT(VARCHAR(10),SALE_DATE,103) = @SALE_DATE AND GODOWN_ID = @GODOWN_ID AND SALE_TO = @SALE_TO " +
        "GROUP BY CASH_MEMO_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@SALE_DATE", objBELUserDetails.Sale_Date);
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@SALE_TO", objBELUserDetails.SALE_TO);
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
    public DataSet GetRcvChallan_No(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT CHALLAN_NO FROM STOCK_RECEIVEDETAILS WHERE CHALLAN_NO = @CHALLAN_NO AND USER_TYPE = @USER_TYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CHALLAN_NO", objBELUserDetails.Challan_No);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet FillAgencyByOAIC()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT AGENCIESID,CASE WHEN AGENCIESNAME = 'SELF' THEN 'OSSC' ELSE AGENCIESNAME END AS AGENCIESNAME FROM STOCK_AGENCIES_MASTER WHERE ISACTIVE = 'Y'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillAgencyByOSSC()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT AGENCIESID,CASE WHEN AGENCIESNAME = 'SELF' THEN 'OAIC' ELSE AGENCIESNAME END AS AGENCIESNAME FROM STOCK_AGENCIES_MASTER WHERE ISACTIVE = 'Y'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillGoDownByUserTypeSaleTo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT A.GODOWN_ID,B.GODOWN_NAME FROM STOCK_SALEDETAILS A " +
                    "LEFT OUTER JOIN STOCK_GODOWN_MASTER B ON A.GODOWN_ID = B.GODOWN_ID " +
                    "WHERE A.USER_TYPE = @USER_TYPE AND A.IS_ACTIVE = 'Y' AND A.STATUS = 'T' AND A.CONFIRM_STATUS = '0' AND SALE_TO = @SALE_TO " +
                    "GROUP BY A.GODOWN_ID,B.GODOWN_NAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@SALE_TO", objBELUserDetails.SaleTo);
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
    public DataSet GetRCLLotDtls(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT A.CROPCATG_ID,B.Category_Name,A.CROP_ID,C.Crop_Name,A.CROP_VERID,D.Variety_Name,A.CLASS,A.LOT_NUMBER,A.BAG_SIZE_KG,A.SALE_NO_OF_BAG,CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(10,2),A.BAG_SIZE_KG)*CONVERT(DECIMAL(10,2),A.SALE_NO_OF_BAG)) QUANTITY_SALE FROM STOCK_SALEDETAILS A " +
                    "LEFT OUTER JOIN mCropCategory B ON A.CROPCATG_ID = B.Category_Code " +
                    "LEFT OUTER JOIN mCrop C ON A.CROP_ID = C.Crop_Code " +
                    "LEFT OUTER JOIN mCropVariety D ON A.CROP_VERID = D.Variety_Code " +
                    "WHERE A.USER_TYPE = @USER_TYPE AND A.IS_ACTIVE = 'Y' AND A.STATUS = 'T' AND A.CONFIRM_STATUS = '0' AND CONVERT(VARCHAR(10),SALE_DATE,103) = @SALE_DATE AND GODOWN_ID = @GODOWN_ID AND CASH_MEMO_NO = @CASH_MEMO_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@SALE_DATE", objBELUserDetails.Sale_Date);
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
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
    public DataSet FillSupplyType(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT SUPPLY_ID,SUPPLY_NAME FROM STOCK_SUPPLYTYPE WHERE USER_TYPE = @USER_TYPE AND ISACTIVE = 'Y' ORDER BY ORDER_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetDealerLicenceByDistCodeUserType(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("SELECT DISTINCT CASE WHEN A.LIC_NO1 IS NOT NULL THEN A.LIC_NO1 ELSE A.LIC_NO END + '/DA & FP(O) - ' + A.APP_FIRMNAME +' - '+ A.LIC_NO AS 'Dealer', A.APP_FIRMNAME, A.LIC_NO FROM SEED_LIC_DIST A " +
        "LEFT OUTER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
        "LEFT OUTER JOIN SEED_LIC_APP_DIST C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID " +
        "WHERE A.DIST_CODE = @DIST_CODE  AND A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND CONVERT(DATE,GETDATE(),103) <= A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = @USER_TYPE AND A.APP_TYPE != 'Secretary PACS' " + //AND A.SOURCE_APP_STATUS = 'A' 
        "ORDER BY A.APP_FIRMNAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetDealerLicenceByDistCodeUserTypeOAIC(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("SELECT DISTINCT CASE WHEN A.LIC_NO1 IS NOT NULL THEN A.LIC_NO1 ELSE A.LIC_NO END + '/DA & FP(O) - ' + A.APP_FIRMNAME +' - '+ A.LIC_NO AS 'Dealer', A.APP_FIRMNAME, A.LIC_NO FROM SEED_LIC_DIST A " +
        "LEFT OUTER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
        "LEFT OUTER JOIN SEED_LIC_APP_DIST C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID " +
        "WHERE A.DIST_CODE = @DIST_CODE  AND A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND CONVERT(DATE,GETDATE(),103)<= A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = 'OAIC' " + //AND A.SOURCE_APP_STATUS = 'A' 
        "ORDER BY A.APP_FIRMNAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetDealerLicenceByDistCodeUserTypePacs(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("SELECT DISTINCT CASE WHEN A.LIC_NO1 IS NOT NULL THEN A.LIC_NO1 ELSE A.LIC_NO END + '/DA & FP(O) - ' + A.APP_FIRMNAME +' - '+ A.LIC_NO AS 'Dealer', A.APP_FIRMNAME, A.LIC_NO FROM SEED_LIC_DIST A " +
        "LEFT OUTER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
        "LEFT OUTER JOIN SEED_LIC_APP_DIST C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID " +
        "WHERE A.DIST_CODE = @DIST_CODE  AND A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND CONVERT(DATE,GETDATE(),103)<= A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = 'OSSC' AND A.APP_TYPE = 'Secretary PACS' " + //AND A.SOURCE_APP_STATUS = 'A' 
        "ORDER BY A.APP_FIRMNAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetDealerLicenceByUserType(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("SELECT A.LIC_NO + '/DA & FP(O) - ' + A.APP_FIRMNAME AS 'Dealer',LIC_NO,A.APP_FIRMNAME FROM SEED_LIC_STATE A " +
            "LEFT OUTER JOIN SEED_LIC_COMP_STATE B ON A.SEED_LIC_STATE_ID = B.SEED_LIC_STATE_ID " +
            "WHERE A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND CONVERT(DATE,GETDATE(),103) <= A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = @USER_TYPE AND A.APP_TYPE != 'Secretary PACS' " +
            "ORDER BY A.LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetDealerLicenceByUserTypePacs(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("SELECT A.LIC_NO + '/DA & FP(O) - ' + A.APP_FIRMNAME AS 'Dealer',LIC_NO,A.APP_FIRMNAME FROM SEED_LIC_STATE A " +
            "LEFT OUTER JOIN SEED_LIC_COMP_STATE B ON A.SEED_LIC_STATE_ID = B.SEED_LIC_STATE_ID " +
            "WHERE A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND CONVERT(DATE,GETDATE(),103) <= A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = @USER_TYPE AND A.APP_TYPE = 'Secretary PACS' " +
            "ORDER BY A.LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet FillDistricts()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT DIST_CODE,DIST_NAME FROM STOCK_DISTRICT ORDER BY DIST_NAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public string GetDistNameByDistCode(BLL_DropDown objBELUserDetails)
    {
        string DistName = "";
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT DIST_NAME FROM STOCK_DISTRICT WHERE DIST_CODE = @DIST_CODE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            con.Open();
            DistName = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return DistName;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return "";
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataTable FillGrid1(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        //SqlCommand cmd = new SqlCommand("SELECT A.STOCK_ID,A.RECEIVE_UNITCD,C.REF_NO,CASE A.RECEIVE_UNITCD WHEN '0123' THEN C.NAME ELSE B.Receive_Unitname END Receive_Unitname,A.LOT_NO,A.BAG_SIZE_IN_KG,CAST(A.AVL_QUANTITY/A.BAG_SIZE_IN_KG*100 AS INT) RECV_NO_OF_BAGS,A.AVL_QUANTITY,A.CROP_VERID " +
        //"FROM STOCK_STOCKDETAILS A " +
        //"LEFT OUTER JOIN Stock_Receive_Unit_Master B ON A.RECEIVE_UNITCD = B.RECEIVE_UNITCD " +
        //"LEFT OUTER JOIN mMouData C ON A.MOU_REFNO = C.REF_NO " +
        //"WHERE A.GODOWN_ID = @GODOWN_ID AND A.CROPCATG_ID = @CROPCATG_ID AND A.CROP_ID = @CROP_ID AND A.CROP_VERID = @CROP_VERID AND A.CLASS = @CLASS AND A.USER_TYPE = @USER_TYPE AND A.RECV_NO_OF_BAGS > 0", con);

        SqlCommand cmd = new SqlCommand("SELECT A.STOCK_ID,A.RECEIVE_UNITCD,C.REF_NO,B.Receive_Unitname,A.LOT_NO,A.BAG_SIZE_IN_KG,CAST(A.AVL_QUANTITY/A.BAG_SIZE_IN_KG*100 AS INT) RECV_NO_OF_BAGS,A.AVL_QUANTITY,A.CROP_VERID " +
        "FROM STOCK_STOCKDETAILS A " +
        "LEFT OUTER JOIN Stock_Receive_Unit_Master B ON A.RECEIVE_UNITCD = B.RECEIVE_UNITCD " +
        "LEFT OUTER JOIN mMouData C ON A.MOU_REFNO = C.REF_NO " +
        "WHERE A.GODOWN_ID = @GODOWN_ID AND A.CROPCATG_ID = @CROPCATG_ID AND A.CROP_ID = @CROP_ID AND A.CROP_VERID = @CROP_VERID AND A.USER_TYPE = @USER_TYPE AND A.AVL_NO_OF_BAGS > 0 AND A.VALIDITY = 1  AND EXPIRY_DATE > GETDATE() AND A.Class IN ('Certified','TL') ", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@CROPCATG_ID", objBELUserDetails.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@CROP_ID", objBELUserDetails.CROP_ID);
            cmd.Parameters.AddWithValue("@CROP_VERID", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);

            return ds.Tables[0];
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
    public Double GetBagsFromStockByLotNoVerId(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        double RECV_NO_OF_BAGS = 0;
        SqlCommand cmd = new SqlCommand("SELECT RECV_NO_OF_BAGS FROM STOCK_STOCKDETAILS WHERE REPLACE(REPLACE(REPLACE(REPLACE(LOT_NO,'/',''),' ',''),'-',''),'\','') = REPLACE(REPLACE(REPLACE(@LOT_NO,'/',''),'-',''),' ','') AND CROP_VERID = @CROP_VERID AND Godown_id = @Godown_id AND AVL_NO_OF_BAGS > 0", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@CROP_VERID", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@Godown_id", objBELUserDetails.Godown_ID);
            if (con.State != ConnectionState.Open)
                con.Open();
            RECV_NO_OF_BAGS = Convert.ToDouble(cmd.ExecuteScalar());
            return RECV_NO_OF_BAGS;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet GetDistFirmNameLicenceNo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("SELECT APP_FIRMNAME,LIC_NO + '/DA & FP(O)' AS LIC_NO FROM SEED_LIC_DIST WHERE LIC_NO = @LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELUserDetails.LIC_NO);
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
    public DataSet GetStateFirmNameLicenceNo(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("SELECT APP_FIRMNAME,LIC_NO + '/DA & FP(O)' AS LIC_NO FROM SEED_LIC_STATE WHERE LIC_NO = @LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELUserDetails.LIC_NO);
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
    public DataSet GetSupDepSale(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.SUPPLY_TYPE,A.CREDIT_BILL_NO,A.DEPT_TYPE,A.SALE_TO,A.DD_NUMBER,A.AMOUNT,A.GODOWN_ID,B.GODOWN_NAME,CONVERT(VARCHAR(10),A.SALE_DATE,103)SALE_DATE,A.USER_TYPE " +
        "FROM STOCK_SALEDETAILS A " +
        "LEFT OUTER JOIN Stock_Godown_Master B ON A.GODOWN_ID = B.GODOWN_ID " +
        "WHERE CASH_MEMO_NO = @CASH_MEMO_NO " +
        "GROUP BY A.SUPPLY_TYPE,A.CREDIT_BILL_NO,A.DEPT_TYPE,A.SALE_TO,A.DD_NUMBER,A.AMOUNT,A.GODOWN_ID,B.GODOWN_NAME,CONVERT(VARCHAR(10),A.SALE_DATE,103),A.USER_TYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
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
    public DataSet FillSaleVoucher(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.CASH_MEMO_NO,A.SALETRANSID,D.Category_Name,A.CLASS,C.Crop_Name,B.Variety_Name,E.RECEIVE_UNITNAME, " +
        "A.LOT_NUMBER,A.SALE_NO_OF_BAG,A.BAG_SIZE_KG, " +
        "CONVERT(DECIMAL(10,2),CONVERT(DECIMAL(10,2),A.SALE_NO_OF_BAG)*CONVERT(DECIMAL(10,2),A.BAG_SIZE_KG)/100) AS QUANTITY_SALE, " +
        "ISNULL(A.PRICE_QTL,0) ALL_IN_COST_PRICE, " +
        "ISNULL(CONVERT(DECIMAL(10,2),A.PRICE_QTL*CONVERT(DECIMAL(10,2),CONVERT(DECIMAL(10,2),A.SALE_NO_OF_BAG)*CONVERT(DECIMAL(10,2),A.BAG_SIZE_KG)/100)),0) TOTAL_AMOUNT " +
        "FROM STOCK_SALEDETAILS A  " +
        "LEFT OUTER JOIN mCropVariety B ON A.CROP_VERID = B.Variety_Code  " +
        "LEFT OUTER JOIN mCrop C ON A.CROP_ID = C.Crop_Code  " +
        "LEFT OUTER JOIN mCropCategory D ON A.CROPCATG_ID = D.Category_Code  " +
        "LEFT OUTER JOIN STOCK_RECEIVE_UNIT_MASTER E ON A.RECEIVE_UNITCD = E.RECEIVE_UNITCD " +
        "WHERE A.CASH_MEMO_NO = @CASH_MEMO_NO " +
        "ORDER BY D.Category_Name,A.CLASS,C.Crop_Name,B.Variety_Name,E.RECEIVE_UNITNAME ", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
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
    public int SyncMOU(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_Sp_InsMOU", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@NAME", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@SESSION", objBELUserDetails.UserType);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            int Cnt = (int)cmd.Parameters["@Val"].Value;
            con.Close();
            con.Open();

            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillMOUByUserTypeDistCode(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("sp_FILLMOUBYUSRTYPE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
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
    public DataSet FillLotByVarietyCode(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("sp_FILLLOTS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_NAME", objBELUserDetails.CropClass);
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
    public DataSet FillCropVarietyByCropId(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Variety_Code,Crop_Code,Variety_Name FROM mCropVariety WHERE Crop_Code = @Crop_Code AND IS_ACTIVE = 1 ORDER BY Variety_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
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
    public DataSet FillCropVarietyByCropIdVarietyNameLike(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Variety_Code,Crop_Code,Variety_Name FROM mCropVariety WHERE Crop_Code = @Crop_Code AND Variety_Name LIKE '%' + @Variety_Name + '%' AND IS_ACTIVE = 1 ORDER BY Variety_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@Variety_Name", objUserBEL.CROPVARIETY_NAME);
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
    public DataSet FillCropVarietyByCropIdSale(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.Variety_Code,B.Variety_Name FROM Stock_StockDetails A " +
        "LEFT OUTER JOIN mCropVariety B ON A.CROP_VERID = B.Variety_Code " +
        "WHERE A.AVL_QUANTITY > 0 AND USER_TYPE = @USERTYPE AND A.CROPCATG_ID = @CROPCATG_ID AND A.CROP_ID = @CROP_ID AND A.GODOWN_ID = @GODOWN_ID AND A.AVL_NO_OF_BAGS > 0 AND A.VALIDITY = 1 AND (A.EXPIRY_DATE = NULL OR CONVERT(DATE,A.EXPIRY_DATE,103) >= CONVERT(DATE,CURRENT_TIMESTAMP,103)) AND B.IS_ACTIVE = 1 " +
        "GROUP BY B.Variety_Code,B.Variety_Name " +
        "ORDER BY B.Variety_Code,B.Variety_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@GODOWN_ID", objUserBEL.Godown_ID);
            cmd.Parameters.AddWithValue("@USERTYPE", objUserBEL.UserType);
            cmd.Parameters.AddWithValue("@CROPCATG_ID", objUserBEL.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@CROP_ID", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objUserBEL.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
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
    public bool GetReceiveStatus(BLL_DropDown objBELUserDetails)
    {
        bool Rcv = false;
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM STOCK_STOCKDETAILS WHERE ENTRY_STATUS = 'R' AND DIST_CODE = @DistCode AND USER_TYPE = @USER_TYPE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DistCode", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            if (Cnt > 0)
            {
                Rcv = true;
            }

            return Rcv;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return false;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet GetDelRcvDateWise(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_DEALERRCVDATEWISE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FROM_DATE", objBELUserDetails.FROM_DATE);
            cmd.Parameters.AddWithValue("@TO_DATE", objBELUserDetails.TO_DATE);
            cmd.Parameters.AddWithValue("@UserID", objBELUserDetails.UserID);
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
    public DataSet FillFinYr(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_FILLFINYR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@STATUS", objBELUserDetails.STATUS);
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
    public DataSet FillSeassion(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_FILLSEASSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@STATUS", objBELUserDetails.STATUS);
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
    public DataSet FillSaleDtlsByDate(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.CASH_MEMO_NO,A.SALE_TO,C.APP_FIRMNAME,A.DD_NUMBER,SUM(A.AMOUNT)AMOUNT FROM Stock_SaleDetails A " +
        "LEFT OUTER JOIN Stock_Godown_Master B ON A.GODOWN_ID = B.GODOWN_ID " +
        "LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] C ON A.SALE_TO = C.LIC_NO " +
        "WHERE B.DIST_CODE = @DIST_CODE AND CONVERT(VARCHAR(10),A.SALE_DATE,103) = @SALE_DATE AND A.SUPPLY_TYPE IN (1,6,9) AND A.USER_TYPE = @USER_TYPE GROUP BY A.CASH_MEMO_NO,A.SALE_TO,C.APP_FIRMNAME,A.DD_NUMBER ", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@SALE_DATE", objBELUserDetails.Sale_Date);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet FillSaleDtlsByDateGODown(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.CASH_MEMO_NO,B.GODOWN_NAME FROM_GODOWN,C.GODOWN_NAME TO_GODOWN FROM Stock_SaleDetails A " +
        "LEFT OUTER JOIN Stock_Godown_Master B ON A.GODOWN_ID = B.GODOWN_ID " +
        "LEFT OUTER JOIN Stock_Godown_Master C ON A.SALE_TO = C.GODOWN_ID " +
        "WHERE B.DIST_CODE = @DIST_CODE AND CONVERT(VARCHAR(10),A.SALE_DATE,103) = @SALE_DATE " +
        "AND A.SUPPLY_TYPE IN (3,8) AND A.USER_TYPE = @USER_TYPE " +
        "GROUP BY A.CASH_MEMO_NO,B.GODOWN_NAME,C.GODOWN_NAME ", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@SALE_DATE", objBELUserDetails.Sale_Date);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
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
    public DataSet GetMobileSMS()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        //SqlCommand cmd = new SqlCommand("SELECT TOP (100) A.NICFARMERID,A.VCHMOBILENO,NAME FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] A " +
        //"LEFT OUTER JOIN [FARMERDB].[DBO].[TBL_FARMERAPPROVE] B ON A.VCHFARMERCODE = B.FARMER_CODE " +
        //"WHERE B.Status='ACCP' AND VCHMOBILENO !='' AND B.SMS_ST IS NULL ", con);
        SqlCommand cmd = new SqlCommand("SELECT NICFARMERID,VCHMOBILENO,NAME FROM tbmob ", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public void UpdateMobSMS(BLL_DropDown objBELUserDetails)
    {

        SqlConnection con = new SqlConnection(_connstrStock);
        //SqlCommand cmd = new SqlCommand("UPDATE [FARMERDB].[DBO].[TBL_FARMERAPPROVE] SET SMS_ST = @MOBSTATUS WHERE VCHMOBILENO = @MOBNO", con);
        SqlCommand cmd = new SqlCommand("UPDATE tbmob SET status = @MOBSTATUS WHERE VCHMOBILENO = @MOBNO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@MOBSTATUS", objBELUserDetails.MOBSTATUS);
            cmd.Parameters.AddWithValue("@MOBNO", objBELUserDetails.MOBNO);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }

    //For Seed Grower
    #region
    public DataSet FillMOUByUserTypeDistCode1(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";
        if (objBELUserDetails.UserType == "OSSC")
        {
            SqlSel = "SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status = '3' AND Agency_Secter = 'OTHER' AND District_code = @DIST_CODE AND " + "session_status = 1  " + //and lot_no='DEC/16-18-64-03G30613-1'
            "union " +
            "SELECT DISTINCT Ref_NO, SG_Name AS NAME FROM VW_agencyTag where Status = '3' AND Agency_Secter = @USER_TYPE AND District_code = @DIST_CODE AND " +
            "session_status = 1 ORDER BY SG_Name ASC ";
        }
        else
        {
            SqlSel = "SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND session_status = 1 ORDER BY SG_Name ASC";
        }
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            // cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objBELUserDetails.SEASON);
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
    public DataSet FillCropVarietyByCropIdReceive1(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter=@UserType and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE and Ref_NO=@Ref_NO and Class_code=@Class_code AND Crop_Code=@CropID AND session_status = 1  ORDER BY Varity_Name ASC", con);

        String SqlSel = "";
        if (objBELUserDetails.UserType == "OSSC")
        {
            SqlSel = "SELECT DISTINCT Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter='OTHER'  and District_code=@DIST_CODE and Ref_NO=@Ref_NO and Class_code=@Class_code AND Crop_Code=@CropID AND session_status = 1 " +//and lotno='DEC/16-18-64-03G30613-1' 
            "union " +
            "SELECT DISTINCT Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter=@UserType  and District_code=@DIST_CODE and Ref_NO=@Ref_NO and Class_code=@Class_code AND Crop_Code=@CropID AND session_status = 1  ORDER BY Varity_Name ASC ";
        }
        else
        {
            SqlSel = "SELECT DISTINCT Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter=@UserType  and District_code=@DIST_CODE and Ref_NO=@Ref_NO and Class_code=@Class_code AND Crop_Code=@CropID AND session_status = 1  ORDER BY Varity_Name ASC";
        }
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@UserType", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            // cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@Ref_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@CropID", objBELUserDetails.CROP_ID);
            //cmd.Parameters.AddWithValue("@SEASON", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@Class_code", objBELUserDetails.CropClass);
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
    public DataSet FillLotByVarietyCode1(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT DISTINCT LotNo FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Ref_NO=@REF_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
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
    public DataSet FillGrowerVarietyName(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT distinct Varity_Name FROM VW_agencyTag where Status='3' AND Ref_NO =@REF_NO AND session_status = 1 ", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
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
    #endregion

    //For Outside Agencies
    #region
    public DataSet FillCropVarietyByOutsideAgencies(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Variety_Code,Crop_Code,Variety_Name FROM mCropVariety WHERE Crop_Code = @Crop_Code AND IS_ACTIVE = 1 ORDER BY Variety_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
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
    #endregion

    // For MOU Agency
    #region
    public DataSet FillByUserTypeMOUAgency(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT Distinct Name_of_agency,AgName FROM VW_agencyTag where Status='3' AND Agency_Secter IN ('Private Seed Industries','OAIC','NSC') AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY AgName ASC", con);

        SqlCommand cmd = new SqlCommand("SELECT Distinct Name_of_agency,AgName FROM VW_agencyTag where Status='3' AND Agency_Secter IN ('Private Seed Industries','OAIC','NSC') AND District_code=@DIST_CODE  AND session_status = 1 ORDER BY AgName ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            //cmd.Parameters.AddWithValue("@USER_TYPE", objBELUserDetails.UserType);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            //  cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objBELUserDetails.SEASON);
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
    public DataSet FillCropVarietyByMOUAgency(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        //    SqlCommand cmd = new SqlCommand("SELECT distinct Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter IN ('Private Seed Industries','OAIC','NSC') and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE and Name_of_agency=@Ref_NO AND Class_code=@Class_code AND session_status = 1  AND Crop_Code=@CropID ORDER BY Varity_Name ASC", con);

        SqlCommand cmd = new SqlCommand("SELECT distinct Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter IN ('Private Seed Industries','OAIC','NSC')  and District_code=@DIST_CODE and Name_of_agency=@Ref_NO AND Class_code=@Class_code AND session_status = 1  AND Crop_Code=@CropID ORDER BY Varity_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            //  cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@Ref_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@Class_code", objBELUserDetails.CropClass);
            //  cmd.Parameters.AddWithValue("@Crop_Name", objBELUserDetails.CROPVARIETY_NAME);
            cmd.Parameters.AddWithValue("@CropID", objBELUserDetails.CROP_ID);
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
    public DataSet FillLotByMOUAgency(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT DISTINCT LotNo FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Name_of_agency=@REF_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
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
    #endregion

    //For Department Farm
    #region
    public DataSet FillGovtFarmByDistCode(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        //  SqlCommand cmd = new SqlCommand("SELECT DISTINCT AgName,Name_of_agency FROM VW_agencyTag WHERE Status='3' and Agency_Secter='Govt.Agri Farm' and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE AND session_status = 1 ORDER BY AgName ASC", con);

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT AgName,Name_of_agency FROM VW_agencyTag WHERE Status='3' and Agency_Secter IN ('ICAR','Govt.Agri Farm') and District_code=@DIST_CODE AND session_status = 1 ORDER BY AgName ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            //   cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
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
    public DataSet FillCropVarietyByGovtFarm(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT distinct Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter='Govt.Agri Farm' and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE and Name_of_agency=@Ref_NO and Class_code=@Class_code AND session_status = 1 AND Crop_Code=@CropID ORDER BY Varity_Name ASC", con);

        SqlCommand cmd = new SqlCommand("SELECT distinct Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter in ('Govt.Agri Farm','ICAR') and District_code=@DIST_CODE and Name_of_agency=@Ref_NO and Class_code=@Class_code AND session_status = 1 AND Crop_Code=@CropID ORDER BY Varity_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            //  cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@Ref_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@Class_code", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@CropID", objBELUserDetails.CROP_ID);
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
    public DataSet FillLotByGovtFarm(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT distinct LotNo FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Name_of_agency=@REF_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
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
    #endregion


    //For Ouat
    #region
    public DataSet FillByDistCodeOUAT(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        //   SqlCommand cmd = new SqlCommand("SELECT DISTINCT AgName,Name_of_agency FROM VW_agencyTag WHERE Status='3' and Agency_Secter='OUAT' and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE AND session_status = 1  ORDER BY AgName ASC", con);

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT AgName,Name_of_agency FROM VW_agencyTag WHERE Status='3' and Agency_Secter='OUAT' and District_code=@DIST_CODE AND session_status = 1  ORDER BY AgName ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            // cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
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
    public DataSet FillCropVarietyByOUAT(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        //   SqlCommand cmd = new SqlCommand("SELECT distinct Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter='OUAT' and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE and Name_of_agency=@Ref_NO and Class_code=@Class_code AND session_status = 1 AND Crop_Code=@CropID ORDER BY Varity_Name ASC", con);

        SqlCommand cmd = new SqlCommand("SELECT distinct Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter='OUAT'  and District_code=@DIST_CODE and Name_of_agency=@Ref_NO and Class_code=@Class_code AND session_status = 1 AND Crop_Code=@CropID ORDER BY Varity_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            // cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@Ref_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@Class_code", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@CropID", objBELUserDetails.CROP_ID);
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
    public DataSet FillLotByOUAT(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT distinct LotNo FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Name_of_agency=@REF_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
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
    #endregion

    //For Self // OSSC/OAIC
    #region
    public DataSet FillCropVarietyBySelf(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Variety_Code,Crop_Code,Variety_Name FROM mCropVariety WHERE Crop_Code = @Crop_Code AND IS_ACTIVE=1  ORDER BY Variety_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
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
    #endregion

    //For Scheme
    #region
    public DataSet FillByUserTypeScheme(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        //      SqlCommand cmd = new SqlCommand("SELECT DISTINCT AgName,Name_of_agency FROM VW_agencyTag WHERE Status='3' and Agency_Secter=@Agency_Secter and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE ORDER BY AgName ASC", con);


        SqlCommand cmd = new SqlCommand("SELECT DISTINCT AgName,Name_of_agency FROM VW_agencyTag WHERE Status='3' and Agency_Secter=@Agency_Secter  and District_code=@DIST_CODE AND session_status = 1 ORDER BY AgName ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Agency_Secter", objBELUserDetails.AGENCYSECTER);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            //  cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objBELUserDetails.SEASON);
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
    public DataSet FillCropVarietyByCropIdScheme(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        //   SqlCommand cmd = new SqlCommand("SELECT DISTINCT Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter=@Agency_Secter and SUBSTRING(REF_NO,8,7) =@FIN_YR and District_code=@DIST_CODE and Name_of_agency=@Ref_NO and Class_code=@Class_code AND session_status = 1 ORDER BY Varity_Name ASC", con);

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT Varity_Code as Variety_Code,Varity_Name as Variety_Name FROM VW_agencyTag WHERE Status='3' and Agency_Secter=@Agency_Secter  and District_code=@DIST_CODE and Name_of_agency=@Ref_NO and Class_code=@Class_code AND session_status = 1 AND Crop_Code=@CropID ORDER BY Varity_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Agency_Secter", objBELUserDetails.AGENCYSECTER);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            //  cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@Ref_NO", objBELUserDetails.REF_NO);
            //cmd.Parameters.AddWithValue("@SEASON", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@Class_code", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@CropID", objBELUserDetails.CROP_ID);
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
    public DataSet FillLotByVarietyCodeScheme(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT DISTINCT LotNo FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Name_of_agency=@REF_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
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
    #endregion



    public DataSet FillAllActiveBags(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT BAG_SIZE,BAG_NAME FROM STOCK_BAGSIZE WHERE IS_ACTIVE = @STATUS ORDER BY BAG_SIZE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@STATUS", objUserBEL.STATUS);
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
    public void UpdateZeroTrans(BLL_DropDown objBELUserDetails, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("sp_UPDATEFARMERID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@TRANSACTION_ID ", objBELUserDetails.TRANSACTIONID);
            cmd.Parameters.AddWithValue("@FARMER_ID ", objBELUserDetails.FARMERID);
            cmd.Parameters.AddWithValue("@UPDATED_BY  ", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@UPDATED_IP ", objBELUserDetails.UserIP);

            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillCashMemoDtls(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.SALETRANSID,A.GODOWN_ID,E.GODOWN_NAME,CONVERT(VARCHAR(10),A.SALE_DATE,103)SALE_DATE,B.LIC_NO,B.LIC_NO+'('+B.APP_FIRMNAME+')'APP_FIRMNAME,C.Crop_Name,D.Variety_Name,A.LOT_NUMBER,A.BAG_SIZE_KG,A.SALE_NO_OF_BAG,CONVERT(DECIMAL(10,2),CONVERT(DECIMAL(10,2),BAG_SIZE_KG )*CONVERT(DECIMAL(10,2),SALE_NO_OF_BAG )/100) AS BAGS_QTL,A.UPDATED_BY,F.SUPPLY_NAME,A.USER_TYPE  " +
        "FROM Stock_SaleDetails A " +
        "LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON A.SALE_TO = B.LIC_NO " +
        "LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.Crop_Code " +
        "LEFT OUTER JOIN mCROPVARIETY D ON A.CROP_VERID = D.Variety_Code " +
        "LEFT OUTER JOIN Stock_Godown_Master E ON A.GODOWN_ID = E.GODOWN_ID " +
        "LEFT OUTER JOIN Stock_SupplyType F ON A.SUPPLY_TYPE = F.SUPPLY_ID " +
        "WHERE A.CASH_MEMO_NO = @CASH_MEMO_NO " +
        "ORDER BY A.SALETRANSID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELUserDetails.CashMemo_No);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);
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
    public DataSet FillSaleDtls(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.SALETRANSID,A.GODOWN_ID,E.GODOWN_NAME,CONVERT(VARCHAR(10),A.SALE_DATE,103)SALE_DATE,B.LIC_NO,B.LIC_NO+'('+B.APP_FIRMNAME+')'APP_FIRMNAME,C.Crop_Name,D.Variety_Name,A.LOT_NUMBER,A.BAG_SIZE_KG,A.SALE_NO_OF_BAG,CONVERT(DECIMAL(10,2),CONVERT(DECIMAL(10,2),BAG_SIZE_KG )*CONVERT(DECIMAL(10,2),SALE_NO_OF_BAG )/100) AS BAGS_QTL,A.UPDATED_BY " +
        "FROM Stock_SaleDetails A " +
        "LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON A.SALE_TO = B.LIC_NO " +
        "LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.Crop_Code " +
        "LEFT OUTER JOIN mCROPVARIETY D ON A.CROP_VERID = D.Variety_Code " +
        "LEFT OUTER JOIN Stock_Godown_Master E ON A.GODOWN_ID = E.GODOWN_ID " +
        "WHERE A.SALETRANSID = @SALETRANSID " +
        "ORDER BY A.SALETRANSID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@SALETRANSID", objBELUserDetails.SALETRANSID);
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
    public DataSet FillSaleTransDtls(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("Stock_SP_SELCASHMEMO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@SALETRANSID", objBELUserDetails.SALETRANSID);
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
    public void CancelCashMemo(BLL_DropDown objBELUserDetails, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_CANCELCASHMEMO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@SALETRANSID", objBELUserDetails.SALETRANSID);
            cmd.Parameters.AddWithValue("@CANCEL_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@CANCEL_IP", objBELUserDetails.UserIP);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataTable FillDdlGodown(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_BINDGODOWN", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@DISTCODE", objBELUserDetails.DistCode);
            cmd.Parameters.AddWithValue("@USERTYPE", objBELUserDetails.UserType);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
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
    public DataTable FillGrdRecv(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_SearchReceiveDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@godownid", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@fromdt", SqlDbType.DateTime).Value = objBELUserDetails.FROM_DATE;
            cmd.Parameters.AddWithValue("@todt", SqlDbType.DateTime).Value = objBELUserDetails.TO_DATE;
            cmd.Parameters.AddWithValue("@userid", SqlDbType.VarChar).Value = objBELUserDetails.UserID;
            cmd.Parameters.AddWithValue("@season", SqlDbType.Char).Value = objBELUserDetails.SEASON;
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
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
    public void ReturnRecvDtls(BLL_DropDown objBELUserDetails, out int outputvalue)
    {
        outputvalue = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_CancelRecieveReturn", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@RECVTRANSID", objBELUserDetails.RECVTRANSID);
            cmd.Parameters.AddWithValue("@CANCEL_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@CANCEL_IP", objBELUserDetails.UserIP);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            outputvalue = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckDate()
    {
        int Cnt = 0;
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("sp_CHKDATE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }

    //bibhu//
    public DataSet FillRecvBagsByLot(BLL_DropDown objBELUserDetails)    // SEED GROWERTE
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT Bags,convert(VARCHAR(10),Testing_date,120) as Testing_Date,CONVERT(VARCHAR(10),DATEADD(M, 9, Testing_date),120) AS Expire_Date FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Ref_NO=@REF_NO AND LOTNO=@LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
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
    public DataSet CountBagsFromStockStockDetails(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT Count(*) as count FROM Stock_StockDetails WHERE Godown_ID=@Godown_ID AND Lot_No=@LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Godown_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
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
    public DataSet fillBagsFromStockStockDetails(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT Recv_No_Of_Bags FROM Stock_StockDetails WHERE Godown_ID=@Godown_ID AND  Lot_No=@LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@Godown_ID", objBELUserDetails.Godown_ID);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
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
    public DataSet FillRecvBagsByLotByDepartmental(BLL_DropDown objBELUserDetails)    // DEPARTMENTAL FARM
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SP_REVALIDATE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
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
    public DataSet FillRecvBagsByLotByOuat(BLL_DropDown objBELUserDetails)    // OUAT FARM
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT Bags,convert(VARCHAR(10),Testing_date,120) as Testing_Date,CONVERT(VARCHAR(10),DATEADD(M, 9, Testing_date),120) AS Expire_Date FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Name_of_agency=@REF_NO AND LOTNO=@LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
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
    public DataSet FillRecvBagsByLotByMOUAgency(BLL_DropDown objBELUserDetails)    //MOU AGENCY
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT Bags,convert(VARCHAR(10),Testing_date,120) as Testing_Date,CONVERT(VARCHAR(10),DATEADD(M, 9, Testing_date),120) AS Expire_Date FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Name_of_agency=@REF_NO AND LOTNO=@LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
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
    public DataSet FillRecvBagsByLotByScheme(BLL_DropDown objBELUserDetails)    //SCHEME
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        SqlCommand cmd = new SqlCommand("SELECT Bags,convert(VARCHAR(10),Testing_date,120) as Testing_Date,CONVERT(VARCHAR(10),DATEADD(M, 9, Testing_date),120) AS Expire_Date FROM VW_agencyTag WHERE Status='3' AND CLASS_CODE=@CLASS_CODE AND Varity_Code=@VARIETY_CODE AND Name_of_agency=@REF_NO AND LOTNO=@LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@VARIETY_CODE", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@CLASS_CODE", objBELUserDetails.CropClass);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
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
    public DataSet FillCropByCategoryIdPriceList(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT B.Crop_Code,Crop_Name " +
        "FROM mCrop A " +
        "INNER JOIN Stock_Pricelist B ON A.Crop_Code = B.Crop_Code " +
        "WHERE Category_Code = @CROPCATG_ID AND B.F_Year = @F_Year AND B.seasons = @seasons " +
        "ORDER BY A.Crop_Name ASC", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@CROPCATG_ID", objBELUserDetails.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@F_Year", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@seasons", objBELUserDetails.SEASON);
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


    //KHARIF 2017-18 //DONE
    //BIBHU
    public DataSet FillCropCategory(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("sp_FILLCROPCATEGORY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@STATUS", objUserBEL.STATUS);
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
    public void InsertCropCategory(BLL_DropDown objBELUserDetails, out int outputvalue)
    {
        outputvalue = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_INSERTCATEGORY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@CATEGORY_CODES", objBELUserDetails.XML_Value);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@IP", objBELUserDetails.UserIP);
            cmd.Parameters.AddWithValue("@UID", objBELUserDetails.UID);
            cmd.Parameters.AddWithValue("@URL", objBELUserDetails.URL);
            cmd.Parameters.AddWithValue("@DEVICETYPE", objBELUserDetails.DEVICETYPE);
            cmd.Parameters.AddWithValue("@MODEL", objBELUserDetails.MODEL);
            cmd.Parameters.AddWithValue("@MANUFACTURE", objBELUserDetails.MANUFACTURE);
            cmd.Parameters.AddWithValue("@OS", objBELUserDetails.OS);
            cmd.Parameters.AddWithValue("@BWSER", objBELUserDetails.BWSER);
            cmd.Parameters.AddWithValue("@SCREENSIZE", objBELUserDetails.SCREENSIZE);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            outputvalue = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillCrop(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("sp_FILLCROP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@CATEGORY_CODE", objUserBEL.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@STATUS", objUserBEL.STATUS);
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
    public void InsertCrop(BLL_DropDown objBELUserDetails, out int outputvalue)
    {
        outputvalue = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_INSERTCROP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@CROP_CODES", objBELUserDetails.XML_Value);
            cmd.Parameters.AddWithValue("@CATEGORY_CODE", objBELUserDetails.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@IP", objBELUserDetails.UserIP);
            cmd.Parameters.AddWithValue("@UID", objBELUserDetails.UID);
            cmd.Parameters.AddWithValue("@URL", objBELUserDetails.URL);
            cmd.Parameters.AddWithValue("@DEVICETYPE", objBELUserDetails.DEVICETYPE);
            cmd.Parameters.AddWithValue("@MODEL", objBELUserDetails.MODEL);
            cmd.Parameters.AddWithValue("@MANUFACTURE", objBELUserDetails.MANUFACTURE);
            cmd.Parameters.AddWithValue("@OS", objBELUserDetails.OS);
            cmd.Parameters.AddWithValue("@BWSER", objBELUserDetails.BWSER);
            cmd.Parameters.AddWithValue("@SCREENSIZE", objBELUserDetails.SCREENSIZE);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            outputvalue = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillCropVariety(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("sp_FILLVARIETY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            // cmd.Parameters.AddWithValue("@CATEGORY_CODE", objUserBEL.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@STATUS", objUserBEL.STATUS);
            cmd.Parameters.AddWithValue("@ABOVE_10YEAR", objUserBEL.ABOVE10YEAR);
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
    public void InsertVariety(BLL_DropDown objBELUserDetails, out int outputvalue)
    {
        outputvalue = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_INSERTVARIETY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@VARIETY_CODES", objBELUserDetails.XML_Value);
            cmd.Parameters.AddWithValue("@CROP_CODE", objBELUserDetails.CROP_ID);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@IP", objBELUserDetails.UserIP);
            cmd.Parameters.AddWithValue("@UID", objBELUserDetails.UID);
            cmd.Parameters.AddWithValue("@URL", objBELUserDetails.URL);
            cmd.Parameters.AddWithValue("@DEVICETYPE", objBELUserDetails.DEVICETYPE);
            cmd.Parameters.AddWithValue("@MODEL", objBELUserDetails.MODEL);
            cmd.Parameters.AddWithValue("@MANUFACTURE", objBELUserDetails.MANUFACTURE);
            cmd.Parameters.AddWithValue("@OS", objBELUserDetails.OS);
            cmd.Parameters.AddWithValue("@BWSER", objBELUserDetails.BWSER);
            cmd.Parameters.AddWithValue("@SCREENSIZE", objBELUserDetails.SCREENSIZE);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            outputvalue = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet Crop_Bind(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_BindCrop", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
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
    public DataSet Source_Bind(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_BindSource", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
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
    public DataSet Variety_Bind(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_BINDVARIETY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@ABOVE10YR", objUserBEL.ABOVE10YEAR);
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
    public void InsertPriceListFordept(BLL_DropDown objBELUserDetails, out int outputvalue)
    {
        outputvalue = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_INSERTPRICEDTLS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@VARIETY_CODES", objBELUserDetails.XML_Value);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@CROP_CODE", objBELUserDetails.CROP_ID);
            cmd.Parameters.AddWithValue("@Variety_Age", objBELUserDetails.ABOVE10YEAR);
            cmd.Parameters.AddWithValue("@Source", objBELUserDetails.RECEIVE_UNITCD);
            cmd.Parameters.AddWithValue("@loadingcharge", objBELUserDetails.loadingcharges);
            cmd.Parameters.AddWithValue("@Transporatation", objBELUserDetails.Transportation);
            cmd.Parameters.AddWithValue("@Dealercommission", objBELUserDetails.Dealercommsiosn);
            cmd.Parameters.AddWithValue("@pacsCommsiosion", objBELUserDetails.Pacscommsiosn);
            cmd.Parameters.AddWithValue("@allincostamnt", objBELUserDetails.allincostprice);
            cmd.Parameters.AddWithValue("@goisubsidy", objBELUserDetails.goisubsidy);
            cmd.Parameters.AddWithValue("@spsubsidy ", objBELUserDetails.spsubsidy);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@IP", objBELUserDetails.UserIP);
            cmd.Parameters.AddWithValue("@UID", objBELUserDetails.UID);
            cmd.Parameters.AddWithValue("@URL", objBELUserDetails.URL);
            cmd.Parameters.AddWithValue("@DEVICETYPE", objBELUserDetails.DEVICETYPE);
            cmd.Parameters.AddWithValue("@MODEL", objBELUserDetails.MODEL);
            cmd.Parameters.AddWithValue("@MANUFACTURE", objBELUserDetails.MANUFACTURE);
            cmd.Parameters.AddWithValue("@OS", objBELUserDetails.OS);
            cmd.Parameters.AddWithValue("@BWSER", objBELUserDetails.BWSER);
            cmd.Parameters.AddWithValue("@SCREENSIZE", objBELUserDetails.SCREENSIZE);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            outputvalue = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public string GetClass(BLL_DropDown objUserBEL)
    {
        string ClassName = "";
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT TOP(1)CLASS FROM STOCK_STOCKDETAILS WHERE replace(replace(replace(replace(Lot_No,char(9),''),char(10),''),char(13),''),char(32),'')  = @LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LOT_NO", objUserBEL.LOT_NO);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objUserBEL.SEASON);
            con.Open();
            ClassName = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return ClassName;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return "";
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }

    public DataSet FillDealerDtls(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT A.LICENCE_NO,B.APP_FIRMNAME,C.Crop_Name,D.Variety_Name,A.LOT_NO,A.BAG_SIZE_IN_KG,A.RECV_NO_OF_BAGS,A.AVL_NO_OF_BAGS,A.AVL_QUANTITY FROM STOCK_DEALERSTOCK A " +
        "INNER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON A.LICENCE_NO = B.LIC_NO " +
        "INNER JOIN mCrop C ON A.CROP_ID = C.Crop_Code " +
        "INNER JOIN mCropVariety D ON A.CROP_VERID = D.Variety_Code " +
        "WHERE LIC_NO = @LIC_NO AND A.USERID = @USERID AND FIN_YR = (SELECT FIN_YR FROM mFINYR WHERE IS_ACTIVE = 1) AND SEASSION = (SELECT SHORT_NAME FROM mSEASSION WHERE IS_ACTIVE = 1) ORDER BY D.Variety_Name,A.LOT_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELUserDetails.LIC_NO);
            cmd.Parameters.AddWithValue("@USERID", objBELUserDetails.UPDATED_BY);
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
    public DataSet FillLotTransDtls(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.SALETRANSID,B.Godown_Name,CONVERT(VARCHAR(10),A.SALE_DATE,103)SALE_DATE,A.DD_NUMBER,A.DD_AMOUNT,A.CASH_MEMO_NO,A.PRICE_QTL,A.AMOUNT,A.BAG_SIZE_KG,A.SALE_NO_OF_BAG,A.STATUS,A.UPDATED_ON FROM Stock_SaleDetails A " +
        "INNER JOIN Stock_Godown_Master B ON A.GODOWN_ID = B.Godown_ID " +
        "WHERE SALE_TO = @LIC_NO AND LOT_NUMBER = @LOT_NO AND A.UPDATED_BY = @USERID AND F_YEAR = (SELECT FIN_YR FROM mFINYR WHERE IS_ACTIVE = 1) AND SEASONS = (SELECT SHORT_NAME FROM mSEASSION WHERE IS_ACTIVE = 1)", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@LIC_NO", objBELUserDetails.LIC_NO);
            cmd.Parameters.AddWithValue("@USERID", objBELUserDetails.UPDATED_BY);
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
    public void ReturnToOSSC(BLL_DropDown objBELUserDetails, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_RETURNSTOCKDEALER", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@SALETRANSID", objBELUserDetails.SALETRANSID);
            cmd.Parameters.AddWithValue("@CANCEL_BY", objBELUserDetails.UPDATED_BY);
            cmd.Parameters.AddWithValue("@CANCEL_IP", objBELUserDetails.UserIP);
            cmd.Parameters.AddWithValue("@RETURN_NO_OF_BAGS", objBELUserDetails.Recv_No_Of_Bags);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataTable GetDataMaster()
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT STATE_CODE,DISTRICT_CODE,TEH_CODE,BLK_CODE,SECTOR_CODE,DEPT_CODE,PROJECT_CODE,CNT1,CNT2,CNT3,CNT4,CNT5,DATAPORTMODE,MODEDESC,DATA_LVL_CODE,YR,MNTH FROM STOCK_DISTCROPFARMER", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }
    public DataSet FillSeedWastage(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_SEEDWASTAGE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION", objBELUserDetails.SEASON);
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
    public DataSet FillSeedWastageDealers(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.LICENCE_NO,B.APP_FIRMNAME,C.dist_name,D.DAO_NAME,E.Category_Name,F.Crop_Name,G.Variety_Name,A.LOT_NO,SUM(A.AVL_QUANTITY)AVL_QUANTITY FROM STOCK_DEALERSTOCK A " +
        "INNER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON A.LICENCE_NO = B.LIC_NO " +
        "INNER JOIN [DAFPSEED].[DBO].[DIST] C ON B.DIST_CODE = C.dist_code " +
        "INNER JOIN [DAFPSEED].[DBO].[FMN_DDA_DAO_LIST] D ON C.dist_code = D.DDA_CODE AND B.DAO_CD = D.DAO_CODE " +
        "INNER JOIN mCropCategory E ON A.CROPCATG_ID = E.Category_Code " +
        "INNER JOIN mCrop F ON A.CROP_ID = F.Crop_Code " +
        "INNER JOIN mCropVariety G ON A.CROP_VERID = G.Variety_Code " +
        "WHERE AVL_QUANTITY > 0 AND CONVERT(DATE,EXPIRY_DATE,103) < CONVERT(DATE,CURRENT_TIMESTAMP,103) AND FIN_YR = @FIN_YEAR AND SEASSION = @SEASSION " +
        "GROUP BY A.LICENCE_NO,B.APP_FIRMNAME,C.dist_name,D.DAO_NAME,E.Category_Name,F.Crop_Name,G.Variety_Name,A.LOT_NO " +
        "ORDER BY C.dist_name,A.LICENCE_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION", objUserBEL.SEASON);
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
    public DataSet FillSeedWastageSupplier(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT C.Dist_Name,B.Godown_Name,D.Category_Name,E.Crop_Name,SUM(A.Avl_Quantity)Avl_Quantity FROM Stock_StockDetails A " +
        "INNER JOIN Stock_Godown_Master B ON A.Godown_ID = B.Godown_ID " +
        "INNER JOIN Stock_District C ON A.Dist_Code = C.Dist_Code " +
        "INNER JOIN mCropCategory D ON A.CROPCATG_ID = D.Category_Code " +
        "INNER JOIN mCrop E ON A.CROP_ID = E.Crop_Code " +
        "INNER JOIN mCropVariety F ON A.CROP_VERID = F.Variety_Code " +
        "WHERE A.FIN_YR = @FIN_YEAR AND A.SEASSION_NAME = @SEASSION AND A.Avl_Quantity > 0 AND CONVERT(DATE,A.EXPIRY_DATE,103) < CONVERT(DATE,CURRENT_TIMESTAMP,103)	" +
        "GROUP BY C.Dist_Name,B.Godown_Name,D.Category_Name,E.Crop_Name " +
        "ORDER BY C.Dist_Name,B.Godown_Name,D.Category_Name,E.Crop_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION", objUserBEL.SEASON);
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
    //public DataSet GetIncentive(BLL_DropDown objUserBEL)
    //{
    //    SqlConnection con = new SqlConnection(_connstrOssopca);
    //    // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
    //    String SqlSel = "";
    //    //Status,ProcessedArea,SG_Name,Ref_No,Agency_Secter,Father_husband_name,Varity_Code,Dist_name,Class_code,Varity_Name,villg_name,Farmerid,Dist_code

    //    SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,LotNo,Ref_No,Varity_Code,Varity_Name,Class_code,ProcessedArea,Dist_code,Farmerid,Inspected_Area FROM dbo.incentive WHERE Farmerid IS NOT NULL";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
    //    //SqlSel = "SELECT UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY FROM mINCENTIVE " +
    //    //         "WHERE FIN_YR = @FIN_YR AND SEASON = @SEASON ORDER BY UPDATED_ON,FARMER_NAME ";
    //    SqlCommand cmd = new SqlCommand(SqlSel, con);
    //    cmd.CommandType = CommandType.Text;

    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
    //        //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    //public DataSet FillIncentive(BLL_DropDown objUserBEL)
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);
    //    // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
    //    String SqlSel = "";

    //    //SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,Lot_No,Ref_No,variety,Varity_Name,Class_code,ProcessedArea FROM dbo.incentive";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
    //    SqlSel = "SELECT FARMER_ID,UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_QTY,SUBSIDISED_QTY,INSPECTED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY FROM mINCENTIVE " +
    //              "WHERE FIN_YR = @FIN_YR AND LEN(FARMER_ID) > 0 ORDER BY FARMER_ID,PROCESSED_QTY,INSPECTED_AREA DESC";
    //    SqlCommand cmd = new SqlCommand(SqlSel, con);
    //    cmd.CommandType = CommandType.Text;

    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
    //        //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    //public void InsertIncentive(BLL_DropDown objUserBEL, out int Val)
    //{
    //    Val = 0;
    //    SqlConnection con = new SqlConnection(_connstrStock);
    //    SqlCommand cmd = new SqlCommand("Stock_SP_Incentive1", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.CommandTimeout = 0;
    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
    //        //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
    //        cmd.Parameters.AddWithValue("@VALUES", objUserBEL.XML_Value);
    //        cmd.Parameters.Add("@Val", SqlDbType.Int);
    //        cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
    //        con.Close();
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    public DataSet GetIncentive(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";
        //Status,ProcessedArea,SG_Name,Ref_No,Agency_Secter,Father_husband_name,Varity_Code,Dist_name,Class_code,Varity_Name,villg_name,Farmerid,Dist_code

        SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,LotNo,Ref_No,Varity_Code,Varity_Name,Class_code,ProcessedArea,Dist_code,Farmerid,Inspected_Area,(ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.incentive WHERE Farmerid IS NOT NULL ORDER BY Farmerid,(ProcessedArea/Inspected_Area)";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        //SqlSel = "SELECT UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY FROM mINCENTIVE " +
        //         "WHERE FIN_YR = @FIN_YR AND SEASON = @SEASON ORDER BY UPDATED_ON,FARMER_NAME ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet GetIncentivePulse(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";
        //Status,ProcessedArea,SG_Name,Ref_No,Agency_Secter,Father_husband_name,Varity_Code,Dist_name,Class_code,Varity_Name,villg_name,Farmerid,Dist_code

        SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,LotNo,Ref_No,Varity_Code,Varity_Name,Class_code,ProcessedArea,Dist_code,Farmerid,Inspected_Area,(ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.incentivePulse WHERE Farmerid IS NOT NULL ORDER BY Farmerid,(ProcessedArea/Inspected_Area)";//and crop_code in ('C011','C012','C015','C022')--Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        //SqlSel = "SELECT UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY FROM mINCENTIVE " +
        //         "WHERE FIN_YR = @FIN_YR AND SEASON = @SEASON ORDER BY UPDATED_ON,FARMER_NAME ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet GetIncentiveOilSeed(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";
        //Status,ProcessedArea,SG_Name,Ref_No,Agency_Secter,Father_husband_name,Varity_Code,Dist_name,Class_code,Varity_Name,villg_name,Farmerid,Dist_code

        SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,LotNo,Ref_No,Varity_Code,Varity_Name,Class_code,ProcessedArea,Dist_code,Farmerid,Inspected_Area,(ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.VwIncentiveOilseed WHERE Farmerid IS NOT NULL and crop_code in ('C026','C027','C033','C029')  ORDER BY Farmerid,(ProcessedArea/Inspected_Area)";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        //SqlSel = "SELECT UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY FROM mINCENTIVE " +
        //         "WHERE FIN_YR = @FIN_YR AND SEASON = @SEASON ORDER BY UPDATED_ON,FARMER_NAME ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet GetIncentiveOilSeed_OUAT(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";
        //Status,ProcessedArea,SG_Name,Ref_No,Agency_Secter,Father_husband_name,Varity_Code,Dist_name,Class_code,Varity_Name,villg_name,Farmerid,Dist_code

        SqlSel = "SELECT SG_Name, Father_husband_name, villg_name, Dist_name, LotNo, Ref_No, Varity_Code, Varity_Name, Class_code, ProcessedArea, Dist_code, Farmerid, Inspected_Area, (ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.VwIncentiveOilseedOUAT WHERE Farmerid IS NOT NULL and crop_code in ('C026','C027','C033','C028','C029','C036') AND SUBSTRING(REF_NO,8,7) = @FIN_YR ORDER BY Farmerid,(ProcessedArea/Inspected_Area)";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        //SqlSel = "SELECT UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY FROM mINCENTIVE " +
        //         "WHERE FIN_YR = @FIN_YR AND SEASON = @SEASON ORDER BY UPDATED_ON,FARMER_NAME ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet GetIncentiveOilSeedAll(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrOssopca);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";
        //Status,ProcessedArea,SG_Name,Ref_No,Agency_Secter,Father_husband_name,Varity_Code,Dist_name,Class_code,Varity_Name,villg_name,Farmerid,Dist_code

        SqlSel = "SELECT SG_Name, Father_husband_name, villg_name, Dist_name, LotNo, Ref_No, Varity_Code, Varity_Name, Class_code, ProcessedArea, Dist_code, Farmerid, Inspected_Area, (ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.VwIncentiveOilseedAll WHERE Farmerid IS NOT NULL and crop_code in ('C026') AND SUBSTRING(REF_NO,8,7) = @FIN_YR ORDER BY Farmerid,(ProcessedArea/Inspected_Area)";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        //SqlSel = "SELECT UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY FROM mINCENTIVE " +
        //         "WHERE FIN_YR = @FIN_YR AND SEASON = @SEASON ORDER BY UPDATED_ON,FARMER_NAME ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet FillIncentive(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";

        //SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,Lot_No,Ref_No,variety,Varity_Name,Class_code,ProcessedArea FROM dbo.incentive";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        SqlSel = "SELECT UPPER(FARMER_ID)FARMER_ID,UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_QTY,SUBSIDISED_QTY,INSPECTED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY,SEASON,QTL_HA,ACC_HOLDERNAME,  '['+ACC_NO+']' as ACC_NO, BANK_ID,B.VCHBANKNAME, BRANCH_ID,B.VCHBRANCHNAME, A.IFSC_CODE FROM mINCENTIVE A " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_BANKBRANCH B ON A.BRANCH_ID = B.intBranchId " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_FARMERBANK C ON B.INTBANKID = C.INTID " +
         "WHERE FIN_YR = @FIN_YR AND LEN(FARMER_ID) > 0  ORDER BY FARMER_ID,QTL_HA DESC";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet FillIncentive10(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";

        //SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,Lot_No,Ref_No,variety,Varity_Name,Class_code,ProcessedArea FROM dbo.incentive";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        SqlSel = "SELECT UPPER(FARMER_ID)FARMER_ID,UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_QTY,SUBSIDISED_QTY,INSPECTED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY,SEASON,QTL_HA,ACC_HOLDERNAME,  '['+ACC_NO+']' as ACC_NO, BANK_ID,B.VCHBANKNAME, BRANCH_ID,B.VCHBRANCHNAME, A.IFSC_CODE FROM mINCENTIVE A " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_BANKBRANCH B ON A.BRANCH_ID = B.intBranchId " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_FARMERBANK C ON B.INTBANKID = C.INTID " +
         "WHERE FIN_YR = @FIN_YR AND LEN(FARMER_ID) > 0 AND FARMER_ID IN ('BAR/72923','BHA/66194','KOR/1712','KOR/1704','NAB/34770','NAB/40802') ORDER BY FARMER_ID,QTL_HA DESC";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet FillIncentivePulse(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";

        //SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,Lot_No,Ref_No,variety,Varity_Name,Class_code,ProcessedArea FROM dbo.incentive";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        SqlSel = "SELECT FARMER_ID,UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_QTY,SUBSIDISED_QTY,INSPECTED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY,SEASON,QTL_HA,ACC_HOLDERNAME, '['+ACC_NO+']' as ACC_NO, BANK_ID,B.VCHBANKNAME, BRANCH_ID,B.VCHBRANCHNAME, A.IFSC_CODE FROM mINCENTIVE1 A " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_BANKBRANCH B ON A.BRANCH_ID = B.intBranchId " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_FARMERBANK C ON B.INTBANKID = C.INTID " +
        "WHERE FIN_YR = @FIN_YR AND LEN(FARMER_ID) > 0 ORDER BY FARMER_ID,QTL_HA DESC";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
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
    public DataSet FillIncentiveOilSeed(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "FillIncentiveOilSeed";
        //SqlSel = "SELECT FARMER_ID,UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_QTY,SUBSIDISED_QTY,INSPECTED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY,SEASON,QTL_HA,ACC_HOLDERNAME, '['+ACC_NO+']' as ACC_NO, BANK_ID,B.VCHBANKNAME, BRANCH_ID,B.VCHBRANCHNAME, A.IFSC_CODE, A.OSSC_STATUS, A.PAY_STATUS FROM mINCENTIVE2 A " +
        //"LEFT JOIN FARMERDB.DBO.M_PDS_BANKBRANCH B ON A.BRANCH_ID = B.intBranchId " +
        //"LEFT JOIN FARMERDB.DBO.M_PDS_FARMERBANK C ON B.INTBANKID = C.INTID " +
        //"WHERE FIN_YR = @FIN_YR AND LEN(FARMER_ID) > 0 ORDER BY FARMER_ID,QTL_HA DESC";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet FillIncentiveOilSeed_OUAT(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "SELECT FARMER_ID,UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_QTY,SUBSIDISED_QTY,INSPECTED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY,SEASON,QTL_HA,ACC_HOLDERNAME, '['+ACC_NO+']' as ACC_NO, BANK_ID,B.VCHBANKNAME, BRANCH_ID,B.VCHBRANCHNAME, A.IFSC_CODE, A.OSSC_STATUS, A.PAY_STATUS FROM mINCENTIVE2_OUAT A " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_BANKBRANCH B ON A.BRANCH_ID = B.intBranchId " +
        "LEFT JOIN FARMERDB.DBO.M_PDS_FARMERBANK C ON B.INTBANKID = C.INTID " +
        "WHERE FIN_YR = @FIN_YR AND LEN(FARMER_ID) > 0 ORDER BY FARMER_ID,QTL_HA DESC";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
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
    public DataSet FillIncentiveOilSeedAll(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "FillIncentiveOilSeedAll";
        //SqlSel = "SELECT FARMER_ID,UPPER(FARMER_NAME)FARMER_NAME,UPPER(FATHER_HUSBAND_NAME)FATHER_HUSBAND_NAME,DIST_NAME,UPPER(VILLG_NAME)VILLG_NAME,LOT_NO,REF_NO,VARIETY_NAME,PROCESSED_QTY,SUBSIDISED_QTY,INSPECTED_AREA,SUBSIDISED_AREA,FARMER_SUBSIDY,OSSC_SUBSIDY,SEASON,QTL_HA,ACC_HOLDERNAME, '['+ACC_NO+']' as ACC_NO, BANK_ID,B.VCHBANKNAME, BRANCH_ID,B.VCHBRANCHNAME, A.IFSC_CODE, A.OSSC_STATUS, A.PAY_STATUS FROM mINCENTIVE3 A " +
        //"LEFT JOIN FARMERDB.DBO.M_PDS_BANKBRANCH B ON A.BRANCH_ID = B.intBranchId " +
        //"LEFT JOIN FARMERDB.DBO.M_PDS_FARMERBANK C ON B.INTBANKID = C.INTID " +
        //"WHERE FIN_YR = @FIN_YR AND LEN(FARMER_ID) > 0 ORDER BY FARMER_ID,QTL_HA DESC";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public void InsertIncentive(BLL_DropDown objUserBEL, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_Incentive1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);            
            cmd.Parameters.AddWithValue("@VALUES", objUserBEL.XML_Value);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void InsertIncentivePulse(BLL_DropDown objUserBEL, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_IncentivePulse", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);            
            cmd.Parameters.AddWithValue("@VALUES", objUserBEL.XML_Value);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void InsertIncentiveOilSeed(BLL_DropDown objUserBEL, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_IncentiveOilSeed", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);            
            cmd.Parameters.AddWithValue("@VALUES", objUserBEL.XML_Value);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void InsertIncentiveOilSeedOUAT(BLL_DropDown objUserBEL, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_IncentiveOilSeed_OUAT", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);            
            cmd.Parameters.AddWithValue("@VALUES", objUserBEL.XML_Value);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void InsertIncentiveOilSeedAll(BLL_DropDown objUserBEL, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_IncentiveOilSeedAll", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);            
            cmd.Parameters.AddWithValue("@VALUES", objUserBEL.XML_Value);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillVarietiesWithIn10Years(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        // SqlCommand cmd = new SqlCommand("SELECT DISTINCT Ref_NO,SG_Name AS NAME FROM VW_agencyTag where Status='3' AND Agency_Secter=@USER_TYPE AND District_code=@DIST_CODE AND SUBSTRING(REF_NO,8,7) =@FIN_YR AND session_status = 1 ORDER BY SG_Name ASC", con);
        String SqlSel = "";

        //SqlSel = "SELECT SG_Name,Father_husband_name,villg_name,Dist_name,Lot_No,Ref_No,variety,Varity_Name,Class_code,ProcessedArea FROM dbo.incentive";//Farmerid,SG_Name,Father_husband_name,Lot_No,Ref_No,Dist_name,villg_name,variety,Varity_Name,Class_code,ProcessedArea
        SqlSel = "SELECT DISTINCT B.Variety_Name FROM STOCK_PRICELIST A " +
        "INNER JOIN mCropVariety B ON A.CROP_VCODE = B.Variety_Code " +
        "WHERE A.VARIETY_AFTER_10YEAR = 0 AND A.F_YEAR = @FIN_YR AND A.Crop_Code = 'C002' AND A.RECEIVE_UNITCD = '0000' " +
        "ORDER BY B.Variety_Name ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            //cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet FillPACS(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        //String SqlSel = "";

        //SqlSel = "SELECT DISTINCT DIST_CODE,LIC_NO,APP_FIRMNAME,APPMOB_NO FROM SEED_LIC_DIST A INNER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID WHERE A.ACC_NO IS NULL AND B.COMP_NAME='OSSC' AND APP_STATUS = 'A' AND APR_UPTO > GETDATE() AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND DIST_CODE = @DIST_CODE ORDER BY APP_FIRMNAME ";
        SqlCommand cmd = new SqlCommand("sp_UPDATEBANKDEALER", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objUserBEL.DistCode);
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
    public int UpdateDealerBankDetails(BLL_DropDown objBELDIST)
    {
        int Val = 0;
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("sp_UPDBANKDETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@AADHAAR_NO", objBELDIST.AADHAAR_NO);
            cmd.Parameters.AddWithValue("@ACC_HOLDERNAME", objBELDIST.ACC_HOLDERNAME);
            cmd.Parameters.AddWithValue("@ACC_NO", objBELDIST.ACC_NO);
            cmd.Parameters.AddWithValue("@BANK_ID", objBELDIST.BANK_ID);
            cmd.Parameters.AddWithValue("@BRANCH_ID", objBELDIST.BRANCH_ID);
            cmd.Parameters.AddWithValue("@IFSC_CODE", objBELDIST.IFSC_CODE);
            cmd.Parameters.AddWithValue("@BANK_UPDATED_BY", objBELDIST.UPDATED_BY);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
            return Val;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillUpdatedPACS(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        String SqlSel = "";
        if (objUserBEL.DistCode == "--All--")
        {
            objUserBEL.DistCode = null;
        }
        //if (objUserBEL.UserType == "PACS")
        //{
        //    objUserBEL.UserType = "1";
        //}
        //if (objUserBEL.UserType == "Dealer")
        //{
        //    objUserBEL.UserType = "2";
        //}
        //if (objUserBEL.UserType == "ALL")
        //{
        //    objUserBEL.UserType = "3";
        //}

        SqlCommand cmd = new SqlCommand("STOCK_UPDATEDPACS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objUserBEL.DistCode);
            //cmd.Parameters.AddWithValue("@IS_PACS", objUserBEL.UserType);
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
    public DataSet FillDetailsPACS(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        String SqlSel = "";

        SqlSel = "SELECT DISTINCT DIST_CODE,LIC_NO,APP_FIRMNAME,A.ACC_NO,A.AADHAAR_NO,A.BANK_ID,A.BRANCH_ID,A.IFSC_CODE FROM SEED_LIC_DIST A " +
        "INNER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
        "WHERE A.ACC_NO IS NOT NULL AND SENT_STATUS IS NULL AND B.COMP_NAME='OSSC' AND APP_STATUS = 'A' AND APR_UPTO > GETDATE() AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND A.LIC_NO = @LICENCENO ORDER BY APP_FIRMNAME ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@LICENCENO", objUserBEL.LICENCENO);
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
    public DataSet FillVerifiedPACS(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        if (objUserBEL.DistCode == "--All--")
        {
            objUserBEL.DistCode = null;
        }
        if (objUserBEL.UserType == "PACS")
        {
            objUserBEL.UserType = "1";
        }
        if (objUserBEL.UserType == "Dealer")
        {
            objUserBEL.UserType = "2";
        }
        if (objUserBEL.UserType == "ALL")
        {
            objUserBEL.UserType = "3";
        }

        SqlCommand cmd = new SqlCommand("STOCK_VERIFIEDPACS", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objUserBEL.DistCode);
            cmd.Parameters.AddWithValue("@IS_PACS", objUserBEL.UserType);
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
    //public DataSet FillDealerRequest()
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);

    //    SqlCommand cmd = new SqlCommand("SELECT B.CPAID,SUM(B.Amount)AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A " +
    //    "INNER JOIN [FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID " +
    //    "WHERE Fin_Year = '2018-19' " +
    //    "GROUP BY B.CPAID", con);
    //    cmd.CommandType = CommandType.Text;

    //    try
    //    {
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    //public DataSet FillDealerRequest(BLL_DropDown objUserBEL)
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);

    //    SqlCommand cmd = new SqlCommand("SELECT B.CPAID,SUM(B.Amount)AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A " +
    //    "INNER JOIN [FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID " +
    //    "WHERE Fin_Year = '2018-19' AND Approve='0' AND scheme_code = @scheme_code " +
    //    "GROUP BY B.CPAID", con);
    //    cmd.CommandType = CommandType.Text;

    //    try
    //    {
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        cmd.Parameters.AddWithValue("@scheme_code", objUserBEL.scheme_code);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    //public DataSet FillDealerRequestOSSC()
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);

    //    SqlCommand cmd = new SqlCommand("SELECT B.CPAID,SUM(B.Amount)AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A " +
    //    "INNER JOIN [FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID " +
    //    "WHERE Fin_Year = '2018-19' AND Approve='1' " +
    //    "GROUP BY B.CPAID", con);
    //    cmd.CommandType = CommandType.Text;
    //    try
    //    {
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    //public DataSet FillDealerSaleDtls(BLL_DropDown objUserBEL)
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);

    //    SqlCommand cmd = new SqlCommand("SELECT AA.NewTrans +' on dated '+convert(varchar(10),UPDATED_ON,103) NewTrans,B.UPDATED_BY LIC_NO,aa.AccountNumber,C.Crop_Name,D.Variety_Name,B.TOT_QTL,B.ADMISSIBLE_SUBSIDY,B.TOT_SUB_AMOUNT_GOI,B.TOT_SUB_AMOUNT_SP,AA.IFSC,AA.VCHMOBILENO FROM ( " +
    //    "SELECT VCHMOBILENO,IFSC,TransactionID,AccountNumber,case when SUBSTRING(TransactionID,22,1)='O' then left(TransactionID,21) when SUBSTRING(TransactionID,21,1)='O' then left(TransactionID,20)  when SUBSTRING(TransactionID,20,1)='O' then left(TransactionID,19) when SUBSTRING(TransactionID,23,1)='O' then left(TransactionID,22) end as NewTrans  FROM [FARMERDB].[DBO].[Request_Payment_tblTransaction] Y INNER JOIN [FARMERDB].[DBO].M_FARMER_REGISTRATION Z ON Y.FarmerID=Z.VCHFARMERCODE " +
    //    "WHERE CPAID = @CPAID)AA " +
    //    "INNER JOIN [STOCK].[DBO].[STOCK_FARMER] B ON AA.NewTrans COLLATE SQL_Latin1_General_CP1_CI_AS = B.TRANSACTION_ID " +
    //    "INNER JOIN [STOCK].[DBO].[mCrop] C ON B.CROP_ID = C.CROP_CODE " +
    //    "INNER JOIN [STOCK].[DBO].[mCropVariety] D ON B.CROP_VERID = D.Variety_Code " +
    //    "ORDER BY B.UPDATED_ON", con);
    //    cmd.CommandType = CommandType.Text;

    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    //public DataSet Fillouter(BLL_DropDown objUserBEL)
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);

    //    SqlCommand cmd = new SqlCommand("SELECT AA.NewTrans +' on dated '+convert(varchar(10),b.UPDATED_ON,103) as NewTrans,B.UPDATED_BY LIC_NO,aa.AccountNumber,e.APP_FIRMNAME,C.Crop_Name,D.Variety_Name,B.TOT_QTL,B.ADMISSIBLE_SUBSIDY,B.TOT_SUB_AMOUNT_GOI,B.TOT_SUB_AMOUNT_SP,AA.IFSC,AA.VCHMOBILENO FROM (    SELECT  VCHMOBILENO,IFSC,TransactionID, AccountNumber,case when SUBSTRING(TransactionID, 22, 1) = 'O' then left(TransactionID, 21) when SUBSTRING(TransactionID, 21, 1) = 'O' then left(TransactionID, 20)  when SUBSTRING(TransactionID, 20, 1) = 'O' then left(TransactionID, 19) when SUBSTRING(TransactionID, 23, 1) = 'O' then left(TransactionID, 22) end as NewTrans  FROM[FARMERDB].[DBO].[Request_Payment_tblTransaction] Y INNER JOIN [FARMERDB].[DBO].M_FARMER_REGISTRATION Z ON Y.FarmerID=Z.VCHFARMERCODE      WHERE CPAID = @CPAID)AA        INNER JOIN[STOCK].[DBO].[STOCK_FARMER] B ON AA.NewTrans COLLATE SQL_Latin1_General_CP1_CI_AS = B.TRANSACTION_ID        INNER JOIN[STOCK].[DBO].[mCrop]    C ON B.CROP_ID = C.CROP_CODE INNER JOIN[STOCK].[DBO].[mCropVariety]    D ON B.CROP_VERID = D.Variety_Code inner join dafpSeed.dbo.SEED_LIC_DIST e on b.UPDATED_BY= e.LIC_NO ORDER BY B.UPDATED_ON", con);

    //    cmd.CommandType = CommandType.Text;

    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
    //        SqlDataAdapter ada = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        ada.Fill(ds);
    //        return ds;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    //public int UpdateAppr(BLL_DropDown objUserBEL)
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("update [FARMERDB].[DBO].[Request_Payment_tblHeader] set Approve='1' where CPAID = @CPAID", con);
    //    cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
    //    int i = cmd.ExecuteNonQuery();
    //    return i;
    //}
    //public int UpdateApprOSSC(BLL_DropDown objUserBEL)
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("update [FARMERDB].[DBO].[Request_Payment_tblHeader] set Approve='2' where CPAID = @CPAID", con);
    //    cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
    //    int i = cmd.ExecuteNonQuery();
    //    return i;
    //}
    public DataSet FillDealerRequest()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.CPAID,SUM(B.Amount)AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A " +
        "INNER JOIN [FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID " +
        "WHERE Fin_Year = '2019-20' " +
        "GROUP BY B.CPAID", con);
        cmd.CommandType = CommandType.Text;

        try
        {
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
    public DataSet FillDealerRequest(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        //SqlCommand cmd = new SqlCommand("SELECT B.CPAID,cast(SUM(B.Amount) as decimal(10,2))AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A "+
        //"INNER JOIN[FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID "+
        //"WHERE A.Fin_Year in ('2018-19', '2019-20', '2020-21', '2021-22') AND A.Approve = '0' AND A.scheme_code = @scheme_code " +
        //"GROUP BY B.CPAID " +
        //"UNION ALL " +
        //"SELECT B.CPAID, cast(SUM(B.Amount) as decimal(10, 2))AMOUNT, COUNT(*)NOOFTRANS FROM[FARMERDB].[DBO].[Request_Payment_tblHeader_Rabi] A " +
        //"INNER JOIN[FARMERDB].[DBO].[Request_Payment_tblTransaction_Rabi] B ON A.CPAID = B.CPAID " +
        //"WHERE A.Fin_Year in ('2018-19', '2019-20', '2020-21', '2021-22') AND A.Approve = '0' AND A.scheme_code = @scheme_code " +
        //"GROUP BY B.CPAID", con);
        SqlCommand cmd = new SqlCommand("SP_DEALERREQ", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@scheme_code", objUserBEL.scheme_code);
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
    public DataSet FillDealerRequestOSSC()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        //SqlCommand cmd = new SqlCommand("SELECT B.CPAID,CAST(SUM(B.Amount)AS DECIMAL(10,2))AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A "+
        //"INNER JOIN[FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID " +
        //"WHERE Fin_Year in ('2018-19', '2019-20', '2020-21', '2021-22') AND Approve = '1' " +
        //"GROUP BY B.CPAID " +
        //"UNION ALL " +
        //"SELECT B.CPAID, CAST(SUM(B.Amount)AS DECIMAL(10, 2))AMOUNT, COUNT(*)NOOFTRANS FROM[FARMERDB].[DBO].[Request_Payment_tblHeader_Rabi] A " +
        //"INNER JOIN[FARMERDB].[DBO].[Request_Payment_tblTransaction_Rabi] B ON A.CPAID = B.CPAID " +
        //"WHERE Fin_Year in ('2018-19', '2019-20', '2020-21', '2021-22') AND Approve = '1' " +
        //"GROUP BY B.CPAID", con);

        SqlCommand cmd = new SqlCommand("SP_DEALERREQ_OSSC", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
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
    public DataSet FillDealerSaleDtls(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        //SqlCommand cmd = new SqlCommand("SELECT AA.NewTrans +' on dated '+convert(varchar(10),UPDATED_ON,103) NewTrans,B.UPDATED_BY LIC_NO,aa.AccountNumber,C.Crop_Name,D.Variety_Name,B.TOT_QTL,B.ADMISSIBLE_SUBSIDY,B.TOT_SUB_AMOUNT_GOI,B.TOT_SUB_AMOUNT_SP,AA.IFSC,AA.VCHMOBILENO,B.FARMER_ID,E.VCHFARMERNAME,AA.Amount FROM ( " +
        //"SELECT Amount,VCHMOBILENO,IFSC,TransactionID,AccountNumber,case when SUBSTRING(TransactionID,22,1)='O' then left(TransactionID,21) when SUBSTRING(TransactionID,21,1)='O' then left(TransactionID,20)  when SUBSTRING(TransactionID,20,1)='O' then left(TransactionID,19) when SUBSTRING(TransactionID,23,1)='O' then left(TransactionID,22) end as NewTrans  FROM [FARMERDB].[DBO].[Request_Payment_tblTransaction] Y INNER JOIN [FARMERDB].[DBO].M_FARMER_REGISTRATION Z ON Y.FarmerID=Z.VCHFARMERCODE " +
        //"WHERE CPAID = @CPAID)AA " +
        //"INNER JOIN [STOCK].[DBO].[STOCK_FARMER] B ON AA.NewTrans COLLATE SQL_Latin1_General_CP1_CI_AS = B.TRANSACTION_ID " +
        //"INNER JOIN [STOCK].[DBO].[mCrop] C ON B.CROP_ID = C.CROP_CODE " +
        //"INNER JOIN [STOCK].[DBO].[mCropVariety] D ON B.CROP_VERID = D.Variety_Code " +
        //"inner join FARMERDB.dbo.M_FARMER_REGISTRATION E on b.FARMER_ID=e.NICFARMERID collate Latin1_General_CI_AI " +
        //"ORDER BY B.UPDATED_ON", con);
        SqlCommand cmd = new SqlCommand("SP_DEALESALEDTL", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
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
    public DataSet Fillouter(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        ////SqlCommand cmd = new SqlCommand("SELECT AA.NewTrans +' on dated '+convert(varchar(10),b.UPDATED_ON,103) as NewTrans,B.UPDATED_BY LIC_NO,aa.AccountNumber,e.APP_FIRMNAME,C.Crop_Name,D.Variety_Name,B.TOT_QTL,B.ADMISSIBLE_SUBSIDY,B.TOT_SUB_AMOUNT_GOI,B.TOT_SUB_AMOUNT_SP,AA.IFSC,AA.VCHMOBILENO,B.FARMER_ID,F.VCHFARMERNAME,AA.Amount FROM (    SELECT  Amount,VCHMOBILENO,IFSC,TransactionID, AccountNumber,case when SUBSTRING(TransactionID, 22, 1) = 'O' then left(TransactionID, 21) when SUBSTRING(TransactionID, 21, 1) = 'O' then left(TransactionID, 20)  when SUBSTRING(TransactionID, 20, 1) = 'O' then left(TransactionID, 19) when SUBSTRING(TransactionID, 23, 1) = 'O' then left(TransactionID, 22) end as NewTrans  FROM[FARMERDB].[DBO].[Request_Payment_tblTransaction] Y INNER JOIN [FARMERDB].[DBO].M_FARMER_REGISTRATION Z ON Y.FarmerID=Z.VCHFARMERCODE      WHERE CPAID = @CPAID)AA        INNER JOIN[STOCK].[DBO].[STOCK_FARMER] B ON AA.NewTrans COLLATE SQL_Latin1_General_CP1_CI_AS = B.TRANSACTION_ID        INNER JOIN[STOCK].[DBO].[mCrop]    C ON B.CROP_ID = C.CROP_CODE INNER JOIN[STOCK].[DBO].[mCropVariety]    D ON B.CROP_VERID = D.Variety_Code inner join dafpSeed.dbo.SEED_LIC_DIST e on b.UPDATED_BY= e.LIC_NO inner join FARMERDB.dbo.M_FARMER_REGISTRATION F on b.FARMER_ID=F.NICFARMERID collate Latin1_General_CI_AI  ORDER BY B.UPDATED_ON", con);
        //SqlCommand cmd = new SqlCommand("SELECT AA.NewTrans +' on dated '+convert(varchar(10),b.UPDATED_ON,103) as NewTrans,B.UPDATED_BY LIC_NO,aa.AccountNumber,e.APP_FIRMNAME,C.Crop_Name,D.Variety_Name,B.TOT_QTL,B.ADMISSIBLE_SUBSIDY,B.TOT_SUB_AMOUNT_GOI,B.TOT_SUB_AMOUNT_SP,AA.IFSC,AA.VCHMOBILENO,B.FARMER_ID,F.VCHFARMERNAME,cast(AA.Amount as decimal(10,2)) Amount FROM (    SELECT  Amount,VCHMOBILENO,IFSC,TransactionID, AccountNumber,case when SUBSTRING(TransactionID, 22, 1) = 'O' then left(TransactionID, 21) when SUBSTRING(TransactionID, 21, 1) = 'O' then left(TransactionID, 20)  when SUBSTRING(TransactionID, 20, 1) = 'O' then left(TransactionID, 19) when SUBSTRING(TransactionID, 23, 1) = 'O' then left(TransactionID, 22) end as NewTrans  FROM[FARMERDB].[DBO].[Request_Payment_tblTransaction] Y INNER JOIN [FARMERDB].[DBO].M_FARMER_REGISTRATION Z ON Y.FarmerID=Z.VCHFARMERCODE      WHERE CPAID = @CPAID)AA        INNER JOIN[STOCK].[DBO].[STOCK_FARMER] B ON AA.NewTrans COLLATE SQL_Latin1_General_CP1_CI_AS = B.TRANSACTION_ID        INNER JOIN[STOCK].[DBO].[mCrop]    C ON B.CROP_ID = C.CROP_CODE INNER JOIN[STOCK].[DBO].[mCropVariety]    D ON B.CROP_VERID = D.Variety_Code inner join dafpSeed.dbo.SEED_LIC_DIST e on b.UPDATED_BY= e.LIC_NO inner join FARMERDB.dbo.M_FARMER_REGISTRATION F on b.FARMER_ID=F.NICFARMERID collate Latin1_General_CI_AI  ORDER BY B.UPDATED_ON", con);

        //cmd.CommandType = CommandType.Text;
        SqlCommand cmd = new SqlCommand("SP_OUTER", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
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
    public int UpdateAppr(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        con.Open();
        //SqlCommand cmd = new SqlCommand("update [FARMERDB].[DBO].[Request_Payment_tblHeader] set Approve='1', DT_DEPT_APPROVE = GETDATE()  where CPAID = @CPAID", con);
        SqlCommand cmd = new SqlCommand("SP_UPDATEAPR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
        int i = cmd.ExecuteNonQuery();
        return i;
    }
    public int UpdateApprOSSC(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        con.Open();
        //SqlCommand cmd = new SqlCommand("update [FARMERDB].[DBO].[Request_Payment_tblHeader] set Approve='2', DT_OSSC_APPROVE = GETDATE() where CPAID = @CPAID", con);
        SqlCommand cmd = new SqlCommand("SP_UPDATEAPROSSC", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CPAID", objUserBEL.CPAID);
        int i = cmd.ExecuteNonQuery();
        return i;
    }
    public DataSet FillDEPTPACS(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        //String SqlSel = "";

        //SqlSel = "SELECT DISTINCT DIST_CODE,LIC_NO,APP_FIRMNAME,APPMOB_NO FROM SEED_LIC_DIST A INNER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID WHERE A.ACC_NO IS NULL AND B.COMP_NAME='OSSC' AND APP_STATUS = 'A' AND APR_UPTO > GETDATE() AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND DIST_CODE = @DIST_CODE ORDER BY APP_FIRMNAME ";
        SqlCommand cmd = new SqlCommand("sp_FILLDEPTPACS", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objUserBEL.DistCode);
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
    //public int UpdatePACSBankDetails(BLL_DropDown objBELDIST)
    //{
    //    int Val = 0;
    //    SqlConnection con = new SqlConnection(_connstrSeed);
    //    SqlCommand cmd = new SqlCommand("sp_UPDBANKDETAILS", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
    //        cmd.Parameters.AddWithValue("@FIRM_NAME", objBELDIST.NAME);
    //        cmd.Parameters.AddWithValue("@AADHAAR_NO", objBELDIST.AADHAAR_NO);
    //        cmd.Parameters.AddWithValue("@ACC_HOLDERNAME", objBELDIST.ACC_HOLDERNAME);
    //        cmd.Parameters.AddWithValue("@ACC_NO", objBELDIST.ACC_NO);
    //        cmd.Parameters.AddWithValue("@BANK_ID", objBELDIST.BANK_ID);
    //        cmd.Parameters.AddWithValue("@BRANCH_ID", objBELDIST.BRANCH_ID);
    //        cmd.Parameters.AddWithValue("@IFSC_CODE", objBELDIST.IFSC_CODE);
    //        cmd.Parameters.AddWithValue("@BANK_UPDATED_BY", objBELDIST.UPDATED_BY);
    //        cmd.Parameters.Add("@VAL", SqlDbType.Int);
    //        cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        Val = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
    //        con.Close();
    //        return Val;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return -1;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    public int UpdatePACSBankDetails(BLL_DropDown objBELDIST)
    {
        int Val = 0;
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("sp_UPDBANKDETAILS1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@ACC_HOLDERNAME", objBELDIST.ACC_HOLDERNAME);
            cmd.Parameters.AddWithValue("@ACC_NO", objBELDIST.ACC_NO);
            cmd.Parameters.AddWithValue("@BANK_ID", objBELDIST.BANK_ID);
            cmd.Parameters.AddWithValue("@BRANCH_ID", objBELDIST.BRANCH_ID);
            cmd.Parameters.AddWithValue("@IFSC_CODE", objBELDIST.IFSC_CODE);
            cmd.Parameters.AddWithValue("@BANK_UPDATED_BY", objBELDIST.UPDATED_BY);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
            return Val;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillCCBRequest(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.CPAID,cast(SUM(B.Amount) as decimal(10,2))AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A " +
        "INNER JOIN [FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID " +
        "WHERE Fin_Year = @FIN_YR AND Approve IN('1','2','4') AND scheme_code = @scheme_code " +
        "GROUP BY B.CPAID", con);
        cmd.CommandType = CommandType.Text;

        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@scheme_code", objUserBEL.scheme_code);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
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
    public DataSet FillOSSCRequest(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.CPAID,cast(SUM(B.Amount) as decimal(10,2))AMOUNT,COUNT(*)NOOFTRANS FROM [FARMERDB].[DBO].[Request_Payment_tblHeader] A " +
        "INNER JOIN [FARMERDB].[DBO].[Request_Payment_tblTransaction] B ON A.CPAID = B.CPAID " +
        "WHERE Fin_Year = '2018-19' AND Approve IN('2','4') " +
        "GROUP BY B.CPAID", con);
        cmd.CommandType = CommandType.Text;

        try
        {
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
    public DataSet STOCK_RPT_SaleVarietywise(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("STOCK_RPT_SaleVarietywise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;

        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@Dist_Code", objUserBEL.DistCode);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@Seassion", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@SCHEME_CODE", objUserBEL.scheme_code);
            cmd.Parameters.AddWithValue("@USER_TYPE", objUserBEL.UserType);
            cmd.Parameters.AddWithValue("@MONTH", objUserBEL.Month);
            cmd.Parameters.AddWithValue("@FRMDT", objUserBEL.FROM_DATE);
            cmd.Parameters.AddWithValue("@TODT", objUserBEL.TO_DATE);
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
    public DataSet STOCK_RPT_SaleVarietywise1(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("STOCK_RPT_SaleVarietywise1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;

        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@Dist_Code", objUserBEL.DistCode);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@Seassion", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@SCHEME_CODE", objUserBEL.scheme_code);
            cmd.Parameters.AddWithValue("@USER_TYPE", objUserBEL.UserType);
            cmd.Parameters.AddWithValue("@MONTH", objUserBEL.Month);
            cmd.Parameters.AddWithValue("@FRMDT", objUserBEL.FROM_DATE);
            cmd.Parameters.AddWithValue("@TODT", objUserBEL.TO_DATE);
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
    public DataSet DLRPaymentSTS(BLL_DropDown objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("PAYMENT_CHECK", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@LICENCE_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@FIN_YEAR", objBELDIST.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objBELDIST.SESSION);
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
    public DataSet FillDistrictAadhaar()
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "select vch_districtname,int_DistrictID,count(NICFARMERID) TotFarmer,sum(a) as NoAadhaar from ( " +
        "select c.vch_districtname, c.int_DistrictID, d.vch_blockname, d.int_BlockID, NICFARMERID, case when vchaadharno is null or vchaadharno = '' then 1 else 0 end as a " +
        "from [FARMERDB].[dbo].[M_FARMER_REGISTRATION] a " +
        "inner join [FARMERDB].[dbo].[Kalia_Farmer] b on a.nicfarmerid = b.farmer_id " +
        "inner join [FARMERDB].[dbo].[PDS_DISTRICTMASTER] c on a.vchdistid = c.int_districtid " +
        "inner join [FARMERDB].[dbo].[PDS_BLOCKMASTER] d on a.vchblockid = d.int_blockid " +
        "inner join [FARMERDB].[dbo].[PDS_GPMASTER] e on a.vchgpid = e.int_gpid " +
        "inner join [FARMERDB].[dbo].[PDS_VILLAGEMASTER] f on a.vchvillageid = f.int_villageid " +
        "inner join [FARMERDB].[dbo].[Tbl_Gender] g on a.intgender = g.[Gender_ID] " +
        ") tbl " +
        "GROUP BY int_DistrictID,vch_districtname " +
        "ORDER BY int_DistrictID,vch_districtname";

        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 0;
        try
        {
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
    public DataSet FillBlockAadhaar(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";

        SqlSel = "select vch_blockname,int_BlockID ,count(NICFARMERID) TotFarmer,sum(a) as NoAadhaar from ( " +
        "select c.vch_districtname, c.int_DistrictID, d.vch_blockname, d.int_BlockID, NICFARMERID, case when vchaadharno is null or vchaadharno = '' then 1 else 0 end as a " +
        "from [FARMERDB].[dbo].[M_FARMER_REGISTRATION] a " +
        "inner join [FARMERDB].[dbo].[Kalia_Farmer] b on a.nicfarmerid = b.farmer_id " +
        "inner join [FARMERDB].[dbo].[PDS_DISTRICTMASTER] c on a.vchdistid = c.int_districtid " +
        "inner join [FARMERDB].[dbo].[PDS_BLOCKMASTER] d on a.vchblockid = d.int_blockid " +
        "inner join [FARMERDB].[dbo].[PDS_GPMASTER] e on a.vchgpid = e.int_gpid " +
        "inner join [FARMERDB].[dbo].[PDS_VILLAGEMASTER] f on a.vchvillageid = f.int_villageid " +
        "inner join [FARMERDB].[dbo].[Tbl_Gender] g on a.intgender = g.[Gender_ID] " +
        ") tbl " +
        "WHERE int_DistrictID = @int_DistrictID " +
        "GROUP BY int_BlockID,vch_blockname " +
        "ORDER BY int_BlockID,vch_blockname";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@int_DistrictID", objUserBEL.DistCode);
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
    public DataSet FillDistWiseFarmerWTOAadhaar(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";

        SqlSel = "select d.vch_blockname,e.vch_gpname,f.vch_villagename,a.nicfarmerid,a.vchfarmername,a.vchfathername,a.INTAGE,g.Gender_Value,case when  LEN(A.VCHMOBILENO)>2 then LEFT(A.VCHMOBILENO,LEN(A.VCHMOBILENO)-2)+'**' else '<i style=color:red>-- n/a --</i>' end as VCHMOBILENO " +
        "from [FARMERDB].[dbo].[M_FARMER_REGISTRATION] a " +
        "inner join [FARMERDB].[dbo].[Kalia_Farmer] b on a.nicfarmerid=b.farmer_id " +
        "inner join [FARMERDB].[dbo].[PDS_DISTRICTMASTER] c on a.vchdistid=c.int_districtid " +
        "inner join [FARMERDB].[dbo].[PDS_BLOCKMASTER] d on a.vchblockid=d.int_blockid " +
        "inner join [FARMERDB].[dbo].[PDS_GPMASTER] e on a.vchgpid=e.int_gpid " +
        "inner join [FARMERDB].[dbo].[PDS_VILLAGEMASTER] f on a.vchvillageid=f.int_villageid " +
        "inner join [FARMERDB].[dbo].[Tbl_Gender] g on a.intgender=g.[Gender_ID] where (a.vchaadharno is null or a.vchaadharno='') and c.Lgd_DistrictCode  = @int_DistrictID " +
        "order by d.vch_blockname,e.vch_gpname ,f.vch_villagename,a.vchfarmername";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@int_DistrictID", objUserBEL.DistCode);
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
    public DataSet FillPDSDist()
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";

        SqlSel = "SELECT int_DistrictID,Lgd_DistrictCode,vch_DistrictName FROM [FARMERDB].[dbo].[PDS_DISTRICTMASTER] ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
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
    public DataSet FillUserType()
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";

        SqlSel = "SELECT USER_TYPE FROM STOCK_USERS GROUP BY USER_TYPE";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
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
    public DataSet FillUserUserByType(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "SELECT A.UserID,A.USER_TYPE,B.FullName " +
        "FROM Stock_Users A " +
        "INNER JOIN Stock_UserProfile B ON A.UserID = B.UserId " +
        "WHERE A.USER_TYPE = @USER_TYPE ORDER BY A.UserID ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objUserBEL.UserType);
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
    public DataSet FillUserUserByIDType(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "SELECT A.UserID,A.USER_TYPE,B.FullName " +
        "FROM Stock_Users A " +
        "INNER JOIN Stock_UserProfile B ON A.UserID = B.UserId " +
        "WHERE A.USER_TYPE = @USER_TYPE AND A.UserID = @UserID  ";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            cmd.Parameters.AddWithValue("@USER_TYPE", objUserBEL.UserType);
            cmd.Parameters.AddWithValue("@UserID", objUserBEL.UserID);
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
    public int ReSetPassword(BLL_DropDown objUserBEL)
    {
        int Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("sp_UPDPASS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@UserID", objUserBEL.UserID);
            cmd.Parameters.AddWithValue("@USER_TYPE", objUserBEL.UserType);
            cmd.Parameters.Add("@VAL", SqlDbType.Int);
            cmd.Parameters["@VAL"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@VAL"].Value);
            con.Close();
            return Val;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return -1;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }

    public DataSet ValidateUserName(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrAuth);

        SqlCommand cmd = new SqlCommand("NicAutht_UserLogin", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@USERNAME", objUserBEL.UserID);
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
    public void UpdPass(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrAuth);

        SqlCommand cmd = new SqlCommand("CHANGE_PASSWORD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@USERNAME", objUserBEL.UserID);
            cmd.Parameters.AddWithValue("@PASSWORD", objUserBEL.UserPass);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillSchemeYear(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("FILLSCHEME_YEAR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@Seassion", objUserBEL.SEASON);
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
    public DataSet FillPaymentYearD(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SEED_SUBSIDY_PAYMENT_GEN_NIC", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@Season", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@Scheme", objUserBEL.scheme_code);
            cmd.Parameters.AddWithValue("@Type", objUserBEL.DEPT_TYPE);
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
    public DataSet FillPaymentYearP(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("Seed_Subsidy_Payment_PACS_New", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@Season", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@Scheme", objUserBEL.scheme_code);
            cmd.Parameters.AddWithValue("@Type", objUserBEL.DEPT_TYPE);
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
    public DataSet FillPaymentYearA(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SEED_SUBSIDY_PAYMENT_GEN_NIC_ALL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@Season", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@Scheme", objUserBEL.scheme_code);
            cmd.Parameters.AddWithValue("@Type", objUserBEL.DEPT_TYPE);
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
    public DataSet FillSchemeByYear(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("FILLSCHEME_BY_YEAR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
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
    public DataSet GetDemonNames()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT DEMON_ID,DEMON_NAME FROM mDEMONHDR WHERE IS_ACTIVE = 1 ORDER BY SL_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet GetDemonSchemes()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT SCHEME_ID,SCHEME_NAME FROM mDEMON_SCHEME WHERE IS_ACTIVE = 1 ORDER BY SCHEME_ID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public int InsertClass(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("CLASS_CHANGE_INS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);//@QTY,@SOURCE,@OSSOPCAREASON
            cmd.Parameters.AddWithValue("@QTY", objBELUserDetails.AMOUNT);
            cmd.Parameters.AddWithValue("@SOURCE", objBELUserDetails.SaleTo);
            cmd.Parameters.AddWithValue("@OSSOPCAREASON", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@TYPE_ID", objBELUserDetails.TypeId);

            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            int strMessage = (int)cmd.Parameters["@Val"].Value;
            con.Close();
            return strMessage;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int InsertClass1(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("CLASS_CHANGE_INS1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@SLNO", objBELUserDetails.slno);
            cmd.Parameters.AddWithValue("@Crop_Code", objBELUserDetails.CROP_ID);
            cmd.Parameters.AddWithValue("@Variety_Code", objBELUserDetails.Crop_Verid);
            cmd.Parameters.AddWithValue("@LOT_NO", objBELUserDetails.LOT_NO);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELUserDetails.UPDATED_BY);//@QTY,@SOURCE,@OSSOPCAREASON
            cmd.Parameters.AddWithValue("@QTY", objBELUserDetails.AMOUNT);
            cmd.Parameters.AddWithValue("@SOURCE", objBELUserDetails.SaleTo);
            cmd.Parameters.AddWithValue("@APROVE_TYPE", objBELUserDetails.ApprovaTypeID);
            cmd.Parameters.AddWithValue("@OSSOPCAREASON", objBELUserDetails.REF_NO);
            cmd.Parameters.AddWithValue("@TYPE_ID", objBELUserDetails.TypeId);

            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            int strMessage = (int)cmd.Parameters["@Val"].Value;
            con.Close();
            return strMessage;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet ClassSel(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("CLASS_CHANGE_SEL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YEAR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION", objUserBEL.SEASON);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@TYPE", objUserBEL.TypeId);
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
    public DataSet ClassSel1(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("CLASS_CHANGE_SEL1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@Crop_Code", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@Variety_Code", objUserBEL.Crop_Verid);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@TYPE", objUserBEL.TypeId);
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
    public DataSet ClassSelByLot(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("CLASS_CHANGE_SEL_LOT", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LOT_NUMBER", objUserBEL.LOT_NO);
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
    public DataSet FillAction(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("ACTION_SEL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@TYPE", objUserBEL.UserType);
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
    public DataSet FillRejectedDealers(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("SHOW_DEALER", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objUserBEL.DistCode);
            cmd.Parameters.AddWithValue("@LIC_NO", objUserBEL.LIC_NO);
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
    public DataSet FillBankBranch(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("FILLBANK_BRANCH", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@APP_TYPE", objUserBEL.DEPT_TYPE);
            cmd.Parameters.AddWithValue("@BANK_ID", objUserBEL.BANK_ID);
            cmd.Parameters.AddWithValue("@BRANCH_ID", objUserBEL.BRANCH_ID);
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
    //public int InsReceiveDetails_api01(BLL_DropDown objBELUserDetails)
    //{
    //    SqlConnection con = new SqlConnection(_connstrStock);
    //    SqlCommand cmd = new SqlCommand("Stock_Sp_InsReceiveDetails_api01", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    try
    //    {
    //        cmd.Parameters.AddWithValue("@Dist_Code", objBELUserDetails.DistCode);
    //        cmd.Parameters.AddWithValue("@Godown_ID", objBELUserDetails.Godown_ID);
    //        cmd.Parameters.AddWithValue("@Challan_No", objBELUserDetails.Challan_No);
    //        cmd.Parameters.AddWithValue("@Recv_Date", objBELUserDetails.Receive_Date);
    //        cmd.Parameters.AddWithValue("@FARMER_ID", objBELUserDetails.FARMERID);
    //        cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SEASON);
    //        cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
    //        cmd.Parameters.AddWithValue("@UserID", objBELUserDetails.UserID);
    //        cmd.Parameters.AddWithValue("@UserIP", objBELUserDetails.UserIP);
    //        cmd.Parameters.AddWithValue("@CropCatg_ID", objBELUserDetails.CROPCATG_ID);
    //        cmd.Parameters.AddWithValue("@Crop_ID", objBELUserDetails.CROP_ID);
    //        cmd.Parameters.AddWithValue("@Crop_Verid", objBELUserDetails.Crop_Verid);
    //        cmd.Parameters.AddWithValue("@Class", objBELUserDetails.CropClass);
    //        cmd.Parameters.AddWithValue("@Lot_No", objBELUserDetails.LOT_NO);
    //        cmd.Parameters.AddWithValue("@Bag_Size_In_kg", objBELUserDetails.Bag_Size_In_kg);
    //        cmd.Parameters.AddWithValue("@Recv_No_Of_Bags", objBELUserDetails.Recv_No_Of_Bags);
    //        cmd.Parameters.AddWithValue("@Recv_Quantity", objBELUserDetails.Receive_Qty);
    //        cmd.Parameters.AddWithValue("@Testing_Date", objBELUserDetails.TestingDate);
    //        cmd.Parameters.AddWithValue("@APIKEY", objBELUserDetails.APIKEY);

    //        cmd.Parameters.Add("@Val", SqlDbType.Int);
    //        cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        int strMessage = (int)cmd.Parameters["@Val"].Value;
    //        con.Close();
    //        return strMessage;
    //    }
    //    catch (Exception exception)
    //    {
    //        ExceptionHandler.WriteEx(exception);
    //        return 0;
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        if (con.State != ConnectionState.Closed)
    //            con.Close();
    //        con.Dispose();
    //    }
    //}
    public DataSet FillSubsidyInvolved(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVED";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet FillSubsidyCrop()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT B.Crop_Code,B.Crop_Name FROM TBLSUBSIDYPER A " +
                                        "INNER JOIN mCrop B ON A.CROP_ID = B.Crop_Code " +
                                        "WHERE A.STATUS1 = 0 " +
                                        "ORDER BY B.Crop_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillSubsidyCropAge(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVEDAGE25";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@CROP_ID", objUserBEL.CROP_ID);
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
    public DataSet FillSubsidyCropSource(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVEDSOURCE25";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@CROP_ID", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@VARIETY_AGE", objUserBEL.AGE);
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
    public DataSet FillSubsidyDtl(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVED25";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@CROP_CODE", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@AGE", objUserBEL.AGE);
            cmd.Parameters.AddWithValue("@SOURCE", objUserBEL.SOURCE);
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
    public int UpdSubsidyInvolved25_1(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_UPDSUBSIDYINVOLVED25_1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@CROP_ID", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@VARIETY_AGE", objUserBEL.AGE);
            cmd.Parameters.AddWithValue("@SOURCE", objUserBEL.SOURCE);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objUserBEL.UPDATED_BY);
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            int strMessage = (int)cmd.Parameters["@Val"].Value;
            con.Close();
            return strMessage;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillSubsidyDtlCategory()
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVED25CATG";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
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
    public DataSet FillSubsidyCrop_()
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("select B.Crop_Name,B.Crop_Code from TblSubsidyPer_ A " +
                                        "INNER JOIN mCrop B ON A.CROP_ID = B.Crop_Code " +
                                        "GROUP BY B.Crop_Name,B.Crop_Code ORDER BY B.Crop_Name,B.Crop_Code ", con);
        cmd.CommandType = CommandType.Text;
        try
        {
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
    public DataSet FillSubsidyCropAge_(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVEDAGE25_";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@CROP_ID", objUserBEL.CROP_ID);
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
    public DataSet FillSubsidyCropSource_(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVEDSOURCE25_";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@CROP_ID", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@VARIETY_AGE", objUserBEL.AGE);
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
    public DataSet FillSubsidyDtl_(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "STOCK_FILLSUBSIDYINVOLVED25_";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@CROP_CODE", objUserBEL.CROP_ID);
            cmd.Parameters.AddWithValue("@AGE", objUserBEL.AGE);
            cmd.Parameters.AddWithValue("@SOURCE", objUserBEL.SOURCE);
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
    public int FillPendingIncentiveOilSeed(BLL_DropDown objUserBEL)
    {
        int Cnt = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM mINCENTIVE2 WHERE FIN_YR = @FIN_YR AND OSSC_STATUS = 'Pending'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void OsscAprvOilSeed(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("UPDATE mINCENTIVE2 SET OSSC_STATUS = 'Approved', OSSC_STATUS_UPDATEDBY = @OSSC_STATUS_UPDATEDBY, OSSC_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = @FIN_YR AND OSSC_STATUS = 'Pending'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@OSSC_STATUS_UPDATEDBY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void OsscAprvOilSeedOUAT(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("UPDATE mINCENTIVE2_OUAT SET OSSC_STATUS = 'Approved', OSSC_STATUS_UPDATEDBY = @OSSC_STATUS_UPDATEDBY, OSSC_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = @FIN_YR AND OSSC_STATUS = 'Pending'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@OSSC_STATUS_UPDATEDBY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int FillPendingIncentiveOilSeedDept(BLL_DropDown objUserBEL)
    {
        int Cnt = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM mINCENTIVE2 WHERE FIN_YR = @FIN_YR AND PAY_STATUS = 'UnPaid'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void OsscAprvOilSeedDept(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("UPDATE mINCENTIVE2 SET TRANS_NO = @TRANS_NO, PAY_STATUS = 'Paid', PAY_STATUS_UPDTAEDBY = @PAY_STATUS_UPDATEDBY, PAY_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = @FIN_YR AND PAY_STATUS = 'UnPaid'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@TRANS_NO", objUserBEL.TRANSACTIONID);
            cmd.Parameters.AddWithValue("@PAY_STATUS_UPDATEDBY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void OsscAprvOilSeedDeptOUAT(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("UPDATE mINCENTIVE2_OUAT SET TRANS_NO = @TRANS_NO, PAY_STATUS = 'Paid', PAY_STATUS_UPDTAEDBY = @PAY_STATUS_UPDATEDBY, PAY_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = @FIN_YR AND PAY_STATUS = 'UnPaid'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@TRANS_NO", objUserBEL.TRANSACTIONID);
            cmd.Parameters.AddWithValue("@PAY_STATUS_UPDATEDBY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int FillPendingIncentiveOilSeedAll(BLL_DropDown objUserBEL)
    {
        int Cnt = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM mINCENTIVE3 WHERE FIN_YR = @FIN_YR AND OSSC_STATUS = 'Pending'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void OsscAprvOilSeedAll(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("UPDATE mINCENTIVE3 SET OSSC_STATUS = 'Approved', OSSC_STATUS_UPDATEDBY = @OSSC_STATUS_UPDATEDBY, OSSC_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = @FIN_YR AND OSSC_STATUS = 'Pending'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@OSSC_STATUS_UPDATEDBY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int FillPendingIncentiveOilSeedDeptAll(BLL_DropDown objUserBEL)
    {
        int Cnt = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM mINCENTIVE3 WHERE FIN_YR = @FIN_YR AND PAY_STATUS = 'UnPaid'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            Cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return 0;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public void OsscAprvOilSeedDeptAll(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("UPDATE mINCENTIVE3 SET TRANS_NO = @TRANS_NO, PAY_STATUS = 'Paid', PAY_STATUS_UPDTAEDBY = @PAY_STATUS_UPDATEDBY, PAY_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = @FIN_YR AND PAY_STATUS = 'UnPaid'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@TRANS_NO", objUserBEL.TRANSACTIONID);
            cmd.Parameters.AddWithValue("@PAY_STATUS_UPDATEDBY", objUserBEL.UPDATED_BY);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillDealerPACSReport(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("Dealer_Pacs_Payment_Report", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@F_year", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@App_type", objUserBEL.SUPPLY_TYPE);
	    cmd.Parameters.AddWithValue("@Scheme", objUserBEL.scheme_code);
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
    public DataSet CropVarFin(BLL_DropDown objUserBEL)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        String SqlSel = "";
        SqlSel = "SP_CROPWISEFINYEARSALE";
        SqlCommand cmd = new SqlCommand(SqlSel, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            cmd.Parameters.AddWithValue("@LIC_TYPE", objUserBEL.UserType);
            cmd.Parameters.AddWithValue("@FIN_YR", objUserBEL.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objUserBEL.SEASON);
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
    public DataSet CheckTransactionDtls(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_CHECKTRANSACTION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@TRANSACTION_ID", objBELUserDetails.TRANSACTIONID);
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
    public DataSet FILLFINWISECROPVARIETY(BLL_DropDown objBELUserDetails)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_FILLFINWISECROPVARIETY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objBELUserDetails.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASSION_NAME", objBELUserDetails.SEASON);
            cmd.Parameters.AddWithValue("@Category_Code", objBELUserDetails.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@CROP_CODE", objBELUserDetails.CROP_ID);
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

    

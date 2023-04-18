using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DLL_Dealer
/// </summary>
public class DLL_Dealer
{
    string _connstrSeed = ConfigurationManager.ConnectionStrings["SqlConSeed"].ConnectionString;
    string _connstrStock = ConfigurationManager.ConnectionStrings["SqlConStock"].ConnectionString;    
    DataSet ds = new DataSet();
    public DLL_Dealer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int CheckDate()
    {
        int Cnt = 0;
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("sp_CHKDATE", con);
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
    public int CheckLogIn(BLL_Dealer objBELDIST)
    {
        int Is_Dealer = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT COUNT(DISTINCT A.SEED_LIC_DIST_ID)Cnt FROM [dafpseed].[dbo].[SEED_LIC_DIST] A " +
                    "INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
                    "INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID " +
                    "WHERE B.APPEMAIL_ID = @APPEMAIL_ID AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@APPEMAIL_ID", objBELDIST.APPEMAIL_ID);
            if (con.State != ConnectionState.Open)
                con.Open();

            Is_Dealer = Convert.ToInt32(cmd.ExecuteScalar());

            if (con.State != ConnectionState.Closed)
                con.Close();
            return Is_Dealer;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return Is_Dealer;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public int CheckLogInOSSC(BLL_Dealer objBELDIST)
    {
        int Is_Dealer = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT COUNT(DISTINCT A.SEED_LIC_DIST_ID)Cnt FROM [dafpseed].[dbo].[SEED_LIC_DIST] A " +
                    "INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID " +
                    "INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID " +
                    "WHERE B.APPEMAIL_ID = @APPEMAIL_ID AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC' AND A.IS_OSSC = 1", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@APPEMAIL_ID", objBELDIST.APPEMAIL_ID);
            if (con.State != ConnectionState.Open)
                con.Open();

            Is_Dealer = Convert.ToInt32(cmd.ExecuteScalar());

            if (con.State != ConnectionState.Closed)
                con.Close();
            return Is_Dealer;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return Is_Dealer;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet CheckLic(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT SEED_LIC_DIST_ID,REF_NO,LIC_NO,LIC_NO1,APP_FIRMNAME,PASSWORD,PASSWORD_SALT,SALTED_PASSWORD,ISLOCKEDOUT,APP_TYPE FROM [dafpseed].[dbo].[SEED_LIC_DIST] WHERE LIC_ACTIVE = 1 AND IS_ACTIVE = 1 AND APP_STATUS = 'A' AND CONVERT(DATE,DATEADD(MONTH,1,APR_UPTO),103) >= CONVERT(DATE,GETDATE(),103) AND LIC_NO1 = @LIC_NO1", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO1", objBELDIST.LICENCENO);
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
    public DataSet CheckLic0(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT SEED_LIC_DIST_ID,REF_NO,LIC_NO,LIC_NO1,APP_FIRMNAME,PASSWORD,PASSWORD_SALT,SALTED_PASSWORD,ISLOCKEDOUT,APP_TYPE FROM [dafpseed].[dbo].[SEED_LIC_DIST] WHERE LIC_ACTIVE = 1 AND IS_ACTIVE = 1 AND APP_STATUS = 'A' AND CONVERT(DATE,DATEADD(MONTH,1,APR_UPTO),103) >= CONVERT(DATE,GETDATE(),103) AND LIC_NO = @LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
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
    public string GetBlockCode(BLL_Dealer objBELDIST)
    {
        string BLOCK_CODE = "";
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("SELECT BLOCK_ID FROM [dafpseed].[dbo].[SEED_LIC_BUS_DIST] WHERE SEED_LIC_DIST_ID = @SEED_LIC_DIST_ID AND IS_ACTIVE = 1", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@SEED_LIC_DIST_ID", objBELDIST.SEED_LIC_DIST_ID);
            if (con.State != ConnectionState.Open)
                con.Open();

            BLOCK_CODE = cmd.ExecuteScalar().ToString();

            if (con.State != ConnectionState.Closed)
                con.Close();
            return BLOCK_CODE;
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
    public int CntLogIn(BLL_Dealer objBELDIST)
    {
        int Cnt = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT COUNT(*)Cnt FROM [dafpseed].[dbo].[SEED_LIC_DIST] WHERE SEED_LIC_DIST_ID IN (SELECT DISTINCT SEED_LIC_DIST_ID FROM [dafpseed].[dbo].[SEED_LIC_APP_DIST] WHERE APPEMAIL_ID = @APPEMAIL_ID) AND APP_STATUS = 'A' AND CONVERT(DATE,DATEADD(MONTH,1,APR_UPTO),103) >= CONVERT(DATE,GETDATE(),103) AND LIC_ACTIVE = 1 AND IS_ACTIVE = 1", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@APPEMAIL_ID", objBELDIST.APPEMAIL_ID);
            if (con.State != ConnectionState.Open)
                con.Open();

            Cnt = Convert.ToInt32(cmd.ExecuteScalar());

            if (con.State != ConnectionState.Closed)
                con.Close();
            return Cnt;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return Cnt;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }
    public DataSet FillSeassion(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_FILLSEASSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.F_Year);
            cmd.Parameters.AddWithValue("@STATUS", objBELDIST.STATUS);
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
    public DataSet FillFinYr(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_FILLFINYR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@STATUS", objBELDIST.STATUS);
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
    public DataSet FillCropVarietyByCropIdDealerStock(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT D.VARIETY_CODE,D.VARIETY_NAME FROM STOCK_DEALERSTOCK A " +
        "LEFT OUTER JOIN mCROPCATEGORY B ON A.CROPCATG_ID = B.CATEGORY_CODE " +
        "LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.CROP_CODE " +
        "LEFT OUTER JOIN mCROPVARIETY D ON A.CROP_VERID = D.VARIETY_CODE " +
        "WHERE A.LICENCE_NO = @LICENCE_NO AND A.CROP_ID = @CROP_CODE AND A.FIN_YR = @FIN_YR AND A.SEASSION = @SEASSION AND A.AVL_NO_OF_BAGS > 0 AND D.IS_ACTIVE = 1 AND A.VALIDITY = 1 " +
        "GROUP BY D.VARIETY_CODE,D.VARIETY_NAME " +
        "ORDER BY D.VARIETY_CODE,D.VARIETY_NAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LICENCE_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@CROP_CODE", objBELDIST.CROP_ID);
            cmd.Parameters.AddWithValue("@SEASSION", objBELDIST.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.F_Year);
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
    public DataSet FillCropByCategoryIdDealerStock(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT C.CROP_CODE,C.CROP_NAME FROM STOCK_DEALERSTOCK A " +
        "LEFT OUTER JOIN mCROPCATEGORY B ON A.CROPCATG_ID = B.CATEGORY_CODE " +
        "LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.CROP_CODE " +
        "WHERE A.LICENCE_NO = @LICENCE_NO AND A.FIN_YR = @FIN_YR AND A.SEASSION = @SEASSION AND C.IS_ACTIVE = 1 AND A.VALIDITY = 1  " +
        "GROUP BY C.CROP_CODE,C.CROP_NAME " +
        "ORDER BY C.CROP_CODE,C.CROP_NAME", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LICENCE_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@SEASSION", objBELDIST.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.F_Year);
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
    public DataSet FillSaleDealerGrid(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("sp_FILLSALEDEALERSALE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LICENCENO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@CROP_ID", objBELDIST.CROP_ID);
            cmd.Parameters.AddWithValue("@CROP_VERID", objBELDIST.CROP_VERID);
            cmd.Parameters.AddWithValue("@SEASONS", objBELDIST.SESSION);
            cmd.Parameters.AddWithValue("@F_Year", objBELDIST.F_Year);
            cmd.Parameters.AddWithValue("@USER_TYPE", objBELDIST.USER_TYPE);
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
    public string GetDistCodeByLicNo(BLL_Dealer objBELDIST)
    {
        string DIST_CODE = "";
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("SELECT DIST_CODE FROM SEED_LIC_DIST WHERE LIC_NO = @LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            if (con.State != ConnectionState.Open)
                con.Open();

            DIST_CODE = cmd.ExecuteScalar().ToString();

            if (con.State != ConnectionState.Closed)
                con.Close();
            return DIST_CODE;
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
    public string GetDAOCodeByLicNo(BLL_Dealer objBELDIST)
    {
        string DAO_CODE = "";
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("SELECT RIGHT(DAO_CD,2) FROM SEED_LIC_DIST WHERE LIC_NO = @LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            if (con.State != ConnectionState.Open)
                con.Open();

            DAO_CODE = cmd.ExecuteScalar().ToString();

            if (con.State != ConnectionState.Closed)
                con.Close();
            return DAO_CODE;
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
    public void InsertSaleDealer(BLL_Dealer objBELDIST, out int Val, out string REFNO)
    {
        Val = 0;
        REFNO = "";
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("Stock_SP_InsDealerSale", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objBELDIST.FARMER_ID);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELDIST.DIST_CODE);            
            cmd.Parameters.AddWithValue("@DAO_CD", objBELDIST.DAO_CD);
            cmd.Parameters.AddWithValue("@LICENCE_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@SEASON", objBELDIST.SESSION);
            cmd.Parameters.AddWithValue("@FINYR", objBELDIST.F_Year);
            cmd.Parameters.AddWithValue("@VALUES", objBELDIST.XMLVALUE);            
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELDIST.UPDATED_BY);
            cmd.Parameters.AddWithValue("@USERIP", objBELDIST.USERIP);
            cmd.Parameters.Add("@TRANSACTION_ID", SqlDbType.VarChar, 50);
            cmd.Parameters["@TRANSACTION_ID"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Val", SqlDbType.Int);
            cmd.Parameters["@Val"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            Val = Convert.ToInt32(cmd.Parameters["@Val"].Value);
            REFNO = cmd.Parameters["@TRANSACTION_ID"].Value.ToString();
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
    public DataSet GetFarmerDtl(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_FARMERINVDTL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objBELDIST.FARMER_ID);
            cmd.Parameters.AddWithValue("@TRANSACTION_ID", objBELDIST.TRANSACTION_ID);
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
    public DataSet GetFarmerInv(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_FARMERINVPRICE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {            
            cmd.Parameters.AddWithValue("@TRANSACTION_ID", objBELDIST.TRANSACTION_ID);
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
    public DataSet GetFarmerInvHdr(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_FARMERINVHDR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objBELDIST.FARMER_ID);
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
    public string GetPRICE_RECEIVE_UNITCD(BLL_Dealer objBELDIST)
    {
        string PRICE_RECEIVE_UNITCD = "";
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT PRICE_RECEIVE_UNITCD FROM PRICE_SOURCEMAPPING WHERE RECEIVE_UNITCD = @RECEIVE_UNITCD AND SEASSION = @SEASSION AND FIN_YR = @FIN_YR", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@RECEIVE_UNITCD", objBELDIST.RECEIVE_UNITCD);
            cmd.Parameters.AddWithValue("@SEASSION", objBELDIST.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.F_Year);
            if (con.State != ConnectionState.Open)
                con.Open();

            PRICE_RECEIVE_UNITCD = cmd.ExecuteScalar().ToString();

            if (con.State != ConnectionState.Closed)
                con.Close();
            return PRICE_RECEIVE_UNITCD;
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
    public string GetReceive_UnitnameByCode(BLL_Dealer objBELDIST)
    {
        string Receive_Unitname = "";
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT Receive_Unitname FROM Stock_Receive_Unit_Master WHERE Receive_Unitcd = @PRICE_RECEIVE_UNITCD", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@PRICE_RECEIVE_UNITCD", objBELDIST.RECEIVE_UNITCD);
            if (con.State != ConnectionState.Open)
                con.Open();

            Receive_Unitname = cmd.ExecuteScalar().ToString();

            if (con.State != ConnectionState.Closed)
                con.Close();
            return Receive_Unitname;
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
    public DataSet RptDateWiseSale(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_RPT_DatewiseSale", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@FromDt", objBELDIST.FDate);
            cmd.Parameters.AddWithValue("@ToDt", objBELDIST.TDate);
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
    public DataSet RptCurStock(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT D.Category_Code,D.Category_Name FROM Stock_ReceiveDealer A " +
        "LEFT OUTER JOIN mCropVariety B ON A.CROP_VARIETY_CODE = B.Variety_Code " +
        "LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
        "LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
        "INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
        "INNER JOIN mSEASSION F ON A.SEASSION_NAME = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
        "WHERE LIC_NO = @LIC_NO GROUP BY D.Category_Code,D.Category_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
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
    public DataSet RptCurStock1(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT C.Crop_Code,C.Crop_Name FROM Stock_ReceiveDealer A " +
            "LEFT OUTER JOIN mCropVariety B ON A.CROP_VARIETY_CODE = B.Variety_Code " +
            "LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
            "LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
            "INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
            "INNER JOIN mSEASSION F ON A.SEASSION_NAME = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
            "WHERE LIC_NO = @LIC_NO AND D.Category_Code = @CropCatg_ID GROUP BY C.Crop_Code,C.Crop_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@CropCatg_ID", objBELDIST.CROPCATG_ID);
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
    public DataSet RptCurStock2(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT A.CROP_CLASS FROM Stock_ReceiveDealer A " +
            "LEFT OUTER JOIN mCropVariety B ON A.CROP_VARIETY_CODE = B.Variety_Code " +
            "LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
            "LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
            "INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
            "INNER JOIN mSEASSION F ON A.SEASSION_NAME = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
            "WHERE LIC_NO = @LIC_NO AND D.Category_Code = @CropCatg_ID AND C.Crop_Code = @Crop_ID GROUP BY A.CROP_CLASS", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@CropCatg_ID", objBELDIST.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@Crop_ID", objBELDIST.CROP_ID);
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
    public DataSet RptCurStock3(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.Variety_Code,B.Variety_Name,SUM(STOCK_QUANTITY)STOCK_QUANTITY,SUM(AVL_QUANTITY)AVL_QUANTITY FROM STOCK_DEALERSTOCK A " +
            "LEFT OUTER JOIN mCropVariety B ON A.CROP_VERID = B.Variety_Code " +
            "LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code " +
            "LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code " +
            "INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 " +
            "INNER JOIN mSEASSION F ON A.SEASSION = F.SHORT_NAME AND F.IS_ACTIVE = 1 " +
            "WHERE A.LICENCE_NO = @LIC_NO AND D.Category_Code = @CropCatg_ID AND C.Crop_Code = @Crop_ID AND A.CLASS = @CROP_CLASS " +
            "GROUP BY B.Variety_Code,B.Variety_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@CropCatg_ID", objBELDIST.CROPCATG_ID);
            cmd.Parameters.AddWithValue("@Crop_ID", objBELDIST.CROP_ID);
            cmd.Parameters.AddWithValue("@CROP_CLASS", objBELDIST.CROP_CLASS);
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
    public DataSet RptPayReq(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("DatewiseSale", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@FrmDt", objBELDIST.FDate);
            cmd.Parameters.AddWithValue("@Todate", objBELDIST.TDate);
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
    public DataSet RptPayDtl(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("STOCK_DealerPayment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@UPDATED_BY", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@FROMDATE", objBELDIST.FDate);
            cmd.Parameters.AddWithValue("@TODATE", objBELDIST.TDate);
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
    public DataSet CheckLicMob(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT COUNT(*)CNT FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE REF_NO = @REF_NO AND ACCESS_MODE = 'W'", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@REF_NO", objBELDIST.REF_NO);
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
    public DataSet GetDistCodeByLic(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT DIST_CODE FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO = @LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
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
    public DataSet GetShortNameByDistCode(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT SHORT_NAME FROM [DAFPSEED].[DBO].[dist] WHERE DIST_CODE = @DIST_CODE", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELDIST.DIST_CODE);
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
    public DataSet GetUserType(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT COMP_NAME FROM [DAFPSEED].[DBO].[SEED_LIC_COMP_DIST] WHERE SEED_LIC_DIST_ID = (SELECT SEED_LIC_DIST_ID FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO = @LIC_NO) AND COMP_TYPE = 1", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
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
    public DataSet GetFirmName(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT APP_FIRMNAME FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO = @LIC_NO", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
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
    public DataSet GetFarmerStatus(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.STATUS FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] A " +
            "LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_FarmerApprove] B ON A.VCHFARMERCODE = B.Farmer_Code " +
            "WHERE A.NICFARMERID = @FARMER_ID", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objBELDIST.FARMER_ID);
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
    public DataSet GetFarmerDetails(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("GETFarmerInfo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //SqlCommand cmd = new SqlCommand("SELECT A.NICFARMERID,A.VCHFARMERNAME,A.VCHFATHERNAME,D.Category_Value,E.Gender_Value,VCHPACSCODE " +
        //        "FROM [FARMERDB].[DBO].[M_FARMER_REGISTRATION] A " +
        //        "LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_FarmerApprove] B ON A.VCHFARMERCODE = B.Farmer_Code " +
        //        "LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_Category] D ON A.INTCATEGOGY = D.Category_ID " +
        //        "LEFT OUTER JOIN [FARMERDB].[DBO].[Tbl_Gender] E ON A.INTGENDER = E.Gender_ID " +
        //        "WHERE A.NICFARMERID = @FARMER_ID AND B.Status IN ('ACCP','CCB')", con);
        //cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objBELDIST.FARMER_ID);
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
    public DataSet GetCropReceived(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SELECT B.Crop_Name,CONVERT(VARCHAR(50),SUM(TOT_QTL))+' Qtl'TOT_QTL FROM [STOCK].[DBO].[STOCK_FARMER_2021-22_R] A LEFT OUTER JOIN [STOCK].[DBO].[mCrop] B ON A.CROP_ID = B.Crop_Code WHERE FARMER_ID = @FARMER_ID AND FIN_YEAR = @FIN_YR AND SEASON = @SEASON GROUP BY B.Crop_Name", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objBELDIST.FARMER_ID);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objBELDIST.SEASON);
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
    public DataSet RptCropDistSale(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("Sp_RptCropDistSale", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {            
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objBELDIST.SEASON);
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
    public DataSet RptCropDistSale1(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("Sp_RptCropDistSale1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.FIN_YR);
            cmd.Parameters.AddWithValue("@SEASON", objBELDIST.SEASON);
            cmd.Parameters.AddWithValue("@CROP_ID", objBELDIST.CROP_ID);
            cmd.Parameters.AddWithValue("@DIST_CODE", objBELDIST.DIST_CODE);
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
    public DataSet GetDealerInfo(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("sp_GETDEALERINFO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
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
    public DataSet FillBank()
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("sp_FILLBANK", con);
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
    public DataSet FillBranchByBankCode(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("sp_FILLBRANCH", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@BnkCode", objBELDIST.BANK_CODE);
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
    public DataSet FillIFSC(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrSeed);

        SqlCommand cmd = new SqlCommand("sp_FILLIFSC", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@BnchId", objBELDIST.BRANCH_CODE);
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
    public int UpdateDealerBankDetails(BLL_Dealer objBELDIST)
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
            cmd.Parameters.AddWithValue("@BANK_ID", objBELDIST.BANK_CODE);
            cmd.Parameters.AddWithValue("@BRANCH_ID", objBELDIST.BRANCH_CODE);
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
    public int CntLic(BLL_Dealer objBELDIST)
    {
        int Val = 0;
        SqlConnection con = new SqlConnection(_connstrSeed);
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*)Cnt FROM SEED_LIC_DIST WHERE LIC_NO = @LIC_NO AND ACC_NO IS NULL", con);
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            
            con.Open();
            Val = Convert.ToInt32(cmd.ExecuteScalar());            
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
    public DataSet RECVDTL_CASHMEMONO(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_RECVDTL_CASHMEMONO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@CASH_MEMO_NO", objBELDIST.CASH_MEO_NO);
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
    public DataSet RECPTDEALER(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_RECPTDEALER", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@FromDt", objBELDIST.FDate);
            cmd.Parameters.AddWithValue("@ToDt", objBELDIST.TDate);
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

    //COUPON BEGIN
    public DataSet GetMobNoOTP(BLL_Dealer objUserBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("Check_MobOTP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objUserBELDIST.FARMER_ID);
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

    public int CountFarmerMob(BLL_Dealer objUserBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("CNT_MOBILEFARMERID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@MOBILE_NO", objUserBELDIST.Mob);
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

    public int CheckCouponValidity(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);
        int Cnt = 0;
        SqlCommand cmd = new SqlCommand("CHECK_COUPONVALIDITY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@COUPON_CODE", objBELDIST.COUPON_CODE);
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

    public DataSet CheckOTP(BLL_Dealer objUserBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("Check_OTP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objUserBELDIST.FARMER_ID);
            cmd.Parameters.AddWithValue("@MOB_NO", objUserBELDIST.Mob);
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
    public void CreateOTP(BLL_Dealer objUserBELDIST, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("CreateOTP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objUserBELDIST.FARMER_ID);
            cmd.Parameters.AddWithValue("@OTP", objUserBELDIST.OTP);
            cmd.Parameters.AddWithValue("@MOB_NO", objUserBELDIST.Mob);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objUserBELDIST.UPDATED_BY);
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


    public void ValidateOTP(BLL_Dealer objUserBELDIST, out int Val)
    {
        Val = 0;
        SqlConnection con = new SqlConnection(_connstrStock);
        SqlCommand cmd = new SqlCommand("CheckOTP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 0;
        try
        {
            cmd.Parameters.AddWithValue("@FARMER_ID", objUserBELDIST.FARMER_ID);
            cmd.Parameters.AddWithValue("@OTP", objUserBELDIST.OTP);
            cmd.Parameters.AddWithValue("@UPDATED_BY", objUserBELDIST.UPDATED_BY);
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

    public DataSet GetCouponDetails(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("GET_COUPONDTLS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@COUPON_CODE", objBELDIST.COUPON_CODE);
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

    //COUPON END
    public int ChkValidLic(BLL_Dealer objBELDIST)
    {
        int Is_Dealer = 0;
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_CHKVALIDLIC", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LICENCENO", objBELDIST.LICENCENO);
            if (con.State != ConnectionState.Open)
                con.Open();

            Is_Dealer = Convert.ToInt32(cmd.ExecuteScalar());

            if (con.State != ConnectionState.Closed)
                con.Close();
            return Is_Dealer;
        }
        catch (Exception exception)
        {
            ExceptionHandler.WriteEx(exception);
            return Is_Dealer;
        }
        finally
        {
            cmd.Dispose();
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }
    }

    public DataSet GetFarmerpmtDtls(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("GetFarmerpmtDtls", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FARMERID", objBELDIST.FARMER_ID);
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
    public DataSet FillCropDealerStock(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_CHECKCROP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LICENCE_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@SEASSION", objBELDIST.SESSION);
            cmd.Parameters.AddWithValue("@FIN_YR", objBELDIST.F_Year);
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
    public DataSet GetPrebooking(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_GETPREBOOKING", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@SP_TYPE", objBELDIST.USER_TYPE);
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
    public DataSet GetPrebookingFarmer(BLL_Dealer objBELDIST)
    {
        SqlConnection con = new SqlConnection(_connstrStock);

        SqlCommand cmd = new SqlCommand("SP_GETPREBOOKING_FARMERID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@LIC_NO", objBELDIST.LICENCENO);
            cmd.Parameters.AddWithValue("@FARMER_ID", objBELDIST.FARMER_ID);
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
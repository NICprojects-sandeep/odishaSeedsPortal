using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for BLL_ACC_LYR
/// </summary>
public class BLL_ACC_LYR
{
	public BLL_ACC_LYR()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public SqlConnection conn()
    {
        DB dal = new DB();
        try
        {
            return DB.DatabaseConnection();
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }

    public DataSet returnDataset(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.record_select(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        { 
            dal = null;

        }

    }
    public int Insert(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.InsertRecord(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }

    }
    public int Delete(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.DeleteRecord(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }

    }
    public int Trunc(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.TruncRecord(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }

    }
    public int Update(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.UpdateRecord(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }

    }
    public string insertPumpProceduremsg(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
          return  DB.insertProcedurepumpdataRMSG(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string insertPumpProcedure(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.insertProcedurepumpdata(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public int insertProc(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.insertdata(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }

    public int JAL_insertProcdata(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.JAL_insertProcdataRecord(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public DataSet JAL_param_passing_fetch(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.JAL_param_passing_fetchdata(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public DataSet JAL_param_passing_fetch_storeprocedure(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.JAL_param_passing_fetch_storeprocedureData(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public DataSet JAL_returnDataset(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.JAL_record_select(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }

    }
    public string JAL_insertProcedure(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.JAL_insertProceduredata(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }




    public int insertProcdata(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.insertProcdataRecord(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public int insertRollbackProc(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return dal.insertRollbackProcData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string insertProcc(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.insertProccData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string insertProcedure(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.insertProceduredata(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
   
    public string insertProcedure1(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.insertProcedure1data(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public DataSet param_passing_fetch(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.param_passing_fetchdata(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public DataSet param_passing_fetchFarmer(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.param_passing_fetchFarmerdata(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public DataSet param_passing_fetch_storeprocedure(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.param_passing_fetch_storeprocedureData(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public DataSet param_passing_fetch_global(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.param_passing_fetch_globalData(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public int param_passing_upd(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.param_passing_updData(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public DataTable param_passing_fetchDataTable(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.param_passing_fetchDataTableData(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public byte[] Ret_image(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.Ret_imageData(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }
    public string SendSMS(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.SendSMSData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string FMN_PermitNo(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.FMN_PermitNoRecord(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string SendSmsPOST(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.SendSmsPOSTData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string BroadcastSMS(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.BroadcastSMSData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public DataTable DataReaderData(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.DataReaderDataRecord(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string FMN_PermitSLNoNew(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.FMN_PermitSLNoNewData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string SLno(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.SLnoRecord(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string SLno1(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.SLno1Record(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    //*Financial Year Calculation*/
    public static string CalcFYear()
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
        catch (Exception ex)
        {
            return null;
        }
    }

    //*end  Financial Year Calculation*/

    public string executequery(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.executequeryData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }

    public string FMN_InvoiceSLNo(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.FMN_InvoiceSLNoNewData(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    public string insertProcedure_OutputMsg(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.insertProceduredata_OutputMsg(app);

        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }
    }
    #region " Validations for File Types"
    private enum ImageFileExtension
    {
        none = 0,
        jpg = 1,
        jpeg = 2,
        bmp = 3,
        gif = 4,
        png = 5
    }

    private enum VideoFileExtension
    {
        none = 0,
        wmv = 1,
        mpg = 2,
        mpeg = 3,
        mp4 = 4,
        avi = 5,
        flv = 6
    }

    private enum PDFFileExtension
    {
        none = 0,
        PDF = 1
    }

    private enum TextFileExtension
    {
        none = 0,
        txt = 1,
        doc = 2,
        docx = 3
    }

    public enum FileType
    {
        None = 0,
        Image = 1,
        Video = 2,
        PDF = 3,
        Text = 4,
        DOC = 5,
        DOCX = 6
    }

    public static bool isValidFile(byte[] bytFile, FileType flType, String FileContentType)
    {
        bool isvalid = false;

        if (flType == FileType.None)
        {
            return isvalid;
        }
        else if (flType == FileType.Image)
        {
            isvalid = isValidImageFile(bytFile, FileContentType);
        }
        else if (flType == FileType.Video)
        {
            isvalid = isValidVideoFile(bytFile, FileContentType);
        }
        else if (flType == FileType.PDF)
        {
            isvalid = isValidPDFFile(bytFile, FileContentType);
        }
        else if (flType == FileType.Text)
        {
            isvalid = isValidTextFile(bytFile, FileContentType);
        }

        return isvalid;

    }

    private static bool isValidImageFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytejpg = { 255, 216, 255, 224 };
        byte[] chkBytebmp = { 66, 77 };
        byte[] chkBytegif = { 71, 73, 70, 56 };
        byte[] chkBytepng = { 137, 80, 78, 71 };
        bool isvalid = false;

        ImageFileExtension imgfileExtn = ImageFileExtension.none;
        if (FileContentType.Contains("jpg") | FileContentType.Contains("jpeg"))
        {
            imgfileExtn = ImageFileExtension.jpg;
        }
        else if (FileContentType.Contains("bmp"))
        {
            imgfileExtn = ImageFileExtension.bmp;
        }
        else if (FileContentType.Contains("gif"))
        {
            imgfileExtn = ImageFileExtension.gif;
        }
        else if (FileContentType.Contains("png"))
        {
            imgfileExtn = ImageFileExtension.png;
        }

        if (imgfileExtn == ImageFileExtension.jpg || imgfileExtn == ImageFileExtension.jpeg)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytejpg[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.bmp)
        {
            if (bytFile.Length >= 2)
            {
                int j = 0;
                for (Int32 i = 0; i <= 1; i++)
                {
                    if (bytFile[i] == chkBytebmp[i])
                    {
                        j = j + 1;
                        if (j == 1)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.gif)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytegif[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.png)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytepng[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }

    private static bool isValidVideoFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytewmv = { 48, 38, 178, 117 };
        byte[] chkByteavi = { 82, 73, 70, 70 };
        byte[] chkByteflv = { 70, 76, 86, 1 };
        byte[] chkBytempg = { 0, 0, 1, 186 };
        byte[] chkBytemp4 = { 0, 0, 0, 20 };
        bool isvalid = false;

        VideoFileExtension vdofileExtn = VideoFileExtension.none;
        if (FileContentType.Contains("wmv"))
        {
            vdofileExtn = VideoFileExtension.wmv;
        }
        else if (FileContentType.Contains("mpg") || FileContentType.Contains("mpeg"))
        {
            vdofileExtn = VideoFileExtension.mpg;
        }
        else if (FileContentType.Contains("mp4"))
        {
            vdofileExtn = VideoFileExtension.mp4;
        }
        else if (FileContentType.Contains("avi"))
        {
            vdofileExtn = VideoFileExtension.avi;
        }
        else if (FileContentType.Contains("flv"))
        {
            vdofileExtn = VideoFileExtension.flv;
        }

        if (vdofileExtn == VideoFileExtension.wmv)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytewmv[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if ((vdofileExtn == VideoFileExtension.mpg || vdofileExtn == VideoFileExtension.mpeg))
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytempg[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.mp4)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytemp4[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.avi)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteavi[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.flv)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteflv[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;

        //if (isinvalid)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}
    }

    private static bool isValidPDFFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytepdf = { 37, 80, 68, 70 };
        bool isvalid = false;

        PDFFileExtension pdffileExtn = PDFFileExtension.none;
        if (FileContentType.Contains("pdf"))
        {
            pdffileExtn = PDFFileExtension.PDF;
        }

        if (pdffileExtn == PDFFileExtension.PDF)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytepdf[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }

    private static bool isValidTextFile(byte[] bytFile, String FileContentType)
    {
        byte[] chkBytetxt = { 99, 114, 101, 97 };
        byte[] chkBytedocx = { 208, 207, 17, 224 };
        byte[] chkBytedoc = { 80, 75, 3, 4 };
        byte[] chkBytetxt_ = { 255, 254, 66, 0 };

        bool isvalid = false;

        TextFileExtension txtfileExtn = TextFileExtension.none;
        if (FileContentType.Contains("txt") || FileContentType.Contains("text"))
        {
            txtfileExtn = TextFileExtension.txt;
        }
        else if (FileContentType.Contains("doc"))
        {
            txtfileExtn = TextFileExtension.doc;
        }
        else if (FileContentType.Contains("docx") || FileContentType.Contains("octet"))
        {
            txtfileExtn = TextFileExtension.docx;
        }

        if (txtfileExtn == TextFileExtension.txt)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytetxt[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (txtfileExtn == TextFileExtension.docx)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytedocx[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (txtfileExtn == TextFileExtension.doc)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (Int32 i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytedoc[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }
    #endregion

    public DataSet fetchFarmerAllData(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.record_fetchFarmerdata(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }

    }

    public int param_passing_updFarmerDB(BLL_Obj app)
    {
        DB dal = new DB();

        try
        {
            return DB.param_passing_updFarmerDBData(app);

        }

        catch (Exception info)
        {

            throw info;

        }

        finally
        {
            dal = null;

        }
    }

    public int TruncWithParam(BLL_Obj app)
    {
        DB dal = new DB();
        try
        {
            return DB.TruncRecordWithparam(app);
        }

        catch (Exception info)
        {
            throw info;
        }

        finally
        {
            dal = null;

        }

    }
}
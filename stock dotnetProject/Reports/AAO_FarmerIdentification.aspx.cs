using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;
using System.IO;
public partial class AAO_FarmerIdentification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();

            string sql = "GET_FarmerDetails_VoterId";
            string[] param = { "@VCHVOTERIDCARDNO" };
            // object[] value = { "OR18129256448" };
            object[] value = { Session["VCHVOTERIDCARDNO"].ToString() };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                litFarmerId.Text = ds.Tables[0].Rows[0]["NICFARMERID"].ToString();
                litFarmerId1.Text = ds.Tables[0].Rows[0]["NICFARMERID"].ToString();

                litFarmerName.Text = ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();
                litFarmerName1.Text = ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString();

                litFather.Text = ds.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();
                litFather1.Text = ds.Tables[0].Rows[0]["VCHFATHERNAME"].ToString();

                litGender.Text = ds.Tables[0].Rows[0]["Gender_Value"].ToString();
                litGender1.Text = ds.Tables[0].Rows[0]["Gender_Value"].ToString();

                litCategory.Text = ds.Tables[0].Rows[0]["Category_Value"].ToString();
                litCategory1.Text = ds.Tables[0].Rows[0]["Category_Value"].ToString();

                litDist.Text = ds.Tables[0].Rows[0]["Dist_Name"].ToString();
                litDist1.Text = ds.Tables[0].Rows[0]["Dist_Name"].ToString();

                litBlock.Text = ds.Tables[0].Rows[0]["block_name"].ToString();
                litBlock1.Text = ds.Tables[0].Rows[0]["block_name"].ToString();

                litGp.Text = ds.Tables[0].Rows[0]["GP_Name"].ToString();
                litGp1.Text = ds.Tables[0].Rows[0]["GP_Name"].ToString();

                litVillage.Text = ds.Tables[0].Rows[0]["villg_name"].ToString();
                litVillage1.Text = ds.Tables[0].Rows[0]["villg_name"].ToString();

                string code = ds.Tables[0].Rows[0]["NICFARMERID"].ToString() + "," + ds.Tables[0].Rows[0]["VCHFARMERNAME"].ToString() + ",";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 120;
                imgBarCode.Width = 120;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    td_QrCode.Controls.Add(imgBarCode);
                }

                System.Web.UI.WebControls.Image imgBarCode2 = new System.Web.UI.WebControls.Image();
                imgBarCode2.Height = 120;
                imgBarCode2.Width = 120;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    td_QrCode2.Controls.Add(imgBarCode2);
                }
            }

        }
    }
}
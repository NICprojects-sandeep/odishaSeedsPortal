using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QRCoder;
using System.Drawing;
using System.IO;

public partial class Masters_RptSaleVoucher : System.Web.UI.Page
{
    DataSet ds;
    DataSet ds1;
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
    protected void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Cache-Control", "no-cache,must-revalidate,no-store");

            if (!IsPostBack)
            {
                if (Session["CashMemo"] == null)
                {
                    Response.Redirect("~/ErrorPages/DefaultErrorPage.aspx");
                }
                else
                {
                    lblddnumber.Text = "DD/UTR Number :";
                    td_LicenceNo.Visible = false;

                    objUserBEL = new BLL_DropDown();
                    objUserBEL.CashMemo_No = Session["CashMemo"].ToString();
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.GetSupDepSale(objUserBEL);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lbl_stockedat.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["GODOWN_NAME"].ToString());
                            lblCompType.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["USER_TYPE"].ToString());

                            objUserBEL.Godown_ID = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["GODOWN_ID"].ToString());
                            Session["SALETO"] = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_TO"].ToString());
                            objUserBEL.Sale_Date = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_DATE"].ToString());
                            objUserBEL.CashMemo_No = HttpUtility.HtmlEncode(Session["CashMemo"].ToString());
                            objUserBEL.UserType = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["USER_TYPE"].ToString());
                            lbl_recvdt.Text = objUserBEL.Sale_Date;
                            lbl_cashmemono.Text = objUserBEL.CashMemo_No;

                            if (objUserBEL.UserType == "OAIC")
                            {
                                lblCompanyName.Text = "ODISHA AGRO INDUSTRIES CORPORATION LTD";
                                Image1.ImageUrl = "~/images/LogoofOAIC.png";
                            }
                            else
                            {
                                lblCompanyName.Text = "ODISHA STATE SEEDS CORPORATION LTD";
                                Image1.ImageUrl = "~/images/OSSCLogo.png";
                            }



                            string SUPPLY_TYPE = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SUPPLY_TYPE"].ToString());
                            string CreditBillNo = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["CREDIT_BILL_NO"].ToString());
                            string DEPT_TYPE = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["DEPT_TYPE"].ToString());
                            string SALE_TO = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["SALE_TO"].ToString());
                            lbl_DDNumber.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["DD_NUMBER"].ToString());
                            lbl_Amount.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["AMOUNT"].ToString());

                            objUserBEL = new BLL_DropDown();
                            objUserBEL.LIC_NO = SALE_TO;
                            objUserDLL = new DLL_DropDown();
                            ds1 = new DataSet();
                            lblddnumber.Visible = true;
                            lbl_DDNumber.Visible = true;
                            if (SUPPLY_TYPE == "1" || SUPPLY_TYPE == "6" || SUPPLY_TYPE == "9")
                            {
                                td_LicenceNo.Visible = true;

                                string Length = SALE_TO.Length.ToString();
                                if (Length == "19")
                                {
                                    ds1 = objUserDLL.GetDistFirmNameLicenceNo(objUserBEL);
                                }
                                else
                                {
                                    ds1 = objUserDLL.GetStateFirmNameLicenceNo(objUserBEL);
                                }

                                if (ds1.Tables.Count > 0)
                                {
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        lbl_saletonm.Text = HttpUtility.HtmlEncode(ds1.Tables[0].Rows[0]["APP_FIRMNAME"].ToString());
                                        lblLicenceNo.Text = HttpUtility.HtmlEncode(ds1.Tables[0].Rows[0]["LIC_NO"].ToString());
                                    }
                                }
                            }
                            else if (SUPPLY_TYPE == "2" || SUPPLY_TYPE == "7")
                            {
                                string[] param1 = { "@DIST_CODE" };
                                object[] val1 = { SALE_TO };

                                objUserBEL = new BLL_DropDown();
                                objUserBEL.DistCode = SALE_TO;

                                if (DEPT_TYPE == "01")
                                {
                                    string DistName = objUserDLL.GetDistNameByDistCode(objUserBEL);
                                    lbl_saletonm.Text = "DDA, " + HttpUtility.HtmlEncode(DistName);
                                }
                                else
                                {
                                    lbl_saletonm.Text = SALE_TO;
                                }
                                lblddnumber.Text = "Credit Bill No :";
                                lbl_DDNumber.Text = CreditBillNo;
                            }
                            else if (SUPPLY_TYPE == "3" || SUPPLY_TYPE == "8")
                            {
                                //string[] param1 = { "@GODOWN_ID" };
                                //object[] val1 = { SALE_TO };
                                objUserBEL = new BLL_DropDown();
                                objUserBEL.Godown_ID = HttpUtility.HtmlEncode(Session["SALETO"].ToString());
                                ds1 = objUserDLL.GetGoDownByGoDownId(objUserBEL);
                                if (ds1.Tables.Count > 0)
                                {
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        lbl_saletonm.Text = HttpUtility.HtmlEncode(ds1.Tables[0].Rows[0]["GODOWN_NAME"].ToString());
                                    }
                                }
                            }
                            else if (SUPPLY_TYPE == "12")
                            {
                                lbl_saletonm.Text = SALE_TO;
                                lblddnumber.Visible = false;
                                lbl_DDNumber.Visible = false;
                            }
                        }
                    }

                    lblReceiver.Text = HttpUtility.HtmlEncode(lbl_saletonm.Text);
                    objUserBEL = new BLL_DropDown();                    
                    objUserBEL.CashMemo_No = Session["CashMemo"].ToString();
                    objUserDLL = new DLL_DropDown();
                    ds = new DataSet();
                    ds = objUserDLL.FillSaleVoucher(objUserBEL);

                    grd_stockdtls.DataSource = ds;
                    grd_stockdtls.DataBind();
                    //grid footer
                    grd_stockdtls.FooterRow.Cells[6].Text = "  TOTAL  ";
                    grd_stockdtls.FooterRow.Cells[6].Font.Bold = true;

                    grd_stockdtls.FooterRow.Cells[7].Font.Bold = true;
                    for (int i = 6; i <= grd_stockdtls.Columns.Count - 1; i++)
                    {
                        Decimal total1 = 0;
                        foreach (GridViewRow gdv in grd_stockdtls.Rows)
                        {
                            Label lbl_Qtlsold = (Label)gdv.FindControl("lbl_Qtlsold");
                            total1 = total1 + Convert.ToDecimal(lbl_Qtlsold.Text);
                        }
                        grd_stockdtls.FooterRow.Cells[7].Text = total1.ToString();
                    }
                    Decimal total2 = 0;
                    for (int i = 8; i <= grd_stockdtls.Columns.Count - 1; i++)
                    {
                        Decimal total3 = 0;
                        foreach (GridViewRow gdv in grd_stockdtls.Rows)
                        {
                            Label lbl_amount = (Label)gdv.FindControl("lbl_amount");
                            total3 = total3 + Convert.ToDecimal(lbl_amount.Text);
                        }
                        grd_stockdtls.FooterRow.Cells[9].Text = total3.ToString();
                        total2 = total3;
                    }
                    lbl_Amount.Text = total2.ToString("N2");
                    string code = lbl_cashmemono.Text + "," + HttpUtility.HtmlDecode(lblLicenceNo.Text) + "," + total2.ToString();
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
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
                }
            }
        }
        catch (Exception ee)
        {
        }
    }
    
    public void MergeCell(int[] colIndices)
    {
        foreach (int colIndex in colIndices)
        {
            for (int rowIndex = grd_stockdtls.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = grd_stockdtls.Rows[rowIndex];
                GridViewRow previousRow = grd_stockdtls.Rows[rowIndex + 1];
                if (row.Cells[colIndex].Text == previousRow.Cells[colIndex].Text)
                {
                    row.Cells[colIndex].RowSpan = previousRow.Cells[colIndex].RowSpan < 2 ? 2 : previousRow.Cells[colIndex].RowSpan + 1;
                    previousRow.Cells[colIndex].Visible = false;
                }
            }
        }
    }
    protected void grd_stockdtls_PreRender(object sender, EventArgs e)
    {
        MergeCell(new int[] { 0 });
        MergeCell(new int[] { 1 });
        MergeCell(new int[] { 2 });
        MergeCell(new int[] { 3 });
        
    }
}
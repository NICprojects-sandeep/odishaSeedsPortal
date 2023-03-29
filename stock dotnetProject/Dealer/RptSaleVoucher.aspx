<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptSaleVoucher.aspx.cs" Inherits="Masters_RptSaleVoucher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Stock Delivery Report::</title>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script lang="JavaScript" type="text/javascript">
        function printpage() {
            document.getElementById('btnPrint').style.visibility = 'hidden';
            window.print()
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-1">
                    <asp:HiddenField ID="hfTransactionNo" runat="server" />
                    <asp:HiddenField ID="hfFarmerId" runat="server" />
                    <asp:HiddenField ID="hfLicNo" runat="server" />
                    <asp:Button id="btnSale" runat="server" OnClick="btnSale_Click" Text="New Sale"/>
                </div>
                <div class="col-10  border">
                    <div class="row">
                        <div class="col-12 text-lg-left">
                            <table border="1" cellpadding="0" cellspacing="0" width="700px">
                                <tr>
                                    <td align="center">
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%" style="font-size: 10pt; font-family: Verdana">
                                            <tr>
                                                <td colspan="2" style="height: 34px" align="right"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center"><strong><span class="style1">
                                                    <br />
                                                    INVOICE<br />
                                                </span></strong>
                                                    <br />
                                                    Stock Delivered from
        <asp:Label ID="lbl_LicNo" runat="server" Font-Bold="True"></asp:Label>
                                                    (<asp:Label ID="lbl_LicName" runat="server" Font-Bold="True"></asp:Label>
                                                    )<br />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 20px">Transaction Id : 
            <asp:Label ID="lbl_TransactionId" runat="server" Font-Bold="True"></asp:Label>
                                                    <h3 class="r"
                                                        style="font-size: 18px; font-weight: normal; margin: 0px; padding: 0px; display: block; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; color: rgb(34, 34, 34); font-family: arial, sans-serif; font-style: normal; font-variant: normal; letter-spacing: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">&nbsp;</h3>
                                                </td>
                                                <td align="right" style="height: 20px">Delivered Date:
                                        <asp:Label ID="lbl_recvdt" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"><strong>&nbsp;Seed Delivered To </strong>
                                                    <br />
                                                </td>
                                                <td align="left" id="td1" runat="server">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left">
                                                    <table style="margin-right: 4px">
                                                        <tr>
                                                            <td align="left" width="150">Name 
                                                            </td>
                                                            <td align="center" width="20">:</td>
                                                            <td id="td_Name" align="left" runat="server"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="150">Father's Name
                                                            </td>
                                                            <td align="center" width="20">:</td>
                                                            <td id="td_FName" align="left" runat="server"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="150">Mobile Number
                                                            </td>
                                                            <td align="center" width="20">:</td>
                                                            <td id="td_MobNo" align="left" runat="server"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="150">Village
                                                            </td>
                                                            <td align="center" width="20">:</td>
                                                            <td id="td_Village" align="left" runat="server"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="150">GP
                                                            </td>
                                                            <td align="center" width="20">:</td>
                                                            <td id="td_GP" align="left" runat="server"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="150">Block
                                                            </td>
                                                            <td align="center" width="20">:</td>
                                                            <td id="td_Block" align="left" runat="server"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="150">Dist
                                                            </td>
                                                            <td align="center" width="20">:</td>
                                                            <td id="td_Dist" align="left" runat="server"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left" style="font-weight: bold">Delivery Details:<br />
                                                    <hr />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left">
                                                    <asp:GridView ID="grd_stockdtls" runat="server" AutoGenerateColumns="false"
                                                        ShowFooter="false" Width="100%" Font-Size="8pt"
                                                        OnRowDataBound="grd_stockdtls_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Crop Name">
                                                                <ItemTemplate>
                                                                    <%#Eval("Crop_Name")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Crop Variety">
                                                                <ItemTemplate>
                                                                    <%#Eval("Variety_Name")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Class1" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Source">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SorceID1" runat="server" Text='<%#Eval("Receive_Unitcd") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Source1" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lot No.">
                                                                <ItemTemplate>
                                                                    <%#Eval("LOT_NUMBER")%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No. of Bags">
                                                                <ItemTemplate>
                                                                    <%#Eval("NO_OF_BAGS")%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bag Size(in Kg.)">
                                                                <ItemTemplate>
                                                                    <%#Eval("BAG_SIZE_KG")%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity (in Qtl.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Qtlsold" runat="server" Text='<%#Eval("TOT_QTL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price/Qtl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rate" runat="server" Text='<%#Eval("PRICE_QTL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("ALL_IN_COST_AMOUNT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Total Subsidy">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Subsidy" runat="server" Text='<%#Eval("SUBSIDY_AMOUNT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="No Item Sold From this Godown" ForeColor="red"></asp:Label>
                                                        </EmptyDataTemplate>
                                                        <RowStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2"></td>
                                            </tr>

                                            <tr>
                                                <td colspan="2" align="right">
                                                    <table width="50%">
                                                        <tr>
                                                            <td align="left" width="150">Total Amount</td>
                                                            <td align="center" width="25">:</td>
                                                            <td id="td_TotAmt" runat="server">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" width="150">Subsidy Amount</td>
                                                            <td align="center" width="25">:</td>
                                                            <td id="td_SubAmt" runat="server">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left">
                                                    <br />

                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left"></td>
                                            </tr>
                                            <tr>
                                                <td align="left">Signature of <strong>
                                                    <asp:Label ID="lblReceiver" runat="server"></asp:Label></strong>

                                                </td>
                                                <td align="right">
                                                    <br />
                                                    Signature of 
        <asp:Label ID="lbl_LicName0" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"></td>
                                                <td align="right">(<asp:Label
                                                    ID="lbl_LicNo0" runat="server" Font-Bold="True"></asp:Label>
                                                    )&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right" style="height: 20px">
                                                    *The subsidy amount will be credited to your account number <asp:Label
                                                    ID="lblAccNo" runat="server" Font-Bold="True"></asp:Label> of Bank <asp:Label
                                                    ID="lblBankName" runat="server" Font-Bold="True"></asp:Label>.</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>
                    <br />
                </div>
                <div class="col-1"></div>
            </div>
            <br />
        </div>
    </form>
</body>
</html>

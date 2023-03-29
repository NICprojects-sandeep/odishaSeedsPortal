<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptSaleVoucher.aspx.cs" Inherits="Masters_RptSaleVoucher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Stock Delivery Report::</title>
    <script lang="JavaScript" type="text/javascript">
        function printpage() {
            document.getElementById('btnPrint').style.visibility = 'hidden';
            window.print()
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <table border="1" cellpadding="0" cellspacing="0" width="700px">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="2" cellspacing="0" width="100%" style="font-size: 10pt; font-family: Verdana">
                                <tr>
                                    <td colspan="2" style="height: 34px" align="right">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="printpage()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 38px" align="center">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width:80%;text-align:center;">
                                                    <asp:LinkButton ID="LinkButton2" runat="server">
                                                        <asp:Image ID="Image1" runat="server" Width="150" Height="150" />
                                                        <%--<img src="../images/orissalogo.jpg" alt="" border="0"/>--%></asp:LinkButton>
                                                </td>
                                                <td style="width:20%" id="td_QrCode" runat="server">

                                                </td>
                                            </tr>
                                        </table>                                        
                                        
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="font-weight: bold">
                                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right"></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">Stock Delivered from
        <asp:Label ID="lbl_stockedat" runat="server" Font-Bold="True"></asp:Label>
                                        Godown<br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 20px">CashMemo Number: 
            <asp:Label ID="lbl_cashmemono" runat="server" Font-Bold="True"></asp:Label>
                                        <h3 class="r"
                                            style="font-size: 18px; font-weight: normal; margin: 0px; padding: 0px; display: block; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; color: rgb(34, 34, 34); font-family: arial, sans-serif; font-style: normal; font-variant: normal; letter-spacing: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">&nbsp;</h3>
                                    </td>
                                    <td align="right" style="height: 20px">Delivered Date:
            <asp:Label ID="lbl_recvdt" runat="server" Font-Bold="True"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;Delivered To :<asp:Label
                                        ID="lbl_saletonm" runat="server" Font-Bold="True" Font-Size="8pt"></asp:Label><br />
                                    </td>
                                    <td align="left" id="td_LicenceNo" runat="server">Licence No :
                                        <asp:Label ID="lblLicenceNo" runat="server" style="font-weight: 700"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align:left;"><b>Payment Details:</b>&nbsp;
                                            <asp:Label ID="lblddnumber" runat="server"></asp:Label><asp:Label
                                        ID="lbl_DDNumber" runat="server" Font-Bold="True" Font-Size="8pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            Amount(in Rs.) :<asp:Label
                                        ID="lbl_Amount" runat="server" Font-Bold="True" Font-Size="8pt"></asp:Label>

                                        <br />
                                        <br />
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
                                        <asp:GridView ID="grd_stockdtls" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="100%" OnPreRender="grd_stockdtls_PreRender">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="Crop_Name" HeaderText="Crop Name" />
                                                <asp:BoundField DataField="CLASS" HeaderText="Class" />
                                                <asp:BoundField DataField="Variety_Name" HeaderText="Crop Variety" />                                                
                                                <asp:BoundField DataField="Receive_Unitname" HeaderText="Source" />
                                                <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot No." />

                                                <%--<asp:BoundField DataField="LOT_NUMBER" HeaderText="No. of Bags" />
                                                <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot No." />
                                                <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot No." />
                                                <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot No." />
                                                <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot No." />--%>
                                                <%--<asp:TemplateField HeaderText="Crop Name">
                                                    <ItemTemplate>
                                                        <%#Eval("Crop_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Crop Variety">
                                                    <ItemTemplate>
                                                        <%#Eval("Variety_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <%#Eval("CLASS")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Source">
                                                    <ItemTemplate>
                                                        <%#Eval("Receive_Unitname")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot No.">
                                                    <ItemTemplate>
                                                        <%#Eval("LOT_NUMBER")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="No. of Bags">
                                                    <ItemTemplate>
                                                        <%#Eval("SALE_NO_OF_BAG")%>
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
                                                        <asp:Label ID="lbl_Qtlsold" runat="server" Text='<%#Eval("Quantity_sale") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price/Qtl">
                                                <ItemTemplate>
                                                <asp:Label ID="lbl_rate" runat="server" Text='<%#Eval("All_in_cost_Price") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("total_amount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>                                                
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
                                <td align="left">

                                    Signature of <strong>
                                            <asp:Label ID="lblReceiver" runat="server"></asp:Label></strong>

                                </td>
                                    <td align="right">
                                        <br />
                                        Signature of <strong>
                                            <asp:Label ID="lblCompType" runat="server"></asp:Label></strong>(Store in Charge) &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                <td align="left">

                                </td>
                                    <td align="right">
                                        <asp:Label ID="lbl_nmincharge" runat="server" Font-Bold="True"></asp:Label>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right" style="height: 20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                </tr>


                            </table>
                        </td>
                    </tr>
                </table>
            </center>

        </div>
    </form>
</body>
</html>

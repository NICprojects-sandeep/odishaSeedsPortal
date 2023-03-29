<%@ Page Title="::Dealer Stock::" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="Rpt_Dealer_Stock.aspx.cs" Inherits="Masters_Rpt_Dealer_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlPrintArea.ClientID %>");
        var printWindow = window.open('', '', 'height=400,width=800');
        printWindow.document.write('<html><head><title>Stock Details</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(panel.innerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        setTimeout(function () {
            printWindow.print();
        }, 500);
        return false;
    }
    </script>
    <style type="text/css">
        .style1
        {
            width: 161px;
        }
    </style>
<center>
        <asp:Panel ID="Panel1" runat="server" Width="700px">
            <fieldset>
                <table width="700px">
                    <tr>
                        <td width="20%">
                            Select a Dealer :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged"
                                Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td><asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();" /></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlPrintArea" runat="server" Width="700px">
            <fieldset>
                <table width="100%">       
                <tr>
                <td>
                <table width="100%">
                <tr>
                <td align="left" width="175px">
                Dealer Licence Number : 
                </td>
                <td align="left">
                <asp:Label ID="lblLicNo" runat="server" Text="" CssClass="bold" ></asp:Label>
                </td>
                </tr>
                <tr>
                <td align="left" width="175px">
                Dealer Name : 
                </td>
                <td align="left">
                <asp:Label ID="lblName" runat="server" Text="" CssClass="bold" ></asp:Label>
                </td>
                </tr>
                </table>
                </td>
                </tr>         
                    <tr>
                <td align="right">
                Stock As On :<asp:Label ID="lblDate" runat="server" 
                        style="font-weight: 700"></asp:Label>
                </td>
                </tr>
                    <tr>
                        <td align="center">                            
                            <asp:GridView ID="grd_stockdtls" runat="server" AutoGenerateColumns="False" ShowFooter="true" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Name">
                                        <ItemTemplate>
                                            <%#Eval("CROP_NAME")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Variety">
                                        <ItemTemplate>
                                            <%#Eval("CROP_VERNAME")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class">
                                        <ItemTemplate>
                                            <%#Eval("CLASS")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Source">
                                        <ItemTemplate>
                                            <%#Eval("RECEIVE_UNITNAME")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lot No.">
                                        <ItemTemplate>
                                            <%#Eval("LOT_NO")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. of Bags">
                                        <ItemTemplate>
                                            <%#Eval("STOCK_NOOFBAGS")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bag Size(in Kg.)">
                                        <ItemTemplate>
                                            <%#Eval("BAG_SIZE_IN_KG")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity (in Qtl.)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Qtlsold" runat="server" Text='<%#Eval("AVL_QUANTITY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="All-In Cost" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_rate" runat="server" Text='<%#Eval("PRICE_QTL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("TOTAL_AMOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="No Item Found" ForeColor="red"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle Font-Size="12px" />
                            </asp:GridView>
                        </td>
                    </tr>                    
                </table>
            </fieldset>
        </asp:Panel>
    </center>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="RptGoDownTransfer.aspx.cs" Inherits="Masters_RptGoDownTransfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
    <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=800');
            printWindow.document.write('<html><head><title>SaleDetails Report</title>');
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
<ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <div style="width: 1100px; background-color: #666; padding: 2px; margin: 0 auto; padding-top: 10px;">
                    <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1100px;">
                        <table style="padding: 10px;" class="tabletxt">
                            <tr>
                                <td style="width: 115px;" class="normaltd">Sale Date</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:TextBox ID="txtSaleDate" runat="server" Width="120px" CssClass="TextboxCss"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" SetFocusOnError="True"
                                                ControlToValidate="txtSaleDate" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                                ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSaleDate"
                                                Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                </td>
                                <td class="normaltd">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                    &nbsp;
                                </td>                                
                            </tr>
                        </table>
                        <asp:Panel ID="pnlContents" runat="server">
                            <asp:GridView ID="gvSaleDtls" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvSaleDtls_RowCommand">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField HeaderText="Cash Memo No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_CASH_MEMO_NO" runat="server" Text='<%#Eval("CASH_MEMO_NO")%>' CausesValidation="false" CommandName="Select"></asp:LinkButton>                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From GoDown">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_APP_FIRMNAME" runat="server" Text='<%#Eval("FROM_GODOWN")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To GoDown">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SALE_TO" runat="server" Text='<%#Eval("TO_GODOWN")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>                                                                        
                                </Columns>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
            </div>
                        <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="updatepanel"
                            DisplayAfter="0">
                            <ProgressTemplate>
                                <div style="text-align: center; vertical-align: middle; position: absolute; top: 0px; left: 0px; z-index: 99; width: 100%; height: 1000px; background-color: #ededed; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60); -moz-opacity: 0.8; opacity: 0.8;"
                                    visible="true">
                                    <div style="width: 48px; height: 48px; margin: 0 auto; margin-top: 275px;">
                                        <asp:Image ImageUrl="~/images/loading.gif" Width="48px" Height="48px" runat="server"
                                            ID="TaskLoadingImage" />
                                    </div>
                                    <div style="font-family: Trebuchet MS; font-size: 12px; color: Red; text-align: center;">
                                        Please Wait ...
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
        </ContentTemplate>

    </ajax:UpdatePanel>
    </asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Scheme.master" AutoEventWireup="true" CodeFile="CCBpayment.aspx.cs" Inherits="CCBpayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("dvContents").innerHTML;
            var printWindow = window.open('', '', 'height=500,width=700');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>    
    <style type="text/css">
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 950px;
            height: 500px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup .body
        {
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .style5
        {
            color: #0000FF;
        }
        p.sansserif
        {
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
<asp:UpdatePanel ID="UpdPnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                <tr>
                    <td class="auto-style14" style="width: 74px">Session Year</td>
                    <td class="auto-style19">:</td>
                    <td class="auto-style15" style="width: 210px">
                        <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                        </asp:DropDownList></td>
                        <td class="auto-style19"><asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
                </tr>
                    <td style="width: 30%;">
                    </td>
                    <td style="width: 40%;">
                        <table style="width: 100%;">                            
                            <tr>
                                <td align="center">
                                    <br />
                                     <asp:GridView ID="gridRequest" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        AutoGenerateColumns="False" CellSpacing="2" Style="height: 138px" OnRowCommand="gridRequest_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>  
                                            <asp:TemplateField HeaderText="Sl No" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle Width="20px" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>                                          
                                            <asp:TemplateField HeaderText="CPAID">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnCPAID" Text='<%#Eval("CPAID") %>' runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />
                                                    <asp:Label ID="lblCPAID" runat="server" Text='<%#Eval("CPAID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="150px" />
                                                <FooterStyle Width="150px" />
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:0.00}"/>
                                            <asp:BoundField DataField="NOOFTRANS" HeaderText="No of Transactions" />
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
                            </tr>                            
                        </table>
                    </td>
                    <td style="width: 30%;">
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnEditPopUp" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="popupEdit" runat="server" OnOkScript="onOk()" OnCancelScript="onCancel()"
                TargetControlID="btnEditPopUp" PopupControlID="pnlEdit" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlEdit" runat="server" CssClass="modalPopup">                
                <div id="dvTable">
                <table style="width: 100%;" cellspacing="4">
                    <tr>
                        <td align="center">

                            <asp:Button ID="btnClose" runat="server" Text="Close" Width="100px" OnClick="btnClose_Click" />&nbsp;&nbsp;
&nbsp;                      <asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" OnClientClick="PrintDiv();" />                            
                        </td>
                    </tr>                    
                    <tr>
                        <td align="center">
                            <br />
                           <asp:Panel ID="pnlSelected" runat="server" ScrollBars="Auto" Height="440px">
                            <fieldset>
                                <div id="dvContents">
                                <asp:Literal ID="litReport" runat="server"></asp:Literal>
                                    </div>
                            </fieldset>
                        </asp:Panel>
                            <%--<asp:Panel ID="pnlSelected" runat="server" ScrollBars="Auto" Height="440px">
                                <fieldset>
                                     <div id="dvContents">
                                    <asp:GridView ID="gvSaleDtl" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        CellSpacing="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle Width="20px" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NewTrans" HeaderText="Transaction No" />
                                            <asp:BoundField DataField="LIC_NO" HeaderText="Licence Number" />
                                            <asp:BoundField DataField="Crop_Name" HeaderText="Crop Name" />
                                            <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name" />
                                            <asp:BoundField DataField="TOT_QTL" HeaderText="Total Sale" />
                                            <asp:BoundField DataField="ADMISSIBLE_SUBSIDY" HeaderText="Subsidy" />
                                            <asp:BoundField DataField="TOT_SUB_AMOUNT_GOI" HeaderText="GOI Amount" />
                                            <asp:BoundField DataField="TOT_SUB_AMOUNT_SP" HeaderText="SP Amount" />
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
                                           </div>
                                </fieldset>                            
                            </asp:Panel>--%>
                          
                        </td>
                    </tr>                      
                </table> 
                    </div>                   
            </asp:Panel>
        </ContentTemplate>  
    </asp:UpdatePanel>
</asp:Content>


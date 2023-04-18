<%@ Page Title="::Sale Cancel::" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="StockSaleCancel.aspx.cs" Inherits="Masters_StockSaleCancel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;        
    }
</style>
    <div class="bedcromb">
                <asp:Label ID="lblTitle" runat="server" Text="Sale Cancellation"></asp:Label>
            </div>
    <div style="padding-top: 15px; padding-left: 30px;">

        <div>
            <asp:UpdatePanel ID="upStockSALE" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <table>
        <tr>
            <td>
                Cash Memo No :
            </td>
            <td>
                <asp:TextBox ID="txtCashMemoNo" runat="server" Width="200px" Text=""></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
            <table id="tblDtls" runat="server">
                <tr>
                    <td>
                        Gown Name : 
                    </td>
                    <td id="tdGODOWN_NAME" runat="server" style="color:blue; font-weight: 700;">
                    </td>
                    <td>
                        Sale Date : 
                    </td>
                    <td id="tdSALE_DATE" runat="server" style="color:blue; font-weight: 700;">
                    </td>
                    <td>
                        Licence No : 
                    </td>
                    <td id="tdAPP_FIRMNAME" runat="server" style="color:blue; font-weight: 700;">
                    </td>
                    <td>
                        Updated By : 
                    </td>
                    <td id="tdUPDATED_BY" runat="server" style="color:blue; font-weight: 700;">
                            
                    </td>
                </tr>
            </table>
            <hr />
            <asp:GridView ID="gvCashMemoDtls" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Transaction No" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="lbtnSALETRANSID" runat="server" OnClick="lbtnSALETRANSID_Click"><%# Eval("SALETRANSID") %></asp:LinkButton>                     
                        <asp:Label ID="lblSALETRANSID" runat="server" Text='<%#Eval("SALETRANSID")%>' visible="false"></asp:Label>
                        <asp:Label ID="lblGODOWN_ID" runat="server" Text='<%#Eval("GODOWN_ID")%>' visible="false"></asp:Label>
                        <asp:Label ID="lblLIC_NO" runat="server" Text='<%#Eval("LIC_NO")%>' visible="false"></asp:Label>                        
                    </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="Crop_Name" HeaderText="Crop Name" />
                    <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name" />
                    <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot Number" />
                    <asp:BoundField DataField="BAG_SIZE_KG" HeaderText="Bag Size" />
                    <asp:BoundField DataField="SALE_NO_OF_BAG" HeaderText="No of Bags" />
                    <asp:BoundField DataField="BAGS_QTL" HeaderText="BAGS(QTL)" />
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" OnClick="btnShow_Click" />
            <%--<asp:ModalPopupExtender ID="ModalPopUp" runat="server" PopupControlID="pnlPopUp"
                 TargetControlID="btnShow" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>--%>
                    <%--<asp:ModalPopupExtender ID="ModalPopUp" runat="server" PopupControlID="pnlPopUp"
                        TargetControlID="btnShow" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>--%>
            <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" Style="width:900px;border-spacing:0px;padding:0px; height:300px;overflow-y:auto;
                        text-align: left; vertical-align: top;" ScrollBars="Auto">
                <asp:Label ID="lblSALETRANSID1" runat="server" Text="" Visible="false"></asp:Label>
                <table style="width:100%">
                    <tr>
                        <td>Sale Date</td>
                        <td>&nbsp;: 
                        </td>
                        <td id="td_SALE_DATE" runat="server" style="color:blue; font-weight: 700;" ></td>
                        <td>Licence No</td>
                        <td>&nbsp;: </td>
                        <td id="td_APP_FIRMNAME" runat="server" style="color:blue; font-weight: 700;"></td>
                    </tr>
                    <tr>
                        <td>Godown Name</td>
                        <td>&nbsp;: 
                        </td>
                        <td id="td_GODOWN_NAME" runat="server" style="color:blue; font-weight: 700;"></td>
                        <td>Lot No</td>
                        <td>&nbsp;: </td>
                        <td id="td_LOTNO" runat="server" style="color:blue; font-weight: 700;"></td>
                    </tr>
                </table>
                <hr />
                 <table style="width: 100%">
                            <tr>
                                <td>
                                    <fieldset>
                                        <legend><asp:Label ID="lblSellerType" runat="server" /> SALE</legend>
                                        <table>
                                            <tr>
                                                <td>Bag Size 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdBagSize1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>No of Bags 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdNoofBags1" runat="server"></td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td>
                                    <fieldset>
                                        <legend><asp:Label ID="lblReceiverType" runat="server" /> Receive</legend>
                                        <table>
                                            <tr>
                                                <td>Bag Size 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdBagSize4" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Rcv No of Bags 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdRcvNoofBags4" runat="server"></td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td>
                                    <fieldset>
                                        <legend><asp:Label ID="lblSeller" runat="server" />Stock</legend>
                                        <table>
                                            <tr>
                                                <td>Bag Size 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdBagSize2" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Rcv No of Bags 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdRcvNoofBags2" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Avl No of Bags 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdAvlNoofBags2" runat="server"></td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td>
                                    <fieldset>
                                        <legend><asp:Label ID="lblReceiver" runat="server" />Stock</legend>
                                        <table>
                                            <tr>
                                                <td>Bag Size 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdBagSize3" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Rcv No of Bags 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdRcvNoofBags3" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Avl No of Bags 
                                                </td>
                                                <td>:
                                                </td>
                                                <td id="tdAvlNoofBags3" runat="server"></td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>                                
                            </tr>
                        </table>
                
                <br />
                <hr />
                <center><asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label></center>
                
                <br />
                <center>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel Transaction" OnClick="btnCancel_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" Text="Close"  />
                        </td>
                    </tr>
                </table>
                </center>
                    </asp:Panel>
            <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upStockSALE"
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
    </asp:UpdatePanel>
    </div>
        </div>
    
</asp:Content>


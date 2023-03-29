<%@ Page Title="::Return Dealer Stock To OSSC::" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="ReturnToOSSC.aspx.cs" Inherits="Masters_ReturnToOSSC" %>
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
    <script lang="javascript" type="text/javascript">
    function RadioCheck(rb) {
            var gv = document.getElementById("<%=gvCashmemoDtls.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
        </script>
    <div class="bedcromb">
                <asp:Label ID="lblTitle" runat="server" Text="Sale Cancellation"></asp:Label>
            </div>
    <div style="padding-top: 15px; padding-left: 30px;">

        <div>
            <asp:UpdatePanel ID="UpdReturn" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td>Licence Number :
                    </td>
                    <td>
                        <asp:TextBox ID="txtLicNo" runat="server" Width="200px" Text=""></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr id="trDtls" runat="server">
                    <td>Dealer Name : 
                    </td>
                    <td id="td_APP_FIRMNAME" runat="server" colspan="2"></td>
                </tr>
            </table>
            <hr />
            <asp:GridView ID="gvLotDtls" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Crop_Name" HeaderText="Crop Name" />
                    <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name" />
                    <asp:TemplateField HeaderText="Lot Number" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="lbtnLOT_NUMBER" runat="server" OnClick="lbtnLOT_NUMBER_Click"><%# Eval("LOT_NO") %></asp:LinkButton>
                        <asp:Label ID="lblLIC_NO" runat="server" Text='<%#Eval("LICENCE_NO")%>' visible="false"></asp:Label>                        
                        <asp:Label ID="lblLOT_NUMBER" runat="server" Text='<%#Eval("LOT_NO")%>' visible="false"></asp:Label> 
                        <asp:Label ID="lblCrop" runat="server" Text='<%#Eval("Crop_Name")%>' visible="false"></asp:Label>                        
                        <asp:Label ID="lblVariety" runat="server" Text='<%#Eval("Variety_Name")%>' visible="false"></asp:Label> 
                        <asp:Label ID="lblAvlQty" runat="server" Text='<%#Eval("AVL_NO_OF_BAGS")%>' visible="false"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:BoundField DataField="BAG_SIZE_IN_KG" HeaderText="Bag Size" />
                    <asp:BoundField DataField="RECV_NO_OF_BAGS" HeaderText="No of Bags(Rcv)" />
                    <asp:BoundField DataField="AVL_NO_OF_BAGS" HeaderText="No of Bags(Avl)" />
                    <asp:BoundField DataField="AVL_QUANTITY" HeaderText="BAGS(QTL)" />
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
            <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopUp" runat="server" PopupControlID="pnlPopUp"
                 TargetControlID="btnShow" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>                    
            <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" Style="display:none;width:1100px;border-spacing:0px;padding:0px; height:300px;overflow-y:auto;
                        text-align: left; vertical-align: top;" ScrollBars="Auto">
                <asp:Label ID="lblLotNo1" runat="server" Text="" Visible="false"></asp:Label>
                <table style="width:100%">
                    <tr>     
                        <td>Crop : </td>                   
                        <td id="td_Crop" runat="server" style="color:blue; font-weight: 700;" ></td>                        
                        <td>Variety : </td>
                        <td id="td_Variety" runat="server" style="color:blue; font-weight: 700;"></td>
                        <td>Lot Number : </td>
                        <td id="td_LotNo" runat="server" style="color:blue; font-weight: 700;" ></td>
                    </tr>                    
                </table>
                <hr />
                <asp:Label ID="lblSALETRANSID1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:GridView ID="gvCashmemoDtls" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>                                              
                        <asp:RadioButton ID="rbtnCashMemo" runat="server" AutoPostBack="True" OnCheckedChanged="rbtnCashMemo_CheckedChanged" onclick="RadioCheck(this);"></asp:RadioButton>
                        <asp:Label ID="lblSALETRANSID" runat="server" Text='<%#Eval("SALETRANSID")%>' visible="false"></asp:Label>
                        <asp:Label ID="lblSALE_NO_OF_BAG" runat="server" Text='<%#Eval("SALE_NO_OF_BAG")%>' visible="false"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="CASH_MEMO_NO" HeaderText="CashMemo Number" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />                     
                    <asp:BoundField DataField="SALE_DATE" HeaderText="Sale Date" />
                    <asp:BoundField DataField="DD_NUMBER" HeaderText="DD No" />
                    <asp:BoundField DataField="DD_AMOUNT" HeaderText="DD Amt" />                    
                    <asp:BoundField DataField="PRICE_QTL" HeaderText="Price/Qtl" />
                    <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                    <asp:BoundField DataField="BAG_SIZE_KG" HeaderText="Bag Size" />
                    <asp:BoundField DataField="SALE_NO_OF_BAG" HeaderText="No of Bags" />                    
                    <asp:BoundField DataField="UPDATED_ON" HeaderText="Entry Date" />
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
                <br />
                <hr />
                <center><asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label></center>
                
                <br />
                <center>
                <table>
                    <tr>
                        <td>
                            Available no of Bags : &nbsp;<asp:Label ID="lblAvlQnty" runat="server" Text="" Font-Bold="True"></asp:Label> 
                        </td>
                        <td>
                            &nbsp;&nbsp;Return no of Bags : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtBags" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Return" OnClick="btnCancel_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" Text="Close"  />
                        </td>
                    </tr>
                </table>
                </center>
                    </asp:Panel>
            <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="UpdReturn"
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


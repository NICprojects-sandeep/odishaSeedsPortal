<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="TransactionzeroList.aspx.cs" EnableEventValidation="false" Inherits="Masters_TransactionzeroList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script language="javascript" type="text/javascript">
        function CnfSubmit() {

            if (confirm("Confirm this farmerid before updating zero id. Do you want to continue?")) {

                return true;
            }
            else {

                return false;
            }
        }
    </script>
    <script lang="javascript" type="text/javascript">
        function blockNonNumbers(obj, e, allowDecimal, allowNegative) {


            var key;
            var isCtrl = false;
            var keychar;
            var reg;

            if (window.event) {
                key = e.keyCode;
                isCtrl = window.event.ctrlKey
            }
            else if (e.which) {
                key = e.which;
                isCtrl = e.ctrlKey;
            }

            if (isNaN(key)) return true;

            keychar = String.fromCharCode(key);

            // check for backspace or delete, or if Ctrl was pressed
            if (key == 8 || isCtrl) {
                return true;
            }

            reg = /\d/;
            var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
            var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

            return isFirstN || isFirstD || reg.test(keychar);
        }
    </script>
    <style type="text/css">
        .modalPopup {
            background-color: #FFFFFF;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup .body {
            min-height: 100px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
    </style>
    <asp:UpdatePanel ID="upStockOB" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                        <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center"><strong>Search By Transaction ID</strong>&nbsp;:&nbsp;<asp:TextBox ID="txtTransIDSearch" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" />&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" OnClick="btnClear_Click" />&nbsp;<asp:Button ID="btnViewAll" runat="server" Text="View All" Width="100px" Visible="false" OnClick="btnViewAll_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--  <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />--%>
                        <asp:GridView ID="grd_TransactionList" runat="server" BackColor="#B2C629"
                            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" Width="98%" ForeColor="Black" OnRowDataBound="grd_TransactionList_RowDataBound">
                            <RowStyle BackColor="White" CssClass="tbl" />
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction_ID" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfTransId" runat="server" Value='<%#Eval("TRANSACTION_ID")%>' />
                                        <asp:HiddenField ID="hfFARMERID" runat="server" Value='<%#Eval("FARMER_ID")%>' />
                                        <asp:HiddenField ID="hfDISTCODE" runat="server" Value='<%#Eval("DIST_CODE")%>' />
                                        <asp:LinkButton ID="lnkTransId" runat="server" Text='<%#Eval("TRANSACTION_ID")%>' OnClick="lnkTransId_Click" CausesValidation="False" ClientIDMode="Static"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Farmer Id" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_FARMERID" runat="server" Text='<%#Eval("FARMER_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="License No." HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_LICNO" runat="server" Text='<%#Eval("LICNO")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SALEDATE" runat="server" Text='<%#Eval("SALEDATE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="All In Cost" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SALE_AMOUNT" runat="server" Text='<%#Eval("ALLINCOST")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_USER_TYPE" runat="server" Text='<%#Eval("USER_TYPE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B2C629" />
                            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="Label1" runat="server" Text="No Records !!!"
                                        Font-Bold="true" ForeColor="red"></asp:Label></center>
                            </EmptyDataTemplate>
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTransIDFarm" runat="server" Visible="false" Text=""></asp:Label>
                        <asp:Label ID="lblDistCodeFarm" runat="server" Visible="false" Text=""></asp:Label>
                        <asp:Label ID="lblFarmID" runat="server" Visible="false" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                        <asp:Button ID="btnEditPopUp" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="popupEdit" runat="server" OnOkScript="onOk()" OnCancelScript="onCancel()"
                            TargetControlID="btnEditPopUp" PopupControlID="pnlEdit" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlEdit" runat="server" CssClass="modalPopup" Style="display: block">
                            <table width="100%">
                                <tr>
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3" style="font-weight: bold; color: blue;">FARMER ID</td>
                                    <td>:</td>
                                    <td colspan="2">
                                        <asp:Label ID="lblDistname" runat="server" Text="" Style="font-weight: bold; color: blue;"></asp:Label><asp:TextBox ID="txtFarmerId" runat="server" Width="150px" onkeypress="return blockNonNumbers(this, event, true, false);" MaxLength="9"></asp:TextBox>&nbsp;<asp:Button ID="btnGoSearch" runat="server" Text="Go" OnClick="btnGoSearch_Click" /></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3" style="font-weight: bold; color: blue;">Beneficiary Name</td>
                                    <td>:</td>
                                    <td colspan="2">
                                        <asp:Label ID="lblBeneficiaryName" runat="server" Text="" Style="font-weight: bold; color: blue;"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3" style="font-weight: bold; color: blue;">Village Name</td>
                                    <td>:</td>
                                    <td colspan="2">
                                        <asp:Label ID="lblVillgeName" runat="server" Text="" Style="font-weight: bold; color: blue;"></asp:Label></td>
                                </tr>
                                <caption>
                                    <br />
                                    <tr>
                                        <td colspan="6">
                                            <asp:GridView ID="gvDtls" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="TRANSACTION_ID" HeaderText="TRANSACTION ID" />
                                                    <asp:BoundField DataField="Crop_Name" HeaderText="CROP NAME" />
                                                    <asp:BoundField DataField="Variety_Name" HeaderText="VARIETY NAME" />
                                                    <asp:BoundField DataField="CROP_CLASS" HeaderText="CROP CLASS" />
                                                    <asp:BoundField DataField="BAG_SIZE_KG" HeaderText="BAG SIZE IN KG" />
                                                    <asp:BoundField DataField="NO_OF_BAGS" HeaderText="NO OF BAGS" />
                                                    <asp:BoundField DataField="TOT_QTL" HeaderText="TOT QTL" />
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            <asp:Button ID="btnUpdate" runat="server" OnClientClick="return CnfSubmit()" OnClick="btnUpdate_Click" Text="Update" Width="100px" Enabled="false" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" Width="100px" />
                                        </td>
                                    </tr>
                                </caption>
                            </table>

                        </asp:Panel>
                    </td>
                </tr>
            </table>
      </ContentTemplate>
    </asp:UpdatePanel>
   <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upStockOB"
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
    </asp:UpdateProgress>
</asp:Content>


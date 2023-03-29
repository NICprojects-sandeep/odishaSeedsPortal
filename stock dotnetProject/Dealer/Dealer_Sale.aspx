<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="Dealer_Sale.aspx.cs" Inherits="Dealer_Dealer_Sale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function getPrint(print_area) {
            var divToPrint = document.getElementById(print_area);
            var printWin = window.open('', 'Mail In', 'left=0,top=0,width=900,height=600,status=0');
            printWin.document.write("<div align='left' style='width:900px;margin:0 auto;padding-top:10px'>");
            printWin.document.write(divToPrint.outerHTML);
            printWin.document.write("</div>");
            printWin.document.close();
            printWin.focus();
            printWin.print();
        }
        

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

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=Grd_Sale.ClientID%>");
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
    <style type="text/css">
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 900px;
            height:650px;
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>            
    <div id="divview" runat="server" style="display: block;">
            <center>
                <%-- <asp:Panel ID="Panel1" runat="server" Height="500px" Width="90%" BorderColor="Bisque"
                    BorderStyle="Solid">--%>
                <asp:HiddenField ID="hdnField" runat="server" />
                <asp:HiddenField ID="hfRandom" runat="server" />
                <asp:HiddenField ID="hfUserType" runat="server" />
                <asp:HiddenField ID="hfFarmerId" runat="server" />
                <asp:HiddenField ID="hfMobNo" runat="server" />
                <table width="100%">
                    <tr>
                        <td align="center" bgcolor="#FFFFCC" style="font-weight: bold; color: #009900;" valign="middle">
                            <h3>
                                Seed Distribution Through Web&nbsp;Page
                                <asp:Label ID="lblSupplier" runat="server" Font-Bold="True" ForeColor="#0099FF" Visible="False"></asp:Label>
                            </h3>
                            <hr />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlHeader" runat="server" Width="90%" BorderColor="Bisque" BorderStyle="Solid">
                    <fieldset>
                        <table>
                            <tr>
                                <td align="left">
                                    Farmer ID
                                </td>
                                <td>:</td>
                                <td align="left"><asp:Label ID="lblPre" runat="server" ForeColor="Blue"></asp:Label>
                                    <asp:TextBox ID="txtFarmerId" runat="server" Width="75px" AutoPostBack="true" OnTextChanged="txtFarmerId_TextChanged"
                                        ForeColor="Blue"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Farmer Name
                                </td>
                                <td>:</td>
                                <td align="left" runat="server" id="td_FarmerName" class="style5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Father Name
                                </td>
                                <td>:</td>
                                <td align="left" runat="server" id="td_FatherName" class="style5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Category
                                </td>
                                <td>:</td>
                                <td align="left" runat="server" id="td_Category" class="style5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Gender
                                </td>
                                <td>:</td>
                                <td align="left" runat="server" id="td_Gender" class="style5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Mobile No
                                </td>
                                <td>:</td>
                                <td align="left" class="style5">
                                    <asp:Label ID="lblFarmerMobNo" runat="server"></asp:Label>
                                    &nbsp; <asp:Button ID="btnChangeMobNo" runat="server" Text="Change" Width="100px" OnClick="btnChangeMobNo_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left">
                                   <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" Width="100px" OnClick="btnSendOTP_Click" />
                                </td>
                                <td></td>
                                <td align="left"class="style5">
                                    <asp:TextBox runat="server" ID="txtCMobNo"></asp:TextBox> 
                                    <asp:Button ID="btnUpdateMobNo" runat="server" Text="Update" Width="100px" OnClick="btnUpdateMobNo_Click" />
                                    <asp:Button ID="btnUpdateMobNoCancel" runat="server" Text="Cancel" Width="100px" OnClick="btnUpdateMobNoCancel_Click" />
                                    <asp:Label ID="lblOTPMsg" runat="server" Text="OTP has been sent(Valid for 10min)" ForeColor="#006600"></asp:Label>
                                    <asp:TextBox ID="txtOTP" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnConfirmOTP" runat="server" Text="Submit" Width="100px" OnClick="btnConfirmOTP_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:GridView ID="gvStore" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="152%" 
                                        EmptyDataText="No Stock Received" EmptyDataRowStyle-ForeColor="Red" 
                                        ShowHeader="false">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSEED_LIC_APP_DIST_ID" runat="server" Text='Stok Received'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Crop_Name" HeaderText="Crop Name">
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TOT_QTL" HeaderText="Sold(Qtl)">
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <EmptyDataRowStyle ForeColor="Red" />
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
                        </table>
                        <asp:Literal ID="litFarmer" runat="server"></asp:Literal>
                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Width="100px" OnClick="btnConfirm_Click" Visible="false" />
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="pnlDetails" runat="server" Width="90%" BorderColor="Bisque" BorderStyle="Solid">
                    <fieldset>
                        <table cellspacing="5">
                        <tr>                                
                                <td align="left">
                                    Financial Year
                                </td>
                                <td>
                                <asp:DropDownList ID="ddlFinYr" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlFinYr_SelectedIndexChanged"></asp:DropDownList>                                    
                                </td>
                                <td align="left">
                                    Seasons
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSeassion" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlSeassion_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Crop Name
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCropName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCropName_SelectedIndexChanged"
                                        Width="100px" Font-Bold="False">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    Crop Variety
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVariety" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVariety_SelectedIndexChanged"
                                        Width="100px" Font-Bold="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="left">
                                    
                                </td>
                                <td>
                                    
                                </td>
                                <td>
                                    Class of Seed
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlClassOfSeed" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClassOfSeed_SelectedIndexChanged"
                                        Width="100px" Font-Bold="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="center" colspan="4">
                                    &nbsp;<asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="pnlContents" runat="server">
                            <div style="width: 100%; overflow: auto;">
                                <asp:GridView ID="Grd_Sale" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                    ForeColor="#333333" GridLines="None" Width="100%" 
                                    onrowdatabound="Grd_Sale_RowDataBound">
                                    <RowStyle BackColor="#E3EAEB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rbtn_lot" runat="server" AutoPostBack="True" CommandName="chk_lot" onclick="RadioCheck(this);"
                                                    OnCheckedChanged="rbtn_lot_CheckedChanged" />
                                                <asp:HiddenField ID="hfSTOCK_ID" runat="server" Value='<%#Eval("STOCK_ID")%>' />                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lot Number" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_LotNo" runat="server" Text='<%#Eval("LOT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Source Name">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lbl_SorceName" runat="server" Text='<%#Eval("Receive_Unitname") %>'></asp:Label>--%>
                                                <asp:Label ID="lbl_SorceName" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_SorceID" runat="server" Text='<%#Eval("RECEIVE_UNITCD") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bag Size(Kg)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_BagSize" runat="server" Text='<%#Eval("BAG_SIZE_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Avl Bags">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AvlQtyBank" runat="server" Text='<%#Eval("AVL_BAGS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Avl Qty(Qtl.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AvlQty" runat="server" Text='<%#Eval("AVL_QUANTITY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price / Qtl">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PricePerQtl" runat="server" Text='<%#Eval("All_in_cost_Price") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subsidy / Qtl">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SubsidyPerQtl" runat="server" Text='<%#Eval("TOT_SUBSIDY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>                                        
                                        
                                        <asp:TemplateField HeaderText="No. of Bags">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_NoOfBag" runat="server" autocomplete="off" Width="70px" CssClass="TextboxCss" 
                                                    BackColor="White" Enabled="False" onkeypress="return blockNonNumbers(this, event, true, false);"
                                                    MaxLength="6" OnTextChanged="txt_NoOfBag_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quintal(in qtl.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_QtyQtl" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Amt" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <FooterStyle HorizontalAlign="Center" Width="100px" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotPen" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btn_Add" runat="server" Font-Bold="True" BackColor="Black" ForeColor="White"
                                                    Text="Add" Enabled="False" CausesValidation="false" OnClick="btn_Add_Click">
                                                </asp:Button>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <EmptyDataTemplate>
                                        <div class="mandiatory" style="width: 850px; text-align: center;">
                                            No Record
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <PagerStyle HorizontalAlign="Center" CssClass="pagination" BackColor="#666666" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                                <br />
                                <asp:GridView ID="grd_lottrans" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" Width="100%" ShowFooter="True" OnRowDataBound="grd_lottrans_RowDataBound">
                                    <RowStyle BackColor="#E3EAEB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cat" runat="server" Text='<%#Eval("CropCatg_Name") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <FooterStyle HorizontalAlign="Center" Width="100px" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTot" runat="server" Text="Total : " />
                                            </FooterTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Crop Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Crop" runat="server" Text='<%#Eval("Crop_Name") %>'></asp:Label>
                                                <asp:Label ID="lbl_LotNo" runat="server" Text='<%#Eval("LOT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variety">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Variety" runat="server" Text='<%#Eval("Crop_VerName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Class">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Class" runat="server" Text='<%#Eval("CROP_CLASS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Source">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Source" runat="server" Text='<%#Eval("Receive_Unitname") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bag Size(Kg.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_BagSize" runat="server" Text='<%#Eval("BAG_SIZE_KG") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bag Receive">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_BagReceive" runat="server" Text='<%#Eval("NO_OF_BAGS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receive Qtl">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Qty1" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <FooterStyle HorizontalAlign="Center" Width="100px" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotQty" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Amt1" runat="server" Text='<%#Eval("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <FooterStyle HorizontalAlign="Center" Width="100px" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotAmt" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btn_Delete" runat="server" Font-Bold="True" BackColor="Red" ForeColor="White"
                                                    Text="Delete" Enabled="true" CausesValidation="false" OnClick="btn_Delete_Click">
                                                </asp:Button>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="pagination" BackColor="#666666" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <input id="inpHide" type="hidden" runat="server" />
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="100px" />
                        <asp:Label ID="lblStkId" runat="server" Text="" Visible="false"></asp:Label>
                    </fieldset>
                </asp:Panel>
                <%--Edit--%>
                <asp:Button ID="btnEditPopUp" runat="server" Style="display: none" />
                <%--<asp:ModalPopupExtender ID="popupEdit" runat="server" OnOkScript="onOk()" OnCancelScript="onCancel()"
                    TargetControlID="btnEditPopUp" PopupControlID="pnlEdit" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>--%>
                <asp:Panel ID="pnlEdit" runat="server" CssClass="modalPopup" Style="display: none">
                    <table cellspacing="5">
                        <tr>
                            <td align="left">
                                Category
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left">
                                Crop Name
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Crop Variety
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Class of Seed
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Bag Size(In Kg.)
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                No. of Bags
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtUNoofBags" runat="server" Text="" Width="50px" AutoPostBack="True"
                                    OnTextChanged="txtUNoofBags_TextChanged" />
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                    TargetControlID="txtUNoofBags" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Quantity(In Qtl.)
                            </td>
                            <td align="left" id="td_Uqty" runat="server">
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                    Width="100px" />
                                &nbsp;<asp:Button ID="btnUCancel" runat="server" OnClick="btnUCancel_Click" Text="Cancel"
                                    Width="100px" />
                                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <!-- REPORT  -->
                <asp:Button ID="btnReport" runat="server" Style="display: none" />
                <%--<asp:ModalPopupExtender ID="mpeReport" runat="server" OnOkScript="onOk()" OnCancelScript="onCancel()"
                    TargetControlID="btnReport" PopupControlID="pnlReport" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>--%>
                
                <asp:Panel ID="pnlReport" runat="server" CssClass="modalPopup" Style="display: none">
                <table width="100%">
                <tr>
                <td align="right" width="50%">
                <asp:Button ID="btnPrint" runat="server" Text="Print" 
                        OnClientClick="getPrint('divPrint')" Width="100px"/>
                </td>
                <td align="right" width="50%">
                <asp:Button ID="btnClose" runat="server" Text="Close" Width="100px" 
                        onclick="btnClose_Click" />
                </td>
                </tr>
                </table>
                <div id="divPrint" style="width:900px;border-spacing:0px;padding:0px;overflow:auto;
                        text-align: center; vertical-align: top;">
                <table border="1" cellpadding="0" cellspacing="0" width="700px">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="2" cellspacing="0" width="100%" style="font-size: 10pt; font-family: Verdana">
                                <tr>
                                    <td colspan="2" style="height: 34px" align="right">
                                        
                                    </td>
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
                                    <td align="left"><strong>&nbsp;Seed Delivered To </strong><br />
                                    </td>
                                    <td align="left" id="td_LicenceNo" runat="server">&nbsp;</td>
                                </tr>
                                <tr>
                                <td colspan="2" align="left">
                                <table style="margin-right: 4px">
                                <tr>
                                <td align="left" width="150">
                                    Name 
                                </td>
                                <td align="center" width="20">
                                    :</td>
                                <td id="td_Name" align="left" runat="server">
                                    
                                </td>
                                </tr>
                                <tr>
                                <td align="left" width="150">                                    
                                    Father's Name
                                </td>
                                <td align="center" width="20">                                    
                                    :</td>
                                <td id="td_FName" align="left" runat="server">
                                    
                                </td>
                                </tr>
                                <tr>
                                <td align="left" width="150">
                                    Mobile Number
                                </td>
                                <td align="center" width="20">
                                    :</td>
                                <td id="td_MobNo" align="left" runat="server">
                                    
                                </td>
                                </tr>
                                <tr>
                                <td align="left" width="150">
                                    Village
                                </td>
                                <td align="center" width="20">
                                    :</td>
                                <td id="td_Village" align="left" runat="server">
                                    
                                </td>
                                </tr>
                                <tr>
                                <td align="left" width="150">
                                    GP
                                </td>
                                <td align="center" width="20">
                                    :</td>
                                <td id="td_GP" align="left" runat="server">
                                    
                                </td>
                                </tr>
                                <tr>
                                <td align="left" width="150">
                                    Block
                                </td>
                                <td align="center" width="20">
                                    :</td>
                                <td id="td_Block" align="left" runat="server">
                                    
                                </td>
                                </tr>                                
                                <tr>
                                <td align="left" width="150">
                                    Dist
                                </td>
                                <td align="center" width="20">
                                    :</td>
                                <td id="td_Dist" align="left" runat="server">
                                    
                                </td>
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
                                            onrowdatabound="grd_stockdtls_RowDataBound">
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
                <asp:TemplateField HeaderText="Total Subsidy">
                <ItemTemplate>
                <asp:Label ID="lbl_Subsidy" runat="server" Text='<%#Eval("SUBSIDY_AMOUNT") %>'></asp:Label>
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
                                    <td colspan="2" align="right">
                                        <table width="50%">
                                            <tr>
                                                <td align="left" width="150">
                                                    Total Amount</td>
                                                <td align="center" width="25">
                                                    :</td>
                                                <td id="td_TotAmt" runat="server">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="150">
                                                    Subsidy Amount</td>
                                                <td align="center" width="25">
                                                    :</td>
                                                <td id="td_SubAmt" runat="server">
                                                    &nbsp;</td>
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
                                <td align="left">

                                    Signature of <strong>
                                            <asp:Label ID="lblReceiver" runat="server"></asp:Label></strong>

                                </td>
                                    <td align="right">
                                        <br />
                                        Signature of 
        <asp:Label ID="lbl_LicName0" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                <td align="left">

                                </td>
                                    <td align="right">
                                        (<asp:Label 
                                            ID="lbl_LicNo0" runat="server" Font-Bold="True"></asp:Label>
                                        )&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right" style="height: 20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </div>
                </asp:Panel>
            </center>
            </div>
            </ContentTemplate>
        <Triggers>
            <ajax:PostBackTrigger ControlID="txtFarmerId" />
            <ajax:PostBackTrigger ControlID="btnSendOTP" />
            <ajax:PostBackTrigger ControlID="btnConfirmOTP" />
            <ajax:PostBackTrigger ControlID="btnConfirm" />
        </Triggers>
        </asp:UpdatePanel>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="Stock_OBEntry.aspx.cs" Inherits="Masters_Stock_OBEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
    <script type="text/javascript" lang="javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=Grd_OBDtls.ClientID%>");
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
        function calquantity() {
            var chk = document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_chk_lot').checked;

            if (chk == true) {
                var bagsize = document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_ddl_BagSize');
                var bagsizevalue = bagsize.options[bagsize.selectedIndex].value;
                var bagrecv = document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_NoOfBag').value;
                if (bagsize.selectedIndex == 0) {
                    alert("Please select Bag size");
                    document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_NoOfBag').value = "";
                    document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_Quintal').value = "";


                    return false;
                }
                if (bagrecv == "") {
                    alert("Please Enter the No. of Receive bags ");
                    document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_Quintal').value = "";


                }
                else if (bagrecv == "0") {
                    alert("No. of bags cannot be zero");
                    document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_NoOfBag').value = "";
                    document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_Quintal').value = "";


                }
                else {
                    var total = parseInt(bagrecv) * parseInt(bagsizevalue);
                    var quantity = parseFloat(total) / 100;
                    if (isNaN(quantity)) {
                        document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_Quintal').value = "";

                    }
                    else {
                        document.getElementById('ctl00_ContentPlaceHolder1_Grd_OBDtls_ctl' + i + '_txt_Quintal').value = quantity;
                    }
                }
            }
        }

        function clear() {
            debugger;
        }
    </script>
    <asp:UpdatePanel ID="upStockOB" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="bedcromb">
                <asp:Label ID="lblTitle" runat="server" Text="Define Opening Balance"></asp:Label>

            </div>
            <div style="padding-top: 15px; padding-left: 30px;">
                <asp:Panel ID="Pnl2" runat="server">
                    <div style="width: 900px; background-color: #666; padding: 2px; margin: 0 auto;">
                        <div style="background-color: #FFF; padding: 10px;">
                            <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
                            <asp:HiddenField ID="hfOBDate" runat="server" Visible="false" />
                            <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />
                            <div style="background-color: #121212; color: #B2C629; width: 880px;">
                                <div>
                                    <center><strong>Opening Stock</strong></center>
                                </div>
                            </div>
                            <table id="tblMainSection" runat="server" style="width:100%;padding:3px;border-spacing:3px;">
                                <tr>
                                    <td style="width:140px;" class="normaltd"></td>
                                    <td style="width:140px;" class="normaltd"></td>
                                    <td style="width:5px;" class="normaltd"></td>
                                    <td><span class="mandatory"><strong>Note :- Enter Opening Balance as on Dt. 01/08/2015.</strong></span></td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">District</td>
                                    <td class="normaltd">:</td>
                                    <td class="normaltd">
                                        <asp:Label ID="lblDistName" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Date</td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:Label ID="lblOBDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Select Godown<span class="mandatory">*</span>
                                    </td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:RadioButtonList ID="Chk_Godown" runat="server" CssClass="normaltd" AutoPostBack="True" OnSelectedIndexChanged="Chk_Godown_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <table id="tblSearch" runat="server" style="width:100%;padding:3px;border-spacing:3px;" visible="false">
                                <tr>
                                    <td style="width:140px;" class="normaltd"></td>
                                    <td style="width:140px;" class="normaltd"></td>
                                    <td style="width:5px;" class="normaltd"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">District</td>
                                    <td class="normaltd">:</td>
                                    <td class="normaltd">
                                        <asp:Label ID="lblDistName1" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Date</td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:Label ID="lblDateOB" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Godown Name</td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:Label ID="lblGodownName" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="normaltd">&nbsp;</td>
                                    <td class="normaltd">Financial Year</td>
                                    <td class="normaltd">&nbsp;</td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:DropDownList ID="ddlFinYr" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlFinYr_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd">&nbsp;</td>
                                    <td class="normaltd">Seassion</td>
                                    <td class="normaltd">&nbsp;</td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:DropDownList ID="ddlSeassion" runat="server" CssClass="DropdownCss">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Crop Catagory<span class="mandatory">*</span></td>
                                    <td class="normaltd">: </td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:DropDownList ID="ddl_CropCatagory" runat="server" AutoPostBack="True" CssClass="DropdownCss" OnSelectedIndexChanged="ddl_CropCatagory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Crop Name<span class="mandatory">*</span></td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:DropDownList ID="ddl_CropName" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddl_CropName_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Class</td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" style="text-align:left;">
                                        <asp:RadioButtonList ID="rbtn_class" runat="server" CssClass="normaltd"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Foundation">Foundation</asp:ListItem>
                                            <asp:ListItem Value="Certified" Selected="True">Certified</asp:ListItem>
                                            <asp:ListItem Value="TL">TL</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd"></td>
                                    <td class="normaltd"></td>
                                    <td class="normaltd" style="text-align:left;"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">
                    <div style="width: 900px; background-color: #666; padding: 2px; margin: 0 auto;">
                        <div style="background-color: #FFF; padding: 10px;">
                            <table style="width:880px;padding:2px;border-spacing:1px;" border="0">
                                <tr>
                                    <td colspan="2">
                                        Type some letters of the variety
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtVariety" runat="server"></asp:TextBox>
                                        
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonCss" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;vertical-align:top;" colspan="6" >
                                        <asp:Panel ID="pnlContents" runat="server">
                                            <div style="width: 880px; overflow: auto;">
                                                <asp:GridView ID="Grd_OBDtls" runat="server" BackColor="#B2C629"
                                                    CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#999999"
                                                    AllowPaging="True" AutoGenerateColumns="False"
                                                    ForeColor="Black" OnPageIndexChanging="Grd_OBDtls_PageIndexChanging" OnRowDataBound="Grd_OBDtls_RowDataBound">
                                                    <RowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSl" runat="server" Text='<%# Container.DataItemIndex + 1 %> '></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rbtn_lot" runat="server" AutoPostBack="True" CommandName="chk_lot" onclick="RadioCheck(this);"
                                                                    OnCheckedChanged="rbtn_lot_CheckedChanged" />
                                                                <asp:HiddenField ID="hfCropVerid" runat="server" Value='<%#Eval("Variety_Code")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CropVerNm" runat="server" Text='<%#Eval("Variety_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Source">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddl_Source" runat="server" Width="150px" CssClass="DropdownCss" Enabled="False">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lot No.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_LotNo" runat="server"  Width="120px" BackColor="White" CssClass="TextboxCss"
                                                                    Enabled="False" AutoPostBack="True" OnTextChanged="txt_LotNo_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bag Size(in Kg.)">
                                                            <ItemTemplate>

                                                                <asp:DropDownList ID="ddl_BagSize" runat="server" CssClass="DropdownCss" Width="120px" OnSelectedIndexChanged="ddl_BagSize_SelectedIndexChanged"
                                                                    AutoPostBack="true" Enabled="False">
                                                                    <asp:ListItem>--- SELECT ---</asp:ListItem>
                                                                    <asp:ListItem Value="2">2Kg</asp:ListItem>
                                                                    <asp:ListItem Value="4">4Kg</asp:ListItem>
                                                                    <asp:ListItem Value="8">8Kg</asp:ListItem>
                                                                    <asp:ListItem Value="10">10Kg</asp:ListItem>
                                                                    <asp:ListItem Value="20">20Kg</asp:ListItem>
                                                                    <asp:ListItem Value="30">30Kg</asp:ListItem>
                                                                    <asp:ListItem Value="40">40Kg</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No. of Bags">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_NoOfBag" runat="server" Width="70px" CssClass="TextboxCss" BackColor="White" Enabled="False" onkeypress="return blockNonNumbers(this, event, true, false);" MaxLength="6"
                                                                    OnTextChanged="txt_NoOfBag_TextChanged" AutoPostBack="true"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quintal(in qtl.)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_Quintal" runat="server" BackColor="White" CssClass="TextboxCss" Width="70px" ReadOnly="true"></asp:TextBox>
                                                                <asp:HiddenField ID="hdn_txtQuintal" runat="server" Visible="False"></asp:HiddenField>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btn_Add" runat="server" Font-Bold="True" BackColor="Black" ForeColor="White"
                                                                    Text="Add" Enabled="False" CausesValidation="false" OnClick="btn_Add_Click"></asp:Button>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div class="mandiatory" style="width: 850px; text-align: center;">
                                                            No Record
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <FooterStyle BackColor="#B2C629"></FooterStyle>
                                                    <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="#B2C629"></HeaderStyle>
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr id="trSubmit" runat="server" visible="false">
                                    <td align="center" colspan="6">
                                        <asp:Button ID="btn_ExistCrop" runat="server" Font-Bold="True"
                                            Text="Exit Crop" OnClick="btn_ExistCrop_Click"></asp:Button>&nbsp;&nbsp;
                                        <asp:Button ID="btn_ExistGodown" runat="server" Font-Bold="True"
                                            Text="Exit Godown" OnClick="btn_ExistGodown_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddl_CropName"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upStockOB"
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
</asp:Content>


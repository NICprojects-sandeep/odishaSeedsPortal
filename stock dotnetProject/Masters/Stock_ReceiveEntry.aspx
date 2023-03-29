<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="Stock_ReceiveEntry.aspx.cs" Inherits="Masters_Stock_ReceiveEntry" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script language="javascript" type="text/javascript">
        function CnfSubmit() {

            if (confirm("You are going to submit all the record. Do you want to continue?")) {

                return true;
            }
            else {

                return false;
            }
        }
        function CnfCancel() {

            if (confirm("You are going to cancel all the record. Do you want to continue?")) {

                return true;
            }
            else {

                return false;
            }
        }
    </script>
    <style type="text/css">
        .MessageBoxPopupBackground {
            filter: Alpha(Opacity=80);
            -moz-opacity: 0.4;
            opacity: 0.4;
            width: 100%;
            height: 100%;
            background-color: Black;
            position: absolute;
            z-index: 500001;
            top: 0px;
            left: 0px;
        }
    </style>
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
        //function Confirm() {
        //    var confirm_value = document.createElement("INPUT");
        //    confirm_value.type = "hidden";
        //    confirm_value.name = "confirm_value";
        //    if (confirm("Do you want to save data?")) {
        //        confirm_value.value = "Yes";
        //    } else {
        //        confirm_value.value = "No";
        //    }
        //    document.forms[0].appendChild(confirm_value);
        //}
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=Grd_RecvDtls.ClientID%>");
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
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="bedcromb">
                <asp:Label ID="lblTitle" runat="server" Text="Define Stock Receive"></asp:Label>
            </div>
            <div id="divview" runat="server" style="display: block;">
            <div style="padding-top: 15px; padding-left: 30px;">
                <asp:Panel ID="Pnl2" runat="server">
                    <div style="width: 1100px; background-color: #666; padding: 2px; margin: 0 auto;">
                        <div style="background-color: #FFF; padding: 10px;">
                            <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
                            <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />
                            <div style="background-color: #121212; color: #B2C629; width: 100%;">
                                <div>
                                    <center><strong>Receipt Information</strong></center>
                                </div>
                            </div>
                            <table id="tblMainSection" runat="server" width="100%" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td width="140" class="normaltd"></td>
                                    <td width="140" class="normaltd"></td>
                                    <td width="5" class="normaltd"></td>
                                    <td><span class="mandatory"></span></td>
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
                                    <td class="normaltd" valign="top">Select Godown<span class="mandatory">*</span>
                                    </td>
                                    <td class="normaltd" valign="top">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:RadioButtonList ID="Chk_Godown" runat="server" class="normaltd" AutoPostBack="True" OnSelectedIndexChanged="Chk_Godown_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <table id="tblSearch" runat="server" width="100%" cellpadding="3" cellspacing="3" visible="false">
                                <tr>
                                    <td width="10" class="normaltd"></td>
                                    <td width="180" class="normaltd"></td>
                                    <td width="5" class="normaltd"></td>
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
                                    <td class="normaltd">Godown Name</td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:Label ID="lblGodownName" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="normaltd">&nbsp;</td>
                                    <td class="normaltd">Financial Year  <span class="mandatory">*</span></td>
                                    <td class="normaltd">:</td>
                                    <td align="left" class="normaltd">
                                    <asp:DropDownList ID="ddlFinYr" runat="server" CssClass="DropdownCss" Enabled="false"></asp:DropDownList>
                                        <%--<asp:DropDownList ID="ddlFinYr" runat="server" CssClass="DropdownCss" Enabled="false">
                                            <asp:ListItem Value="0">--SELECT--</asp:ListItem>                              
                                            <asp:ListItem Value="2016-17" Selected="True">2016-17</asp:ListItem>
                                             <asp:ListItem Value="2017-18" Selected="True">2017-18</asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd">&nbsp;</td>
                                    <td class="normaltd">Seassion  <span class="mandatory">*</span></td>
                                    <td class="normaltd">:</td>
                                    <td class="normaltd" align="left">
                                    <asp:DropDownList ID="ddlSeassion" runat="server" CssClass="DropdownCss" Enabled="false"></asp:DropDownList>
                                        <%--<asp:DropDownList ID="ddlSeassion" runat="server" CssClass="DropdownCss" Enabled="false">
                                            <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                            <asp:listitem value="K" selected="true">Kharif</asp:listitem>
                                           <asp:ListItem Value="R" Selected="True">RABI</asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                </tr>
                                <tr id="trRecvedFromOAIC" runat="server">
                                    <td class="normaltd"></td>
                                    <td class="normaltd" style="vertical-align: top;">Received From</td>
                                    <td class="normaltd" style="vertical-align: top;">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:RadioButtonList ID="rbtn_RecvedFrom" runat="server" RepeatDirection="Horizontal" CssClass="normaltd" AutoPostBack="True" OnSelectedIndexChanged="rbtn_RecvedFrom_SelectedIndexChanged" RepeatColumns="4">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr id="trScheme" runat="server">
                                    <td class="normaltd"></td>
                                    <td class="normaltd">
                                        <asp:Label ID="Label1" runat="server" Text="Scheme"></asp:Label>
                                        <span class="mandatory">*</span></td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:DropDownList ID="ddl_Scheme" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddl_Scheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                            <asp:ListItem Value="BGREI">BGREI</asp:ListItem>
                                            <asp:ListItem Value="ICRISAT">ICRISAT</asp:ListItem>
                                            <asp:ListItem Value="NFSM">NFSM</asp:ListItem>
                                            <asp:ListItem Value="NMOOP-DAandFP">NMOOP-DAandFP</asp:ListItem>
                                            <asp:ListItem Value="NSC">NSC</asp:ListItem>
                                            <asp:ListItem Value="RKVY">RKVY</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr id="trReceivedType" runat="server">
                                    <td class="normaltd"></td>
                                    <td class="normaltd">
                                        <asp:Label ID="lblRecvText" runat="server" Text=""></asp:Label>
                                        <span class="mandatory">*</span></td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:DropDownList ID="ddl_RecvFrom" runat="server" CssClass="DropdownCss"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddl_RecvFrom_SelectedIndexChanged">
                                        </asp:DropDownList>&nbsp;&nbsp;
                                        <asp:Label ID="lblGrowerVareity" runat="server" Font-Bold="True" ForeColor="#A6B639" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trWayBill" runat="server">
                                    <td class="normaltd"></td>
                                    <td class="normaltd">
                                        <asp:Label ID="lblWayBill" runat="server" Text=""></asp:Label>
                                        <span class="mandatory">*</span></td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:TextBox ID="txtWayBillNo" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Date
                                        <span class="mandatory">*</span></td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:TextBox ID="txt_Date" runat="server" CssClass="TextboxCss" AutoPostBack="True" OnTextChanged="txt_Date_TextChanged" ReadOnly="false"></asp:TextBox>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter Date"
                                            ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                            EnableClientScript="False" ControlToValidate="txt_Date" SetFocusOnError="True"></asp:RegularExpressionValidator>

                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Date"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>

                                        <%--   <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_Date"
                                            MaskType="Date" Mask="99/99/9999">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                </tr>
                                <tr id="trChallan" runat="server">
                                    <td class="normaltd"></td>
                                    <td class="normaltd">
                                        <asp:Label ID="lblChallan" runat="server" Text="Challan No."></asp:Label>
                                        <span class="mandatory">*</span></td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:TextBox ID="txtChallanNo" runat="server" CssClass="TextboxCss" AutoPostBack="True" OnTextChanged="txtChallanNo_TextChanged"></asp:TextBox>
                                        <asp:DropDownList ID="ddl_ChallanNo" runat="server" CssClass="DropdownCss">
                                        </asp:DropDownList>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="*" ValidationExpression="^[a-zA-Z0-9]*$" EnableClientScript="False"
                                            ControlToValidate="txtChallanNo" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                            ErrorMessage="*" ValidationExpression="^[a-zA-Z0-9 :/!#%&'&quot;*()?+-]*$" EnableClientScript="False"
                                            ControlToValidate="ddl_ChallanNo" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lbl_challan" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd"></td>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btn_Proceed" Font-Bold="True" runat="server" Width="100px" Text="Proceed" OnClick="btn_Proceed_Click" CausesValidation="true" />
                                        &nbsp; &nbsp; 
                                        <asp:Button ID="btn_Cancel" runat="server" Font-Bold="True" Width="100px"
                                            Text="Cancel" OnClientClick="return CnfCancel()" OnClick="btn_Cancel_Click"></asp:Button>
                                        &nbsp; &nbsp;
                                        <asp:Button ID="btn_ExistGodown" runat="server" Font-Bold="True" Visible="false"
                                            Text="Exit Godown" OnClick="btn_ExistGodown_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <table id="tblCropInf" runat="server" width="100%" cellpadding="3" cellspacing="3" visible="false">
                                <tr>
                                    <td width="10" class="normaltd"></td>
                                    <td width="180" class="normaltd"></td>
                                    <td width="5" class="normaltd"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Crop Catagory<span class="mandatory">*</span></td>
                                    <td class="normaltd">: </td>
                                    <td align="left" class="normaltd">
                                        <asp:DropDownList ID="ddl_CropCatagory" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddl_CropCatagory_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Crop Name<span class="mandatory">*</span></td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:DropDownList ID="ddl_CropName" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddl_CropName_SelectedIndexChanged1">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Class</td>
                                    <td class="normaltd">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:RadioButtonList ID="rbtn_class" runat="server" class="normaltd"
                                            RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtn_class_SelectedIndexChanged">
                                            <%--<asp:ListItem Value="Foundation">Foundation</asp:ListItem>--%>
                                            <asp:ListItem Value="Certified" Selected="True">Certified</asp:ListItem>
                                            <asp:ListItem Value="Foundation">Foundation</asp:ListItem>
                                            <asp:ListItem Value="TL">TL</asp:ListItem>
                                            <asp:ListItem Value="Breeder">Breeder</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">
                    <div style="width: 1100px; background-color: #666; padding: 2px; margin: 0 auto;">
                        <div style="background-color: #FFF; padding: 10px;">
                            <table width="98%" border="0" cellspacing="1" cellpadding="2">
                                <tr>
                                    <td style="text-align: left;" valign="top" colspan="6">
                                        <asp:Panel ID="pnlContents" runat="server">
                                            <div style="width: 98%; overflow: auto;">
                                                <asp:GridView ID="Grd_RecvDtls" runat="server" BackColor="#B2C629"
                                                    CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#999999"
                                                    AllowPaging="True" AutoGenerateColumns="False"
                                                    ForeColor="Black" OnPageIndexChanging="Grd_RecvDtls_PageIndexChanging" OnRowDataBound="Grd_RecvDtls_RowDataBound">
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

                                                                <%--<asp:HiddenField ID="hfRefNo" runat="server" Value='<%#Eval("Ref_NO")%>' />--%>
                                                                <asp:HiddenField ID="hfCropVerid" runat="server" Value='<%#Eval("Variety_Code")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CropVerNm" runat="server" Text='<%#Eval("Variety_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lot No.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_LotNo" runat="server" autocomplete="off" Width="200px" BackColor="White" CssClass="TextboxCss"
                                                                    Enabled="False" AutoPostBack="True" OnTextChanged="txt_LotNo_TextChanged"></asp:TextBox>
                                                                <asp:DropDownList ID="ddl_LotNo" runat="server" Width="200px" CssClass="DropdownCss" Enabled="False" 
                                                                    OnSelectedIndexChanged="ddl_LotNo_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>


                           <asp:HiddenField ID="hfTestingdate" runat="server" />

                          <asp:HiddenField ID="hfExpiredate" runat="server" />

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bag Size <br>(in Kg.)">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddl_BagSize" runat="server" CssClass="DropdownCss" Width="80px" OnSelectedIndexChanged="ddl_BagSize_SelectedIndexChanged"
                                                                    AutoPostBack="true" Enabled="False" CausesValidation="false">
                                                                    <%--<asp:ListItem>-Select-</asp:ListItem>
                                                                    <asp:ListItem Value="2">2Kg</asp:ListItem>
                                                                    <asp:ListItem Value="4">4Kg</asp:ListItem>
                                                                    <asp:ListItem Value="8">8Kg</asp:ListItem>
                                                                    <asp:ListItem Value="10">10Kg</asp:ListItem>
                                                                    <asp:ListItem Value="20">20Kg</asp:ListItem>
                                                                    <asp:ListItem Value="25">25Kg</asp:ListItem>
                                                                    <asp:ListItem Value="30">30Kg</asp:ListItem>
                                                                    <asp:ListItem Value="40">40Kg</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>



                                                        <%-- <asp:TemplateField HeaderText="Issue Bags">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_IssueBag" runat="server"  Width="70px" CssClass="TextboxCss" BackColor="White" ReadOnly="true" Enabled="False" 
                                                                   ></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>--%>

                                                         <asp:TemplateField HeaderText="Ossopca Issue Bags">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_IssueBag" runat="server"></asp:Label>

                                                                 <asp:TextBox ID="txt_testingDate" runat="server" CssClass="TextboxCss" AutoPostBack="True" ReadOnly="false" Visible="false" Width="100px" OnTextChanged="txt_testingDate_TextChanged" onkeypress="return blockNonNumbers(this, event, true, false);" ></asp:TextBox>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ErrorMessage="Enter Date"
                                            ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                            EnableClientScript="False" ControlToValidate="txt_testingDate" SetFocusOnError="True"></asp:RegularExpressionValidator>

                                        <cc1:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txt_testingDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Avl.No.of Bags">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_AvlBag" runat="server" ></asp:Label>
                                                                 <asp:HiddenField ID="hfavlbag" runat="server"  />

                                                                 <asp:Label ID="lbl_ExpiryDate" runat="server" ReadOnly="true" Visible="false" Width="100px"></asp:Label>
                                                                <asp:HiddenField ID="hfTestDate" runat="server"  />
                                                                <asp:HiddenField ID="hfExpDate" runat="server"  />

                                   

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>




                                                        <asp:TemplateField HeaderText="Recv.No.of Bags">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_NoOfBag" runat="server" autocomplete="off" Width="70px" CssClass="TextboxCss" BackColor="White" Enabled="False" onkeypress="return blockNonNumbers(this, event, true, false);" MaxLength="6"
                                                                    OnTextChanged="txt_NoOfBag_TextChanged" AutoPostBack="true"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity (in qtl.)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_Quintal" runat="server" autocomplete="off" BackColor="White" CssClass="TextboxCss" Width="100px" ReadOnly="true"></asp:TextBox>
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

                                                <asp:GridView ID="grd_lottrans" runat="server" BackColor="#B2C629" AutoGenerateColumns="False"
                                                    AllowPaging="True" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="4" ForeColor="Black">
                                                    <RowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

<%--                                                          <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rdobtn_lot" runat="server" AutoPostBack="True" OnCheckedChanged="rdobtn_lot_CheckedChanged"  />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Catagory">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_catgorynmt" runat="server" Text='<%#Eval("Category_Name") %>'></asp:Label>
                                                                <asp:Label ID="lbl_catgorycd" runat="server" Text='<%#Eval("CROPCATG_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_cropnmt" runat="server" Text='<%#Eval("Crop_Name") %>'></asp:Label>
                                                                <asp:Label ID="lbl_cropcd" runat="server" Text='<%#Eval("CROP_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_varietynmt" runat="server" Text='<%#Eval("Variety_Name") %>'></asp:Label>
                                                                <asp:Label ID="lbl_varietytcdt" runat="server" Text='<%#Eval("CROP_VERID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Eval("CLASS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lot No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_lotnmt" runat="server" Text='<%#Eval("LOT_NUMBER") %>'></asp:Label>
                                                                <asp:Label ID="lbl_RefNo" runat="server" Text='<%#Eval("CROP_VERID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bag Size(in Kg.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_bagsizet" runat="server" Text='<%#Eval("BAG_SIZE_KG") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bag Receive">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_bagrecvet" runat="server" Text='<%#Eval("SALE_NO_OF_BAG") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Receive Quantity(in qtl.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_recvqtlt" runat="server" Text='<%#Eval("Quantity_sale") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>







                                                    </Columns>
                                                    <FooterStyle BackColor="#B2C629" />
                                                    <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="#B2C629" />
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr id="trTempDetails" runat="server" visible="false">
                                    <td colspan="3">
                                        <asp:GridView ID="grdTempDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                            CssClass="largetb">
                                            <Columns>
                                                <%-- <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <input type="checkbox" name="Checkb" value='<%# Eval("SlNo") %>' />
                                                        <asp:HiddenField ID="hfSlno" runat="server" Value='<%#Eval("SlNo") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7px" />
                                                    <HeaderTemplate>
                                                        <input type="checkbox" value="ON" name="toggleAll" onclick='ToggleAll(this)' value="ON" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="7px" />
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" HeaderText="Action" ItemStyle-Width="30px"
                                                    HeaderStyle-Width="30px" />--%>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSl" runat="server" Text='<%# Container.DataItemIndex + 1 %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Variety">
                                                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("VARIETY_NAME")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot No.">
                                                    <ItemTemplate>
                                                        <%#Eval("LOT_NO")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bag Size</br>(in Kg.)">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("BAG_SIZE")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="gridtxt" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of Bags">
                                                    <ItemTemplate>
                                                        <%#Eval("NO_OF_BAGS")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="gridtxt" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity (in qtl.)">
                                                    <ItemTemplate>
                                                        <%#Eval("QUANTITY")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="gridtxt" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btn_Delete" runat="server" Font-Bold="True" BackColor="Black" ForeColor="White"
                                                            Text="Delete" CausesValidation="false" OnClick="btn_Delete_Click"></asp:Button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#EFEFEF" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="#FDFDFD" />
                                            <HeaderStyle BackColor="#A6B639" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                No Record
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="trSubmit" runat="server" visible="false">
                                    <td align="center" colspan="3">
                                        <asp:Button ID="btn_Submit" runat="server" Font-Bold="True" Width="100px"
                                            Text="Submit" OnClientClick="return CnfSubmit()" OnClick="btn_Submit_Click"></asp:Button>&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel1" runat="server" Font-Bold="True" Width="100px"
                                            Text="Cancel" OnClientClick="return CnfCancel()" OnClick="btn_Cancel1_Click"></asp:Button>

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
            </div>
            <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="updatepanel"
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
        </ContentTemplate>
        <%--<Triggers>            
            <asp:PostBackTrigger ControlID="btnCancel"></asp:PostBackTrigger>
        </Triggers>--%>
    </ajax:UpdatePanel>

    <%-- <asp:Button runat="server" ID="btnMessagePopupTargetButton" Style="display: none;" />
    <cc1:ModalPopupExtender ID="mpeMessagePopup" runat="server" PopupControlID="pnlMessageBox"
        TargetControlID="btnMessagePopupTargetButton" OkControlID="btnOk"
        BackgroundCssClass="MessageBoxPopupBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" ID="pnlMessageBox" BackColor="White" Width="420" BorderWidth="2" BorderStyle="Solid" BorderColor="#780606" Style="display: none">
        <div style="max-height: 500px; width: 420px; overflow: hidden;">
            <div style="float: left; width: 380px; margin: 20px;">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left; vertical-align: top;">
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                &nbsp;
                            </p>
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                Are you sure to receive the stock ?
                            </p>
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                &nbsp;
                            </p>
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                Once you received the stock can not add the stock to the&nbsp; Opening Stock
                            </p>
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                &nbsp;
                            </p>
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                Press OK to receive the stock
                            </p>
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                Press Cancel to add the Opening Stock
                            </p>
                            <p style="margin: 0px; padding: 0px; color: #5F0202;">
                                &nbsp;
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; vertical-align: top;">
                            <asp:Button ID="btnOk" runat="server" Text="OK" Width="100px" OnClick="btnOk_Click"></asp:Button>&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" OnClick="btnCancel_Click"></asp:Button>


                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>--%>
</asp:Content>


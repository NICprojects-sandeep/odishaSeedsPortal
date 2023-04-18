<%@ Page Title="Stock Sale To Dealer" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="Stock_SaleEntry.aspx.cs" Inherits="Masters_Stock_SaleEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
<link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css"/>
  <script src="../js/jquery.min.js" ></script>
  <script src="../css/bootstrap/js/bootstrap.min.js" ></script>
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
        .auto-style3
        {
            color: #000000;
            font-size: 12px;
            font-weight: normal;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            width: 211px;
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

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=grd_lotdtls.ClientID%>");
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
            alert('1');
            var limit = 2 + parseInt(document.getElementById('ctl00_ContentPlaceHolder2_HiddenField1').value); //no of rows of grid
            alert('2');
            for (i = 2; i < limit; i++) {
                if (i < 10) {
                    i = "0" + i;
                }
                var chk = document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_rbtn_lot').checked;
                if (chk == true) {
                    var bagsize = document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_lbl_bagsize').innerHTML;
                    var bagstock = document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_lbl_bagno').innerHTML;
                    var bag = document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_bagsold').value;
                    if (bag == "") {
                        alert("Please Enter the No. of bags");
                        document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_quantitysold').value = "";
                    }
                    else if (bag == "0") {
                        alert("No. of bags sold cannot be zero");
                        document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_bagsold').value = "";
                        document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_quantitysold').value = "";
                    }
                    else if (parseInt(bagstock) < parseInt(bag)) {
                        alert("You can not Sale more than the Remaining Stock!!!");
                        document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_bagsold').value = "";
                        document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_quantitysold').value = "";
                    }
                    else {
                        var total = parseInt(bag) * parseInt(bagsize);
                        var quantity = parseFloat(total) / 100;
                        if (isNaN(quantity)) {
                            document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_quantitysold').value = "";
                        }
                        else {
                            document.getElementById('ctl00_ContentPlaceHolder2_grd_lotdtls_ctl' + i + '_txt_quantitysold').value = quantity;
                        }
                    }
                }
            }
        }
    </script>
    <asp:UpdatePanel ID="upStockSALE" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="bedcromb">
                <asp:Label ID="lblTitle" runat="server" Text="Stock Distribution"></asp:Label>
            </div>
            <div id="divview" runat="server" style="display: block;">
                <div style="padding-top: 15px; padding-left: 30px;">
                    <asp:Label ID="lblHiddenText" runat="server" Visible="false"></asp:Label>
                    <asp:Panel ID="Pnl2" runat="server">
                        <div style="width: 1000px; background-color: #666; padding: 2px; margin: 0 auto;">
                            <div style="background-color: #FFF; padding: 10px;">
                                <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
                                <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />
                                <asp:HiddenField ID="hfRandom" runat="server" />
                                <div style="background-color: #121212; color: #B2C629; width: 880px;">
                                    <div>
                                        <center>
                                            <strong>Stock Distribution</strong></center>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlMainSection" runat="server">
                                    <table id="tblMainSection" runat="server" style="width: 100%; padding: 3px; border-spacing: 3px;">
                                        <tr>
                                            <td style="width: 10px" class="normaltd"></td>
                                            <td style="width: 140px" class="normaltd">Stock Supplied To<span class="mandatory">*</span>
                                            </td>
                                            <td style="width: 5px" class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:RadioButtonList ID="rbtn_stocksupplied" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtn_stocksupplied_SelectedIndexChanged"
                                                    CssClass="normaltd" RepeatDirection="Horizontal">
                                                    <%--<asp:ListItem Value="01">Dealer</asp:ListItem>
                                                <asp:ListItem Value="02">Departmental</asp:ListItem>
                                                <asp:ListItem Value="03">Godown</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="tr_CreditBillNo" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Credit Bill No.<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:TextBox ID="txt_Deptcreditno" runat="server" Width="120px" CssClass="TextboxCss"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="txt_Deptcreditno" EnableClientScript="False" ValidationExpression="^[a-zA-Z0-9 :/!#%&'&quot;*()?+-]*$"
                                                    ErrorMessage="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr id="tr_Date" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Date<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:TextBox ID="txt_Deptdate" runat="server" Width="120px" CssClass="TextboxCss"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="txt_Deptdate" EnableClientScript="False" ValidationExpression="^(0[1-9]|1[012])[-/.](0[1-9]|[12][0-9]|3[01])[-/.](19|20)\d\d$"
                                                    ValidationGroup="check"
                                                    ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_Deptdate"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr id="tr_Select" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Select<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:RadioButtonList ID="rbtn_depttype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtn_depttype_SelectedIndexChanged"
                                                    CssClass="normaltd" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="01">DDA</asp:ListItem>
                                                    <asp:ListItem Value="02">Others</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <%--<tr id="tr_SelectGoDown" runat="server">
                                        <td class="normaltd">
                                        </td>
                                        <td class="normaltd">
                                            Select GoDown<span class="mandatory">*</span>
                                        </td>
                                        <td class="normaltd">
                                            :
                                        </td>
                                        <td class="normaltd" style="text-align:left;">
                                            <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged"
                                                CssClass="DropdownCss" Width="260px">
                                            </asp:DropDownList>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" SetFocusOnError="True"
                                                ControlToValidate="ddl_godown" EnableClientScript="False" ValidationExpression="^[a-zA-Z0-9]*$"
                                                ErrorMessage="*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>--%>
                                        <tr id="tr_Deliverydate" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Delivery&nbsp;date<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:TextBox ID="txt_saledt" runat="server" AutoPostBack="True" Width="120px" OnTextChanged="txt_saledt_TextChanged"
                                                    CssClass="TextboxCss"></asp:TextBox>
                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" SetFocusOnError="True"
                                                ControlToValidate="txt_saledt" EnableClientScript="true" ValidationExpression="(((0|1)[1-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>--%>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_saledt"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr id="tr_SaleTo" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Sale To<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddl_SellTo" runat="server" CssClass="DropdownCss" Width="260px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddl_SellTo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txt_saleto" runat="server" Width="120px" CssClass="TextboxCss" AutoPostBack="True"
                                                    OnTextChanged="txt_saleto_TextChanged"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="txt_saleto" EnableClientScript="False" ValidationExpression="^[a-zA-Z0-9]*$"
                                                    ErrorMessage="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr id="tr_StockDeliveredTo" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Stock Delivered&nbsp;To<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged"
                                                                                                                CssClass="DropdownCss" Width="260px">
                                                                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddl_StockDeliveredTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_StockDeliveredTo_SelectedIndexChanged"
                                                                                                                CssClass="DropdownCss" Width="260px">
                                                                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDemonName" runat="server" Width="350px" CssClass="TextboxCss" Visible="false"></asp:TextBox>
                                                        <asp:RadioButtonList ID="rbtnlPacsRebate" runat="server" CssClass="normaltd" RepeatDirection="Horizontal">
                                                                                                        </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </td>
                                        </tr>
                                        <tr id="tr_Scheme" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="auto-style3">Sheme Name<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddl_Scheme" runat="server" CssClass="DropdownCss" Width="260px"></asp:DropDownList>                                                
                                            </td>
                                        </tr>
                                        <tr id="tr_DDNumber" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="auto-style3"><asp:DropDownList ID="ddlCollectNo" runat="server" CssClass="DropdownCss">
                                            <asp:ListItem Value="DU">SBI Collect DU No</asp:ListItem>
                                            <asp:ListItem Value="UT">SBI Collect UTR No</asp:ListItem>
                                        </asp:DropDownList><span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:<%--onkeypress="return blockNonNumbers(this, event, true, false);"--%></td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:TextBox ID="txt_ddnum" runat="server" Width="120px" MaxLength="50" CssClass="TextboxCss" AutoPostBack="True"
                                                    OnTextChanged="txt_ddnum_TextChanged"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                    SetFocusOnError="True" ControlToValidate="txt_ddnum" EnableClientScript="False"
                                                    ValidationExpression="^[a-zA-Z0-9 :/!#%&'&quot;*()?+-]*$" ErrorMessage="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr id="tr_CashMemoNo" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Cash Memo&nbsp;No.<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:TextBox ID="txt_CashMemoNo" runat="server" AutoPostBack="True" Width="120px"
                                                    OnTextChanged="txt_CashMemoNo_TextChanged" CssClass="TextboxCss"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*"
                                                    ValidationExpression="^[a-zA-Z0-9]*$" EnableClientScript="False" ControlToValidate="txt_CashMemoNo"
                                                    SetFocusOnError="True"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr id="tr_Amount" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Amount(in Rs.)<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:TextBox ID="txt_amount" runat="server" Width="120px" CssClass="TextboxCss"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="txt_amount" EnableClientScript="False" ValidationExpression="^?\d*\.{0,1}\d+$"
                                                    ErrorMessage="Enter Proper Amount"></asp:RegularExpressionValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_amount"
                                                    FilterType="Custom,Numbers">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Literal ID="litBooking" runat="server"></asp:Literal>
                                    <table id="Table1" runat="server" width="100%" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td style="width: 10px;" class="normaltd"></td>
                                            <td style="width: 140px;" class="normaltd"></td>
                                            <td style="width: 5px;" class="normaltd">&nbsp;
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Proceed" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDelivery" runat="server">
                                    <table id="tblDelivery" runat="server" width="100%" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td style="width: 10px;" class="normaltd"></td>
                                            <td style="width: 140px;" class="normaltd">
                                                <asp:Label ID="lbl_refno" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 5px;" class="normaltd">&nbsp;
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:Label ID="lbl_dist" runat="server" Visible="False"></asp:Label>
                                                &nbsp;<asp:HiddenField ID="HiddenField1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normaltd">&nbsp;</td>
                                            <td class="normaltd">Financial Year</td>
                                            <td class="normaltd">:</td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddlFinYr" runat="server" AutoPostBack="True" CssClass="DropdownCss" OnSelectedIndexChanged="ddlFinYr_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normaltd">&nbsp;</td>
                                            <td class="normaltd">Seassion</td>
                                            <td class="normaltd">:</td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddlSeassion" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlSeassion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="tr_SelectGoDown" runat="server">
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Select GoDown<span class="mandatory">*</span>
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged"
                                                    CssClass="DropdownCss" Width="260px">
                                                </asp:DropDownList>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="ddl_godown" EnableClientScript="False" ValidationExpression="^[a-zA-Z0-9]*$"
                                                    ErrorMessage="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Crop&nbsp;Catagory </td>
                                            <td class="normaltd">: </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddl_cropCatagory" runat="server" AutoPostBack="True" CssClass="DropdownCss" OnSelectedIndexChanged="ddl_cropCatagory_SelectedIndexChanged" Width="125px">
                                                </asp:DropDownList>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="ddl_cropCatagory" EnableClientScript="False" ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9]*$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Crop&nbsp;Name
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddl_crop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_crop_SelectedIndexChanged"
                                                    CssClass="DropdownCss" Width="125px">
                                                </asp:DropDownList>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                    SetFocusOnError="True" ControlToValidate="ddl_crop" EnableClientScript="False"
                                                    ValidationExpression="^[a-zA-Z0-9]*$" ErrorMessage="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normaltd"></td>
                                            <td class="normaltd">Variety
                                            </td>
                                            <td class="normaltd">:
                                            </td>
                                            <td class="normaltd" style="text-align: left;">
                                                <asp:DropDownList ID="ddl_cropvariety" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_cropvariety_SelectedIndexChanged"
                                                    CssClass="DropdownCss" Width="125px">
                                                </asp:DropDownList>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                    SetFocusOnError="True" ControlToValidate="ddl_cropvariety" EnableClientScript="False"
                                                    ValidationExpression="^(([a-zA-Z0-9]{1,}?)(\-{0,}?)([a-zA-Z0-9]{1,}?))+$" ErrorMessage="*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                        <td class="normaltd">
                                            &nbsp;
                                        </td>
                                        <td class="normaltd">
                                            Class
                                        </td>
                                        <td class="normaltd">
                                            :
                                        </td>
                                        <td style="text-align:left;" class="normaltd">
                                            <asp:RadioButtonList ID="rbtn_class" runat="server" class="normaltd" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rbtn_class_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="Foundation">Foundation</asp:ListItem>
                                                <asp:ListItem Value="Certified" Selected="True">Certified</asp:ListItem>
                                                <asp:ListItem Value="TL">TL</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>--%>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel1" runat="server">
                        <div style="width: 1000px; background-color: #666; padding: 2px; margin: 0 auto;">
                            <div style="background-color: #FFF; padding: 10px;">
                                <table style="width: 1000px; padding: 2px; border-spacing: 1px;" border="0">
                                    <tr>
                                        <td style="text-align: left; vertical-align: top;" colspan="6">
                                            <asp:Panel ID="pnlContents" runat="server">
                                                <div style="width: 950px; overflow: auto;">
                                                    <asp:GridView ID="grd_lotdtls" runat="server" BackColor="#B2C629"
                                                        AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="4" Width="100%" ForeColor="Black" OnRowDataBound="grd_lotdtls_RowDataBound">
                                                        <RowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:RadioButton ID="rbtn_lot" runat="server" AutoPostBack="True" OnCheckedChanged="rbtn_lot_CheckedChanged" onclick="RadioCheck(this);" />
                                                                    <%--<asp:CheckBox ID="chk_lot" runat="server" AutoPostBack="True" OnCheckedChanged="chk_lot_CheckedChanged"
                                                                                    CommandName="chk_lot"></asp:CheckBox>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Source">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_source" runat="server" Text='<%#Eval("Receive_Unitname")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_sourceid" runat="server" Text='<%#Eval("RECEIVE_UNITCD")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblRefNo" runat="server" Text='<%#Eval("REF_NO")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lot Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_lotno" runat="server" Text='<%#Eval("LOT_NO")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bag Size(in Kg.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_bagsize" runat="server" Text='<%#Eval("BAG_SIZE_IN_KG")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Balance Bag">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_bagno" runat="server" Text='<%#Eval("RECV_NO_OF_BAGS")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Balance Quantity(in Qtl.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_quantity" runat="server" Text='<%#Eval("AVL_QUANTITY")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No. of Bag(Sold)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_bagsold" OnTextChanged="txt_bagsold_TextChanged" runat="server" BackColor="White" AutoPostBack="true"
                                                                        Width="100px" Enabled="false"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_bagsold"
                                                                        FilterType="Custom,Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity Sold(in Qtl.)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_quantitysold" runat="server" ForeColor="black" Font-Bold="true"
                                                                        BackColor="White" Enabled="false"></asp:TextBox>
                                                                    <br />
                                                                    <asp:HiddenField ID="hdn_txtquintal" runat="server" Visible="False"></asp:HiddenField>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btn_add" runat="server" Font-Bold="True" BackColor="Black" ForeColor="White"
                                                                        Text="Add" Enabled="False" CausesValidation="false" OnClick="btn_add_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#B2C629" />
                                                        <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="Data not found for this Variety,Select another Variety"
                                                                Font-Bold="true" ForeColor="red"></asp:Label>
                                                        </EmptyDataTemplate>
                                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="#B2C629" />
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
                                                            <asp:TemplateField HeaderText="GoDown Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_GoDown" runat="server" Text='<%#Eval("GODOWN_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Variety">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Variety" runat="server" Text='<%#Eval("VARIETY_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lot No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Crop" runat="server" Text='<%#Eval("Crop_Name") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_LotNo" runat="server" Text='<%#Eval("LOT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bag Size(Kg.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_BagSize" runat="server" Text='<%#Eval("BAG_SIZE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Bags">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_BagReceive" runat="server" Text='<%#Eval("NO_OF_BAGS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total(In Qtl)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Qty1" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <FooterStyle HorizontalAlign="Center" Width="100px" ForeColor="White" />
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotQty" runat="server" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Amt1" runat="server" Text='<%#Eval("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <FooterStyle HorizontalAlign="Center" Width="100px" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotAmt" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btn_Delete" runat="server" Font-Bold="True" BackColor="Red" ForeColor="White"
                                                                        Text="Delete" Enabled="true" CausesValidation="false" OnClick="btn_Delete_Click"></asp:Button>
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
                                        </td>
                                    </tr>
                                    <tr id="trSubmit" runat="server">
                                        <td style="text-align: center;" colspan="3">
                                            <asp:Button ID="btn_Submit" runat="server" Font-Bold="True" Text="Submit" OnClick="btn_Submit_Click"></asp:Button>&nbsp;&nbsp;
                                        <asp:Button ID="btn_ExistVariety" runat="server" Font-Bold="True" Text="Exit Variety"
                                            OnClick="btn_ExistVariety_Click"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopUp" runat="server" PopupControlID="pnlPopUp"
                            TargetControlID="btnShow" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" Style="display: none; width: 900px; border-spacing: 0px; padding: 0px; height: 600px; overflow-y: auto; text-align: center; vertical-align: top;"
                            ScrollBars="Auto">
                            <center>
                                <table border="1" style="width: 100%; border-spacing: 0px; padding: 0px; background-color: lightyellow;">
                                    <tr>
                                        <td style="text-align: center;">
                                            <table border="0" style="font-size: 10pt; font-family: Verdana; padding: 2px; border-spacing: 0px; width: 100px;">
                                                <tr>
                                                    <td colspan="2" style="height: 38px; text-align: center;">
                                                        <asp:LinkButton ID="LinkButton2" runat="server">
                                                            <asp:Image ID="Image1" runat="server" Width="150" Height="150" />
                                                            <%--<img src="../images/orissalogo.jpg" id="imgLogo" runat="server" alt="" border="0"/>--%>
                                                        </asp:LinkButton>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; font-weight: bold;" colspan="2">
                                                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: right;"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center;">Stock Delivered from
                                                <asp:Label ID="lbl_stockedat" runat="server" Font-Bold="True"></asp:Label>
                                                        Godown<br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; height: 20px;">
                                                        <%--CashMemo Number:
                                                <asp:Label ID="lbl_cashmemono" runat="server" Font-Bold="True"></asp:Label>--%>
                                                    </td>
                                                    <td style="text-align: right; height: 20px;">Delivered Date:
                                                <asp:Label ID="lbl_recvdt" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">&nbsp;Delivered To :<asp:Label ID="lbl_saletonm" runat="server" Font-Bold="True"
                                                        Font-Size="8pt"></asp:Label><br />
                                                    </td>
                                                    <td align="left" id="td_LicenceNo" runat="server">Licence No :
                                                <asp:Label ID="lblLicenceNo" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-weight: bold; text-align: left;">Delivery Details:<br />
                                                        <hr />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: left;">

                                                        <asp:GridView ID="grd_stockdtls" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                                            Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No.">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GoDown Name">
                                                                    <ItemTemplate>
                                                                        <%#Eval("GoDown_Name")%>
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
                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <%#Eval("CROP_CLASS")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Source">
                                                                    <ItemTemplate>
                                                                        <%#Eval("Receive_Unitname")%>
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
                                                                        <asp:Label ID="lbl_NoofBags" runat="server" Text='<%#Eval("NO_OF_BAGS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bag Size(in Kg.)">
                                                                    <ItemTemplate>
                                                                        <%#Eval("BAG_SIZE")%>
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
                                                    <td colspan="2" style="text-align: left;">
                                                        <br />
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center; height: 20px;">
                                                        <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Confirm"
                                                            Width="100px" />
                                                        &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                    Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
            <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upStockSALE"
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
    </asp:UpdatePanel>
</asp:Content>


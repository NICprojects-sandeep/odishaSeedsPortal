<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="OsscLotEntry.aspx.cs" Inherits="Reports_OsscLotEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%--<link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>
    <%--    <link href="../bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
    <%--    <link href="../bootstrap/css/bootstrap-grid.css" rel="stylesheet" />
    <link href="../bootstrap/css/bootstrap-grid.min.css" rel="stylesheet" />--%>
    <script language="javascript" type="text/javascript">
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

    <div class="bedcromb">
        <asp:Label ID="lblTitle" runat="server" Text="Foundation to Certifieds"></asp:Label>
    </div>
    <div id="divview" runat="server" style="display: block;">
        <div style="padding-top: 15px; padding-left: 30px;">
            <asp:Panel ID="Pnl2" runat="server">
                <div style="width: 1100px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px;">
                        <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
                        <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />
                        <div style="width: 100%;">
                            <div>
                                <center><strong>Foundation to Certified</strong></center>
                            </div>
                            <hr />
                            <table id="tblMainSection" runat="server" width="100%" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td width="140" class="normaltd" style="text-align:left;"></td>
                                    <td width="140" class="normaltd"></td>
                                    <td width="5" class="normaltd"></td>
                                    <td width="140" class="normaltd"></td>
                                    <td width="140" class="normaltd"></td>
                                    <td width="5" class="normaltd"></td>
                                    <td><span class="mandatory"></span></td>
                                </tr>
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd">Crop</td>
                                    <td class="normaltd">:</td>
                                    <td class="normaltd">
                                        <asp:DropDownList ID="ddlCrop" class="DropdownCss" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td class="normaltd" valign="top">Variety</td>
                                    <td class="normaltd" valign="top">:
                                    </td>
                                    <td class="normaltd" align="left">
                                        <asp:DropDownList ID="ddlVariety" class="DropdownCss" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd" valign="top">Lot Number</td>
                                    <td class="normaltd" valign="top">:</td>
                                    <td class="normaltd" align="left">
                                        <asp:TextBox ID="txtLotNo" runat="server" CssClass="TextboxCss"></asp:TextBox></td>
                                    <td class="normaltd" valign="top">Qty</td>
                                    <td class="normaltd" valign="top">:</td>
                                    <td class="normaltd" align="left">
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="TextboxCss" onkeypress="return blockNonNumbers(this, event, true, false);"></asp:TextBox></td>
                                </tr>                               
                                <tr>
                                    <td class="normaltd"></td>
                                    <td class="normaltd" valign="top">Source</td>
                                    <td class="normaltd" valign="top">:</td>
                                    <td class="normaltd" align="left">
                                        <asp:TextBox ID="txtSource" runat="server" CssClass="TextboxCss"></asp:TextBox></td>
                                    <td class="normaltd" valign="top"></td>
                                    <td class="normaltd" valign="top"></td>
                                    <td class="normaltd" align="left">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" class="ButtonCss" OnClick="btnAdd_Click" Width="100%" /></td>
                                </tr>
                               
                            </table>
                            <hr />
<br />
    <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


    
</asp:Content>


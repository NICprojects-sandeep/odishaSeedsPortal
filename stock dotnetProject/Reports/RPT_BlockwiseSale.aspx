<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="RPT_BlockwiseSale.aspx.cs" Inherits="Reports_RPT_BlockwiseSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script>

        function Valid() {


            var CropName = document.getElementById("<%=drpCropName.ClientID %>");
            if (CropName.value.trim() == "--- SELECT ---") {
                alert('Please Select Crop Name !');
                CropName.focus();
                return false;
            }
        }
        function popUp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=900,height=500,left = 190,top = 154');");
        }
    </script>
    <style>
        .tbltxt {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000;
            padding: 3px;
        }
    </style>
    <div>
        <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;" CssClass="tbltxt">
            <div style="width: 1200px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1200px;">
                    <table style="padding: 10px;" class="tabletxt">

                        <tr>

                            <td style="width: 70px;">Dist Name</td>
                            <td style="width: 5px;">:</td>
                            <td>

                                <asp:Label ID="lblDist" runat="server"></asp:Label>

                            </td>

                            <td style="width: 70px;">Crop </td>
                            <td style="width: 5px;">:</td>
                            <td>

                                <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>

                            </td>

                        </tr>
                        <tr>
                            <td style="width: 115px;" class="normaltd">Session Year </td>
                            <td style="width: 5px;">:</td>
                            <td style="width: 238px">

                                <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>

                            </td>

                            <td style="width: 115px;" class="normaltd">Season  </td>
                            <td style="width: 5px;">:</td>
                            <td>

                                <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                    <asp:ListItem Value="0">--All--</asp:ListItem>
                                    <asp:ListItem Value="R">Rabi</asp:ListItem>
                                    <asp:ListItem Value="K">Kharif</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <br>
                        <tr>

                            <td colspan="5">&nbsp;</td>
                            <td align="right">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClientClick="return Valid();" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnExport_Click" />
                            </td>
                        </tr>

                    </table>
                    <fieldset class="tbl">

                        <legend style="margin-left: 10px; font-weight: bold;">Blockwise Sale.</legend>
                        <asp:Literal ID="litReport" runat="server"></asp:Literal>
                    </fieldset>

                </div>
            </div>
            <div style="height: 30px;">&nbsp;</div>
        </asp:Panel>
    </div>
</asp:Content>


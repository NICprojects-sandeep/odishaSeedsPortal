<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true"
    CodeFile="RPT_DISTWISEUPDTDEALER.aspx.cs" Inherits="Reports_RPT_DISTWISEUPDTDEALER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp; Distwise Stock Details
    </div>
    <br />
    <script>

        function Valid() {

            var CropCatagory = document.getElementById("<%=drpCropName.ClientID %>");
            if (CropCatagory.value.trim() == "--- SELECT ---") {
                alert('Please Select Crop Name !');
                CropCatagory.focus();
                return false;
            }
        }
        function popUp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=700,height=500,left = 390,top = 184');");
        }
    </script>
    <style>
        .tbltxt
        {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000;
            padding: 3px;
        }
    </style>
    <div>
        <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;" CssClass="tbltxt">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 0px; width: 1200px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt" cellpadding="10" cellspacing="10">
                        <tr>
                            <td style="width: 115px;" class="normaltd">
                                Session Year
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>
                            <td style="width: 115px;" class="normaltd">
                                Season
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSeason" runat="server" CssClass="DropdownCss">
                                    <asp:ListItem Value="0">--All--</asp:ListItem>
                                    <asp:ListItem Value="K">Kharif</asp:ListItem>
                                    <asp:ListItem Value="R">Rabi</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="normaltd" style="width: 115px;">
                                Dist Name
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td style="width: 247px">
                                <asp:DropDownList ID="drpDist" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 115px;">
                                Crop Name
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td style="width: 125px;">
                                <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click"
                                    OnClientClick="return Valid();" />
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                            </td>
                        </tr>
                    </table>
                    <div style="color: red;">
                        *<b>NOTE:</b> The physical Sale is entered/updated by the field official represents
                        the real sale irrespective of Dealer has entered the data in the system or not.
                        But if the updation of the physical Sale is not done by the field official on time
                        and online sale entry is done by the Dealer then online sale may be represented
                        more than the physical sale. In this case physical sale may be considered as old
                        information.</div>
                    <fieldset class="tbl">
                        <legend style="margin-left: 10px; font-weight: bold;">Figures in Qntl.</legend>
                        <div style="overflow: scroll; width: 1170px;">
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div style="height: 30px;">
                &nbsp;</div>
        </asp:Panel>
    </div>
</asp:Content>

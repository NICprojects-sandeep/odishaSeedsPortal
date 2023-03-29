<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true"
    CodeFile="RPT_DealerPacksSale.aspx.cs" Inherits="Reports_RPT_DealerPacksSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp; Dealer/PACS Sale
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

    </script>
    <style>
        .tbltxt
        {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000;
            padding: 3px;
        }
        
        .auto-style1
        {
            color: #000000;
            font-size: 12px;
            font-weight: normal;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            width: 86px;
        }
        
        .auto-style2
        {
            width: 229px;
        }
        
        .auto-style3
        {
            color: #000000;
            font-size: 12px;
            font-weight: normal;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            width: 53px;
        }
    </style>
    <div>
        <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;" CssClass="tbltxt">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="width: 115px;" class="normaltd">
                                Session Year
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 115px;" class="normaltd">
                                Season
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                    <asp:ListItem Value="0">--All--</asp:ListItem>
                                    <asp:ListItem Value="R">Rabi</asp:ListItem>
                                    <asp:ListItem Value="K">Kharif</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                Crop Name
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style3">
                                User
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td style="width: 266px">
                                <asp:DropDownList ID="drpUser" runat="server" CssClass="DropdownCss">
                                    <asp:ListItem Value="0">--ALL--</asp:ListItem>
                                    <asp:ListItem>OSSC</asp:ListItem>
                                    <asp:ListItem>OAIC</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click"
                                    OnClientClick="return Valid();" />
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                            </td>
                        </tr>
                    </table>
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

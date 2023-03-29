<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RPT_DealerSaleDtl.aspx.cs" Inherits="Reports_RPT_DealerSaleDtl" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp;
 Dealer wise Sale Details
    </div>
    <br />

    <style>
        .tbltxt {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000;
            padding: 3px;
        }

        .auto-style1 {
            width: 4px;
        }

        .auto-style2 {
            width: 211px;
        }
    </style>
    <script>


        function popUp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=800,height=500,left = 290,top = 114');");
        }
    </script>
    <div>
        <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" CssClass="tbltxt" Style="padding-top: 30px;">
                    <div style="width: 1220px; background-color: #666; padding: 2px;">
                        <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                            <table style="width: 100%; padding: 0px;" class="tabletxt" cellpadding="10" cellspacing="0">
                                <tr>
                                    <td style="width: 115px;" class="normaltd">Session Year</td>
                                    <td style="width: 5px;">:</td>
                                    <td>
                                        <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>
                                    <td style="width: 115px;" class="normaltd">Season</td>
                                    <td style="width: 5px;">:</td>
                                    <td>

                                        <asp:DropDownList ID="drpSeason" runat="server" CssClass="DropdownCss">
                                            <asp:ListItem Value="0">--All--</asp:ListItem>
                                            <asp:ListItem Value="K">Kharif</asp:ListItem>
                                            <asp:ListItem Value="R">Rabi</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>



                                    <td style="width: 115px;" class="normaltd">Crop</td>
                                    <td style="width: 5px;">:</td>
                                    <td>

                                        <asp:DropDownList ID="drpCrop" runat="server" CssClass="DropdownCss">
                                            <asp:ListItem>--- All ---</asp:ListItem>
                                            <asp:ListItem Value="C002">PaddyDhan</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>

                                    <td style="width: 115px;" class="normaltd">Type of Dealer</td>
                                    <td style="width: 5px;">:</td>
                                    <td class="auto-style2">

                                        <asp:DropDownList ID="drpDealer" runat="server" CssClass="DropdownCss">
                                            <asp:ListItem Value="0">--All--</asp:ListItem>
                                            <asp:ListItem Value="Proprietor">Dealer</asp:ListItem>
                                            <asp:ListItem Value="Secretary PACS">PACS</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td style="width: 115px;" class="normaltd">Scheme</td>
                                    <td style="width: 5px;">:</td>
                                    <td>

                                        <asp:DropDownList ID="drpScheme" runat="server" CssClass="DropdownCss">
                                        </asp:DropDownList>

                                    </td>
                                    <td class="normaltd" colspan="3">From Date<span class="mandatory">*</span>:<asp:TextBox ID="txtfromdate" Width="90px" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>

                                        To Date<span class="mandatory">*</span>:
                                  <asp:TextBox ID="txtTodate" runat="server" CssClass="TextboxCss" Width="90px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>

                                <tr>

                                    <td style="width: 115px;" class="normaltd">District</td>
                                    <td style="width: 5px;">:</td>
                                    <td class="auto-style1">
                                        <asp:DropDownList ID="drpDistrict" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList></td>
                                    <td style="width: 115px;" class="normaltd">Block</td>
                                    <td style="width: 5px;">:</td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="drpBlock" runat="server" CssClass="DropdownCss">
                                        </asp:DropDownList></td>

                                    <td colspan="3">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />

                                    </td>
                                </tr>

                            </table>
                            <fieldset class="tbl">

                                <legend style="margin-left: 10px; font-weight: bold;">Dealer wise sale detail</legend>
                                <div style="overflow: scroll; width: 1170px;">
                                    <asp:Literal ID="litReport" runat="server"></asp:Literal>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div style="height: 30px;">&nbsp;</div>
                </asp:Panel>

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
            <Triggers>

                <ajax:PostBackTrigger ControlID="btnExport" />
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            </Triggers>
        </ajax:UpdatePanel>
    </div>
</asp:Content>




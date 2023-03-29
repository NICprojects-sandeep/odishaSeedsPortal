<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="RPT_Schemewisedtl1.aspx.cs" Inherits="Reports_RPT_Schemewisedtl1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp; SCHEMEWISE DETAILS
    </div>
    <br />
    <script>

        function Valid() {


        }
        function popUp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=800,height=500,left = 290,top = 114');");
        }
    </script>
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;">
                <div style="width: 1200px; background-color: #666; padding: 2px;">
                    <div style="background-color: #FFF; width: 1200px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">
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



                                <td style="width: 115px;" class="normaltd">Scheme</td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:DropDownList ID="drpScheme" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                              
                                <td style="width: 115px;" class="normaltd">Type of Dealer</td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:DropDownList ID="drpDealer" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem Value="Dealer">Dealer</asp:ListItem>
                                        <asp:ListItem Value="PACS">PACS</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td style="width: 115px;" class="normaltd">Crop </td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>

                                </td> <td style="width: 115px;" class="normaltd">District</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drpDistrict" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>

                            </tr>



                            <tr>

                                <td colspan="8">&nbsp;</td>
                                <td colspan="2">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                                </td>
                            </tr>

                        </table>
                        <fieldset class="tbl">

                            <legend style="margin-left: 10px; font-weight: bold;">Schemewise Details</legend>
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </fieldset>
                    </div>
                </div>
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
        </Triggers>
    </ajax:UpdatePanel>
</asp:Content>



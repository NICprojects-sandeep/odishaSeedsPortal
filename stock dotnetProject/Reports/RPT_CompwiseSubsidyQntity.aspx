<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true"
    CodeFile="RPT_CompwiseSubsidyQntity.aspx.cs" Inherits="Reports_RPT_CompwiseSubsidyQntity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp; Componentwise Subsidy Sale (Quantity)
    </div>
    <br />
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
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=700,height=500,left = 390,top = 184');");
        }
    </script>
    <ajax:updatepanel id="updatepanel" runat="server" updatemode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server"  style="padding-top: 30px;">
                <div style="width: 1200px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1200px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">
                            <tr>
                                <td style="width: 115px;" class="normaltd">Session Year</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss" AutoPostBack="true" OnSelectedIndexChanged="drpSession_SelectedIndexChanged" >
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
                                <td style="width: 115px;" class="normaltd">Agency</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drpAgency" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem>OSSC</asp:ListItem>
                                        <asp:ListItem>OAIC</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="width: 115px;" class="normaltd">Type of Dealer</td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:DropDownList ID="drpDealer" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem Value="Proprietor">Dealer</asp:ListItem>
                                        <asp:ListItem Value="Secretary PACS">PACS</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td style="width: 115px;" class="normaltd">Crop </td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>

                                <td colspan="6">&nbsp;</td>
                                <td colspan="3">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                                </td>
                            </tr>

                        </table>
                        <fieldset class="tbl">

                            <legend style="margin-left: 10px; font-weight: bold;">Figures in Qntl.</legend>
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </fieldset>
                    </div>
                </div>
            </asp:Panel>
            </div>
                      <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="updatepanel"
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
        </ContentTemplate>
        <Triggers>

            <ajax:PostBackTrigger ControlID="btnExport" />
<asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
        </Triggers>
    </ajax:updatepanel>
</asp:Content>

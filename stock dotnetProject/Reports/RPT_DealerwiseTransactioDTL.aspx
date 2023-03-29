<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RPT_DealerwiseTransactioDTL.aspx.cs" Inherits="Reports_RPT_DealerwiseTransactioDTL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
         <div class="titleHeader">
        &nbsp; Dealerwise Transaction Details
    </div>
    <br />
    <script>
        function PrintPanel() 
        {
            var panel = document.getElementById("<%=pnlPrintArea.ClientID %>");
              var printWindow = window.open('', '', 'height=400,width=800');
              printWindow.document.write('<html><head>');
              printWindow.document.write('</head><body >');
              printWindow.document.write(panel.innerHTML);
              printWindow.document.write('</body></html>');
              printWindow.document.close();
              setTimeout(function () {
                  printWindow.print();
              }, 500);
              return false;
          }
         

        function popUp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=900,height=500,left = 190,top = 154');");
        }
    </script>
    <div>
         <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px; height: 500px;">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
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
                                    <tr>
                                        <td class="normaltd" style="width: 115px;">Dist Name</td>
                                        <td style="width: 5px;">:</td>
                                        <td style="width: 247px">
                                            <asp:DropDownList ID="drpDist" runat="server" CssClass="DropdownCss">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="normaltd" style="width: 115px;">Crop Name</td>
                                        <td style="width: 5px;">:</td>
                                        <td style="width: 185px;">
                                            <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" Width="98px" />
                                              <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();" />
                                        </td>
                                    </tr>

                    </table>
                                    <div style="color:red;">*<b>NOTE:</b> The physical Sale is entered/updated by the field official represents the real sale irrespective of Dealer has entered the data in the system or not. But if the updation of the physical Sale is not done by the field official on time and online sale entry is done by the Dealer then online sale may be represented more than the physical sale. In this case physical sale may be considered as old information.</div>
                     <asp:Panel ID="pnlPrintArea" runat="server" Width="100%">
                    <asp:Literal ID="litReport" runat="server"></asp:Literal>
                         </asp:Panel>
                </div>
            </div>
        </asp:Panel>

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

            <asp:PostBackTrigger ControlID="btnPrint"></asp:PostBackTrigger>
        </Triggers>
    </ajax:UpdatePanel>
    </div>
</asp:Content>


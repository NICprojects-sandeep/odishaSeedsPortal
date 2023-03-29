<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="RPT_RejectedFarmerList.aspx.cs" Inherits="Reports_RPT_RejectedFarmerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
                             <div class="titleHeader">
        &nbsp; Rejected Farmer List
    </div>
    <br />
     <script>

         function Valid()
         {
             var FarmId = document.getElementById("<%=drpDist.ClientID %>");
             if (FarmId.value.trim() == "--- SELECT ---")
             {
                alert('Please Select District !');
                FarmId.focus();
                return false;
            }
         }
         function getPrint(print_area)
         {
             var divToPrint = document.getElementById(print_area);
             var printWin = window.open('', 'Mail In', 'left=0,top=0,width=1000,height=600,status=0');
             printWin.document.write("<div align='center' style='width:1000px;margin:auto;padding-top:10px'>");
             printWin.document.write(divToPrint.outerHTML);
             printWin.document.write("</div>");
             printWin.document.close();
             printWin.focus();
             printWin.print();
         }
         function popUp(URL)
         {
             day = new Date();
             id = day.getTime();
             eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=900,height=500,left = 190,top = 154');");
         }
    </script>
     <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
     <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;" CssClass="tbltxt">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                    <table style="width: 100%; padding:5px;" class="tabletxt" cellpadding="0" cellspacing="0">
                        <tr>
                             <td style="width: 112px; height: 25px;" class="normaltd">District<span class="mandatory">*</span></td>
                                <td style="width: 208px; height: 25px;">:<asp:DropDownList ID="drpDist" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpDist_SelectedIndexChanged" AutoPostBack="true"> </asp:DropDownList>
                                </td>

                            <td style="width: 45px; height: 25px;" class="normaltd">Block</td>
                                <td style="width: 289px; height: 25px;">:<asp:DropDownList ID="drpBlock" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpBlock_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                </td>
                           
                        </tr>
                        <tr>
                             <td style="width: 112px; height: 25px;" class="normaltd">Gram Panchayat</td>
                                <td style="width: 208px; height: 25px;">:<asp:DropDownList ID="drpGP" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpGP_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                </td>

                             <td style="width: 45px; height: 25px;" class="normaltd">Village</td>
                                <td style="width: 289px; height: 25px;">:<asp:DropDownList ID="drpVillage" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>
                                </td>
                                
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                            <td colspan="4" style="width: 289px"  >
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClientClick="return Valid();" OnClick="btnSearch_Click"   />
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnExport_Click" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" />
                            </td>
                        </tr>
                    </table>
                    <fieldset class="tbl">
                        <div id="divPrint">
                        <legend style="margin-left: 10px; font-weight: bold;">Figures in Qntl.</legend>
                        <div style="overflow: scroll; width: 1170px;">
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </div>
                            </div>
                    </fieldset>
                </div>
            </div>
            <div style="height: 30px;">&nbsp;</div>
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

            <ajax:PostBackTrigger ControlID="btnExport" />
<asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnPrint"></asp:PostBackTrigger>
        </Triggers>
    </ajax:UpdatePanel>
</asp:Content>


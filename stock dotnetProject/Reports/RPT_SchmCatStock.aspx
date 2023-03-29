<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RPT_SchmCatStock.aspx.cs" Inherits="Reports_RPT_SchmCatStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp; Schemewise Categorywise Details
    </div>
    <br />
    <script>
        function getPrint(print_area) {
            var divToPrint = document.getElementById(print_area);
            var printWin = window.open('', 'Mail In', 'left=0,top=0,width=1000,height=600,status=0');
            printWin.document.write("<div align='center' style='width:1000px;margin:auto;padding-top:10px'>");
            printWin.document.write(divToPrint.outerHTML);
            printWin.document.write("</div>");
            printWin.document.close();
            printWin.focus();
            printWin.print();
        }
    </script>
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;" CssClass="tbltxt">
                <div style="width: 1220px; padding: 2px; margin: 0 auto;">
                    <div style="padding: 10px; width: 1200px;">
                        <table style="width: 100%; padding: 5px;" class="tabletxt" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style14" style="width: 74px">Session Year</td>
                                <td class="auto-style19">:</td>
                                <td class="auto-style15" style="width: 210px">
                                    <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>
                                <td class="auto-style7" style="width: 45px">Season </td>
                                <td>:</td>
                                <td class="auto-style17" style="width: 213px">
                                    <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem Value="R">Rabi</asp:ListItem>
                                        <asp:ListItem Value="K">Kharif</asp:ListItem>
                                    </asp:DropDownList></td>                                                                
                                <td style="width: 50px;" class="normaltd">Crop</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 207px;">
                                    <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClientClick="return Valid();" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnExport_Click" />
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divPrint">
                        <table style="width:100%;">
                            <tr>
                                <td style="width:50%;"><asp:Literal ID="littime" runat="server" Text="SCHEMEWISE DETAILS   on"></asp:Literal></td>
                                <td style="width:50%;text-align:right;"><asp:Literal ID="litCrop" runat="server" Text=""></asp:Literal></td>
                            </tr>
                        </table>
                        
                        <fieldset class="tbl">
                            <legend style="margin-left: 10px; font-weight: bold;"></legend>
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </fieldset>
                    </div>
                        </div>
                </fieldset>
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
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnPrint"></asp:PostBackTrigger>
        </Triggers>
    </ajax:UpdatePanel>


</asp:Content>


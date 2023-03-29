<%@ Page Title="Input Subsidy" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="RPT_InputSubsidy.aspx.cs" Inherits="Reports_RPT_InputSubsidy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <div class="titleHeader">
        &nbsp;
 INPUT SUBSIDY
    </div>
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <div style="width: 1200px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1200px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">
                            <tr>
                                <td style="width: 154px;" class="normaltd">Session Year</td>
                                <td style="width: 395px">:<asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>
                                </td>

                                <td style="width: 115px;" class="normaltd">Season </td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem Value="R">Rabi</asp:ListItem>
                                        <asp:ListItem Value="K">Kharif</asp:ListItem>
                                    </asp:DropDownList></td>

                            </tr>                            

                            <tr>

                                <td colspan="2">&nbsp;</td>
                                <td colspan="3">

                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                            </tr>

                        </table>
                        <fieldset class="tbl">

                            <legend style="margin-left: 10px; font-weight: bold;">Figures in Qntl. For Quantity Sold </legend>                            
                            <div style="overflow: scroll; width: 1170px;">
                                <asp:Literal ID="litReport" runat="server"></asp:Literal>                                
                            </div>
                        </fieldset>
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

            <ajax:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
        </Triggers>
    </ajax:UpdatePanel>
</asp:Content>


﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="Rpt_DistwiseUserwiseStock2.aspx.cs" Inherits="Reports_Rpt_DistwiseUserwiseStock2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <script>

           function Valid() {

               var CropCatagory = document.getElementById("<%=drpCropCatagory.ClientID %>");
            if (CropCatagory.value.trim() == "--- SELECT ---") {
                alert('Please Select Crop Catagory !');
                CropCatagory.focus();
                return false;
            }
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
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <div style="width: 1200px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1200px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">
                            <tr>
                                <td style="width: 115px;" class="normaltd">Session Year</td>
                                <td style="width: 32px;">:</td>
                                <td style="width: 251px">
                                    <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>
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
                            <tr>
                                <td style="width: 115px;" class="normaltd">Crop Catagory<span class="mandatory">*</span></td>
                                <td style="width: 32px;">:</td>
                                <td style="width: 251px">

                                    <asp:DropDownList ID="drpCropCatagory" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddl_CropCatagory_SelectedIndexChanged1">
                                    </asp:DropDownList>

                                </td>


                                 <td class="normaltd">Crop Name<span class="mandatory">*</span></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss"></asp:DropDownList></td>
                                <td class="normaltd"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="4"></td>
                    <td colspan="3">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" OnClientClick="return Valid();" Text="Search" Width="98px" />
                        <asp:Button ID="btnExport" runat="server" OnClick="btnToExcel_Click" Text="Export to Excel" Width="128px" />
                                </td>
                               
                            </tr>

                        </table>
                        <fieldset class="tbl">

                            <legend style="margin-left: 10px; font-weight:bold;">Figures in Qntl.</legend>
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
        </Triggers>
    </ajax:UpdatePanel>
</asp:Content>


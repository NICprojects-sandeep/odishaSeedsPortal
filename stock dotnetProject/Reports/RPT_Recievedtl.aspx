<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="RPT_Recievedtl.aspx.cs" Inherits="Reports_RPT_Recievedtl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <script>

        function Valid() {
            var frmDt = document.getElementById("<%=txtFrmDt.ClientID %>");
            if (frmDt.value.trim() == "") {
                alert('Please Select From Date !');
                frmDt.focus();
                return false;
            }
            var toDt = document.getElementById("<%=txtToDt.ClientID %>");
            if (toDt.value.trim() == "") {
                alert('Please Select To Date !');
                toDt.focus();
                return false;
            }

        }
    </script>
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" Style="margin-top: 15px;">
                <div style="width: 1300px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; width: 1280px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">
                            <tr>
                                <td style="width: 115px;" class="normaltd">Season</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                 <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                       <asp:ListItem Value="0">--All--</asp:ListItem>
                                       <asp:ListItem Value="R">Rabi</asp:ListItem>
                                       <asp:ListItem Value="K">Kharif</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                <td style="width: 115px;" class="normaltd">Select Godown</td>
                                <td style="width: 5px;">:</td>
                                <td >
                                    <asp:DropDownList ID="drpGodown" CssClass="DropdownCss" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 94px;" class="normaltd">From Date</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 261px">
                                    <asp:TextBox ID="txtFrmDt" runat="server" CssClass="TextboxCss" ></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" SetFocusOnError="True"
                                        ControlToValidate="txtFrmDt" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                        ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrmDt"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>

                                </td>
                                <td style="width: 72px;" class="normaltd">To Date</td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:TextBox ID="txtToDt" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="True"
                                        ControlToValidate="txtToDt" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                        ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDt"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>

                                </td>
                                 </tr>
                            <tr>
                                <td colspan="9" align="center">
                           
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                    &nbsp;<asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                                </td>
                           </tr>
                            
                        </table>
                        <hr />
                        <asp:Literal ID="litReport" runat="server"></asp:Literal>
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


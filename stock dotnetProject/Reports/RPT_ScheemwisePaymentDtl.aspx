<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="RPT_ScheemwisePaymentDtl.aspx.cs" Inherits="Reports_RPT_ScheemwisePaymentDtl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp; Schemewise Payment Details
    </div>
    <br />
    <script type="text/javascript">

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
    <ajax:updatepanel id="updatepanel" runat="server" updatemode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" style="padding-top: 30px;">
                <div style="width: 1200px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1200px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">

                            <tr>
                                 <td style="width: 105px;" class="normaltd">Session Year</td>
                                <td style="width: 5px;">:</td>
                                <td class="auto-style4" style="width: 299px">
                                    <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>
                                <td style="width: 103px;" class="normaltd">Season </td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 295px">
                                    <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem Value="R">Rabi</asp:ListItem>
                                        <asp:ListItem Value="K">Kharif</asp:ListItem>
                                    </asp:DropDownList></td>

                                
                                <td style="width: 42px;" class="normaltd">Scheme</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 175px;">

                                    <asp:DropDownList ID="drpScheme" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>

                                </td>
                              </tr>
                            <tr>
                              <td style="width: 105px;" class="normaltd">Crop</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 299px;">
                                    <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td>

                                  <td style="width: 103px;" class="normaltd">District</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 295px">
                                    <asp:DropDownList ID="drpDistrict" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList></td> 

                              <td style="width: 42px;" class="normaltd">User</td>
                                <td style="width: 5px;">:</td>
                               
                             <td>
                                    <asp:DropDownList ID="drpUser" runat="server" CssClass="DropdownCss">
                                         <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem>OSSC</asp:ListItem>
                                        <asp:ListItem>OAIC</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 105px;" class="normaltd">Month </td>
                                <td style="width: 5px;">:</td>
                               <td class="auto-style4" style="width: 299px">
                                    <asp:DropDownList ID="drpMonth" runat="server" CssClass="DropdownCss">
                                      <asp:ListItem Value="0">--All--</asp:ListItem>
                                     <asp:ListItem Value="01">JANUARY</asp:ListItem>
                                     <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                                     <asp:ListItem Value="03">MARCH</asp:ListItem>
                                     <asp:ListItem Value="04">APRIL</asp:ListItem>
                                     <asp:ListItem Value="05">MAY</asp:ListItem>
                                     <asp:ListItem Value="06">JUNE</asp:ListItem>
                                     <asp:ListItem Value="07">JULY</asp:ListItem>
                                     <asp:ListItem Value="08">AUGUST</asp:ListItem>
                                     <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                                     <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                                     <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                     <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                                    </asp:DropDownList></td>  

                              <td class="normaltd" colspan="3"> From Date<span class="mandatory"></span>:<asp:TextBox ID="txtfromdate" Width="95px" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                  <asp:CalendarExtender ID="fromDatecalendarExtender" runat="server" TargetControlID="txtfromdate"
                                                Format="yyyy-MM-dd" ></asp:CalendarExtender>
                                  
                                  To Date<span class="mandatory"></span>:
                                  <asp:TextBox ID="txtTodate" runat="server" CssClass="TextboxCss" Width="95px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTodate"
                                                Format="yyyy-MM-dd" ></asp:CalendarExtender>
                              </td>
                               
                          <td colspan="9">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" OnClientClick="return Valid();" Style="margin-left: 14px" Text="Search" Width="98px" />
                                <asp:Button ID="btnExport" runat="server" OnClick="btnToExcel_Click" Text="Export to Excel" Width="128px" />
                                <asp:Button ID="btnPrint" runat="server" OnClientClick="getPrint('divPrint')" Text="Print" />
                                </td>
                            </tr>


                        </table>
                        <fieldset class="tbl">

                            <legend style="margin-left: 10px; font-weight: bold;">Consolidated Seed Sale</legend>
                            <div id="divPrint">
                                <asp:Literal ID="litReport" runat="server"></asp:Literal>
                            </div>
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
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
        </Triggers>
    </ajax:updatepanel>
</asp:Content>

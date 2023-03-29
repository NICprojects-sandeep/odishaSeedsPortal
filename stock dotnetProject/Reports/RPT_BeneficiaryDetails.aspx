<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="RPT_BeneficiaryDetails.aspx.cs" Inherits="Reports_RPT_BeneficiaryDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" crossorigin="anonymous">
    <div class="titleHeader">
        &nbsp;  Benificiary Details
    </div>
    <br />
    <%--<script type="text/javascript">
         function verifyDate(sender, args) {
             var selectedDate = new Date();
             var toDt = document.getElementById("<%=CalendarExtender1.ClientID %>")
             selectedDate = sender._selectedDate;
             if (document.getElementById("<%=txtTodate.ClientID %>").value.trim() !='')
             {
             if (selectedDate > toDt.valueOf._selectedDate) {
                 alert("You cannot select a day earlier than From Date!");
                
             }
         }
         }
    </script>--%>
    <script>

        function Valid() {


            var CropName = document.getElementById("<%=drpCropName.ClientID %>");
            if (CropName.value.trim() == "--- Select ---") {
                alert('Please Select Crop Name !');
                CropName.focus();
                return false;
            }

            var Dist = document.getElementById("<%=drpDistrict.ClientID %>");
            if (Dist.value.trim() == "--- Select ---") {
                alert('Please Select District !');
                Dist.focus();
                return false;
            }

            var Block = document.getElementById("<%=drpBlock.ClientID %>");
            if (Block.value.trim() == "---Select---") {
                alert('Please Select Block !');
                Block.focus();
                return false;
            }

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
                <div style="width: 1150px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; width: 1150px;">
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
                                <td style="width: 115px;" class="normaltd">User</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drUser" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                        <asp:ListItem>OSSC</asp:ListItem>
                                        <asp:ListItem>OAIC</asp:ListItem>
                                    </asp:DropDownList></td>


                                <td style="width: 115px;" class="normaltd">Crop </td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>

                                </td>

                                <td style="width: 115px;" class="normaltd">District</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drpDistrict" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpDistrict_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList></td>
                            </tr>

                            <tr>
                                <td style="width: 115px;" class="normaltd">Block</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drpBlock" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpBlock_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                <td style="width: 115px;" class="normaltd">G.P </td>
                                <td style="width: 5px;">:</td>
                                <td>

                                    <asp:DropDownList ID="drpGP" runat="server" CssClass="DropdownCss"  AutoPostBack="True" OnSelectedIndexChanged="drpGp_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </td>

                                <td class="normaltd">Village</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="drpVillage" runat="server"  CssClass="DropdownCss" ></asp:DropDownList></td>
                            </tr>

                            <tr>




                                <td colspan="6">From Date<span class="mandatory"></span>:<asp:TextBox ID="txtfromdate" Width="98px" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                    <asp:CalendarExtender ID="fromDatecalendarExtender" runat="server" TargetControlID="txtfromdate"
                                        Format="yyyy-MM-dd">
                                    </asp:CalendarExtender>

                                    To Date<span class="mandatory"></span>:
                                  <asp:TextBox ID="txtTodate" runat="server" CssClass="TextboxCss" Width="98px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTodate" Enabled="true"
                                        Format="yyyy-MM-dd">
                                    </asp:CalendarExtender>
                                </td>
                                <td colspan="3">
                                    <asp:Button ID="btnSearch" runat="server" OnClientClick="return Valid();" Text="Search" Width="98px" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnExport_Click" />
                                </td>


                            </tr>

                        </table>
                        <asp:Literal ID="litNote" runat="server"></asp:Literal>
                        <fieldset class="tbl">

                            <legend style="margin-left: 10px; font-weight: bold;">Beneficiary Details</legend>
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
        </Triggers>
    </ajax:UpdatePanel>

</asp:Content>


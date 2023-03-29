<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="CropWise_Involvement.aspx.cs" Inherits="Reports_CropWise_Involvement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        Cropwise Details
    </div>
    <script>
        function Valid() {
            var Season = document.getElementById("<%=drpSeassion.ClientID %>");
            if (Season.value.trim() == "--- SELECT ---") {
                alert('Please Select Season !');
                Season.focus();
                return false;
            }
            var CropCatagory = document.getElementById("<%=drpCropName.ClientID %>");
            if (CropCatagory.value.trim() == "--- SELECT ---") {
                alert('Please Select Crop Name !');
                CropCatagory.focus();
                return false;
            }
        }
    </script>
    <br />
    <br />
    <br />
    <asp:UpdatePanel ID="upFarmerDtls" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 1120px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1100px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt">
                        <tr>
                            <td style="width: 115px;" class="normaltd">
                                Session Year
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>
                            </td>
                            <td style="width: 115px;" class="normaltd">
                                Season
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                    <asp:ListItem Value="--- SELECT ---">--- SELECT ---</asp:ListItem>
                                    <asp:ListItem Value="R">Rabi</asp:ListItem>
                                    <asp:ListItem Value="K">Kharif</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 115px;" class="normaltd">
                                Crop Name
                            </td>
                            <td style="width: 5px;">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpCropName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click"
                                    OnClientClick="return Valid();" />
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="litReport" runat="server"></asp:Literal>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="upFarmerDtls"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="text-align: center; vertical-align: middle; position: absolute; top: 0px;
                left: 0px; z-index: 99; width: 100%; height: 1000px; background-color: #ededed;
                -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60);
                -moz-opacity: 0.8; opacity: 0.8;" visible="true">
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
</asp:Content>

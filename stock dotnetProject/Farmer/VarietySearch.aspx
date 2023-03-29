<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/MasterPage.master" AutoEventWireup="true" CodeFile="VarietySearch.aspx.cs" Inherits="Farmer_VarietySearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>

        function Valid() {
            var FarmId = document.getElementById("<%=drpDist.ClientID %>");
               if (FarmId.value.trim() == "--- SELECT ---") {
                   alert('Please Select District !');
                   FarmId.focus();
                   return false;
               }

               var CropCatagory = document.getElementById("<%=drpCropName.ClientID %>");
            if (CropCatagory.value.trim() == "--- SELECT ---") {
                alert('Please Select Crop Name !');
                CropCatagory.focus();
                return false;
            }
            var CropName = document.getElementById("<%=drpCropVar.ClientID %>");
            if (CropName.value.trim() == "--- SELECT ---") {
                alert('Please Select Crop Variety !');
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
    <div>
     <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <asp:Panel ID="Panel1" runat="server" Style="padding-top: 10px; height:500px;">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt">
                        <tr>
                            <td style="width: 115px;" class="normaltd">Dist Name</td>
                            <td style="width: 5px;">:</td>
                            <td>

                                <asp:DropDownList ID="drpDist" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>

                            </td>
                            <td style="width: 115px;" class="normaltd">Crop Name</td>
                            <td style="width: 5px;">:</td>
                            <td>

                                <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpCropName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList></td>
                            <td style="width: 115px;" class="normaltd">Crop Variety</td>
                            <td style="width: 5px;">:</td>
                            <td>

                                <asp:DropDownList ID="drpCropVar" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td colspan="8"></td>
                            <td>

                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                            </td>
                        </tr>

                    </table>
                    <asp:Literal ID="litReport" runat="server"></asp:Literal>
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
    </ajax:UpdatePanel>
    </div>
</asp:Content>


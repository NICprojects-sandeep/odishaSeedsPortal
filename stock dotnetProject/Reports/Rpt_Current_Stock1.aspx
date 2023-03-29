<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Current_Stock1.aspx.cs" Inherits="Reports_Rpt_Current_Stock1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="scri1" runat="server"></asp:ScriptManager>
    <div>
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
                <div style="width: 1100px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1100px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">
                            <tr>
                                <td style="width: 115px;" class="normaltd">Session Year</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem>2016-17</asp:ListItem>
                                              <asp:ListItem>2017-18</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="width: 115px;" class="normaltd">User Type</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                    
                                    <asp:DropDownList ID="drpUser" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem>OSSC</asp:ListItem>
                                        <asp:ListItem>OAIC</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="normaltd">Crop Catagory<span class="mandatory">*</span></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="drpCropCatagory" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddl_CropCatagory_SelectedIndexChanged1">
                                    </asp:DropDownList></td>
                                <td class="normaltd">Crop Name<span class="mandatory">*</span></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td colspan="5"></td>
                                <td>

                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="98px" />
                                </td>
                            </tr>

                        </table>
                        <asp:Literal ID="litReport" runat="server"></asp:Literal>
                    </div>
                </div>
            </asp:Panel>
            </div>
            <%--            <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="updatepanel"
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
            </asp:UpdateProgress>--%>
        </ContentTemplate>

    </ajax:UpdatePanel>
    </div>
    </form>
</body>
</html>

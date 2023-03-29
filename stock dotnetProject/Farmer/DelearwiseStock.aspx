<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DelearwiseStock.aspx.cs" Inherits="Reports_DelearwiseStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../css/sms.css" type="text/css" />
    <link rel="stylesheet" href="../css/otb-admin.css" media="all" type="text/css" />
    <title></title>
    <script>

        function Valid() {
            var FarmId = document.getElementById("<%=drpCropName.ClientID %>");
            if (FarmId.value.trim() == "--- SELECT ---") {
                alert('Please Enter Farmer Id !');
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;">
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
        </div>
    </form>
</body>
</html>

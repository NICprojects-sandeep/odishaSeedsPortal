<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RPT_VarietywiseLift.aspx.cs" Inherits="Reports_RPT_VarietywiseLift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <script>

        function Valid() {

            var CropCatagory = document.getElementById("<%=drpCropName.ClientID %>");
             if (CropCatagory.value.trim() == "--- SELECT ---") {
                 alert('Please Select Crop Name !');
                 CropCatagory.focus();
                 return false;
             }
         }

    </script>
    <style>
        .tbltxt {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000;
            padding: 3px;
        }
    </style>

    <div>
            <div class="titleHeader">
        &nbsp;
        Varietywise Lift
    </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="tbltxt">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt" cellpadding="2" cellspacing="2">
                        <tr>
                            <td class="auto-style14">Session Year</td>
                            <td class="auto-style19">:</td>
                            <td class="auto-style15">
                                <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="drpSession_SelectedIndexChanged" >
                                </asp:DropDownList></td>

                            <td class="auto-style7">Season </td>
                            <td class="auto-style16">:</td>
                            <td class="auto-style17">
                                <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="drpSeassion_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--All--</asp:ListItem>
                                    <asp:ListItem Value="R">Rabi</asp:ListItem>
                                    <asp:ListItem Value="K">Kharif</asp:ListItem>
                                </asp:DropDownList></td>

                            <td class="auto-style18">Crop Name</td>
                            <td>:</td>
                            <td class="auto-style2">

                                <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList></td>

                        </tr>
                        <tr>

                            <td class="auto-style14">User Type</td>
                            <td class="auto-style19">:</td>
                            <td class="auto-style15">

                                <asp:DropDownList ID="drpUser" runat="server" CssClass="DropdownCss">
                                    <asp:ListItem Value="0">--ALL--</asp:ListItem>
                                    <asp:ListItem>OSSC</asp:ListItem>
                                    <asp:ListItem>OAIC</asp:ListItem>
                                </asp:DropDownList></td>


                            <td class="auto-style3">District</td>
                            <td class="auto-style16">:</td>
                            <td class="auto-style17">

                                <asp:DropDownList ID="drpDistrict" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList></td>


                            <td class="auto-style18">Month</td>
                            <td class="auto-style13">:</td>
                            <td class="auto-style12">

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
                        </tr>
                        <tr>
                            <td class="normaltd" colspan="6">From Date&nbsp; :&nbsp;&nbsp;&nbsp;
                                 <asp:TextBox ID="txtfromdate" runat="server" CssClass="TextboxCss" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="fromDatecalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="txtfromdate">
                                </asp:CalendarExtender>
                                &nbsp;&nbsp;&nbsp; To Date&nbsp; :&nbsp;&nbsp;
                                  <asp:TextBox ID="txtTodate" runat="server" CssClass="TextboxCss" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTodate"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                            </td>
                            <td class="auto-style8" colspan="3">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" OnClientClick="return Valid();" Text="Search" Width="98px" />
                                <asp:Button ID="btnExport" runat="server" OnClick="btnToExcel_Click" Text="Export to Excel" Width="128px" />
                        </tr>

                    </table>
                    <fieldset class="tbl">

                        <legend style="margin-left: 10px; font-weight: bold;">Figures in Qntl.</legend>
                        <div style="overflow: scroll; width: 1170px;">
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div style="height: 30px;">&nbsp;</div>
        </asp:Panel>
    </div>
</asp:Content>


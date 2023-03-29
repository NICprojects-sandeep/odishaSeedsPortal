<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="rpt_MonthwiseStock.aspx.cs" Inherits="Reports_rpt_MonthwiseStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script>

        function Valid() {

            var CropName = document.getElementById("<%=drpMonth.ClientID %>");
            if (CropName.value.trim() == "--- SELECT ---") {
                alert('Please Select Month Name !');
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
    <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;">
        <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
        <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />
        <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
            <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                <table style="width: 100%; padding: 10px;" class="tabletxt">
                    <tr>
                        <td style="width: 115px;" class="normaltd">Session Year</td>
                        <td style="width: 5px;">:</td>
                        <td style="width: 154px">

                            <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss" Width="100px">
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                            </asp:DropDownList>
                        <td style="width: 110px;" class="normaltd">Season</td>
                                <td style="width: 5px;">:</td>
                                <td>
                                 <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                       <asp:ListItem Value="0">--All--</asp:ListItem>
                                       <asp:ListItem Value="R">Rabi</asp:ListItem>
                                       <asp:ListItem Value="K">Kharif</asp:ListItem>
                                    </asp:DropDownList>
                        </td>
                        <td style="width: 115px;" class="normaltd">Month Name</td>
                        <td style="width: 5px;">:</td>
                        <td style="width: 215px;">

                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="DropdownCss" Width="130px">
                            </asp:DropDownList></td>

                        
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                            </td>
                    </tr>

                </table>
                <asp:Literal ID="litReport" runat="server"></asp:Literal>
            </div>
        </div>
    </asp:Panel>
</asp:Content>


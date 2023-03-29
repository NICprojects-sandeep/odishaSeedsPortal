<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="BlockWiseLifting.aspx.cs" Inherits="Reports_BlockWiseLifting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><asp:Panel ID="Panel1" runat="server">
        <div style="width: 1200px; background-color: #666; padding: 2px; margin: 0 auto;">
            <div style="background-color: #FFF; padding: 10px; overflow: scroll; width: 1200px;">
                <table style="width: 100%; padding: 10px;" class="tabletxt">
                    <tr>
                        <td style="width: 115px; text-align: left;">Dist Name</td>
                        <td style="width: 5px;">:</td>
                        <td>
                            <asp:DropDownList ID="drpDist" runat="server" CssClass="DropdownCss">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 115px; text-align: right">&nbsp;</td>
                        <td style="width: 5px;">&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td colspan="4">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClientClick="return Valid();" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnToExcel" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                        </td>
                    </tr>
                </table>
                <fieldset class="tbl">
                    <legend style="margin-left: 10px; font-weight: bold;">Block wise Stock</legend>
                    <asp:Literal ID="litReport" runat="server"></asp:Literal>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
</asp:Content>


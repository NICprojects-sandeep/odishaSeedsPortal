<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="BGREIPaymentStatus.aspx.cs" Inherits="Reports_BGREIPaymentStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        BGREI Payment Status
    </div>
        <br />
    <br />
    <br />
    <div style="width: 1120px; background-color: #666; padding: 2px; margin: 0 auto;">
        <div style="background-color: #FFF; padding: 10px; width: 1100px;">
            <table style="width: 100%; padding: 10px;" class="tabletxt">
                <tr>
                    <td style="width: 115px;" class="normaltd">Session Year
                    </td>
                    <td style="width: 5px;">:
                    </td>
                    <td style="width: 225px">
                        <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>
                    </td>
                    <td style="width: 91px;" class="normaltd">Season
                    </td>
                    <td style="width: 5px;">:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                            <asp:ListItem Value="0">---All---</asp:ListItem>
                            <asp:ListItem Value="R">Rabi</asp:ListItem>
                            <asp:ListItem Value="K">Kharif</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" />

                    </td>

                </tr>
                <tr>
                    <td colspan="5"></td>
                    <td></td>
                </tr>
            </table>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </div>

    <asp:Literal ID="litReport" runat="server"></asp:Literal>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="OSSC_Farmer_Incentive_OilSeedAll.aspx.cs" Inherits="Reports_OSSC_Farmer_Incentive_OilSeedAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <table width="100%">
        <tr>
            <td style="font-weight:bold;color:maroon;text-align:center;font-size:15px;">
                Agency : <asp:Label ID="lblAgency" runat="server" Text="" ForeColor="Green"></asp:Label>,&nbsp;&nbsp;&nbsp;&nbsp;
                Crop : <asp:Label ID="lblCrop" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                Financial Year :&nbsp; <asp:DropDownList ID="ddlFinYear" runat="server">
                    <asp:ListItem>2019-20</asp:ListItem>
                    <asp:ListItem>2020-21</asp:ListItem>
                    <asp:ListItem>2021-22</asp:ListItem>
                </asp:DropDownList>
                &nbsp; <asp:Label ID="lblFinYr" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                Season :&nbsp; <asp:DropDownList ID="ddlSeason" runat="server">
                    <asp:ListItem>Rabi</asp:ListItem>
                    <asp:ListItem>Kharif</asp:ListItem>
                </asp:DropDownList>
                &nbsp; <asp:Label ID="lblSeason" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />&nbsp;&nbsp;&nbsp;&nbsp;
                Variety Type : All Varieties
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow: scroll; width: 1170px;">
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
    </div>
    <div id="dvOSSC" runat="server">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" Height="39px" OnClick="btnApprove_Click" Width="98px" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvDEPT" runat="server">
        <table width="100%">
            <tr>
                <td align="center" width="50%">
                    <asp:TextBox ID="txtTrn" runat="server" TextMode="MultiLine" ToolTip="Transaction Numbers" placeholder="Transaction Numbers" Width="100%"></asp:TextBox>
                </td>
                <td align="center" width="20%">
                    <asp:Button ID="btnPay" runat="server" Text="Pay" Height="39px" OnClick="btnPay_Click" Width="100%"/>
                </td>
                <td align="center" width="30%"></td>
            </tr>
        </table>
    </div>
</asp:Content>

